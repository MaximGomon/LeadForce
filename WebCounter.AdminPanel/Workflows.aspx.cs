using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Workflows : LeadForceBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Процессы - LeadForce";

            gridWorkflows.SiteID = SiteId;
            gridWorkflows.Actions.Add(new GridAction { Text = "Карточка процесса", NavigateUrl = string.Format("~/{0}/Workflows/Edit/{{0}}", CurrentTab), ImageUrl = "~/App_Themes/Default/images/icoView.png" });
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWorkflows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWorkflows_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                ((Literal)item.FindControl("litStatus")).Text = EnumHelper.GetEnumDescription((WorkflowStatus)int.Parse(data["tbl_Workflow_Status"].ToString()));

                try
                {
                    var workflowTemplate = DataManager.WorkflowTemplate.SelectById(SiteId, data["tbl_Workflow_WorkflowTemplateID"].ToString().ToGuid());
                    var workflowTemplateParameterId = workflowTemplate.tbl_WorkflowTemplateParameter.SingleOrDefault(a => a.Name == "Контакт").ID;
                    var parameterId = DataManager.Workflow.SelectById(SiteId, data["ID"].ToString().ToGuid()).tbl_WorkflowParameter.SingleOrDefault(a => a.WorkflowTemplateParameterID == workflowTemplateParameterId).Value.ToGuid();
                    var userFullName = DataManager.Contact.SelectById(SiteId, parameterId).UserFullName;
                    ((Literal)item.FindControl("litParameter")).Text = userFullName;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}