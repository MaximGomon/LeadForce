using System;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class ClientBaseGrowth : WidgetBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var site = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
            var totalPageCount = site.tbl_SiteDomain.Sum(o => o.TotalPageCount);
            var pageCountWithForms = site.tbl_SiteDomain.Sum(o => o.PageCountWithForms);
            var pageCountWithoutForms = totalPageCount - pageCountWithForms;

            lrlTotalPages.Text = totalPageCount.ToString();
            lrlTotalForms.Text = pageCountWithForms.ToString();
            
            if (totalPageCount != 0 && pageCountWithoutForms/((decimal)totalPageCount/100) > 10)
            {
                var menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Form");
                string url;
                if (menu != null)
                {
                    var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                    url = ResolveUrl(string.Format("~/{0}/{1}/List", tab, menu.tbl_Module.Name));
                }
                else 
                    url = UrlsData.AP_SiteActivityRules((int) RuleType.Form, "Evaluation");

                lrlMessage.Text = string.Format("<li class=\"widget-error\">{0} страниц не содержит контактных форм! <a href=\"{1}\">Перейти к модулю Формы</a></li>", pageCountWithoutForms, url);
            }

            var clientBaseGrowthTemplateForms = DataManager.StatisticData.ClientBaseGrowthTemplateForm;
            var sb = new StringBuilder();

            foreach (var clientBaseGrowthTemplateForm in clientBaseGrowthTemplateForms)
            {
                if (clientBaseGrowthTemplateForm.Value.DbValue == 0)
                    continue;

                var siteActivityRule = DataManager.SiteActivityRules.SelectById(Guid.Parse(clientBaseGrowthTemplateForm.Key.Split('_')[1]));
                sb.Append(string.Format("<li>{0} {1}</li>", siteActivityRule.Name, clientBaseGrowthTemplateForm.Value.DbValue.ToString("F0")));
            }

            lrlClientBaseGrowthTemplateForm.Text = sb.ToString();

            if (DataManager.StatisticData.ClientBaseOtherFormsCount.DbValue > 0)
                lrlClientBaseOtherFormsCount.Text = string.Format("<li>Другие <b>{0}</b></li>", DataManager.StatisticData.ClientBaseOtherFormsCount.DbValue.ToString("F0"));

            if (!string.IsNullOrEmpty(lrlClientBaseGrowthTemplateForm.Text) || !string.IsNullOrEmpty(lrlClientBaseOtherFormsCount.Text))
                lrlTitle.Text = "Из них";
            else
                lrlTitle.Text = string.Empty;

            rbtnImport.NavigateUrl = GetImportUrl();
        }


        protected string GetImportUrl()
        {
            var menu = DataManager.Menu.SelectByAccessProfileID(AccessProfile.ID).FirstOrDefault(o => o.ModuleID.HasValue && o.tbl_Module.Name == "Imports");
            string url;
            if (menu != null)
            {
                var tab = string.IsNullOrEmpty(menu.TabName) && menu.ParentID.HasValue ? DataManager.Menu.SelectByID(menu.ParentID.Value).TabName : menu.TabName;
                url = ResolveUrl(string.Format("~/{0}/{1}/Add", tab, menu.tbl_Module.Name));
            }
            else
                url = UrlsData.AP_ImportAdd("Settings");

            return url;
        }
    }
}