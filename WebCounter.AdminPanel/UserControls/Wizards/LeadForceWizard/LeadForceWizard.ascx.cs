using System;
using System.Collections;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;

namespace WebCounter.AdminPanel.UserControls.Wizards.LeadForceWizard
{
    public partial class LeadForceWizard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                AddTab("Шаг 1", "SelectProductEdition", true);
                var pageView = new RadPageView { ID = "SelectProductEdition" };
                rmpWizard.PageViews.Add(pageView);

                AddTab("Шаг 2", "SelectSiteTemplate", false);
                AddTab("Шаг 3", "SelectDictionary", false);
                AddTab("Шаг 4", "SelectWorkflowTemplate", false);
                AddTab("Шаг 5", "SelectSiteActionTemplate", false);
                AddTab("Шаг 6", "SelectSourceMonitoring", false);
                AddTab("Шаг 7", "SelectAccessProfile", false);                
            }
        }



        /// <summary>
        /// Adds the tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <param name="value">The value.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void AddTab(string tabName, string value, bool enabled)
        {
            var tab = new RadTab(tabName) { Enabled = enabled, Value = value };
            rtsWizard.Tabs.Add(tab);
        }



        /// <summary>
        /// Clears the pageviews.
        /// </summary>
        public void ClearPageviews()
        {
            if (rmpWizard.PageViews.Count > 1)
            {
                for (int i = 1; i < rtsWizard.Tabs.Count; i++)
                {
                    var pageView = rmpWizard.FindPageViewByID(rtsWizard.Tabs[i].Value);
                    rtsWizard.Tabs[i].Enabled = false;
                    if (pageView != null)
                        rmpWizard.PageViews.Remove(pageView);
                }
                
                ((BaseWizardStep)FindControl(rmpWizard.PageViews[0].ID + "UserControl")).BindData();
            }
        }



        /// <summary>
        /// Handles the OnPageViewCreated event of the rmpWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadMultiPageEventArgs"/> instance containing the event data.</param>
        protected void rmpWizard_OnPageViewCreated(object sender, RadMultiPageEventArgs e)
        {
            var pageViewContents = (BaseWizardStep)LoadControl("~/UserControls/Wizards/LeadForceWizard/" + e.PageView.ID + ".ascx");
            pageViewContents.BindData();
            pageViewContents.ID = e.PageView.ID + "UserControl";
            e.PageView.Controls.Add(pageViewContents);
        }



        /// <summary>
        /// Handles the OnAjaxRequest event of the rapLeadForceWizard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void rapLeadForceWizard_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Clear")
                ClearPageviews();
        }
    }
}