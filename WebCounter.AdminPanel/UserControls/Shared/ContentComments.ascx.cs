using System;
using System.Linq;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class ContentComments : System.Web.UI.UserControl
    {
        protected int CommentsCount = 0;
        protected string OfficialComment = string.Empty;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool EnableHtmlCommentEditor
        {
            get
            {
                if (ViewState["EnableHtmlCommentEditor"] == null)
                    ViewState["EnableHtmlCommentEditor"] = false;

                return (bool)ViewState["EnableHtmlCommentEditor"];
            }
            set { ViewState["EnableHtmlCommentEditor"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ContentId
        {
            get
            {
                if (ViewState["ContentId"] == null)
                    ViewState["ContentId"] = Guid.Empty;

                return (Guid) ViewState["ContentId"];
            }
            set { ViewState["ContentId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ExpandComments
        {
            get
            {
                if (ViewState["ExpandComments"] == null)
                    ViewState["ExpandComments"] = false;

                return (bool)ViewState["ExpandComments"];
            }
            set { ViewState["ExpandComments"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public CommentTables CommentType
        {
            get { return (CommentTables) ViewState["CommentType"]; }
            set { ViewState["CommentType"] = value; }
        }

        public RadEditor HtmlEditor
        {
            get { return ucHtmlEditor.Editor; }
        }

        public TextBox TextBoxEditor
        {
            get { return textEditor; }
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
            CommentsCount = ContentCommentRepository.GetCommentsCount(CurrentUser.Instance.SiteID, ContentId, CommentType);
            OfficialComment = ContentCommentRepository.SelectOfficialComment(CurrentUser.Instance.SiteID, ContentId,CommentType);

            tbl_ServiceLevelClient serviceLevelClient = null;

            var dataManager = new DataManager();

            switch (CommentType)
            {
                case CommentTables.tbl_RequirementComment:                    
                    var requirement = dataManager.Requirement.SelectById(CurrentUser.Instance.SiteID, ContentId);
                    if (requirement != null)
                    {
                        serviceLevelClient =
                            requirement.tbl_ServiceLevel.tbl_ServiceLevelClient.FirstOrDefault(
                                o => o.ClientID == requirement.CompanyID);                        
                    }
                    break;
                case CommentTables.tbl_RequestComment:                    
                    var request = dataManager.Request.SelectById(CurrentUser.Instance.SiteID, ContentId);
                    if (request != null)
                    {
                        serviceLevelClient =
                            request.tbl_ServiceLevel.tbl_ServiceLevelClient.FirstOrDefault(
                                o => o.ClientID == request.CompanyID);                        
                    }
                    break;
            }

            if (serviceLevelClient != null)
            {
                rprDesitnations.DataSource = serviceLevelClient.tbl_ServiceLevelContact.Where(
                    o => o.tbl_Contact.tbl_User.FirstOrDefault() != null).Select(
                        o =>
                        new
                        {
                            UserID = o.tbl_Contact.tbl_User.FirstOrDefault().ID,
                            o.tbl_Contact.UserFullName
                        });
                rprDesitnations.DataBind();
            }
        }
    }
}