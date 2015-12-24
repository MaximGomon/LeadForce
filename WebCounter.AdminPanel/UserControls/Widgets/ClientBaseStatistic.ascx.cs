using System;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Widgets
{
    public partial class ClientBaseStatistic : WidgetBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            int? profileActiveContactCount = null;
            tbl_AccessProfile accessProfile = null;
            if (CurrentUser.Instance.SiteAccessProfileID.HasValue)
            {
                accessProfile = DataManager.AccessProfile.SelectById((Guid)CurrentUser.Instance.SiteAccessProfileID);                
                profileActiveContactCount = accessProfile.ActiveContactsCount;
            }

            if (profileActiveContactCount.HasValue && profileActiveContactCount > 0)
            {
                var percent = (decimal)profileActiveContactCount.Value/100;
                if (DataManager.StatisticData.ClientBaseStatisticCountInBase.DbValue / percent > 90)
                    lrlMessage.Text = string.Format("<li class\"widget-error\">Использовано {0} из {1} активных контактов. Свяжитесь с <a href=\"{2}\">отделом продаж</a> для увеличения количества активных пользователей!</li>", DataManager.StatisticData.ClientBaseStatisticCountInBase.DbValue.ToString("F0"), profileActiveContactCount.Value, accessProfile.ContactsPageUrl);
            }
        }
    }
}