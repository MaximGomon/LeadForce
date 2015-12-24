using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using System.Data;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class MassWorkflows : LeadForceBasePage
    {
        protected string Filter = string.Empty;
        protected string Url = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Мероприятия - LeadForce";

            gridMassWorkflows.AddNavigateUrl = UrlsData.AP_MassWorkflowAdd();
            gridMassWorkflows.SiteID = SiteId;

            rbAddMassWorkflow.NavigateUrl = UrlsData.AP_MassWorkflowAdd();

            var optionTagPanel = ((LeadForceBasePage)Page).CurrentModuleEditionOptions.SingleOrDefault(a => a.SystemName == "TagAndFiltersPanel");
            if (optionTagPanel == null && !((LeadForceBasePage)Page).IsDefaultEdition)
            {
                gridMassWorkflows.ShowSelectCheckboxes = false;
                RadPanelBar1.Items[0].Visible = false;
                RadPanelBar1.Items[1].Visible = false;
            }

            Url = UrlsData.AP_MassWorkflows();

            Filter = Request.QueryString["f"];
            if (string.IsNullOrEmpty(Filter))
                Filter = "current";
            
            switch (Filter)
            {
                case "current":
                    gridMassWorkflows.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_MassWorkflow.Status={0} OR tbl_MassWorkflow.Status={1})", (int)MassWorkflowStatus.Active, (int)MassWorkflowStatus.InPlans) });
                    break;
                case "done":
                    gridMassWorkflows.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_MassWorkflow.Status={0})", (int)MassWorkflowStatus.Done) });
                    break;
                case "cancel":
                    gridMassWorkflows.Where.Add(new GridWhere { CustomQuery = string.Format("(tbl_MassWorkflow.Status={0})", (int)MassWorkflowStatus.Cancelled) });
                    break;                    
            }            

        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridMassWorkflows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridMassWorkflows_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((HyperLink)item.FindControl("spanName")).Text = data["tbl_Massworkflow_Name"].ToString();
                ((HyperLink)item.FindControl("spanName")).NavigateUrl = UrlsData.AP_MassWorkflowEdit(Guid.Parse(data["tbl_Massworkflow_ID"].ToString()));                
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_MassWorkflowEdit(Guid.Parse(data["tbl_Massworkflow_ID"].ToString()));
                if (!string.IsNullOrEmpty(data["tbl_MassWorkflowType_Title"].ToString()))
                    ((Literal)item.FindControl("lMassWorkflowType")).Text = string.Format("Тип мероприятия: {0}", data["tbl_MassWorkflowType_Title"]);

                ((Literal)item.FindControl("lStatus")).Text = EnumHelper.GetEnumDescription((MassWorkflowStatus)int.Parse(data["tbl_MassWorkflow_Status"].ToString()));

                var lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
                lbDelete.CommandArgument = data["ID"].ToString();
            }
        }



        /// <summary>
        /// Handles the OnCommand event of the lbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbDelete_OnCommand(object sender, CommandEventArgs e)
        {
            DataManager.MassWorkflow.DeleteById(SiteId, Guid.Parse(e.CommandArgument.ToString()));
            gridMassWorkflows.Rebind();
        }
    }
}