using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.DictionaryEditForm
{
    public partial class PaymentArticleEdit : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        private object _dataItem = null;


        /// <summary>
        /// Gets or sets the data item.
        /// </summary>
        /// <value>
        /// The data item.
        /// </value>
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }



        /// <summary>
        /// Gets the service level.
        /// </summary>
        protected tbl_PaymentArticle PaymentArticle
        {
            get { return (tbl_PaymentArticle)ViewState["PaymentArticle"]; }
            set { ViewState["PaymentArticle"] = value; }
        }


        /// <summary>
        /// Gets or sets the service level client id.
        /// </summary>
        /// <value>
        /// The service level client id.
        /// </value>
        protected Guid PaymentArticleId
        {
            get
            {
                if (ViewState["PaymentArticleId"] == null)
                    ViewState["PaymentArticleId"] = Guid.Empty;

                return (Guid)ViewState["PaymentArticleId"];
            }
            set { ViewState["PaymentArticleId"] = value; }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rgPaymentPass.Culture = new CultureInfo("ru-RU");
            if (DataItem != null && !(DataItem is GridInsertionObject))
                PaymentArticle = (tbl_PaymentArticle)DataItem;
            if (PaymentArticle != null)
            {
                edsPaymentPass.Where = string.Format("it.OutgoPaymentArticleID = GUID '{0}' OR it.IncomePaymentArticleID = GUID '{0}'",
                                                     PaymentArticle.ID);
                edsPaymentBalance.Where = string.Format("it.PaymentArticleID = GUID '{0}'",
                                                     PaymentArticle.ID);
            }
        }


        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            dcbPaymentPassCategory.SiteID = CurrentUser.Instance.SiteID;
            BindData();
        }
 
        private void BindData()
        {
                        if (PaymentArticle != null)
                        {
                            if (dcbPaymentPassCategory.SelectedId != PaymentArticle.PaymentPassCategoryID && dcbPaymentPassCategory.SelectedId==Guid.Empty)
                            {
                                dcbPaymentPassCategory.SelectedId = PaymentArticle.PaymentPassCategoryID;
                                var paymentPassCategory =
                                    DataManager.PaymentPassCategory.SelectById(PaymentArticle.PaymentPassCategoryID);
                                dcbPaymentPassCategory.SelectedText = paymentPassCategory != null
                                                                          ? paymentPassCategory.Title
                                                                          : "";
                            }
                        }
        }

        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            var paymentArticle = DataManager.PaymentArticle.SelectById(PaymentArticle.ID);

            if (paymentArticle != null)
            {
                paymentArticle.Title = txtTitle.Text;
                paymentArticle.PaymentPassCategoryID = dcbPaymentPassCategory.SelectedId;
                paymentArticle.Note = txtNote.Text;
                DataManager.PaymentArticle.Update(paymentArticle);
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {
            var PaymentArticle = new tbl_PaymentArticle
                                   {
                                       ID = Guid.NewGuid(),
                                       SiteID = CurrentUser.Instance.SiteID,
                                       Title = txtTitle.Text,
                                       PaymentPassCategoryID = dcbPaymentPassCategory.SelectedId,
                                       Note = txtNote.Text
                                   };

            DataItem = PaymentArticle;

            DataManager.PaymentArticle.Add(PaymentArticle);            
        }
    }
}