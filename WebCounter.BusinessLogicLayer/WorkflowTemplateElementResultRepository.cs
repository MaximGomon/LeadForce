using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementResultRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementResultRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementResultRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementResult> SelectAll(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElementResult.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateElementResultId">The workflow template element result id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementResult SelectById(Guid workflowTemplateElementResultId)
        {
            return _dataContext.tbl_WorkflowTemplateElementResult.SingleOrDefault(a => a.ID == workflowTemplateElementResultId);
        }



        /// <summary>
        /// Adds the specified workflow template element result.
        /// </summary>
        /// <param name="workflowTemplateElementResult">The workflow template element result.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementResult Add(tbl_WorkflowTemplateElementResult workflowTemplateElementResult)
        {
            if (workflowTemplateElementResult.ID == Guid.Empty)
                workflowTemplateElementResult.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementResult.AddObject(workflowTemplateElementResult);
            _dataContext.SaveChanges();

            return workflowTemplateElementResult;
        }



        /// <summary>
        /// Updates the specified workflow template element result.
        /// </summary>
        /// <param name="workflowTemplateElementResult">The workflow template element result.</param>
        public void Update(tbl_WorkflowTemplateElementResult workflowTemplateElementResult)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element result list.
        /// </summary>
        /// <param name="workflowTemplateElementResultList">The workflow template element result list.</param>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        public void Save(List<WorkflowTemplateElementResultMap> workflowTemplateElementResultList, Guid workflowTemplateElementId)
        {
            var existsElementResults = SelectAll(workflowTemplateElementId).ToList();

            foreach (var elementResult in workflowTemplateElementResultList)
            {
                var existElementResult = existsElementResults.Where(a => a.ID == elementResult.ID).SingleOrDefault();

                if (existElementResult == null)
                {
                    _dataContext.tbl_WorkflowTemplateElementResult.AddObject(new tbl_WorkflowTemplateElementResult
                                                                             {
                                                                                 ID = elementResult.ID,
                                                                                 WorkflowTemplateElementID = workflowTemplateElementId,
                                                                                 Name = elementResult.Name
                                                                             });
                }
                else
                {
                    existElementResult.ID = elementResult.ID;
                    existElementResult.WorkflowTemplateElementID = workflowTemplateElementId;
                    existElementResult.Name = elementResult.Name;
                }
            }

            foreach (var existsElementResult in existsElementResults)
            {
                if (workflowTemplateElementResultList.Where(op => op.ID == existsElementResult.ID).SingleOrDefault() == null)
                    _dataContext.tbl_WorkflowTemplateElementResult.DeleteObject(existsElementResult);
            }

            _dataContext.SaveChanges();
        }
    }
}