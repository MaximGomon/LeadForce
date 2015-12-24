using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class WorkflowProcessing
    {
        /// <summary>
        /// Processings the specified workflow element id.
        /// </summary>
        /// <param name="workflowElementId">The workflow element id.</param>
        /// <param name="result">The result.</param>
        public static void Processing(Guid workflowElementId, string result)
        {
            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["ADONETConnectionString"]))
            {
                connection.Open();
                var command = new SqlCommand("WorkflowProcessing", connection) { CommandType = CommandType.StoredProcedure };

                command.Parameters.AddWithValue("@_WorkflowElementID", workflowElementId);
                command.Parameters.AddWithValue("@_StartElementResult", result);

                command.ExecuteNonQuery();
            }
        }



        /// <summary>
        /// Workflows the element by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid WorkflowElementByValue(Guid value)
        {
            var dataManager = new DataManager();

            var workflowElement = dataManager.WorkflowElement.SelectByValue(value.ToString());
            if (workflowElement != null)
                return workflowElement.ID;

            return Guid.Empty;
        }



        /// <summary>
        /// Gets the workflow id by element id.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid GetWorkflowIdByValue(Guid value)
        {
            var dataManager = new DataManager();

            var workflowElement = dataManager.WorkflowElement.SelectByValue(value.ToString());

            if (workflowElement != null)
                return workflowElement.tbl_Workflow.ID;

            return Guid.Empty;
        }



        /// <summary>
        /// Determines whether the specified value is workflow.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is workflow; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWorkflow(Guid value)
        {
            var dataManager = new DataManager();
            var workflowElement = dataManager.WorkflowElement.SelectByValue(value.ToString());
            return workflowElement != null;
        }



        /// <summary>
        /// Workflows the element results by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static List<tbl_WorkflowTemplateElementResult> WorkflowElementResultsByValue(Guid value)
        {
            var dataManager = new DataManager();
            var workflowElement = dataManager.WorkflowElement.SelectByValue(value.ToString());

            return dataManager.WorkflowTemplateElementResult.SelectAll(workflowElement.WorkflowTemplateElementID).ToList();
        }



        /// <summary>
        /// Workflows the element result.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static tbl_WorkflowTemplateElementResult WorkflowElementResult(Guid value, string name)
        {
            var dataManager = new DataManager();
            var workflowElement = dataManager.WorkflowElement.SelectByValue(value.ToString());

            return dataManager.WorkflowTemplateElementResult.SelectAll(workflowElement.WorkflowTemplateElementID).FirstOrDefault(a => a.Name == name && a.IsSystem);
        }
    }
}