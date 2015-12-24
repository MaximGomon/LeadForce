using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;
using System.Linq;

namespace WebCounter.AdminPanel.UserControls.SiteSettings
{
    public partial class SiteDomains : System.Web.UI.UserControl
    {
        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        /// <value>
        /// The site id.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                if (ViewState["SiteId"] == null)
                    ViewState["SiteId"] = CurrentUser.Instance.SiteID;

                return (Guid)ViewState["SiteId"];
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }




        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Module
        {
            get
            {
                return (string)ViewState["Module"];
            }
            set
            {
                ViewState["Module"] = value;
            }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(lbtnRegisterDomainsByActionLog, gridSiteDomains);

            if (SiteId == Guid.Empty)
            {                
                plContainer.Visible = false;
                return;
            }

            gridSiteDomains.SiteID = SiteId;
            gridSiteDomains.AddNavigateUrl = UrlsData.AP_SiteDomainsAdd(Module, SiteId);            
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the gridSiteDomains control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridSiteDomains_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var data = (DataRowView)e.Item.DataItem;
                
                ((HyperLink)item.FindControl("hlEdit")).NavigateUrl = UrlsData.AP_SiteDomainsEdit(Module, Guid.Parse(data["ID"].ToString()));

                ((ImageButton)item.FindControl("ibDelete")).CommandArgument = data["tbl_SiteDomain_ID"].ToString();
                ((ImageButton)item.FindControl("ibDelete")).OnClientClick = "return confirm(\"Вы действительно хотите удалить запись?\");";
                ((Literal) item.FindControl("lrlSiteDomainStatus")).Text = EnumHelper.GetEnumDescription((SiteDomainStatus) int.Parse(data["tbl_SiteDomain_StatusID"].ToString()));
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnRegisterDomainsByActionLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnRegisterDomainsByActionLog_OnClick(object sender, EventArgs e)
        {
            var dataManager = new DataManager();
            var domains = dataManager.SiteDomain.SelectDomainFromContactActivity(SiteId);

            var site = dataManager.Sites.SelectById(SiteId);
            int? domainsCount = null;
            if (site.AccessProfileID.HasValue)
            {
                tbl_AccessProfile accessProfile = dataManager.AccessProfile.SelectById(site.AccessProfileID.Value);
                if (accessProfile != null)
                    domainsCount = accessProfile.DomainsCount;
            }


            foreach (var domain in domains)
            {
                if (string.IsNullOrEmpty(domain))
                    continue;

                var siteDomain = new tbl_SiteDomain {Domain = domain, SiteID = SiteId, Aliases = string.Empty};
                if (dataManager.SiteDomain.SelectByDomain(siteDomain.Domain).ID == Guid.Empty &&
                    (!domainsCount.HasValue ||
                     dataManager.SiteDomain.SelectDomainsBySiteId(site.ID).Count() < domainsCount))
                {
                    dataManager.SiteDomain.Add(siteDomain);
                }
            }

            gridSiteDomains.Rebind();
        }



        /// <summary>
        /// Handles the OnCommand event of the ibDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void ibDelete_OnCommand(object sender, CommandEventArgs e)
        {
            var dataManager = new DataManager();

            var siteDomain = dataManager.SiteDomain.SelectById(SiteId, e.CommandArgument.ToString().ToGuid());
            dataManager.SiteDomain.Delete(siteDomain);

            gridSiteDomains.Rebind();
        }
    }
}