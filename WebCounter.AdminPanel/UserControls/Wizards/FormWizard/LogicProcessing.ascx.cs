using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class LogicProcessing : FormWizardStep
    {
        public ContactData contactData;


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            contactData = new ContactData(CurrentUser.Instance.SiteID);

            var siteActivityRuleLayout = new List<tbl_SiteActivityRuleLayout>();            

            ddlOutputFormatFields.Items.Clear();
            ddlOutputFormatFields.Items.Add(new ListItem("Заголовок над элементом", "1"));
            ddlOutputFormatFields.Items.Add(new ListItem("Заголовок слева от элемента", "2"));
            ddlOutputFormatFields.Items.Add(new ListItem("Без заголовка", "3"));
            ddlOutputFormatFields.Items.Add(new ListItem("Заголовок в элементе", "4"));

            var siteColumns = DataManager.SiteColumns.SelectAll(CurrentUser.Instance.SiteID).Where(a => a.SiteActivityRuleID == null).ToList();
            if (IsEditMode)
            {
                var siteActivityRule = DataManager.SiteActivityRules.SelectById(EditObjectId.Value);

                if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Feedback))
                {
                    plNotFeedBack.Visible = false;
                    plFeedBack.Visible = true;
                    BindFeedBackAttributes(siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Feedback).LayoutParams);
                }
                else
                {
                    siteActivityRuleLayout = siteActivityRule.tbl_SiteActivityRuleLayout.Where(o => o.ParentID.HasValue && IsContainer(o.ParentID)).ToList();

                    var fields = contactData.GetCollection(siteActivityRuleId: siteActivityRule.ID);

                    //var filteredSiteColumns = siteColumns.Where(z => !siteActivityRuleLayout.Select(o => o.Name).Contains(z.Name)).ToList();
                    var filteredSiteColumns = fields.Where(z => !siteActivityRuleLayout.Select(o => o.Name).Contains(z.Name)).ToList();
                    rlbSource.DataSource = filteredSiteColumns;

                    //rlbDestination.DataSource = siteActivityRuleLayout.Where(o => o.SiteColumnID.HasValue).OrderBy(o => o.Order).Select(o => o.tbl_SiteColumns);
                    var layouts = siteActivityRuleLayout.Where(o => o.SiteColumnID.HasValue || !string.IsNullOrEmpty(o.SysField)).OrderBy(o => o.Order);
                    foreach (var layout in layouts)
                    {
                        FieldCollection fieldCollection;
                        fieldCollection = layout.SiteColumnID.HasValue ? fields.FirstOrDefault(a => a.Value == layout.SiteColumnID.ToString()) : fields.FirstOrDefault(a => a.Value == layout.SysField);
                        if (fieldCollection != null)
                            rlbDestination.Items.Add(new RadListBoxItem(fieldCollection.Name, fieldCollection.Value));
                    }
                    rlbDestination.DataTextField = "Name";
                    rlbDestination.DataValueField = "ID";
                    rlbDestination.DataBind();

                    if (siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => IsContainer(o.ID)) != null)
                        ddlOutputFormatFields.SelectedIndex = ddlOutputFormatFields.FindItemIndexByValue(siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => IsContainer(o.ID)).OutputFormat.ToString());
                }
            }
            else
            {
                var siteActivityRule = DataManager.SiteActivityRules.SelectById(CurrentForm);
                if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Feedback))
                {
                    plNotFeedBack.Visible = false;
                    plFeedBack.Visible = true;
                    BindFeedBackAttributes(siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Feedback).LayoutParams);
                }
                else
                {
                    //rlbSource.DataSource = siteColumns;
                    rlbSource.DataSource = contactData.GetCollection();
                }
            }

            if (plNotFeedBack.Visible)
            {
                rlbSource.DataValueField = "Value";
                rlbSource.DataTextField = "Name";
                rlbSource.DataBind();

                /*if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Phone))
                    rlbSource.Items.Insert(0, new RadListBoxItem("Телефон", "sys_phone"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Phone).Order ?? 0, new RadListBoxItem("Телефон", "sys_phone"));

                if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Email))
                    rlbSource.Items.Insert(0, new RadListBoxItem("E-mail", "sys_email"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Email).Order ?? 0, new RadListBoxItem("E-mail", "sys_email"));

                if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Patronymic))
                    rlbSource.Items.Insert(0, new RadListBoxItem("Отчество", "sys_patronymic"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Patronymic).Order ?? 0, new RadListBoxItem("Отчество", "sys_patronymic"));

                if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Name))
                    rlbSource.Items.Insert(0, new RadListBoxItem("Имя", "sys_name"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Name).Order ?? 0, new RadListBoxItem("Имя", "sys_name"));

                if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.Surname))
                    rlbSource.Items.Insert(0, new RadListBoxItem("Фамилия", "sys_surname"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.Surname).Order ?? 0, new RadListBoxItem("Фамилия", "sys_surname"));

                if (!siteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.FullName))
                    rlbSource.Items.Insert(0, new RadListBoxItem("Ф.И.О.", "sys_fullname"));
                else
                    rlbDestination.Items.Insert(siteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.FullName).Order ?? 0, new RadListBoxItem("Ф.И.О.", "sys_fullname"));*/
            }
        }


        protected void BindFeedBackAttributes(string lp)
        {
            rblStep.Items.Clear();
            
            foreach (var step in EnumHelper.EnumToList<FormFeedBackSteps>())
                rblStep.Items.Add(new ListItem(EnumHelper.GetEnumDescription(step), ((int)step).ToString()));

            rblKnowledgeBase.Items.Clear();
            foreach (var knowledgeBase in EnumHelper.EnumToList<FormFeedBackKnowledgeBase>())
                rblKnowledgeBase.Items.Add(new ListItem(EnumHelper.GetEnumDescription(knowledgeBase), ((int)knowledgeBase).ToString()));

            var publicationTypes = DataManager.PublicationType.SelectByPublicationKindID(CurrentUser.Instance.SiteID, (int)PublicationKind.Discussion);
            chxPublicationType.DataSource = publicationTypes;
            chxPublicationType.DataTextField = "Title";
            chxPublicationType.DataValueField = "ID";
            chxPublicationType.DataBind();

            if (!string.IsNullOrEmpty(lp))
            {
                var layoutParams = LayoutParams.Deserialize(lp);
                rblStep.SelectedIndex = rblStep.Items.IndexOf(rblStep.Items.FindByValue(layoutParams.GetValue("step")));
                rblKnowledgeBase.SelectedIndex = rblKnowledgeBase.Items.IndexOf(rblKnowledgeBase.Items.FindByValue(layoutParams.GetValue("kb")));
                var publicationTypeIds = layoutParams.GetValue("pt").Split(',');
                foreach (var publicationTypeId in publicationTypeIds)
                    if (!string.IsNullOrEmpty(publicationTypeId) && chxPublicationType.Items.FindByValue(publicationTypeId) != null)
                        chxPublicationType.Items.FindByValue(publicationTypeId).Selected = true;
            }
            else
            {
                rblStep.Items.FindByValue("1").Selected = true;
                rblKnowledgeBase.Items.FindByValue("1").Selected = true;
            }
        }


        /// <summary>
        /// Determines whether the specified parent id is container.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <returns>
        ///   <c>true</c> if the specified parent id is container; otherwise, <c>false</c>.
        /// </returns>
        private bool IsContainer(Guid? parentId)
        {
            var siteActivityRuleLayout = DataManager.SiteActivityRuleLayout.SelectById(parentId.Value);

            if (string.IsNullOrEmpty(siteActivityRuleLayout.LayoutParams))
                return false;

            var lp = LayoutParams.Deserialize(siteActivityRuleLayout.LayoutParams);
            if (!string.IsNullOrEmpty(lp.GetValue("IsUsedForAdditionalDetails")) && bool.Parse(lp.GetValue("IsUsedForAdditionalDetails")))
                return true;

            return false;
        }
    }
}