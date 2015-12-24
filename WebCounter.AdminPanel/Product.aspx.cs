using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using Labitec.UI.BaseWorkspace;
using Labitec.UI.Dictionary;
using Labitec.UI.Photo;
using Labitec.UI.Photo.Controls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Product : LeadForceBasePage
    {
        public Guid _productID;
        protected tbl_Product productData = null;

        protected List<tbl_ProductPhoto> UploadedProductPhoto = new List<tbl_ProductPhoto>();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {


            Title = "Карточка продукта - LeadForce";
            
            if (Page.RouteData.Values["id"] != null)
                _productID = Guid.Parse(Page.RouteData.Values["id"] as string);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ddlProductType, radTabs, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ddlProductType, RadMultiPage1, null, UpdatePanelRenderMode.Inline);            

            hlCancel.NavigateUrl = UrlsData.AP_Products();

            InitImageGallery();

            tagsProduct.ObjectID = _productID;

            if (!Page.IsPostBack)
            {
                BindData();
            }
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            dcbManufacturer.SiteID = SiteId;
            //dcbManufacturer.EditLink = UrlsData.AP_Company(Guid.Empty);
            
            dcbSupplier.SiteID = SiteId;
            //dcbSupplier.EditLink = UrlsData.AP_Company(Guid.Empty);


            sccCategory.ValidationGroup = ValidationSummary.ValidationGroup;
            sccCategory.SiteID = SiteId;

            dcbCountry.Order = new List<DictionaryComboBox.DictionaryOrderColumn>();
            dcbCountry.Order.Add(new DictionaryComboBox.DictionaryOrderColumn() { Name = "it.[Name]", Direction = "ASC" });

            ProductComplectation.SiteID = SiteId;
            ProductPrice.SiteID = SiteId;

            BindProductType();
            BindProductStatus();
            BindBrand();
            BindUnit();

            productData = DataManager.Product.SelectById(SiteId, _productID);

            ProductComplectation.ProductID = Guid.Empty;
            ProductPrice.ProductID = Guid.Empty;

            if (productData != null)
            {

                BindImageGallery(productData.ID);
                ProductComplectation.ProductID = productData.ID;
                ProductPrice.ProductID = productData.ID;
                txtSKU.Text = productData.SKU;
                txtTitle.Text = productData.Title;
                ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(productData.ProductTypeID.ToString()));
                txtPrice.Text = productData.Price == null ? "0,00" : ((decimal)productData.Price).ToString("F");
                ddlProductStatus.SelectedIndex = ddlProductStatus.Items.IndexOf(ddlProductStatus.Items.FindByValue(productData.ProductStatusID.ToString()));
                ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(ddlBrand.Items.FindByValue(productData.BrandID.ToString()));
                ddlUnit.SelectedIndex = ddlUnit.Items.IndexOf(ddlUnit.Items.FindByValue(productData.UnitID.ToString()));
                txtWholesalePrice.Text = productData.WholesalePrice == null ? "0,00" : ((decimal)productData.WholesalePrice).ToString("F");
                ucHtmlEditor.Content = productData.Description;
                sccCategory.SelectedCategoryId = productData.ProductCategoryID == null ? Guid.Empty:(Guid)productData.ProductCategoryID;


                dcbCountry.SelectedId = productData.CountryID == null ? Guid.Empty:(Guid)productData.CountryID;
                dcbManufacturer.SelectedId = productData.ManufacturerID == null ? Guid.Empty : (Guid)productData.ManufacturerID;
                dcbSupplier.SelectedId = productData.SupplierID == null ? Guid.Empty : (Guid)productData.SupplierID;


                txtSupplierSKU.Text = productData.SupplierSKU;

                
            }
            sccCategory.BindData();            

        }



        private void BindProductType()
        {
            ddlProductType.DataSource = DataManager.ProductType.SelectAll(SiteId);
            ddlProductType.DataValueField = "ID";
            ddlProductType.DataTextField = "Title";
            ddlProductType.DataBind();
        }


        private void BindProductStatus()
        {
            ddlProductStatus.Items.Clear();
            foreach (var productStatus in EnumHelper.EnumToList<ProductStatus>())
                ddlProductStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(productStatus), ((int)productStatus).ToString()));
        }



        private void BindBrand()
        {
            ddlBrand.DataSource = DataManager.Brand.SelectBySiteId(SiteId);
            ddlBrand.DataValueField = "ID";
            ddlBrand.DataTextField = "Title";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem() { Text = "Выберите значение", Value = Guid.Empty.ToString() });
        }



        private void BindUnit()
        {
            ddlUnit.DataSource = DataManager.Unit.SelectBySiteId(SiteId);
            ddlUnit.DataValueField = "ID";
            ddlUnit.DataTextField = "Title";
            ddlUnit.DataBind();
        }

        

        /// <summary>
        /// Updates the photo.
        /// </summary>
        /// <param name="productId">The product id.</param>
        protected void UpdateProductPhotos(Guid productId)
        {
            foreach (var productPhoto in UploadedProductPhoto)
            {
                productPhoto.ProductId = productId;
                DataManager.ProductPhoto.Save(productPhoto);
            }

            var photos = labitecImageGallery.Photos;
            foreach (var photo in photos)
            {
                var productPhoto = DataManager.ProductPhoto.Select(photo.PhotoId);

                if (photo.IsDeleted)
                {
                    FileStorageProvider.Delete(BusinessLogicLayer.Configuration.Settings.ProductImageAbsolutePath(CurrentUser.Instance.SiteID), productPhoto.Photo);
                    FileStorageProvider.Delete(BusinessLogicLayer.Configuration.Settings.ProductImageAbsolutePath(CurrentUser.Instance.SiteID), productPhoto.Preview);
                    DataManager.ProductPhoto.Delete(photo.PhotoId);
                }
                else
                {                    
                    DataManager.ProductPhoto.Save(productPhoto.Id, productPhoto.ProductId, productPhoto.Photo,
                                                productPhoto.Preview, photo.Description, photo.IsMain);
                }
            }
        }


        #region Image gallery

        /// <summary>
        /// Inits the image gallery.
        /// </summary>
        private void InitImageGallery()
        {
            labitecUploadPhoto.ImageUploaded += labitecUploadPhoto_ImageUploaded;
            labitecUploadPhoto.UploadPath = BusinessLogicLayer.Configuration.Settings.ProductImageAbsolutePath(CurrentUser.Instance.SiteID);
            var sSize = ConfigurationManager.AppSettings["ProductThumbnailImageSize"].Split('x');
            labitecUploadPhoto.ThumbnailSize = new Size(int.Parse(sSize[0]), int.Parse(sSize[1]));
            labitecUploadPhoto.AllowedFileExtensions = ConfigurationManager.AppSettings["ValidImageExtensions"].Split(new char[] { ',' }).Select(p => p.Trim()).ToArray();
            labitecImageGallery.NoPhotoUrl = BusinessLogicLayer.Configuration.Settings.ProductImageVirtualPath() + ConfigurationManager.AppSettings["NoPhotoUrl"];
        }



        /// <summary>
        /// Binds the image gallery.
        /// </summary>
        private void BindImageGallery(Guid productId)
        {
            var productPhotos = DataManager.ProductPhoto.SelectByProductId(productId);
            var photos = productPhotos.Select(productPhoto => new Photo()
            {
                PhotoId = productPhoto.Id,
                Description = productPhoto.Description,
                IsMain = productPhoto.IsMain,
                PhotoUrl = BusinessLogicLayer.Configuration.Settings.ProductImageVirtualPath(CurrentUser.Instance.SiteID) + productPhoto.Photo,
                ThumbnailUrl = BusinessLogicLayer.Configuration.Settings.ProductImageVirtualPath(CurrentUser.Instance.SiteID) + productPhoto.Preview
            }).ToList();
            labitecImageGallery.Photos = photos;
            labitecImageGallery.BindData();
            labitecImageGallery.NoPhotoUrl = BusinessLogicLayer.Configuration.Settings.ProductImageVirtualPath(CurrentUser.Instance.SiteID) + ConfigurationManager.AppSettings["NoPhotoUrl"];
        }



        /// <summary>
        /// Handles the ImageUploaded event of the labitecUploadPhoto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.UI.Photo.Controls.ImageUploadedEventArgs"/> instance containing the event data.</param>
        protected void labitecUploadPhoto_ImageUploaded(object sender, ImageUploadedEventArgs e)
        {            
            UploadedProductPhoto.Add(new tbl_ProductPhoto() { Photo = e.ImageFileName, Preview = e.ThumbnailImageFileName, Description = string.Empty, IsMain = false });
        }

        #endregion        


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            productData = DataManager.Product.SelectById(SiteId, _productID);

            var isUpdate = productData != null;

            productData = DataManager.Product.Save(
                                      SiteId,
                                      productData == null ? Guid.Empty : productData.ID, 
                                      txtTitle.Text, 
                                      txtSKU.Text,
                                      sccCategory.SelectedCategoryId,
                                      ddlProductType.SelectedValue == "" ? Guid.Empty:Guid.Parse(ddlProductType.SelectedValue),
                                      txtPrice.Text == "" ? null : (decimal?)(decimal.Parse(txtPrice.Text)),
                                      int.Parse(ddlProductStatus.SelectedValue),
                                      ddlUnit.SelectedValue == "" ? null : (Guid?)Guid.Parse(ddlUnit.SelectedValue),
                                      txtWholesalePrice.Text == "" ? null : (decimal?)decimal.Parse(txtWholesalePrice.Text),
                                      dcbSupplier.SelectedId == Guid.Empty ? null : (Guid?)dcbSupplier.SelectedId,
                                      dcbManufacturer.SelectedId == Guid.Empty ? null : (Guid?)dcbManufacturer.SelectedId,
                                      txtSupplierSKU.Text,
                                      dcbCountry.SelectedId == Guid.Empty ? null : (Guid?)dcbCountry.SelectedId,
                                      ucHtmlEditor.Content,
                                      CurrentUser.Instance.ID,
                                      ddlBrand.SelectedValue == Guid.Empty.ToString() ? null : (Guid?)Guid.Parse(ddlBrand.SelectedValue)
                                      );

            UpdateProductPhotos(productData.ID);

            BindImageGallery(productData.ID);

            ProductComplectation.SaveComplectation(productData.ID);
            ProductPrice.SavePrice(productData.ID);

            tagsProduct.SaveTags(productData.ID);

            if (!isUpdate)
                Response.Redirect(UrlsData.AP_ProductEdit(productData.ID));

        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the ddlProductType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlProductType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var productType = DataManager.ProductType.SelectById(Guid.Parse(ddlProductType.SelectedValue));

            var productComplectationTab = radTabs.Tabs.SingleOrDefault(o => o.Value == "ProductComplectation");

            if (productType != null && productType.ProductWorkWithComplectationID != (int)ProductWorkWithComplectation.Simple)
            {
                productComplectationTab.Visible = true;                
            }
            else
            {
                productComplectationTab.Visible = false;
                if (productComplectationTab.Selected)
                {
                    radTabs.SelectedIndex = 0;
                    RadMultiPage1.SelectedIndex = 0;
                }
            }
        }
    }
}