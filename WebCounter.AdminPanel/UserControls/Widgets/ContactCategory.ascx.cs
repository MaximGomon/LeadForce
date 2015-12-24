using System;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class ContactCategory : WidgetBase
    {
        protected string Url = string.Empty;
        protected string Category = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Contacts");
            if (menu != null)
            {
                var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                Url = ResolveUrl(string.Format("~/{0}/{1}/List", tab, menu.tbl_Module.Name));
            }
            else            
                Url = UrlsData.AP_Contacts("Monitoring");            

            if (Request.QueryString["c"] != null)
                Category = Request.QueryString["c"].ToLower();
            else if (IsLeftColumn && Request.QueryString["f"] == null)
                Category = "known";

            int? profileActiveContactCount = null;
            if (CurrentUser.Instance.SiteAccessProfileID.HasValue)            
                profileActiveContactCount = DataManager.AccessProfile.SelectById((Guid)CurrentUser.Instance.SiteAccessProfileID).ActiveContactsCount;

            if (profileActiveContactCount.HasValue && profileActiveContactCount > 0)
            {
                if (DataManager.StatisticData.ContactCategoryActiveCount.DbValue > profileActiveContactCount)
                    lrlActiveCount.Text = string.Format("<b><span style=\"color:red\">{0}</span> из {1}</b>", DataManager.StatisticData.ContactCategoryActiveCount.DbValue.ToString("F0"), profileActiveContactCount);
                else
                    lrlActiveCount.Text = string.Format("<b>{0} из {1}</b>", DataManager.StatisticData.ContactCategoryActiveCount.DbValue.ToString("F0"), profileActiveContactCount);
            }
            else
                lrlActiveCount.Text = string.Format("<b>{0}</b>", DataManager.StatisticData.ContactCategoryActiveCount.DbValue.ToString("F0"));
        }
    }
}