using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls.Payment
{
    public partial class EditPaymentPassRule : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? PaymentPassRuleId
        {
            get { return (Guid?)ViewState["PaymentPassRuleId"]; }
            set { ViewState["PaymentPassRuleId"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<PaymentPassRuleCompany.PaymentPassRuleCompanyMap> PaymentPassRuleCompanies
        {
            get
            {
                if (ViewState["PaymentPassRuleCompanies"] == null) ViewState["PaymentPassRuleCompanies"] = new List<PaymentPassRuleCompany.PaymentPassRuleCompanyMap>();
                return (List<PaymentPassRuleCompany.PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"];
            }
            set { ViewState["PaymentPassRuleCompanies"] = value; }

        }

        /// <summary>
        /// Gets the service level.
        /// </summary>
        protected tbl_PaymentPassRule PaymentPassRule
        {
            get
            {
                if (ViewState["PaymentPassRule"] == null) ViewState["PaymentPassRule"] = new tbl_PaymentPassRule();
                return (tbl_PaymentPassRule) ViewState["PaymentPassRule"];
            }
            set { ViewState["PaymentPassRule"] = value; }
        }
        



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void BindData()
        {
            ddlPaymentType.Items.Clear();
            foreach (var mode in EnumHelper.EnumToList<PaymentType>())
                ddlPaymentType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(mode), ((int)mode).ToString()));

            if (PaymentPassRuleId != null)
            {
                PaymentPassRule = DataManager.PaymentPassRule.SelectById(CurrentUser.Instance.SiteID, (Guid)PaymentPassRuleId);
                txtTitle.Text = PaymentPassRule.Title;
                chxIsActive.Checked = PaymentPassRule.IsActive ?? false;
                chxIsAutomatic.Checked = PaymentPassRule.IsAutomatic ?? false;
                ucPaymentPass.PaymentPasses = PaymentPassRule.tbl_PaymentPassRulePass.Select(op => new PaymentPass.PaymentPassMap()
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
                    Value = op.Value
                }).ToList();

                ucPaymentPassRuleCompany.PaymentPassRuleCompanies =
                    PaymentPassRule.tbl_PaymentPassRuleCompany.Select(op => new PaymentPassRuleCompany.PaymentPassRuleCompanyMap()
                                                                                {
                                                                                    ID = op.ID,
                                                                                    PayerID = op.PayerID,
                                                                                    Payer = op.tbl_Company != null ? op.tbl_Company.Name : "",
                                                                                    PayerLegalAccountID = op.PayerLegalAccountID,
                                                                                    PayerLegalAccount = op.tbl_CompanyLegalAccount != null ? op.tbl_CompanyLegalAccount.Title : "",
                                                                                    RecipientID = op.RecipientID,
                                                                                    Recipient = op.tbl_Company1 != null ? op.tbl_Company1.Name : "",
                                                                                    RecipientLegalAccountID = op.RecipientLegalAccountID,
                                                                                    RecipientLegalAccount = op.tbl_CompanyLegalAccount1 != null ? op.tbl_CompanyLegalAccount1.Title : ""

                                                                                }).ToList();

            }
            else
            {
                txtTitle.Text = "";
                chxIsActive.Checked =  false;
                chxIsAutomatic.Checked = false;
                ucPaymentPass.PaymentPasses = new List<PaymentPass.PaymentPassMap>();
                ucPaymentPassRuleCompany.PaymentPassRuleCompanies = new List<PaymentPassRuleCompany.PaymentPassRuleCompanyMap>();
                if (PaymentPassRuleCompanies != null)
                {
                    ucPaymentPassRuleCompany.PaymentPassRuleCompanies = PaymentPassRuleCompanies;
                }
            }
            ucPaymentPassRuleCompany.BindData();
            ucPaymentPass.BindData();
        }


        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {        
            var paymentPassRule = DataManager.PaymentPassRule.SelectById(CurrentUser.Instance.SiteID, PaymentPassRule.ID);

            if (paymentPassRule != null)
            {                
                paymentPassRule.Title = txtTitle.Text;
                paymentPassRule.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
                paymentPassRule.IsActive = chxIsActive.Checked;
                paymentPassRule.IsAutomatic = chxIsAutomatic.Checked;
                DataManager.PaymentPassRule.Update(paymentPassRule);
            }
            else
            {
                paymentPassRule = new tbl_PaymentPassRule
                {
                    ID = Guid.NewGuid(),
                    SiteID = CurrentUser.Instance.SiteID,
                    Title = txtTitle.Text,
                    PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue),
                    IsActive = chxIsActive.Checked,
                    IsAutomatic = chxIsAutomatic.Checked
                };

                DataManager.PaymentPassRule.Add(paymentPassRule);
            }


            #region Save Payment Pass Rule Pass
            var oldPaymentPasses = DataManager.PaymentPassRulePass.SelectAll(CurrentUser.Instance.SiteID, paymentPassRule.ID);
            var ids = new List<tbl_PaymentPassRulePass>();
            foreach (var old in oldPaymentPasses)
            {
                if (ucPaymentPass.PaymentPasses.Where(a => a.ID == old.ID).Select(a => a.ID).SingleOrDefault() == Guid.Empty)
                {
                    ids.Add(old);
                }
            }
            foreach (var tblPaymentPass in ids)
            {
                DataManager.PaymentPassRulePass.Delete(tblPaymentPass);
            }
            foreach (var paymentPass in ucPaymentPass.PaymentPasses)
            {
                var oldPaymentPass = DataManager.PaymentPassRulePass.SelectById(paymentPassRule.ID, paymentPass.ID);
                if (oldPaymentPass != null)
                {
                    oldPaymentPass.SiteID = CurrentUser.Instance.SiteID;
                    oldPaymentPass.PaymentPassRuleID = paymentPassRule.ID;
                    oldPaymentPass.IncomePaymentPassCategoryID = paymentPass.IncomePaymentPassCategoryID;
                    oldPaymentPass.IncomeCFOID = paymentPass.IncomeCFOID;
                    oldPaymentPass.IncomePaymentArticleID = paymentPass.IncomePaymentArticleID;

                    oldPaymentPass.OutgoPaymentPassCategoryID = paymentPass.OutgoPaymentPassCategoryID;
                    oldPaymentPass.OutgoCFOID = paymentPass.OutgoCFOID;
                    oldPaymentPass.OutgoPaymentArticleID = paymentPass.OutgoPaymentArticleID;

                    oldPaymentPass.FormulaID = paymentPass.FormulaID;
                    oldPaymentPass.Value = paymentPass.Value;
                    DataManager.PaymentPassRulePass.Update(oldPaymentPass);
                }
                else
                {
                    DataManager.PaymentPassRulePass.Add(new tbl_PaymentPassRulePass() { SiteID = CurrentUser.Instance.SiteID, PaymentPassRuleID = paymentPassRule.ID, IncomePaymentPassCategoryID = paymentPass.IncomePaymentPassCategoryID, IncomeCFOID = paymentPass.IncomeCFOID, IncomePaymentArticleID = paymentPass.IncomePaymentArticleID, OutgoPaymentPassCategoryID = paymentPass.OutgoPaymentPassCategoryID, OutgoCFOID = paymentPass.OutgoCFOID, OutgoPaymentArticleID = paymentPass.OutgoPaymentArticleID, FormulaID = paymentPass.FormulaID, Value = paymentPass.Value});
                }
            }
            #endregion


            #region Save Payment Pass Rule Company
            var oldPaymentPassRuleCompanies = DataManager.PaymentPassRuleCompany.SelectAll(CurrentUser.Instance.SiteID, paymentPassRule.ID);
            var ids2 = new List<tbl_PaymentPassRuleCompany>();
            foreach (var old in oldPaymentPassRuleCompanies)
            {
                if (ucPaymentPassRuleCompany.PaymentPassRuleCompanies.Where(a => a.ID == old.ID).Select(a => a.ID).SingleOrDefault() == Guid.Empty)
                {
                    ids2.Add(old);
                }
            }
            foreach (var tblPaymentPass in ids2)
            {
                DataManager.PaymentPassRuleCompany.Delete(tblPaymentPass);
            }
            foreach (var paymentPass in ucPaymentPassRuleCompany.PaymentPassRuleCompanies)
            {
                var oldPaymentPass = DataManager.PaymentPassRuleCompany.SelectById(paymentPassRule.ID, paymentPass.ID);
                if (oldPaymentPass != null)
                {
                    oldPaymentPass.SiteID = CurrentUser.Instance.SiteID;
                    oldPaymentPass.PaymentPassRuleID = paymentPassRule.ID;
                    oldPaymentPass.PayerID = paymentPass.PayerID;
                    oldPaymentPass.PayerLegalAccountID = paymentPass.PayerLegalAccountID;
                    oldPaymentPass.RecipientID = paymentPass.RecipientID;

                    oldPaymentPass.RecipientLegalAccountID = paymentPass.RecipientLegalAccountID;
                    DataManager.PaymentPassRuleCompany.Update(oldPaymentPass);
                }
                else
                {
                    DataManager.PaymentPassRuleCompany.Add(new tbl_PaymentPassRuleCompany() { SiteID = CurrentUser.Instance.SiteID, PaymentPassRuleID = paymentPassRule.ID, PayerID = paymentPass.PayerID, PayerLegalAccountID = paymentPass.PayerLegalAccountID, RecipientID = paymentPass.RecipientID, RecipientLegalAccountID = paymentPass.RecipientLegalAccountID });
                }
            }
            #endregion
        }

    }
}