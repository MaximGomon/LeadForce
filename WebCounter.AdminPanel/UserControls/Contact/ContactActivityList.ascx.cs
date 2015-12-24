using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;


namespace WebCounter.AdminPanel.UserControls
{
    public partial class ContactActivityList : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();
        public Guid siteID = new Guid();
        private Guid _contactID;

        public bool ShowWidgets
        {
            set { ucLeftColumn.Visible = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rttmSessionInfo.TargetControls.Clear();
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(gridContactActivity, rttmSessionInfo);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPeriodFilter, gridContactActivity);

            siteID = ((LeadForceBasePage)Page).SiteId;

            var contactId = Page.RouteData.Values["id"] as string;

            if (contactId != "null" && !Guid.TryParse(contactId, out _contactID))
                Response.Redirect(UrlsData.AP_Contacts());

            gridContactActivity.SiteID = siteID;

            ucPeriodFilter.FilterChanged += ucPeriodFilter_FilterChanged;

            /*if (_contactID != Guid.Empty)
                ucPeriodFilter.AllPeriod = true;*/
            
            BindData(ucPeriodFilter.StartDate, ucPeriodFilter.EndDate);
        }


        protected void BindData(DateTime? startDate, DateTime? endDate)
        {
            gridContactActivity.Where = new List<GridWhere>();

            if (_contactID != Guid.Empty)
            {                
                gridContactActivity.Where.Add(new GridWhere { Column = "ContactID", Value = _contactID.ToString() });
                gridContactActivity.Columns[3].Visible = false;
            }

            if (startDate.HasValue && endDate.HasValue)
                gridContactActivity.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_ContactActivity.CreatedAt >= '{0}' AND tbl_ContactActivity.CreatedAt <= '{1}'", startDate.Value.ToString("yyyy-MM-dd"), endDate.Value.AddDays(1).ToString("yyyy-MM-dd")) });
            else
                gridContactActivity.Where.Add(new GridWhere { CustomQuery = string.Format("tbl_ContactActivity.CreatedAt >= '{0}' AND tbl_ContactActivity.CreatedAt <= '{1}'", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")) });            
        }



        protected void ucPeriodFilter_FilterChanged(object sender, Shared.PeriodFilter.FilterChangedEventArgs e)
        {
            BindData(e.StartDate, e.EndDate);
            gridContactActivity.Rebind();
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            siteID = ((LeadForceBasePage)Page).SiteId;
        }


        /// <summary>
        /// Called when [ajax update].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Telerik.Web.UI.ToolTipUpdateEventArgs"/> instance containing the event data.</param>
        protected void OnAjaxUpdate(object sender, ToolTipUpdateEventArgs args)
        {
            UpdateToolTip(args.Value, args.UpdatePanel);
        }



        /// <summary>
        /// Updates the tool tip.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <param name="panel">The panel.</param>
        private void UpdateToolTip(string elementId, UpdatePanel panel)
        {
            Control ctrl = Page.LoadControl("~/UserControls/SessionInfo.ascx");
            var sessionInfo = (SessionInfo)ctrl;
            if (!string.IsNullOrEmpty(elementId))
                sessionInfo.SessionId = Guid.Parse(elementId);
            panel.ContentTemplateContainer.Controls.Add(ctrl);            
        }

        protected void gridContactActivity_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                var target = ((LinkButton)item.FindControl("lbtnShowSessionInfo"));
                if (data["tbl_ContactActivity_ContactSessionID"] != null)
                    rttmSessionInfo.TargetControls.Add(target.ClientID, data["tbl_ContactActivity_ContactSessionID"].ToString(), true);

                target.Text = data["tbl_ContactSessions_UserSessionNumber"] != null && !string.IsNullOrEmpty(data["tbl_ContactSessions_UserSessionNumber"].ToString()) ? "Посещение " + data["tbl_ContactSessions_UserSessionNumber"] : "---";

                if (item.FindControl("lContact") != null)
                {
                    if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                        ((Literal)item.FindControl("lContact")).Text = string.Format("<a href=\"{0}\">{1}</a>", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), data["tbl_Contact_UserFullName"]);
                    else
                        ((Literal)item.FindControl("lContact")).Text = string.Format("&lt;<a href=\"{0}\">Незнакомец</a> от {1}&gt;", UrlsData.AP_Contact(Guid.Parse(data["tbl_Contact_ID"].ToString())), DateTime.Parse(data["tbl_Contact_CreatedAt"].ToString()).ToString("dd.MM.yyyy HH:mm"));
                }

                if (!string.IsNullOrEmpty(data["tbl_ContactActivity_ActivityCode"].ToString()))
                {
                    switch ((ActivityType)int.Parse(data["tbl_ContactActivity_ActivityTypeID"].ToString()))
                    {
                        case ActivityType.InboxMessage:
                            ((Literal)item.FindControl("lActivityCode")).Text = data["tbl_ContactActivity_ActivityCode"].ToString();
                            break;
                        case ActivityType.ViewPage:
                            ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", Server.UrlDecode(data["tbl_ContactActivity_ActivityCode"].ToString()));
                            break;
                        case ActivityType.Link:
                            ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", Server.UrlDecode(data["tbl_ContactActivity_ActivityCode"].ToString()), Server.UrlDecode(data["tbl_ContactActivity_ActivityCode"].ToString()));
                            break;
                        case ActivityType.OpenForm:
                        case ActivityType.FillForm:
                        case ActivityType.CancelForm:
                            var codeForm = data["tbl_ContactActivity_ActivityCode"].ToString();
                            codeForm = Regex.Replace(codeForm, @"^(.*)(#(.*))$", "$1");

                            var siteActivityRuleForm = dataManager.SiteActivityRules.Select(siteID, codeForm);
                            if (siteActivityRuleForm != null)
                                ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", UrlsData.AP_SiteActivityRule(siteActivityRuleForm.ID, siteActivityRuleForm.RuleTypeID), data["tbl_ContactActivity_ActivityCode"].ToString());
                            break;
                        case ActivityType.DownloadFile:
                            var siteActivityRule = dataManager.Links.Select(siteID, data["tbl_ContactActivity_ActivityCode"].ToString());
                            if (siteActivityRule != null)
                                ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", UrlsData.AP_SiteActivityRule(siteActivityRule.ID, siteActivityRule.RuleTypeID), siteActivityRule.Code);
                            break;
                        /*case ActivityType.Event:
                            var siteEventTemplate = dataManager.SiteEventTemplates.SelectById(Guid.Parse(data["tbl_ContactActivity_ActivityCode"].ToString()));
                            if (siteEventTemplate != null)
                                ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", UrlsData.AP_SiteEventTemplate(siteEventTemplate.ID), siteEventTemplate.Title);
                            ((Literal)item.FindControl("lActivityCode")).Text = siteEventTemplate.Title;
                            break;*/
                        case ActivityType.UserEvent:
                            var workflow = dataManager.WorkflowElement.SelectByValue(data["tbl_ContactActivity_ID"].ToString());
                            if (workflow != null)
                                ((Literal)item.FindControl("lActivityCode")).Text = string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", UrlsData.AP_WorkflowEdit(workflow.WorkflowID), data["tbl_ContactActivity_ActivityCode"]);
                            else
                                ((Literal)item.FindControl("lActivityCode")).Text = data["tbl_ContactActivity_ActivityCode"].ToString();
                            break;
                    }
                }
            }
        }
    }
}