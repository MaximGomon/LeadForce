using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.FormCode;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class FormWizard : System.Web.UI.UserControl
    {
        public bool IsEditMode
        {
            get
            {
                if (ViewState["IsEditMode"] == null)
                    ViewState["IsEditMode"] = false;

                return (bool)ViewState["IsEditMode"];
            }
            set { ViewState["IsEditMode"] = value; }
        }


        public Guid? EditFormId
        {
            get { return (Guid?) ViewState["EditFormId"]; }
            set { ViewState["EditFormId"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                if (!IsEditMode)
                {
                    rtsWizard.AutoPostBack = true;
                    rtsWizard.TabClick += rtsWizard_OnTabClick;
                    AddTab("Выбор типа формы", "SelectFormType", true);                    
                    rmpWizard.PageViews.Add(new RadPageView { ID = "SelectFormType" });
                    AddTab("Инструкции и тексты", "InstructionAndText", false);                    
                    AddTab("Дизайн формы", "Design", false);                    
                    AddTab("Логика обработки", "LogicProcessing", false);                    
                    AddTab("Действие после обработки ", "ActionAfterProcessing", false);                    
                }
                else
                {                    
                    rtsWizard.AutoPostBack = false;
                    plButtons.Visible = true;
                    hlCancel.NavigateUrl = UrlsData.AP_SiteActivityRules((int)RuleType.Form);
                    AddTab("Инструкции и тексты", "InstructionAndText", true);                    
                    rmpWizard.PageViews.Add(new RadPageView { ID = "InstructionAndText" });
                    AddTab("Дизайн формы", "Design", true);
                    rmpWizard.PageViews.Add(new RadPageView { ID = "Design" });
                    var dataManager = new DataManager();
                    var siteActivityRule = dataManager.SiteActivityRules.SelectById(EditFormId.Value);
                    if (!siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
                    {
                        AddTab("Логика обработки", "LogicProcessing", true);
                        rmpWizard.PageViews.Add(new RadPageView { ID = "LogicProcessing" });
                    }
                    AddTab("Действие после обработки ", "ActionAfterProcessing", true);
                    rmpWizard.PageViews.Add(new RadPageView { ID = "ActionAfterProcessing" });
                }
            }
        }



        /// <summary>
        /// Adds the tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="value">The value.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void AddTab(string tabName, string value, bool enabled)
        {
            var tab = new RadTab(tabName) { Enabled = enabled, Value = value };            
            rtsWizard.Tabs.Add(tab);
        }



        /// <summary>
        /// Clears the pageviews.
        /// </summary>
        public void ClearPageviews()
        {
            if (rmpWizard.PageViews.Count > 1)
            {
                for (int i = 1; i < rtsWizard.Tabs.Count; i++)
                {
                    var pageView = rmpWizard.FindPageViewByID(rtsWizard.Tabs[i].Value);
                    rtsWizard.Tabs[i].Enabled = false;
                    if (pageView != null)
                        rmpWizard.PageViews.Remove(pageView);
                }
                //((BaseWizardStep)FindControl(rmpWizard.PageViews[0].ID + "UserControl")).BindData();
            }

            //Хак для первого шага без кнопки
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "GoToNextStep", "GoToNextStep();", true);
        }



        /// <summary>
        /// Handles the OnPageViewCreated event of the rmpWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadMultiPageEventArgs"/> instance containing the event data.</param>
        protected void rmpWizard_OnPageViewCreated(object sender, RadMultiPageEventArgs e)
        {
            var pageViewContents = (BaseWizardStep)LoadControl("~/UserControls/Wizards/FormWizard/" + e.PageView.ID + ".ascx");
            pageViewContents.IsEditMode = IsEditMode;
            pageViewContents.EditObjectId = EditFormId;
            pageViewContents.BindData();
            pageViewContents.ID = e.PageView.ID + "UserControl";
            e.PageView.Controls.Add(pageViewContents);
        }



        /// <summary>
        /// Handles the OnAjaxRequest event of the rapFormWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void rapFormWizard_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Clear")
                ClearPageviews();
        }



        /// <summary>
        /// Handles the OnTabClick event of the rtsWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void rtsWizard_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            rtsWizard.ValidationGroup = e.Tab.Value;
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (EditFormId.HasValue)
                Save(EditFormId.Value);
        }



        /// <summary>
        /// Saves the specified site activity rule id.
        /// </summary>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        public void Save(Guid siteActivityRuleId)
        {
            var dataManager = new DataManager();

            var txtFormTitle = ((TextBox)rmpWizard.FindPageViewByID("InstructionAndText").Controls[0].FindControl("txtFormTitle"));
            var txtTextButton = ((TextBox)rmpWizard.FindPageViewByID("InstructionAndText").Controls[0].FindControl("txtTextButton"));
            var txtFormWidth = ((RadNumericTextBox)rmpWizard.FindPageViewByID("Design").Controls[0].FindControl("txtFormWidth"));
            var ucCssEditorInstruction = ((CssEditor)rmpWizard.FindPageViewByID("Design").Controls[0].FindControl("ucCssEditorInstruction"));
            var ucCssEditorColumns = ((CssEditor)rmpWizard.FindPageViewByID("Design").Controls[0].FindControl("ucCssEditorColumns"));
            var ucCssEditorButton = ((CssEditor)rmpWizard.FindPageViewByID("Design").Controls[0].FindControl("ucCssEditorButton"));
            var rcpBackgroundColor = ((RadColorPicker)rmpWizard.FindPageViewByID("Design").Controls[0].FindControl("rcpBackgroundColor"));            
            var txtUrl = ((TextBox)rmpWizard.FindPageViewByID("ActionAfterProcessing").Controls[0].FindControl("txtUrl"));
            var plInviteFriendSettings = ((Panel)rmpWizard.FindPageViewByID("ActionAfterProcessing").Controls[0].FindControl("plInviteFriendSettings"));
            var dcbWorkflowTemplate = ((DictionaryOnDemandComboBox)rmpWizard.FindPageViewByID("ActionAfterProcessing").Controls[0].FindControl("dcbWorkflowTemplate"));
            var ucPopupSiteActionTemplate = ((PopupSiteActionTemplate)rmpWizard.FindPageViewByID("ActionAfterProcessing").Controls[0].FindControl("ucPopupSiteActionTemplate"));

            tbl_SiteActivityRules siteActivityRule = null;
            if (!IsEditMode)
            {
                siteActivityRule = dataManager.SiteActivityRules.CopyByID(siteActivityRuleId);
                siteActivityRule.TemplateID = siteActivityRuleId;
                siteActivityRule.SiteID = CurrentUser.Instance.SiteID;                
                siteActivityRule.Code = string.Concat("form_", DateTime.Now.ToString("[ddMMyyyy]_[mmss]"));

                var currentSiteColumnCategories = dataManager.ColumnCategories.SelectAll(CurrentUser.Instance.SiteID);

                foreach (tbl_SiteActivityRuleLayout ruleLayout in siteActivityRule.tbl_SiteActivityRuleLayout)                
                    ruleLayout.SiteID = CurrentUser.Instance.SiteID;
                foreach (tbl_SiteColumns siteColumns in dataManager.SiteColumns.SelectByActivityRuleId(siteActivityRule.ID))
                {
                    siteColumns.SiteID = CurrentUser.Instance.SiteID;
                    var columnCategory = currentSiteColumnCategories.SingleOrDefault(o => o.Title == siteColumns.tbl_ColumnCategories.Title) ?? currentSiteColumnCategories.FirstOrDefault();
                    siteColumns.CategoryID = columnCategory.ID;
                }                
            }
            else
                siteActivityRule = dataManager.SiteActivityRules.SelectById(siteActivityRuleId);

            
            siteActivityRule.Name = txtFormTitle.Text;
            siteActivityRule.TextButton = txtTextButton.Text;
            siteActivityRule.RepostURL = txtUrl.Text;
            siteActivityRule.FormWidth = (int?)txtFormWidth.Value;
            siteActivityRule.CSSButton = ucCssEditorButton.GetCss();                        
            siteActivityRule.CSSForm = EncodeBackgroundCss(rcpBackgroundColor);

            var order = 0;

            var existsiteActivityRule = dataManager.SiteActivityRules.SelectById(siteActivityRuleId);
            foreach (var activityRuleLayout in existsiteActivityRule.tbl_SiteActivityRuleLayout)
            {
                if (string.IsNullOrEmpty(activityRuleLayout.LayoutParams))
                    continue;

                var toUpdate = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.Name == activityRuleLayout.Name);

                if (toUpdate == null)
                    continue;

                var lp = LayoutParams.Deserialize(activityRuleLayout.LayoutParams);
                if (!string.IsNullOrEmpty(lp.GetValue("ShowInMaster")))
                {
                    switch ((ShowTextBlockInMaster)int.Parse(lp.GetValue("ShowInMaster")))
                    {
                        case ShowTextBlockInMaster.Text:
                            var text = ((TextBox)FindControlRecursive(rmpWizard.FindPageViewByID("InstructionAndText").Controls[0], activityRuleLayout.ID.ToString())).Text;
                            text = text.Replace("\n", "#@#");
                            text = Sanitizer.GetSafeHtmlFragment(text);
                            text = text.Replace("#@#", "\n");
                            toUpdate.Description = text;
                            break;
                        case ShowTextBlockInMaster.HTML:
                            toUpdate.Description = ((RadEditor)FindControlRecursive(rmpWizard.FindPageViewByID("InstructionAndText").Controls[0], activityRuleLayout.ID.ToString())).Content;
                            break;
                    }
                    toUpdate.CSSStyle = ucCssEditorInstruction.GetCss();
                }                
            }

            foreach (tbl_SiteActivityRuleLayout ruleLayout in siteActivityRule.tbl_SiteActivityRuleLayout)
            {
                if (string.IsNullOrEmpty(ruleLayout.LayoutParams) && ruleLayout.LayoutType != (int)LayoutType.Feedback
                     && ruleLayout.LayoutType != (int)LayoutType.GroupFields && ruleLayout.LayoutType != (int)LayoutType.InviteFriend
                     && ruleLayout.LayoutType != (int)LayoutType.Root && ruleLayout.LayoutType != (int)LayoutType.TextBlock)
                    ruleLayout.CSSStyle = ucCssEditorColumns.GetCss();
            }

            if (rmpWizard.FindPageViewByID("LogicProcessing") != null)
            {
                var ddlOutputFormatFields = ((DropDownList)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("ddlOutputFormatFields"));
                var rlbDestination = ((RadListBox)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("rlbDestination"));
                var plFeedBack = ((Panel)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("plFeedBack"));
                var rblStep = ((RadioButtonList)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("rblStep"));
                var rblKnowledgeBase = ((RadioButtonList)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("rblKnowledgeBase"));
                var chxPublicationType = ((CheckBoxList)rmpWizard.FindPageViewByID("LogicProcessing").Controls[0].FindControl("chxPublicationType"));

                tbl_SiteActivityRuleLayout parentSiteActivityRuleLayout =
                    (from ruleLayout in siteActivityRule.tbl_SiteActivityRuleLayout
                     where !string.IsNullOrEmpty(ruleLayout.LayoutParams)
                     let layoutParams = LayoutParams.Deserialize(ruleLayout.LayoutParams)
                     where
                         !string.IsNullOrEmpty(layoutParams.GetValue("IsUsedForAdditionalDetails")) &&
                         bool.Parse(layoutParams.GetValue("IsUsedForAdditionalDetails"))
                     select ruleLayout).FirstOrDefault();

                if (parentSiteActivityRuleLayout != null)
                {
                    var idsToDelete =
                        siteActivityRule.tbl_SiteActivityRuleLayout.Where(
                            o => o.ParentID == parentSiteActivityRuleLayout.ID).Select(o => o.ID).ToList();
                    foreach (Guid id in idsToDelete)
                    {
                        dataManager.SiteActivityRuleLayout.Delete(id);
                    }

                    foreach (RadListBoxItem item in rlbDestination.Items)
                    {
                        var siteActivityRuleLayout = new tbl_SiteActivityRuleLayout
                                                         {
                                                             ID = Guid.NewGuid(),
                                                             SiteID = CurrentUser.Instance.SiteID,
                                                             SiteActivityRuleID = siteActivityRule.ID,
                                                             Order = order,
                                                             Name = item.Text,
                                                             ParentID = parentSiteActivityRuleLayout.ID,
                                                             LayoutType = (int)LayoutType.ProfileField
                                                         };

                        Guid outSiteColumnId;
                        if (Guid.TryParse(item.Value, out outSiteColumnId))
                        {
                            siteActivityRuleLayout.SiteColumnID = outSiteColumnId;
                            siteActivityRuleLayout.SysField = null;
                        }
                        else
                        {
                            siteActivityRuleLayout.SiteColumnID = null;
                            siteActivityRuleLayout.SysField = item.Value;
                        }

                        /*switch (item.Value)
                        {
                            case "sys_fullname":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.FullName;
                                break;
                            case "sys_email":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.Email;
                                break;
                            case "sys_phone":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.Phone;
                                break;
                            case "sys_surname":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.Surname;
                                break;
                            case "sys_name":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.Name;
                                break;
                            case "sys_patronymic":
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.Patronymic;
                                break;
                            default:
                                siteActivityRuleLayout.SiteColumnID = Guid.Parse(item.Value);
                                siteActivityRuleLayout.LayoutType = (int) LayoutType.ProfileField;
                                break;
                        }*/
                        order++;

                        siteActivityRule.tbl_SiteActivityRuleLayout.Add(siteActivityRuleLayout);
                    }

                    parentSiteActivityRuleLayout.OutputFormatFields = int.Parse(ddlOutputFormatFields.SelectedValue);

                    dataManager.SiteActivityRuleLayout.Update(parentSiteActivityRuleLayout);
                }

                if (plFeedBack.Visible)
                {
                    var feedBackComponent = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int) LayoutType.Feedback);
                    if (feedBackComponent != null)
                    {
                        var layoutParams = new List<LayoutParams>();
                        layoutParams.Add(new LayoutParams() { Name = "step", Value = rblStep.SelectedValue });
                        layoutParams.Add(new LayoutParams() { Name = "kb", Value = rblKnowledgeBase.SelectedValue });
                        var publicationTypeValues = (from ListItem item in chxPublicationType.Items where item.Selected select item.Value).ToList();
                        layoutParams.Add(new LayoutParams() { Name = "pt", Value = string.Join(",", publicationTypeValues) });
                        feedBackComponent.LayoutParams = LayoutParams.Serialize(layoutParams);
                    }
                }                
            }

            if (plInviteFriendSettings.Visible)
            {
                var inviteFriendComponent = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.InviteFriend);
                if (inviteFriendComponent != null)
                {
                    var lp = new List<LayoutParams>();
                    var item = new LayoutParams
                                   {
                                       Name = "WorkflowTemplateID",
                                       Value = dcbWorkflowTemplate.SelectedIdNullable.HasValue
                                               ? dcbWorkflowTemplate.SelectedId.ToString()
                                               : string.Empty
                                   };
                    lp.Add(item);

                    item = new LayoutParams
                               {
                                   Name = "SiteActionTemplateID",
                                   Value = ucPopupSiteActionTemplate.SiteActionTemplateId != Guid.Empty
                                           ? ucPopupSiteActionTemplate.SiteActionTemplateId.ToString()
                                           : string.Empty
                               };
                    lp.Add(item);

                    inviteFriendComponent.LayoutParams = LayoutParams.Serialize(lp);
                }
            }                                   

            dataManager.SiteActivityRules.Update(siteActivityRule);

            Response.Redirect(UrlsData.AP_SiteActivityRules((int)RuleType.ExternalForm));
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id) return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null) return rc;
            }
            return null;
        }




        /// <summary>
        /// Encodes the CSS.
        /// </summary>
        /// <returns></returns>
        protected string EncodeBackgroundCss(RadColorPicker rcpBackgroundColor)
        {
            var css = new StringBuilder();

            if (!rcpBackgroundColor.SelectedColor.IsEmpty)
                css.AppendFormat("background-color:{0};", ColorTranslator.ToHtml(rcpBackgroundColor.SelectedColor));

            if (css.ToString() == "")
                return null;

            return css.ToString();
        }
    }
}