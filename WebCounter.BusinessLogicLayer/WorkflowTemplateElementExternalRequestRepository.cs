using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateElementExternalRequestRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateElementExternalRequestRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateElementExternalRequestRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateElementExternalRequest> SelectAll(Guid workflowTemplateElementId)
        {
            return _dataContext.tbl_WorkflowTemplateElementExternalRequest.Where(a => a.WorkflowTemplateElementID == workflowTemplateElementId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateElemenExternalRequestId">The workflow template elemen external request id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementExternalRequest SelectById(Guid workflowTemplateElemenExternalRequestId)
        {
            return _dataContext.tbl_WorkflowTemplateElementExternalRequest.SingleOrDefault(a => a.ID == workflowTemplateElemenExternalRequestId);
        }



        /// <summary>
        /// Adds the specified workflow template element external request.
        /// </summary>
        /// <param name="workflowTemplateElementExternalRequest">The workflow template element external request.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateElementExternalRequest Add(tbl_WorkflowTemplateElementExternalRequest workflowTemplateElementExternalRequest)
        {
            if (workflowTemplateElementExternalRequest.ID == Guid.Empty)
                workflowTemplateElementExternalRequest.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateElementExternalRequest.AddObject(workflowTemplateElementExternalRequest);
            _dataContext.SaveChanges();

            return workflowTemplateElementExternalRequest;
        }



        /// <summary>
        /// Updates the specified workflow template element external request.
        /// </summary>
        /// <param name="workflowTemplateElementExternalRequest">The workflow template element external request.</param>
        public void Update(tbl_WorkflowTemplateElementExternalRequest workflowTemplateElementExternalRequest)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template element external request list.
        /// </summary>
        /// <param name="workflowTemplateElementExternalRequestList">The workflow template element external request list.</param>
        /// <param name="workflowTemplateElementId">The workflow template element id.</param>
        public void Save(List<WorkflowTemplateElementExternalRequestMap> workflowTemplateElementExternalRequestList, Guid workflowTemplateElementId)
        {
            var existsElementExternalRequests = SelectAll(workflowTemplateElementId).ToList();

            foreach (var elementExternalRequest in workflowTemplateElementExternalRequestList)
            {
                var existElementExternalRequest = existsElementExternalRequests.SingleOrDefault(a => a.ID == elementExternalRequest.ID);

                if (existElementExternalRequest == null)
                {
                    _dataContext.tbl_WorkflowTemplateElementExternalRequest.AddObject(new tbl_WorkflowTemplateElementExternalRequest
                                                                             {
                                                                                 ID = elementExternalRequest.ID,
                                                                                 WorkflowTemplateElementID = workflowTemplateElementId,
                                                                                 Name = elementExternalRequest.Name,
                                                                                 Value = elementExternalRequest.Value
                                                                             });
                }
                else
                {
                    existElementExternalRequest.ID = elementExternalRequest.ID;
                    existElementExternalRequest.WorkflowTemplateElementID = workflowTemplateElementId;
                    existElementExternalRequest.Name = elementExternalRequest.Name;
                    existElementExternalRequest.Value = elementExternalRequest.Value;
                }
            }

            foreach (var existsElementExternalRequest in existsElementExternalRequests)
            {
                if (workflowTemplateElementExternalRequestList.SingleOrDefault(op => op.ID == existsElementExternalRequest.ID) == null)
                    _dataContext.tbl_WorkflowTemplateElementExternalRequest.DeleteObject(existsElementExternalRequest);
            }

            _dataContext.SaveChanges();
        }
    }
}