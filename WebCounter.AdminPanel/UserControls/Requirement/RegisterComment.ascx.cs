using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Requirement
{
    public partial class RegisterComment : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? SelectedRequirementId
        {
            get
            {
                var guid = (Guid?)ViewState["SelectedRequirementId"];
                if (guid == Guid.Empty)
                    ViewState["SelectedRequirementId"] = null;

                return (Guid?)ViewState["SelectedRequirementId"];
            }
            set { ViewState["SelectedRequirementId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Comment
        {
            get { return (string) ViewState["Comment"]; }
            set { ViewState["Comment"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected List<Guid> SelectedStatuses
        {
            get
            {
                if (ViewState["SelectedStatuses"] == null)
                    ViewState["SelectedStatuses"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedStatuses"];
            }
            private set
            {
                ViewState["SelectedStatuses"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CompanyId
        {
            get
            {
                return (Guid?)ViewState["CompanyId"];
            }
            set
            {
                ViewState["CompanyId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid RequestId
        {
            get
            {
                if (ViewState["RequestId"] == null)
                    ViewState["RequestId"] = Guid.Empty;
                return (Guid)ViewState["RequestId"];
            }
            set
            {
                ViewState["RequestId"] = value;
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
            {
                BindStatuses();
                BindData();
            }
        }



        /// <summary>
        /// Binds the statuses.
        /// </summary>
        private void BindStatuses()
        {
            SelectedStatuses.Clear();
            chxlistRequirementStatuses.Items.Clear();
            var requirementStatuses = DataManager.RequirementStatus.SelectAll(CurrentUser.Instance.SiteID);
            foreach (var requirementStatus in requirementStatuses)
            {
                chxlistRequirementStatuses.Items.Add(new ListItem
                {
                    Text = requirementStatus.Title,
                    Value = requirementStatus.ID.ToString(),
                    Selected = !requirementStatus.IsLast
                });

            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData(bool isClear = false)
        {
            var requirements = DataManager.Requirement.SelectAll(CurrentUser.Instance.SiteID).OrderBy(o => o.CreatedAt);
            
            var requirement = new tbl_Requirement();
            if (SelectedRequirementId.HasValue)            
                requirement = requirements.SingleOrDefault(o => o.ID == SelectedRequirementId);                            

            if (CompanyId.HasValue)
                requirements = requirements.Where(o => o.CompanyID == CompanyId).OrderBy(o => o.CreatedAt);

            if (SelectedRequirementId.HasValue)
            {
                if (requirement != null && SelectedStatuses.Count == 0)
                    SelectedStatuses = new List<Guid>() { requirement.RequirementStatusID };

                foreach (ListItem item in chxlistRequirementStatuses.Items)
                    item.Selected = SelectedStatuses.Contains(Guid.Parse(item.Value));
            }

            if (SelectedStatuses.Count > 0)
                requirements = requirements.Where(o => SelectedStatuses.Contains(o.RequirementStatusID)).OrderBy(o => o.CreatedAt);

            rtvRequirements.DataSource = requirements;
            rtvRequirements.DataValueField = "ID";
            rtvRequirements.DataFieldID = "ID";
            rtvRequirements.DataFieldParentID = "ParentID";
            rtvRequirements.DataTextField = "Number";
            rtvRequirements.DataBind();
        }



        /// <summary>
        /// Handles the OnNodeDataBound event of the rtvRequirements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvRequirements_OnNodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            var requirement = (tbl_Requirement)e.Node.DataItem;
            e.Node.Text = string.Format("Требование №{0} от {1} ({2})", requirement.Number, requirement.CreatedAt.ToString("dd.MM.yyyy"), requirement.ShortDescription);
            e.Node.Checked = SelectedRequirementId == requirement.ID;
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAssign control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdate_OnClick(object sender, EventArgs e)
        {
            SelectedRequirementId = rtvRequirements.CheckedNodes.Select(checkedNode => Guid.Parse(checkedNode.Value)).FirstOrDefault();
            Comment = ucComment.Content;

            if (RequestId != Guid.Empty)
            {
                var request = DataManager.Request.SelectById(CurrentUser.Instance.SiteID, RequestId);
                SaveComment(request);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseRegisterCommentRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseRegisterCommentRadWindow", "CloseRegisterCommentRadWindow();", true);
        }


        /// <summary>
        /// Saves the comment.
        /// </summary>
        /// <param name="request">The request.</param>
        public void SaveComment(tbl_Request request)
        {
            if (SelectedRequirementId.HasValue && !string.IsNullOrEmpty(Comment) && request.ContactID.HasValue)
            {
                var user = DataManager.User.SelectByContactIdExtended(CurrentUser.Instance.SiteID, (Guid)request.ContactID) ??
                               DataManager.User.AddPortalUser(CurrentUser.Instance.SiteID, (Guid)request.ContactID);
                if (user != null)
                {
                    var requirement = DataManager.Requirement.SelectById(CurrentUser.Instance.SiteID, (Guid)SelectedRequirementId);
                    if (!request.tbl_Requirement.Any(o => o.ID == requirement.ID))
                    {
                        request.tbl_Requirement.Add(requirement);
                        DataManager.Request.Update(request);
                    }

                    ContentCommentRepository.Add(CurrentUser.Instance.SiteID, user.ID, (Guid)SelectedRequirementId,
                                                 Comment, null, null, string.Empty,
                                                 CommentTables.tbl_RequirementComment);
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCancel_OnClick(object sender, EventArgs e)
        {            
            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseRegisterCommentRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseRegisterCommentRadWindow", "CloseRegisterCommentRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the chxlistRequirementStatuses control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void chxlistRequirementStatuses_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedStatuses.Clear();
            SelectedStatuses.AddRange(from ListItem item in chxlistRequirementStatuses.Items where item.Selected select Guid.Parse(item.Value));
            BindData(true);
        }
    }
}