using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Order;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Shipment
{
    public partial class ShipmentProducts : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler ShipmentProductsChanged;
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? ShipmentId
        {
            get
            {
                object o = ViewState["ShipmentId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["ShipmentId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteId
        {
            get
            {
                object o = ViewState["SiteId"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["SiteId"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? PriceListId
        {
            get
            {
                object o = ViewState["PriceListId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["PriceListId"] = value;
            }
        }


        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? ShipmentDate
        {
            get
            {
                if (ViewState["ShipmentDate"] == null)
                    return null;
                return (DateTime)ViewState["ShipmentDate"];
            }
            set { ViewState["ShipmentDate"] = value; }
        }



        /// <summary>
        /// Gets the shipment products list.
        /// </summary>
        public List<ShipmentProductsMap>  ShipmentProductsList
        {
            get { return (List<ShipmentProductsMap>)ViewState["ShipmentProducts"]; }
        }



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {        
            ajaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(ajaxPanel_AjaxRequest);

            _dataManager = ((LeadForceBasePage)Page).DataManager;

            rgShipmentProducts.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (ShipmentId != null)
                ViewState["ShipmentProducts"] = _dataManager.ShipmentProducts.SelectAll((Guid)ShipmentId).Select(ip => new ShipmentProductsMap()
                                                                                                                  {
                                                                                                                    ID = ip.ID,
                                                                                                                    ShipmentID = ip.ShipmentID,
                                                                                                                    ProductID = ip.ProductID,
                                                                                                                    TaskID = ip.TaskID,
                                                                                                                    AnyProductName = ip.AnyProductName,
                                                                                                                    SerialNumber = ip.SerialNumber,
                                                                                                                    PriceListID = ip.PriceListID,
                                                                                                                    Quantity = ip.Quantity,
                                                                                                                    UnitID = ip.UnitID,
                                                                                                                    CurrencyID = ip.CurrencyID,
                                                                                                                    Rate = ip.Rate,
                                                                                                                    CurrencyPrice = ip.CurrencyPrice,
                                                                                                                    CurrencyAmount = ip.CurrencyAmount,
                                                                                                                    Price = ip.Price,
                                                                                                                    Amount = ip.Amount,
                                                                                                                    SpecialOfferPriceListID = ip.SpecialOfferPriceListID,
                                                                                                                    Discount = ip.Discount,
                                                                                                                    CurrencyDiscountAmount = ip.CurrencyDiscountAmount,
                                                                                                                    DiscountAmount = ip.DiscountAmount,
                                                                                                                    CurrencyTotalAmount = ip.CurrencyTotalAmount,
                                                                                                                    TotalAmount = ip.TotalAmount
                                                                                                                  }).ToList();
            else
                ViewState["ShipmentProducts"] = new List<ShipmentProductsMap>();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentProducts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgShipmentProducts.DataSource = ViewState["ShipmentProducts"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentProducts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var gridEditFormItem = (GridEditFormItem)e.Item;                

                var dcbProducts = (DictionaryComboBox)gridEditFormItem.FindControl("dcbProducts");
                dcbProducts.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "ProductStatusID", DbType = DbType.Int32, Value = ((int)ProductStatus.Current).ToString()});
                dcbProducts.SiteID = SiteId;
                dcbProducts.BindData();

                var rcbTasks = (RadComboBox)gridEditFormItem.FindControl("rcbTasks");
                BindTasks(rcbTasks, null);

                var dcbPriceList = (PriceListComboBox)gridEditFormItem.FindControl("dcbPriceList");                
                dcbPriceList.SiteId = SiteId;
                if (PriceListId.HasValue && PriceListId != Guid.Empty)
                    dcbPriceList.SelectedId = (Guid)PriceListId;
                else
                {
                    dcbPriceList.SelectPriceListType = PriceListType.PriceList;
                    dcbPriceList.SelectPriceListStatus = PriceListStatus.Current;
                }
                dcbPriceList.OrderDate = ShipmentDate;
                dcbPriceList.BindData();


                var dcbCurrency = (DictionaryComboBox)gridEditFormItem.FindControl("dcbCurrency");
                dcbCurrency.SiteID = SiteId;
                dcbCurrency.BindData();

                var dcbUnit = (DictionaryComboBox)gridEditFormItem.FindControl("dcbUnit");
                dcbUnit.SiteID = SiteId;
                dcbUnit.BindData();

                var dcbDiscountPriceList = (PriceListComboBox)gridEditFormItem.FindControl("dcbSpecialOfferPriceList");
                dcbDiscountPriceList.SiteId = SiteId;                
                dcbDiscountPriceList.OrderDate = ShipmentDate;
                dcbDiscountPriceList.BindData();

                var item = e.Item as GridEditableItem;
                
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var shipmentProduct = (ShipmentProductsMap)item.DataItem;                    

                    ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId = shipmentProduct.ProductID;
                    BindTasks(rcbTasks, shipmentProduct.ProductID);
                    rcbTasks.SelectedIndex = rcbTasks.FindItemIndexByValue(shipmentProduct.TaskID.ToString());

                    ((TextBox)item.FindControl("txtAnyProductName")).Text = shipmentProduct.AnyProductName;
                    ((TextBox)item.FindControl("txtSerialNumber")).Text = shipmentProduct.SerialNumber;
                    if (shipmentProduct.PriceListID.HasValue)
                        ((PriceListComboBox)item.FindControl("dcbPriceList")).SelectedId = shipmentProduct.PriceListID.Value;
                    else
                        ((PriceListComboBox) item.FindControl("dcbPriceList")).SelectedId = Guid.Empty;
                    ((RadNumericTextBox)item.FindControl("rntxtQuantity")).Value = (double)shipmentProduct.Quantity;
                    ((DictionaryComboBox)item.FindControl("dcbCurrency")).SelectedId = shipmentProduct.CurrencyID;
                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyPrice")).Value = (double)shipmentProduct.CurrencyPrice;
                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyAmount")).Value = (double)shipmentProduct.CurrencyAmount;
                    ((DictionaryComboBox)item.FindControl("dcbUnit")).SelectedId = shipmentProduct.UnitID;
                    ((RadNumericTextBox)item.FindControl("rntxtRate")).Value = (double)shipmentProduct.Rate;
                    ((RadNumericTextBox)item.FindControl("rntxtPrice")).Value = (double)shipmentProduct.Price;
                    ((RadNumericTextBox)item.FindControl("rntxtAmount")).Value = (double)shipmentProduct.Amount;

                    if (shipmentProduct.SpecialOfferPriceListID.HasValue)
                        ((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId = (Guid)shipmentProduct.SpecialOfferPriceListID;

                    if (shipmentProduct.CurrencyDiscountAmount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value = (double)shipmentProduct.CurrencyDiscountAmount;

                    if (shipmentProduct.DiscountAmount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value = (double)shipmentProduct.DiscountAmount;

                    if (shipmentProduct.Discount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value = (double)shipmentProduct.Discount;

                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyTotalAmount")).Value = (double)shipmentProduct.CurrencyTotalAmount;
                    ((RadNumericTextBox)item.FindControl("rntxtTotalAmount")).Value = (double)shipmentProduct.TotalAmount;
                }
                else
                {
                    var currency = _dataManager.Currency.SelectAll(CurrentUser.Instance.SiteID).FirstOrDefault(o => o.IsBaseCurrency);
                    if (currency != null)
                        ((DictionaryComboBox)item.FindControl("dcbCurrency")).SelectedId = currency.ID;
                }
            }
            else if (e.Item is GridDataItem)
            {
                var shipmentProduct = e.Item.DataItem as ShipmentProductsMap;
                ((Literal) e.Item.FindControl("lrlProductName")).Text = _dataManager.Product.SelectNameById(shipmentProduct.ProductID);
                ((Literal)e.Item.FindControl("lrlUnitName")).Text = _dataManager.Unit.SelectNameById(shipmentProduct.UnitID);
                ((Literal)e.Item.FindControl("lrlCurrencyName")).Text = _dataManager.Currency.SelectNameById(shipmentProduct.CurrencyID);
            }
        }        



        /// <summary>
        /// Handles the DeleteCommand event of the rgShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentProducts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<ShipmentProductsMap>)ViewState["ShipmentProducts"]).Remove(
                ((List<ShipmentProductsMap>)ViewState["ShipmentProducts"]).Where(s => s.ID == id).FirstOrDefault());

            if (ShipmentProductsChanged != null)
                ShipmentProductsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentProducts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            
            SaveToViewState(Guid.Empty, item);

            rgShipmentProducts.MasterTableView.IsItemInserted = false;
            rgShipmentProducts.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgShipmentProducts_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }




        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid orderProductId, GridEditableItem item)
        {
            var shipmentProduct = ((List<ShipmentProductsMap>)ViewState["ShipmentProducts"]).Where(s => s.ID == orderProductId).FirstOrDefault() ?? new ShipmentProductsMap();
           
            shipmentProduct.ProductID = ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId;


            var rcbTasks = (RadComboBox)item.FindControl("rcbTasks");

            if (!string.IsNullOrEmpty(rcbTasks.SelectedValue))
                shipmentProduct.TaskID = Guid.Parse(rcbTasks.SelectedValue);
            else
                shipmentProduct.TaskID = null;

            shipmentProduct.AnyProductName = ((TextBox)item.FindControl("txtAnyProductName")).Text;
            shipmentProduct.SerialNumber = ((TextBox)item.FindControl("txtSerialNumber")).Text;
            if (((PriceListComboBox)item.FindControl("dcbPriceList")).SelectedId != Guid.Empty)
                shipmentProduct.PriceListID = ((PriceListComboBox)item.FindControl("dcbPriceList")).SelectedId;
            else
                shipmentProduct.PriceListID = null;

            shipmentProduct.Quantity = (decimal)((RadNumericTextBox)item.FindControl("rntxtQuantity")).Value;
            shipmentProduct.CurrencyID = ((DictionaryComboBox)item.FindControl("dcbCurrency")).SelectedId;
            shipmentProduct.CurrencyPrice = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyPrice")).Value;
            shipmentProduct.CurrencyAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyAmount")).Value;
            shipmentProduct.UnitID = ((DictionaryComboBox)item.FindControl("dcbUnit")).SelectedId;
            shipmentProduct.Rate = (decimal)((RadNumericTextBox)item.FindControl("rntxtRate")).Value;
            shipmentProduct.Price = (decimal)((RadNumericTextBox)item.FindControl("rntxtPrice")).Value;
            shipmentProduct.Amount = (decimal)((RadNumericTextBox)item.FindControl("rntxtAmount")).Value;

            if (((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId != Guid.Empty)
                shipmentProduct.SpecialOfferPriceListID = ((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId;
            else
                shipmentProduct.SpecialOfferPriceListID = null;

            if (((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value.HasValue)
                shipmentProduct.CurrencyDiscountAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value;
            else
                shipmentProduct.CurrencyDiscountAmount = null;

            if (((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value.HasValue)
                shipmentProduct.DiscountAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value;
            else
                shipmentProduct.DiscountAmount = null;

            if (((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value.HasValue)
                shipmentProduct.Discount = (decimal)((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value;
            else
                shipmentProduct.Discount = null;

            shipmentProduct.CurrencyTotalAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyTotalAmount")).Value;
            shipmentProduct.TotalAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtTotalAmount")).Value;

            if (shipmentProduct.ID == Guid.Empty)
            {
                shipmentProduct.ID = Guid.NewGuid();
                ((List<ShipmentProductsMap>) ViewState["ShipmentProducts"]).Add(shipmentProduct);
            }

            if (ShipmentProductsChanged != null)
                ShipmentProductsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbProducts_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbProducts = (DictionaryComboBox) sender;
            var product = _dataManager.Product.SelectById(SiteId, dcbProducts.SelectedId);
            if (product != null)
            {
                ((TextBox) dcbProducts.Parent.FindControl("txtAnyProductName")).Text = product.Title;

                BindTasks((RadComboBox)dcbProducts.Parent.FindControl("rcbTasks"), product.ID);

                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).ProductId = product.ID;
                if (PriceListId.HasValue && PriceListId != Guid.Empty)
                    ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId = (Guid)PriceListId;
                else
                {
                    ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectPriceListType = PriceListType.PriceList;
                    ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectPriceListStatus = PriceListStatus.Current;
                }
                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).BindData();

                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbSpecialOfferPriceList")).ProductId = product.ID;
                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbSpecialOfferPriceList")).BindData();

                if (product.UnitID.HasValue)
                    ((DictionaryComboBox)dcbProducts.Parent.FindControl("dcbUnit")).SelectedId = (Guid)product.UnitID;

                UpdateCurrencyPrice(product.ID, ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId, dcbProducts.Parent);
            }
            else
            {
                BindTasks((RadComboBox)dcbProducts.Parent.FindControl("rcbTasks"),null);
                UpdateCurrencyPrice(Guid.Empty, ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId, dcbProducts.Parent);
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dcbPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbPriceList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbPriceList = (PriceListComboBox)sender;
            UpdateCurrencyPrice(((DictionaryComboBox)dcbPriceList.Parent.FindControl("dcbProducts")).SelectedId, dcbPriceList.SelectedId, dcbPriceList.Parent);
        }



        /// <summary>
        /// Handles the AjaxRequest event of the radAjaxManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        void ajaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {            
            RecalculatePrices(FindControlRecursive(rgShipmentProducts, "plEditForm"), e.Argument);            
        }



        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID != null && root.ID == id)
                return root;
            return (from Control c in root.Controls select FindControlRecursive(c, id)).FirstOrDefault(rc => rc != null);
        }



        /// <summary>
        /// Updates the currency price.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="priceListId">The price list id.</param>
        /// <param name="container">The container.</param>
        private void UpdateCurrencyPrice(Guid productId, Guid priceListId, Control container)
        {
            var productPrice = _dataManager.ProductPrice.SelectByProductAndPriceListId(productId, priceListId);
            if (productPrice != null)            
                ((RadNumericTextBox) container.FindControl("rntxtCurrencyPrice")).Value = productPrice.Price;
            else            
                ((RadNumericTextBox)container.FindControl("rntxtCurrencyPrice")).Value = 0;

            RecalculatePrices(container, "rntxtCurrencyPrice");
        }



        /// <summary>
        /// Recalculates the prices.
        /// </summary>
        /// <param name="container">The container.</param>
        private void RecalculatePrices(Control container, string senderId)
        {
            var rntxtQuantity = ((RadNumericTextBox)container.FindControl("rntxtQuantity"));
            var rntxtCurrencyPrice = ((RadNumericTextBox) container.FindControl("rntxtCurrencyPrice"));
            var rntxtCurrencyAmount = ((RadNumericTextBox)container.FindControl("rntxtCurrencyAmount"));
            var rntxtRate = ((RadNumericTextBox)container.FindControl("rntxtRate"));
            var rntxtPrice = ((RadNumericTextBox)container.FindControl("rntxtPrice"));
            var rntxtAmount = ((RadNumericTextBox)container.FindControl("rntxtAmount"));
            var rntxtCurrencyDiscountAmount = ((RadNumericTextBox)container.FindControl("rntxtCurrencyDiscountAmount"));
            var rntxtDiscountAmount = ((RadNumericTextBox)container.FindControl("rntxtDiscountAmount"));
            var rntxtDiscount = ((RadNumericTextBox)container.FindControl("rntxtDiscount"));
            var rntxtCurrencyTotalAmount = ((RadNumericTextBox)container.FindControl("rntxtCurrencyTotalAmount"));
            var rntxtTotalAmount = ((RadNumericTextBox)container.FindControl("rntxtTotalAmount"));

            var quantity = rntxtQuantity.Value ?? 0;
            var currencyPrice = rntxtCurrencyPrice.Value ?? 0;
            var currencyAmount = rntxtCurrencyAmount.Value ?? 0;            
            var rate = rntxtRate.Value ?? 0;
            var price = rntxtPrice.Value ?? 0;
            var amount = rntxtAmount.Value ?? 0;
            var currencyDiscountAmount = rntxtCurrencyDiscountAmount.Value ?? 0;
            var discountAmount = rntxtDiscountAmount.Value ?? 0;
            var discount = rntxtDiscount.Value ?? 0;
            var currencyTotalAmount = rntxtCurrencyTotalAmount.Value ?? 0;
            var totalAmount = rntxtTotalAmount.Value ?? 0;

            switch (senderId)
            {
                case "rntxtQuantity":                      
                case "rntxtCurrencyPrice":
                case "rntxtRate":
                    currencyAmount = currencyPrice*quantity;
                    price = currencyPrice*rate;
                    amount = currencyAmount*rate;
                    currencyDiscountAmount = (currencyAmount/100)*discount;
                    discountAmount = (amount / 100) * discount;
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;                    
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyDiscountAmount.Value = currencyDiscountAmount;
                    rntxtDiscountAmount.Value = discountAmount;
                    rntxtCurrencyAmount.Value = currencyAmount;
                    rntxtPrice.Value = price;
                    rntxtAmount.Value = amount;            
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtCurrencyAmount":
                    currencyPrice = currencyAmount/quantity;
                    price = currencyPrice*rate;
                    amount = currencyAmount*rate;
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyPrice.Value = currencyPrice;                    
                    rntxtPrice.Value = price;
                    rntxtAmount.Value = amount;            
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtPrice":
                    currencyPrice = price/rate;
                    currencyAmount = currencyPrice*quantity;                    
                    amount = currencyAmount*rate;
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyPrice.Value = currencyPrice;
                    rntxtCurrencyAmount.Value = currencyAmount;                    
                    rntxtAmount.Value = amount;            
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtAmount":
                    currencyAmount = amount/rate;
                    price = amount/quantity;
                    currencyPrice = currencyAmount / quantity;
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyPrice.Value = currencyPrice;
                    rntxtCurrencyAmount.Value = currencyAmount;                    
                    rntxtPrice.Value = price;            
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtCurrencyDiscountAmount":
                    discountAmount = currencyDiscountAmount*rate;
                    if (currencyAmount > 0)
                        discount = 100 - (((currencyAmount - currencyDiscountAmount) / currencyAmount) * 100);
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtDiscountAmount.Value = discountAmount;
                    rntxtDiscount.Value = discount;
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtDiscountAmount":
                    currencyDiscountAmount = discountAmount/rate;
                    if (amount > 0)
                        discount = 100 - (((amount - discountAmount) / amount) * 100);
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyDiscountAmount.Value = currencyDiscountAmount;
                    rntxtDiscount.Value = discount;
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtDiscount":
                    currencyDiscountAmount = (currencyAmount / 100) * discount;
                    discountAmount = (amount / 100) * discount;
                    currencyTotalAmount = currencyAmount - currencyDiscountAmount;
                    totalAmount = amount - discountAmount;
                    rntxtCurrencyDiscountAmount.Value = currencyDiscountAmount;
                    rntxtDiscountAmount.Value = discountAmount;
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtCurrencyTotalAmount":
                    totalAmount = currencyTotalAmount*rate;
                    discount = ((currencyAmount - currencyTotalAmount) / currencyAmount) * 100;
                    discountAmount = (amount / 100) * discount;
                    currencyDiscountAmount = (currencyAmount / 100) * discount;
                    rntxtCurrencyDiscountAmount.Value = currencyDiscountAmount;
                    rntxtDiscountAmount.Value = discountAmount;
                    rntxtDiscount.Value = discount;
                    rntxtTotalAmount.Value = totalAmount >= 0 ? totalAmount : 0;
                    break;
                case "rntxtTotalAmount":
                    currencyTotalAmount = totalAmount/rate;
                    discount = ((amount - totalAmount) / amount) * 100;
                    discountAmount = (amount / 100) * discount;
                    currencyDiscountAmount = (currencyAmount / 100) * discount;
                    rntxtCurrencyDiscountAmount.Value = currencyDiscountAmount;
                    rntxtDiscountAmount.Value = discountAmount;
                    rntxtDiscount.Value = discount;
                    rntxtCurrencyTotalAmount.Value = currencyTotalAmount >= 0 ? currencyTotalAmount : 0;
                    break;
            }            
        }



        /// <summary>
        /// Binds the tasks.
        /// </summary>
        /// <param name="rcbTasks">The RCB tasks.</param>
        /// <param name="productId">The product id.</param>
        protected void BindTasks(RadComboBox rcbTasks, Guid? productId)
        {
            var tasks = _dataManager.Task.SelectAll(CurrentUser.Instance.SiteID);
            if (productId.HasValue)
                tasks = tasks.Where(t => t.tbl_TaskType.ProductID == productId);            

            rcbTasks.DataSource = tasks;
            rcbTasks.DataValueField = "ID";
            rcbTasks.DataTextField = "Title";
            rcbTasks.DataBind();            

            rcbTasks.Items.Insert(0, new RadComboBoxItem("Выберите значение"));
        }
    }
}
