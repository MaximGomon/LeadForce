using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class SelectFormType : FormWizardStep
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
                DataManager.SiteActivityRules.SelectAllTemplates().Where(o => o.RuleTypeID ==(int)RuleType.Form).Select(
                o => new {o.ID, Title = string.Format("{0} {1}", o.Name, !string.IsNullOrEmpty(o.Description) ? string.Format("<br/><i>{0}</i>", o.Description) : string.Empty)});

            rblForms.DataSource = forms;
            rblForms.DataTextField = "Title";
            rblForms.DataValueField = "ID";
            rblForms.DataBind();

            foreach (ListItem listItem in rblForms.Items)
            {
                listItem.Attributes.Add("onclick", "if (rblFormsOnClientSelectedIndexChanging() == false) return false;");
            }            
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rblForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rblForms_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageViewsCount > 1)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "WizardNotification", "ClearTabs();", true);                
                return;
            }            

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
            CurrentForm = Guid.Parse(rblForms.SelectedValue);

            var siteActivityRule = DataManager.SiteActivityRules.SelectById(CurrentForm);

            if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
            {
                var tab = TabStrip.Tabs.FindTabByValue("LogicProcessing");
                if (tab != null)
                {
                    if (MultiPage.PageViews.Count > tab.Index)
                        MultiPage.PageViews.Remove(MultiPage.PageViews[tab.Index]);
                    TabStrip.Tabs.Remove(tab);
                }
            }
            else
            {
                var tab = TabStrip.Tabs.FindTabByValue("LogicProcessing");
                if (tab == null)
                {
                    tab = new RadTab("Логика обработки") { Enabled = false, Value = "LogicProcessing" };
                    TabStrip.Tabs.Insert(3, tab);
                }
            }
        }        
    }
}

