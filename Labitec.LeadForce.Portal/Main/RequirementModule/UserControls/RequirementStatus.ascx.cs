using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Main.RequirementModule.UserControls
{
    public partial class RequirementStatus : System.Web.UI.UserControl
    {
        protected DataManager DataManager = null;

        public event RequirementStatusChangedEventHandler RequirementStatusChanged;
        public delegate void RequirementStatusChangedEventHandler(object sender, RequirementStatusChangedEventArgs e);
        public class RequirementStatusChangedEventArgs : EventArgs
        {
            public Guid? ResponsibleId { get; set; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid RequirementId
        {
            get
            {
                if (ViewState["RequirementId"] == null)
                    ViewState["RequirementId"] = Guid.Empty;
                return (Guid)ViewState["RequirementId"];
            }
            set
            {
                ViewState["RequirementId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ServiceLevelId
        {
            get
            {
                if (ViewState["ServiceLevelId"] == null)
                    ViewState["ServiceLevelId"] = Guid.Empty;
                return (Guid)ViewState["ServiceLevelId"];
            }
            set
            {
                ViewState["ServiceLevelId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get { return (Guid?)ViewState["CompanyId"]; }
            set { ViewState["CompanyId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid RequirementStatusId
        {
            get
            {
                if (ViewState["RequirementStatusId"] == null)
                    ViewState["RequirementStatusId"] = Guid.Empty;
                return (Guid)ViewState["RequirementStatusId"];
            }
            set
            {
                ViewState["RequirementStatusId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid RequirementTypeId
        {
            get
            {
                if (ViewState["RequirementTypeId"] == null)
                    ViewState["RequirementTypeId"] = Guid.Empty;
                return (Guid)ViewState["RequirementTypeId"];
            }
            set
            {
                ViewState["RequirementTypeId"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnSave, lrlRequirementStatus, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnSave, rlvRequirementTransitions, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnSave, rbtnClientReview, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rlvRequirementTransitions, lbtnSave, null, UpdatePanelRenderMode.Inline);

            DataManager = new DataManager();

            dcbRequirementImplementationComplete.SiteID = CurrentUser.Instance.SiteID;
            dcbRequirementSatisfaction.SiteID = CurrentUser.Instance.SiteID;
            dcbRequirementSpeedTime.SiteID = CurrentUser.Instance.SiteID;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            var requirement = DataManager.Requirement.SelectById(CurrentUser.Instance.SiteID, RequirementId);
            if (requirement != null)
            {
                dcbRequirementSpeedTime.SelectedIdNullable = requirement.RequirementSpeedTimeID;
                dcbRequirementSatisfaction.SelectedIdNullable = requirement.RequirementSatisfactionID;
                dcbRequirementImplementationComplete.SelectedIdNullable = requirement.RequirementImplementationCompleteID;
                txtEstimationComment.Text = requirement.EstimationComment;
                if (!string.IsNullOrEmpty(requirement.EstimationComment))
                    rbtnClientReview.Visible = true;
            }

            var requirementStatus = DataManager.RequirementStatus.SelectById(CurrentUser.Instance.SiteID, RequirementStatusId);

            if (requirementStatus != null)
                lrlRequirementStatus.Text = requirementStatus.Title;
            else
            {
                var requirementDefaultStatus = DataManager.RequirementStatus.SelectDefault(CurrentUser.Instance.SiteID);
                if (requirementDefaultStatus != null)
                {
                    lrlRequirementStatus.Text = requirementDefaultStatus.Title;
                    RequirementStatusId = requirementDefaultStatus.ID;
                }
            }

            var requirementTransitions =
                DataManager.RequirementTransition.SelectAll(CurrentUser.Instance.SiteID).Where(
                    rt =>
                    rt.IsActive && rt.InitialRequirementStatusID == RequirementStatusId &&
                    (rt.AllowedAccessProfileID == null ||
                    (rt.AllowedAccessProfileID != null && rt.AllowedAccessProfileID == CurrentUser.Instance.AccessProfileID)) && rt.IsPortalAllowed);

            if (RequirementTypeId != Guid.Empty)
                requirementTransitions = requirementTransitions.Where(rt => rt.RequirementTypeID == null || rt.RequirementTypeID != null && rt.RequirementTypeID == RequirementTypeId);

            rlvRequirementTransitions.DataSource = requirementTransitions;
            rlvRequirementTransitions.DataBind();
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnRequirementTransition control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnRequirementTransition_OnClick(object sender, EventArgs e)
        {            
            var radButton = (RadButton)sender;

            var requirementTransitionId = Guid.Parse(radButton.CommandArgument);
            var requirementTransition = DataManager.RequirementTransition.SelectById(CurrentUser.Instance.SiteID, requirementTransitionId);
            if (requirementTransition.IsReviewRequired)
            {
                var requirement = DataManager.Requirement.SelectById(CurrentUser.Instance.SiteID, RequirementId);
                if (requirement != null && string.IsNullOrEmpty(requirement.EstimationComment))
                {
                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowClientReviewRadWindow"))
                        ScriptManager.RegisterStartupScript(Page, typeof (Page), "ShowClientReviewRadWindow",
                                                            "ShowClientReviewRadWindow();", true);

                    lbtnSave.CommandArgument = requirementTransition.FinalRequirementStatusID.ToString();
                    return;
                }
            }

            RequirementStatusId = requirementTransition.FinalRequirementStatusID;

            ProceedStatus();

            BindData();
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            var requirement = DataManager.Requirement.SelectById(CurrentUser.Instance.SiteID, RequirementId);
            if (requirement != null)
            {
                ((LeadForcePortalBasePage)Page).CheckWriteAccess(requirement.OwnerID, "Requirements");

                requirement.RequirementImplementationCompleteID = dcbRequirementImplementationComplete.SelectedIdNullable;
                requirement.RequirementSpeedTimeID = dcbRequirementSpeedTime.SelectedIdNullable;
                requirement.RequirementSatisfactionID = dcbRequirementSatisfaction.SelectedIdNullable;
                requirement.EstimationComment = txtEstimationComment.Text;

                DataManager.Requirement.Update(requirement);

                rbtnClientReview.Visible = true;

                if (!string.IsNullOrEmpty(lbtnSave.CommandArgument))
                {
                    var statusId = Guid.Parse(lbtnSave.CommandArgument);
                    RequirementStatusId = statusId;
                    BindData();
                    lbtnSave.CommandArgument = string.Empty;

                    ProceedStatus();
                }

                if (!Page.ClientScript.IsStartupScriptRegistered("CloseClientReviewRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseClientReviewRadWindow",
                                                        "CloseClientReviewRadWindow();", true);
            }
        }



        /// <summary>
        /// Proceeds the status.
        /// </summary>
        private void ProceedStatus()
        {
            if (RequirementStatusChanged != null)
            {
                var requirementStatus = DataManager.RequirementStatus.SelectById(CurrentUser.Instance.SiteID, RequirementStatusId);
                var serviceLevel = DataManager.ServiceLevel.SelectById(CurrentUser.Instance.SiteID, ServiceLevelId);

                var eventArgs = new RequirementStatusChangedEventArgs();

                if (requirementStatus != null && serviceLevel != null)
                {
                    var responsibleId = DataManager.RequirementStatus.SelectResponsibleId(requirementStatus, serviceLevel, CompanyId);
                    eventArgs.ResponsibleId = responsibleId;
                }

                RequirementStatusChanged(this, eventArgs);
            }
        }
    }
}