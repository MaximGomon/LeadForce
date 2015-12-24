using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;
using System.Text.RegularExpressions;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElement> SelectAll(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateElement.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        public List<WorkflowTemplateElementMap> SelectAllMap(Guid workflowTemplateId)
        {
            var elements = _dataContext.tbl_WorkflowTemplateElement.Where(w => w.WorkflowTemplateID == workflowTemplateId).Select(
                a =>
                new WorkflowTemplateElementMap
                    {
                        ID = a.ID,
                        WorkflowTemplateID = a.WorkflowTemplateID,
                        Name = a.Name,
                        ElementType = a.ElementType,
                        Optional = a.Optional,
                        ResultName = a.ResultName,
                        AllowOptionalTransfer = a.AllowOptionalTransfer,
                        ShowCurrentUser = a.ShowCurrentUser,
                        Description = a.Description,
                        Order = a.Order,
                        StartAfter = a.StartAfter,
                        StartPeriod = a.StartPeriod,
                        DurationHours = a.DurationHours,
                        DurationMinutes = a.DurationMinutes,
                        ControlAfter = a.ControlAfter,
                        ControlPeriod = a.ControlPeriod,
                        ControlFromBeginProccess = a.ControlFromBeginProccess,
                        IsActive = a.IsActive,
                        IsDeleted = a.IsDeleted
                    }).OrderBy(a => a.Order).ToList();


            foreach (var element in elements)
            {
                element.Parameters = _dataContext.tbl_WorkflowTemplateElementParameter.Where(a => a.WorkflowTemplateElementID == element.ID).ToDictionary(a => a.Name, a => a.Value);
                element.ConditionEvent = new List<WorkflowTemplateConditionEventMap>(); // !!!
                var elementEvent = _dataContext.tbl_WorkflowTemplateElementEvent.SingleOrDefault(a => a.WorkflowTemplateElementID == element.ID);
                if (elementEvent != null)
                {
                    element.Condition = elementEvent.Condition;
                    element.ActivityCount = elementEvent.ActivityCount;

                    var conditionEvents = elementEvent.tbl_WorkflowTemplateConditionEvent;
                    element.ConditionEvent = new List<WorkflowTemplateConditionEventMap>();
                    foreach (var conditionEvent in conditionEvents)
                    {
                        element.ConditionEvent.Add(new WorkflowTemplateConditionEventMap
                                                       {
                                                           ID = conditionEvent.ID,
                                                           WorkflowTemplateID = conditionEvent.WorkflowTemplateID,
                                                           WorkflowTemplateElementEventID = conditionEvent.WorkflowTemplateElementEventID,
                                                           Category = conditionEvent.Category,
                                                           ActivityType = conditionEvent.ActivityType,
                                                           Code = conditionEvent.Code,
                                                           ActualPeriod = conditionEvent.ActualPeriod,
                                                           Requisite = conditionEvent.Requisite,
                                                           Formula = conditionEvent.Formula,
                                                           Value = conditionEvent.Value
                                                       });
                    }
                }

                var elementResults = _dataContext.tbl_WorkflowTemplateElementResult.Where(a => a.WorkflowTemplateElementID == element.ID);
                element.ElementResult = new List<WorkflowTemplateElementResultMap>();
                foreach (var elementResult in elementResults)
                {
                    element.ElementResult.Add(new WorkflowTemplateElementResultMap
                                                  {
                                                      ID = elementResult.ID,
                                                      WorkflowTemplateElementID = elementResult.WorkflowTemplateElementID,
                                                      Name = elementResult.Name,
                                                      IsSystem = elementResult.IsSystem
                                                  });
                }

                var elementTags = _dataContext.tbl_WorkflowTemplateElementTag.Where(a => a.WorkflowTemplateElementID == element.ID);
                element.Tag = new List<WorkflowTemplateElementTagMap>();
                foreach (var elementTag in elementTags)
                {
                    var workflowTemplateElementTag = new WorkflowTemplateElementTagMap
                                  {
                                      ID = elementTag.ID,
                                      WorkflowTemplateElementID = elementTag.WorkflowTemplateElementID,
                                      SiteTagID = elementTag.SiteTagID,
                                      Operation = elementTag.Operation
                                  };
                    var siteTagName = _dataContext.tbl_SiteTags.FirstOrDefault(a => a.ID == elementTag.SiteTagID);
                    if (siteTagName != null)
                        workflowTemplateElementTag.SiteTagName = siteTagName.Name;

                    element.Tag.Add(workflowTemplateElementTag);
                }

                var elementPeriods = _dataContext.tbl_WorkflowTemplateElementPeriod.Where(a => a.WorkflowTemplateElementID == element.ID);
                element.Period = new List<WorkflowTemplateElementPeriodMap>();
                foreach (var elementPeriod in elementPeriods)
                {
                    element.Period.Add(new WorkflowTemplateElementPeriodMap
                    {
                        ID = elementPeriod.ID,
                        WorkflowTemplateElementID = elementPeriod.WorkflowTemplateElementID,
                        DayWeek = elementPeriod.DayWeek,
                        FromTime = elementPeriod.FromTime,
                        ToTime = elementPeriod.ToTime
                    });
                }

                var elementExternalRequests = _dataContext.tbl_WorkflowTemplateElementExternalRequest.Where(a => a.WorkflowTemplateElementID == element.ID);
                element.ExternalRequest = new List<WorkflowTemplateElementExternalRequestMap>();
                foreach (var elementExternalRequest in elementExternalRequests)
                {
                    element.ExternalRequest.Add(new WorkflowTemplateElementExternalRequestMap
                    {
                        ID = elementExternalRequest.ID,
                        WorkflowTemplateElementID = elementExternalRequest.WorkflowTemplateElementID,
                        Name = elementExternalRequest.Name,
                        Value = elementExternalRequest.Value
                    });
                }
            }

            return elements;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElement SelectById(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElement.SingleOrDefault(a => a.ID == workflowTemplateElementId);
        }



        /// <summary>
        /// Adds the specified workflow template element.
        /// </summary>
        /// <param name="workflowTemplateElement">The workflow template element.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElement Add(tbl_WorkflowTemplateElement workflowTemplateElement)
        {
            if (workflowTemplateElement.ID == Guid.Empty)
                workflowTemplateElement.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElement.AddObject(workflowTemplateElement);
            _dataContext.SaveChanges();

            return workflowTemplateElement;
        }



        /// <summary>
        /// Updates the specified workflow template element.
        /// </summary>
        /// <param name="workflowTemplateElement">The workflow template element.</param>
        public void Update(tbl_WorkflowTemplateElement workflowTemplateElement)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element list.
        /// </summary>
        /// <param name="workflowTemplateElementList">The workflow template element list.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(List<WorkflowTemplateElementMap> workflowTemplateElementList, Guid workflowTemplateId)
        {
            var existsElements = SelectAll(workflowTemplateId).ToList();

            foreach (var element in workflowTemplateElementList)
            {
                var existElement = existsElements.Where(a => a.ID == element.ID).SingleOrDefault();

                if (existElement == null)
                {
                    _dataContext.tbl_WorkflowTemplateElement.AddObject(new tbl_WorkflowTemplateElement
                                                                        {
                                                                            ID = element.ID,
                                                                            WorkflowTemplateID = workflowTemplateId,
                                                                            Name = element.Name,
                                                                            ElementType = element.ElementType,
                                                                            Optional = element.Optional,
                                                                            ResultName = element.ResultName,
                                                                            AllowOptionalTransfer = element.AllowOptionalTransfer,
                                                                            ShowCurrentUser = element.ShowCurrentUser,
                                                                            Description = element.Description,
                                                                            Order = element.Order,
                                                                            StartAfter = element.StartAfter,
                                                                            StartPeriod = element.StartPeriod,
                                                                            DurationHours = element.DurationHours,
                                                                            DurationMinutes = element.DurationMinutes,
                                                                            ControlAfter = element.ControlAfter,
                                                                            ControlPeriod = element.ControlPeriod,
                                                                            ControlFromBeginProccess = element.ControlFromBeginProccess,
                                                                            IsActive = element.IsActive
                                                                        });
                }
                else
                {
                    existElement.ID = element.ID;
                    existElement.WorkflowTemplateID = workflowTemplateId;
                    existElement.Name = element.Name;
                    existElement.ElementType = element.ElementType;
                    existElement.Optional = element.Optional;
                    existElement.ResultName = element.ResultName;
                    existElement.AllowOptionalTransfer = element.AllowOptionalTransfer;
                    existElement.ShowCurrentUser = element.ShowCurrentUser;
                    existElement.Description = element.Description;
                    existElement.Order = element.Order;
                    existElement.StartAfter = element.StartAfter;
                    existElement.StartPeriod = element.StartPeriod;
                    existElement.DurationHours = element.DurationHours;
                    existElement.DurationMinutes = element.DurationMinutes;
                    existElement.ControlAfter = element.ControlAfter;
                    existElement.ControlPeriod = element.ControlPeriod;
                    existElement.ControlFromBeginProccess = element.ControlFromBeginProccess;
                    existElement.IsActive = element.IsActive;
                }

                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementParameter WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                if (element.Parameters != null && element.Parameters.Count > 0)
                {
                    foreach (var p in element.Parameters)
                    {
                        _dataContext.tbl_WorkflowTemplateElementParameter.AddObject(new tbl_WorkflowTemplateElementParameter
                                                                                        {
                                                                                            ID = Guid.NewGuid(),
                                                                                            WorkflowTemplateElementID = element.ID,
                                                                                            Name = p.Key,
                                                                                            Value = p.Value
                                                                                        });

                        Guid siteActionTemplateId;
                        if (element.ElementType == (int)WorkflowTemplateElementType.Message && Guid.TryParse(p.Value, out siteActionTemplateId))
                        {
                            var siteActionTemplate = _dataContext.tbl_SiteActionTemplate.SingleOrDefault(x => x.ID == siteActionTemplateId);
                            if (siteActionTemplate != null && siteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.BaseTemplate)                            
                                siteActionTemplate.UsageID = workflowTemplateId;
                        }
                    }
                }

                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementEvent WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                if ((WorkflowTemplateElementType)element.ElementType == WorkflowTemplateElementType.WaitingEvent)
                {
                    var newId = Guid.NewGuid();
                    _dataContext.tbl_WorkflowTemplateElementEvent.AddObject(new tbl_WorkflowTemplateElementEvent
                                                                                {
                                                                                    ID = newId,
                                                                                    WorkflowTemplateElementID = element.ID,
                                                                                    Condition = (int)element.Condition,
                                                                                    ActivityCount = element.ActivityCount
                                                                                });
                    foreach (var conditionEvent in element.ConditionEvent)
                    {
                        _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(new tbl_WorkflowTemplateConditionEvent
                                                                                      {
                                                                                          ID = Guid.NewGuid(),
                                                                                          WorkflowTemplateElementEventID = newId,
                                                                                          Category = conditionEvent.Category,
                                                                                          ActivityType = conditionEvent.ActivityType,
                                                                                          Code = conditionEvent.Code,
                                                                                          ActualPeriod = conditionEvent.ActualPeriod,
                                                                                          Requisite = conditionEvent.Requisite,
                                                                                          Formula = conditionEvent.Formula,
                                                                                          Value = conditionEvent.Value
                                                                                      });
                    }
                }

                if ((WorkflowTemplateElementType)element.ElementType != WorkflowTemplateElementType.Task)
                {
                    _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementRelation WHERE StartElementResult = @StartElementResult", new SqlParameter { ParameterName = "StartElementResult", Value = element.ID.ToString() });
                    _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementResult WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                }

                var workflowTemplateElementResultList = element.ElementResult;
                if ((WorkflowTemplateElementType)element.ElementType == WorkflowTemplateElementType.Task)
                {
                    var existsElementResults = _dataContext.tbl_WorkflowTemplateElementResult.Where(a => a.WorkflowTemplateElementID == element.ID).ToList();

                    foreach (var elementResult in workflowTemplateElementResultList)
                    {
                        var existElementResult = existsElementResults.Where(a => a.ID == elementResult.ID).SingleOrDefault();

                        if (existElementResult == null)
                        {
                            _dataContext.tbl_WorkflowTemplateElementResult.AddObject(new tbl_WorkflowTemplateElementResult
                            {
                                ID = elementResult.ID,
                                WorkflowTemplateElementID = element.ID,
                                Name = elementResult.Name,
                                IsSystem = elementResult.IsSystem
                            });
                        }
                        else
                        {
                            existElementResult.ID = elementResult.ID;
                            existElementResult.WorkflowTemplateElementID = element.ID;
                            existElementResult.Name = elementResult.Name;
                            existElementResult.IsSystem = elementResult.IsSystem;
                        }
                    }

                    foreach (var existsElementResult in existsElementResults)
                    {
                        if (workflowTemplateElementResultList.Where(op => op.ID == existsElementResult.ID).SingleOrDefault() == null)
                            _dataContext.tbl_WorkflowTemplateElementResult.DeleteObject(existsElementResult);
                    }
                }

                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementTag WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                if (element.Tag != null && element.Tag.Count > 0)
                {
                    foreach (var t in element.Tag)
                    {
                        _dataContext.tbl_WorkflowTemplateElementTag.AddObject(new tbl_WorkflowTemplateElementTag
                        {
                            ID = Guid.NewGuid(),
                            WorkflowTemplateElementID = element.ID,
                            SiteTagID = t.SiteTagID,
                            Operation = t.Operation
                        });
                    }
                }

                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementPeriod WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                if (element.Period != null && element.Period.Count > 0)
                {
                    foreach (var t in element.Period)
                    {
                        _dataContext.tbl_WorkflowTemplateElementPeriod.AddObject(new tbl_WorkflowTemplateElementPeriod
                        {
                            ID = Guid.NewGuid(),
                            WorkflowTemplateElementID = element.ID,
                            DayWeek = t.DayWeek,
                            FromTime = t.FromTime,
                            ToTime = t.ToTime
                        });
                    }
                }

                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateElementExternalRequest WHERE WorkflowTemplateElementID = @WorkflowTemplateElementID", new SqlParameter { ParameterName = "WorkflowTemplateElementID", Value = element.ID });
                if (element.ExternalRequest != null && element.ExternalRequest.Count > 0)
                {
                    foreach (var t in element.ExternalRequest)
                    {
                        _dataContext.tbl_WorkflowTemplateElementExternalRequest.AddObject(new tbl_WorkflowTemplateElementExternalRequest
                        {
                            ID = Guid.NewGuid(),
                            WorkflowTemplateElementID = element.ID,
                            Name = t.Name,
                            Value = t.Value
                        });
                    }
                }
            }

            foreach (var existsElement in existsElements)
            {
                var deletedworkflowTemplateElement = workflowTemplateElementList.SingleOrDefault(op => op.ID == existsElement.ID);
                if (deletedworkflowTemplateElement == null)
                {
                    var countExistingElement = _dataContext.tbl_WorkflowElement.Count(a => a.WorkflowTemplateElementID == existsElement.ID);
                    if (countExistingElement == 0)
                        _dataContext.tbl_WorkflowTemplateElement.DeleteObject(existsElement);
                    else
                        existsElement.IsDeleted = true;
                }
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
            var workflows = _dataContext.tbl_Workflow.Where(a => a.WorkflowTemplateID == workflowTemplateId);
            var workflowElements = _dataContext.tbl_WorkflowElement.Where(a => a.tbl_Workflow.WorkflowTemplateID == workflowTemplateId);

            if (workflowStartDate != null)
            {
                workflows = workflows.Where(a => a.StartDate >= workflowStartDate);
                workflowElements = workflowElements.Where(a => a.tbl_Workflow.StartDate >= workflowStartDate);
            }
                
            if (workflowEndDate != null)
            {
                workflows = workflows.Where(a => a.EndDate <= workflowEndDate);
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
               
            var countWorkflow = workflows.Count();
            return workflowElements.GroupBy(a => a.WorkflowTemplateElementID).Select(
                a =>
                new WorkflowConversion
                    {
                        Id = a.Key,
                        Count = a.Count(),
                        Conversion = countWorkflow != 0 ? Math.Round(((double)a.Count() / countWorkflow) * 100) : 0
                    }).ToList();
        }
    }
}