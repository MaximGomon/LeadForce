using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class WorkflowTemplateParameterRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowTemplateParameterRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public WorkflowTemplateParameterRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }


        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_WorkflowTemplateParameter> SelectAll(Guid workflowTemplateId)
        {
            return _dataContext.tbl_WorkflowTemplateParameter.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="workflowTemplateParameterId">The workflow template parameter id.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateParameter SelectById(Guid workflowTemplateParameterId)
        {
            return _dataContext.tbl_WorkflowTemplateParameter.SingleOrDefault(a => a.ID == workflowTemplateParameterId);
        }



        /// <summary>
        /// Adds the specified workflow template parameter.
        /// </summary>
        /// <param name="workflowTemplateParameter">The workflow template parameter.</param>
        /// <returns></returns>
        public tbl_WorkflowTemplateParameter Add(tbl_WorkflowTemplateParameter workflowTemplateParameter)
        {
            if (workflowTemplateParameter.ID == Guid.Empty)
                workflowTemplateParameter.ID = Guid.NewGuid();
            _dataContext.tbl_WorkflowTemplateParameter.AddObject(workflowTemplateParameter);
            _dataContext.SaveChanges();

            return workflowTemplateParameter;
        }



        /// <summary>
        /// Updates the specified workflow template parameter.
        /// </summary>
        /// <param name="workflowTemplateParameter">The workflow template parameter.</param>
        public void Update(tbl_WorkflowTemplateParameter workflowTemplateParameter)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Saves the specified workflow template parameter list.
        /// </summary>
        /// <param name="workflowTemplateParameterList">The workflow template parameter list.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(List<WorkflowTemplateParameterMap> workflowTemplateParameterList, Guid workflowTemplateId)
        {
            //var existsParameters = SelectAll(workflowTemplateId).Where(a => a.Name != "tbl_Contact.ID").ToList(); // !!!
            var existsParameters = SelectAll(workflowTemplateId).ToList();

            foreach (var parameter in workflowTemplateParameterList)
            {
                var existParameter = existsParameters.Where(a => a.ID == parameter.ID).SingleOrDefault();

                if (existParameter == null)
                {
                    _dataContext.tbl_WorkflowTemplateParameter.AddObject(new tbl_WorkflowTemplateParameter
                                                                             {
                                                                                 ID = parameter.ID,
                                                                                 WorkflowTemplateID = workflowTemplateId,
                                                                                 Name = parameter.Name,
                                                                                 ModuleID = parameter.ModuleID,
                                                                                 IsSystem = parameter.IsSystem,
                                                                                 Description = parameter.Description
                                                                             });
                }
                else
                {
                    existParameter.ID = parameter.ID;
                    existParameter.WorkflowTemplateID = workflowTemplateId;
                    existParameter.Name = parameter.Name;
                    existParameter.ModuleID = parameter.ModuleID;
                    existParameter.IsSystem = parameter.IsSystem;
                    existParameter.Description = parameter.Description;
                }
            }

            foreach (var existsParameter in existsParameters)
            {
                if (workflowTemplateParameterList.Where(op => op.ID == existsParameter.ID).SingleOrDefault() == null)
                    _dataContext.tbl_WorkflowTemplateParameter.DeleteObject(existsParameter);
            }

            _dataContext.SaveChanges();
        }
    }
}