using System;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;

namespace WebCounter.AdminPanel
{
    public partial class ContactSession : LeadForceBasePage
    {        
        protected Guid SessionId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Сессии - LeadForce";

            if (Page.RouteData.Values["id"] != null)
                SessionId = Guid.Parse(Page.RouteData.Values["id"] as string);

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        protected void BindData()
        {
            var session = DataManager.ContactSessions.SelectById(SiteId, SessionId);
            if (session == null)
                return;

            var contact = session.tbl_Contact;
            hlContact.NavigateUrl = UrlsData.AP_Contact(contact.ID);
            hlContact.Text = contact.UserFullName;

            lrlSessionNumber.Text = session.UserSessionNumber.ToString();
            lrlSessionDate.Text = session.SessionDate.ToString("dd.MM.yyyy HH:mm");
            lrlRefferURL.Text = session.RefferURL;
            lrlCameFromURL.Text = session.CameFromURL;
            lrlEnterPointUrl.Text = session.EnterPointUrl;            

            lrlKeywords.Text = session.Keywords;

            lrlContent.Text = session.Content;
            lrlUserIP.Text = session.UserIP;

            if (session.RefferID.HasValue)
            {
                var contactReffer = DataManager.Contact.SelectById(SiteId, (Guid)session.RefferID);
                if (contactReffer != null)
                {
                    hlReffer.Text = contactReffer.UserFullName;
                    hlReffer.NavigateUrl = UrlsData.AP_Contact((Guid)session.RefferID);
                } 
            }

            lrlUserAgent.Text = session.UserAgent;

            if (session.BrowserID.HasValue)
            {
                var browser = session.tbl_Browsers;
                lrlBrowser.Text = string.Format("{0} {1}", browser.Name, browser.Version);
            }

            if (session.OperatingSystemID.HasValue)
            {
                var os = session.tbl_OperatingSystems;
                lrlOperationSystem.Text = string.Format("{0} {1}", os.Name, os.Version);
            }

            if (session.ResolutionID.HasValue)           
                lrlResolution.Text = session.tbl_Resolutions.Value;            

            if (session.MobileDeviceID.HasValue)            
                lrlMobileDevice.Text = session.tbl_MobileDevices.Name;            

            if (session.CountryID.HasValue)            
                lrlCountry.Text = session.tbl_Country.Name;

            if (session.CityID.HasValue)
            {
                var city = session.tbl_City;
                lrlCity.Text = city.Name;
                lrlDistrict.Text = city.tbl_District.Name;
                lrlRegion.Text = city.tbl_Region.Name;
            }

            if (session.AdvertisingCampaignID.HasValue)
                lrlAdvertisingCampaign.Text = session.tbl_AdvertisingCampaign.Title;

            if (session.AdvertisingPlatformID.HasValue)
                lrlAdvertisingPlatform.Text = session.tbl_AdvertisingPlatform.Title;

            if (session.AdvertisingTypeID.HasValue)
                lrlAdvertisingType.Text = session.tbl_AdvertisingType.Title;
        }
    }
}