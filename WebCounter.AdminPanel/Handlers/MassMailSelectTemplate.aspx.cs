using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.Handlers
{
    public partial class MassMailSelectTemplate : LeadForceBasePage
    {        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
                BindGrid();
        }


        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            bool sortAscending = this.SortDirection == SortDirection.Ascending ? true : false;
            var siteActionTemplates = new List<tbl_SiteActionTemplate>();
            siteActionTemplates = DataManager.SiteActionTemplate.SelectAll(SiteId);

            switch (this.SortExpression)
            {
                case "Title":
                    siteActionTemplates = sortAscending ? siteActionTemplates.OrderBy(u => u.Title).ToList() : siteActionTemplates.OrderByDescending(u => u.Title).ToList();
                    break;
                case "tbl_ActionTypes.Title":
                    siteActionTemplates = sortAscending ? siteActionTemplates.OrderBy(u => u.tbl_ActionTypes.Title).ToList() : siteActionTemplates.OrderByDescending(u => u.tbl_ActionTypes.Title).ToList();
                    break;
                case "MessageCaption":
                    siteActionTemplates = sortAscending ? siteActionTemplates.OrderBy(u => u.MessageCaption).ToList() : siteActionTemplates.OrderByDescending(u => u.MessageCaption).ToList();
                    break;
            }

            gvSiteActionTemplates.DataSource = siteActionTemplates;
            gvSiteActionTemplates.DataBind();
        }



        /// <summary>
        /// Handles the PageIndexChanging event of the gvSiteActionTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvSiteActionTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSiteActionTemplates.PageIndex = e.NewPageIndex;
            BindGrid();
        }



        /// <summary>
        /// Handles the Sorting event of the gvSiteActionTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gvSiteActionTemplates_Sorting(object sender, GridViewSortEventArgs e)
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
            gvSiteActionTemplates.EditIndex = -1;
            gvSiteActionTemplates.SelectedIndex = -1;
        }



        /// <summary>
        /// Handles the Sorted event of the gvSiteActionTemplates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void gvSiteActionTemplates_Sorted(object sender, EventArgs e)
        {
            BindGrid();
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