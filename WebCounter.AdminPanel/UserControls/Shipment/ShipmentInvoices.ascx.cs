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
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Shipment
{
    public partial class ShipmentInvoices : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler ShipmentInvoicesChanged;
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ShipmentId
        {
            get
            {                
                return (Guid?)ViewState["ShipmentId"];
            }
            set
            {
                ViewState["ShipmentId"] = value;
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
        public decimal ShipmentAmount
        {
            get
            {
                if (ViewState["ShipmentAmount"] == null)
                    ViewState["ShipmentAmount"] = 0;

                return (decimal)ViewState["ShipmentAmount"];
            }
            set
            {
                ViewState["ShipmentAmount"] = value;
            }
        }

        /// <summary>
        /// Gets the shipment invoices list.
        /// </summary>
        public List<InvoiceMap> ShipmentInvoicesList
        {
            get
            {
                if (ViewState["ShipmentInvoices"] == null)
                    ViewState["ShipmentInvoices"] = new List<InvoiceMap>();

                return (List<InvoiceMap>)ViewState["ShipmentInvoices"];
            } 
            set { ViewState["ShipmentInvoices"] = value; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {                    
            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgShipmentInvoices.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (ShipmentId != null)
            {
                var shipment = _dataManager.Shipment.SelectById(ShipmentId.Value);

                ShipmentInvoicesList = shipment.tbl_Invoice.Select(o => new InvoiceMap()
                                                                            {
                                                                                ID = o.ID,
                                                                                CreatedAt = o.CreatedAt,
                                                                                InvoiceTypeID = o.InvoiceTypeID,
                                                                                Note = o.Note,
                                                                                Number = o.Number
                                                                            }).ToList();

            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgShipmentInvoices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentInvoices_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgShipmentInvoices.DataSource = ShipmentInvoicesList;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgShipmentInvoices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentInvoices_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var item = (GridEditFormItem)e.Item;                

                if (e.Item.OwnerTableView.IsItemInserted)
                {                    
                    var dcbInvoices = ((DictionaryOnDemandComboBox) item.FindControl("dcbInvoices"));
                    dcbInvoices.SiteID = CurrentUser.Instance.SiteID;
                    dcbInvoices.MaskFilters.Clear();
                    dcbInvoices.MaskFilters.Add("#Number", "Number");
                    dcbInvoices.MaskFilters.Add("#CreatedAt", "CreatedAt");

                    ((RadNumericTextBox) item.FindControl("rntxtShipmentAmount")).Value = (double?)ShipmentAmount;

                    AddFilters(dcbInvoices);
                }                
            }
            else if (e.Item is GridDataItem)
            {
                var invoice = e.Item.DataItem as InvoiceMap;
                ((Literal) e.Item.FindControl("lrlInvoiceType")).Text = _dataManager.InvoiceType.SelectById(CurrentUser.Instance.SiteID, invoice.InvoiceTypeID).Title;
            }
        }



        private void AddFilters(DictionaryOnDemandComboBox dcbInvoices)
        {
            dcbInvoices.SelectedIdNullable = null;
            dcbInvoices.SelectedText = string.Empty;

            dcbInvoices.Filters.Clear();

            if (ShipmentAmount != 0)
                dcbInvoices.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "InvoiceAmount",
                    DbType = DbType.Decimal,
                    Operation = FilterOperation.Equal,
                    Value = ShipmentAmount.ToString()
                });
            if (ExecutorCompanyLegalAccountId.HasValue)
                dcbInvoices.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "ExecutorCompanyLegalAccountID",
                    DbType = DbType.Guid,
                    Operation = FilterOperation.Equal,
                    Value = ExecutorCompanyLegalAccountId.Value.ToString()
                });
            if (BuyerCompanyLegalAccountId.HasValue)
                dcbInvoices.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                {
                    Name = "BuyerCompanyLegalAccountID",
                    DbType = DbType.Guid,
                    Operation = FilterOperation.Equal,
                    Value = BuyerCompanyLegalAccountId.Value.ToString()
                });

            dcbInvoices.ShowEmpty = false;

            var invoice = _dataManager.Invoice.SelectForShipment(ShipmentAmount, BuyerCompanyLegalAccountId, ExecutorCompanyLegalAccountId).FirstOrDefault();
            if (invoice != null)
            {
                dcbInvoices.SelectedId = invoice.ID;
                dcbInvoices.SelectedText = string.Format("Счет № {0} от {1}",invoice.Number, invoice.CreatedAt.ToString("dd.MM.yyyy"));
            }
        }        



        /// <summary>
        /// Handles the DeleteCommand event of the rgShipmentInvoices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentInvoices_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ShipmentInvoicesList.Remove(ShipmentInvoicesList.FirstOrDefault(s => s.ID == id));

            if (ShipmentInvoicesChanged != null)
                ShipmentInvoicesChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgShipmentInvoices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentInvoices_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            
            SaveToViewState(item);

            rgShipmentInvoices.MasterTableView.IsItemInserted = false;
            rgShipmentInvoices.Rebind();
        }



        /// <summary>
        /// Saves the state of to view.
        /// </summary>        
        /// <param name="item">The item.</param>
        private void SaveToViewState(GridEditableItem item)
        {
            var shipmentInvoice = new InvoiceMap();
           
            shipmentInvoice.ID = ((DictionaryOnDemandComboBox)item.FindControl("dcbInvoices")).SelectedId;            

            var invoice = _dataManager.Invoice.SelectById(shipmentInvoice.ID);
            shipmentInvoice.CreatedAt = invoice.CreatedAt;
            shipmentInvoice.InvoiceTypeID = invoice.InvoiceTypeID;
            shipmentInvoice.Note = invoice.Note;
            shipmentInvoice.Number = invoice.Number;

            if (ShipmentInvoicesList.SingleOrDefault(o => o.ID == shipmentInvoice.ID) == null)
                ShipmentInvoicesList.Add(shipmentInvoice);

            if (ShipmentInvoicesChanged != null)
                ShipmentInvoicesChanged(this, new EventArgs());
        }


        protected void rntxtShipmentAmount_OnTextChanged(object sender, EventArgs e)
        {
            var rntxtShipmentAmount = (RadNumericTextBox) sender;
            var dcbInvoices = (DictionaryOnDemandComboBox)rntxtShipmentAmount.Parent.FindControl("dcbInvoices");

            if (rntxtShipmentAmount.Value.HasValue)
                ShipmentAmount = (decimal)rntxtShipmentAmount.Value;
            else
                ShipmentAmount = 0;

            AddFilters(dcbInvoices);
        }
    }
}
