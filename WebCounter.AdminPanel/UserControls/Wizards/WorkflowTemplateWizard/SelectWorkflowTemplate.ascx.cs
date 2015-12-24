using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard
{
    public partial class SelectWorkflowTemplate : WorkflowTemplateWizardStep
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();

            var forms =
                DataManager.WorkflowTemplate.SelectAllTemplates().Where(a => a.DataBaseStatusID == (int)DataBaseStatus.Active).Select(
                o => new { o.ID, Title = string.Format("{0} {1}", o.Name, !string.IsNullOrEmpty(o.Description) ? string.Format("<br/><i>{0}</i>", o.Description) : string.Empty) });

            rblWorkflowTemplates.DataSource = forms;
            rblWorkflowTemplates.DataTextField = "Title";
            rblWorkflowTemplates.DataValueField = "ID";
            rblWorkflowTemplates.DataBind();

            foreach (ListItem listItem in rblWorkflowTemplates.Items)
            {
                listItem.Attributes.Add("onclick", "if (rblWorkflowTemplatesOnClientSelectedIndexChanging() == false) return false;");
            }
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rblWorkflowTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rblWorkflowTemplates_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageViewsCount > 1)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "WizardNotification", "ClearTabs();", true);
                return;
            }

            Session["CurrentDockStates"] = null;
            Session["DocksConditions"] = null;
            Session["Materials"] = null;
            Session["SiteActionTemplates"] = null;

            NextStep();

            base.lbtnNext_OnClick(sender, e);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void lbtnNext_OnClick(object sender, EventArgs e)
        {
            NextStep();
            base.lbtnNext_OnClick(sender, e);
        }



        /// <summary>
        /// Nexts the step.
        /// </summary>
        protected void NextStep()
        {
            CurrentWorkflowTemplate = Guid.Parse(rblWorkflowTemplates.SelectedValue);

            var workflowTemplate = DataManager.WorkflowTemplate.SelectById(CurrentWorkflowTemplate);

            if (workflowTemplate.AutomaticMethod == (int)WorkflowTemplateAutomaticMethod.ActivityContact)
            {
                var tab = TabStrip.Tabs.FindTabByValue("RunCondition");
                if (tab == null)
                {
                    tab = new RadTab("Условия запуска") { Enabled = false, Value = "RunCondition" };
                    TabStrip.Tabs.Insert(2, tab);
                }
            }
            else
            {
                var tab = TabStrip.Tabs.FindTabByValue("RunCondition");
                if (tab != null)
                {
                    if (MultiPage.PageViews.Count > tab.Index)
                        MultiPage.PageViews.Remove(MultiPage.PageViews[tab.Index]);
                    TabStrip.Tabs.Remove(tab);
                }
            }

            var workflowTemplateElements = DataManager.WorkflowTemplateElement.SelectAll(CurrentWorkflowTemplate);
            if (workflowTemplateElements.Count(a => a.ElementType == (int)WorkflowTemplateElementType.Tag) > 0)
            {
                var tab = TabStrip.Tabs.FindTabByValue("Qualification");
                if (tab == null)
                {
                    tab = new RadTab("Квалификация") { Enabled = false, Value = "Qualification" };
                    TabStrip.Tabs.Insert(3, tab);
                }
            }
            else
            {
                var tab = TabStrip.Tabs.FindTabByValue("Qualification");
                if (tab != null)
                {
                    if (MultiPage.PageViews.Count > tab.Index)
                        MultiPage.PageViews.Remove(MultiPage.PageViews[tab.Index]);
                    TabStrip.Tabs.Remove(tab);
                }
            }
        }
    }
}