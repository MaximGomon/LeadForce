using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class WorkflowTemplates : LeadForceBasePage
    {
        private RadAjaxManager radAjaxManager = null;
        private bool allowWorkflowDesigner = true;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Шаблоны процессов - LeadForce";

            radAjaxManager = RadAjaxManager.GetCurrent(Page);

            radAjaxManager.AjaxSettings.AddAjaxSetting(InPlansAndActive, gridWorkflowTemplates, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(InPlans, gridWorkflowTemplates, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(Active, gridWorkflowTemplates, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(Archive, gridWorkflowTemplates, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(All, gridWorkflowTemplates, null, UpdatePanelRenderMode.Inline);

            rbAddWorkflowTemplate.NavigateUrl = UrlsData.AP_WorkflowTemplateAdd();
            rbAddWithMaster.NavigateUrl = UrlsData.AP_WorkflowTemplateWizard();
            gridWorkflowTemplates.SiteID = SiteId;
            gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = string.Format("((tbl_WorkflowTemplate.Status=0 OR tbl_WorkflowTemplate.Status=1) AND DataBaseStatusID = {0})", (int)DataBaseStatus.Active) });

            var optionTagPanel = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "TagAndFiltersPanel");
            if (optionTagPanel == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                gridWorkflowTemplates.ShowSelectCheckboxes = false;
                RadPanelBar1.Items[0].Visible = false;
                RadPanelBar1.Items[1].Visible = false;
            }

            var allowWorkflowDesignerOption = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "AllowWorkflowDesigner");
            if (allowWorkflowDesignerOption == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                rbAddWorkflowTemplate.Visible = false;
                allowWorkflowDesigner = false;
            }

            //gridWorkflowTemplates.Actions.Add(new GridAction { Text = "Карточка шаблона процесса", NavigateUrl = string.Format("~/{0}/WorkflowTemplates/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWorkflowTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWorkflowTemplates_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("lrlWorkflowTemplateStatus")).Text = EnumHelper.GetEnumDescription((WorkflowTemplateStatus)int.Parse(data["tbl_WorkflowTemplate_Status"].ToString()));
                ((HyperLink) item.FindControl("hlName")).Text = data["tbl_WorkflowTemplate_Name"].ToString();
                if (allowWorkflowDesigner)
                    ((HyperLink) item.FindControl("hlName")).NavigateUrl = UrlsData.AP_WorkflowTemplateEdit(Guid.Parse(data["tbl_WorkflowTemplate_ID"].ToString()));
                else
                    ((HyperLink)item.FindControl("hlName")).NavigateUrl = UrlsData.AP_WorkflowTemplateWizard(Guid.Parse(data["tbl_WorkflowTemplate_ID"].ToString()));
                ((HyperLink) item.FindControl("hlEdit")).NavigateUrl = ((HyperLink) item.FindControl("hlName")).NavigateUrl;

                ((LinkButton)item.FindControl("lbCopy")).CommandArgument = data["tbl_WorkflowTemplate_ID"].ToString();
                ((LinkButton)item.FindControl("lbCopy")).OnClientClick = string.Format("return confirm(\"Создать копию шаблона процесса '{0}' и перейти к редактированию?\");", data["tbl_WorkflowTemplate_Name"]);
                ((LinkButton)item.FindControl("lbDelete")).CommandArgument = data["tbl_WorkflowTemplate_ID"].ToString();
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the ibCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbCopy_OnCommand(object sender, CommandEventArgs e)
        {
            var workflowTemplateId = DataManager.WorkflowTemplate.Copy(SiteId, e.CommandArgument.ToString().ToGuid());
            Response.Redirect(UrlsData.AP_WorkflowTemplateEdit(workflowTemplateId));
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.WorkflowTemplate.Delete(CurrentUser.Instance.SiteID, e.CommandArgument.ToString().ToGuid());
            gridWorkflowTemplates.Rebind();
        }



        /// <summary>
        /// Handles the OnCheckedChanged event of the filters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void filters_OnCheckedChanged(object sender, EventArgs e)
        {
            gridWorkflowTemplates.Where.Clear();
            var radioButton = (RadioButton)sender;
            switch (radioButton.ID)
            {
                case "InPlansAndActive":
                    gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = "(tbl_WorkflowTemplate.Status=0 OR tbl_WorkflowTemplate.Status=1)" });
                    break;
                case "InPlans":
                    gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = "(tbl_WorkflowTemplate.Status=0)" });
                    break;
                case "Active":
                    gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = "(tbl_WorkflowTemplate.Status=1)" });
                    break;
                case "Archive":
                    gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = "(tbl_WorkflowTemplate.Status=2)" });
                    break;
            }

            gridWorkflowTemplates.Where.Add(new GridWhere { CustomQuery = string.Format("(DataBaseStatusID = {0})", (int)DataBaseStatus.Active) });

            gridWorkflowTemplates.Rebind();
        }
    }
}