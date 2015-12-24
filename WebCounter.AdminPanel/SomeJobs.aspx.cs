using System;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.DataAccessLayer;
using WufooSharp;

namespace WebCounter.AdminPanel
{
    public partial class SomeJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = new WufooClient("swotme", "1R68-WOYM-LDWD-GIL0");
            //var client = new WufooClient("crm2012", "X031-12R2-NR70-1R92");
            var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == "s7p0x3");
            //var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == "z7x3p9");
            var users = client.GetAllUsers();            
            var user = users.Count() > 1 ? users.FirstOrDefault(o => o.IsAccountOwner) : users.FirstOrDefault();
            var utcNow = DateTime.UtcNow;
            var timezone = (DateTime.Now - utcNow).TotalHours;

            var fb = new FilterBuilder(FilterMatchType.And);
            fb.IsAfter("DateCreated", DateTime.Now.Date.AddDays(-1));
            var entries = client.GetEntriesByFormId(wufooForm.Hash, fb, new Sort("DateCreated", SortDirection.Asc)).ToList();            
        }



        /// <summary>
        /// Updates the sources.
        /// </summary>
        private void UpdateSources()
        {
            Server.ScriptTimeout = 12000;

            var dataManager = new DataManager();
            var contacts =
                dataManager.Contact.SelectAll().Where(
                    o =>
                    !o.AdvertisingCampaignID.HasValue && !o.AdvertisingPlatformID.HasValue &&
                    !o.AdvertisingTypeID.HasValue).ToList();

            foreach (var contact in contacts)
            {
                var firstSession = dataManager.ContactSessions.SelectFirstSession(contact.SiteID, contact.ID);
                if (firstSession == null)
                    continue;

                if (string.IsNullOrEmpty(firstSession.EnterPointUrl))
                    continue;

                var result = SessionSource.Select(firstSession.EnterPointUrl, contact.RefferURL);
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.AdvertisingCampaign))
                    {
                        var advertisingCampaign = dataManager.AdvertisingCampaign.SelectByTitle(contact.SiteID, result.AdvertisingCampaign);
                        if (advertisingCampaign == null)
                        {
                            advertisingCampaign = new tbl_AdvertisingCampaign {SiteID = contact.SiteID};
                            advertisingCampaign.Title = advertisingCampaign.Code = result.AdvertisingCampaign;
                            dataManager.AdvertisingCampaign.Add(advertisingCampaign);
                        }

                        contact.AdvertisingCampaignID = advertisingCampaign.ID;
                        firstSession.AdvertisingCampaignID = advertisingCampaign.ID;
                    }

                    if (!string.IsNullOrEmpty(result.AdvertisingPlatform))
                    {
                        var advertisingPlatform = dataManager.AdvertisingPlatform.SelectByTitle(contact.SiteID, result.AdvertisingPlatform);
                        if (advertisingPlatform == null)
                        {
                            advertisingPlatform = new tbl_AdvertisingPlatform();
                            advertisingPlatform.SiteID = contact.SiteID;
                            advertisingPlatform.Title = advertisingPlatform.Code = result.AdvertisingPlatform;
                            dataManager.AdvertisingPlatform.Add(advertisingPlatform);
                        }

                        contact.AdvertisingPlatformID = advertisingPlatform.ID;
                        firstSession.AdvertisingPlatformID = advertisingPlatform.ID;
                    }


                    if (!string.IsNullOrEmpty(result.AdvertisingType))
                    {
                        var advertisingType = dataManager.AdvertisingType.SelectByTitle(contact.SiteID, result.AdvertisingType);
                        if (advertisingType == null)
                        {
                            advertisingType = new tbl_AdvertisingType();
                            advertisingType.SiteID = contact.SiteID;
                            advertisingType.Title = advertisingType.Code = result.AdvertisingType;
                            advertisingType.AdvertisingTypeCategoryID = 1;
                            dataManager.AdvertisingType.Add(advertisingType);
                        }

                        contact.AdvertisingTypeID = advertisingType.ID;
                        firstSession.AdvertisingTypeID = advertisingType.ID;
                    }                                        
                    
                    contact.CameFromURL = result.CameFromUrl;                    
                    firstSession.Content = result.Content;
                    firstSession.Keywords = result.Keywords;
                    firstSession.CameFromURL = result.CameFromUrl;

                    dataManager.Contact.Update(contact);
                    dataManager.ContactSessions.Update(firstSession);
                }             
            }
        }
    }
}