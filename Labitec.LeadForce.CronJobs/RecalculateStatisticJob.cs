using System;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services;

namespace Labitec.LeadForce.CronJobs
{
    public class RecalculateStatisticJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();
            var sites = dataManager.Sites.SelectAll();            
            var startDate = DateTime.Now.AddDays(1).AddMonths(-1).Date;
            var endDate = DateTime.Now.Date.AddDays(1);

            var previousStartDate = startDate.AddMonths(-1);
            var previousEndDate = endDate.AddMonths(-1).AddDays(-1);

            foreach (var site in sites)
            {
                dataManager = new DataManager();                

                dataManager.StatisticData.SiteId = site.ID;                 

                #region Категории клиентов

                try
                {
                    //dataManager.StatisticData.ContactCategoryKnownCount.DbValue = dataManager.Contact.SelectAll(site.ID).Count(a => a.Category == (int)ContactCategory.Known || a.Category == (int)ContactCategory.Active || a.Category == (int)ContactCategory.ActiveAboveTariff);
                    //dataManager.StatisticData.ContactCategoryKnownCount.Update(site.ID);
                    RecalculateStatisticService.ContactCategoryKnownCount(site.ID, dataManager.StatisticData.ContactCategoryKnownCount.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("ContactCategoryKnownCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.ContactCategoryActiveCount.DbValue = dataManager.Contact.SelectAll(site.ID).Count(a => a.Category == (int)ContactCategory.Active || a.Category == (int)ContactCategory.ActiveAboveTariff);
                    //dataManager.StatisticData.ContactCategoryActiveCount.Update(site.ID);
                    RecalculateStatisticService.ContactCategoryActiveCount(site.ID, dataManager.StatisticData.ContactCategoryActiveCount.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("ContactCategoryActiveCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.ContactCategoryAnonymousCount.DbValue = dataManager.Contact.SelectAll(site.ID).Count(a => a.Category == (int)ContactCategory.Anonymous);
                    //dataManager.StatisticData.ContactCategoryAnonymousCount.Update(site.ID);
                    RecalculateStatisticService.ContactCategoryAnonymousCount(site.ID, dataManager.StatisticData.ContactCategoryAnonymousCount.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("ContactCategoryAnonymousCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.ContactCategoryTotalCount.DbValue = dataManager.Contact.SelectAll(site.ID).Count(a => a.Category != (int)ContactCategory.Deleted);
                    //dataManager.StatisticData.ContactCategoryTotalCount.Update(site.ID);
                    RecalculateStatisticService.ContactCategoryTotalCount(site.ID, dataManager.StatisticData.ContactCategoryTotalCount.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("ContactCategoryTotalCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.ContactCategoryDeletedCount.DbValue = dataManager.Contact.SelectAll(site.ID).Count(a => a.Category == (int)ContactCategory.Deleted);
                    //dataManager.StatisticData.ContactCategoryDeletedCount.Update(site.ID);
                    RecalculateStatisticService.ContactCategoryDeletedCount(site.ID, dataManager.StatisticData.ContactCategoryDeletedCount.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("ContactCategoryDeletedCount " + site.ID, ex);
                }                

                #endregion

                #region Источники посетителей

                try
                {
                    //dataManager.StatisticData.VisitorSourceNewAnonymousTotal.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate && o.Category == (int)ContactCategory.Anonymous);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousTotal.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.Category == (int)ContactCategory.Anonymous);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousTotal.Update(site.ID);

                    RecalculateStatisticService.VisitorSourceNewAnonymousTotal(site.ID,
                                                                               dataManager.StatisticData.VisitorSourceNewAnonymousTotal.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("VisitorSourceNewAnonymousTotal " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.VisitorSourceNewAnonymousReffer.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.RefferID.HasValue && o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousReffer.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.RefferID.HasValue && o.CreatedAt >= startDate && o.CreatedAt <= endDate);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousReffer.Update(site.ID);

                    RecalculateStatisticService.VisitorSourceNewAnonymousReffer(site.ID,
                                                                               dataManager.StatisticData.VisitorSourceNewAnonymousReffer.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("VisitorSourceNewAnonymousReffer " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.VisitorSourceNewAnonymousDirect.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).Count(o => string.IsNullOrEmpty(o.RefferURL) && o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousDirect.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => string.IsNullOrEmpty(o.RefferURL) && o.CreatedAt >= startDate && o.CreatedAt <= endDate);
                    //dataManager.StatisticData.VisitorSourceNewAnonymousDirect.Update(site.ID);

                    RecalculateStatisticService.VisitorSourceNewAnonymousDirect(site.ID,
                                                                               dataManager.StatisticData.VisitorSourceNewAnonymousDirect.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("VisitorSourceNewAnonymousDirect " + site.ID, ex);
                }

                try
                {
                    RecalculateStatisticService.VisitorSourceNewAnonymousAdvertisingPlatform(site.ID, startDate, endDate, previousStartDate, previousEndDate);
                    //var advertisingPlatforms = dataManager.AdvertisingPlatform.SelectAll(site.ID).Select(o => o.ID).ToList();
                    //foreach (var advertisingPlatform in advertisingPlatforms)
                    //{
                    //    var statisticDataValue = dataManager.StatisticData.GetStatisticDataValueByCode(string.Format("VisitorSourceNewAnonymousAdvertisingPlatform_{0}", advertisingPlatform));
                    //    statisticDataValue.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.AdvertisingPlatformID == advertisingPlatform);
                    //    statisticDataValue.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate && o.AdvertisingPlatformID == advertisingPlatform);
                    //    statisticDataValue.Update(site.ID);
                    //}                
                }
                catch (Exception ex)
                {
                    Log.Error("VisitorSourceNewAnonymousAdvertisingPlatform " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.VisitorSourceRepeatedAnonymous.DbPreviousValue = dataManager.ContactSessions.SelectAll(site.ID).Where(o => o.UserSessionNumber > 1 && o.SessionDate >= previousStartDate && o.SessionDate <= previousEndDate).Select(o => o.ContactID).Distinct().Count();
                    //dataManager.StatisticData.VisitorSourceRepeatedAnonymous.DbValue = dataManager.ContactSessions.SelectAll(site.ID).Where(o => o.UserSessionNumber > 1 && o.SessionDate >= startDate && o.SessionDate <= endDate).Select(o => o.ContactID).Distinct().Count();
                    //dataManager.StatisticData.VisitorSourceRepeatedAnonymous.Update(site.ID);

                    RecalculateStatisticService.VisitorSourceRepeatedAnonymous(site.ID,
                                                                               dataManager.StatisticData.VisitorSourceRepeatedAnonymous.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("VisitorSourceRepeatedAnonymous " + site.ID, ex);
                }

                #endregion

                #region Статистика клиентской базы                

                try
                {
                    //dataManager.StatisticData.ClientBaseStatisticActiveCount.DbValue = dataManager.Contact.SelectClientBaseStatisticActiveCount(site.ID, startDate, endDate);
                    //dataManager.StatisticData.ClientBaseStatisticActiveCount.DbPreviousValue = dataManager.Contact.SelectClientBaseStatisticActiveCount(site.ID, previousStartDate, previousEndDate);
                    //dataManager.StatisticData.ClientBaseStatisticActiveCount.Update(site.ID);

                    RecalculateStatisticService.ClientBaseStatisticActiveCount(site.ID,
                                                                               dataManager.StatisticData.ClientBaseStatisticActiveCount.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("ClientBaseStatisticActiveCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.ClientBaseStatisticCountInBase.DbPreviousValue =
                    //dataManager.Contact.SelectAll(site.ID).Count(
                    //    o => o.CreatedAt <= previousEndDate &&
                    //    (o.Category == (int)ContactCategory.Active ||
                    //     o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //     o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseStatisticCountInBase.DbValue =
                    //    dataManager.Contact.SelectAll(site.ID).Count(
                    //        o => o.CreatedAt <= endDate &&
                    //             (o.Category == (int)ContactCategory.Active ||
                    //              o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //              o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseStatisticCountInBase.Update(site.ID);

                    RecalculateStatisticService.ClientBaseStatisticCountInBase(site.ID,
                                                           dataManager.StatisticData.ClientBaseStatisticCountInBase.Code,
                                                           endDate, previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("ClientBaseStatisticCountInBase " + site.ID, ex);
                }
                
                #endregion

                #region Рост клиентской базы                

                try
                {
                    //dataManager.StatisticData.ClientBaseGrowthNewTotal.DbPreviousValue =
                    //dataManager.Contact.SelectAll(site.ID).Count(
                    //    o => o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate &&
                    //    (o.Category == (int)ContactCategory.Active ||
                    //     o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //     o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseGrowthNewTotal.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && (o.Category == (int)ContactCategory.Active ||
                    //         o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //         o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseGrowthNewTotal.Update(site.ID);

                    RecalculateStatisticService.ClientBaseGrowthNewTotal(site.ID,
                                                                               dataManager.StatisticData.ClientBaseGrowthNewTotal.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("ClientBaseGrowthNewTotal " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.ClientBaseGrowthNewRegistered.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).
                    //    Count(o => o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate && o.RegistrationSourceID == 
                    //        (int)RegistrationSource.CounterService && (o.Category == (int)ContactCategory.Active ||
                    //     o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //     o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseGrowthNewRegistered.DbValue = dataManager.Contact.SelectAll(site.ID).
                    //    Count(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.RegistrationSourceID == (int)RegistrationSource.CounterService && (o.Category == (int)ContactCategory.Active ||
                    //         o.Category == (int)ContactCategory.ActiveAboveTariff ||
                    //         o.Category == (int)ContactCategory.Known));
                    //dataManager.StatisticData.ClientBaseGrowthNewRegistered.Update(site.ID);

                    RecalculateStatisticService.ClientBaseGrowthNewRegistered(site.ID,
                                                                               dataManager.StatisticData.ClientBaseGrowthNewRegistered.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("ClientBaseGrowthNewRegistered " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.ClientBaseGrowthNewImported.DbPreviousValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= previousStartDate && o.CreatedAt <= previousEndDate && o.RegistrationSourceID == (int)RegistrationSource.Import);
                    //dataManager.StatisticData.ClientBaseGrowthNewImported.DbValue = dataManager.Contact.SelectAll(site.ID).Count(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate && o.RegistrationSourceID == (int)RegistrationSource.Import);
                    //dataManager.StatisticData.ClientBaseGrowthNewImported.Update(site.ID);

                    RecalculateStatisticService.ClientBaseGrowthNewImported(site.ID,
                                                                               dataManager.StatisticData.ClientBaseGrowthNewImported.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("ClientBaseGrowthNewImported " + site.ID, ex);
                }

                foreach (var domain in site.tbl_SiteDomain)
                {
                    try
                    {
                        dataManager.SiteDomain.CheckAll(domain);
                    }
                    catch(Exception ex)
                    {                  
                        Log.Warn(string.Format("Ошибка проверки домена : ID {0} ", domain.ID), ex);
                    }                    
                }

                #endregion   
             
                #region Заявок без ответа

                try
                {
                    //dataManager.StatisticData.LoyaltyManagementUnansweredRequest.DbValue = dataManager.Requirement.SelectAll(site.ID).Count(o => !o.tbl_RequirementStatus.IsLast);
                    //dataManager.StatisticData.LoyaltyManagementUnansweredRequest.Update(site.ID);
                    RecalculateStatisticService.LoyaltyManagementUnansweredRequest(site.ID, dataManager.StatisticData.LoyaltyManagementUnansweredRequest.Code);
                }
                catch (Exception ex)
                {
                    Log.Error("LoyaltyManagementUnansweredRequest " + site.ID, ex);
                }

                #endregion

                #region Управление лояльностью
                
                try
                {
                    dataManager.StatisticData.LoyaltyManagementIsExistPortal.DbValue = dataManager.PortalSettings.IsPortalUp(site.ID) ? 1 : 0;
                    dataManager.StatisticData.LoyaltyManagementIsExistPortal.Update(site.ID);
                }
                catch (Exception ex)
                {
                    Log.Error("LoyaltyManagementIsExistPortal " + site.ID, ex);
                }

                #endregion

                #region Новые продажи -> Потенциальные клиенты
                
                try
                {
                    //dataManager.StatisticData.NewSalesPotentialClientCount.DbPreviousValue = dataManager.Invoice.SelectNewSalesPotentialClientCount(site.ID, previousStartDate, previousEndDate);
                    //dataManager.StatisticData.NewSalesPotentialClientCount.DbValue = dataManager.Invoice.SelectNewSalesPotentialClientCount(site.ID, startDate, endDate);
                    //dataManager.StatisticData.NewSalesPotentialClientCount.Update(site.ID);

                    RecalculateStatisticService.NewSalesPotentialClientCount(site.ID,
                                                                               dataManager.StatisticData.NewSalesPotentialClientCount.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesPotentialClientCount " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.NewSalesPotentialClientActiveCount.DbPreviousValue = dataManager.Invoice.SelectNewSalesPotentialClientActiveCount(site.ID, previousStartDate, previousEndDate);
                    //dataManager.StatisticData.NewSalesPotentialClientActiveCount.DbValue = dataManager.Invoice.SelectNewSalesPotentialClientActiveCount(site.ID, startDate, endDate);
                    //dataManager.StatisticData.NewSalesPotentialClientActiveCount.Update(site.ID);
                    RecalculateStatisticService.NewSalesPotentialClientActiveCount(site.ID,
                                                                               dataManager.StatisticData.NewSalesPotentialClientActiveCount.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesPotentialClientActiveCount " + site.ID, ex);
                }
                

                #endregion

                #region Новые продажи -> Счета к оплате

                //var newForPaid = dataManager.Invoice.SelectNewSalesInvoiceForPayment(site.ID, startDate, endDate);
                //var newPreviousForPaid = dataManager.Invoice.SelectNewSalesInvoiceForPayment(site.ID, previousStartDate, previousEndDate);

                try
                {
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentCount.DbValue = newForPaid.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentCount.DbPreviousValue = newPreviousForPaid.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentCount.Update(site.ID);

                    RecalculateStatisticService.NewSalesInvoiceForPayment(site.ID,                                                                               
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesInvoiceForPaymentCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentAmount.DbValue = newForPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentAmount.DbPreviousValue = newPreviousForPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentAmount.Update(site.ID);
                    //RecalculateStatisticService.NewSalesInvoiceForPaymentAmount(site.ID,
                    //                                                           dataManager.StatisticData.NewSalesInvoiceForPaymentAmount.Code,
                    //                                                           startDate, endDate, previousStartDate,
                    //                                                           previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesInvoiceForPaymentAmount " + site.ID, ex);
                }

                //var newExposed = dataManager.Invoice.SelectNewSalesInvoiceForPaymentExposed(site.ID, startDate, endDate);
                //var newPreviousExposed = dataManager.Invoice.SelectNewSalesInvoiceForPaymentExposed(site.ID, previousStartDate, previousEndDate);

                try
                {
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedCount.DbValue = newExposed.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedCount.DbPreviousValue = newPreviousExposed.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedCount.Update(site.ID);
                    RecalculateStatisticService.NewSalesInvoiceForPaymentExposed(site.ID, startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesInvoiceForPaymentExposed " + site.ID, ex);
                }
                
                try
                {
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedAmount.DbValue = newExposed.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedAmount.DbPreviousValue = newPreviousExposed.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentExposedAmount.Update(site.ID);

                    //RecalculateStatisticService.NewSalesInvoiceForPaymentExposedAmount(site.ID,
                    //                                                           dataManager.StatisticData.NewSalesInvoiceForPaymentExposedAmount.Code,
                    //                                                           startDate, endDate, previousStartDate,
                    //                                                           previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesInvoiceForPaymentExposedAmount " + site.ID, ex);
                }

                //var newPaid = dataManager.Invoice.SelectNewSalesInvoiceForPaymentPayed(site.ID, startDate, endDate);
                //var newPreviousPaid = dataManager.Invoice.SelectNewSalesInvoiceForPaymentPayed(site.ID, previousStartDate, previousEndDate);

                try
                {
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentPayedCount.DbValue = newPaid.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentPayedCount.DbPreviousValue = newPreviousPaid.Count();
                    //dataManager.StatisticData.NewSalesInvoiceForPaymentPayedCount.Update(site.ID);

                    RecalculateStatisticService.NewSalesInvoiceForPaymentPayed(site.ID, startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("NewSalesInvoiceForPaymentPayed " + site.ID, ex);
                }
                
                //try
                //{
                //    dataManager.StatisticData.NewSalesInvoiceForPaymentPayedAmount.DbValue = newPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                //    dataManager.StatisticData.NewSalesInvoiceForPaymentPayedAmount.DbPreviousValue = newPreviousPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                //    dataManager.StatisticData.NewSalesInvoiceForPaymentPayedAmount.Update(site.ID);
                //}
                //catch (Exception ex)
                //{
                //    Log.Error("NewSalesInvoiceForPaymentPayedAmount " + site.ID, ex);
                //}

                #endregion


                #region Повторные продажи -> Потенциальные клиенты

                try
                {
                    //dataManager.StatisticData.RepeatSalesPotentialClientCount.DbPreviousValue = dataManager.Invoice.SelectRepeatSalesPotentialClientCount(site.ID, previousStartDate, previousEndDate);
                    //dataManager.StatisticData.RepeatSalesPotentialClientCount.DbValue = dataManager.Invoice.SelectRepeatSalesPotentialClientCount(site.ID, startDate, endDate);
                    //dataManager.StatisticData.RepeatSalesPotentialClientCount.Update(site.ID);

                    RecalculateStatisticService.RepeatSalesPotentialClientCount(site.ID,
                                                                               dataManager.StatisticData.RepeatSalesPotentialClientCount.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesPotentialClientCount " + site.ID, ex);
                }

                try
                {
                    //dataManager.StatisticData.RepeatSalesPotentialClientActiveCount.DbPreviousValue = dataManager.Invoice.SelectRepeatSalesPotentialClientActiveCount(site.ID, previousStartDate, previousEndDate);
                    //dataManager.StatisticData.RepeatSalesPotentialClientActiveCount.DbValue = dataManager.Invoice.SelectRepeatSalesPotentialClientActiveCount(site.ID, startDate, endDate);
                    //dataManager.StatisticData.RepeatSalesPotentialClientActiveCount.Update(site.ID);

                    RecalculateStatisticService.RepeatSalesPotentialClientActiveCount(site.ID,
                                                                               dataManager.StatisticData.RepeatSalesPotentialClientActiveCount.Code,
                                                                               startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesPotentialClientActiveCount " + site.ID, ex);
                }
                

                #endregion

                #region Повторные продажи -> Счета к оплате

                //var repeatForPaid = dataManager.Invoice.SelectRepeatSalesInvoiceForPayment(site.ID, startDate, endDate);
                //var repeatPreviousForPaid = dataManager.Invoice.SelectRepeatSalesInvoiceForPayment(site.ID, previousStartDate, previousEndDate);

                try
                {
                    //dataManager.StatisticData.RepeatSalesInvoiceForPaymentCount.DbValue = repeatForPaid.Count();
                    //dataManager.StatisticData.RepeatSalesInvoiceForPaymentCount.DbPreviousValue = repeatPreviousForPaid.Count();
                    //dataManager.StatisticData.RepeatSalesInvoiceForPaymentCount.Update(site.ID);
                    RecalculateStatisticService.RepeatSalesInvoiceForPayment(site.ID, startDate, endDate, previousStartDate,
                                                                               previousEndDate);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesInvoiceForPaymentCount " + site.ID, ex);
                }

                //try
                //{
                //    dataManager.StatisticData.RepeatSalesInvoiceForPaymentAmount.DbValue = repeatForPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                //    dataManager.StatisticData.RepeatSalesInvoiceForPaymentAmount.DbPreviousValue = repeatPreviousForPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                //    dataManager.StatisticData.RepeatSalesInvoiceForPaymentAmount.Update(site.ID);
                //}
                //catch (Exception ex)
                //{
                //    Log.Error("RepeatSalesInvoiceForPaymentAmount " + site.ID, ex);
                //}
                
                var repeatExposed = dataManager.Invoice.SelectRepeatSalesInvoiceForPaymentExposed(site.ID, startDate, endDate);
                var repeatPreviousExposed = dataManager.Invoice.SelectRepeatSalesInvoiceForPaymentExposed(site.ID, previousStartDate, previousEndDate);

                try
                {
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedCount.DbValue = repeatExposed.Count();
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedCount.DbPreviousValue = repeatPreviousExposed.Count();
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedCount.Update(site.ID);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesInvoiceForPaymentExposedCount " + site.ID, ex);
                }

                try
                {
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedAmount.DbValue = repeatExposed.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedAmount.DbPreviousValue = repeatPreviousExposed.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentExposedAmount.Update(site.ID);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesInvoiceForPaymentExposedAmount " + site.ID, ex);
                }

                var repeatPaid = dataManager.Invoice.SelectRepeatSalesInvoiceForPaymentPayed(site.ID, startDate, endDate);
                var repeatPreviousPaid = dataManager.Invoice.SelectRepeatSalesInvoiceForPaymentPayed(site.ID, previousStartDate, previousEndDate);

                try
                {
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedCount.DbValue = repeatPaid.Count();
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedCount.DbPreviousValue = repeatPreviousPaid.Count();
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedCount.Update(site.ID);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesInvoiceForPaymentPayedCount " + site.ID, ex);
                }
                
                try
                {
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedAmount.DbValue = repeatPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedAmount.DbPreviousValue = repeatPreviousPaid.Sum(o => (decimal?)o.InvoiceAmount) ?? 0;
                    dataManager.StatisticData.RepeatSalesInvoiceForPaymentPayedAmount.Update(site.ID);
                }
                catch (Exception ex)
                {
                    Log.Error("RepeatSalesInvoiceForPaymentPayedAmount " + site.ID, ex);
                }
                

                #endregion
            }
        }
    }
}
