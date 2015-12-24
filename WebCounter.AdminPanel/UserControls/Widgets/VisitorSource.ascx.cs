using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class VisitorSource : WidgetBase
    {
        protected string ContactsUrl = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Contacts");
            if (menu != null)
            {
                var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                ContactsUrl = ResolveUrl(string.Format("~/{0}/{1}/List", tab, menu.tbl_Module.Name));
            }
            else
                ContactsUrl = UrlsData.AP_Contacts("Monitoring");  

            var advertisingPlatforms = DataManager.StatisticData.VisitorSourceNewAnonymousAdvertisingPlatform.OrderByDescending(o => o.Value.DbValue).Take(3);
            var sb = new StringBuilder();

            foreach (var advertisingPlatform in advertisingPlatforms)
            {
                if (advertisingPlatform.Value.Value == 0)
                    continue;

                var advPlatform = DataManager.AdvertisingPlatform.SelectById(Guid.Parse(advertisingPlatform.Key.Split('_')[1]));
                sb.Append(string.Format("<li><a href=\"{0}?f={1}\">{2} {3}</a></li>", ContactsUrl, advPlatform.ID, advPlatform.Title, advertisingPlatform.Value.HtmlValue));
            }

            lrlVisitorSourceNewAnonymousAdvertisingPlatform.Text = sb.ToString();
        }
    }
}