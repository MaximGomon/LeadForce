using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementTagRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementTagRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementTagRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementTag> SelectAll(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElementTag.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateElementTagId">The workflow template element tag id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementTag SelectById(Guid workflowTemplateElementTagId)
        {
            return _dataContext.tbl_WorkflowTemplateElementTag.SingleOrDefault(a => a.ID == workflowTemplateElementTagId);
        }



        /// <summary>
        /// Adds the specified workflow template element tag.
        /// </summary>
        /// <param name="workflowTemplateElementTag">The workflow template element tag.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementTag Add(tbl_WorkflowTemplateElementTag workflowTemplateElementTag)
        {
            if (workflowTemplateElementTag.ID == Guid.Empty)
                workflowTemplateElementTag.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementTag.AddObject(workflowTemplateElementTag);
            _dataContext.SaveChanges();

            return workflowTemplateElementTag;
        }



        /// <summary>
        /// Updates the specified workflow template element tag.
        /// </summary>
        /// <param name="workflowTemplateElementTag">The workflow template element tag.</param>
        public void Update(tbl_WorkflowTemplateElementTag workflowTemplateElementTag)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element tag list.
        /// </summary>
        /// <param name="workflowTemplateElementTagList">The workflow template element tag list.</param>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        public void Save(List<WorkflowTemplateElementTagMap> workflowTemplateElementTagList, Guid workflowTemplateElementId)
        {
            var existsElementTags = SelectAll(workflowTemplateElementId).ToList();

            foreach (var elementTag in workflowTemplateElementTagList)
            {
                var existElementTag = existsElementTags.SingleOrDefault(a => a.ID == elementTag.ID);

                if (existElementTag == null)
                {
                    _dataContext.tbl_WorkflowTemplateElementTag.AddObject(new tbl_WorkflowTemplateElementTag
                                                                             {
                                                                                 ID = elementTag.ID,
                                                                                 WorkflowTemplateElementID = workflowTemplateElementId,
                                                                                 SiteTagID = elementTag.SiteTagID,
                                                                                 Operation = elementTag.Operation
                                                                             });
                }
                else
                {
                    existElementTag.ID = elementTag.ID;
                    existElementTag.WorkflowTemplateElementID = workflowTemplateElementId;
                    existElementTag.SiteTagID = elementTag.SiteTagID;
                    existElementTag.Operation = elementTag.Operation;
                }
            }

            foreach (var existsElementTag in existsElementTags)
            {
                if (workflowTemplateElementTagList.SingleOrDefault(op => op.ID == existsElementTag.ID) == null)
                    _dataContext.tbl_WorkflowTemplateElementTag.DeleteObject(existsElementTag);
            }

            _dataContext.SaveChanges();
        }
    }
}