using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementParameterRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementParameterRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementParameterRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by element id.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementParameter> SelectByElementId(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElementParameter.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Selects the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementParameter SelectByValue(string value)
        {
            return _dataContext.tbl_WorkflowTemplateElementParameter.FirstOrDefault(a => a.Value == value);
        }


        /// <summary>
        /// Adds the specified workflow template element parameter.
        /// </summary>
        /// <param name="workflowTemplateElementParameter">The workflow template element parameter.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementParameter Add(tbl_WorkflowTemplateElementParameter workflowTemplateElementParameter)
        {
            if (workflowTemplateElementParameter.ID == Guid.Empty)
                workflowTemplateElementParameter.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementParameter.AddObject(workflowTemplateElementParameter);
            _dataContext.SaveChanges();

            return workflowTemplateElementParameter;
        }



        /// <summary>
        /// Deletes all by element id.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        public void DeleteAllByElementId(Guid workflowTemplateElementId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementParameter WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = workflowTemplateElementId });
        }
    }
}