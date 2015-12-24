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

namespace WebCounter.AdminPanel
{
    public partial class Workflow : LeadForceBasePage
    {
        private Guid _workflowId;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Процессы - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                _workflowId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Workflows();

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var workflow = DataManager.Workflow.SelectById(SiteId, _workflowId);
            if (workflow != null)
            {
                litName.Text = workflow.tbl_WorkflowTemplate.Name;
                if (workflow.Author.HasValue)
                    litAuthor.Text = DataManager.Contact.SelectById(SiteId, (Guid)workflow.Author).UserFullName;
                litStartDate.Text = workflow.StartDate.ToString("dd.MM.yyyy hh:mm");
                if (workflow.EndDate.HasValue)
                    litEndDate.Text = ((DateTime)workflow.EndDate).ToString("dd.MM.yyyy hh:mm");
                litStatus.Text = EnumHelper.GetEnumDescription((WorkflowStatus)workflow.Status);

                gridWorkflowElements.Where = new List<GridWhere>();
                gridWorkflowElements.Where.Add(new GridWhere { Column = "WorkflowID", Value = _workflowId .ToString() });

                gridWorkflowParameter.Where = new List<GridWhere>();
                gridWorkflowParameter.Where.Add(new GridWhere { Column = "WorkflowID", Value = _workflowId.ToString() });
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWorkflowElements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gridWorkflowElements_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                if (data["tbl_WorkflowElement_Result"] != null && !string.IsNullOrEmpty(data["tbl_WorkflowElement_Result"].ToString()))
                {
                    switch (data["tbl_WorkflowTemplateElement_ElementType"].ToString().ToEnum<WorkflowTemplateElementType>())
                    {
                        case WorkflowTemplateElementType.Message:
                            switch (data["tbl_WorkflowElement_Result"].ToString().ToEnum<ActionStatus>())
                            {
                                case ActionStatus.Done:
                                    ((Literal)item.FindControl("litResult")).Text = "Выполнено";
                                    break;
                                case ActionStatus.Error:
                                    ((Literal)item.FindControl("litResult")).Text = "Ошибка";
                                    break;
                            }
                            break;
                        case WorkflowTemplateElementType.WaitingEvent:
                            switch (data["tbl_WorkflowElement_Result"].ToString())
                            {
                                case "0":
                                    ((Literal)item.FindControl("litResult")).Text = "Событие не произошло";
                                    break;
                                case "1":
                                    ((Literal)item.FindControl("litResult")).Text = "Событие произошло";
                                    break;
                            }
                            break;
                        case WorkflowTemplateElementType.Task:
                            if (data["tbl_WorkflowElement_Result"] != null)
                            {
                                var resultId = data["tbl_WorkflowElement_Result"].ToString().ToGuid();
                                ((Literal)item.FindControl("litResult")).Text = DataManager.WorkflowTemplateElementResult.SelectById(resultId).Name;
                            }
                            break;
                        case WorkflowTemplateElementType.ExternalRequest:
                            switch (data["tbl_WorkflowElement_Result"].ToString())
                            {
                                case "1":
                                    ((Literal)item.FindControl("litResult")).Text = "Выполнено";
                                    break;
                                case "0":
                                    ((Literal)item.FindControl("litResult")).Text = "Ошибка";
                                    break;
                            }
                            break;
                    }
                }

                ((Literal)item.FindControl("litStatus")).Text = EnumHelper.GetEnumDescription(data["tbl_WorkflowElement_Status"].ToString().ToEnum<WorkflowElementStatus>());
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridWorkflowParameter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridWorkflowParameter_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;

                if (data["tbl_WorkflowParameter_Value"] != null && !string.IsNullOrEmpty(data["tbl_WorkflowParameter_Value"].ToString()))
                {
                    switch (data["tbl_WorkflowTemplateParameter_Name"].ToString())
                    {
                        case "Компания":
                            ((Literal)item.FindControl("litValue")).Text = DataManager.Company.SelectById(SiteId, data["tbl_WorkflowParameter_Value"].ToString().ToGuid()).Name;
                            break;
                        default:
                            ((Literal)item.FindControl("litValue")).Text = DataManager.Contact.SelectById(SiteId, data["tbl_WorkflowParameter_Value"].ToString().ToGuid()).UserFullName;
                            break;
                    }
                }
                ((Literal)item.FindControl("litName")).Text = data["tbl_WorkflowTemplateParameter_Name"].ToString();
            }
        }
    }
}