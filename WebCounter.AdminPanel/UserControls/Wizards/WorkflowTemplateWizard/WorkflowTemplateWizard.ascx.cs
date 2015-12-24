using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class WorkflowTemplateWizard : System.Web.UI.UserControl
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
            get { return (Guid?)ViewState["EditFormId"]; }
            set { ViewState["EditFormId"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["CurrentDockStates"] = null;
                Session["DocksConditions"] = null;
                Session["Materials"] = null;
                Session["SiteActionTemplates"] = null;

                if (!IsEditMode)
                {
                    rtsWizard.AutoPostBack = true;
                    rtsWizard.TabClick += rtsWizard_OnTabClick;
                    AddTab("Выбор типового процесса", "SelectWorkflowTemplate", true);
                    rmpWizard.PageViews.Add(new RadPageView {ID = "SelectWorkflowTemplate"});
                    AddTab("Основная информация", "GeneralInformation", false);
                    AddTab("Условия запуска", "RunCondition", false);
                    AddTab("Квалификация", "Qualification", false);
                    AddTab("Материалы", "Materials", false);
                }
                else
                {
                    rtsWizard.AutoPostBack = false;
                    plButtons.Visible = true;
                    hlCancel.NavigateUrl = UrlsData.AP_WorkflowTemplates();
                    AddTab("Основная информация", "GeneralInformation", true);
                    rmpWizard.PageViews.Add(new RadPageView { ID = "GeneralInformation" });
                    var dataManager = new DataManager();
                    var workflowTemplate = dataManager.WorkflowTemplate.SelectById(EditFormId.Value);
                    if (workflowTemplate.AutomaticMethod == (int)WorkflowTemplateAutomaticMethod.ActivityContact)
                    {
                        AddTab("Условия запуска", "RunCondition", true);
                        rmpWizard.PageViews.Add(new RadPageView { ID = "RunCondition" });
                    }
                    var workflowTemplateElements = dataManager.WorkflowTemplateElement.SelectAll(workflowTemplate.ID);
                    if (workflowTemplateElements.Count(a => a.ElementType == (int)WorkflowTemplateElementType.Tag) > 0)
                    {
                        AddTab("Квалификация", "Qualification", true);
                        rmpWizard.PageViews.Add(new RadPageView { ID = "Qualification" });
                    }
                    AddTab("Материалы", "Materials", true);
                    rmpWizard.PageViews.Add(new RadPageView { ID = "Materials" });
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
        /// Handles the OnTabClick event of the rtsWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void rtsWizard_OnTabClick(object sender, RadTabStripEventArgs e)
        {
            rtsWizard.ValidationGroup = e.Tab.Value;
        }



        /// <summary>
        /// Handles the OnPageViewCreated event of the rmpWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadMultiPageEventArgs"/> instance containing the event data.</param>
        protected void rmpWizard_OnPageViewCreated(object sender, RadMultiPageEventArgs e)
        {
            var pageViewContents = (BaseWizardStep)LoadControl("~/UserControls/Wizards/WorkflowTemplateWizard/" + e.PageView.ID + ".ascx");
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
        /// Saves the specified workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(Guid workflowTemplateId)
        {
            var dataManager = new DataManager();

            tbl_WorkflowTemplate workflowTemplate = null;

            var rolesList = new List<WorkflowTemplateWizardRole>();
            var conditionEventList = new List<WorkflowTemplateWizardConditionEvent>();
            var tagsList = new List<WorkflowTemplateWizardTag>();
            var actionTemplatesList = new List<WorkflowTemplateWizardActionTemplate>();
            var materialsList = new List<WorkflowTemplateWizardMaterial>();
            int? condition = null;
            int? activityCount = null;


            var gridRoles = ((RadGrid)rmpWizard.FindPageViewByID("GeneralInformation").Controls[0].FindControl("rgContactRoles"));
            foreach (GridDataItem item in gridRoles.Items)
            {
                rolesList.Add(new WorkflowTemplateWizardRole
                                  {
                                      OldContactRoleID = item.GetDataKeyValue("ContactRoleID").ToString().ToGuid(),
                                      OldEmail = item.GetDataKeyValue("RoleInTemplate").ToString(),
                                      ContactRoleID = ((DictionaryOnDemandComboBox)item.FindControl("dcbContactRole")).SelectedId,
                                      Email = ((TextBox)item.FindControl("txtFromEmail")).Text,
                                      DisplayName = ((TextBox)item.FindControl("txtFromName")).Text
                                  });
            }

            if (rmpWizard.FindPageViewByID("RunCondition") != null)
            {
                condition = ((DropDownList)rmpWizard.FindPageViewByID("RunCondition").Controls[0].FindControl("ddlCondition")).SelectedValue.ToInt();
                if ((WorkflowTemplateCondition)condition == WorkflowTemplateCondition.NEvent)
                    activityCount = (int)((RadNumericTextBox)rmpWizard.FindPageViewByID("RunCondition").Controls[0].FindControl("txtActivityCount")).Value;

                var dockCondition = ((RadDockZone)rmpWizard.FindPageViewByID("RunCondition").Controls[0].FindControl("radDockZone"));
                var docks = dockCondition.Docks.Where(a => !a.Closed);
                foreach (RadDock dock in docks)
                {
                    var workflowTemplateWizardConditionEvent = new WorkflowTemplateWizardConditionEvent
                                                                   {
                                                                       ActivityType = dock.Attributes["ActivityType"].ToInt(),
                                                                       ActualPeriod = (int)(((RadNumericTextBox)FindControlRecursive(dock, "txtActualPeriod")).Value)
                                                                   };

                    if ((ActivityType)dock.Attributes["ActivityType"].ToInt() == ActivityType.ViewPage)
                        workflowTemplateWizardConditionEvent.Code = ((TextBox)FindControlRecursive(dock, "txtCode")).Text;
                    else
                        workflowTemplateWizardConditionEvent.Code = dock.Attributes["Code"];

                    if (FindControlRecursive(dock, "txtParameter") != null)
                    {
                        var parameter = ((TextBox)FindControlRecursive(dock, "txtParameter")).Text;
                        if (!string.IsNullOrEmpty(parameter) && (ActivityType)workflowTemplateWizardConditionEvent.ActualPeriod != ActivityType.DownloadFile)
                            workflowTemplateWizardConditionEvent.Code = workflowTemplateWizardConditionEvent.Code + "#" + parameter;
                    }

                    conditionEventList.Add(workflowTemplateWizardConditionEvent);
                }
            }

            if (rmpWizard.FindPageViewByID("Qualification") != null)
            {
                var gridTags = ((RadGrid)rmpWizard.FindPageViewByID("Qualification").Controls[0].FindControl("rgWorkflowTemplateElement"));
                foreach (GridDataItem item in gridTags.Items)
                {
                    tagsList.Add(
                        new WorkflowTemplateWizardTag
                        {
                            Id = item.GetDataKeyValue("ID").ToString().ToGuid(),
                            Operation = (((RadioButtonList)item.FindControl("rblOperation")).SelectedValue).ToInt(),
                            TagId = ((DictionaryOnDemandComboBox)item.FindControl("dcbTag")).SelectedId
                        });
                }
            }

            /*var rpbActionTemplates = ((RadPanelBar)rmpWizard.FindPageViewByID("Materials").Controls[0].FindControl("rpbActionTemplates"));
            foreach (RadPanelItem actionTemplate in rpbActionTemplates.Items)
            {
                actionTemplatesList.Add(new WorkflowTemplateWizardActionTemplate
                                            {
                                                Id = actionTemplate.Value.ToGuid(),
                                                MessageBody = ((HtmlEditor)actionTemplate.FindControl("htmlEditor")).Content
                                            });
            }*/

            var sessionMaterials = ((List<MaterialMap>)Session["Materials"]).Where(a => (MaterialType)a.Type != MaterialType.ActionTemplate);
            foreach (var material in sessionMaterials)
            {
                materialsList.Add(new WorkflowTemplateWizardMaterial
                                      {
                                          Id = material.ID,
                                          Type = material.Type,
                                          OldValue = material.OldValue,
                                          Value = material.Value
                                      });
            }

            var sessionSiteActionTemplates = (List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"];
            foreach (var siteActionTemplate in sessionSiteActionTemplates)
            {
                if (siteActionTemplate.FromContactRoleID.HasValue)
                {
                    var role = rolesList.FirstOrDefault(a => a.OldContactRoleID == siteActionTemplate.FromContactRoleID);
                    if (role != null)
                    {
                        if (role.ContactRoleID != Guid.Empty)
                            siteActionTemplate.FromContactRoleID = role.ContactRoleID;
                        else
                        {
                            siteActionTemplate.FromContactRoleID = null;
                            siteActionTemplate.FromEmail = role.Email;
                            if (!string.IsNullOrEmpty(role.DisplayName))
                                siteActionTemplate.FromName = role.DisplayName;
                            else
                                siteActionTemplate.FromName = null;
                        }
                    }
                }
                else
                {
                    var role = rolesList.FirstOrDefault(a => a.OldEmail == siteActionTemplate.FromEmail);
                    if (role != null)
                    {
                        if (role.ContactRoleID != Guid.Empty)
                            siteActionTemplate.FromContactRoleID = role.ContactRoleID;
                        else
                        {
                            siteActionTemplate.FromEmail = role.Email;
                            if (!string.IsNullOrEmpty(role.DisplayName))
                                siteActionTemplate.FromName = role.DisplayName;
                            else
                                siteActionTemplate.FromName = null;
                        }
                    }
                }

                foreach (var siteActionTemplateRecipient in siteActionTemplate.tbl_SiteActionTemplateRecipient)
                {
                    if (siteActionTemplateRecipient.ContactRoleID.HasValue)
                    {
                        var role = rolesList.FirstOrDefault(a => a.OldContactRoleID == siteActionTemplateRecipient.ContactRoleID);
                        if (role != null)
                        {
                            if (role.ContactRoleID != Guid.Empty)
                                siteActionTemplateRecipient.ContactRoleID = role.ContactRoleID;
                            else
                            {
                                siteActionTemplateRecipient.ContactRoleID = null;
                                siteActionTemplateRecipient.Email = role.Email;
                                if (!string.IsNullOrEmpty(role.DisplayName))
                                    siteActionTemplateRecipient.DisplayName = role.DisplayName;
                            }
                        }
                    }
                    else
                    {
                        var role = rolesList.FirstOrDefault(a => a.OldEmail == siteActionTemplateRecipient.Email);
                        if (role != null)
                        {
                            if (role.ContactRoleID != Guid.Empty)
                                siteActionTemplateRecipient.ContactRoleID = role.ContactRoleID;
                            else
                            {
                                siteActionTemplateRecipient.ContactRoleID = null;
                                siteActionTemplateRecipient.Email = role.Email;
                                if (!string.IsNullOrEmpty(role.DisplayName))
                                    siteActionTemplateRecipient.DisplayName = role.DisplayName;
                            }
                        }
                    }
                }
            }

            
            if (!IsEditMode)
            {
                workflowTemplate = dataManager.WorkflowTemplate.CopyWizard(workflowTemplateId, CurrentUser.Instance.SiteID, (Guid)CurrentUser.Instance.ContactID, rolesList, condition, activityCount, conditionEventList, tagsList, (List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"], materialsList);
            }
            else
            {
                workflowTemplate = dataManager.WorkflowTemplate.UpdateWizard(workflowTemplateId, CurrentUser.Instance.SiteID, (Guid)CurrentUser.Instance.ContactID, rolesList, condition, activityCount, conditionEventList, tagsList, (List<tbl_SiteActionTemplate>)Session["SiteActionTemplates"], materialsList);
            }
                

            if (workflowTemplate != null)
            {
                workflowTemplate.Name = ((TextBox)rmpWizard.FindPageViewByID("GeneralInformation").Controls[0].FindControl("txtName")).Text;
                workflowTemplate.Status = ((DropDownList)rmpWizard.FindPageViewByID("GeneralInformation").Controls[0].FindControl("ddlStatus")).SelectedValue.ToInt();
                dataManager.WorkflowTemplate.Update(workflowTemplate);
                Response.Redirect(UrlsData.AP_WorkflowTemplates());
            }
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
    }
}