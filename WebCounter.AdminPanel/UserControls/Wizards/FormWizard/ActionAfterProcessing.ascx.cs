using System;
using System.Linq;
using System.Web.UI;
using WebCounter.AdminPanel.UserControls.Wizards.Controls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Wizards.FormWizard
{
    public partial class ActionAfterProcessing : FormWizardStep
    {
        protected tbl_SiteActivityRules siteActivityRule = null;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            dcbWorkflowTemplate.SiteID = CurrentUser.Instance.SiteID;

            if (IsEditMode)
                ucPopupSiteActionTemplate.ValidationGroup = "groupEdit";
        }

        
        /// <summary>
        /// Binds the data.
        /// </summary>
        public override void BindData()
        {
            base.BindData();                                    

            if (IsEditMode && EditObjectId.HasValue)
            {
                siteActivityRule = DataManager.SiteActivityRules.SelectById(EditObjectId.Value);
                if (siteActivityRule != null)
                {
                    txtUrl.Text = siteActivityRule.RepostURL;
                    if (siteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
                    {
                        plInviteFriendSettings.Visible = true;
                        var inviteFriendComponent = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.InviteFriend);
                        if (inviteFriendComponent != null && !string.IsNullOrEmpty(inviteFriendComponent.LayoutParams))
                        {
                            var lp = LayoutParams.Deserialize(inviteFriendComponent.LayoutParams);
                            if (!string.IsNullOrEmpty(lp.GetValue("WorkflowTemplateID")))
                            {
                                var workflowTemplate = DataManager.WorkflowTemplate.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(lp.GetValue("WorkflowTemplateID")));
                                if (workflowTemplate != null)
                                {
                                    dcbWorkflowTemplate.SelectedIdNullable = workflowTemplate.ID;
                                    dcbWorkflowTemplate.SelectedText = workflowTemplate.Name;
                                }

                                ucPopupSiteActionTemplate.UpdateUI(DataManager.SiteActionTemplate.SelectById(Guid.Parse(lp.GetValue("SiteActionTemplateID"))));
                                ucPopupSiteActionTemplate.SiteActionTemplateId = Guid.Parse(lp.GetValue("SiteActionTemplateID"));                                
                            }
                        }
                    }
                }
            }
            else
            {
                var templateSiteActivityRule = DataManager.SiteActivityRules.SelectById(CurrentForm);
                if (templateSiteActivityRule.tbl_SiteActivityRuleLayout.Any(o => o.LayoutType == (int)LayoutType.InviteFriend))
                {
                    plInviteFriendSettings.Visible = true;
                }
            }

            if (plInviteFriendSettings.Visible)
                ucPopupSiteActionTemplate.ForceNotPostbackPageLoad();
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (plInviteFriendSettings.Visible)
            {
                if (siteActivityRule != null)
                {
                    var inviteFriendComponent = siteActivityRule.tbl_SiteActivityRuleLayout.FirstOrDefault(o => o.LayoutType == (int)LayoutType.InviteFriend);
                    if (inviteFriendComponent != null && !string.IsNullOrEmpty(inviteFriendComponent.LayoutParams))
                    {
                        var lp = LayoutParams.Deserialize(inviteFriendComponent.LayoutParams);
                        if (!string.IsNullOrEmpty(lp.GetValue("SiteActionTemplateID")))
                            ucPopupSiteActionTemplate.UpdateUI(DataManager.SiteActionTemplate.SelectById(Guid.Parse(lp.GetValue("SiteActionTemplateID"))));
                    }
                }
                else
                {
                    var siteActionTemplate = DataManager.SiteActionTemplate.SelectById(ucPopupSiteActionTemplate.SiteActionTemplateId);
                    ucPopupSiteActionTemplate.UpdateUI(siteActionTemplate);
                }
            }
        }




        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void lbtnNext_OnClick(object sender, EventArgs e)
        {
            ((FormWizard)FindControlRecursive(Page, "ucFormWizard")).Save(CurrentForm);
            CurrentForm = Guid.Empty;
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control rc = FindControlRecursive(c, id);
                if (rc != null)
                    return rc;
            }
            return null;
        }
    }
}