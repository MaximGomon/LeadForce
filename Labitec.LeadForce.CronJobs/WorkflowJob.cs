using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Interfaces;

namespace Labitec.LeadForce.CronJobs
{
    public class WorkflowJob : ICronJob
    {
        public void Run()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
                {
                    connection.Open();
                    var command = new SqlCommand("WorkflowScheduler", connection)
                                      {CommandType = CommandType.StoredProcedure, CommandTimeout = 120};
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Workflow Scheduler ERROR: ", ex);
            }
        }
    }
}