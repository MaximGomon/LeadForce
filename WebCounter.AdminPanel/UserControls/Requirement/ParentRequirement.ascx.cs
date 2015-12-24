using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Requirement
{
    public partial class ParentRequirement : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ParentRequirementId
        {
            get
            {
                var guid = (Guid?) ViewState["ParentRequirementId"];
                if (guid == Guid.Empty)
                    ViewState["ParentRequirementId"] = null;

                return (Guid?) ViewState["ParentRequirementId"];
            }
            set { ViewState["ParentRequirementId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? CurrentRequirementId
        {
            get { return (Guid?)ViewState["CurrentRequirementId"]; }
            set { ViewState["CurrentRequirementId"] = value; }
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

        

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnUpdate, lrlParentRequirment, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnCancel, lrlParentRequirment, null, UpdatePanelRenderMode.Inline);

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
        public void BindData()
        {                      
            var requirements = DataManager.Requirement.SelectAll(CurrentUser.Instance.SiteID).OrderBy(o => o.CreatedAt);

            if (CurrentRequirementId.HasValue)
                requirements = requirements.Where(o => o.ID != CurrentRequirementId).OrderBy(o => o.CreatedAt);


            var requirement = new tbl_Requirement();
            if (ParentRequirementId.HasValue)
            {                
                requirement = requirements.SingleOrDefault(o => o.ID == ParentRequirementId);
                if (requirement != null)
                    lrlParentRequirment.Text = string.Format("Требование №{0} от {1} ({2})", requirement.Number,
                                                             requirement.CreatedAt.ToString("dd.MM.yyyy"),
                                                             requirement.ShortDescription);
                else
                    lrlParentRequirment.Text = string.Empty;
            }

            if (CompanyId.HasValue)
                requirements = requirements.Where(o => o.CompanyID == CompanyId).OrderBy(o => o.CreatedAt);

            if (ParentRequirementId.HasValue)
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
            e.Node.Checked = ParentRequirementId == requirement.ID;
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnAssign control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdate_OnClick(object sender, EventArgs e)
        {
            ParentRequirementId = rtvRequirements.CheckedNodes.Select(checkedNode => Guid.Parse(checkedNode.Value)).FirstOrDefault();

            BindData();

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseParentRequirementRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseParentRequirementRadWindow", "CloseParentRequirementRadWindow();", true);
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

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseParentRequirementRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseParentRequirementRadWindow", "CloseParentRequirementRadWindow();", true);
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
            BindData();
        }
    }
}