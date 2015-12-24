using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class WorkflowTemplate : LeadForceBasePage
    {
        private Guid _workflowTemplateId;
        public Access access;
        protected RadAjaxManager radAjaxManager = null;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool IsEditable
        {
            get
            {
                object o = ViewState["IsEditable"];
                return (o == null ? true : (bool)o);
            }
            set
            {
                ViewState["IsEditable"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблон процесса - LeadForce";

            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            dprReminder.RegisterSkipCheckingTrigger(lbtnSave);

            if (Page.RouteData.Values["id"] != null)
                _workflowTemplateId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_WorkflowTemplates();

            ucWorkflowTemplateGoal.WorkflowTemplateId = _workflowTemplateId;
            ucWorkflowTemplateMaterial.WorkflowTemplateId = _workflowTemplateId;

            if (!Page.IsPostBack)
            {
                BindData();
                SLObejct(false);
            }

            if (CurrentUser.Instance.IsTemplateSite)
                RadTabStrip1.FindTabByValue("Materials").Visible = true;

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ddlStatus, ucNotificationMessage);
            radAjaxManager.AjaxSettings.AddAjaxSetting(ddlModule, ucWorkflowTemplateParameter);
            radAjaxManager.AjaxSettings.AddAjaxSetting(RadTabStrip1, ucWorkflowTemplateGoal);
        }



        /// <summary>
        /// SLs the obejct.
        /// </summary>
        private void SLObejct(bool isMapConversion)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<object id=\"{0}\" data=\"data:application/x-silverlight-2,\" type=\"application/x-silverlight-2\" width=\"100%\" height=\"100%\">", isMapConversion ? "silverlightMapConversion" : "silverlightWorkflow");
            //sb.AppendFormat("<param name=\"initParams\" value=\"siteid={0}, workflowtemplateid={1}, endpointaddress={2}, domainname={3}, userid={4}, ismapconversion={5}\" />", SiteId, _workflowTemplateId, WebCounter.BusinessLogicLayer.Configuration.Settings.WorkflowApiUrl, Request.Url.Host, CurrentUser.Instance.ID, isMapConversion);
            sb.AppendFormat("<param name=\"initParams\" value=\"siteid={0}, workflowtemplateid={1}, endpointaddress={2}, userid={3}, ismapconversion={4}, isfulledit={5}\" />", SiteId, _workflowTemplateId, WebCounter.BusinessLogicLayer.Configuration.Settings.WorkflowApiUrl, CurrentUser.Instance.ID, isMapConversion, IsEditable);
            sb.AppendFormat("<param name=\"source\" value=\"{0}\"/>", ResolveUrl("~/ClientBin/Labitec.LeadForce.WorkflowDesigner.xap"));
		    sb.Append("<param name=\"onError\" value=\"onSilverlightError\" />");
		    sb.Append("<param name=\"background\" value=\"white\" />");
		    sb.Append("<param name=\"minRuntimeVersion\" value=\"5.0.61118.0\" />");
		    sb.Append("<param name=\"autoUpgrade\" value=\"true\" />");
            //sb.Append("<param name=\"background\" value=\"transparent\" />");
            //sb.Append("<param name=\"windowless\" value=\"true\" />");
            sb.Append("<param name=\"onLoad\" value=\"pluginLoaded\" />");
		    sb.Append("<a href=\"http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0\" style=\"text-decoration:none\"><img src=\"http://go.microsoft.com/fwlink/?LinkId=161376\" alt=\"Get Microsoft Silverlight\" style=\"border-style:none\"/></a>");
            sb.Append("</object><iframe id=\"_sl_historyFrame\" style=\"visibility:hidden;height:0px;width:0px;border:0px\"></iframe>");

            if (isMapConversion)
                silverlightMapConversionHost.InnerHtml = sb.ToString();
            else
                silverlightControlHost.InnerHtml = sb.ToString();
        }




        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            RadFilter1.Culture = new CultureInfo("ru-RU");

            ucWorkflowTemplateConditionEvent.WorkflowTemplateId = _workflowTemplateId;
            ucWorkflowTemplateParameter.WorkflowTemplateId = _workflowTemplateId;
            ////ucWorkflowTemplateElement.WorkflowTemplateId = _workflowTemplateId;
            ////ucWorkflowTemplateElementRelation.WorkflowTemplateId = _workflowTemplateId;

            //var modules = DataManager.Module.SelectAll();
            var moduleContacts = DataManager.Module.SelectByName("Contacts");
            ddlModule.Items.Add(new ListItem("Выберите значение", ""));
            ddlModule.Items.Add(new ListItem(moduleContacts.Title, moduleContacts.ID.ToString()));

            EnumHelper.EnumToDropDownList<WorkflowTemplateStatus>(ref ddlStatus, false);
            EnumHelper.EnumToDropDownList<WorkflowTemplateAutomaticMethod>(ref ddlAutomaticMethod, false);
            EnumHelper.EnumToDropDownList<WorkflowTemplateEvent>(ref ddlEvent);
            EnumHelper.EnumToDropDownList<WorkflowTemplateCondition>(ref ddlCondition);

            var workflowTemplate = DataManager.WorkflowTemplate.SelectById(SiteId, _workflowTemplateId);
            if (workflowTemplate != null)
            {
                txtName.Text = workflowTemplate.Name;
                ucAuthor.SelectedValue = workflowTemplate.ContactID;

                if (workflowTemplate.ModuleID != null)
                    ddlModule.Items.FindByValue(workflowTemplate.ModuleID.ToString()).Selected = true;

                ddlStatus.Items.FindByValue(workflowTemplate.Status.ToString()).Selected = true;
                rdpStartDate.SelectedDate = workflowTemplate.StartDate;
                rdpEndDate.SelectedDate = workflowTemplate.EndDate;
                txtDescription.Text = workflowTemplate.Description;
                cbManualStart.Checked = workflowTemplate.ManualStart;
                ddlAutomaticMethod.Items.FindByValue(workflowTemplate.AutomaticMethod.ToString()).Selected = true;
                txtFrequency.Text = workflowTemplate.Frequency.ToString();
                cbDenyMultipleRun.Checked = workflowTemplate.DenyMultipleRun;

                switch ((WorkflowTemplateAutomaticMethod)workflowTemplate.AutomaticMethod)
                {
                    case WorkflowTemplateAutomaticMethod.EditRecord:
                        ddlEvent.Items.FindByValue(workflowTemplate.Event.ToString()).Selected = true;
                        pnlEditRecord.Visible = true;
                        /*if (workflowTemplate.ModuleID != null)
                            pnlFilter.Visible = true;*/
                        break;
                    case WorkflowTemplateAutomaticMethod.ActivityContact:
                        ddlCondition.Items.FindByValue(workflowTemplate.Condition.ToString()).Selected = true;
                        if ((WorkflowTemplateCondition)workflowTemplate.Condition == WorkflowTemplateCondition.NEvent)
                        {
                            pnlActivityCount.Visible = true;
                            txtActivityCount.Text = workflowTemplate.ActivityCount.ToString();
                        }
                        pnlActivityContact.Visible = true;
                        break;
                }

                var workflowCount = DataManager.Workflow.SelectByWorkflowTemplateId(_workflowTemplateId).ToList().Count;
                ddlStatus.Attributes.Add("onchange", "ConfirmInPlans();");
                if (workflowCount > 0 && (WorkflowTemplateStatus)workflowTemplate.Status != WorkflowTemplateStatus.InPlans)
                {
                    IsEditable = false;
                    ucNotificationMessage.Visible = true;
                    ddlModule.Enabled = false;

                    RadTabStrip1.FindTabByValue("SilverlightMapConversion").Visible = true;
                    SLObejct(true);
                }

                xmlWorkflow.Value = workflowTemplate.WorkflowXml ?? "";
                var elements = DataManager.WorkflowTemplateElement.SelectAllMap(_workflowTemplateId);
                jsonElements.Value = JsonConvert.SerializeObject(elements);
                var elementRelations = DataManager.WorkflowTemplateElementRelation.SelectAllMap(_workflowTemplateId);
                jsonElementRelations.Value = JsonConvert.SerializeObject(elementRelations);
            }
            else
            {
                if (CurrentUser.Instance.ContactID.HasValue)
                    ucAuthor.SelectedValue = CurrentUser.Instance.ContactID;
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlAutomaticMethod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlAutomaticMethod_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            pnlEditRecord.Visible = false;
            pnlActivityContact.Visible = false;
            pnlTimer.Visible = false;

            if (!string.IsNullOrEmpty(ddlAutomaticMethod.SelectedValue))
            {
                switch ((WorkflowTemplateAutomaticMethod)int.Parse(ddlAutomaticMethod.SelectedValue))
                {
                    case WorkflowTemplateAutomaticMethod.EditRecord:
                        pnlEditRecord.Visible = true;
                        break;
                    case WorkflowTemplateAutomaticMethod.ActivityContact:
                        pnlActivityContact.Visible = true;
                        ucWorkflowTemplateConditionEvent.Rebind();
                        break;
                    /*case WorkflowAutomaticMethod.Timer:
                        pnlTimer.Visible = true;
                        break;*/
                }
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

            dprReminder.MarkPageClean();

            var workflowTemplate = DataManager.WorkflowTemplate.SelectById(SiteId, _workflowTemplateId) ?? new tbl_WorkflowTemplate();

            workflowTemplate.SiteID = SiteId;
            workflowTemplate.Name = txtName.Text;
            workflowTemplate.ContactID = (Guid)ucAuthor.SelectedValue;
            if (IsEditable)
                workflowTemplate.ModuleID = !string.IsNullOrEmpty(ddlModule.SelectedValue) ? (Guid?)ddlModule.SelectedValue.ToGuid() : null;
            workflowTemplate.Status = ddlStatus.SelectedValue.ToInt();
            workflowTemplate.StartDate = rdpStartDate.SelectedDate;
            workflowTemplate.EndDate = rdpEndDate.SelectedDate;
            workflowTemplate.Description = !string.IsNullOrEmpty(txtDescription.Text) ? txtDescription.Text : null;
            workflowTemplate.ManualStart = cbManualStart.Checked;
            workflowTemplate.AutomaticMethod = ddlAutomaticMethod.SelectedValue.ToInt();
            workflowTemplate.Frequency = txtFrequency.Text.ToInt();
            workflowTemplate.DenyMultipleRun = cbDenyMultipleRun.Checked;
            ////if (IsEditable)
                workflowTemplate.WorkflowXml = xmlWorkflow.Value;

            switch (ddlAutomaticMethod.SelectedValue.ToEnum<WorkflowTemplateAutomaticMethod>())
            {
                case WorkflowTemplateAutomaticMethod.None:
                    workflowTemplate.Event = null;
                    workflowTemplate.Condition = null;
                    workflowTemplate.ActivityCount = null;
                    break;
                case WorkflowTemplateAutomaticMethod.EditRecord:
                    workflowTemplate.Event = ddlEvent.SelectedValue.ToInt();
                    workflowTemplate.Condition = null;
                    workflowTemplate.ActivityCount = null;
                    break;
                case WorkflowTemplateAutomaticMethod.ActivityContact:
                    workflowTemplate.Event = null;
                    workflowTemplate.Condition = ddlCondition.SelectedValue.ToInt();
                    if (ddlCondition.SelectedValue.ToEnum<WorkflowTemplateCondition>() == WorkflowTemplateCondition.NEvent)
                        workflowTemplate.ActivityCount = txtActivityCount.Text.ToInt();
                    else
                        workflowTemplate.ActivityCount = null;
                    break;
            }

            if (workflowTemplate.ID == Guid.Empty)
            {
                workflowTemplate.ID = Guid.NewGuid();
                DataManager.WorkflowTemplate.Add(workflowTemplate);

                /*var module = DataManager.Module.SelectById((Guid)workflowTemplate.ModuleID);
                switch (module.Name)
                {
                    case "Contacts":
                        var workflowTemplateParameter = new tbl_WorkflowTemplateParameter { WorkflowTemplateID = workflowTemplate.ID, Name = "tbl_Contact.ID", IsSystem = true };
                        DataManager.WorkflowTemplateParameter.Add(workflowTemplateParameter);
                        break;
                }*/
            }
            else
            {
                DataManager.WorkflowTemplate.Update(workflowTemplate);

                // if change status to 'InPlans'
                if (ddlStatus.SelectedValue.ToEnum<WorkflowTemplateStatus>() == WorkflowTemplateStatus.InPlans)
                    DataManager.Workflow.DeleteByWorkflowTemplateId(workflowTemplate.ID);
            }

            if (ddlAutomaticMethod.SelectedValue.ToEnum<WorkflowTemplateAutomaticMethod>() == WorkflowTemplateAutomaticMethod.ActivityContact)
                ucWorkflowTemplateConditionEvent.Save(workflowTemplate.ID);
            else
                DataManager.WorkflowTemplateConditionEvent.DeleteAll(workflowTemplate.ID);


            var deserializedElements = JsonConvert.DeserializeObject<List<WorkflowTemplateElementMap>>(jsonElements.Value);
            var deserializedElementRelations = JsonConvert.DeserializeObject<List<WorkflowTemplateElementRelationMap>>(jsonElementRelations.Value);
            DataManager.WorkflowTemplateElement.Save(deserializedElements, workflowTemplate.ID);
            DataManager.WorkflowTemplateElementRelation.Save(deserializedElementRelations, workflowTemplate.ID);

            if (IsEditable)
            {
                ucWorkflowTemplateParameter.WorkflowTemplateId = workflowTemplate.ID;
                ucWorkflowTemplateParameter.Save(workflowTemplate.ID);

                //Сохранение целей
                ucWorkflowTemplateGoal.Save(workflowTemplate.ID);
                ucWorkflowTemplateMaterial.Save(workflowTemplate.ID);

                //ucWorkflowTemplateElement.Save(workflowTemplate.ID);
                //ucWorkflowTemplateElementRelation.Save(workflowTemplate.ID);
            }

            Response.Redirect(UrlsData.AP_WorkflowTemplates());
        }



        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return string.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }




        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlModule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlModule_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (!string.IsNullOrEmpty(ddlModule.SelectedValue))
                pnlFilter.Visible = true;
            else
                pnlFilter.Visible = false;*/

            ucWorkflowTemplateParameter.AddSystemParameters(!string.IsNullOrEmpty(ddlModule.SelectedValue) ? ddlModule.SelectedValue.ToGuid() : Guid.Empty);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlCondition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlCondition_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCondition.SelectedValue) && ddlCondition.SelectedValue.ToEnum<WorkflowTemplateCondition>() == WorkflowTemplateCondition.NEvent)
                pnlActivityCount.Visible = true;
            else
                pnlActivityCount.Visible = false;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var workflowCount = DataManager.Workflow.SelectByWorkflowTemplateId(_workflowTemplateId).ToList().Count;
            if (workflowCount > 0)
            {
                if (ddlStatus.SelectedValue.ToEnum<WorkflowTemplateStatus>() != WorkflowTemplateStatus.InPlans)
                {
                    IsEditable = false;
                    ucNotificationMessage.Visible = true;
                    ddlModule.Enabled = false;
                }
                else
                {
                    IsEditable = true;
                    ucNotificationMessage.Visible = false;
                    ddlModule.Enabled = true;
                }

                ////((RadGrid)ucWorkflowTemplateElement.FindControl("rgWorkflowTemplateElement")).Rebind();
                ////((RadGrid)ucWorkflowTemplateElementRelation.FindControl("rgWorkflowTemplateElementRelation")).Rebind();
                ((RadGrid)ucWorkflowTemplateParameter.FindControl("rgWorkflowTemplateParameter")).Rebind(); 
            }
        }


        /// <summary>
        /// Deletes the relation by result id.
        /// </summary>
        /// <param name="elementResultId">The element result id.</param>
        public void DeleteRelationByResultId(Guid elementResultId)
        {
            ////ucWorkflowTemplateElementRelation.DeleteByResultId(elementResultId);
        }
    }
}