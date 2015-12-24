using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Interfaces;

namespace Labitec.LeadForce.CronJobs
{
    public class MassMailJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();

            var sites = dataManager.Sites.SelectAll().Where(a => a.IsActive);
            foreach (var site in sites)
            {
                var massMails =
                    dataManager.MassMail.SelectAll(site.ID).Where(
                        a =>
                        a.MassMailStatusID == (int) MassMailStatus.Scheduled && a.MailDate != null &&
                        a.MailDate <= DateTime.Now).ToList();
                if (massMails.Count > 0)
                {
                    foreach (var massMail in massMails)
                    {
                        dataManager.MassMailContact.AddToQueueSiteAction(massMail.ID);
                    }
                }
            }
            
        }
    }
}
