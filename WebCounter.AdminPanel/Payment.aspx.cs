using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using Labitec.UI.BaseWorkspace;
using Labitec.UI.Dictionary;
using Labitec.UI.Photo;
using Labitec.UI.Photo.Controls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.AdminPanel.UserControls.Payment;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Payment : LeadForceBasePage
    {
        public Guid _paymentID;
        protected tbl_Payment paymentData = null;

        

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Карточка оплаты - LeadForce";
            
            if (Page.RouteData.Values["id"] != null)
                _paymentID = Guid.Parse(Page.RouteData.Values["id"] as string);


            hlCancel.NavigateUrl = UrlsData.AP_Payments();

//            ucPaymentStatus.PaymentStatusChanged += ucPaymentStatus_PaymentStatusChanged;
            ucPaymentStatus.PaymentId = _paymentID;
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            BindPaymentType();
            paymentData = DataManager.Payment.SelectById(SiteId, _paymentID);
            dcbOrder.SiteID = dcbInvoice.SiteID = dcbPayer.SiteID = dcbRecipient.SiteID = dcbPayerLegalAccount.SiteID = dcbRecipientLegalAccount.SiteID = SiteId;
            var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn()
            {
                Name = "IsActive",
                DbType = DbType.Boolean,
                Value = "TRUE"
            };

            dcbPayerLegalAccount.Filters.Add(filter);
            dcbRecipientLegalAccount.Filters.Add(filter);

            dcbPayer.BindData();
            dcbRecipient.BindData();
            dcbPayerLegalAccount.BindData();
            dcbRecipientLegalAccount.BindData();

            BindPaymentPassRule(0, null, null, null, null);
            BindSelectPaymentPassRule(0);
            if (paymentData != null)
            {

                txtAssignment.Text = paymentData.Assignment;
                rdpDatePlan.SelectedDate = paymentData.DatePlan;
                rdpDateFact.SelectedDate = paymentData.DateFact;
                
                if (paymentData.PaymentTypeID.HasValue)
                {
                    ddlPaymentType.SelectedIndex = ddlPaymentType.FindItemIndexByValue(((int)paymentData.PaymentTypeID).ToString());                    
                }

                ucPaymentStatus.PaymentStatusId = paymentData.StatusID ?? Guid.Empty;

                if (paymentData.PayerID.HasValue)
                {
                    dcbPayer.SelectedId = (Guid)paymentData.PayerID;
                    dcbPayer.SelectedText = DataManager.Company.SelectById(SiteId, (Guid)paymentData.PayerID).Name;
                    dcbPayerLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = paymentData.PayerID.ToString() });
                }

                if (paymentData.RecipientID.HasValue)
                {
                    dcbRecipient.SelectedId = (Guid)paymentData.RecipientID;
                    dcbRecipient.SelectedText = DataManager.Company.SelectById(SiteId, (Guid)paymentData.RecipientID).Name;
                    dcbRecipientLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = paymentData.RecipientID.ToString() });
                }

                if (paymentData.PayerLegalAccountID.HasValue)
                {
                    dcbPayerLegalAccount.SelectedId = (Guid)paymentData.PayerLegalAccountID;
                    dcbPayerLegalAccount.SelectedText = DataManager.CompanyLegalAccount.SelectById((Guid)paymentData.PayerLegalAccountID).Title;
                }

                if (paymentData.RecipientLegalAccountID.HasValue)
                {
                    dcbRecipientLegalAccount.SelectedId = (Guid)paymentData.RecipientLegalAccountID;
                    dcbRecipientLegalAccount.SelectedText = DataManager.CompanyLegalAccount.SelectById((Guid)paymentData.RecipientLegalAccountID).Title;
                }
                if (paymentData.OrderID.HasValue)
                {
                    dcbOrder.SelectedId = (Guid) paymentData.OrderID;
                    dcbOrder.SelectedText = DataManager.Order.SelectById(SiteId,(Guid)paymentData.OrderID).Number;
                }
                if (paymentData.InvoiceID.HasValue)
                {
                    dcbInvoice.SelectedId = (Guid)paymentData.InvoiceID;
                    dcbInvoice.SelectedText = DataManager.Invoice.SelectById(SiteId, (Guid)paymentData.InvoiceID).Number;
                }


                if (paymentData.PaymentTypeID.HasValue)
                {
                    switch (paymentData.PaymentTypeID)
                    {
                        case (int)PaymentType.Income:
                            dcbRecipient.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                            break;
                        case (int)PaymentType.Outgo:
                            dcbPayer.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                            break;
                        case (int)PaymentType.Transfer:
                            dcbRecipient.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                            dcbPayer.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                            break;
                    }
                    BindSelectPaymentPassRule((int)paymentData.PaymentTypeID);
                }
                if(paymentData.PaymentPassRuleID.HasValue)
                {
                    rcbPaymentPassRule.SelectedIndex = rcbPaymentPassRule.FindItemIndexByValue(paymentData.PaymentPassRuleID.ToString());
                }

                dcbCurrency.SelectedId = paymentData.CurrencyID;
                rntxtCourse.Value = paymentData.Course;
                rntxtAmount.Value = (double)paymentData.Amount;
                rntxtTotal.Value = (double)paymentData.Total;

                BindPaymentPassRule((int) paymentData.PaymentTypeID, paymentData.PayerID,
                                    paymentData.PayerLegalAccountID, paymentData.RecipientID,
                                    paymentData.RecipientLegalAccountID);

                if (paymentData.tbl_PaymentPass!=null)

                ucPaymentPass.PaymentPasses = paymentData.tbl_PaymentPass.Select(op => new PaymentPass.PaymentPassMap()
                                                                                           {
                                                                                            ID = op.ID,
                                                                                            OutgoPaymentPassCategoryID = op.OutgoPaymentPassCategoryID,
                                                                                            OutgoPaymentPassCategory = op.tbl_PaymentPassCategory !=null ? op.tbl_PaymentPassCategory.Title :"",
                                                                                            OutgoCFOID = op.OutgoCFOID,
                                                                                            OutgoCFO = op.tbl_PaymentCFO!=null ? op.tbl_PaymentCFO.Title:"",
                                                                                            OutgoPaymentArticleID = op.OutgoPaymentArticleID,
                                                                                            OutgoPaymentArticle = op.tbl_PaymentArticle!=null ? op.tbl_PaymentArticle.Title:"",
                                                                                            IncomePaymentPassCategoryID = op.IncomePaymentPassCategoryID,
                                                                                            IncomePaymentPassCategory = op.tbl_PaymentPassCategory1!=null ? op.tbl_PaymentPassCategory1.Title:"",
                                                                                            IncomeCFOID = op.IncomeCFOID,
                                                                                            IncomeCFO = op.tbl_PaymentCFO1!=null ? op.tbl_PaymentCFO1.Title:"",
                                                                                            IncomePaymentArticleID = op.IncomePaymentArticleID,
                                                                                            IncomePaymentArticle = op.tbl_PaymentArticle1!=null ? op.tbl_PaymentArticle1.Title:"",
                                                                                            FormulaID = op.FormulaID,
                                                                                            Amount = op.Amount,
                                                                                            Value = op.Value
                                                                                           }).ToList();
                ucPaymentPass.Total = double.Parse(paymentData.Amount.ToString());

            } else
            {
                ucPaymentPass.PaymentPasses = new List<PaymentPass.PaymentPassMap>();
                ucPaymentPass.Total = 0;
            }
        }



        private void BindPaymentPassRule(int paymentType, Guid? payerId, Guid? payerLegalAccountId, Guid? recipientId, Guid? recipientLegalAccountId)
        {
            var oldValue = rcbPaymentPassRule.SelectedValue;
            rcbPaymentPassRule.DataSource = DataManager.PaymentPassRule.SelectAll(SiteId, paymentType, payerId, payerLegalAccountId, recipientId, recipientLegalAccountId);
            rcbPaymentPassRule.DataValueField = "ID";
            rcbPaymentPassRule.DataTextField = "Title";
            rcbPaymentPassRule.DataBind();
            rcbPaymentPassRule.Items.Insert(0, new RadComboBoxItem("Выберите правило", Guid.Empty.ToString()));
            rcbPaymentPassRule.SelectedIndex = rcbPaymentPassRule.FindItemIndexByValue(oldValue);

        }
        private void BindSelectPaymentPassRule(int paymentType)
        {
            rcbSelectPaymentPassRule.DataSource = DataManager.PaymentPassRule.SelectAll(SiteId, paymentType, null, null, null, null);
            rcbSelectPaymentPassRule.DataValueField = "ID";
            rcbSelectPaymentPassRule.DataTextField = "Title";
            rcbSelectPaymentPassRule.DataBind();
            rcbSelectPaymentPassRule.Items.Insert(0, new RadComboBoxItem("Выберите правило", Guid.Empty.ToString()));

        }


        private void BindPaymentType()
        {
            ddlPaymentType.Items.Clear();

            foreach (var mode in EnumHelper.EnumToList<PaymentType>())
                ddlPaymentType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(mode), ((int)mode).ToString()));
        }






        



        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (DataManager.PaymentStatus.SelectById(CurrentUser.Instance.SiteID, ucPaymentStatus.PaymentStatusId).IsLast)
            {
                double? total = 0.0;
                bool isValid = true;
                if (ucPaymentPass.PaymentPasses != null && ucPaymentPass.PaymentPasses.Count > 0)
                {
                    foreach (var paymentPass in ucPaymentPass.PaymentPasses)
                    {
                        if (paymentPass.IncomeCFOID == null || paymentPass.IncomePaymentArticleID == null ||
                            paymentPass.OutgoCFOID == null || paymentPass.OutgoPaymentArticleID == null)
                        {
                            isValid = false;
                            cvGroupUpdate.ErrorMessage = "Не указан ЦФО или статья";
                            break;
                        }
                        total += paymentPass.Amount;
                    }
                    if (total != rntxtTotal.Value)
                    {
                        isValid = false;
                        cvGroupUpdate.ErrorMessage = "Сумма проводок не равна сумме оплаты";
                    }
                } else
                {
                    isValid = false;
                    cvGroupUpdate.ErrorMessage = "Не указаны проводки";  
                }
                cvGroupUpdate.IsValid = isValid;
            }
            if (cvGroupUpdate.IsValid)
            {
                paymentData = DataManager.Payment.SelectById(SiteId, _paymentID);

                var isUpdate = paymentData != null;

                paymentData = DataManager.Payment.Save(
                    SiteId,
                    paymentData == null ? Guid.Empty : paymentData.ID,
                    txtAssignment.Text, rdpDatePlan.SelectedDate ?? DateTime.Now, rdpDateFact.SelectedDate,
                    int.Parse(ddlPaymentType.SelectedValue),
                    ucPaymentStatus.PaymentStatusId, dcbPayer.SelectedIdNullable,
                    dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable,
                    dcbRecipientLegalAccount.SelectedIdNullable, dcbCurrency.SelectedId, rntxtCourse.Value ?? 1,
                    rntxtAmount.Value.HasValue ? decimal.Parse(rntxtAmount.Value.ToString()) : 0,
                    rntxtTotal.Value.HasValue ? decimal.Parse(rntxtTotal.Value.ToString()) : 0,
                    dcbOrder.SelectedIdNullable, dcbInvoice.SelectedIdNullable

                    );



                var oldPaymentPasses = DataManager.PaymentPass.SelectAll(CurrentUser.Instance.SiteID, paymentData.ID);
                List<tbl_PaymentPass> ids = new List<tbl_PaymentPass>();
                foreach (var old in oldPaymentPasses)
                {
                    if (ucPaymentPass.PaymentPasses.Where(a => a.ID == old.ID).Select(a => a.ID).SingleOrDefault() ==
                        Guid.Empty)
                    {
                        ids.Add(old);
                    }
                }
                foreach (var tblPaymentPass in ids)
                {
                    if (tblPaymentPass.ProcessedByCron)
                    {
                        tblPaymentPass.ProcessedByCron = false;
                        tblPaymentPass.ToDelete = true;
                        DataManager.PaymentPass.Update(tblPaymentPass);

                        //tblPaymentPass.OldCreatedAt = tblPaymentPass.CreatedAt;
                        //tblPaymentPass.OldAmount = tblPaymentPass.Amount;
                        //tblPaymentPass.OldOutgoCFOID = tblPaymentPass.OldOutgoCFOID;
                        //tblPaymentPass.OldOutgoPaymentArticleID = tblPaymentPass.OutgoPaymentArticleID;
                    } else {
                            DataManager.PaymentPass.Delete(tblPaymentPass);
                    }
                }
                foreach (var paymentPass in ucPaymentPass.PaymentPasses)
                {
                    var oldPaymentPass = DataManager.PaymentPass.SelectById(paymentData.ID, paymentPass.ID);
                    if (oldPaymentPass != null)
                    {
                        oldPaymentPass.SiteID = CurrentUser.Instance.SiteID;
                        oldPaymentPass.PaymentID = paymentData.ID;
                        oldPaymentPass.IncomePaymentPassCategoryID = paymentPass.IncomePaymentPassCategoryID;
                        oldPaymentPass.IncomeCFOID = paymentPass.IncomeCFOID;
                        oldPaymentPass.IncomePaymentArticleID = paymentPass.IncomePaymentArticleID;
                        oldPaymentPass.OutgoPaymentPassCategoryID = paymentPass.OutgoPaymentPassCategoryID;

                        if (oldPaymentPass.OutgoCFOID != paymentPass.OutgoCFOID)
                        {
                            oldPaymentPass.OldOutgoCFOID = oldPaymentPass.OutgoCFOID;
                            oldPaymentPass.ProcessedByCron = false;
                        }
                        if (oldPaymentPass.OutgoPaymentArticleID != paymentPass.OutgoPaymentArticleID)
                        {
                            oldPaymentPass.OldOutgoPaymentArticleID = oldPaymentPass.OldOutgoPaymentArticleID;
                            oldPaymentPass.ProcessedByCron = false;
                        }
                        if (rdpDateFact.SelectedDate != null && oldPaymentPass.CreatedAt != rdpDateFact.SelectedDate || rdpDateFact.SelectedDate == null && rdpDatePlan.SelectedDate != null && oldPaymentPass.CreatedAt != rdpDatePlan.SelectedDate)
                        {
                            oldPaymentPass.OldCreatedAt = oldPaymentPass.CreatedAt;
                            oldPaymentPass.ProcessedByCron = false;
                        }
                        if (oldPaymentPass.Amount != paymentPass.Amount)
                        {
                            oldPaymentPass.Amount = oldPaymentPass.Amount;
                            oldPaymentPass.ProcessedByCron = false;
                        }

                        oldPaymentPass.CreatedAt = rdpDateFact.SelectedDate ?? rdpDatePlan.SelectedDate;
                        oldPaymentPass.OutgoCFOID = paymentPass.OutgoCFOID;
                        oldPaymentPass.OutgoPaymentArticleID = paymentPass.OutgoPaymentArticleID;
                        
                        oldPaymentPass.FormulaID = paymentPass.FormulaID;
                        oldPaymentPass.Value = paymentPass.Value;
                        oldPaymentPass.Amount = paymentPass.Amount;
                        if (DataManager.PaymentStatus.SelectById(CurrentUser.Instance.SiteID, ucPaymentStatus.PaymentStatusId).IsLast)
                        {
                            oldPaymentPass.IsFact = true;
                        }
                        DataManager.PaymentPass.Update(oldPaymentPass);
                    }
                    else
                    {
                        DataManager.PaymentPass.Add(new tbl_PaymentPass()
                                                        {
                                                            SiteID = CurrentUser.Instance.SiteID,
                                                            PaymentID = paymentData.ID,
                                                            CreatedAt = rdpDateFact.SelectedDate ?? rdpDatePlan.SelectedDate,
                                                            IncomePaymentPassCategoryID =
                                                                paymentPass.IncomePaymentPassCategoryID,
                                                            IncomeCFOID = paymentPass.IncomeCFOID,
                                                            IncomePaymentArticleID = paymentPass.IncomePaymentArticleID,
                                                            OutgoPaymentPassCategoryID =
                                                                paymentPass.OutgoPaymentPassCategoryID,
                                                            OutgoCFOID = paymentPass.OutgoCFOID,
                                                            OutgoPaymentArticleID = paymentPass.OutgoPaymentArticleID,
                                                            FormulaID = paymentPass.FormulaID,
                                                            Value = paymentPass.Value,
                                                            Amount = paymentPass.Amount
                                                        });
                    }
                }


                if (!isUpdate)
                    Response.Redirect(UrlsData.AP_PaymentsEdit(paymentData.ID));
            }
        }



        protected void ddlPaymentType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (int.Parse(ddlPaymentType.SelectedValue))
            {
                case (int)PaymentType.Income:
                    dcbPayer.Filters.Clear();
                    //dcbPayer.BindData();

                    dcbRecipient.SelectedId = Guid.Empty;
                    dcbRecipient.SelectedText = string.Empty;
                    dcbRecipient.Filters.Clear();
                    dcbRecipient.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                    dcbRecipient.BindData();

                                    
                    dcbRecipientLegalAccount.SelectedId = Guid.Empty;
                    dcbRecipientLegalAccount.SelectedText = string.Empty;
                    dcbRecipientLegalAccount.Filters.Clear();
                    dcbRecipientLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                    {
                        Name = "IsActive",
                        DbType = DbType.Boolean,
                        Value = "TRUE"
                    });
                    dcbRecipientLegalAccount.BindData();
                    break;
                case (int)PaymentType.Outgo:
                    dcbRecipient.Filters.Clear();
                    //dcbRecipient.BindData();

                    dcbPayer.SelectedId = Guid.Empty;
                    dcbPayer.SelectedText = string.Empty;
                    dcbPayer.Filters.Clear();
                    dcbPayer.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                    dcbPayer.BindData();


                    dcbPayerLegalAccount.SelectedId = Guid.Empty;
                    dcbPayerLegalAccount.SelectedText = string.Empty;
                    dcbPayerLegalAccount.Filters.Clear();
                    dcbPayerLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                    {
                        Name = "IsActive",
                        DbType = DbType.Boolean,
                        Value = "TRUE"
                    });
                    dcbPayerLegalAccount.BindData();
                    break;
                case (int)PaymentType.Transfer:
                    dcbRecipient.SelectedId = Guid.Empty;
                    dcbRecipient.SelectedText = string.Empty;
                    dcbRecipient.Filters.Clear();
                    dcbRecipient.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                    dcbRecipient.BindData();


                    dcbRecipientLegalAccount.SelectedId = Guid.Empty;
                    dcbRecipientLegalAccount.SelectedText = string.Empty;
                    dcbRecipientLegalAccount.Filters.Clear();
                    dcbRecipientLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                    {
                        Name = "IsActive",
                        DbType = DbType.Boolean,
                        Value = "TRUE"
                    });
                    dcbRecipientLegalAccount.BindData();

                    dcbPayer.SelectedId = Guid.Empty;
                    dcbPayer.SelectedText = string.Empty;
                    dcbPayer.Filters.Clear();
                    dcbPayer.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyTypeID", DbType = DbType.Guid, Value = DataManager.CompanyType.SelectOurCompanyTypeID(SiteId).ToString() });
                    dcbPayer.BindData();


                    dcbPayerLegalAccount.SelectedId = Guid.Empty;
                    dcbPayerLegalAccount.SelectedText = string.Empty;
                    dcbPayerLegalAccount.Filters.Clear();
                    dcbPayerLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                    {
                        Name = "IsActive",
                        DbType = DbType.Boolean,
                        Value = "TRUE"
                    });
                    dcbPayerLegalAccount.BindData();
                    break;

            }

        }

        protected void dcbPayer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbPayerLegalAccount.SelectedId = Guid.Empty;
            dcbPayerLegalAccount.SelectedText = string.Empty;
            dcbPayerLegalAccount.Filters.Clear();
            dcbPayerLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
            {
                Name = "IsActive",
                DbType = DbType.Boolean,
                Value = "TRUE"
            });
            dcbPayerLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = e.Value });
            dcbPayerLegalAccount.BindData();
            BindPaymentPassRule(int.Parse(ddlPaymentType.SelectedValue), dcbPayer.SelectedIdNullable, dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable, dcbRecipientLegalAccount.SelectedIdNullable);
        }


        protected void dcbPayerLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPaymentPassRule(int.Parse(ddlPaymentType.SelectedValue), dcbPayer.SelectedIdNullable, dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable, dcbRecipientLegalAccount.SelectedIdNullable);
        }


        protected void dcbRecipient_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbRecipientLegalAccount.SelectedId = Guid.Empty;
            dcbRecipientLegalAccount.SelectedText = string.Empty;
            dcbRecipientLegalAccount.Filters.Clear();
            dcbRecipientLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
            {
                Name = "IsActive",
                DbType = DbType.Boolean,
                Value = "TRUE"
            });
            dcbRecipientLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = e.Value });
            dcbRecipientLegalAccount.BindData();
            BindPaymentPassRule(int.Parse(ddlPaymentType.SelectedValue), dcbPayer.SelectedIdNullable, dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable, dcbRecipientLegalAccount.SelectedIdNullable);

        }


        protected void dcbRecipientLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindPaymentPassRule(int.Parse(ddlPaymentType.SelectedValue), dcbPayer.SelectedIdNullable, dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable, dcbRecipientLegalAccount.SelectedIdNullable);

        }

        protected void rntxtCourse_OnTextChanged(object sender, EventArgs e)
        {
            rntxtTotal.Value = rntxtAmount.Value * rntxtCourse.Value;
            double a = 0;
            Double.TryParse(rntxtTotal.Value.ToString(), out a);
            ucPaymentPass.Total = a;
        }

        protected void rntxtAmount_OnTextChanged(object sender, EventArgs e)
        {
            rntxtTotal.Value = rntxtAmount.Value * rntxtCourse.Value;
            double a = 0;
            Double.TryParse(rntxtTotal.Value.ToString(), out a);
            ucPaymentPass.Total = a;
        }

        protected void dcbOrder_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
        }

        protected void rcbPaymentPassRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var paymentPassesByRulePass = DataManager.PaymentPassRulePass.SelectByRuleId(CurrentUser.Instance.SiteID,Guid.Parse(rcbPaymentPassRule.SelectedValue));
            var paymentPassesByRulePassLis = new List<PaymentPass.PaymentPassMap>();
            foreach (var op in paymentPassesByRulePass)
            {
                double? Amount = 0;
                switch (op.FormulaID)
                {
                    case (int)PaymentPassFormula.Percent:
                        Amount = rntxtTotal.Value * op.Value / 100;
                        break;
                    case (int)PaymentPassFormula.FixedSum:
                        Amount = op.Value;
                        break;                        
                }

                paymentPassesByRulePassLis.Add(new PaymentPass.PaymentPassMap()
                                                   {
                                                       ID = op.ID,
                                                       OutgoPaymentPassCategoryID = op.OutgoPaymentPassCategoryID,
                                                       OutgoPaymentPassCategory = op.tbl_PaymentPassCategory != null ? op.tbl_PaymentPassCategory.Title : "",
                                                       OutgoCFOID = op.OutgoCFOID,
                                                       OutgoCFO = op.tbl_PaymentCFO != null ? op.tbl_PaymentCFO.Title : "",
                                                       OutgoPaymentArticleID = op.OutgoPaymentArticleID,
                                                       OutgoPaymentArticle = op.tbl_PaymentArticle != null ? op.tbl_PaymentArticle.Title : "",
                                                       IncomePaymentPassCategoryID = op.IncomePaymentPassCategoryID,
                                                       IncomePaymentPassCategory = op.tbl_PaymentPassCategory1 != null ? op.tbl_PaymentPassCategory1.Title : "",
                                                       IncomeCFOID = op.IncomeCFOID,
                                                       IncomeCFO = op.tbl_PaymentCFO1 != null ? op.tbl_PaymentCFO1.Title : "",
                                                       IncomePaymentArticleID = op.IncomePaymentArticleID,
                                                       IncomePaymentArticle = op.tbl_PaymentArticle1 != null ? op.tbl_PaymentArticle1.Title : "",
                                                       FormulaID = op.FormulaID,
                                                       Amount = Amount,
                                                       Value = op.Value                                                       
                                                   });
            }
            ucPaymentPass.PaymentPasses = paymentPassesByRulePassLis;
            ucPaymentPass.BindData();
        }

        protected void lbtnSelectPaymentPassRule_OnClick(object sender, EventArgs e)
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("ShowPaymentPassRuleRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "ShowPaymentPassRuleRadWindow", "ShowPaymentPassRuleRadWindow();", true);
        }

        protected void lbtnIncludeInRule_OnClick(object sender, EventArgs e)
        {
            if (rcbSelectPaymentPassRule.SelectedValue !=Guid.Empty.ToString())
            {
                var paymentPassRuleCompany = new tbl_PaymentPassRuleCompany();
                paymentPassRuleCompany.SiteID = CurrentUser.Instance.SiteID;
                paymentPassRuleCompany.PaymentPassRuleID = Guid.Parse(rcbSelectPaymentPassRule.SelectedValue);
                paymentPassRuleCompany.PayerID = dcbPayer.SelectedIdNullable;
                paymentPassRuleCompany.PayerLegalAccountID = dcbPayerLegalAccount.SelectedIdNullable;
                paymentPassRuleCompany.RecipientID = dcbRecipient.SelectedIdNullable;
                paymentPassRuleCompany.RecipientLegalAccountID = dcbRecipientLegalAccount.SelectedIdNullable;
                DataManager.PaymentPassRuleCompany.Add(paymentPassRuleCompany);
                BindPaymentPassRule(int.Parse(ddlPaymentType.SelectedValue), dcbPayer.SelectedIdNullable, dcbPayerLegalAccount.SelectedIdNullable, dcbRecipient.SelectedIdNullable, dcbRecipientLegalAccount.SelectedIdNullable);
            }
        }

        protected void lbtnOpenRule_OnClick(object sender, EventArgs e)
        {
            if (rcbSelectPaymentPassRule.SelectedValue !=Guid.Empty.ToString())
            {
                if (!Page.ClientScript.IsStartupScriptRegistered("ShowEditPaymentPassRuleRadWindow"))
                    ScriptManager.RegisterStartupScript(Page, typeof (System.Web.UI.Page),
                                                        "ShowEditPaymentPassRuleRadWindow",
                                                        "ShowEditPaymentPassRuleRadWindow();", true);
            }
        }

        protected void lbtnAddRule_OnClick(object sender, EventArgs e)
        {
           var PaymentPassRuleCompanies = new List<PaymentPassRuleCompany.PaymentPassRuleCompanyMap>()
                                                                                                  {
                                                                                                      new PaymentPassRuleCompany.PaymentPassRuleCompanyMap()
                                                                                                          {
                                                                                                              ID=Guid.NewGuid(),
                                                                                                              PayerID = dcbPayer.SelectedIdNullable,
                                                                                                              Payer = dcbPayer.SelectedIdNullable!=null ? dcbPayer.SelectedText:"",
                                                                                                              PayerLegalAccountID = dcbPayerLegalAccount.SelectedIdNullable,
                                                                                                              PayerLegalAccount = dcbPayerLegalAccount.SelectedIdNullable!=null?dcbPayerLegalAccount.SelectedText:"",
                                                                                                              RecipientID = dcbRecipient.SelectedIdNullable,
                                                                                                              Recipient = dcbRecipient.SelectedIdNullable != null?dcbRecipient.SelectedText:"",
                                                                                                              RecipientLegalAccount = dcbRecipientLegalAccount.SelectedIdNullable!=null?dcbRecipientLegalAccount.SelectedText:"",
                                                                                                          }
                                                                                                  };
            ucPaymentPassRuleEdit.PaymentPassRuleId = null;
            if (dcbPayer.SelectedIdNullable != null || dcbPayerLegalAccount.SelectedIdNullable != null || dcbRecipient.SelectedIdNullable != null || dcbRecipientLegalAccount.SelectedIdNullable != null)
            {
                ucPaymentPassRuleEdit.PaymentPassRuleCompanies = PaymentPassRuleCompanies;
            }
            ucPaymentPassRuleEdit.BindData();
            if (!Page.ClientScript.IsStartupScriptRegistered("ShowEditPaymentPassRuleRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page),
                                                    "ShowEditPaymentPassRuleRadWindow",
                                                    "ShowEditPaymentPassRuleRadWindow();", true);

        }


        protected void rcbSelectPaymentPassRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcbSelectPaymentPassRule.SelectedValue != Guid.Empty.ToString())
            {
                ucPaymentPassRuleEdit.PaymentPassRuleId = Guid.Parse(rcbSelectPaymentPassRule.SelectedValue);
                ucPaymentPassRuleEdit.BindData();
            }
        }
    }
}