using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.Handlers
{
    public partial class MassMailSelectContacts : LeadForceBasePage
    {        
        List<tbl_Contact> _contacts = new List<tbl_Contact>();
        private Guid _massMailId;



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            
            _massMailId = Guid.Parse(Request.QueryString["massmailid"]);

            if (!Page.IsPostBack)
            {
                ddlReadyToSell.DataSource = DataManager.ReadyToSell.SelectAll(SiteId);
                ddlReadyToSell.DataTextField = "Title";
                ddlReadyToSell.DataValueField = "ID";
                ddlReadyToSell.DataBind();
                ddlReadyToSell.Items.Insert(0, new ListItem("", ""));

                ddlPriorities.DataSource = DataManager.Priorities.SelectAll(SiteId).OrderByDescending(a => a.Title);
                ddlPriorities.DataTextField = "Title";
                ddlPriorities.DataValueField = "ID";
                ddlPriorities.DataBind();
                ddlPriorities.Items.Insert(0, new ListItem("", ""));

                ddlStatusFilter.DataSource = DataManager.Status.SelectAll(SiteId);
                ddlStatusFilter.DataTextField = "Title";
                ddlStatusFilter.DataValueField = "ID";
                ddlStatusFilter.DataBind();
                ddlStatusFilter.Items.Insert(0, new ListItem("", ""));

                BindGrid();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            bool sortAscending = this.SortDirection == SortDirection.Ascending ? true : false;
            var contacts = new List<tbl_Contact>();

            var massMailContacts = new List<Guid>();
            massMailContacts = DataManager.MassMailContact.SelectByMassMailId(SiteId, _massMailId).Select(a => a.ContactID).ToList();

            contacts = DataManager.Contact.SelectAll(SiteId).Where(a => !massMailContacts.Contains(a.ID)).ToList();

            if (ddlReadyToSell.SelectedValue != "")
                contacts = contacts.Where(a => a.ReadyToSellID == Guid.Parse(ddlReadyToSell.SelectedValue)).ToList();
            if (ddlPriorities.SelectedValue != "")
                contacts = contacts.Where(a => a.PriorityID == Guid.Parse(ddlPriorities.SelectedValue)).ToList();
            if (ddlStatusFilter.SelectedValue != "")
                contacts = contacts.Where(a => a.StatusID == Guid.Parse(ddlStatusFilter.SelectedValue)).ToList();

            //if (ucTags.TagIDs != null && ucTags.TagIDs.Count > 0)
            //{
            //    var siteTagObjectIds = DataManager.SiteTagObjects.SelectIdsByTagID(ucTags.TagIDs);
            //    contacts = contacts.Where(a => siteTagObjectIds.Contains(a.ID)).ToList();
            //}

            switch (this.SortExpression)
            {
                case "UserFullName":
                    contacts = sortAscending ? contacts.OrderBy(u => u.UserFullName).ToList() : contacts.OrderByDescending(u => u.UserFullName).ToList();
                    break;
                case "Email":
                    contacts = sortAscending ? contacts.OrderBy(u => u.Email).ToList() : contacts.OrderByDescending(u => u.Email).ToList();
                    break;
                case "tbl_ReadyToSell.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_ReadyToSell.Title).ToList() : contacts.OrderByDescending(u => u.tbl_ReadyToSell.Title).ToList();
                    break;
                case "tbl_Priorities.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_Priorities.Title).ToList() : contacts.OrderByDescending(u => u.tbl_Priorities.Title).ToList();
                    break;
                case "tbl_Status.Title":
                    contacts = sortAscending ? contacts.OrderBy(u => u.tbl_Status.Title).ToList() : contacts.OrderByDescending(u => u.tbl_Status.Title).ToList();
                    break;
            }

            _contacts = contacts;
        }


        private void BindGrid()
        {
            BindData();

            gvContacts.DataSource = _contacts;
            gvContacts.DataBind();

            upContacts.Update();
        }



        /// <summary>
        /// Handles the PageIndexChanging event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvContacts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContacts.PageIndex = e.NewPageIndex;
            BindGrid();
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlReadyToSell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlReadyToSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlPriorities control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlPriorities_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlStatusFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }



        /// <summary>
        /// Handles the Sorting event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gvContacts_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.SortExpression == e.SortExpression)
            {
                this.SortDirection = this.SortDirection == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                this.SortDirection = SortDirection.Ascending;
            }
            this.SortExpression = e.SortExpression;
            gvContacts.EditIndex = -1;
            gvContacts.SelectedIndex = -1;
        }



        /// <summary>
        /// Handles the Sorted event of the gvSiteUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gvContacts_Sorted(object sender, EventArgs e)
        {
            BindGrid();
        }



        /// <summary>
        /// Handles the Selected event of the ucTags control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ucTags_Selected(object sender, EventArgs e)
        {
            BindGrid();
        }



        /// <summary>
        /// Handles the Click event of the BtnInclude control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnInclude_Click(object sender, EventArgs e)
        {
            BindData();

            //Session.Add("MassMailSiteUsers", _siteUsers);
            var massMailContact = new tbl_MassMailContact();
            foreach (var contact in _contacts)
            {
                massMailContact = new tbl_MassMailContact();
                massMailContact.SiteID = SiteId;
                massMailContact.MassMailID = _massMailId;
                massMailContact.ContactID = contact.ID;
                DataManager.MassMailContact.Add(massMailContact);
            }

            ClientScript.RegisterStartupScript(this.GetType(), "doReturnToParent", "returnToParent();", true);
        }



        protected string SortExpression
        {
            get
            {
                return ViewState["SortExpression"] as string;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }



        protected SortDirection SortDirection
        {
            get
            {
                object o = ViewState["SortDirection"];
                if (o == null)
                    return SortDirection.Ascending;
                else
                    return (SortDirection)o;
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }
    }
}