using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Requirement
{
    public partial class RequirementStatus : System.Web.UI.UserControl
    {
        public event RequirementStatusChangedEventHandler RequirementStatusChanged;
        public delegate void RequirementStatusChangedEventHandler(object sender, RequirementStatusChangedEventArgs e);
        public class RequirementStatusChangedEventArgs : EventArgs
        {
            public Guid? ResponsibleId { get; set; }            
        }


        protected DataManager DataManager = new DataManager();

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
            get { return (Guid?) ViewState["CompanyId"]; }
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
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {            
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
                    (rt.AllowedAccessProfileID != null && rt.AllowedAccessProfileID == CurrentUser.Instance.AccessProfileID)));

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
            var radButton = (RadButton) sender;
            RequirementStatusId = Guid.Parse(radButton.CommandArgument);
            BindData();

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