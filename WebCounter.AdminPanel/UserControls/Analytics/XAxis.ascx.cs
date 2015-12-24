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
    public partial class XAxis : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        public event XAxisChangedEventHandler XAxisChanged;
        public delegate void XAxisChangedEventHandler(object sender);

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
        /// Gets or sets the X axis to build chart.
        /// </summary>
        /// <value>
        /// The X axis to build chart.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? XAxisToBuildChart
        {
            get { return (Guid?)ViewState["XAxisToBuildChart"]; }
            set { ViewState["XAxisToBuildChart"] = value; }
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

            var analyticValues = DataManager.AnalyticReportSystem.SelectXAxisByAnalyticReportId((Guid)AnalyticReportId);

            if (!analyticValues.Any() || analyticValues.Count() == 1)
            {
                Visible = false;
                return;
            }

            Visible = true;

            var label = new Label() { Text = "Ось X: " };

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
            XAxisToBuildChart = Guid.Parse(((DropDownList)sender).SelectedValue);
            if (XAxisChanged != null)
                XAxisChanged(this);
        }
    }
}