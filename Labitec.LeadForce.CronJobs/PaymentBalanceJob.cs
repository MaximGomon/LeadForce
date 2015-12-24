using System;
using System.Globalization;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.CronJobs
{
    public class PaymentBalanceJob : ICronJob
    {
        public void Run()
        {
            var dataManager = new DataManager();

            var paymentPasses = dataManager.PaymentPass.SelectAll().Where(a => a.ProcessedByCron == false && a.OutgoCFOID!=null && a.OutgoPaymentArticleID!=null).OrderBy(a => a.tbl_Payment.DatePlan).OrderBy(a => a.tbl_Payment.DateFact);

            var today = DateTime.Now.Date;

            foreach (var paymentPass in paymentPasses)
            {
                #region Новая проводка
                if (paymentPass.OldAmount == null && paymentPass.OldCreatedAt==null && paymentPass.OldOutgoCFOID==null && paymentPass.OldOutgoPaymentArticleID==null && paymentPass.OldOutgoPaymentPassCategoryID==null && !paymentPass.ToDelete)
                {
                    var paymentBalances = dataManager.PaymentBalance.SelectByDate(paymentPass.SiteID, paymentPass.OutgoPaymentPassCategoryID, paymentPass.OutgoCFOID, paymentPass.OutgoPaymentArticleID, paymentPass.CreatedAt);
                    foreach (var paymentBalance in paymentBalances)
                    {
                        if (paymentPass.Amount != null)
                        {
                            paymentBalance.BalancePlan += (decimal)paymentPass.Amount;
                            if (paymentPass.IsFact != null && paymentPass.IsFact == true) paymentBalance.BalanceFact += (decimal)paymentPass.Amount;
                            dataManager.PaymentBalance.Update(paymentBalance);
                        }
                    }
                    if (paymentBalances==null)
                    {
                        var balance = new tbl_PaymentBalance();
                        balance.SiteID = paymentPass.SiteID;
                        balance.PaymentPassCategoryID = paymentPass.OutgoPaymentPassCategoryID;
                        balance.CFOID = paymentPass.OutgoCFOID;
                        balance.PaymentArticleID = paymentPass.OutgoPaymentArticleID;
                        balance.Date = paymentPass.CreatedAt;
                        balance.BalancePlan = (decimal) paymentPass.Amount;
                        balance.BalanceFact = paymentPass.IsFact == true ? (decimal) paymentPass.Amount : 0;
                        dataManager.PaymentBalance.Add(balance);
                    }
                    paymentPass.ProcessedByCron = true;
                    dataManager.PaymentPass.Update(paymentPass);
                    continue;
                }
                #endregion
                #region На удаление
                if (paymentPass.ToDelete)
                {
                    var paymentBalances = dataManager.PaymentBalance.SelectByDate(paymentPass.SiteID,paymentPass.OutgoPaymentPassCategoryID, paymentPass.OutgoCFOID, paymentPass.OutgoPaymentArticleID, paymentPass.CreatedAt);
                    foreach (var paymentBalance in paymentBalances)
                    {
                        if (paymentBalance.BalancePlan!=null && paymentPass.Amount != null && paymentBalance.BalancePlan>=(decimal)paymentPass.Amount)
                        {
                            paymentBalance.BalancePlan -= (decimal)paymentPass.Amount;
                            if (paymentPass.IsFact != null && paymentPass.IsFact == true) paymentBalance.BalanceFact -= (decimal)paymentPass.Amount;
                            dataManager.PaymentBalance.Update(paymentBalance);
                        }
                    }
                    dataManager.PaymentPass.Delete(paymentPass);
                    continue;
                }
                #endregion

                #region Изменение
                if ((paymentPass.OldAmount != null || paymentPass.OldCreatedAt!=null || paymentPass.OldOutgoCFOID!=null || paymentPass.OldOutgoPaymentArticleID!=null || paymentPass.OldOutgoPaymentPassCategoryID!=null) && !paymentPass.ToDelete)
                {
                    var paymentBalances = dataManager.PaymentBalance.SelectByDate(paymentPass.SiteID, paymentPass.OutgoPaymentPassCategoryID, paymentPass.OutgoCFOID, paymentPass.OutgoPaymentArticleID, paymentPass.CreatedAt);
                    foreach (var paymentBalance in paymentBalances) // Обновить новые значения
                    {
                        if (paymentPass.Amount != null)
                        {
                            paymentBalance.BalancePlan += (decimal) paymentPass.Amount;
                            if (paymentPass.IsFact != null && paymentPass.IsFact == true)
                                paymentBalance.BalanceFact += (decimal) paymentPass.Amount;
                            dataManager.PaymentBalance.Update(paymentBalance);
                        }
                    }
                    if (paymentBalances == null) //Создать новый баланс
                    {
                        var balance = new tbl_PaymentBalance();
                        balance.SiteID = paymentPass.SiteID;
                        balance.PaymentPassCategoryID = paymentPass.OutgoPaymentPassCategoryID;
                        balance.CFOID = paymentPass.OutgoCFOID;
                        balance.PaymentArticleID = paymentPass.OutgoPaymentArticleID;
                        balance.Date = paymentPass.CreatedAt;
                        balance.BalancePlan = (decimal)paymentPass.Amount;
                        balance.BalanceFact = paymentPass.IsFact == true ? (decimal)paymentPass.Amount : 0;
                        dataManager.PaymentBalance.Add(balance);
                    }
                    paymentBalances = dataManager.PaymentBalance.SelectByDate(paymentPass.SiteID, paymentPass.OldOutgoPaymentPassCategoryID ?? paymentPass.OutgoPaymentPassCategoryID, paymentPass.OldOutgoCFOID??paymentPass.OldOutgoCFOID, paymentPass.OldOutgoPaymentArticleID??paymentPass.OutgoPaymentArticleID, paymentPass.OldCreatedAt??paymentPass.CreatedAt);
                    foreach (var paymentBalance in paymentBalances) // Обновить старые значения
                    {
                        if (paymentPass.Amount != null)
                        {
                            paymentBalance.BalancePlan -= (decimal) paymentPass.Amount;
                            if (paymentPass.IsFact != null && paymentPass.IsFact == true)
                                paymentBalance.BalanceFact -= (decimal) paymentPass.Amount;
                            dataManager.PaymentBalance.Update(paymentBalance);
                        }
                    }
                    paymentPass.OldAmount = null;
                    paymentPass.OldCreatedAt = null;
                    paymentPass.OldOutgoCFOID = null;
                    paymentPass.OldOutgoPaymentArticleID = null;
                    paymentPass.OldOutgoPaymentPassCategoryID = null;
                    paymentPass.ProcessedByCron = true;
                    dataManager.PaymentPass.Update(paymentPass);
                    continue;
                }
                #endregion
            }
        }
    }
}