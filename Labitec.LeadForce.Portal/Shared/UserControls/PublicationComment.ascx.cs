using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class PublicationComment : System.Web.UI.UserControl
    {
        protected int SumLike = 0;
        protected int CommentsCount = 0;
        protected string ContactLikeUserText = string.Empty;
        protected int ContactLike = 0;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid PublicationId
        {
            get
            {
                object o = ViewState["PublicationId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set { ViewState["PublicationId"] = value; }
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
        private void BindData()
        {
            var dataManager = new DataManager();
            var publicationMap =
                dataManager.Publication.SelectPublicationMapById(((LeadForcePortalBasePage) Page).SiteId, PublicationId,
                                                                 CurrentUser.Instance == null ? null : CurrentUser.Instance.ContactID);

            if (publicationMap != null)
            {
                CommentsCount = publicationMap.CommentsCount;
                SumLike = publicationMap.SumLike ?? 0;
                ContactLikeUserText = publicationMap.ContactLikeUserText;
                ContactLike = publicationMap.ContactLike ?? 0;
            }
        }
    }
}