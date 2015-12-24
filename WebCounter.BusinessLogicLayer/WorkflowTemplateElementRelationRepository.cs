using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementRelationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementRelationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementRelationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementRelation> SelectAll(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateElementRelation.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Selects all map.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public List<WorkflowTemplateElementRelationMap> SelectAllMap(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateElementRelation.Where(a => a.WorkflowTemplateID == workflowTemplateId).Select(a => new WorkflowTemplateElementRelationMap
                                                                                                                                           {
                                                                                                                                               ID = a.ID,
                                                                                                                                               WorkflowTemplateID = a.WorkflowTemplateID,
                                                                                                                                               StartElementID = a.StartElementID,
                                                                                                                                               StartElementResult = a.StartElementResult,
                                                                                                                                               EndElementID = a.EndElementID
                                                                                                                                           }).ToList();
        }



        /// <summary>
        /// Adds the specified workflow template element relation.
        /// </summary>
        /// <param name="workflowTemplateElementRelation">The workflow template element relation.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementRelation Add(tbl_WorkflowTemplateElementRelation workflowTemplateElementRelation)
        {
            if (workflowTemplateElementRelation.ID == Guid.Empty)
                workflowTemplateElementRelation.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementRelation.AddObject(workflowTemplateElementRelation);
            _dataContext.SaveChanges();

            return workflowTemplateElementRelation;
        }



        /// <summary>
        /// Updates the specified workflow template element relation.
        /// </summary>
        /// <param name="workflowTemplateElementRelation">The workflow template element relation.</param>
        public void Update(tbl_WorkflowTemplateElementRelation workflowTemplateElementRelation)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element relation list.
        /// </summary>
        /// <param name="workflowTemplateElementRelationList">The workflow template element relation list.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(List<WorkflowTemplateElementRelationMap> workflowTemplateElementRelationList, Guid workflowTemplateId)
        {
            var existsElementRelations = SelectAll(workflowTemplateId).ToList();

            foreach (var elementRelation in workflowTemplateElementRelationList)
            {
                var existElementRelation = existsElementRelations.Where(a => a.ID == elementRelation.ID).SingleOrDefault();

                if (existElementRelation == null)
                {
                    _dataContext.tbl_WorkflowTemplateElementRelation.AddObject(new tbl_WorkflowTemplateElementRelation
                                                                                   {
                                                                                       ID = elementRelation.ID,
                                                                                       WorkflowTemplateID = workflowTemplateId,
                                                                                       StartElementID = elementRelation.StartElementID,
                                                                                       StartElementResult = elementRelation.StartElementResult,
                                                                                       EndElementID = elementRelation.EndElementID
                                                                                   });
                }
                else
                {
                    existElementRelation.ID = elementRelation.ID;
                    existElementRelation.WorkflowTemplateID = workflowTemplateId;
                    existElementRelation.StartElementID = elementRelation.StartElementID;
                    existElementRelation.StartElementResult = elementRelation.StartElementResult;
                    existElementRelation.EndElementID = elementRelation.EndElementID;
                }
            }

            foreach (var existsElementRelation in existsElementRelations)
            {
                if (workflowTemplateElementRelationList.Where(op => op.ID == existsElementRelation.ID).SingleOrDefault() == null)
                    _dataContext.tbl_WorkflowTemplateElementRelation.DeleteObject(existsElementRelation);
            }

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Gets the map conversion.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public List<WorkflowConversion> GetMapConversion(Guid workflowTemplateId, DateTime? workflowStartDate, DateTime? workflowEndDate, DateTime? activityStartDate, DateTime? activityEndDate)
        {
            var workflowElements =
                _dataContext.tbl_WorkflowElement.Where(
                    a =>
                    a.tbl_Workflow.WorkflowTemplateID == workflowTemplateId &&
                    a.tbl_WorkflowTemplateElement.ElementType != (int)WorkflowTemplateElementType.EndProcess);

            if (workflowStartDate != null)
            {
                workflowElements = workflowElements.Where(a => a.tbl_Workflow.StartDate >= workflowStartDate);
            }

            if (workflowEndDate != null)
            {
                workflowElements = workflowElements.Where(a => a.tbl_Workflow.EndDate <= workflowEndDate);
            }

            if (activityStartDate != null)
            {
                workflowElements = workflowElements.Where(a => a.EndDate >= activityStartDate);
            }

            if (activityEndDate != null)
            {
                workflowElements = workflowElements.Where(a => a.EndDate <= activityEndDate);
            }

            var countWorkflowElements = workflowElements.GroupBy(a => a.WorkflowTemplateElementID).Select(a => new { Id = a.Key, Count = a.Count() });


            var workflowElementList = workflowElements.
                GroupBy(a => new {a.WorkflowTemplateElementID, a.Result}).Select(
                    a =>
                    new WorkflowConversion
                        {
                            Id = a.Key.WorkflowTemplateElementID,
                            Result = a.Key.Result,
                            Count =
                                a.Count(
                                    x =>
                                    x.Status == (int) WorkflowElementStatus.Done ||
                                    (x.tbl_WorkflowTemplateElement.ElementType == (int) WorkflowTemplateElementType.WaitingEvent && x.Status == (int)WorkflowElementStatus.Expired)) // Количество переходов
                        }).ToList();

            var resultList = new List<WorkflowConversion>();
            foreach (var workflowElement in workflowElementList)
            {
                var result = new WorkflowConversion
                                 {
                                     Id = workflowElement.Id,
                                     Result = workflowElement.Result,
                                     Count = workflowElement.Count
                                 };
                result.Conversion = Math.Round(((double)result.Count / countWorkflowElements.FirstOrDefault(a => a.Id == result.Id).Count) * 100); // [Количество переходов] / [Количество запуска начального элемента] * 100
                resultList.Add(result);
            }

            return resultList;

            /*return workflowElements.
                GroupBy(a => a.WorkflowTemplateElementID).Select(
                    a =>
                    new WorkflowConversion
                        {
                            Id = a.Key,
                            Count = a.Count(x => x.Status == (int) WorkflowElementStatus.Done), // Количество переходов
                            Conversion = (a.Count(x => x.Status == (int) WorkflowElementStatus.Done)/a.Count())*100 // [Количество переходов] / [Количество запуска начального элемента] * 100
                        }).ToList();*/
        }
    }
}