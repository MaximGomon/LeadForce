using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateConditionEventRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateConditionEventRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateConditionEventRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateConditionEvent> SelectAllByWorkflowTemplateId(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Selects all by element id.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateConditionEvent> SelectAllByElementId(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.tbl_WorkflowTemplateElementEvent.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Adds the specified workflow template condition event.
        /// </summary>
        /// <param name="workflowTemplateConditionEvent">The workflow template condition event.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateConditionEvent Add(tbl_WorkflowTemplateConditionEvent workflowTemplateConditionEvent)
        {
            if (workflowTemplateConditionEvent.ID == Guid.Empty)
                workflowTemplateConditionEvent.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(workflowTemplateConditionEvent);
            _dataContext.SaveChanges();

            return workflowTemplateConditionEvent;
        }



        /// <summary>
        /// Updates the specified workflow template condition event.
        /// </summary>
        /// <param name="workflowTemplateConditionEvent">The workflow template condition event.</param>
        public void Update(tbl_WorkflowTemplateConditionEvent workflowTemplateConditionEvent)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template condition event list.
        /// </summary>
        /// <param name="workflowTemplateConditionEventList">The workflow template condition event list.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(List<WorkflowTemplateConditionEventMap> workflowTemplateConditionEventList, Guid workflowTemplateId)
        {
            var existsConditionEvents = SelectAllByWorkflowTemplateId(workflowTemplateId).ToList();

            foreach (var conditionEvent in workflowTemplateConditionEventList)
            {
                var existConditionEvent = existsConditionEvents.Where(a => a.ID == conditionEvent.ID).SingleOrDefault();

                if (existConditionEvent == null)
                {
                    _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(new tbl_WorkflowTemplateConditionEvent()
                                                                                  {
                                                                                      ID = conditionEvent.ID,
                                                                                      WorkflowTemplateID = workflowTemplateId,
                                                                                      Category = conditionEvent.Category,
                                                                                      ActivityType = conditionEvent.ActivityType,
                                                                                      Code = conditionEvent.Code,
                                                                                      ActualPeriod = conditionEvent.ActualPeriod,
                                                                                      Requisite = conditionEvent.Requisite,
                                                                                      Formula = conditionEvent.Formula,
                                                                                      Value = conditionEvent.Value,
                                                                                  });
                }
                else
                {
                    existConditionEvent.ID = conditionEvent.ID;
                    existConditionEvent.WorkflowTemplateID = workflowTemplateId;
                    existConditionEvent.Category = conditionEvent.Category;
                    existConditionEvent.ActivityType = conditionEvent.ActivityType;
                    existConditionEvent.Code = conditionEvent.Code;
                    existConditionEvent.ActualPeriod = conditionEvent.ActualPeriod;
                    existConditionEvent.Requisite = conditionEvent.Requisite;
                    existConditionEvent.Formula = conditionEvent.Formula;
                    existConditionEvent.Value = conditionEvent.Value;
                }
            }

            foreach (var existsConditionEvent in existsConditionEvents)
            {
                if (workflowTemplateConditionEventList.Where(op => op.ID == existsConditionEvent.ID).SingleOrDefault() == null)
                    _dataContext.tbl_WorkflowTemplateConditionEvent.DeleteObject(existsConditionEvent);
            }

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void DeleteAll(Guid workflowTemplateId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateConditionEvent WHERE WorkflowTemplateID = @WorkflowTemplateID",
                                             new SqlParameter { ParameterName = "WorkflowTemplateID", Value = workflowTemplateId });
        }
    }
}