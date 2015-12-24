using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.WorkflowTemplate
{
    public partial class SaveWorkflowTemplateElement : System.Web.UI.UserControl
    {
        private DataManager _dataManager;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _dataManager = ((LeadForceBasePage)Page).DataManager;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            //EnumHelper.EnumToDropDownList<WorkflowElementType>(ref ddlElementType);
            ddlElementType.Items.Add(new ListItem("Выберите значение", ""));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.Message), ((int)WorkflowTemplateElementType.Message).ToString()));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.Task), ((int)WorkflowTemplateElementType.Task).ToString()));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.WaitingEvent), ((int)WorkflowTemplateElementType.WaitingEvent).ToString()));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.Activity), ((int)WorkflowTemplateElementType.Activity).ToString()));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.StartProcess), ((int)WorkflowTemplateElementType.StartProcess).ToString()));
            ddlElementType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(WorkflowTemplateElementType.EndProcess), ((int)WorkflowTemplateElementType.EndProcess).ToString()));

            //dcbMessageSiteActionTemplate.SiteID = CurrentUser.Instance.SiteID;
            //dcbMessageSiteActionTemplate.BindData();

            ucWorkflowTemplateElementConditionEvent.BindData();
            ucWorkflowTemplateElementResult.BindData();

            dcbTaskType.SiteID = CurrentUser.Instance.SiteID;
            dcbTaskType.BindData();

            lbtnSave.Visible = ((AdminPanel.WorkflowTemplate)Page).IsEditable;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlElementType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlElementType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindTabs();
            BindPanels();
        }



        /// <summary>
        /// Binds the tabs.
        /// </summary>
        public void BindTabs()
        {
            rtsTabs.Tabs.FindTabByValue("ElementSettings").Visible = false;
            rtsTabs.Tabs.FindTabByValue("ResultElementProcess").Visible = false;
            rtsTabs.Tabs.FindTabByValue("ElementParameters").Visible = false;

            pnlMessageSettings.Visible = false;
            pnlWaitingEvent.Visible = false;
            pnlActivitySettings.Visible = false;
            pnlTaskSettings.Visible = false;

            var selectedValue = ddlElementType.SelectedValue;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                switch ((WorkflowTemplateElementType)selectedValue.ToInt())
                {
                    case WorkflowTemplateElementType.Message:
                        pnlMessageSettings.Visible = true;
                        rtsTabs.Tabs.FindTabByValue("ElementSettings").Visible = true;
                        break;
                    case WorkflowTemplateElementType.WaitingEvent:
                        pnlWaitingEvent.Visible = true;
                        rtsTabs.Tabs.FindTabByValue("ElementSettings").Visible = true;
                        break;
                    case WorkflowTemplateElementType.Activity:
                        pnlActivitySettings.Visible = true;
                        rtsTabs.Tabs.FindTabByValue("ElementSettings").Visible = true;
                        break;
                    case WorkflowTemplateElementType.Task:
                        pnlTaskSettings.Visible = true;
                        if (ucWorkflowTemplateElementResult.WorkflowTemplateElementResultList.FirstOrDefault(a => a.Name == "Задача просрочена" && a.IsSystem) == null &&
                            ucWorkflowTemplateElementResult.WorkflowTemplateElementResultList.FirstOrDefault(a => a.Name == "Задача отменена" && a.IsSystem) == null)
                        {
                            var workflowTemplateElementResultList = new List<WorkflowTemplateElementResultMap>();
                            workflowTemplateElementResultList.Add(new WorkflowTemplateElementResultMap { ID = Guid.NewGuid(), Name = "Задача просрочена", IsSystem = true });
                            workflowTemplateElementResultList.Add(new WorkflowTemplateElementResultMap { ID = Guid.NewGuid(), Name = "Задача отменена", IsSystem = true });
                            ucWorkflowTemplateElementResult.WorkflowTemplateElementResultList = workflowTemplateElementResultList;
                            ucWorkflowTemplateElementResult.BindData();
                        }
                        rtsTabs.Tabs.FindTabByValue("ElementSettings").Visible = true;
                        rtsTabs.Tabs.FindTabByValue("ResultElementProcess").Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Binds the panels.
        /// </summary>
        public void BindPanels()
        {
            pnlTypicalDuration.Visible = false;
            pnlRequiredControl.Visible = false;
            //pnlActivitySettings.Visible = false;
            if (!string.IsNullOrEmpty(ddlElementType.SelectedValue))
            {
                switch (ddlElementType.SelectedValue.ToEnum<WorkflowTemplateElementType>())
                {
                    case WorkflowTemplateElementType.StartProcess:
                    case WorkflowTemplateElementType.EndProcess:
                        break;
                    case WorkflowTemplateElementType.Message:
                        break;
                    case WorkflowTemplateElementType.WaitingEvent:
                        pnlRequiredControl.Visible = true;
                        break;
                    case WorkflowTemplateElementType.Activity:
                        break;
                    case WorkflowTemplateElementType.Task:
                        pnlRequiredControl.Visible = true;
                        pnlTypicalDuration.Visible = true;
                        break;
                }
            }
        }



        /// <summary>
        /// Handles the OnCheckedChanged event of the cbOptional control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cbOptional_OnCheckedChanged(object sender, EventArgs e)
        {
            pnlResultName.Visible = cbOptional.Checked;
        }
    }
}