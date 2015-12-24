using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Report
{
    public class RequirementReportHelper
    {
        public static string GetComment(Guid requirementId)
        {
            var dataManager = new DataManager();
            return dataManager.RequirementComment.SelectCommentForReport(requirementId);
        }
    }
}
