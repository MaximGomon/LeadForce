using System;
using System.ComponentModel;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon
{
    public partial class PublicationsRibbon : System.Web.UI.UserControl
    {
        private readonly DataManager _dataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationTypeId
        {
            get
            {
                object o = ViewState["PublicationTypeId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["PublicationTypeId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationCategoryId
        {
            get
            {
                object o = ViewState["PublicationCategoryId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["PublicationCategoryId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Filter
        {
            get
            {
                return (string)ViewState["Filter"];
            }
            set
            {
                ViewState["Filter"] = value;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rlvcbePublications.DataBindingSettings.LocationUrl = ResolveUrl("~/Handlers/ActivityRibbon.aspx");
            ucPublicationFilter.FilterChanged += ucPublicationFilter_FilterChanged;
        }



        /// <summary>
        /// Handles the FilterChanged event of the ucPublicationFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebCounter.AdminPanel.UserControls.ActivityRibbon.PublicationFilter.FilterChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationFilter_FilterChanged(object sender, PublicationFilter.FilterChangedEventArgs e)
        {
            Filter = e.Filter;
            BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            hfPublicationFilter.Value = Filter;
            hfPublicationType.Value = PublicationTypeId.ToString();
            hfPublicationCategory.Value = PublicationCategoryId.ToString();
            rlvPublications.DataSource = _dataManager.Publication.SelectActivityRibbon(((LeadForcePortalBasePage)Page).SiteId,
                                                                                       CurrentUser.Instance != null && CurrentUser.Instance.ContactID.HasValue 
                                                                                       ? (Guid)CurrentUser.Instance.ContactID : Guid.Empty,
                                                                                       PublicationTypeId,
                                                                                       PublicationCategoryId, Filter, 0, ((LeadForcePortalBasePage)Page).PortalSettingsId);
            rlvPublications.DataBind();
        }
    }
}