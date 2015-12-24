using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Analytics
{
    public partial class AnalyticReportAxisValue : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        public event ReportAxisValueChangedEventHandler ReportAxisValueChanged;
        public delegate void ReportAxisValueChangedEventHandler(object sender);

        /// <summary>
        /// Gets or sets the analytic report id.
        /// </summary>
        /// <value>
        /// The analytic report id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? AnalyticReportId
        {
            get { return (Guid?)ViewState["AnalyticReportId"]; }
            set { ViewState["AnalyticReportId"] = value; }
        }



        /// <summary>
        /// Gets or sets the value to build chart.
        /// </summary>
        /// <value>
        /// The value to build chart.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ValueToBuildChart
        {
            get { return (Guid?)ViewState["AxisValue"]; }
            set { ViewState["AxisValue"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (!AnalyticReportId.HasValue)
                return;

            plContainer.Controls.Clear();

            var analyticValues = DataManager.AnalyticReportSystem.SelectAxisValuesByAnalyticReportId((Guid)AnalyticReportId);

            if (!analyticValues.Any() || analyticValues.Count() == 1)
            {
                Visible = false;
                return;
            }
            
            Visible = true;

            
            var label = new Label() { Text = "Значение для построения: " };

            var dropDownList = new DropDownList()
                                    {
                                        CssClass = "select-text",
                                        AutoPostBack = true,
                                        ID = "ddlValuesToBuild"
                                    };

            dropDownList.SelectedIndexChanged += dropDownList_SelectedIndexChanged;

            foreach (var analyticValue in analyticValues)
            {
                var item = new ListItem(analyticValue.tbl_AnalyticAxis.Title, analyticValue.AnalyticAxisID.ToString());

                if (analyticValue.IsDefault)
                    item.Selected = true;
                
                dropDownList.Items.Add(item);
            }

            plContainer.Controls.Add(label);
            plContainer.Controls.Add(dropDownList);
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dropDownList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueToBuildChart = Guid.Parse(((DropDownList) sender).SelectedValue);
            if (ReportAxisValueChanged != null)
                ReportAxisValueChanged(this);
        }
    }
}