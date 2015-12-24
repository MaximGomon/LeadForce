using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplate SelectById(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplate.SingleOrDefault(a => a.ID == workflowTemplateId && a.DataBaseStatusID == (int)DataBaseStatus.Active);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplate SelectById(Guid siteId, Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplate.SingleOrDefault(a => a.SiteID == siteId && a.ID == workflowTemplateId && a.DataBaseStatusID == (int)DataBaseStatus.Active);
        }



        public tbl_WorkflowTemplate SelectByIdWithDeleted(Guid siteId, Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplate.SingleOrDefault(a => a.SiteID == siteId && a.ID == workflowTemplateId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplate> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_WorkflowTemplate.Where(wt => wt.SiteID == siteId && wt.DataBaseStatusID == (int)DataBaseStatus.Active);
        }



        /// <summary>
        /// Selects all templates.
        /// </summary>
        /// <returns></returns>
        public List<tbl_WorkflowTemplate> SelectAllTemplates()
        {
            return _dataContext.tbl_WorkflowTemplate.Where(a => a.tbl_Sites.IsTemplate).ToList();
        }



        /// <summary>
        /// Adds the specified workflow template.
        /// </summary>
        /// <param name="workflowTemplate">The workflow template.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplate Add(tbl_WorkflowTemplate workflowTemplate)
        {
            if (workflowTemplate.ID == Guid.Empty)
            {
                workflowTemplate.ID = Guid.NewGuid();
                workflowTemplate.DataBaseStatusID = (int) DataBaseStatus.Active;
            }
            _dataContext.tbl_WorkflowTemplate.AddObject(workflowTemplate);
            _dataContext.SaveChanges();

            return workflowTemplate;
        }



        /// <summary>
        /// Updates the specified workflow template.
        /// </summary>
        /// <param name="workflowTemplate">The workflow template.</param>
        public void Update(tbl_WorkflowTemplate workflowTemplate)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Copies the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public Guid Copy(Guid siteId, Guid workflowTemplateId)
        {
            var elements = new Dictionary<string, Guid>(); // 1 - old id, 2 - new id
            var results = new Dictionary<string, Guid>(); // 1 - old id, 2 - new id

            var workflowTemplate = SelectById(siteId, workflowTemplateId);
            var workflowTemplateCopy = new tbl_WorkflowTemplate
            {
                ID = Guid.NewGuid(),
                SiteID = siteId,
                Name = workflowTemplate.Name,
                ContactID = workflowTemplate.ContactID,
                ModuleID = workflowTemplate.ModuleID,
                Status = (int)WorkflowTemplateStatus.InPlans,
                StartDate = workflowTemplate.StartDate,
                EndDate = workflowTemplate.EndDate,
                Description = workflowTemplate.Description,
                ManualStart = workflowTemplate.ManualStart,
                AutomaticMethod = workflowTemplate.AutomaticMethod,
                Event = workflowTemplate.Event,
                Frequency = workflowTemplate.Frequency,
                Condition = workflowTemplate.Condition,
                ActivityCount = workflowTemplate.ActivityCount,
                DenyMultipleRun = workflowTemplate.DenyMultipleRun
            };
            _dataContext.tbl_WorkflowTemplate.AddObject(workflowTemplateCopy);

            var workflowTemplateElements = _dataContext.tbl_WorkflowTemplateElement.Where(a => a.WorkflowTemplateID == workflowTemplateId);
            if (workflowTemplateElements.Any())
            {
                foreach (var workflowTemplateElement in workflowTemplateElements)
                {
                    var workflowTemplateElementCopy = new tbl_WorkflowTemplateElement
                    {
                        ID = Guid.NewGuid(),
                        WorkflowTemplateID = workflowTemplateCopy.ID,
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
                        ControlFromBeginProccess = workflowTemplateElement.ControlFromBeginProccess
                    };
                    _dataContext.tbl_WorkflowTemplateElement.AddObject(workflowTemplateElementCopy);
                    elements.Add(workflowTemplateElement.ID.ToString(), workflowTemplateElementCopy.ID);

                    var workflowTemplateElementParameters = _dataContext.tbl_WorkflowTemplateElementParameter.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementParameters.Any())
                    {
                        foreach (var workflowTemplateElementParameter in workflowTemplateElementParameters)
                        {
                            var workflowTemplateElementParameterCopy = new tbl_WorkflowTemplateElementParameter
                                                                           {
                                                                               ID = Guid.NewGuid(),
                                                                               WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                                                               Name = workflowTemplateElementParameter.Name,
                                                                               Value = workflowTemplateElementParameter.Value
                                                                           };
                            _dataContext.tbl_WorkflowTemplateElementParameter.AddObject(workflowTemplateElementParameterCopy);
                        }
                    }

                    var workflowTemplateElementEvents = _dataContext.tbl_WorkflowTemplateElementEvent.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementEvents.Any())
                    {
                        foreach (var workflowTemplateElementEvent in workflowTemplateElementEvents)
                        {
                            var workflowTemplateElementEventCopy = new tbl_WorkflowTemplateElementEvent
                            {
                                ID = Guid.NewGuid(),
                                WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                Condition = workflowTemplateElementEvent.Condition,
                                ActivityCount = workflowTemplateElementEvent.ActivityCount
                            };
                            _dataContext.tbl_WorkflowTemplateElementEvent.AddObject(workflowTemplateElementEventCopy);

                            var workflowTemplateConditionEventElements = _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.tbl_WorkflowTemplateElementEvent.WorkflowTemplateElementID == workflowTemplateElementEvent.ID);
                            if (workflowTemplateConditionEventElements.Any())
                            {
                                foreach (var workflowTemplateConditionEventElement in workflowTemplateConditionEventElements)
                                {
                                    var workflowTemplateConditionEventElementCopy = new tbl_WorkflowTemplateConditionEvent
                                    {
                                        ID = Guid.NewGuid(),
                                        WorkflowTemplateElementEventID = workflowTemplateElementEventCopy.ID,
                                        Category = workflowTemplateConditionEventElement.Category,
                                        ActivityType = workflowTemplateConditionEventElement.ActivityType,
                                        Code = workflowTemplateConditionEventElement.Code,
                                        ActualPeriod = workflowTemplateConditionEventElement.ActualPeriod,
                                        Requisite = workflowTemplateConditionEventElement.Requisite,
                                        Formula = workflowTemplateConditionEventElement.Formula,
                                        Value = workflowTemplateConditionEventElement.Value
                                    };
                                    _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(workflowTemplateConditionEventElementCopy);
                                }
                            }
                        }
                    }


                    var workflowTemplateElementResults = _dataContext.tbl_WorkflowTemplateElementResult.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementResults.Any())
                    {
                        foreach (var workflowTemplateElementResult in workflowTemplateElementResults)
                        {
                            var workflowTemplateElementResultCopy = new tbl_WorkflowTemplateElementResult
                            {
                                ID = Guid.NewGuid(),
                                WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                Name = workflowTemplateElementResult.Name,
                                IsSystem = workflowTemplateElementResult.IsSystem
                            };
                            _dataContext.tbl_WorkflowTemplateElementResult.AddObject(workflowTemplateElementResultCopy);
                            results.Add(workflowTemplateElementResult.ID.ToString(), workflowTemplateElementResultCopy.ID);
                        }
                    }


                    var workflowTemplateElementTags = _dataContext.tbl_WorkflowTemplateElementTag.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementTags.Any())
                    {
                        foreach (var workflowTemplateElementTag in workflowTemplateElementTags)
                        {
                            var workflowTemplateElementTagCopy = new tbl_WorkflowTemplateElementTag
                            {
                                ID = Guid.NewGuid(),
                                WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                SiteTagID = workflowTemplateElementTag.SiteTagID,
                                Operation = workflowTemplateElementTag.Operation
                            };
                            _dataContext.tbl_WorkflowTemplateElementTag.AddObject(workflowTemplateElementTagCopy);
                        }
                    }

                    var workflowTemplateElementPeriods = _dataContext.tbl_WorkflowTemplateElementPeriod.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementPeriods.Any())
                    {
                        foreach (var workflowTemplateElementPeriod in workflowTemplateElementPeriods)
                        {
                            var workflowTemplateElementPeriodCopy = new tbl_WorkflowTemplateElementPeriod
                            {
                                ID = Guid.NewGuid(),
                                WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                DayWeek = workflowTemplateElementPeriod.DayWeek,
                                FromTime = workflowTemplateElementPeriod.FromTime,
                                ToTime = workflowTemplateElementPeriod.ToTime
                            };
                            _dataContext.tbl_WorkflowTemplateElementPeriod.AddObject(workflowTemplateElementPeriodCopy);
                        }
                    }

                    var workflowTemplateElementExternalRequests = _dataContext.tbl_WorkflowTemplateElementExternalRequest.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                    if (workflowTemplateElementExternalRequests.Any())
                    {
                        foreach (var workflowTemplateElementExternalRequest in workflowTemplateElementExternalRequests)
                        {
                            var workflowTemplateElementExternalRequestCopy = new tbl_WorkflowTemplateElementExternalRequest
                            {
                                ID = Guid.NewGuid(),
                                WorkflowTemplateElementID = workflowTemplateElementCopy.ID,
                                Name = workflowTemplateElementExternalRequest.Name,
                                Value = workflowTemplateElementExternalRequest.Value
                            };
                            _dataContext.tbl_WorkflowTemplateElementExternalRequest.AddObject(workflowTemplateElementExternalRequestCopy);
                        }
                    }
                }
            }

            var workflowTemplateElementRelations = _dataContext.tbl_WorkflowTemplateElementRelation.Where(a => a.WorkflowTemplateID == workflowTemplateId);
            if (workflowTemplateElementRelations.Any())
            {
                foreach (var workflowTemplateElementRelation in workflowTemplateElementRelations)
                {
                    var workflowTemplateElementRelationCopy = new tbl_WorkflowTemplateElementRelation
                    {
                        ID = Guid.NewGuid(),
                        WorkflowTemplateID = workflowTemplateCopy.ID,
                        StartElementID = elements.FirstOrDefault(a => a.Key == workflowTemplateElementRelation.StartElementID.ToString()).Value,
                        //StartElementResult = workflowTemplateElementRelation.StartElementResult,
                        EndElementID = elements.FirstOrDefault(a => a.Key == workflowTemplateElementRelation.EndElementID.ToString()).Value
                    };
                    var resultValue = Guid.Empty;
                    if (Guid.TryParse(workflowTemplateElementRelation.StartElementResult, out resultValue))
                        workflowTemplateElementRelationCopy.StartElementResult = results.FirstOrDefault(a => a.Key == workflowTemplateElementRelation.StartElementResult).Value.ToString();
                    else
                        workflowTemplateElementRelationCopy.StartElementResult = workflowTemplateElementRelation.StartElementResult;
                    _dataContext.tbl_WorkflowTemplateElementRelation.AddObject(workflowTemplateElementRelationCopy);
                }
            }

            var workflowTemplateConditionEvents = _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.WorkflowTemplateID == workflowTemplateId);
            if (workflowTemplateConditionEvents.Any())
            {
                foreach (var workflowTemplateConditionEvent in workflowTemplateConditionEvents)
                {
                    var workflowTemplateConditionEventCopy = new tbl_WorkflowTemplateConditionEvent
                    {
                        ID = Guid.NewGuid(),
                        WorkflowTemplateID = workflowTemplateCopy.ID,
                        Category = workflowTemplateConditionEvent.Category,
                        ActivityType = workflowTemplateConditionEvent.ActivityType,
                        Code = workflowTemplateConditionEvent.Code,
                        ActualPeriod = workflowTemplateConditionEvent.ActualPeriod,
                        Requisite = workflowTemplateConditionEvent.Requisite,
                        Formula = workflowTemplateConditionEvent.Formula,
                        Value = workflowTemplateConditionEvent.Value
                    };
                    _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(workflowTemplateConditionEventCopy);
                }
            }

            var workflowTemplateParameters = _dataContext.tbl_WorkflowTemplateParameter.Where(a => a.WorkflowTemplateID == workflowTemplateId);
            if (workflowTemplateParameters.Any())
            {
                foreach (var workflowTemplateParameter in workflowTemplateParameters)
                {
                    var workflowTemplateParameterCopy = new tbl_WorkflowTemplateParameter
                    {
                        ID = Guid.NewGuid(),
                        WorkflowTemplateID = workflowTemplateCopy.ID,
                        Name = workflowTemplateParameter.Name,
                        ModuleID = workflowTemplateParameter.ModuleID,
                        IsSystem = workflowTemplateParameter.IsSystem,
                        Description = workflowTemplateParameter.Description
                    };
                    _dataContext.tbl_WorkflowTemplateParameter.AddObject(workflowTemplateParameterCopy);
                }
            }


            //Workflow goal
            var workflowTemplateGoals = _dataContext.tbl_WorkflowTemplateGoal.Where(o => o.WorkflowTemplateID == workflowTemplateId);

            foreach (var workflowTemplateGoal in workflowTemplateGoals)
            {
                var workflowTemplateGoalElements = workflowTemplateGoal.tbl_WorkflowTemplateElement.ToList();
                _dataContext.Detach(workflowTemplateGoal);
                workflowTemplateGoal.ID = Guid.NewGuid();
                workflowTemplateGoal.WorkflowTemplateID = workflowTemplateCopy.ID;

                foreach (var workflowTemplateGoalElement in workflowTemplateGoalElements)
                {
                    workflowTemplateGoal.tbl_WorkflowTemplateElement.Clear();
                    workflowTemplateGoal.tbl_WorkflowTemplateElement.Add(
                        _dataContext.tbl_WorkflowTemplateElement.SingleOrDefault(
                            o => o.ID == workflowTemplateGoalElement.ID));
                }

                _dataContext.tbl_WorkflowTemplateGoal.AddObject(workflowTemplateGoal);
            }

            _dataContext.SaveChanges();

            return workflowTemplateCopy.ID;
        }



        public tbl_WorkflowTemplate CopyWizard(Guid workflowTemplateId, Guid siteId, Guid contactId, List<WorkflowTemplateWizardRole> rolesList, int? condition, int? activityCount, List<WorkflowTemplateWizardConditionEvent> conditionEventList, List<WorkflowTemplateWizardTag> tagsList, List<tbl_SiteActionTemplate> actionTemplatesList, List<WorkflowTemplateWizardMaterial> materialsList)
        {
            var elementsOldNew = new List<dynamic>();
            var resultsOldNew = new List<dynamic>();
            var relationsOldNew = new List<dynamic>();
            Guid parentSiteId;

            var workflowTemplate = SelectById(workflowTemplateId);
            if (workflowTemplate != null)
            {
                var newWorkflowTemplate = new tbl_WorkflowTemplate();
                newWorkflowTemplate = workflowTemplate;
                parentSiteId = workflowTemplate.SiteID;
                _dataContext.Detach(newWorkflowTemplate);
                newWorkflowTemplate.ID = new Guid();
                newWorkflowTemplate.SiteID = siteId;
                newWorkflowTemplate.ContactID = contactId;
                newWorkflowTemplate.Condition = condition;
                newWorkflowTemplate.ActivityCount = activityCount;
                ////newWorkflowTemplate.WorkflowXml = null;
                newWorkflowTemplate = Add(newWorkflowTemplate);

                var materialFiles = materialsList.Where(a => a.Type == (int)MaterialType.File);
                foreach (var materialFile in materialFiles)
                {
                    if (materialFile.OldValue == materialFile.Value)
                    {
                        var oldSiteActivityRuleId = materialFile.OldValue.ToGuid();
                        var oldSiteActivityRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == oldSiteActivityRuleId);

                        var fsp = new FileSystemProvider();
                        var fileMetadata = fsp.Copy(oldSiteActivityRule.SiteID, oldSiteActivityRule.Name, siteId, "WorkflowTemplates");

                        var link = new tbl_Links();
                        link.ID = Guid.NewGuid();
                        link.SiteID = siteId;
                        link.Name = fileMetadata.Name;
                        link.RuleTypeID = (int)RuleType.File;
                        link.URL = fileMetadata.Name;
                        link.FileSize = fileMetadata.Size;
                        string code = String.Format("file_[{0}]_[{1}]", DateTime.Now.ToString("ddMMyyyy"), DateTime.Now.ToString("mmss"));
                        var maxCode = _dataContext.tbl_Links.Count(a => a.Code == code && a.SiteID == siteId);
                        if (maxCode != 0) maxCode++;
                        link.Code = code + (maxCode != 0 ? String.Format("[{0}]", maxCode >= 10 ? maxCode.ToString() : "0" + maxCode.ToString()) : "");
                        _dataContext.tbl_Links.AddObject(link);

                        materialFile.Value = link.ID.ToString();
                    }
                }

                _dataContext.SaveChanges();

                var workflowTemplateElements = _dataContext.tbl_WorkflowTemplateElement.Where(a => a.WorkflowTemplateID == workflowTemplateId);
                if (workflowTemplateElements.Any())
                {
                    foreach (var workflowTemplateElement in workflowTemplateElements)
                    {
                        // Elements
                        _dataContext.Detach(workflowTemplateElement);
                        var oldWorkflowTemplateElementId = workflowTemplateElement.ID;
                        var newWorkflowTemplateElementId = Guid.NewGuid();
                        workflowTemplateElement.ID = newWorkflowTemplateElementId;
                        workflowTemplateElement.WorkflowTemplateID = newWorkflowTemplate.ID;
                        _dataContext.tbl_WorkflowTemplateElement.AddObject(workflowTemplateElement);
                        
                        elementsOldNew.Add(new { OldId = oldWorkflowTemplateElementId, NewId = newWorkflowTemplateElementId });

                        // Tags
                        switch (workflowTemplateElement.ElementType)
                        {
                            case (int)WorkflowTemplateElementType.Tag:
                                var tag = new tbl_WorkflowTemplateElementTag { ID = Guid.NewGuid(), WorkflowTemplateElementID = newWorkflowTemplateElementId };
                                var tagFromList = tagsList.FirstOrDefault(a => a.Id == oldWorkflowTemplateElementId);
                                tag.SiteTagID = tagFromList.TagId;
                                tag.Operation = tagFromList.Operation;
                                _dataContext.tbl_WorkflowTemplateElementTag.AddObject(tag);
                                break;
                        }
                        
                        // Parameters
                        var workflowTemplateElementParameters = _dataContext.tbl_WorkflowTemplateElementParameter.Where(a => a.WorkflowTemplateElementID == oldWorkflowTemplateElementId);
                        if (workflowTemplateElementParameters.Any())
                        {
                            foreach (var workflowTemplateElementParameter in workflowTemplateElementParameters)
                            {
                                _dataContext.Detach(workflowTemplateElementParameter);
                                workflowTemplateElementParameter.ID = Guid.NewGuid();
                                workflowTemplateElementParameter.WorkflowTemplateElementID = newWorkflowTemplateElementId;
                                if (workflowTemplateElement.ElementType == (int)WorkflowTemplateElementType.Message && workflowTemplateElementParameter.Name == "SiteActionTemplateID")
                                {
                                    var val = workflowTemplateElementParameter.Value.ToGuid();
                                    var _actionTemplate = actionTemplatesList.FirstOrDefault(a => a.ID == val);
                                    var actionTemplate = _dataContext.tbl_SiteActionTemplate.FirstOrDefault(a => a.ID == val);
                                    _dataContext.Detach(actionTemplate);
                                    actionTemplate.ID = Guid.NewGuid();
                                    actionTemplate.SiteID = siteId;
                                    actionTemplate.OwnerID = contactId;
                                    actionTemplate.MessageCaption = _actionTemplate.MessageCaption;
                                    actionTemplate.MessageBody = _actionTemplate.MessageBody;
                                    actionTemplate.FromContactRoleID = _actionTemplate.FromContactRoleID;
                                    actionTemplate.FromEmail = _actionTemplate.FromEmail;
                                    actionTemplate.FromName = _actionTemplate.FromName;

                                    var files = materialsList.Where(a => a.Type == (int)MaterialType.File);
                                    foreach (var file in files)
                                    {
                                        var oldSiteActivityRuleId = file.OldValue.ToGuid();
                                        var oldSiteActivityRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == oldSiteActivityRuleId);
                                        var siteActivityRuleId = file.Value.ToGuid();
                                        var siteActivityRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == siteActivityRuleId);

                                        actionTemplate.MessageBody = actionTemplate.MessageBody.Replace("#Link." + oldSiteActivityRule.Code + "#", "#Link." + siteActivityRule.Code + "#");
                                    }

                                    _dataContext.tbl_SiteActionTemplate.AddObject(actionTemplate);

                                    foreach (var siteActionTemplateRecipient in _actionTemplate.tbl_SiteActionTemplateRecipient)
                                    {
                                        var newSiteActionTemplateRecipient = new tbl_SiteActionTemplateRecipient();
                                        newSiteActionTemplateRecipient.ID = Guid.NewGuid();
                                        newSiteActionTemplateRecipient.SiteActionTemplateID = actionTemplate.ID;
                                        newSiteActionTemplateRecipient.ContactRoleID = siteActionTemplateRecipient.ContactRoleID;
                                        newSiteActionTemplateRecipient.ContactID = siteActionTemplateRecipient.ContactID;
                                        newSiteActionTemplateRecipient.Email = siteActionTemplateRecipient.Email;
                                        newSiteActionTemplateRecipient.DisplayName = siteActionTemplateRecipient.DisplayName;

                                        _dataContext.tbl_SiteActionTemplateRecipient.AddObject(newSiteActionTemplateRecipient);
                                    }

                                    workflowTemplateElementParameter.Value = actionTemplate.ID.ToString();
                                }

                                if (workflowTemplateElement.ElementType == (int)WorkflowTemplateElementType.Scoring && workflowTemplateElementParameter.Name == "SiteActivityScoreType")
                                {
                                    var value = workflowTemplateElementParameter.Value.ToGuid();
                                    var tScoreType = _dataContext.tbl_SiteActivityScoreType.FirstOrDefault(a => a.ID == value);
                                    if (tScoreType != null)
                                    {
                                        var scoreType = _dataContext.tbl_SiteActivityScoreType.FirstOrDefault(a => a.SiteID == siteId && a.Title == tScoreType.Title);
                                        if (scoreType != null)
                                            workflowTemplateElementParameter.Value = scoreType.ID.ToString();
                                        else
                                        {
                                            var newScoreType = new tbl_SiteActivityScoreType
                                                                   {
                                                                       ID = Guid.NewGuid(),
                                                                       SiteID = siteId,
                                                                       Title = tScoreType.Title
                                                                   };
                                            _dataContext.tbl_SiteActivityScoreType.AddObject(newScoreType);
                                            workflowTemplateElementParameter.Value = newScoreType.ID.ToString();
                                        }
                                    }
                                }

                                if (workflowTemplateElement.ElementType == (int)WorkflowTemplateElementType.ExternalRequest && workflowTemplateElementParameter.Name == "ExternalRequestURL")
                                {
                                    foreach (var material in materialsList)
                                    {
                                        if (workflowTemplateElementParameter.Value.Contains(material.OldValue))
                                            workflowTemplateElementParameter.Value = workflowTemplateElementParameter.Value.Replace(material.OldValue, material.Value);
                                    }
                                }

                                _dataContext.tbl_WorkflowTemplateElementParameter.AddObject(workflowTemplateElementParameter);
                            }
                        }
                        
                        // Events
                        var workflowTemplateElementEvents = _dataContext.tbl_WorkflowTemplateElementEvent.Where(a => a.WorkflowTemplateElementID == oldWorkflowTemplateElementId);
                        if (workflowTemplateElementEvents.Any())
                        {
                            foreach (var workflowTemplateElementEvent in workflowTemplateElementEvents)
                            {
                                _dataContext.Detach(workflowTemplateElementEvent);
                                var oldWorkflowTemplateElementEventId = workflowTemplateElementEvent.ID;
                                var newWorkflowTemplateElementEventId = Guid.NewGuid();
                                workflowTemplateElementEvent.ID = newWorkflowTemplateElementEventId;
                                workflowTemplateElementEvent.WorkflowTemplateElementID = newWorkflowTemplateElementId;
                                _dataContext.tbl_WorkflowTemplateElementEvent.AddObject(workflowTemplateElementEvent);

                                var workflowTemplateElementConditionEvents = _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.WorkflowTemplateElementEventID == oldWorkflowTemplateElementEventId);
                                if (workflowTemplateElementConditionEvents.Any())
                                {
                                    foreach (var workflowTemplateElementConditionEvent in workflowTemplateElementConditionEvents)
                                    {
                                        _dataContext.Detach(workflowTemplateElementConditionEvent);
                                        workflowTemplateElementConditionEvent.ID = Guid.NewGuid();
                                        workflowTemplateElementConditionEvent.WorkflowTemplateElementEventID = newWorkflowTemplateElementEventId;

                                        if ((ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.OpenForm ||
                                            (ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.FillForm ||
                                            (ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.CancelForm)
                                        {
                                            var rule = _dataContext.tbl_SiteActivityRules.FirstOrDefault(a => a.SiteID == parentSiteId && a.Code == workflowTemplateElementConditionEvent.Code);
                                            var m = materialsList.FirstOrDefault(a => a.OldValue == rule.ID.ToString());
                                            if (m != null)
                                            {
                                                var v = m.Value.ToGuid();
                                                var newRule = _dataContext.tbl_SiteActivityRules.FirstOrDefault(a => a.ID == v);
                                                workflowTemplateElementConditionEvent.Code = newRule.Code;
                                            }
                                        }

                                        if ((ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.DownloadFile)
                                        {
                                            var rule = _dataContext.tbl_Links.FirstOrDefault(a => a.SiteID == parentSiteId && a.Code == workflowTemplateElementConditionEvent.Code);
                                            var m = materialsList.FirstOrDefault(a => a.OldValue == rule.ID.ToString());
                                            if (m != null)
                                            {
                                                var v = m.Value.ToGuid();
                                                var newRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == v);
                                                workflowTemplateElementConditionEvent.Code = newRule.Code;
                                            }
                                        }

                                        if ((ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.ViewPage)
                                        {
                                            foreach (var material in materialsList)
                                            {
                                                if (workflowTemplateElementConditionEvent.Code.Contains(material.OldValue))
                                                    workflowTemplateElementConditionEvent.Code = workflowTemplateElementConditionEvent.Code.Replace(material.OldValue, material.Value);
                                            }
                                        }

                                        _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(workflowTemplateElementConditionEvent);
                                    }
                                }
                            }
                        }

                        // Results
                        var workflowTemplateElementResults = _dataContext.tbl_WorkflowTemplateElementResult.Where(a => a.WorkflowTemplateElementID == oldWorkflowTemplateElementId);
                        if (workflowTemplateElementResults.Any())
                        {
                            foreach (var workflowTemplateElementResult in workflowTemplateElementResults)
                            {
                                _dataContext.Detach(workflowTemplateElementResult);
                                var oldWorkflowTemplateElementResultId = workflowTemplateElementResult.ID;
                                workflowTemplateElementResult.ID = Guid.NewGuid();
                                workflowTemplateElementResult.WorkflowTemplateElementID = newWorkflowTemplateElementId;
                                _dataContext.tbl_WorkflowTemplateElementResult.AddObject(workflowTemplateElementResult);
                                resultsOldNew.Add(new { OldId = oldWorkflowTemplateElementResultId, NewId = workflowTemplateElementResult.ID });
                            }
                        }

                        // Periods
                        var workflowTemplateElementPeriods = _dataContext.tbl_WorkflowTemplateElementPeriod.Where(a => a.WorkflowTemplateElementID == oldWorkflowTemplateElementId);
                        if (workflowTemplateElementPeriods.Any())
                        {
                            foreach (var workflowTemplateElementPeriod in workflowTemplateElementPeriods)
                            {
                                _dataContext.Detach(workflowTemplateElementPeriod);
                                workflowTemplateElementPeriod.ID = Guid.NewGuid();
                                workflowTemplateElementPeriod.WorkflowTemplateElementID = newWorkflowTemplateElementId;
                                _dataContext.tbl_WorkflowTemplateElementPeriod.AddObject(workflowTemplateElementPeriod);
                            }
                        }


                        // ExternalRequests
                        var workflowTemplateElementExternalRequests = _dataContext.tbl_WorkflowTemplateElementExternalRequest.Where(a => a.WorkflowTemplateElementID == oldWorkflowTemplateElementId);
                        if (workflowTemplateElementExternalRequests.Any())
                        {
                            foreach (var workflowTemplateElementExternalRequest in workflowTemplateElementExternalRequests)
                            {
                                _dataContext.Detach(workflowTemplateElementExternalRequest);
                                workflowTemplateElementExternalRequest.ID = Guid.NewGuid();
                                workflowTemplateElementExternalRequest.WorkflowTemplateElementID = newWorkflowTemplateElementId;
                                _dataContext.tbl_WorkflowTemplateElementExternalRequest.AddObject(workflowTemplateElementExternalRequest);
                            }
                        }
                    }

                    // Relations
                    var workflowTemplateElementRelations = _dataContext.tbl_WorkflowTemplateElementRelation.Where(a => a.WorkflowTemplateID == workflowTemplateId);
                    if (workflowTemplateElementRelations.Any())
                    {
                        foreach (var workflowTemplateElementRelation in workflowTemplateElementRelations)
                        {
                            _dataContext.Detach(workflowTemplateElementRelation);
                            var oldworkflowTemplateElementRelationId = workflowTemplateElementRelation.ID;
                            workflowTemplateElementRelation.ID = Guid.NewGuid();
                            workflowTemplateElementRelation.WorkflowTemplateID = newWorkflowTemplate.ID;
                            workflowTemplateElementRelation.StartElementID = elementsOldNew.FirstOrDefault(a => a.OldId == workflowTemplateElementRelation.StartElementID).NewId;
                            workflowTemplateElementRelation.EndElementID = elementsOldNew.FirstOrDefault(a => a.OldId == workflowTemplateElementRelation.EndElementID).NewId;
                            var resultValue = Guid.Empty;
                            if (Guid.TryParse(workflowTemplateElementRelation.StartElementResult, out resultValue))
                                workflowTemplateElementRelation.StartElementResult = (resultsOldNew.FirstOrDefault(a => a.OldId == workflowTemplateElementRelation.StartElementResult.ToGuid()).NewId).ToString();

                            _dataContext.tbl_WorkflowTemplateElementRelation.AddObject(workflowTemplateElementRelation);
                            relationsOldNew.Add(new { OldId = oldworkflowTemplateElementRelationId, NewId = workflowTemplateElementRelation.ID });
                        }
                    }

                    // Events
                    foreach (var conditionEvent in conditionEventList)
                    {
                        var cEvent = new tbl_WorkflowTemplateConditionEvent
                                            {
                                                ID = Guid.NewGuid(),
                                                WorkflowTemplateID = newWorkflowTemplate.ID,
                                                Category = 0, // Действие
                                                ActivityType = conditionEvent.ActivityType,
                                                Code = conditionEvent.Code,
                                                ActualPeriod = conditionEvent.ActualPeriod
                                            };
                        _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(cEvent);
                    }

                    // Parameters
                    var workflowTemplateParameters = _dataContext.tbl_WorkflowTemplateParameter.Where(a => a.WorkflowTemplateID == workflowTemplateId);
                    if (workflowTemplateParameters.Any())
                    {
                        foreach (var workflowTemplateParameter in workflowTemplateParameters)
                        {
                            _dataContext.Detach(workflowTemplateParameter);
                            workflowTemplateParameter.ID = Guid.NewGuid();
                            workflowTemplateParameter.WorkflowTemplateID = newWorkflowTemplate.ID;
                            _dataContext.tbl_WorkflowTemplateParameter.AddObject(workflowTemplateParameter);
                        }
                    }

                    // Goals
                    var workflowTemplateGoals = _dataContext.tbl_WorkflowTemplateGoal.Where(o => o.WorkflowTemplateID == workflowTemplateId);
                    foreach (var workflowTemplateGoal in workflowTemplateGoals)
                    {
                        var workflowTemplateGoalElements = workflowTemplateGoal.tbl_WorkflowTemplateElement.ToList();
                        _dataContext.Detach(workflowTemplateGoal);
                        workflowTemplateGoal.ID = Guid.NewGuid();
                        workflowTemplateGoal.WorkflowTemplateID = newWorkflowTemplate.ID;

                        foreach (var workflowTemplateGoalElement in workflowTemplateGoalElements)
                        {
                            workflowTemplateGoal.tbl_WorkflowTemplateElement.Clear();
                            workflowTemplateGoal.tbl_WorkflowTemplateElement.Add(
                                _dataContext.tbl_WorkflowTemplateElement.SingleOrDefault(
                                    o => o.ID == workflowTemplateGoalElement.ID));
                        }

                        _dataContext.tbl_WorkflowTemplateGoal.AddObject(workflowTemplateGoal);
                    }

                    // Material
                    foreach (var material in materialsList)
                    {
                        var newMaterial = _dataContext.tbl_Material.FirstOrDefault(a => a.ID == material.Id);
                        _dataContext.Detach(newMaterial);
                        newMaterial.ID = Guid.NewGuid();
                        newMaterial.SiteID = siteId;
                        newMaterial.Value = material.Value;
                        newMaterial.WorkflowTemplateID = newWorkflowTemplate.ID;
                        _dataContext.tbl_Material.AddObject(newMaterial);
                    }

                    workflowTemplate.WorkflowXml = CopyWorkflowXml(workflowTemplate.WorkflowXml, elementsOldNew, resultsOldNew, relationsOldNew);

                    _dataContext.SaveChanges();
                }

                return newWorkflowTemplate;
            }

            return null;
        }

        public tbl_WorkflowTemplate UpdateWizard(Guid workflowTemplateId, Guid siteId, Guid contactId, List<WorkflowTemplateWizardRole> rolesList, int? condition, int? activityCount, List<WorkflowTemplateWizardConditionEvent> conditionEventList, List<WorkflowTemplateWizardTag> tagsList, List<tbl_SiteActionTemplate> actionTemplatesList, List<WorkflowTemplateWizardMaterial> materialsList)
        {
            var workflowTemplate = SelectById(workflowTemplateId);
            if (workflowTemplate != null)
            {
                workflowTemplate.Condition = condition;
                workflowTemplate.ActivityCount = activityCount;

                // Tags
                foreach (var tag in tagsList)
                {
                    var workflowTemplateElementTag =
                        _dataContext.tbl_WorkflowTemplateElementTag.FirstOrDefault(
                            a => a.WorkflowTemplateElementID == tag.Id);
                    if (workflowTemplateElementTag != null)
                    {
                        workflowTemplateElementTag.Operation = tag.Operation;
                        workflowTemplateElementTag.SiteTagID = tag.TagId;
                    }
                }

                // Events
                _dataContext.ExecuteStoreCommand("DELETE FROM tbl_WorkflowTemplateConditionEvent WHERE WorkflowTemplateID = @WorkflowTemplateID", new SqlParameter {ParameterName = "WorkflowTemplateID", Value = workflowTemplate.ID.ToString()});
                foreach (var conditionEvent in conditionEventList)
                {
                    var cEvent = new tbl_WorkflowTemplateConditionEvent
                                     {
                                         ID = Guid.NewGuid(),
                                         WorkflowTemplateID = workflowTemplate.ID,
                                         Category = 0,
                                         // Действие
                                         ActivityType = conditionEvent.ActivityType,
                                         Code = conditionEvent.Code,
                                         ActualPeriod = conditionEvent.ActualPeriod
                                     };
                    _dataContext.tbl_WorkflowTemplateConditionEvent.AddObject(cEvent);
                }

                // Action template
                foreach (var _actionTemplate in actionTemplatesList)
                {
                    var actionTemplate = _dataContext.tbl_SiteActionTemplate.FirstOrDefault(a => a.ID == _actionTemplate.ID);
                    actionTemplate.MessageCaption = _actionTemplate.MessageCaption;
                    actionTemplate.MessageBody = _actionTemplate.MessageBody;
                    actionTemplate.FromContactRoleID = _actionTemplate.FromContactRoleID;
                    actionTemplate.FromEmail = _actionTemplate.FromEmail;
                    actionTemplate.FromName = _actionTemplate.FromName;

                    var files = materialsList.Where(a => (MaterialType)a.Type == MaterialType.File);
                    foreach (var file in files)
                    {
                        if (file.OldValue != file.Value)
                        {
                            var oldSiteActivityRuleId = file.OldValue.ToGuid();
                            var oldSiteActivityRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == oldSiteActivityRuleId);
                            var siteActivityRuleId = file.Value.ToGuid();
                            var siteActivityRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == siteActivityRuleId);

                            actionTemplate.MessageBody = actionTemplate.MessageBody.Replace("#Link." + oldSiteActivityRule.Code + "#", "#Link." + siteActivityRule.Code + "#");
                        }
                    }

                    _dataContext.ExecuteStoreCommand("DELETE FROM tbl_SiteActionTemplateRecipient WHERE SiteActionTemplateID = @SiteActionTemplateID", new SqlParameter { ParameterName = "SiteActionTemplateID", Value = _actionTemplate.ID });
                    foreach (var siteActionTemplateRecipient in _actionTemplate.tbl_SiteActionTemplateRecipient)
                    {
                        var newSiteActionTemplateRecipient = new tbl_SiteActionTemplateRecipient();
                        newSiteActionTemplateRecipient.ID = Guid.NewGuid();
                        newSiteActionTemplateRecipient.SiteActionTemplateID = actionTemplate.ID;
                        newSiteActionTemplateRecipient.ContactRoleID = siteActionTemplateRecipient.ContactRoleID;
                        newSiteActionTemplateRecipient.ContactID = siteActionTemplateRecipient.ContactID;
                        newSiteActionTemplateRecipient.Email = siteActionTemplateRecipient.Email;
                        newSiteActionTemplateRecipient.DisplayName = siteActionTemplateRecipient.DisplayName;

                        _dataContext.tbl_SiteActionTemplateRecipient.AddObject(newSiteActionTemplateRecipient);
                    }
                }

                var workflowTemplateElements = _dataContext.tbl_WorkflowTemplateElement.Where(a => a.WorkflowTemplateID == workflowTemplateId);
                if (workflowTemplateElements.Any())
                {
                    foreach (var workflowTemplateElement in workflowTemplateElements)
                    {
                        if (workflowTemplateElement.ElementType == (int)WorkflowTemplateElementType.ExternalRequest)
                        {
                            var workflowTemplateElementParameter = _dataContext.tbl_WorkflowTemplateElementParameter.FirstOrDefault(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID && a.Name == "ExternalRequestURL");
                            foreach (var material in materialsList)
                            {
                                if (workflowTemplateElementParameter.Value.Contains(material.OldValue))
                                    workflowTemplateElementParameter.Value = workflowTemplateElementParameter.Value.Replace(material.OldValue, material.Value);
                            }
                        }

                        var workflowTemplateElementEvents = _dataContext.tbl_WorkflowTemplateElementEvent.Where(a => a.WorkflowTemplateElementID == workflowTemplateElement.ID);
                        if (workflowTemplateElementEvents.Any())
                        {
                            foreach (var workflowTemplateElementEvent in workflowTemplateElementEvents)
                            {
                                var workflowTemplateElementConditionEvents = _dataContext.tbl_WorkflowTemplateConditionEvent.Where(a => a.WorkflowTemplateElementEventID == workflowTemplateElementEvent.ID);
                                if (workflowTemplateElementConditionEvents.Any())
                                {
                                    foreach (var workflowTemplateElementConditionEvent in workflowTemplateElementConditionEvents)
                                    {
                                        if ((ActivityType) workflowTemplateElementConditionEvent.ActivityType == ActivityType.OpenForm ||
                                            (ActivityType) workflowTemplateElementConditionEvent.ActivityType == ActivityType.FillForm ||
                                            (ActivityType) workflowTemplateElementConditionEvent.ActivityType == ActivityType.CancelForm)
                                        {
                                            var rule = _dataContext.tbl_SiteActivityRules.FirstOrDefault(a => a.SiteID == siteId && a.Code == workflowTemplateElementConditionEvent.Code);
                                            var m = materialsList.FirstOrDefault(a => a.OldValue == rule.ID.ToString());
                                            if (m != null)
                                            {
                                                var v = m.Value.ToGuid();
                                                var newRule = _dataContext.tbl_SiteActivityRules.FirstOrDefault(a => a.ID == v);
                                                workflowTemplateElementConditionEvent.Code = newRule.Code;
                                            }
                                        }

                                        if ((ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.DownloadFile)
                                        {
                                            var rule = _dataContext.tbl_Links.FirstOrDefault(a => a.SiteID == siteId && a.Code == workflowTemplateElementConditionEvent.Code);
                                            var m = materialsList.FirstOrDefault(a => a.OldValue == rule.ID.ToString());
                                            if (m != null)
                                            {
                                                var v = m.Value.ToGuid();
                                                var newRule = _dataContext.tbl_Links.FirstOrDefault(a => a.ID == v);
                                                workflowTemplateElementConditionEvent.Code = newRule.Code;
                                            }
                                        }

                                        if ((ActivityType)workflowTemplateElementConditionEvent.ActivityType == ActivityType.ViewPage)
                                        {
                                            foreach (var material in materialsList)
                                            {
                                                if (workflowTemplateElementConditionEvent.Code.Contains(material.OldValue))
                                                    workflowTemplateElementConditionEvent.Code = workflowTemplateElementConditionEvent.Code.Replace(material.OldValue, material.Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                // Material
                foreach (var material in materialsList)
                {
                    var newMaterial = _dataContext.tbl_Material.FirstOrDefault(a => a.ID == material.Id);
                    newMaterial.Value = material.Value;
                }

                _dataContext.SaveChanges();

                return workflowTemplate;
            }

            return null;
        }


        /// <summary>
        /// Copies the workflow XML.
        /// </summary>
        /// <param name="oldXml">The old XML.</param>
        /// <param name="elements">The elements.</param>
        /// <param name="results">The results.</param>
        /// <param name="relations">The relations.</param>
        /// <returns></returns>
        public string CopyWorkflowXml(string oldXml, List<dynamic> elements, List<dynamic> results, List<dynamic> relations)
        {
            string newXml = oldXml;

            foreach (var element in elements)
                newXml = newXml.Replace((string)element.OldId.ToString(), (string)element.NewId.ToString());

            foreach (var result in results)
                newXml = newXml.Replace((string)result.OldId.ToString(), (string)result.NewId.ToString());

            foreach (var relation in relations)
                newXml = newXml.Replace((string)relation.OldId.ToString(), (string)relation.NewId.ToString());

            return newXml;
        }


        public void WorkflowInit(Guid contactId, Guid workflowTemplateId)
        {
            var workflowId = Guid.NewGuid();
            var contact = _dataContext.tbl_Contact.FirstOrDefault(a => a.ID == contactId);


            if (contact != null)
            {
                var workflow = new tbl_Workflow
                {
                    ID = workflowId,
                    SiteID = contact.SiteID,
                    WorkflowTemplateID = workflowTemplateId,
                    StartDate = DateTime.Now,
                    MassWorkflowID = null
                };
                _dataContext.tbl_Workflow.AddObject(workflow);

                var workflowTemplateParameterId = _dataContext.tbl_WorkflowTemplateParameter.FirstOrDefault(a => a.WorkflowTemplateID == workflowTemplateId && a.Name == "Контакт" && a.IsSystem).ID;
                var workflowParameter = new tbl_WorkflowParameter
                                            {
                                                ID = Guid.NewGuid(),
                                                WorkflowID = workflowId,
                                                WorkflowTemplateParameterID = workflowTemplateParameterId,
                                                Value = contactId.ToString()
                                            };
                _dataContext.tbl_WorkflowParameter.AddObject(workflowParameter);

                workflowTemplateParameterId = _dataContext.tbl_WorkflowTemplateParameter.FirstOrDefault(a => a.WorkflowTemplateID == workflowTemplateId && a.Name == "Компания" && a.IsSystem).ID;
                workflowParameter = new tbl_WorkflowParameter
                                        {
                                            ID = Guid.NewGuid(),
                                            WorkflowID = workflowId,
                                            WorkflowTemplateParameterID = workflowTemplateParameterId,
                                            Value = contact.CompanyID != null ? contact.CompanyID.ToString() : null
                                        };
                _dataContext.tbl_WorkflowParameter.AddObject(workflowParameter);

                workflowTemplateParameterId = _dataContext.tbl_WorkflowTemplateParameter.FirstOrDefault(a => a.WorkflowTemplateID == workflowTemplateId && a.Name == "Автор (роль)" && a.IsSystem).ID;
                workflowParameter = new tbl_WorkflowParameter
                                        {
                                            ID = Guid.NewGuid(),
                                            WorkflowID = workflowId,
                                            WorkflowTemplateParameterID = workflowTemplateParameterId,
                                            Value = null
                                        };
                _dataContext.tbl_WorkflowParameter.AddObject(workflowParameter);

                workflowTemplateParameterId = _dataContext.tbl_WorkflowTemplateParameter.FirstOrDefault(a => a.WorkflowTemplateID == workflowTemplateId && a.Name == "Ответственный по компании (роль)" && a.IsSystem).ID;
                var company = _dataContext.tbl_Company.FirstOrDefault(a => a.ID == contact.CompanyID);
                workflowParameter = new tbl_WorkflowParameter
                                        {
                                            ID = Guid.NewGuid(),
                                            WorkflowID = workflowId,
                                            WorkflowTemplateParameterID = workflowTemplateParameterId,
                                            Value = company != null ? company.OwnerID.ToString() : null
                                        };
                _dataContext.tbl_WorkflowParameter.AddObject(workflowParameter);


                var workflowElementId = Guid.NewGuid();
                var startElement = _dataContext.tbl_WorkflowTemplateElement.FirstOrDefault(a => a.WorkflowTemplateID == workflowTemplateId && a.ElementType == (int)WorkflowTemplateElementType.StartProcess);
                var workflowElement = new tbl_WorkflowElement
                                          {
                                              ID = workflowElementId,
                                              WorkflowID = workflowId,
                                              WorkflowTemplateElementID = startElement.ID,
                                              StartDate = DateTime.Now,
                                              EndDate = DateTime.Now,
                                              Status = (int)WorkflowElementStatus.Done
                                          };
                _dataContext.tbl_WorkflowElement.AddObject(workflowElement);

                _dataContext.SaveChanges();

                WorkflowProcessing.Processing(workflowElementId, null);
            }
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Delete(Guid siteId, Guid workflowTemplateId)
        {
            var workflowTemplate = SelectById(siteId, workflowTemplateId);
            workflowTemplate.DataBaseStatusID = (int) DataBaseStatus.Deleted;
            Update(workflowTemplate);
        }
    }
}