using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Invoice;

namespace Labitec.LeadForce.Portal.Main.InvoiceModule
{
    public partial class Invoice : LeadForcePortalBasePage
    {
        protected RadAjaxManager radAjaxManager = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;

            radAjaxManager = RadAjaxManager.GetCurrent(Page);                      
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnUpdatePostponePaymentDate, lrlPaymentDatePlanned, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnUpdatePostponePaymentDate, ucNotificationMessage, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(lbtnUpdateNotFulfilledLiabilities, ucNotificationMessage, null, UpdatePanelRenderMode.Inline);                        

            var accessCheck = Access.Check(TblUser, "Invoices");
            if (!accessCheck.Read)
                Response.Redirect(UrlsData.LFP_AccessDenied(PortalSettingsId));

            if (!Page.IsPostBack)
                BindData();
        }


        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (ObjectId != Guid.Empty)
            {
                var invoice = DataManager.Invoice.SelectById(SiteId, ObjectId);
                if (invoice != null)
                {
                    CheckReadAccess(invoice.OwnerID, "Invoices");

                    Title =
                        lrlTitle.Text =
                        string.Format("Счет №{0} от {1}", invoice.Number,
                                      invoice.CreatedAt.ToString("dd.MM.yyyy"));

                    lrlInvoiceStatus.Text = EnumHelper.GetEnumDescription((InvoiceStatus) invoice.InvoiceStatusID);
                    lrlInvoiceAmount.Text = invoice.InvoiceAmount.ToString("F");
                    rbtnPrint.NavigateUrl = UrlsData.LFP_InvoicePrint(ObjectId, PortalSettingsId);

                    if (invoice.ExecutorContactID.HasValue)
                        lrlExecutorContact.Text =
                            DataManager.Contact.SelectById(CurrentUser.Instance.SiteID, invoice.ExecutorContactID.Value)
                                .UserFullName;

                    if (invoice.BuyerCompanyLegalAccountID.HasValue)
                    {
                        lbtnBuyerCompanyLegal.Text = DataManager.CompanyLegalAccount.SelectById(invoice.BuyerCompanyLegalAccountID.Value).Title;
                        lbtnBuyerCompanyLegal.CommandArgument = invoice.BuyerCompanyLegalAccountID.Value.ToString();                        
                    }

                    if (invoice.ExecutorCompanyLegalAccountID.HasValue)
                    {
                        lbtnExecutorCompanyLegal.Text = DataManager.CompanyLegalAccount.SelectById(invoice.ExecutorCompanyLegalAccountID.Value).Title;
                        lbtnExecutorCompanyLegal.CommandArgument = invoice.ExecutorCompanyLegalAccountID.Value.ToString();                        
                    }                        

                    if (invoice.PaymentDateActual.HasValue)
                        lrlPaymentDateActual.Text = invoice.PaymentDateActual.Value.ToString("dd.MM.yyyy");

                    if (invoice.PaymentDatePlanned.HasValue)
                        lrlPaymentDatePlanned.Text = invoice.PaymentDatePlanned.Value.ToString("dd.MM.yyyy");

                    lrlNote.Text = invoice.Note;

                    rprInvoiceProducts.DataSource = DataManager.InvoiceProducts.SelectAll(invoice.ID);
                    rprInvoiceProducts.DataBind();

                    lrlInvoiceAmount1.Text = invoice.InvoiceAmount.ToString("F");
                    lrlInvoiceAmount2.Text = invoice.InvoiceAmount.ToString("F");

                    plNote.Visible = invoice.IsPaymentDateFixedByContract;
                }
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnUpdatePostponePaymentDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdatePostponePaymentDate_OnClick(object sender, EventArgs e)
        {
            var invoice = DataManager.Invoice.SelectById(CurrentUser.Instance.SiteID, ObjectId);
            if (invoice != null)
            {
                var commentText =
                    string.Format("Перенесена дата оплаты с {0} на {1}. Причины: {2}", invoice.PaymentDatePlanned.HasValue ? invoice.PaymentDatePlanned.Value.ToString("dd.MM.yyyy") : "[Дата не установлена]",
                    rdpInvoicePlannedPaymentDate.SelectedDate.Value.ToString("dd.MM.yyyy"),
                                  txtCommentForManager.Text);

                ContentCommentRepository.Add(CurrentUser.Instance.SiteID, CurrentUser.Instance.ID, invoice.ID, commentText, null,
                                             null, string.Empty, CommentTables.tbl_InvoiceComment);

                invoice.PaymentDatePlanned = rdpInvoicePlannedPaymentDate.SelectedDate;

                lrlPaymentDatePlanned.Text = rdpInvoicePlannedPaymentDate.SelectedDate.Value.ToString("dd.MM.yyyy");

                DataManager.Invoice.Update(invoice);
            }

            rdpInvoicePlannedPaymentDate.SelectedDate = null;
            txtCommentForManager.Text = string.Empty;

            ucNotificationMessage.Text = "Дата оплаты успешно перенесена.";

            if (!Page.ClientScript.IsStartupScriptRegistered("ClosePostponePaymentDateRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ClosePostponePaymentDateRadWindow",
                                                            "ClosePostponePaymentDateRadWindow();", true);            
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnUpdateNotFulfilledLiabilities control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnUpdateNotFulfilledLiabilities_OnClick(object sender, EventArgs e)
        {
            var invoice = DataManager.Invoice.SelectById(CurrentUser.Instance.SiteID, ObjectId);
            if (invoice != null)
            {
                var commentText =
                    string.Format("Не выполнены обязательства Исполнителя для оплаты счета. Причины: {0}", txtNotFulfilledLiabilities.Text);

                ContentCommentRepository.Add(CurrentUser.Instance.SiteID, CurrentUser.Instance.ID, invoice.ID, commentText, null,
                                             null, string.Empty, CommentTables.tbl_InvoiceComment);

                invoice.IsExistBuyerComplaint = true;

                DataManager.Invoice.Update(invoice);
            }

            txtNotFulfilledLiabilities.Text = string.Empty;

            ucNotificationMessage.Text = "Комментарий успешно добавлен.";

            if (!Page.ClientScript.IsStartupScriptRegistered("CloseNotFulfilledLiabilitiesRadWindow"))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseNotFulfilledLiabilitiesRadWindow",
                                                            "CloseNotFulfilledLiabilitiesRadWindow();", true);
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnCompanyLegal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnCompanyLegal_OnClick(object sender, EventArgs e)
        {
            Guid companyLegalAccountId = Guid.Empty;
            var lbtnSender = (LinkButton) sender;

            if (Guid.TryParse(lbtnSender.CommandArgument, out companyLegalAccountId))
            {
                var companyLegalAccount = DataManager.CompanyLegalAccount.SelectById(CurrentUser.Instance.SiteID, companyLegalAccountId);
                if (companyLegalAccount != null)
                {                    
                    lrlCLAOfficialTitle.Text = companyLegalAccount.OfficialTitle;
                    lrlCLALegalAddress.Text = companyLegalAccount.LegalAddress;
                    lrlCLARegistrationDate.Text = companyLegalAccount.RegistrationDate.HasValue ? companyLegalAccount.RegistrationDate.Value.ToString("dd.MM.yyyy") : string.Empty;
                    lrlCLAOGRN.Text = companyLegalAccount.OGRN;
                    lrlCLAINN.Text = companyLegalAccount.INN;
                    lrlCLAKPP.Text = companyLegalAccount.KPP;                    
                    lrlCLARS.Text = companyLegalAccount.RS;

                    if (companyLegalAccount.BankID.HasValue)
                    {
                        lrlCLABank.Text = companyLegalAccount.tbl_Bank.Title;
                        lrlCLBik.Text = companyLegalAccount.tbl_Bank.BIK;
                        lrlCLKS.Text = companyLegalAccount.tbl_Bank.KS;
                    }
                    else
                    {
                        lrlCLABank.Text = string.Empty;
                        lrlCLBik.Text = string.Empty;
                        lrlCLKS.Text = string.Empty;
                    }                                    

                    if (!Page.ClientScript.IsStartupScriptRegistered("ShowCompanyLegalRadWindow"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ShowCompanyLegalRadWindow",
                                                            "ShowCompanyLegalRadWindow();", true);
                }
            }
        }

        protected void radAjaxPanel_OnAjaxSettingCreated(object sender, AjaxSettingCreatedEventArgs e)
        {
            e.UpdatePanel.UpdateMode = UpdatePanelUpdateMode.Always;            
        }
    }
}