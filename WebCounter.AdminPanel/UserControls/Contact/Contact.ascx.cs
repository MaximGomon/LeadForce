using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using Page = Labitec.UI.BaseWorkspace.Page;

namespace WebCounter.AdminPanel.UserControls.Contact
{
    public partial class Contact : System.Web.UI.UserControl
    {
        public Access access;
        public Guid _contactID;
        public Guid SiteId = new Guid();
        private DataManager dataManager = new DataManager();



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Section Section
        {
            get
            {
                object o = ViewState["Section"];
                return (o == null ? Section.Monitoring : (Section)o);
            }
            set
            {
                ViewState["Section"] = value;
            }
        }




        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteId = ((LeadForceBasePage)Page).SiteId;

            ((Site)Page.Master).HideInaccessibleTabs(ref RadTabStrip1, ref RadMultiPage1);

            string contactId = Page.RouteData.Values["id"] as string;

            var category = Request.QueryString["c"];
            if (!string.IsNullOrEmpty(category))
                hlCancel.NavigateUrl = UrlsData.AP_Contacts() + "?c=" + category;
            else
                hlCancel.NavigateUrl = UrlsData.AP_Contacts();
            

            if (!Guid.TryParse(contactId, out _contactID))
            {
                Response.Redirect(UrlsData.AP_Contacts());
            }
            //@@@
            ucTaskList.ContactId = _contactID;
            ucTaskList.SiteId = SiteId;

            if (!Page.IsPostBack)
            {
                ucContactEdit.ContactId = _contactID;
                ucContactEdit.SiteId = SiteId;
                ucContactEdit.Section = Section;
                ucContactEdit.BindData();

                BindSiteActivityRules();                

                tagsContact.ObjectID = ucContactEdit.ContactId;

                dcbWorkflowTemplate.SiteID = SiteId;
                dcbWorkflowTemplate.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn
                                                    {
                                                        Name = "ManualStart",
                                                        DbType = DbType.Boolean,
                                                        Value = "TRUE"
                                                    });
                dcbWorkflowTemplate.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn
                                                    {
                                                        Name = "Status",
                                                        DbType = DbType.Int32,
                                                        Value = "1"
                                                    });
                dcbWorkflowTemplate.BindData();
            }

            aWorkflow.Attributes.Add("onclick", this.ClientID + "_ShowWorkflowTemplateRadWindow();");
            aWorkflow.Visible = ((LeadForceBasePage)Page).IsDefaultEdition;

            access = Access.Check(ucContactEdit.OwnerId, ucContactEdit.CompanyId);
            if (!access.Write)
            {
                BtnFillForm.Visible = false;
                //hlSendMessage.Visible = false;
                lbtnSave.Visible = false;
            }

            gridContactColumnValues.Where = new List<GridWhere>();
            gridContactColumnValues.Where.Add(new GridWhere { Column = "ContactID", Value = _contactID.ToString() });

            //@@@
            gridContactSessions.Actions.Add(new GridAction { Text = "Карточка сессии", NavigateUrl = string.Format("~/{0}/Contacts/Session/View/{{0}}", Page.RouteData.Values["tab"] as string), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
            gridContactSessions.SiteID = SiteId;
            gridContactSessions.Where = new List<GridWhere>();
            gridContactSessions.Where.Add(new GridWhere { Column = "ContactID", Value = _contactID.ToString() });
        }




        /// <summary>
        /// Binds the site activity rules.
        /// </summary>
        private void BindSiteActivityRules()
        {
            bool sortAscending = this.SortDirection == SortDirection.Ascending ? true : false;
            var siteActivityRules = new List<tbl_SiteActivityRules>();
            siteActivityRules = dataManager.SiteActivityRules.SelectAll(SiteId).Where(a => (RuleType)a.RuleTypeID == RuleType.Form).ToList();

            switch (this.SortExpression)
            {
                case "Name":
                    siteActivityRules = sortAscending ? siteActivityRules.OrderBy(u => u.Name).ToList() : siteActivityRules.OrderByDescending(u => u.Name).ToList();
                    break;
                case "Code":
                    siteActivityRules = sortAscending ? siteActivityRules.OrderBy(u => u.Code).ToList() : siteActivityRules.OrderByDescending(u => u.Code).ToList();
                    break;
            }

            gvSiteActivityRules.DataSource = siteActivityRules;
            gvSiteActivityRules.DataBind();
        }




        /// <summary>
        /// Handles the PageIndexChanging event of the gvSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvSiteActivityRules_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSiteActivityRules.PageIndex = e.NewPageIndex;
            BindSiteActivityRules();
        }




        /// <summary>
        /// Handles the Sorting event of the gvSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gvSiteActivityRules_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.SortExpression == e.SortExpression)
            {
                this.SortDirection = this.SortDirection == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                this.SortDirection = SortDirection.Ascending;
            }
            this.SortExpression = e.SortExpression;
            gvSiteActivityRules.EditIndex = -1;
            gvSiteActivityRules.SelectedIndex = -1;
        }



        /// <summary>
        /// Handles the Sorted event of the gvSiteActivityRules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gvSiteActivityRules_Sorted(object sender, EventArgs e)
        {
            BindSiteActivityRules();
        }



        /// <summary>
        /// Handles the Click event of the BtnFillForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnFillForm_Click(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            BindSiteActivityRules();
            rttSiteActivityRules.Show();
        }



        protected string SortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }



        protected SortDirection SortDirection
        {
            get
            {
                object o = ViewState["SortDirection"];
                if (o == null)
                    return SortDirection.Ascending;
                else
                    return (SortDirection)o;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }



        protected void gridContactColumnValues_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                switch ((ColumnType)data["tbl_SiteColumns_TypeID"])
                {
                    case ColumnType.String:
                    case ColumnType.Number:
                    case ColumnType.Text:
                        ((Literal)item.FindControl("ContactColumnValue")).Text = data["tbl_ContactColumnValues_StringValue"].ToString();
                        break;
                    case ColumnType.Date:
                        ((Literal)item.FindControl("ContactColumnValue")).Text = data["tbl_ContactColumnValues_DateValue"].ToString();
                        break;
                    case ColumnType.Enum:
                        ((Literal)item.FindControl("ContactColumnValue")).Text = data["tbl_SiteColumnValues_Value"].ToString();
                        break;
                    case ColumnType.Logical:
                        ((Literal)item.FindControl("ContactColumnValue")).Text = bool.Parse(data["tbl_ContactColumnValues_LogicalValue"].ToString()) ? "Да" : "Нет";
                        break;
                }
            }
        }



        protected void gridContactSessions_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lUserSessionNumber")).Text = string.Format("Посещение {0}", data["tbl_ContactSessions_UserSessionNumber"]);
                ((Literal)item.FindControl("lBrowser")).Text = string.Format("{0} {1}", data["tbl_Browsers_Name"], data["tbl_Browsers_Version"]);
                ((Literal)item.FindControl("lOperatingSystem")).Text = string.Format("{0} {1}", data["tbl_OperatingSystems_Name"], data["tbl_OperatingSystems_Version"]);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var saveResult = ucContactEdit.Save();
            tagsContact.SaveTags(ucContactEdit.ContactId);

            if (saveResult)
            {
                var category = Request.QueryString["c"];
                if (!string.IsNullOrEmpty(category))
                    Response.Redirect(UrlsData.AP_Contacts() + "?c=" + category);
                else
                    Response.Redirect(UrlsData.AP_Contacts());
            }
                
        }



        /// <summary>
        /// Handles the OnClick event of the lbRunWorkflow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbRunWorkflow_OnClick(object sender, EventArgs e)
        {
            dataManager.WorkflowTemplate.WorkflowInit(_contactID, dcbWorkflowTemplate.SelectedId);

            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_CloseWorkflowTemplateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_CloseWorkflowTemplateRadWindow", this.ClientID + "_CloseWorkflowTemplateRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnTemplateSaved event of the ucPopupSiteActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.PopupSiteActionTemplate.TemplateSavedEventArgs"/> instance containing the event data.</param>
        protected void ucPopupSiteActionTemplate_OnTemplateSaved(object sender, PopupSiteActionTemplate.TemplateSavedEventArgs e)
        {
            var siteActionTemplate = dataManager.SiteActionTemplate.SelectById(e.SelectedTemplateId);

            var siteAction = new tbl_SiteAction
            {
                SiteID = SiteId,
                SiteActionTemplateID = siteActionTemplate.ID,
                ContactID = _contactID,
                ActionStatusID = (int)ActionStatus.Scheduled,
                ActionDate = DateTime.Now,
                MessageTitle = siteActionTemplate.MessageCaption,
                MessageText = siteActionTemplate.MessageBody
            };
            dataManager.SiteAction.Add(siteAction);
        }
    }
}