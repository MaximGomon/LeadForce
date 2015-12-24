using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.WorkflowAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Workflow : IWorkflow
    {
        public IEnumerable<WorkflowTemplateElement> GetWorkflowTemplateElements(Guid workflowTemplateId)
        {
            var dataManager = new DataManager();
            var workflowTemplateElements = dataManager.WorkflowTemplateElement.SelectAllMap(workflowTemplateId);

            return
                workflowTemplateElements.Select(
                    a =>
                    new WorkflowTemplateElement
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
                            Parameters = a.Parameters,
                            Condition = a.Condition,
                            ActivityCount = a.ActivityCount,
                            IsActive = a.IsActive,
                            ConditionEvent = a.ConditionEvent.Select(x => new WorkflowTemplateConditionEvent
                                                                              {
                                                                                  ID = x.ID,
                                                                                  WorkflowTemplateElementEventID =
                                                                                      x.WorkflowTemplateElementEventID,
                                                                                  Category = x.Category,
                                                                                  ActivityType = x.ActivityType,
                                                                                  Code = x.Code,
                                                                                  ActualPeriod = x.ActualPeriod,
                                                                                  Requisite = x.Requisite,
                                                                                  Formula = x.Formula,
                                                                                  Value = x.Value
                                                                              }).ToList(),
                            ElementResult =
                                a.ElementResult.Select(
                                    x =>
                                    new WorkflowTemplateElementResult
                                        {
                                            ID = x.ID,
                                            WorkflowTemplateElementID = x.WorkflowTemplateElementID,
                                            Name = x.Name,
                                            IsSystem = x.IsSystem
                                        }).ToList(),
                            Tag =
                                a.Tag.Select(
                                    x =>
                                    new WorkflowTemplateElementTag
                                        {
                                            ID = x.ID,
                                            WorkflowTemplateElementID = x.WorkflowTemplateElementID,
                                            SiteTagID = x.SiteTagID,
                                            SiteTagName = dataManager.SiteTags.SelectById(x.SiteTagID).Name,
                                            Operation = x.Operation
                                        }).ToList(),
                            Period =
                                a.Period.Select(
                                    x =>
                                    new WorkflowTemplateElementPeriod
                                    {
                                        ID = x.ID,
                                        WorkflowTemplateElementID = x.WorkflowTemplateElementID,
                                        DayWeek = x.DayWeek,
                                        FromTime = x.FromTime,
                                        ToTime = x.ToTime
                                    }).ToList(),
                            ExternalRequest = 
                                a.ExternalRequest.Select(
                                    x =>
                                    new WorkflowTemplateElementExternalRequest
                                    {
                                        ID = x.ID,
                                        WorkflowTemplateElementID = x.WorkflowTemplateElementID,
                                        Name = x.Name,
                                        Value = x.Value
                                    }).ToList()
                        });
        }



        public string GetSiteActionTemplateName(Guid siteActionTemplateId)
        {
            var dataManager = new DataManager();
            var siteActionTemplate = dataManager.SiteActionTemplate.SelectById(siteActionTemplateId);

            return siteActionTemplate.Title;
        }



        public Dictionary<string, string> GetWorkflowTemplates(Guid siteId)
        {
            var dataManager = new DataManager();
            return dataManager.WorkflowTemplate.SelectAll(siteId).Where(a => a.Status != (int)WorkflowTemplateStatus.InPlans).ToDictionary(a => a.Name, a => a.ID.ToString());
        }



        public WorkflowTemplate GetWorkflowTemplate(Guid siteId, Guid workflowTemplateId)
        {
            var dataManager = new DataManager();
            var workflowTemplate = new WorkflowTemplate();
            var wt = dataManager.WorkflowTemplate.SelectById(siteId, workflowTemplateId);
            if (wt != null)
            {
                workflowTemplate.ID = wt.ID;
                workflowTemplate.WorkflowXml = wt.WorkflowXml;
            }

            return workflowTemplate;
        }



        public IEnumerable<TaskType> GetTaskTypes(Guid siteId)
        {
            var dataManager = new DataManager();
            var taskTypes = dataManager.TaskType.SelectAll(siteId);

            return taskTypes.Select(a => new TaskType {ID = a.ID, Title = a.Title});
        }



        public void SaveWorkflowTemplateElements(IEnumerable<WorkflowTemplateElement> workflowTemplateElementList, Guid workflowTemplateId)
        {
            var dataManager = new DataManager();
            var workflowTemplateElementMap = new List<WorkflowTemplateElementMap>();

            foreach (var workflowTemplateElement in workflowTemplateElementList)
            {
                var map = new WorkflowTemplateElementMap
                              {
                                  ID = workflowTemplateElement.ID,
                                  WorkflowTemplateID = workflowTemplateElement.WorkflowTemplateID,
                                  Name = workflowTemplateElement.Name,
                                  ElementType = workflowTemplateElement.ElementType,
                                  Optional = workflowTemplateElement.Optional,
                                  ResultName = workflowTemplateElement.ResultName,
                                  AllowOptionalTransfer = workflowTemplateElement.AllowOptionalTransfer,
                                  ShowCurrentUser = workflowTemplateElement.ShowCurrentUser,
                                  Description = workflowTemplateElement.Description,
                                  Order = workflowTemplateElement.Order,
                                  StartAfter = workflowTemplateElement.StartAfter,
                                  StartPeriod = workflowTemplateElement.StartPeriod,
                                  DurationHours = workflowTemplateElement.DurationHours,
                                  DurationMinutes = workflowTemplateElement.DurationMinutes,
                                  ControlAfter = workflowTemplateElement.ControlAfter,
                                  ControlPeriod = workflowTemplateElement.ControlPeriod,
                                  ControlFromBeginProccess = workflowTemplateElement.ControlFromBeginProccess,
                                  Parameters = new Dictionary<string, string>(),
                                  Condition = workflowTemplateElement.Condition,
                                  ActivityCount = workflowTemplateElement.ActivityCount,
                                  ConditionEvent = new List<WorkflowTemplateConditionEventMap>(),
                                  ElementResult = new List<WorkflowTemplateElementResultMap>(),
                                  Tag = new List<WorkflowTemplateElementTagMap>(),
                                  Period = new List<WorkflowTemplateElementPeriodMap>(),
                                  ExternalRequest = new List<WorkflowTemplateElementExternalRequestMap>(),
                                  IsActive = workflowTemplateElement.IsActive
                              };
                if (workflowTemplateElement.Parameters != null)
                {
                    foreach (var parameter in workflowTemplateElement.Parameters)
                        map.Parameters.Add(parameter.Key, parameter.Value);
                }


                if (workflowTemplateElement.ConditionEvent != null)
                {
                    foreach (var conditionEvent in workflowTemplateElement.ConditionEvent)
                    {
                        map.ConditionEvent.Add(new WorkflowTemplateConditionEventMap
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

                if (workflowTemplateElement.ElementResult != null)
                {
                    foreach (var result in workflowTemplateElement.ElementResult)
                    {
                        map.ElementResult.Add(new WorkflowTemplateElementResultMap
                        {
                            ID = result.ID,
                            WorkflowTemplateElementID = result.WorkflowTemplateElementID,
                            Name = result.Name,
                            IsSystem = result.IsSystem
                        });
                    }
                }

                if (workflowTemplateElement.Tag != null)
                {
                    foreach (var tag in workflowTemplateElement.Tag)
                    {
                        map.Tag.Add(new WorkflowTemplateElementTagMap
                        {
                            ID = tag.ID,
                            WorkflowTemplateElementID = tag.WorkflowTemplateElementID,
                            SiteTagID = tag.SiteTagID,
                            Operation = tag.Operation
                        });
                    }
                }

                if (workflowTemplateElement.Period != null)
                {
                    foreach (var period in workflowTemplateElement.Period)
                    {
                        map.Period.Add(new WorkflowTemplateElementPeriodMap
                        {
                            ID = period.ID,
                            WorkflowTemplateElementID = period.WorkflowTemplateElementID,
                            DayWeek = period.DayWeek,
                            FromTime = period.FromTime,
                            ToTime = period.ToTime
                        });
                    }
                }

                if (workflowTemplateElement.ExternalRequest != null)
                {
                    foreach (var externalRequest in workflowTemplateElement.ExternalRequest)
                    {
                        map.ExternalRequest.Add(new WorkflowTemplateElementExternalRequestMap
                        {
                            ID = externalRequest.ID,
                            WorkflowTemplateElementID = externalRequest.WorkflowTemplateElementID,
                            Name = externalRequest.Name,
                            Value = externalRequest.Value
                        });
                    }
                }

                workflowTemplateElementMap.Add(map);
            }

            dataManager.WorkflowTemplateElement.Save(workflowTemplateElementMap, workflowTemplateId);
        }



        public IEnumerable<WorkflowTemplateElementRelation> GetWorkflowTemplateElementRelations(Guid workflowTemplateId)
        {
            var dataManager = new DataManager();
            var workflowTemplateElementRelations = dataManager.WorkflowTemplateElementRelation.SelectAll(workflowTemplateId);

            return
                workflowTemplateElementRelations.Select(
                    a =>
                    new WorkflowTemplateElementRelation
                        {
                            ID = a.ID,
                            WorkflowTemplateID = a.WorkflowTemplateID,
                            StartElementID = a.StartElementID,
                            StartElementResult = a.StartElementResult,
                            EndElementID = a.EndElementID
                        });
        }



        public void SaveWorkflowTemplateElementRelations(IEnumerable<WorkflowTemplateElementRelation> workflowTemplateElementRelationList, Guid workflowTemplateId)
        {
            var dataManager = new DataManager();
            var workflowTemplateElementRelationMap = new List<WorkflowTemplateElementRelationMap>();

            foreach (var workflowTemplateElementRelation in workflowTemplateElementRelationList)
            {
                var map = new WorkflowTemplateElementRelationMap
                              {
                                  ID = workflowTemplateElementRelation.ID,
                                  WorkflowTemplateID = workflowTemplateElementRelation.WorkflowTemplateID,
                                  StartElementID = workflowTemplateElementRelation.StartElementID,
                                  StartElementResult = workflowTemplateElementRelation.StartElementResult,
                                  EndElementID = workflowTemplateElementRelation.EndElementID
                              };
                workflowTemplateElementRelationMap.Add(map);
            }

            dataManager.WorkflowTemplateElementRelation.Save(workflowTemplateElementRelationMap, workflowTemplateId);
        }



        public void SaveWorkflowTemplate(Guid siteId, Guid workflowTemplateId, string workflowXml)
        {
            var dataManager = new DataManager();
            var workflowTemplate = dataManager.WorkflowTemplate.SelectById(siteId, workflowTemplateId);
            workflowTemplate.WorkflowXml = workflowXml;

            dataManager.WorkflowTemplate.Update(workflowTemplate);
        }



        public IEnumerable<WorkflowTemplateConditionEvent> GetWorkflowTemplateConditionEvents(Guid workflowTemplateElementId)
        {
            var dataManager = new DataManager();
            var workflowTemplateConditionEvent = dataManager.WorkflowTemplateConditionEvent.SelectAllByElementId(workflowTemplateElementId);

            return workflowTemplateConditionEvent.Select(a => new WorkflowTemplateConditionEvent
                                                                  {
                                                                      ID = a.ID,
                                                                      WorkflowTemplateElementEventID = a.WorkflowTemplateElementEventID,
                                                                      Category = a.Category,
                                                                      ActivityType = a.ActivityType,
                                                                      Code = a.Code,
                                                                      ActualPeriod = a.ActualPeriod,
                                                                      Requisite = a.Requisite,
                                                                      Formula = a.Formula,
                                                                      Value = a.Value
                                                                  });
        }




        public IEnumerable<SiteColumn> GetSiteColumns(Guid siteId)
        {
            var dataManager = new DataManager();
            var siteColumns = dataManager.SiteColumns.SelectAll(siteId).Where(a => a.SiteActivityRuleID == null);

            return siteColumns.Select(a => new SiteColumn
                                               {
                                                   ID = a.ID,
                                                   SiteID = a.SiteID,
                                                   SiteActivityRuleID = a.SiteActivityRuleID,
                                                   Name = a.Name,
                                                   CategoryID = a.CategoryID,
                                                   TypeID = a.TypeID,
                                                   Code = a.Code
                                               });
        }



        public SiteColumn GetSiteColumn(Guid siteId, Guid siteColumnId)
        {
            var dataManager = new DataManager();
            var siteColumn = dataManager.SiteColumns.SelectById(siteId, siteColumnId);

            var map = new SiteColumn
                          {
                              ID = siteColumn.ID,
                              SiteID = siteColumn.SiteID,
                              SiteActivityRuleID = siteColumn.SiteActivityRuleID,
                              Name = siteColumn.Name,
                              CategoryID = siteColumn.CategoryID,
                              TypeID = siteColumn.TypeID,
                              Code = siteColumn.Code
                          };

            return map;
        }



        public IEnumerable<SiteColumnValue> GetSiteColumnValues(Guid siteColumnId)
        {
            var dataManager = new DataManager();
            var siteColumnValues = dataManager.SiteColumnValues.SelectAll(siteColumnId);

            return siteColumnValues.Select(a => new SiteColumnValue
                                                    {
                                                        ID = a.ID,
                                                        SiteColumnID = a.SiteColumnID,
                                                        Value = a.Value
                                                    });
        }



        public IEnumerable<SiteActivityScoreType> GetSiteActivityScoreTypes(Guid siteId)
        {
            var dataManager = new DataManager();
            var siteActivityScoreTypes = dataManager.SiteActivityScoreType.SelectAll(siteId);

            return siteActivityScoreTypes.Select(a => new SiteActivityScoreType
                                                          {
                                                              ID = a.ID,
                                                              SiteID = a.SiteID,
                                                              Title = a.Title
                                                          });
        }



        public Dictionary<string, string> GetCodes(Guid siteId, int activityType)
        {
            var dataManager = new DataManager();
            var codes = new Dictionary<string, string>();

            var siteActivityRules = new List<tbl_SiteActivityRules>();
            var siteEventTemplates = new List<tbl_SiteEventTemplates>();
            var siteActionTemplates = new List<tbl_SiteActionTemplate>();
            switch (activityType)
            {
                case 1:
                    var contactActivities = dataManager.ContactActivity.Select(siteId, null, (ActivityType)activityType).Select(sua => new { sua.ActivityCode }).Distinct();
                    foreach (var contactActivity in contactActivities)
                        codes.Add(contactActivity.ActivityCode, contactActivity.ActivityCode);
                        //rcbCode.Items.Add(new RadComboBoxItem(HttpUtility.UrlDecode(contactActivity.ActivityCode)));
                    break;
                case 2:                    
                    foreach (var siteActivityRule in dataManager.Links.SelectByRuleType(siteId, new List<int> { (int)RuleType.Link }).ToList())
                        codes.Add(siteActivityRule.Name, siteActivityRule.Code);
                    break;
                case 3:
                case 4:
                case 9:
                    siteActivityRules = dataManager.SiteActivityRules.SelectByRuleType(siteId, new List<int> { (int)RuleType.Form, (int)RuleType.ExternalForm }).ToList();
                    foreach (var siteActivityRule in siteActivityRules)
                        codes.Add(siteActivityRule.Name, siteActivityRule.Code);
                    break;
                case 10:
                    foreach (var siteActivityRule in dataManager.Links.SelectByRuleType(siteId, new List<int> { (int)RuleType.File }).ToList())
                        codes.Add(siteActivityRule.Name, siteActivityRule.Code);
                    break;
                case 5:
                    siteEventTemplates = dataManager.SiteEventTemplates.SelectAll(siteId);
                    foreach (var siteEventTemplate in siteEventTemplates)
                        codes.Add(siteEventTemplate.Title, siteEventTemplate.ID.ToString());
                    break;
                case 7:
                    siteActionTemplates = dataManager.SiteActionTemplate.SelectAll(siteId);
                    foreach (var siteActionTemplate in siteActionTemplates)
                        codes.Add(siteActionTemplate.Title, siteActionTemplate.ID.ToString());
                    break;
            }

            return codes;
        }



        public IEnumerable<string> GetModules(Guid userId)
        {
            var dataManager = new DataManager();
            var user = dataManager.User.SelectById(userId);
            var modules = new[] { "SiteAction", "Tasks" };

            return modules.Where(module => Access.Check(user, module).Read);
        }



        public Dictionary<string, string> GetSiteTags(Guid siteId)
        {
            var dataManager = new DataManager();

            return dataManager.SiteTags.SelectAll(siteId).Where(a => a.ObjectTypeID == 1).ToDictionary(a => a.Name, a => a.ID.ToString());
        }



        public IEnumerable<WorkflowTemplateElementTag> GetWorkflowTemplateElementTags(Guid workflowTemplateElementId)
        {
            var dataManager = new DataManager();
            return
                dataManager.WorkflowTemplateElementTag.SelectAll(workflowTemplateElementId).Select(
                    a =>
                    new WorkflowTemplateElementTag
                        {
                            ID = a.ID,
                            WorkflowTemplateElementID = a.WorkflowTemplateElementID,
                            SiteTagID = a.SiteTagID,
                            SiteTagName = dataManager.SiteTags.SelectById(a.SiteTagID).Name,
                            Operation = a.Operation
                        });
        }



        public Guid AddSiteTag(Guid siteId, Guid userId, string name)
        {
            var dataManager = new DataManager();
            var siteTagAdded = dataManager.SiteTags.Add(new tbl_SiteTags {SiteID = siteId, UserID = userId, Name = name, ObjectTypeID = 1});

            return siteTagAdded.ID;
        }



        public IEnumerable<WorkflowConversion> GetElementConversion(Guid workflowTemplateId, DateTime? workflowStartDate, DateTime? workflowEndDate, DateTime? activityStartDate, DateTime? activityEndDate)
        {
            var dataManager = new DataManager();

            return
                dataManager.WorkflowTemplateElement.GetMapConversion(workflowTemplateId, workflowStartDate, workflowEndDate, activityStartDate, activityEndDate).Select(
                    a =>
                    new WorkflowConversion
                        {
                            Id = a.Id,
                            Count = a.Count,
                            Conversion = a.Conversion
                        });
        }



        public IEnumerable<WorkflowConversion> GetRelationConversion(Guid workflowTemplateId, DateTime? workflowStartDate, DateTime? workflowEndDate, DateTime? activityStartDate, DateTime? activityEndDate)
        {
            var dataManager = new DataManager();

            return
                dataManager.WorkflowTemplateElementRelation.GetMapConversion(workflowTemplateId, workflowStartDate, workflowEndDate, activityStartDate, activityEndDate).Select(
                    a =>
                    new WorkflowConversion
                    {
                        Id = a.Id,
                        Result = a.Result ?? "",
                        Count = a.Count,
                        Conversion = a.Conversion
                    });
        }


        public bool ShowDeletedMessage(IEnumerable<Guid> workflowTemplatesIdList)
        {
            var dataManager = new DataManager();

            foreach (var id in workflowTemplatesIdList)
            {
                var elements = dataManager.WorkflowElement.SelectByWorkflowTemplateElement(id);
                if (elements.Count(a => a.Status == 0) > 0)
                    return true;
            }

            return false;
        }
    }
}