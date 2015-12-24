using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class ProductComplectation : System.Web.UI.UserControl
    {
        private DataManager dataManager = new DataManager();

        [Serializable]
        private class ProductComplectationStructure
        {
            public Guid ID { get; set; }
            public string Title{ get; set; }
            public Guid BaseProductID{ get; set; }
            public Guid ProductID{ get; set; }
            public double Quantity{ get; set; }
            public decimal Price { get; set; }

        }

        private List<ProductComplectationStructure> _productComplectationStructure = new List<ProductComplectationStructure>();

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
                BindProducts();
                rgProductComplectation.Culture = new CultureInfo("ru-RU");
            }

        }



        private void BindProducts()
        {

            foreach (var v in dataManager.ProductComplectation.SelectByBaseProductId(ProductID).ToList())
            {
                _productComplectationStructure.Add(new ProductComplectationStructure()
                                                       {
                                                           ID = v.ID,
                                                           BaseProductID = v.BaseProductID,
                                                           ProductID = v.ProductID,
                                                           Quantity = v.Quantity,
                                                           Title = v.tbl_Product1.Title,
                                                           Price = v.Price
                                                       });
            }
            ViewState["ProductComplectation"] = _productComplectationStructure;
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgProductComplectation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgProductComplectation_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;

                var dropDownList = (DictionaryComboBox)gridEditFormItem["Title"].FindControl("dcbProducts");
                dropDownList.DictionaryName = "tbl_Product";
                dropDownList.SiteID = CurrentUser.Instance.SiteID;
                dropDownList.BindData();

                if (!(gridEditFormItem.DataItem is GridInsertionObject))
                {
                    var productComplectation = (ProductComplectationStructure)gridEditFormItem.DataItem;

                    if (productComplectation != null)
                    {
                        dropDownList.SelectedId = productComplectation.ProductID;
                    }
                }
            }
        }



        /// <summary>
        /// Handles the NeedDataSource event of the rgProductComplectation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void rgProductComplectation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgProductComplectation.DataSource = ViewState["ProductComplectation"];
        }



        /// <summary>
        /// Handles the UpdateCommand event of the rgProductComplectation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductComplectation_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var id = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var productComplectation = ((List<ProductComplectationStructure>)ViewState["ProductComplectation"]).Where(s => s.ID == id).FirstOrDefault();
            productComplectation.ProductID = ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId;
            productComplectation.Quantity = double.Parse(((RadNumericTextBox)item.FindControl("txtQuantity")).Text);
            productComplectation.Price = decimal.Parse(((RadNumericTextBox)item.FindControl("txtPrice")).Text);
        }



        /// <summary>
        /// Handles the InsertCommand event of the rgProductComplectation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductComplectation_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var productComplectation = new ProductComplectationStructure();
            productComplectation.ID = Guid.NewGuid();
            productComplectation.BaseProductID = ProductID;
            productComplectation.ProductID = ((DictionaryComboBox)item.FindControl("dcbProducts")).SelectedId;
            productComplectation.Quantity = double.Parse(((RadNumericTextBox)item.FindControl("txtQuantity")).Text);
            productComplectation.Price = decimal.Parse(((RadNumericTextBox)item.FindControl("txtPrice")).Text);
            productComplectation.Title =
                dataManager.Product.SelectById(SiteID, productComplectation.ProductID).Title;
            //productComplectation.tbl_Product1.Title =
            //    dataManager.Product.SelectById(SiteID, productComplectation.ProductID).Title;
            ((List<ProductComplectationStructure>)ViewState["ProductComplectation"]).Add(productComplectation);
        }



        /// <summary>
        /// Handles the DeleteCommand event of the rgProductComplectation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgProductComplectation_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<ProductComplectationStructure>)ViewState["ProductComplectation"]).Remove(
                ((List<ProductComplectationStructure>)ViewState["ProductComplectation"]).Where(s => s.ID == id).FirstOrDefault());
        }




        public void SaveComplectation(Guid BaseProductID)
        {
            List<tbl_ProductComplectation> productComplectation = new List<tbl_ProductComplectation>();

            foreach (var v in (List<ProductComplectationStructure>)ViewState["ProductComplectation"])
            {
                productComplectation.Add(new tbl_ProductComplectation() { ID = v.ID, BaseProductID = BaseProductID, ProductID = v.ProductID, Quantity = v.Quantity, Price = v.Price});
            }

            dataManager.ProductComplectation.DeleteAll(BaseProductID);
            dataManager.ProductComplectation.Add(productComplectation);

        }

    }
}
