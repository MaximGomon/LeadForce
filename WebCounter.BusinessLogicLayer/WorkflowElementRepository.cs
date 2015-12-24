using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowElementRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowElementRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowElementRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by workflow id.
        /// </summary>
        /// <param name="workflowId">The workflow id.</param>
        /// <returns></returns>
        public tbl_WorkflowElement SelectByWorkflowId(Guid workflowId)
        {
            return _dataContext.tbl_WorkflowElement.SingleOrDefault(a => a.WorkflowID == workflowId);
        }



        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public tbl_WorkflowElement SelectByValue(string value)
        {
            return _dataContext.tbl_WorkflowElement.SingleOrDefault(a => a.Value == value);
        }



        /// <summary>
        /// Selects the by workflow template element.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowElement> SelectByWorkflowTemplateElement(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowElement.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }
    }
}