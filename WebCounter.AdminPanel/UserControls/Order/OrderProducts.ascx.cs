using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Order
{
    public partial class OrderProducts : System.Web.UI.UserControl
    {
        private DataManager _dataManager;
        public event EventHandler OrderProductsChanged;
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? OrderId
        {
            get
            {
                object o = ViewState["OrderId"];
                return (o == null ? null : (Guid?)o);
            }
            set
            {
                ViewState["OrderId"] = value;
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
        public DateTime? OrderDate
        {
            get
            {
                if (ViewState["OrderDate"] == null)
                    return null;
                return (DateTime)ViewState["OrderDate"];
            }
            set { ViewState["OrderDate"] = value; }
        }



        /// <summary>
        /// Gets the order products list.
        /// </summary>
        public List<OrderProductsMap>  OrderProductsList
        {
            get { return (List<OrderProductsMap>) ViewState["OrderProducts"]; }
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

            rgOrderProducts.Culture = new CultureInfo("ru-RU");

            if (!Page.IsPostBack)
                BindData();
        }

        

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            if (OrderId != null)
                ViewState["OrderProducts"] = _dataManager.OrderProducts.SelectAll((Guid)OrderId).Select(op => new OrderProductsMap()
                                                                                                                  {
                                                                                                                    ID = op.ID,
                                                                                                                    OrderID = op.OrderID,
                                                                                                                    ProductID = op.ProductID,
                                                                                                                    AnyProductName = op.AnyProductName,
                                                                                                                    SerialNumber = op.SerialNumber,
                                                                                                                    PriceListID = op.PriceListID,
                                                                                                                    Quantity = op.Quantity,
                                                                                                                    UnitID = op.UnitID,
                                                                                                                    CurrencyID = op.CurrencyID,
                                                                                                                    Rate = op.Rate,
                                                                                                                    CurrencyPrice = op.CurrencyPrice,
                                                                                                                    CurrencyAmount = op.CurrencyAmount,
                                                                                                                    Price = op.Price,
                                                                                                                    Amount = op.Amount,
                                                                                                                    SpecialOfferPriceListID = op.SpecialOfferPriceListID,
                                                                                                                    Discount = op.Discount,
                                                                                                                    CurrencyDiscountAmount = op.CurrencyDiscountAmount,
                                                                                                                    DiscountAmount = op.DiscountAmount,
                                                                                                                    CurrencyTotalAmount = op.CurrencyTotalAmount,
                                                                                                                    TotalAmount = op.TotalAmount
                                                                                                                  }).ToList();
            else
                ViewState["OrderProducts"] = new List<OrderProductsMap>();
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgOrderProducts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgOrderProducts.DataSource = ViewState["OrderProducts"];
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgOrderProducts_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {                
                var gridEditFormItem = (GridEditFormItem)e.Item;                

                var dcbProducts = (DictionaryComboBox)gridEditFormItem.FindControl("dcbProducts");
                dcbProducts.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "ProductStatusID", DbType = DbType.Int32, Value = ((int)ProductStatus.Current).ToString()});
                dcbProducts.SiteID = SiteId;
                dcbProducts.BindData();

                var dcbPriceList = (PriceListComboBox)gridEditFormItem.FindControl("dcbPriceList");                
                dcbPriceList.SiteId = SiteId;
                if (PriceListId.HasValue && PriceListId != Guid.Empty)
                    dcbPriceList.SelectedId = (Guid)PriceListId;
                else
                {
                    dcbPriceList.SelectPriceListType = PriceListType.PriceList;
                    dcbPriceList.SelectPriceListStatus = PriceListStatus.Current;
                }
                dcbPriceList.OrderDate = OrderDate;
                dcbPriceList.BindData();


                var dcbCurrency = (DictionaryComboBox)gridEditFormItem.FindControl("dcbCurrency");
                dcbCurrency.SiteID = SiteId;
                dcbCurrency.BindData();

                var dcbUnit = (DictionaryComboBox)gridEditFormItem.FindControl("dcbUnit");
                dcbUnit.SiteID = SiteId;
                dcbUnit.BindData();

                var dcbDiscountPriceList = (PriceListComboBox)gridEditFormItem.FindControl("dcbSpecialOfferPriceList");
                dcbDiscountPriceList.SiteId = SiteId;                
                dcbDiscountPriceList.OrderDate = OrderDate;
                dcbDiscountPriceList.BindData();

                var item = e.Item as GridEditableItem;
                
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    var orderProduct = (OrderProductsMap)item.DataItem;                    

                    ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId = orderProduct.ProductID;
                    ((TextBox)item.FindControl("txtAnyProductName")).Text = orderProduct.AnyProductName;
                    ((TextBox)item.FindControl("txtSerialNumber")).Text = orderProduct.SerialNumber;
                    ((PriceListComboBox)item.FindControl("dcbPriceList")).SelectedId = orderProduct.PriceListID;
                    ((RadNumericTextBox)item.FindControl("rntxtQuantity")).Value = (double)orderProduct.Quantity;
                    ((DictionaryComboBox)item.FindControl("dcbCurrency")).SelectedId = orderProduct.CurrencyID;
                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyPrice")).Value = (double)orderProduct.CurrencyPrice;
                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyAmount")).Value = (double)orderProduct.CurrencyAmount;
                    ((DictionaryComboBox)item.FindControl("dcbUnit")).SelectedId = orderProduct.UnitID;
                    ((RadNumericTextBox)item.FindControl("rntxtRate")).Value = (double)orderProduct.Rate;
                    ((RadNumericTextBox)item.FindControl("rntxtPrice")).Value = (double)orderProduct.Price;
                    ((RadNumericTextBox)item.FindControl("rntxtAmount")).Value = (double)orderProduct.Amount;

                    if (orderProduct.SpecialOfferPriceListID.HasValue)
                        ((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId = (Guid)orderProduct.SpecialOfferPriceListID;

                    if (orderProduct.CurrencyDiscountAmount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value = (double)orderProduct.CurrencyDiscountAmount;

                    if (orderProduct.DiscountAmount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value = (double)orderProduct.DiscountAmount;

                    if (orderProduct.Discount.HasValue)
                        ((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value = (double)orderProduct.Discount;

                    ((RadNumericTextBox)item.FindControl("rntxtCurrencyTotalAmount")).Value = (double)orderProduct.CurrencyTotalAmount;
                    ((RadNumericTextBox)item.FindControl("rntxtTotalAmount")).Value = (double)orderProduct.TotalAmount;
                }
            }
            else if (e.Item is GridDataItem)
            {
                var orderProduct = e.Item.DataItem as OrderProductsMap;
                ((Literal) e.Item.FindControl("lrlProductName")).Text = _dataManager.Product.SelectNameById(orderProduct.ProductID);
                ((Literal)e.Item.FindControl("lrlUnitName")).Text = _dataManager.Unit.SelectNameById(orderProduct.UnitID);
                ((Literal)e.Item.FindControl("lrlCurrencyName")).Text = _dataManager.Currency.SelectNameById(orderProduct.CurrencyID);
                ((Literal) e.Item.FindControl("lrlStockQuantity")).Text = _dataManager.OrderProducts.StockQuantity(orderProduct.ID, orderProduct.ProductID).ToString();

            }
        }        



        /// <summary>
        /// Handles the DeleteCommand event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgOrderProducts_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<OrderProductsMap>)ViewState["OrderProducts"]).Remove(
                ((List<OrderProductsMap>)ViewState["OrderProducts"]).Where(s => s.ID == id).FirstOrDefault());

            if (OrderProductsChanged != null)
                OrderProductsChanged(this, new EventArgs());
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgOrderProducts_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            
            SaveToViewState(Guid.Empty, item);

            rgOrderProducts.MasterTableView.IsItemInserted = false;
            rgOrderProducts.Rebind();
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgOrderProducts_UpdateCommand(object sender, GridCommandEventArgs e)
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
            var orderProduct = ((List<OrderProductsMap>)ViewState["OrderProducts"]).Where(s => s.ID == orderProductId).FirstOrDefault() ?? new OrderProductsMap();            

            orderProduct.ProductID = ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId;
            orderProduct.AnyProductName = ((TextBox)item.FindControl("txtAnyProductName")).Text;
            orderProduct.SerialNumber = ((TextBox)item.FindControl("txtSerialNumber")).Text;
            orderProduct.PriceListID = ((PriceListComboBox)item.FindControl("dcbPriceList")).SelectedId;
            orderProduct.Quantity = (decimal)((RadNumericTextBox)item.FindControl("rntxtQuantity")).Value;
            orderProduct.CurrencyID = ((DictionaryComboBox)item.FindControl("dcbCurrency")).SelectedId;
            orderProduct.CurrencyPrice = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyPrice")).Value;
            orderProduct.CurrencyAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyAmount")).Value;
            orderProduct.UnitID = ((DictionaryComboBox)item.FindControl("dcbUnit")).SelectedId;
            orderProduct.Rate = (decimal)((RadNumericTextBox)item.FindControl("rntxtRate")).Value;
            orderProduct.Price = (decimal)((RadNumericTextBox)item.FindControl("rntxtPrice")).Value;
            orderProduct.Amount = (decimal)((RadNumericTextBox)item.FindControl("rntxtAmount")).Value;

            if (((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId != Guid.Empty)
                orderProduct.SpecialOfferPriceListID = ((PriceListComboBox)item.FindControl("dcbSpecialOfferPriceList")).SelectedId;
            else
                orderProduct.SpecialOfferPriceListID = null;

            if (((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value.HasValue)
                orderProduct.CurrencyDiscountAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyDiscountAmount")).Value;
            else
                orderProduct.CurrencyDiscountAmount = null;

            if (((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value.HasValue)
                orderProduct.DiscountAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtDiscountAmount")).Value;
            else
                orderProduct.DiscountAmount = null;

            if (((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value.HasValue)
                orderProduct.Discount = (decimal)((RadNumericTextBox)item.FindControl("rntxtDiscount")).Value;
            else
                orderProduct.Discount = null;

            orderProduct.CurrencyTotalAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtCurrencyTotalAmount")).Value;
            orderProduct.TotalAmount = (decimal)((RadNumericTextBox)item.FindControl("rntxtTotalAmount")).Value;

            if (orderProduct.ID == Guid.Empty)
            {
                orderProduct.ID = Guid.NewGuid();
                ((List<OrderProductsMap>) ViewState["OrderProducts"]).Add(orderProduct);
            }

            if (OrderProductsChanged != null)
                OrderProductsChanged(this, new EventArgs());
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
                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).ProductId = product.ID;
                if (PriceListId.HasValue && PriceListId != Guid.Empty)
                    ((PriceListComboBox) dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId = (Guid)PriceListId;
                else
                {
                    ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectPriceListType = PriceListType.PriceList;
                    ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectPriceListStatus = PriceListStatus.Current;
                }
                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).BindData();

                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbSpecialOfferPriceList")).ProductId = product.ID;
                ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbSpecialOfferPriceList")).BindData();
                
                if (product.UnitID.HasValue)
                    ((DictionaryComboBox) dcbProducts.Parent.FindControl("dcbUnit")).SelectedId = (Guid)product.UnitID;

                UpdateCurrencyPrice(product.ID, ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId, dcbProducts.Parent);
            }
            else
                UpdateCurrencyPrice(Guid.Empty, ((PriceListComboBox)dcbProducts.Parent.FindControl("dcbPriceList")).SelectedId, dcbProducts.Parent);
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
            RecalculatePrices(FindControlRecursive(rgOrderProducts, "plEditForm"), e.Argument);            
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
    }
}
