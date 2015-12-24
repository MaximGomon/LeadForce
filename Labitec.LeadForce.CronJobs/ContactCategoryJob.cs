using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;

namespace Labitec.LeadForce.CronJobs
{
    public class ContactCategoryJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();

            try
            {
                var sites = dataManager.Sites.SelectAll();
                foreach (var site in sites)
                {
                    var contacts =
                        dataManager.Contact.SelectAll(site.ID).Where(
                            a =>
                            a.Category == (int)ContactCategory.Active ||
                            a.Category == (int)ContactCategory.ActiveAboveTariff).ToList();

                    int activityCount = 0;
                    int actionCount = 0;
                    if (site.AccessProfileID != null)
                    {
                        var durationPeriod = dataManager.AccessProfile.SelectById((Guid)site.AccessProfileID).DurationPeriod;
                        
                        foreach (var contact in contacts)
                        {
                            if (durationPeriod.HasValue && durationPeriod.Value > 0)
                            {
                                activityCount = dataManager.ContactActivity.Select(site.ID, contact.ID, startDate: DateTime.Now.AddDays(-1 * durationPeriod.Value)).Count;
                                actionCount =
                                    dataManager.SiteAction.SelectAll(site.ID).Count(
                                        a =>
                                        a.ContactID == contact.ID &&
                                        DateTime.Now.AddDays(-1*durationPeriod.Value) >= a.ActionDate &&
                                        a.tbl_SiteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.System);
                            }
                            else
                            {
                                activityCount = dataManager.ContactActivity.Select(site.ID, contact.ID).Count;
                                actionCount =
                                    dataManager.SiteAction.SelectAll(site.ID).Count(a => a.ContactID == contact.ID &&
                                                                                         a.tbl_SiteActionTemplate.SiteActionTemplateCategoryID != (int)SiteActionTemplateCategory.System);
                            }

                            if (activityCount > 0 || actionCount > 0)
                                contact.Category = (int)ContactCategory.Known;

                            dataManager.Contact.Update(contact);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ContactCategory Scheduler ERROR: ", ex);
            }
        }
    }
}
