using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Report
{
    using System;

    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RequirementReport.
    /// </summary>
    public partial class RequirementReport : Telerik.Reporting.Report
    {
        protected DataManager dataManager = new DataManager();
        private Guid _siteId = Guid.Empty;
        private Guid? _companyId = null;
        private DateTime _startDate = DateTime.Now.AddDays(-100);
        private DateTime _endDate = DateTime.Now;
        

        public RequirementReport(Guid siteId, Guid? companyId, DateTime startDate, DateTime endDate)
        {            
            InitializeComponent();

            _siteId = siteId;
            _companyId = companyId;
            _startDate = startDate;
            _endDate = endDate;

            txtPeriod.Value = string.Format("{0}-{1}", startDate.ToString("dd.MM.yyyy"), endDate.ToString("dd.MM.yyyy"));
        }



        /// <summary>
        /// Handles the NeedDatasource event of the ReportsByCompanyList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReportsByCompanyList_NeedDatasource(object sender, EventArgs e)
        {
            var requirements = dataManager.Requirement.SelectByPeriod(_siteId, _startDate, _endDate);
            var companies = new List<tbl_Company>();
            if (!_companyId.HasValue)
                companies = requirements.Select(o => o.tbl_Company).Distinct().OrderBy(o => o.Name).ToList();
            else
                companies = requirements.Where(o => o.CompanyID == _companyId.Value).Select(o => o.tbl_Company).Distinct().OrderBy(o => o.Name).ToList();

            var requirementsStruct = new List<ReportRequirementsStruct>();

            foreach (tbl_Company company in companies)
            {
                if (company == null)
                    continue;

                var requirementsInWork = requirements.Where(o => !o.tbl_RequirementStatus.IsLast && o.CompanyID == company.ID);
                var requirementsCompleted = requirements.Where(o => o.tbl_RequirementStatus.IsLast && o.CompanyID == company.ID);

                if (requirementsInWork.Any() || requirementsCompleted.Any())
                {
                    requirementsStruct.Add(new ReportRequirementsStruct()
                                               {
                                                   Company = company,
                                                   RequirementsInWork = requirementsInWork.ToList(),
                                                   RequirementsCompleted = requirementsCompleted.ToList()
                                               });
                }
            }
            
            if (!_companyId.HasValue)
            {
                txtForCompanyLabel.Visible = false;
            }
            else if (companies.Count == 1)
            {
                txtCompany.Value = companies[0].Name;
            }

            (sender as Telerik.Reporting.Processing.Table).DataSource = requirementsStruct;
        }



        /// <summary>
        /// Handles the NeedDataSource event of the tblRequirementsInWorks control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tblRequirementsInWorks_NeedDataSource(object sender, EventArgs e)
        {
            var reportRequirementsStruct = (ReportRequirementsStruct)((sender as Telerik.Reporting.Processing.Table).Parent.DataObject).RawData;
            (sender as Telerik.Reporting.Processing.Table).DataSource = reportRequirementsStruct.RequirementsInWork;

            if (!reportRequirementsStruct.RequirementsInWork.Any())
            {
                (sender as Telerik.Reporting.Processing.Table).Parent.ChildElements.SingleOrDefault(
                    o => o.Name == "txtLabelInWork").Visible = false;
                (sender as Telerik.Reporting.Processing.Table).Visible = false;                
            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the tblRequirementsCompleted control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tblRequirementsCompleted_NeedDataSource(object sender, EventArgs e)
        {
            var reportRequirementsStruct = (ReportRequirementsStruct)((sender as Telerik.Reporting.Processing.Table).Parent.DataObject).RawData;
            (sender as Telerik.Reporting.Processing.Table).DataSource = reportRequirementsStruct.RequirementsCompleted;

            if (!reportRequirementsStruct.RequirementsCompleted.Any())
            {
                (sender as Telerik.Reporting.Processing.Table).Parent.ChildElements.SingleOrDefault(
                    o => o.Name == "txtLabelCompleted").Visible = false;
                (sender as Telerik.Reporting.Processing.Table).Visible = false;                
            }
        }
    }

    public class ReportRequirementsStruct
    {
        public tbl_Company Company { get; set; }
        public List<tbl_Requirement> RequirementsInWork { get; set; }
        public List<tbl_Requirement> RequirementsCompleted { get; set; } 
    }
}