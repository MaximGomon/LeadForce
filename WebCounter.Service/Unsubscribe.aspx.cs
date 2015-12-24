using System;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.Service
{
    public partial class Unsubscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["c"] != null && Request.QueryString["s"] != null)
            {
                var strContactId = Request.QueryString["c"];
                var strSiteId = Request.QueryString["s"];
                Guid contactId, siteId;
                if (Guid.TryParse(strContactId, out contactId) && Guid.TryParse(strSiteId, out siteId))
                {
                    var dm = new DataManager();
                    var contact = dm.Contact.SelectById(siteId, contactId);
                    if (contact != null)
                    {
                        contact.EmailStatusID = (int) EmailStatus.Banned;
                        dm.Contact.Update(contact);

                        var emailStats = dm.EmailStats.SelectByEmail(contact.Email);
                        if (emailStats != null)
                        {
                            if (emailStats.tbl_EmailStatsUnsubscribe.SingleOrDefault(o => o.SiteID == siteId) == null)
                                emailStats.tbl_EmailStatsUnsubscribe.Add(new tbl_EmailStatsUnsubscribe() {SiteID = siteId, CreatedAt = DateTime.Now});
                        }
                        else
                        {
                            emailStats = new tbl_EmailStats {Email = contact.Email};
                            dm.EmailStats.Add(emailStats);
                            emailStats.tbl_EmailStatsUnsubscribe.Add(new tbl_EmailStatsUnsubscribe() { SiteID = siteId, CreatedAt = DateTime.Now });
                            dm.EmailStats.Update(emailStats);
                        }
                    }
                }
            }
        }
    }
}