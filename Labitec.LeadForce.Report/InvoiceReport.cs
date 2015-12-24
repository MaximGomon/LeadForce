using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Report
{        
    /// <summary>
    /// Summary description for InvoiceReport.
    /// </summary>
    public partial class InvoiceReport : Telerik.Reporting.Report
    {
        private readonly Guid _invoiceId;
        private readonly Guid _siteId;
        private bool _isStampEnabled;
        private int _rowIndex;
        private decimal _sum;
        private double _NDS = 0.18;


        public InvoiceReport(Guid siteId, Guid invoiceId, bool isStampEnabled)
        {        
            InitializeComponent();

            _siteId = siteId;
            _invoiceId = invoiceId;
            _isStampEnabled = isStampEnabled;

            _rowIndex = 0;
            _sum = 0;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");        
            
        }



        /// <summary>
        /// Handles the NeedDataSource event of the InvoiceReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InvoiceReport_NeedDataSource(object sender, EventArgs e)
        {
            var dataManager = new DataManager();
            var invoice = dataManager.Invoice.SelectById(_siteId, _invoiceId);
            if (invoice != null)
            {
                (sender as Telerik.Reporting.Processing.Report).DocumentName = "Счет #" + invoice.Number;
                (sender as Telerik.Reporting.Processing.Report).DataSource = invoice;

                _sum = invoice.InvoiceAmount;

                htbSchetHeader.Value = String.Format("СЧЕТ № {0} от {1}", invoice.Number, invoice.CreatedAt.ToShortDateString());
                htbTotal.Value = String.Format("Всего наименований {0} на сумму {1}", invoice.tbl_InvoiceProducts.Count, (_sum).ToString("F"));
                htbSumWord.Value = NumByWords.RurPhrase(_sum);

                tbl_CompanyLegalAccount executorLegalAccount = null;

                if (invoice.ExecutorCompanyLegalAccountID.HasValue)
                    executorLegalAccount = dataManager.CompanyLegalAccount.SelectById((Guid)invoice.ExecutorCompanyLegalAccountID);

                if (executorLegalAccount != null)
                {
                    tbTitle.Value = executorLegalAccount.OfficialTitle;
                    tbAdress.Value = executorLegalAccount.LegalAddress;
                    tbAdress2.Value = executorLegalAccount.LegalAddress;

                    tbReciver.Value = executorLegalAccount.OfficialTitle + " ИНН " + executorLegalAccount.INN;                    
                    tbSchet.Value = !string.IsNullOrEmpty(executorLegalAccount.RS) ? executorLegalAccount.RS : "Нет данных";
                    tbExecutorRS.Value = !string.IsNullOrEmpty(executorLegalAccount.RS) ? executorLegalAccount.RS : "Нет данных";
                    tbExecutorKPP.Value = !string.IsNullOrEmpty(executorLegalAccount.KPP) ? executorLegalAccount.KPP : "Нет данных";
                    tbExecutorINN.Value = !string.IsNullOrEmpty(executorLegalAccount.INN) ? executorLegalAccount.INN : "Нет данных";


                    if (_isStampEnabled)
                    {
                        var fsp = new FileSystemProvider();

                        if (!string.IsNullOrEmpty(executorLegalAccount.HeadSignatureFileName))
                        {
                            string img = fsp.GetPhysicalPath(invoice.SiteID, "CompanyLegalAccounts",
                                                             executorLegalAccount.HeadSignatureFileName, FileType.Attachment);
                            Image myImg = Image.FromFile(img);
                            pbHeadSignature.Value = myImg.Clone();                           
                            myImg.Dispose();
                        }

                        if (!string.IsNullOrEmpty(executorLegalAccount.AccountantSignatureFileName))
                        {
                            string img = fsp.GetPhysicalPath(invoice.SiteID, "CompanyLegalAccounts",
                                                             executorLegalAccount.AccountantSignatureFileName, FileType.Attachment);
                            Image myImg = Image.FromFile(img);
                            pbAccountantSignature.Value = myImg.Clone();                            
                            myImg.Dispose();
                        }

                        if (!string.IsNullOrEmpty(executorLegalAccount.StampFileName))
                        {
                            string img = fsp.GetPhysicalPath(invoice.SiteID, "CompanyLegalAccounts",
                                                             executorLegalAccount.StampFileName, FileType.Attachment);
                            Image myImg = Image.FromFile(img);                            
                            pbStamp.Value = myImg.Clone();
                            myImg.Dispose();
                        }
                    }
                    else
                    {
                        pbHeadSignature.Visible = false;
                        pbAccountantSignature.Visible = false;
                        pbStamp.Visible = false;
                    }

                    if (executorLegalAccount.BankID.HasValue)
                    {
                        var bank = dataManager.Bank.SelectById((Guid) executorLegalAccount.BankID);
                        if (bank != null)
                        {
                            tbReciverBank.Value = bank.Title;
                            tbExecutorBank.Value = bank.Title;
                            if (bank.CityID.HasValue)
                            {
                                tbExecutorBank.Value += " г. " + bank.tbl_City.Name;
                                tbReciverBank.Value += " г. " + bank.tbl_City.Name;
                            }
                            tbBIK.Value = !string.IsNullOrEmpty(bank.BIK) ? bank.BIK : "Нет данных";
                            tbBIK2.Value = tbBIK.Value;
                            tbSchet2.Value = !string.IsNullOrEmpty(bank.KS) ? bank.KS : "Нет данных";
                            tbExecutorKS.Value = tbSchet2.Value;
                        }
                        else
                        {
                            tbReciverBank.Value = tbBIK.Value = tbBIK2.Value = tbSchet2.Value = "Нет данных";
                        }
                    }

                    if (executorLegalAccount.HeadID.HasValue)
                    {
                        var head = dataManager.Contact.SelectById(invoice.SiteID, (Guid)executorLegalAccount.HeadID);
                        if (head != null)
                        {

                            tbHeadFullName.Value = string.Format("/{0} {1}.{2}./", head.Surname,
                                                                 !string.IsNullOrEmpty(head.Name) && head.Name.Length > 1 ? (object)head.Name[0] : "",
                                                                 !string.IsNullOrEmpty(head.Patronymic) && head.Patronymic.Length > 1 ? (object)head.Patronymic[0] : "");
                        }

                        var accountant = dataManager.Contact.SelectById(invoice.SiteID, (Guid)executorLegalAccount.AccountantID);
                        if (accountant != null)
                        {
                            tbAccountantFullName.Value = string.Format("/{0} {1}.{2}./", accountant.Surname,
                                                                 !string.IsNullOrEmpty(accountant.Name) && accountant.Name.Length > 1 ? (object)accountant.Name[0] : "",
                                                                 !string.IsNullOrEmpty(accountant.Patronymic) && accountant.Patronymic.Length > 1 ? (object)accountant.Patronymic[0] : "");
                        }
                    }
                }

                tbl_Company executorCompany = null;

                if (invoice.ExecutorCompanyID.HasValue)
                    executorCompany = dataManager.Company.SelectById(invoice.SiteID, (Guid) invoice.ExecutorCompanyID);

                if (executorCompany != null)
                {
                    tbPhone.Value = executorCompany.Phone1;
                    //tbFax.Value = executorCompany.Fax;
                }

                tbl_CompanyLegalAccount buyerLegalAccount = null;

                if (invoice.BuyerCompanyLegalAccountID.HasValue)
                    buyerLegalAccount = dataManager.CompanyLegalAccount.SelectById((Guid)invoice.BuyerCompanyLegalAccountID);

                if (buyerLegalAccount != null)
                {
                    tbBuyerTitle.Value = buyerLegalAccount.OfficialTitle;
                    tbBuyerOfficialTitle.Value = buyerLegalAccount.OfficialTitle;                    
                }               
            }
        }



        /// <summary>
        /// Handles the ItemDataBound event of the textBox10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBox10_ItemDataBound(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox procTextbox = (Telerik.Reporting.Processing.TextBox)sender;
            procTextbox.Value = ++_rowIndex;

        }



        /// <summary>
        /// Handles the ItemDataBound event of the textBox20 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBox20_ItemDataBound(object sender, EventArgs e)
        {
            (sender as Telerik.Reporting.Processing.TextBox).Value = _sum;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the textBox21 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBox21_ItemDataBound(object sender, EventArgs e)
        {
            (sender as Telerik.Reporting.Processing.TextBox).Value = "--";
        }



        /// <summary>
        /// Handles the ItemDataBound event of the textBox22 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBox22_ItemDataBound(object sender, EventArgs e)
        {
            (sender as Telerik.Reporting.Processing.TextBox).Value = _sum;
        }



        /// <summary>
        /// Handles the NeedDataSource event of the table1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void table1_NeedDataSource(object sender, EventArgs e)
        {
            var dataManager = new DataManager();
            (sender as Telerik.Reporting.Processing.Table).DataSource = dataManager.InvoiceProducts.SelectAll(_invoiceId);
        }
    }
}