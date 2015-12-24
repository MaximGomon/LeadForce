using System;
using System.ComponentModel;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;


namespace Labitec.LeadForce.Portal.Shared.UserControls
{
    public partial class ContentComments : System.Web.UI.UserControl
    {
        protected int CommentsCount = 0;
        protected string OfficialComment = string.Empty;


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ContentId
        {
            get
            {
                if (ViewState["ContentId"] == null)
                    ViewState["ContentId"] = Guid.Empty;

                return (Guid)ViewState["ContentId"];
            }
            set { ViewState["ContentId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public CommentTables CommentType
        {
            get { return (CommentTables)ViewState["CommentType"]; }
            set { ViewState["CommentType"] = value; }
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
            CommentsCount = ContentCommentRepository.GetCommentsCount(CurrentUser.Instance.SiteID, ContentId, CommentType, true);
            OfficialComment = ContentCommentRepository.SelectOfficialComment(CurrentUser.Instance.SiteID, ContentId, CommentType);
        }
    }
}