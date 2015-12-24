using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Requirement
{
    public partial class AssignToRequirements : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        public event RequirementsAssignedEventHandler RequirementsAssigned;
        public delegate void RequirementsAssignedEventHandler(object sender);

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<Guid> SelectedRequirments
        {
            get
            {
                if (ViewState["SelectedRequirments"] == null)
                    ViewState["SelectedRequirments"] = new List<Guid>();
                return (List<Guid>)ViewState["SelectedRequirments"];
            }
            private set
            {
                ViewState["SelectedRequirments"] = value;
            }
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
            if (isClear)
                SelectedRequirments.Clear();
                        
            var requirements = DataManager.Requirement.SelectAll(CurrentUser.Instance.SiteID).OrderBy(o => o.CreatedAt);
            if (CompanyId.HasValue)
                requirements = requirements.Where(o => o.CompanyID == CompanyId).OrderBy(o => o.CreatedAt);

            var request = DataManager.Request.SelectById(CurrentUser.Instance.SiteID, RequestId);            

            if (request != null && SelectedRequirments.Count == 0)
            {
                SelectedRequirments = request.tbl_Requirement.Select(o => o.ID).ToList();             
            }

            if (SelectedRequirments.Count > 0)
            {
                if (request != null && SelectedStatuses.Count == 0)
                    SelectedStatuses = request.tbl_Requirement.Select(o => o.tbl_RequirementStatus.ID).Distinct().ToList();

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
            e.Node.Checked = SelectedRequirments.Contains(requirement.ID);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAssign control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnAssign_OnClick(object sender, EventArgs e)
        {
            SelectedRequirments = rtvRequirements.CheckedNodes.Select(checkedNode => Guid.Parse(checkedNode.Value)).ToList();

            if (RequestId != Guid.Empty)
            {
                var request = DataManager.Request.SelectById(CurrentUser.Instance.SiteID, RequestId);

                if (request != null)
                {
                    request.tbl_Requirement.Clear();
                    var requirements = DataManager.Requirement.SelectAll(CurrentUser.Instance.SiteID).Where(
                            o => SelectedRequirments.Contains(o.ID)).ToList();
                    foreach (var requirement in requirements)
                        request.tbl_Requirement.Add(requirement);

                    DataManager.Request.Update(request);

                    if (RequirementsAssigned != null)
                        RequirementsAssigned(this);
                }
            }            

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseAssignToRequirement"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseAssignToRequirement", "CloseAssignToRequirementRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCancel_OnClick(object sender, EventArgs e)
        {
            //BindStatuses();
            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseAssignToRequirement"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseAssignToRequirement", "CloseAssignToRequirementRadWindow();", true);
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