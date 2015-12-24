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
    public partial class PaymentCFOEdit : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        private object _dataItem = null;




        /// <summary>
        /// Gets the service level.
        /// </summary>
        protected tbl_ServiceLevel ServiceLevel
        {
            get { return (tbl_ServiceLevel)ViewState["ServiceLevel"]; }
            set { ViewState["ServiceLevel"] = value; }
        }


        /// <summary>
        /// Gets or sets the service level client id.
        /// </summary>
        /// <value>
        /// The service level client id.
        /// </value>
        protected Guid ServiceLevelClientId
        {
            get
            {
                if (ViewState["ServiceLevelClientId"] == null)
                    ViewState["ServiceLevelClientId"] = Guid.Empty;

                return (Guid)ViewState["ServiceLevelClientId"];
            }
            set { ViewState["ServiceLevelClientId"] = value; }
        }


        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        protected Guid ClientId
        {
            get
            {
                if (ViewState["ClientId"] == null)
                    ViewState["ClientId"] = Guid.Empty;

                return (Guid)ViewState["ClientId"];
            }
            set { ViewState["ClientId"] = value; }
        }


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
        protected tbl_PaymentCFO PaymentCFO
        {
            get { return (tbl_PaymentCFO)ViewState["PaymentCFO"]; }
            set { ViewState["PaymentCFO"] = value; }
        }


        /// <summary>
        /// Gets or sets the service level client id.
        /// </summary>
        /// <value>
        /// The service level client id.
        /// </value>
        protected Guid PaymentCFOId
        {
            get
            {
                if (ViewState["PaymentCFOId"] == null)
                    ViewState["PaymentCFOId"] = Guid.Empty;

                return (Guid)ViewState["PaymentCFOId"];
            }
            set { ViewState["PaymentCFOId"] = value; }
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
                PaymentCFO = (tbl_PaymentCFO)DataItem;
            if (PaymentCFO != null)
            {
                edsPaymentPass.Where = string.Format("it.OutgoCFOID = GUID '{0}' OR it.IncomeCFOID = GUID '{0}' ",
                                                     PaymentCFO.ID);
                edsPaymentBalance.Where = string.Format("it.CFOID = GUID '{0}'",
                                                     PaymentCFO.ID);
            }
//            dcbPaymentPassCategory.SelectedId = (Guid)paymentData.RecipientLegalAccountID;
//            dcbPaymentPassCategory.SelectedText = DataManager.CompanyLegalAccount.SelectById((Guid)paymentData.RecipientLegalAccountID).Title;

            //rgServiceLevelClient.Culture = new CultureInfo("ru-RU");

            //edsServiceLevel.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            //edsServiceLevelRole.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            //edsClients.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            //edsClients.OrderBy = "it.Name";
            //edsContacts.Where = string.Format("it.SiteID = GUID '{0}' AND it.UserFullName <> ''", CurrentUser.Instance.SiteID.ToString());
            //edsContacts.OrderBy = "it.UserFullName";

            //if (DataItem != null && !(DataItem is GridInsertionObject))
            //    ServiceLevel = ((ICustomTypeDescriptor)DataItem).GetPropertyOwner(null) as tbl_ServiceLevel;
            
            //if (ServiceLevel != null)
            //    edsServiceLevelClient.Where = string.Format("it.ServiceLevelID = GUID '{0}'", ServiceLevel.ID);
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
                        if (PaymentCFO != null)
                        {
                            if (dcbPaymentPassCategory.SelectedId != PaymentCFO.PaymentPassCategoryID && dcbPaymentPassCategory.SelectedId==Guid.Empty)
                            {
                                dcbPaymentPassCategory.SelectedId = PaymentCFO.PaymentPassCategoryID;
                                var paymentPassCategory =
                                    DataManager.PaymentPassCategory.SelectById(PaymentCFO.PaymentPassCategoryID);
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
            var paymentCFO = DataManager.PaymentCFO.SelectById(PaymentCFO.ID);

            if (paymentCFO != null)
            {
                paymentCFO.Title = txtTitle.Text;
                paymentCFO.PaymentPassCategoryID = dcbPaymentPassCategory.SelectedId;
                paymentCFO.Note = txtNote.Text;
                DataManager.PaymentCFO.Update(paymentCFO);
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {
            var paymentCFO = new tbl_PaymentCFO
                                   {
                                       ID = Guid.NewGuid(),
                                       SiteID = CurrentUser.Instance.SiteID,
                                       Title = txtTitle.Text,
                                       PaymentPassCategoryID = dcbPaymentPassCategory.SelectedId,
                                       Note = txtNote.Text
                                   };

            DataItem = paymentCFO;

            DataManager.PaymentCFO.Add(paymentCFO);            
        }
    }
}