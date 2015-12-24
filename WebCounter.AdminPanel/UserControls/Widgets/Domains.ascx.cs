using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class Domains : WidgetBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var dataManager = new DataManager();
            var site = dataManager.Sites.SelectById(CurrentUser.Instance.SiteID);            
            if (AccessProfile != null)
            {
                int? maxDomainsCount = AccessProfile.DomainsCount;
                var menu = dataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "DomainSettings");
                var url = string.Empty;
                if (menu != null)
                {
                    var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? dataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                    url = ResolveUrl(string.Format("~/{0}/{1}/List", tab, menu.tbl_Module.Name));
                }

                IsNotExistsDomains.Visible = site.tbl_SiteDomain.Count == 0;
                if (site.tbl_SiteDomain.Count > 0)
                {
                    lrlDomainsUsed.Text = string.Format("<a href='{0}'>{1}</a>", !string.IsNullOrEmpty(url) ? url : UrlsData.AP_Settings("Settings"), GetDomainsWithCase(site.tbl_SiteDomain.Count)); 

                    if (AccessProfile.DomainsCount.HasValue)                    
                        lrlDomainRestriction.Text = string.Format(" из <b>{0}</b> доступных", AccessProfile.DomainsCount);                    
                }
                else
                    DomainsUsage.Visible = false;

                if (!string.IsNullOrEmpty(url))
                    rbtnAddDomain.NavigateUrl = url;
                else
                    rbtnAddDomain.NavigateUrl = UrlsData.AP_SiteDomainsAdd("Settings", CurrentUser.Instance.SiteID);

                if (!maxDomainsCount.HasValue || maxDomainsCount.Value > site.tbl_SiteDomain.Count)                
                    AddDomain.Visible = true;                
                else
                    AddDomain.Visible = false;

                if (maxDomainsCount.HasValue && maxDomainsCount.Value == site.tbl_SiteDomain.Count)                
                    hlContactUrl.NavigateUrl = AccessProfile.ContactsPageUrl;                
                else
                    NoMoreDomains.Visible = false;

                if (site.tbl_SiteDomain.Count > 0)
                {
                    var pageCountWithCounter = site.tbl_SiteDomain.Sum(o => o.PageCountWithCounter);
                    var totalPageCount = site.tbl_SiteDomain.Sum(o => o.TotalPageCount);
                    lrlCounterFoundStats.Text = string.Format("{0} из {1}", pageCountWithCounter, totalPageCount);
                    
                    if (totalPageCount != 0 && (pageCountWithCounter/((double)totalPageCount / 100) < 80))
                        CounterFoundStats.Attributes.Add("class", "widget-error");
                }
                else
                    CounterFoundStats.Visible = false;

                if (!string.IsNullOrEmpty(url))                
                    rbtnCheck.NavigateUrl = url + "?check=true";                
                else
                    Check.Visible = false;
            }       
        }



        /// <summary>
        /// Gets the domains with case.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static string GetDomainsWithCase(int count)
        {            
            string domains = count.ToString();
            string domainsCase = "доменов";
            char lastChar = domains[domains.Length - 1];

            if (lastChar == '1')                        
                domainsCase = "домен";            
            else if (lastChar == '2' || lastChar == '3' || lastChar == '4')            
                domainsCase = "домена";

            return string.Format("{0} {1}", domains, domainsCase);
        }
    }
}