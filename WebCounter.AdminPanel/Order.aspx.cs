using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Order : LeadForceBasePage
    {
        private Guid _orderId;
        public Access access;

        protected void Page_Load(object sender, EventArgs e)
        {
            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            Title = "Заказ - LeadForce";

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbOrderType, dcbOrderType);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucPriceList, ucPriceList);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rdpOrderDate, ucOrderProducts, null, UpdatePanelRenderMode.Inline);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbOrderType, plExpirationDate);            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbOrderType, plDeliveryDate);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucOrderProducts, lrlOrdered);

            dcbOrderType.SelectedIndexChanged += dcbOrderType_SelectedIndexChanged;
            ucPriceList.SelectedIndexChanged += ucPriceList_SelectedIndexChanged;
            rdpOrderDate.SelectedDateChanged += rdpOrderDate_SelectedDateChanged;
            ucOrderProducts.SiteId = SiteId;
            ucOrderProducts.OrderProductsChanged += ucOrderProducts_OrderProductsChanged;

            if (Page.RouteData.Values["id"] != null)
                _orderId = Guid.Parse(Page.RouteData.Values["id"] as string);

            ucTaskList.OrderId = _orderId;
            ucTaskList.SiteId = SiteId;

            hlCancel.NavigateUrl = UrlsData.AP_Orders();

            tagsOrder.ObjectID = _orderId;

            if (!Page.IsPostBack)
                BindData();
        }        



        /// <summary>
        /// Handles the SelectedDateChanged event of the rdpOrderDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpOrderDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {            
            ucOrderProducts.OrderDate = rdpOrderDate.SelectedDate;
        }



        /// <summary>
        /// Handles the OrderProductsChanged event of the ucOrderProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ucOrderProducts_OrderProductsChanged(object sender, EventArgs e)
        {
            lrlOrdered.Text = ucOrderProducts.OrderProductsList.Sum(orderProduct => orderProduct.TotalAmount).ToString("F");
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPriceList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucOrderProducts.PriceListId = ucPriceList.SelectedId;
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            rdpOrderDate.SelectedDate = DateTime.Now;
            ucPriceList.SiteId = dcbOrderType.SiteID = dcbExecutorCompany.SiteID = dcbBuyerCompany.SiteID = SiteId;            
            BindOrderStatus();            
            ucOrderProducts.OrderDate = DateTime.Now;            

            var order = DataManager.Order.SelectById(SiteId, _orderId);
            if (order != null)
            {
                ucOrderProducts.OrderId = _orderId;
                ucOrderProducts.OrderDate = order.CreatedAt;                
                lrlNumber.Text = order.Number;
                rdpOrderDate.SelectedDate = order.CreatedAt;
                dcbOrderType.SelectedId = order.OrderTypeID;
                ProceedOrderType(order.OrderTypeID);
                ddlOrderStatus.SelectedIndex = ddlOrderStatus.Items.IndexOf(ddlOrderStatus.Items.FindByValue(order.OrderStatusID.ToString()));
                txtNote.Text = order.Note;
                if (order.ExecutorCompanyID.HasValue)
                    dcbExecutorCompany.SelectedId = (Guid)order.ExecutorCompanyID;
                if (order.BuyerCompanyID.HasValue)
                    dcbBuyerCompany.SelectedId = (Guid)order.BuyerCompanyID;
                ucBuyerContact.SelectedValue = order.BuyerContactID;
                ucExecutorContact.SelectedValue = order.ExecutorContactID;
                rdpExpirationDateBegin.SelectedDate = order.ExpirationDateBegin;
                rdpExpirationDateEnd.SelectedDate = order.ExpirationDateEnd;
                rdpPlannedDeliveryDate.SelectedDate = order.PlannedDeliveryDate;
                rdpActualDeliveryDate.SelectedDate = order.ActualDeliveryDate;
                if (order.PriceListID.HasValue)
                    ucPriceList.SelectedId = (Guid)order.PriceListID;                
                lrlOrdered.Text = order.Ordered.ToString("F");
                lrlPaid.Text = order.Paid.ToString("F");
                lrlShipped.Text = order.Shipped.ToString("F");
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the dcbOrderType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        private void dcbOrderType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ProceedOrderType(dcbOrderType.SelectedId);                                    
        }



        /// <summary>
        /// Proceeds the type of the order.
        /// </summary>
        /// <param name="orderTypeId">The order type id.</param>
        private void ProceedOrderType(Guid orderTypeId)
        {
            var orderType = DataManager.OrderType.SelectById(SiteId, orderTypeId);
            if (orderType != null)
            {
                switch ((ExpirationAction) orderType.ExpirationActionID)
                {
                    case ExpirationAction.None:
                        plExpirationDate.Visible = false;
                        rdpExpirationDateBegin.SelectedDate = null;
                        rdpExpirationDateEnd.SelectedDate = null;
                        break;
                    case ExpirationAction.MoneyBack:
                    case ExpirationAction.Close:
                        plExpirationDate.Visible = true;
                        break;
                }

                plDeliveryDate.Visible = orderType.IsPhysicalDelivery;                
            }
            else
            {
                plExpirationDate.Visible = false;                
                plDeliveryDate.Visible = false;                
            }
        }




        /// <summary>
        /// Binds the order status.
        /// </summary>
        private void BindOrderStatus()
        {
            foreach (var orderStatus in EnumHelper.EnumToList<OrderStatus>())
                ddlOrderStatus.Items.Add(new ListItem(EnumHelper.GetEnumDescription(orderStatus), ((int)orderStatus).ToString()));
            ddlOrderStatus.Items.Insert(0, new ListItem("Выберите значение", Guid.Empty.ToString()) { Selected = true });
            rfvOrderstatus.InitialValue = Guid.Empty.ToString();
            ddlOrderStatus.SelectedIndex = ddlOrderStatus.Items.IndexOf(ddlOrderStatus.Items.FindByValue(((int) OrderStatus.InPlans).ToString()));
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSave_OnClick(object sender, EventArgs e)
        {
            if (!access.Write)
                return;

            var order = DataManager.Order.SelectById(SiteId, _orderId) ?? new tbl_Order();
            
            order.CreatedAt = (DateTime)rdpOrderDate.SelectedDate;
            order.OrderTypeID = dcbOrderType.SelectedId;
            order.OrderStatusID = int.Parse(ddlOrderStatus.SelectedValue);
            order.Note = txtNote.Text;
            
            if (dcbExecutorCompany.SelectedId != Guid.Empty)
                order.ExecutorCompanyID = dcbExecutorCompany.SelectedId;
            else
                order.ExecutorCompanyID = null;

            if (dcbBuyerCompany.SelectedId != Guid.Empty)
                order.BuyerCompanyID = dcbBuyerCompany.SelectedId;
            else
                order.BuyerCompanyID = null;

            order.BuyerContactID = ucBuyerContact.SelectedValue;                        
            order.ExecutorContactID = ucExecutorContact.SelectedValue;            

            order.ExpirationDateBegin = rdpExpirationDateBegin.SelectedDate;
            order.ExpirationDateEnd = rdpExpirationDateEnd.SelectedDate;
            order.PlannedDeliveryDate = rdpPlannedDeliveryDate.SelectedDate;
            order.ActualDeliveryDate = rdpActualDeliveryDate.SelectedDate;

            if (ucPriceList.SelectedId != Guid.Empty)
                order.PriceListID = ucPriceList.SelectedId;
            else
                order.PriceListID = null;            

            order.Ordered = ucOrderProducts.OrderProductsList.Sum(orderProduct => orderProduct.TotalAmount);

            if (order.ID == Guid.Empty)
            {
                order.SiteID = SiteId;
                order.OwnerID = CurrentUser.Instance.ContactID;

                var orderType = DataManager.OrderType.SelectById(SiteId, order.OrderTypeID);
                if (orderType != null && orderType.NumeratorID.HasValue)
                {
                    var documentNumerator = DocumentNumerator.GetNumber((Guid)orderType.NumeratorID, order.CreatedAt, orderType.tbl_Numerator.Mask, "tbl_Order");
                    order.Number = documentNumerator.Number;
                    order.SerialNumber = documentNumerator.SerialNumber;
                }

                DataManager.Order.Add(order);
            }
            else
                DataManager.Order.Update(order);
            
            DataManager.OrderProducts.Update(ucOrderProducts.OrderProductsList, order.ID);

            tagsOrder.SaveTags(order.ID);

            Response.Redirect(UrlsData.AP_Orders());
        }
    }
}