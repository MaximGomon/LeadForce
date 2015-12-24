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
    public partial class LoyaltyManagement : WidgetBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var site = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
            var totalPageCount = site.tbl_SiteDomain.Sum(o => o.TotalPageCount);
            var pageCountWithForms = site.tbl_SiteDomain.Sum(o => o.PageCountWithForms);            

            lrlTotalPages.Text = totalPageCount.ToString();
            lrlTotalForms.Text = pageCountWithForms.ToString();

            var menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Form");
            string url = string.Empty;
            if (menu != null)
            {
                var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                url = ResolveUrl(string.Format("~/{0}/{1}/List", tab, menu.tbl_Module.Name));
            }
            else 
                url = UrlsData.AP_SiteActivityRules((int)RuleType.Form, "Evaluation");

            if (totalPageCount != 0 && pageCountWithForms / ((decimal)totalPageCount / 100) < 50)
            {                
                lrlMessage1.Text = string.Format("<li class=\"widget-error\">Для клиентов важна возможность задать вопрос или оставить отзыв. Подключите <a href=\"{0}\">формы обратной связи</a> к сайту!</li>", url);
            }

            if (DataManager.StatisticData.LoyaltyManagementInviteFriendFormCount.Value == 0)
                lrlMessage2.Text = string.Format("<li class=\"widget-error\">Используй силу рекомендаций лояльных клиентов. Подключите форму <a href=\"{0}\">Пригласи друга</a> к сайту!</li>", url);

            if (DataManager.StatisticData.LoyaltyManagementIsExistPortal.DbValue == 1)
            {
                var portal = DataManager.PortalSettings.SelectBySiteId(CurrentUser.Instance.SiteID);
                lrlPortal.Text = string.Format("<li>Портал поддержки доступен по ссылке: <a href=\"{0}\" target=\"_blank\">{0}</a></li>", string.Concat("http://", portal.Domain.Replace("http://", string.Empty)));
            }
            else
            {
                menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Discussions");
                if (menu != null)
                {
                    var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                    url = ResolveUrl(string.Format("~/{0}/{1}/PortalSetting", tab, menu.tbl_Module.Name));
                }
                else
                    url = UrlsData.AP_Settings("Settings");
                
                lrlPortal.Text = string.Format("<li>Внешний портал не настроен. <a href=\"{0}\">Настроить!</a></li>", url);
            }
        }
    }
}