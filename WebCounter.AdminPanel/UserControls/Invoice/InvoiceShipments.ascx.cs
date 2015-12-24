using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations.Shipment;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Invoice
{
    public partial class InvoiceShipments : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler InvoiceShipmentsChanged;
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? InvoiceId
        {
            get
            {                
                return (Guid?)ViewState["InvoiceId"];
            }
            set
            {
                ViewState["InvoiceId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? BuyerCompanyLegalAccountId
        {
            get
            {
                return (Guid?)ViewState["BuyerCompanyLegalAccountId"];
            }
            set
            {
                ViewState["BuyerCompanyLegalAccountId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ExecutorCompanyLegalAccountId
        {
            get
            {
                return (Guid?)ViewState["ExecutorCompanyLegalAccountId"];
            }
            set
            {
                ViewState["ExecutorCompanyLegalAccountId"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public decimal InvoiceAmount
        {
            get
            {
                if (ViewState["InvoiceAmount"] == null)
                    ViewState["InvoiceAmount"] = 0;

                return (decimal)ViewState["InvoiceAmount"];
            }
            set
            {
                ViewState["InvoiceAmount"] = value;
            }
        }

        /// <summary>
        /// Gets the shipment invoices list.
        /// </summary>
        public List<ShipmentMap> InvoiceShipmentsList
        {
            get
            {
                if (ViewState["InvoiceShipmentsList"] == null)
                    ViewState["InvoiceShipmentsList"] = new List<ShipmentMap>();

                return (List<ShipmentMap>)ViewState["InvoiceShipmentsList"];
            }
            set { ViewState["InvoiceShipmentsList"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {                    
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgInvoiceShipments.Culture = new CultureInfo("ru-RU");

            if (!InvoiceId.HasValue)
                rbtnGenerateShipment.Visible = false;

            if (!Page.IsPostBack)
                BindData();
        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (InvoiceId != null)
            {
                var invoice = _dataManager.Invoice.SelectById(InvoiceId.Value);

                InvoiceShipmentsList = invoice.tbl_Shipment.Select(o => new ShipmentMap()
                                                                            {
                                                                                ID = o.ID,
                                                                                CreatedAt = o.CreatedAt,
                                                                                ShipmentTypeID = o.ShipmentTypeID,
                                                                                Note = o.Note,
                                                                                Number = o.Number
                                                                            }).ToList();

            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgInvoiceShipments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgInvoiceShipments_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgInvoiceShipments.DataSource = InvoiceShipmentsList;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgInvoiceShipments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgInvoiceShipments_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var item = (GridEditFormItem)e.Item;                

                if (e.Item.OwnerTableView.IsItemInserted)
                {
                    var dcbShipments = ((DictionaryOnDemandComboBox)item.FindControl("dcbShipments"));
                    dcbShipments.SiteID = CurrentUser.Instance.SiteID;
                    dcbShipments.MaskFilters.Clear();
                    dcbShipments.MaskFilters.Add("#Number", "Number");
                    dcbShipments.MaskFilters.Add("#CreatedAt", "CreatedAt");

                    ((RadNumericTextBox) item.FindControl("rntxtShipmentAmount")).Value = (double?)InvoiceAmount;

                    AddFilters(dcbShipments);
                }                
            }
            else if (e.Item is GridDataItem)
            {
                var shipment = e.Item.DataItem as ShipmentMap;
                ((Literal) e.Item.FindControl("lrlShipmentType")).Text = _dataManager.ShipmentType.SelectById(CurrentUser.Instance.SiteID, shipment.ShipmentTypeID).Title;
            }
        }



        private void AddFilters(DictionaryOnDemandComboBox dcbShipments)
        {
            dcbShipments.SelectedIdNullable = null;
            dcbShipments.SelectedText = string.Empty;

            dcbShipments.Filters.Clear();

            if (InvoiceAmount != 0)
                dcbShipments.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "ShipmentAmount",
                    DbType = DbType.Decimal,
                    Operation = FilterOperation.Equal,
                    Value = InvoiceAmount.ToString()
                });
            if (ExecutorCompanyLegalAccountId.HasValue)
                dcbShipments.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "ExecutorCompanyLegalAccountID",
                    DbType = DbType.Guid,
                    Operation = FilterOperation.Equal,
                    Value = ExecutorCompanyLegalAccountId.Value.ToString()
                });
            if (BuyerCompanyLegalAccountId.HasValue)
                dcbShipments.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "BuyerCompanyLegalAccountID",
                    DbType = DbType.Guid,
                    Operation = FilterOperation.Equal,
                    Value = BuyerCompanyLegalAccountId.Value.ToString()
                });

            dcbShipments.ShowEmpty = false;

            var shipment = _dataManager.Shipment.SelectForInvoice(InvoiceAmount, BuyerCompanyLegalAccountId, ExecutorCompanyLegalAccountId).FirstOrDefault();
            if (shipment != null)
            {
                dcbShipments.SelectedId = shipment.ID;
                dcbShipments.SelectedText = string.Format("Отгрузка № {0} от {1}",shipment.Number, shipment.CreatedAt.ToString("dd.MM.yyyy"));
            }
        }        



        /// <summary>
        /// Handles the DeleteCommand event of the rgInvoiceShipments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgInvoiceShipments_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            InvoiceShipmentsList.Remove(InvoiceShipmentsList.FirstOrDefault(s => s.ID == id));

            if (InvoiceShipmentsChanged != null)
                InvoiceShipmentsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgInvoiceShipments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgInvoiceShipments_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            
            SaveToViewState(item);

            rgInvoiceShipments.MasterTableView.IsItemInserted = false;
            rgInvoiceShipments.Rebind();
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>        
        /// <param name="item">The item.</param>
        private void SaveToViewState(GridEditableItem item)
        {
            var invoiceShipment = new ShipmentMap();
           
            invoiceShipment.ID = ((DictionaryOnDemandComboBox)item.FindControl("dcbShipments")).SelectedId;            

            var shipment = _dataManager.Shipment.SelectById(invoiceShipment.ID);
            invoiceShipment.CreatedAt = shipment.CreatedAt;
            invoiceShipment.ShipmentTypeID = shipment.ShipmentTypeID;
            invoiceShipment.Note = shipment.Note;
            invoiceShipment.Number = shipment.Number;

            if (InvoiceShipmentsList.SingleOrDefault(o => o.ID == invoiceShipment.ID) == null)
                InvoiceShipmentsList.Add(invoiceShipment);

            if (InvoiceShipmentsChanged != null)
                InvoiceShipmentsChanged(this, new EventArgs());
        }


        protected void rntxtShipmentAmount_OnTextChanged(object sender, EventArgs e)
        {
            var rntxtInvoiceAmount = (RadNumericTextBox) sender;
            var dcbInvoices = (DictionaryOnDemandComboBox)rntxtInvoiceAmount.Parent.FindControl("dcbShipments");

            if (rntxtInvoiceAmount.Value.HasValue)
                InvoiceAmount = (decimal)rntxtInvoiceAmount.Value;
            else
                InvoiceAmount = 0;

            AddFilters(dcbInvoices);
        }



        protected void rbtnGenerateShipment_OnClick(object sender, EventArgs e)
        {
            var shipmentType = _dataManager.ShipmentType.SelectAll(CurrentUser.Instance.SiteID).SingleOrDefault(o => o.IsDefault);

            if (!InvoiceId.HasValue)
                return;

            if (shipmentType == null)
            {
                ucMessage.Text = "Необходимо указать тип документа по умолчанию";
                return;
            }

            var invoice = _dataManager.Invoice.SelectById(InvoiceId.Value);

            var shipment = new tbl_Shipment
                               {
                                   CreatedAt = DateTime.Now,
                                   ShipmentTypeID = shipmentType.ID,
                                   ShipmentStatusID = (int) ShipmentStatus.Prepared,
                                   Note = invoice.Note,
                                   ExecutorCompanyID = invoice.ExecutorCompanyID,
                                   BuyerCompanyID = invoice.BuyerCompanyID,
                                   ExecutorCompanyLegalAccountID = invoice.ExecutorCompanyLegalAccountID,
                                   BuyerCompanyLegalAccountID = invoice.BuyerCompanyLegalAccountID,
                                   BuyerContactID = invoice.BuyerContactID,
                                   ExecutorContactID = invoice.ExecutorContactID,
                                   SendDate = null,
                                   PriceListID = null,
                                   OrderID = invoice.OrderID,
                                   ShipmentAmount = invoice.InvoiceAmount
                               };




            shipment.tbl_Invoice.Clear();
            shipment.tbl_Invoice.Add(invoice);            
            shipment.SiteID = invoice.SiteID;
            shipment.OwnerID = CurrentUser.Instance.ContactID;            
            var documentNumerator = DocumentNumerator.GetNumber((Guid) shipmentType.NumeratorID,
                                                                shipment.CreatedAt,
                                                                shipmentType.tbl_Numerator.Mask, "tbl_Shipment");
            shipment.Number = documentNumerator.Number;
            shipment.SerialNumber = documentNumerator.SerialNumber;

            foreach (var product in invoice.tbl_InvoiceProducts.ToList())
            {
                var shipmentProduct = new tbl_ShipmentProducts
                                          {
                                              ID = Guid.NewGuid(),
                                              Amount = product.Amount,
                                              AnyProductName = product.AnyProductName,
                                              CurrencyAmount = product.CurrencyAmount,
                                              CurrencyDiscountAmount = product.CurrencyDiscountAmount,
                                              CurrencyID = product.CurrencyID,
                                              CurrencyPrice = product.CurrencyPrice,
                                              CurrencyTotalAmount = product.CurrencyTotalAmount,
                                              Discount = product.Discount,
                                              DiscountAmount = product.DiscountAmount,
                                              Price = product.Price,
                                              PriceListID = product.PriceListID,
                                              ProductID = product.ProductID,
                                              Quantity = product.Quantity,
                                              Rate = product.Rate,
                                              SerialNumber = product.SerialNumber,
                                              ShipmentID = shipment.ID,
                                              SpecialOfferPriceListID = product.SpecialOfferPriceListID,
                                              TaskID = product.TaskID,
                                              TotalAmount = product.TotalAmount,
                                              UnitID = product.UnitID
                                          };

                shipment.tbl_ShipmentProducts.Add(shipmentProduct);
            }

            _dataManager.Shipment.Add(shipment);

            Response.Redirect(UrlsData.AP_ShipmentEdit(shipment.ID));
        }
    }
}
