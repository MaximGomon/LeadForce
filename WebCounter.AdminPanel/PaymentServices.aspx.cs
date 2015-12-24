using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Providers.Payments.RBKMoney;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class PaymentServices : LeadForceBasePage
    {
        protected RadAjaxManager radAjaxManager = null;

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentProductId
        {
            get
            {
                if (ViewState["CurrentProductId"] == null)
                    ViewState["CurrentProductId"] = Guid.Empty;

                return (Guid) ViewState["CurrentProductId"];
            }
            set { ViewState["CurrentProductId"] = value; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid CurrentPriceId
        {
            get
            {
                if (ViewState["CurrentPriceId"] == null)
                    ViewState["CurrentPriceId"] = Guid.Empty;

                return (Guid)ViewState["CurrentPriceId"];
            }
            set { ViewState["CurrentPriceId"] = value; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Оплата услуг - LeadForce";

            radAjaxManager = RadAjaxManager.GetCurrent(Page);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, lrlSelectedProduct, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, lrlPrice, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, lrlDiscount, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, lvPriceList);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, radSliderPriceList);            
            radAjaxManager.AjaxSettings.AddAjaxSetting(radAjaxManager, rntxtPriceForPayment, null, UpdatePanelRenderMode.Inline); 
            radAjaxManager.AjaxSettings.AddAjaxSetting(radSliderPriceList, lrlDiscount, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radSliderPriceList, lrlPrice, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(radSliderPriceList, rntxtPriceForPayment, null, UpdatePanelRenderMode.Inline);
            radAjaxManager.AjaxSettings.AddAjaxSetting(rbtnPay, ucNotificationMessage, null, UpdatePanelRenderMode.Inline); 

            radAjaxManager.AjaxRequest += radAjaxManager_AjaxRequest;
            radAjaxManager.ClientEvents.OnResponseEnd = "responseEnd";
            radAjaxManager.ClientEvents.OnRequestStart = "requestStart";


            if (!Page.IsPostBack)
                BindData();
        }

        void radAjaxManager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var product = DataManager.Product.SelectById(Guid.Parse(e.Argument));
            CurrentProductId = product.ID;
            lrlSelectedProduct.Text = product.Title;            
            var productPrice = DataManager.ProductPrice.SelectByProductId(product.ID).OrderBy(o => o.QuantityFrom).OrderBy(o => o.QuantityFrom).ToList();
                        
            radSliderPriceList.Items.Clear();            

            var index = -1;
            foreach (tbl_ProductPrice price in productPrice)
            {
                index++;
                var item = new RadSliderItem(string.Empty, index.ToString());                
                radSliderPriceList.Items.Add(item);
            }
                        
            radSliderPriceList.Value = 0;
            radSliderPriceList.MaximumValue = index;

            if (productPrice.Any() && productPrice[0].Price.HasValue && productPrice[0].Discount.HasValue)
            {                
                lrlDiscount.Text = productPrice[0].Discount.ToString();
                var price = productPrice[0].Price.Value * productPrice[0].QuantityFrom.Value;
                rntxtPriceForPayment.Value = price;
                lrlPrice.Text = price.ToString("F");
                CurrentPriceId = productPrice[0].ID;
            }

            lvPriceList.DataSource = productPrice;
            lvPriceList.DataBind();            
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var site = DataManager.Sites.SelectById(SiteId);

            lrlActiveUntilDate.Text = site.ActiveUntilDate.HasValue
                                          ? site.ActiveUntilDate.Value.ToString("dd.MM.yyyy")
                                          : "Неограниченно";

            if (site.AccessProfileID.HasValue)
            {
                var accessProfile = DataManager.AccessProfile.SelectById(site.AccessProfileID.Value);
                if (accessProfile != null && accessProfile.ProductID.HasValue)
                {
                    var product = DataManager.Product.SelectById(accessProfile.ProductID.Value);
                    if (product != null && product.BrandID.HasValue)
                    {
                        var products = DataManager.Product.SelectByBrandId(product.BrandID.Value).OrderBy(o => o.Price).Select(o => new ProductInfo()
                                                                                                                  {
                                                                                                                      Id = o.ID,
                                                                                                                      Title = o.Title,
                                                                                                                      SiteId = o.SiteID,
                                                                                                                      Image = o.tbl_ProductPhoto.FirstOrDefault(x => x.IsMain),
                                                                                                                      Price = o.Price,
                                                                                                                      IsSelected = product.ID == o.ID,
                                                                                                                      Description = o.Description
                                                                                                                  });
                        if (products.Count() > 4)                        
                            rotatorProducts.Width = 160*4 + 40;
                        else
                            rotatorProducts.Width = 160 * products.Count() + 40;

                        rotatorProducts.DataSource = products;
                        rotatorProducts.DataBind();
                    }
                }
            }
        }



        /// <summary>
        /// Handles the OnValueChanged event of the radSliderPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void radSliderPriceList_OnValueChanged(object sender, EventArgs e)
        {
            var price =
                DataManager.ProductPrice.SelectByProductId(CurrentProductId).OrderBy(o => o.QuantityFrom).OrderBy(
                    o => o.QuantityFrom).ToList()[(int) radSliderPriceList.Value];

            var amount = price.Price.Value*price.QuantityFrom.Value;

            lrlPrice.Text = amount.ToString("F");
            rntxtPriceForPayment.Value = amount;
            lrlDiscount.Text = price.Discount.ToString();            

            CurrentPriceId = price.ID;
        }



        /// <summary>
        /// Handles the OnClick event of the rbtnPay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rbtnPay_OnClick(object sender, EventArgs e)
        {
            ucNotificationMessage.Text = string.Empty;
            ucNotificationMessage.MessageType = NotificationMessageType.Warning;

            try
            {
                var site = DataManager.Sites.SelectById(CurrentUser.Instance.SiteID);
                if (!site.MainUserID.HasValue)
                {
                    ucNotificationMessage.Text = "Для данного аккаунта не указан главный пользователь.";
                    return;
                }
                
                var product = DataManager.Product.SelectById(CurrentProductId);
                var price = DataManager.ProductPrice.SelectById(CurrentPriceId);

                var paymentStatus = DataManager.PaymentStatus.SelectDefault(product.SiteID);
                var currency = DataManager.Currency.SelectAll(product.SiteID).FirstOrDefault(o => o.IsDefault);

                var payment = DataManager.Payment.Save(product.SiteID,
                                         Guid.Empty,
                                         string.Format("Оплата тарифа {0} за период {1}", product.Title,
                                                       price.tbl_PriceList.Title),
                                         DateTime.Now, null, (int) PaymentType.Income, paymentStatus.ID, site.PayerCompanyID, null,
                                         null, null, currency.ID, 1, (decimal) rntxtPriceForPayment.Value, (decimal) rntxtPriceForPayment.Value, null,
                                         null);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "RBKMoney",RBKMoneyProvider.Pay(payment, site), true);
                ucNotificationMessage.MessageType = NotificationMessageType.Success;
                ucNotificationMessage.Text = "Информация об оплате успешно добавлена в систему. Сейчас вы будете перенаправлены на страницу ввода платежных реквизитов.";                
            }
            catch (Exception ex)
            {
                Log.Error("Ошибка оплаты", ex);
                ucNotificationMessage.Text = "Возникла ошибка при оплате. Повторите попытку позже.";
            }
        }
    }


    public class ProductInfo
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public tbl_ProductPhoto Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool IsSelected { get; set; }
        public string ImageUrl
        {
            get
            {
                if (this.Image != null)
                {
                    return BusinessLogicLayer.Configuration.Settings.ProductImageVirtualPath(this.SiteId) + this.Image.Photo;
                }

                return string.Empty;
            }
        }
    }
}