using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="workflowId">The workflow id.</param>
        /// <returns></returns>
        public tbl_Workflow SelectById(Guid siteId, Guid workflowId)
        {
            return _dataContext.tbl_Workflow.SingleOrDefault(a => a.SiteID == siteId && a.ID == workflowId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Workflow> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Workflow.Where(a => a.SiteID == siteId);
        }



        /// <summary>
        /// Selects the by workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Workflow> SelectByWorkflowTemplateId(Guid workflowTemplateId)
        {
            return _dataContext.tbl_Workflow.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Deletes the by workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void DeleteByWorkflowTemplateId(Guid workflowTemplateId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_Workflow WHERE WorkflowTemplateID = @WorkflowTemplateID",
                                             new SqlParameter { ParameterName = "WorkflowTemplateID", Value = workflowTemplateId });
        }
    }
}