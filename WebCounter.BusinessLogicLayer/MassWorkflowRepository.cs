using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MassWorkflowRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWorkflowRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MassWorkflowRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="massWorkflowId">The mass workflow id.</param>
        /// <returns></returns>
        public tbl_MassWorkflow SelectById(Guid siteId, Guid massWorkflowId)
        {
            return _dataContext.tbl_MassWorkflow.SingleOrDefault(a => a.SiteID == siteId && a.ID == massWorkflowId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_MassWorkflow> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_MassWorkflow.Where(a => a.SiteID == siteId);
        }



        /// <summary>
        /// Adds the specified mass workflow.
        /// </summary>
        /// <param name="massWorkflow">The mass workflow.</param>
        /// <returns></returns>
        public tbl_MassWorkflow Add(tbl_MassWorkflow massWorkflow)
        {
            if (massWorkflow.ID == Guid.Empty)
                massWorkflow.ID = Guid.NewGuid();
            _dataContext.tbl_MassWorkflow.AddObject(massWorkflow);
            _dataContext.SaveChanges();

            return massWorkflow;
        }



        /// <summary>
        /// Updates the specified mass workflow.
        /// </summary>
        /// <param name="massWorkflow">The mass workflow.</param>
        public void Update(tbl_MassWorkflow massWorkflow)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Workflows the init.
        /// </summary>
        /// <param name="massWorkflowId">The mass workflow id.</param>
        public void MassWorkflowInit(Guid massWorkflowId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var command = new SqlCommand("MassWorkflowInit", connection) { CommandType = CommandType.StoredProcedure };

                command.Parameters.AddWithValue("@MassWorkflowID", massWorkflowId);

                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Masses the workflow cancel.
        /// </summary>
        /// <param name="massWorkflowId">The mass workflow id.</param>
        public void MassWorkflowCancel(Guid massWorkflowId)
        {
            var workflows = _dataContext.tbl_Workflow.Where(a => a.MassWorkflowID == massWorkflowId);
            foreach (var workflow in workflows)
                workflow.Status = (int)WorkflowStatus.Cancelled;
            _dataContext.SaveChanges();
        }




        /// <summary>
        /// Deletes the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="id">The id.</param>
        public void DeleteById(Guid siteId, Guid id)
        {
            _dataContext.tbl_MassWorkflow.DeleteObject(_dataContext.tbl_MassWorkflow.SingleOrDefault(o => o.SiteID == siteId && o.ID == id));
            _dataContext.SaveChanges();
        }
    }
}