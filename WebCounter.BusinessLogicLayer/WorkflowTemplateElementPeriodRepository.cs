using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementPeriodRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementPeriodRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementPeriodRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementPeriod> SelectAll(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElementPeriod.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateElementPeriodId">The workflow template element period id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementPeriod SelectById(Guid workflowTemplateElementPeriodId)
        {
            return _dataContext.tbl_WorkflowTemplateElementPeriod.SingleOrDefault(a => a.ID == workflowTemplateElementPeriodId);
        }



        /// <summary>
        /// Adds the specified workflow template element period.
        /// </summary>
        /// <param name="workflowTemplateElementPeriod">The workflow template element period.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementPeriod Add(tbl_WorkflowTemplateElementPeriod workflowTemplateElementPeriod)
        {
            if (workflowTemplateElementPeriod.ID == Guid.Empty)
                workflowTemplateElementPeriod.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementPeriod.AddObject(workflowTemplateElementPeriod);
            _dataContext.SaveChanges();

            return workflowTemplateElementPeriod;
        }



        /// <summary>
        /// Updates the specified workflow template element period.
        /// </summary>
        /// <param name="workflowTemplateElementPeriod">The workflow template element period.</param>
        public void Update(tbl_WorkflowTemplateElementPeriod workflowTemplateElementPeriod)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element period list.
        /// </summary>
        /// <param name="workflowTemplateElementPeriodList">The workflow template element period list.</param>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        public void Save(List<WorkflowTemplateElementPeriodMap> workflowTemplateElementPeriodList, Guid workflowTemplateElementId)
        {
            var existsElementPeriods = SelectAll(workflowTemplateElementId).ToList();

            foreach (var elementPeriod in workflowTemplateElementPeriodList)
            {
                var existElementPeriod = existsElementPeriods.SingleOrDefault(a => a.ID == elementPeriod.ID);

                if (existElementPeriod == null)
                {
                    _dataContext.tbl_WorkflowTemplateElementPeriod.AddObject(new tbl_WorkflowTemplateElementPeriod
                                                                             {
                                                                                 ID = elementPeriod.ID,
                                                                                 WorkflowTemplateElementID = workflowTemplateElementId,
                                                                                 DayWeek = elementPeriod.DayWeek,
                                                                                 FromTime = elementPeriod.FromTime,
                                                                                 ToTime = elementPeriod.ToTime
                                                                             });
                }
                else
                {
                    existElementPeriod.ID = elementPeriod.ID;
                    existElementPeriod.WorkflowTemplateElementID = workflowTemplateElementId;
                    existElementPeriod.DayWeek = elementPeriod.DayWeek;
                    existElementPeriod.FromTime = elementPeriod.FromTime;
                    existElementPeriod.ToTime = elementPeriod.ToTime;
                }
            }

            foreach (var existsElementPeriod in existsElementPeriods)
            {
                if (workflowTemplateElementPeriodList.SingleOrDefault(op => op.ID == existsElementPeriod.ID) == null)
                    _dataContext.tbl_WorkflowTemplateElementPeriod.DeleteObject(existsElementPeriod);
            }

            _dataContext.SaveChanges();
        }
    }
}