using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateGoalRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateGoalRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateGoalRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateGoalId">The workflow template goal id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateGoal SelectById(Guid workflowTemplateGoalId)
        {
            return _dataContext.tbl_WorkflowTemplateGoal.SingleOrDefault(o => o.ID == workflowTemplateGoalId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateGoal> SelectAll(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateGoal.Where(o => o.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Adds the specified workflow template goal.
        /// </summary>
        /// <param name="workflowTemplateGoal">The workflow template goal.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateGoal Add(tbl_WorkflowTemplateGoal workflowTemplateGoal)
        {
            if (workflowTemplateGoal.ID == Guid.Empty)
                workflowTemplateGoal.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateGoal.AddObject(workflowTemplateGoal);
            _dataContext.SaveChanges();

            return workflowTemplateGoal;
        }



        /// <summary>
        /// Updates the specified workflow template goal.
        /// </summary>
        /// <param name="workflowTemplateGoal">The workflow template goal.</param>
        public void Update(tbl_WorkflowTemplate workflowTemplateGoal)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified to update workflow template goals.
        /// </summary>
        /// <param name="toUpdateWorkflowTemplateGoals">To update workflow template goals.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Update(List<WorkflowTemplateGoalMap> toUpdateWorkflowTemplateGoals, Guid workflowTemplateId)
        {
            var existsWorkflowTemplateGoals = SelectAll(workflowTemplateId).ToList();

            foreach (var updateWorkflowTempalteGoal in toUpdateWorkflowTemplateGoals)
            {
                var existWorkflowTemplateGoal = existsWorkflowTemplateGoals.SingleOrDefault(op => op.ID == updateWorkflowTempalteGoal.ID);

                if (existWorkflowTemplateGoal == null)
                {
                    var newWorkflowTemplateGoal = new tbl_WorkflowTemplateGoal()
                        {
                            ID = updateWorkflowTempalteGoal.ID,
                            WorkflowTemplateID = workflowTemplateId,
                            Title = updateWorkflowTempalteGoal.Title,
                            Description = updateWorkflowTempalteGoal.Description
                        };

                    foreach (var elementMap in updateWorkflowTempalteGoal.Elements)
                    {
                        var element = _dataContext.tbl_WorkflowTemplateElement.SingleOrDefault(o => o.ID == elementMap.ID);
                        if (element != null)
                        {
                            newWorkflowTemplateGoal.tbl_WorkflowTemplateElement.Add(element);
                        }
                    }

                    _dataContext.tbl_WorkflowTemplateGoal.AddObject(newWorkflowTemplateGoal);


                }
                else
                {
                    existWorkflowTemplateGoal.Title = updateWorkflowTempalteGoal.Title;
                    existWorkflowTemplateGoal.Description = updateWorkflowTempalteGoal.Description;
                    existWorkflowTemplateGoal.tbl_WorkflowTemplateElement.Clear();
                    foreach (var elementMap in updateWorkflowTempalteGoal.Elements)
                    {
                        var element = _dataContext.tbl_WorkflowTemplateElement.SingleOrDefault(o => o.ID == elementMap.ID);
                        if (element != null)
                            existWorkflowTemplateGoal.tbl_WorkflowTemplateElement.Add(element);
                    }
                }
            }

            foreach (var existsWorkflowTemplateGoal in existsWorkflowTemplateGoals)
            {
                if (toUpdateWorkflowTemplateGoals.SingleOrDefault(op => op.ID == existsWorkflowTemplateGoal.ID) == null)
                {
                    existsWorkflowTemplateGoal.tbl_WorkflowTemplateElement.Clear();
                    _dataContext.tbl_WorkflowTemplateGoal.DeleteObject(existsWorkflowTemplateGoal);
                }
            }

            _dataContext.SaveChanges();
        }
    }
}