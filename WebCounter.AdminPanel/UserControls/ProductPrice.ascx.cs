using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class ProductPrice : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class ProductPriceStructure
        {
            public Guid ID { get; set; }
            public string PriceListTitle{ get; set; }
            public Guid PriceListID { get; set; }
            public string SupplierTitle { get; set; }
            public Guid? SupplierID { get; set; }
            public Guid ProductID { get; set; }
            public DateTime? DateFrom { get; set; }
            public DateTime? DateTo { get; set; }
            public double? QuantityFrom { get; set; }
            public double? QuantityTo { get; set; }
            public double? Discount { get; set; }
            public double? Price { get; set; }
        }

        private List<ProductPriceStructure> _productPriceStructure = new List<ProductPriceStructure>();

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid ProductID
        {
            get
            {
                object o = ViewState["ProductID"];
                return (o == null ? Guid.Empty : (Guid)o);
            }
            set
            {
                ViewState["ProductID"] = value;
            }
        }



        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid SiteID
        {
            get
            {
                if (ViewState["SiteID"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["SiteID"];
            }
            set { ViewState["SiteID"] = value; }
        }




        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPrices();
                rgProductPrice.Culture = new CultureInfo("ru-RU");
            }

        }


        private void BindPrices()
        {

            foreach (var v in dataManager.ProductPrice.SelectByProductId(ProductID).ToList())
            {
                var priceListTitle = v.tbl_PriceList.Title + "(" + v.tbl_PriceList.tbl_PriceListType.Title + ")";
                _productPriceStructure.Add(new ProductPriceStructure() { ID = v.ID, DateFrom = v.DateFrom, DateTo = v.DateTo, Discount = v.Discount, Price = v.Price, PriceListID = v.PriceListID, PriceListTitle = priceListTitle, ProductID = v.ProductID, QuantityFrom = v.QuantityFrom, QuantityTo = v.QuantityTo, SupplierTitle = v.SupplierID != null ? v.tbl_Company1.Name:"", SupplierID = v.SupplierID});
            }
            ViewState["ProductPrice"] = _productPriceStructure;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgProductPrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgProductPrice_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var rfv = (RequiredFieldValidator)gridEditFormItem["PriceListTitle"].FindControl("RequiredFieldValidator4");
                rfv.InitialValue = Guid.Empty.ToString();

                var ddlPriceList = (DropDownList)gridEditFormItem["PriceListTitle"].FindControl("ddlPriceList");
                foreach (var v in dataManager.PriceList.SelectAll(SiteID).ToList())
                {
                    var priceListTitle = v.Title + "(" + v.tbl_PriceListType.Title + ")";
                    ddlPriceList.Items.Add(new ListItem(priceListTitle, v.ID.ToString()));
                }
                ddlPriceList.Items.Insert(0, new ListItem("Выберите значение", Guid.Empty.ToString()));

                
                var dcbSupplier = (DictionaryComboBox)gridEditFormItem["SupplierTitle"].FindControl("dcbSupplier");

                dcbSupplier.SiteID = SiteID;
                //dcbSupplier.EditLink = UrlsData.AP_Company(Guid.Empty);
                dcbSupplier.BindData();

                if (!(gridEditFormItem.DataItem is GridInsertionObject))
                {
                    var productComplectation = (ProductPriceStructure)gridEditFormItem.DataItem;

                    if (productComplectation != null)
                    {
                        ddlPriceList.SelectedIndex = ddlPriceList.Items.IndexOf(ddlPriceList.Items.FindByValue(productComplectation.PriceListID.ToString()));
                        dcbSupplier.SelectedIdNullable = productComplectation.SupplierID;
                    }
                }
            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgProductPrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgProductPrice_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgProductPrice.DataSource = ViewState["ProductPrice"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgProductPrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductPrice_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var productPrice = ((List<ProductPriceStructure>)ViewState["ProductPrice"]).FirstOrDefault(s => s.ID == id);
            productPrice.ProductID = ProductID;
            productPrice.DateFrom = (((RadDatePicker)item["DateFrom"].Controls[0])).SelectedDate;
            productPrice.DateTo = (((RadDatePicker)item["DateTo"].Controls[0])).SelectedDate;
            productPrice.QuantityFrom = ((RadNumericTextBox)item["QuantityFrom"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["QuantityFrom"].Controls[0]).Text);
            productPrice.QuantityTo = ((RadNumericTextBox)item["QuantityTo"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["QuantityTo"].Controls[0]).Text);
            productPrice.Discount = ((RadNumericTextBox)item["Discount"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["Discount"].Controls[0]).Text);
            productPrice.Price = ((RadNumericTextBox)item["Price"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["Price"].Controls[0]).Text);
            productPrice.SupplierID = ((DictionaryComboBox)item.FindControl("dcbSupplier")).SelectedIdNullable;
            productPrice.SupplierTitle =
                ((DictionaryComboBox) item.FindControl("dcbSupplier")).SelectedIdNullable.HasValue
                    ? dataManager.Company.SelectById(SiteID,((DictionaryComboBox) item.FindControl("dcbSupplier")).SelectedId).Name
                    : string.Empty;
            productPrice.PriceListID = Guid.Parse(((DropDownList)item.FindControl("ddlPriceList")).SelectedValue);
            var v = dataManager.PriceList.SelectById(productPrice.PriceListID);
            var priceListTitle = v.Title + "(" + v.tbl_PriceListType.Title + ")";
            productPrice.PriceListTitle = priceListTitle;
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgProductPrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductPrice_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var productPrice = new ProductPriceStructure();
            productPrice.ID = Guid.NewGuid();
            productPrice.ProductID = ProductID;
            productPrice.DateFrom = (((RadDatePicker)item["DateFrom"].Controls[0])).SelectedDate;
            productPrice.DateTo = (((RadDatePicker)item["DateTo"].Controls[0])).SelectedDate;
            productPrice.QuantityFrom = ((RadNumericTextBox)item["QuantityFrom"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["QuantityFrom"].Controls[0]).Text);
            productPrice.QuantityTo = ((RadNumericTextBox)item["QuantityTo"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["QuantityTo"].Controls[0]).Text);
            productPrice.Discount = ((RadNumericTextBox)item["Discount"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["Discount"].Controls[0]).Text);
            productPrice.Price = ((RadNumericTextBox)item["Price"].Controls[0]).Text == "" ? null : (double?)Double.Parse(((RadNumericTextBox)item["Price"].Controls[0]).Text);
            productPrice.SupplierID = ((DictionaryComboBox)item.FindControl("dcbSupplier")).SelectedIdNullable;
            productPrice.SupplierTitle = ((DictionaryComboBox)item.FindControl("dcbSupplier")).SelectedId != Guid.Empty ?
                dataManager.Company.SelectById(SiteID, ((DictionaryComboBox)item.FindControl("dcbSupplier")).SelectedId).Name
                : "";
            productPrice.PriceListID = Guid.Parse(((DropDownList)item.FindControl("ddlPriceList")).SelectedValue);
            var v = dataManager.PriceList.SelectById(productPrice.PriceListID);
            var priceListTitle = v.Title + "(" + EnumHelper.GetEnumDescription((PriceListType)int.Parse(v.PriceListTypeID.ToString())) + ")";
            productPrice.PriceListTitle = priceListTitle;
            ((List<ProductPriceStructure>)ViewState["ProductPrice"]).Add(productPrice);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgProductPrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductPrice_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<ProductPriceStructure>)ViewState["ProductPrice"]).Remove(
                ((List<ProductPriceStructure>)ViewState["ProductPrice"]).Where(s => s.ID == id).FirstOrDefault());
        }




        /// <summary>
        /// Saves the price.
        /// </summary>
        /// <param name="ProductID">The product ID.</param>
        public void SavePrice(Guid ProductID)
        {
            List<tbl_ProductPrice> productPrice = new List<tbl_ProductPrice>();

            foreach (var v in (List<ProductPriceStructure>)ViewState["ProductPrice"])
            {
                productPrice.Add(new tbl_ProductPrice()
                                     {
                                         ID = v.ID,
                                         ProductID = ProductID,
                                         DateFrom = v.DateFrom,
                                         DateTo = v.DateTo,
                                         Discount = v.Discount,
                                         Price = v.Price,
                                         PriceListID = v.PriceListID,
                                         QuantityFrom = v.QuantityFrom,
                                         QuantityTo = v.QuantityTo,
                                         SupplierID = v.SupplierID
                                     });
            }

            dataManager.ProductPrice.DeleteAll(ProductID);
            dataManager.ProductPrice.Add(productPrice);

        }

    }
}