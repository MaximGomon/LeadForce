using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services;
using WufooSharp;

namespace Labitec.LeadForce.CronJobs
{
    public class WufooFormJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();
            var sites = dataManager.Sites.SelectAll();
            var currentDate = DateTime.Now;

            foreach (var site in sites)
            {
                var forms = dataManager.SiteActivityRules.SelectByRuleType(site.ID, new List<int>() { (int)RuleType.WufooForm }).ToList();

                foreach (var form in forms)
                {
                    try
                    {
                        if (!form.WufooUpdatePeriod.HasValue)
                            continue;

                        switch ((WufooUpdatePeriod)form.WufooUpdatePeriod)
                        {
                            case WufooUpdatePeriod.Hourly:
                                if (form.WufooRevisionDate.HasValue && currentDate.ToString("yyyy-MM-dd HH") == form.WufooRevisionDate.Value.ToString("yyyy-MM-dd HH"))
                                    continue;
                                break;
                            case WufooUpdatePeriod.Daily:
                                if (form.WufooRevisionDate.HasValue && currentDate.ToString("yyyy-MM-dd") == form.WufooRevisionDate.Value.ToString("yyyy-MM-dd"))
                                    continue;
                                break;
                            case WufooUpdatePeriod.Manual:
                                continue;
                        }

                        var externalForm = dataManager.SiteActivityRuleExternalForms.Select(form.Code, form.ID);
                        if (externalForm == null || !externalForm.tbl_SiteActivityRuleExternalFormFields.Any())
                            continue;
                        
                        
                        var revisionDate = DateTime.Now;
                        if (!form.WufooRevisionDate.HasValue)
                            form.WufooRevisionDate = revisionDate;

                        ExternalFormService.WufooLoadData(site.ID, externalForm.SiteActivityRuleID, form.WufooRevisionDate.Value.Date.AddDays(-2));

                        //var client = new WufooClient(form.WufooName, form.WufooAPIKey);
                        //var wufooForm = client.GetAllForms().FirstOrDefault(o => o.Hash == form.Code);
                        //IEnumerable<Entry> entries = null;
                        //var fb = new FilterBuilder(FilterMatchType.And);
                        //fb.IsAfter("DateCreated", form.WufooRevisionDate.Value);
                        //entries = client.GetEntriesByFormId(wufooForm.Hash, fb, new Sort("DateCreated", SortDirection.Asc)).ToList();
                        
                        //foreach (var entry in entries)
                        //{
                        //    ExternalFormService.ProceedExternalForm(CurrentUser.Instance.SiteID, externalForm.ID, wufooForm.Hash,
                        //                                            null, entry.Responses,
                        //                                            string.Format("{0}${1}${2}", site.ID, wufooForm.Hash, entry.EntryId), entry.DateCreated);
                        //}

                        form.WufooRevisionDate = revisionDate;
                        dataManager.SiteActivityRules.Update(form);                    
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Ошибка обработки формы: {0}", form.Code), ex);
                    }                    
                }
            }
        }
    }
}
