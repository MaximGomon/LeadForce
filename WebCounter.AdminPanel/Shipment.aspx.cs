using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labitec.UI.BaseWorkspace;
using Telerik.Web.UI;
using WebCounter.AdminPanel.UserControls.Shared;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Shipment;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel
{
    public partial class Shipment : LeadForceBasePage
    {
        private Guid _shipmentId;
        public Access access;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ((Site)Page.Master).HideInaccessibleTabs(ref RadTabStrip1, ref RadMultiPage1);
            access = Access.Check();
            if (!access.Write)
                lbtnSave.Visible = false;

            Title = "Отгрузка - LeadForce";
            
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(rdpCreatedAt, rdpCreatedAt, null,
                                                                        UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucShipmentProducts, lrlShipmentAmount, null,
                                                                        UpdatePanelRenderMode.Inline);

            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(ucShipmentProducts, ucShipmentInvoices, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbBuyerCompanyLegalAccount, ucShipmentInvoices, null, UpdatePanelRenderMode.Inline);
            RadAjaxManager.GetCurrent(Page).AjaxSettings.AddAjaxSetting(dcbExecutorCompanyLegalAccount, ucShipmentInvoices, null, UpdatePanelRenderMode.Inline);
            
            rdpCreatedAt.SelectedDateChanged += rdpShipmentDate_SelectedDateChanged;
            ucShipmentProducts.SiteId = SiteId;
            ucShipmentProducts.ShipmentProductsChanged += ucShipmentProducts_OrderProductsChanged;

            if (Page.RouteData.Values["id"] != null)
                _shipmentId = Guid.Parse(Page.RouteData.Values["id"] as string);

            hlCancel.NavigateUrl = UrlsData.AP_Shipments();

            tagsShipments.ObjectID = _shipmentId;

            ucContentComments.ContentId = _shipmentId;

            gridShipmentHistory.Where.Add(new GridWhere() {Column = "ShipmentID", Value = _shipmentId.ToString()});

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            EnumHelper.EnumToDropDownList<ShipmentStatus>(ref ddlShipmentStatus);            

            dcbShipmentType.SiteID =
                dcbExecutorCompany.SiteID =
                dcbBuyerCompany.SiteID =
                dcbBuyerCompanyLegalAccount.SiteID =
                dcbExecutorCompanyLegalAccount.SiteID = dcbOrders.SiteID = SiteId;
            rdpCreatedAt.SelectedDate = DateTime.Now;
            ucShipmentProducts.ShipmentDate = DateTime.Now;

            var filter = new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                             {
                                 Name = "IsActive",
                                 DbType = DbType.Boolean,
                                 Value = "TRUE"
                             };

            dcbBuyerCompanyLegalAccount.Filters.Add(filter);
            dcbExecutorCompanyLegalAccount.Filters.Add(filter);

            dcbExecutorCompany.BindData();
            dcbBuyerCompanyLegalAccount.BindData();
            dcbExecutorCompanyLegalAccount.BindData();

            var shipment = DataManager.Shipment.SelectById(SiteId, _shipmentId);
            if (shipment != null)
            {
                plComments.Visible = true;
                //lbtnShowSendShipmentRadWindow.Visible = true;

                ucShipmentInvoices.ShipmentId = shipment.ID;
                ucShipmentInvoices.BuyerCompanyLegalAccountId = shipment.BuyerCompanyLegalAccountID;
                ucShipmentInvoices.ExecutorCompanyLegalAccountId = shipment.ExecutorCompanyLegalAccountID;
                ucShipmentInvoices.ShipmentAmount = shipment.ShipmentAmount;

                rpbLeft.FindItemByValue("Reports", true).Visible = false;

                hlWithStamp.NavigateUrl =
                    ResolveUrl(string.Format("~/ShowShipmentReport.aspx?shipmentId={0}&ss={1}", shipment.ID, true));
                hlWithoutStamp.NavigateUrl =
                    ResolveUrl(string.Format("~/ShowShipmentReport.aspx?shipmentId={0}&ss={1}", shipment.ID, false));

                ucShipmentProducts.ShipmentId = _shipmentId;
                ucShipmentProducts.ShipmentDate = shipment.CreatedAt;
                lrlNumber.Text = shipment.Number;
                rdpCreatedAt.SelectedDate = shipment.CreatedAt;
                dcbShipmentType.SelectedId = shipment.ShipmentTypeID;
                ddlShipmentStatus.SelectedIndex =
                    ddlShipmentStatus.Items.IndexOf(ddlShipmentStatus.Items.FindByValue(shipment.ShipmentStatusID.ToString()));
                txtNote.Text = shipment.Note;
                if (shipment.ExecutorCompanyID.HasValue)
                {
                    dcbExecutorCompany.SelectedId = (Guid) shipment.ExecutorCompanyID;
                    dcbExecutorCompany.SelectedText =
                        DataManager.Company.SelectById(SiteId, (Guid) shipment.ExecutorCompanyID).Name;
                }

                if (shipment.BuyerCompanyID.HasValue)
                {
                    dcbBuyerCompany.SelectedId = (Guid) shipment.BuyerCompanyID;
                    dcbBuyerCompany.SelectedText =
                        DataManager.Company.SelectById(SiteId, (Guid) shipment.BuyerCompanyID).Name;
                    dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                                {
                                                                    Name = "CompanyID",
                                                                    DbType = DbType.Guid,
                                                                    Value = shipment.BuyerCompanyID.ToString()
                                                                });
                }

                if (shipment.ExecutorCompanyLegalAccountID.HasValue)
                {
                    dcbExecutorCompanyLegalAccount.SelectedId = (Guid) shipment.ExecutorCompanyLegalAccountID;
                    dcbExecutorCompanyLegalAccount.SelectedText =
                        DataManager.CompanyLegalAccount.SelectById((Guid) shipment.ExecutorCompanyLegalAccountID).Title;
                    dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                                   {
                                                                       Name = "CompanyID",
                                                                       DbType = DbType.Guid,
                                                                       Value = shipment.ExecutorCompanyID.ToString()
                                                                   });
                }

                if (shipment.BuyerCompanyLegalAccountID.HasValue)
                {
                    dcbBuyerCompanyLegalAccount.SelectedId = (Guid) shipment.BuyerCompanyLegalAccountID;
                    dcbBuyerCompanyLegalAccount.SelectedText =
                        DataManager.CompanyLegalAccount.SelectById((Guid) shipment.BuyerCompanyLegalAccountID).Title;
                }

                ucBuyerContact.SelectedValue = shipment.BuyerContactID;
                ucExecutorContact.SelectedValue = shipment.ExecutorContactID;

                ucSendShipmentBuyerContact.SelectedValue = shipment.BuyerContactID;
                ucSendShipmentExecutorContact.SelectedValue = shipment.ExecutorContactID;
                
                if (shipment.OrderID.HasValue)
                {
                    dcbOrders.SelectedId = (Guid) shipment.OrderID;
                    dcbOrders.SelectedText = shipment.tbl_Order.Number;
                }
                
                lrlShipmentAmount.Text = shipment.ShipmentAmount.ToString("F");
                rdpSendDate.SelectedDate = shipment.SendDate;
            }
            else
            {
                dcbExecutorCompany.SelectedIdNullable = CurrentUser.Instance.CompanyID;
                if (CurrentUser.Instance.CompanyID.HasValue)
                {
                    var company = DataManager.Company.SelectById(CurrentUser.Instance.SiteID,
                                                                 CurrentUser.Instance.CompanyID.Value);
                    if (company != null)
                    {
                        dcbExecutorCompany.SelectedText = company.Name;
                        var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(company.ID);
                        if (companyLegalAccount != null)
                        {
                            dcbExecutorCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                            dcbExecutorCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                        }
                    }
                }

                ucExecutorContact.SelectedValue = CurrentUser.Instance.ContactID;
            }
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

            var shipment = DataManager.Shipment.SelectById(SiteId, _shipmentId) ?? new tbl_Shipment();

            shipment.CreatedAt = (DateTime) rdpCreatedAt.SelectedDate;
            shipment.ShipmentTypeID = dcbShipmentType.SelectedId;
            shipment.ShipmentStatusID = int.Parse(ddlShipmentStatus.SelectedValue);
            shipment.Note = txtNote.Text;

            if (dcbExecutorCompany.SelectedId != Guid.Empty)
                shipment.ExecutorCompanyID = dcbExecutorCompany.SelectedId;
            else
                shipment.ExecutorCompanyID = null;

            if (dcbBuyerCompany.SelectedId != Guid.Empty)
                shipment.BuyerCompanyID = dcbBuyerCompany.SelectedId;
            else
                shipment.BuyerCompanyID = null;

            if (dcbExecutorCompanyLegalAccount.SelectedId != Guid.Empty)
                shipment.ExecutorCompanyLegalAccountID = dcbExecutorCompanyLegalAccount.SelectedId;
            else
                shipment.ExecutorCompanyLegalAccountID = null;

            if (dcbBuyerCompanyLegalAccount.SelectedId != Guid.Empty)
                shipment.BuyerCompanyLegalAccountID = dcbBuyerCompanyLegalAccount.SelectedId;
            else
                shipment.BuyerCompanyLegalAccountID = null;

            shipment.BuyerContactID = ucBuyerContact.SelectedValue;
            shipment.ExecutorContactID = ucExecutorContact.SelectedValue;
            shipment.SendDate = rdpSendDate.SelectedDate;
            shipment.PriceListID = null;

            if (dcbOrders.SelectedId != Guid.Empty)
                shipment.OrderID = dcbOrders.SelectedId;
            else
                shipment.OrderID = null;

            shipment.ShipmentAmount =
                ucShipmentProducts.ShipmentProductsList.Sum(shipmentProduct => shipmentProduct.TotalAmount);

            shipment.tbl_Invoice.Clear();
            var invoiceIds = ucShipmentInvoices.ShipmentInvoicesList.Select(x => x.ID).ToList();
            var invoices = DataManager.Invoice.SelectAll(SiteId).Where(
                    o => invoiceIds.Contains(o.ID));

            foreach (var invoice in invoices)            
                shipment.tbl_Invoice.Add(invoice);            
            
            if (shipment.ID == Guid.Empty)
            {
                shipment.SiteID = SiteId;
                shipment.OwnerID = CurrentUser.Instance.ContactID;
                var shipmentType = DataManager.ShipmentType.SelectById(SiteId, shipment.ShipmentTypeID);
                if (shipmentType != null)
                {
                    var documentNumerator = DocumentNumerator.GetNumber((Guid) shipmentType.NumeratorID,
                                                                        shipment.CreatedAt,
                                                                        shipmentType.tbl_Numerator.Mask, "tbl_Shipment");
                    shipment.Number = documentNumerator.Number;
                    shipment.SerialNumber = documentNumerator.SerialNumber;
                }

                DataManager.Shipment.Add(shipment);
            }
            else
                DataManager.Shipment.Update(shipment);

            DataManager.ShipmentProducts.Update(ucShipmentProducts.ShipmentProductsList, shipment.ID);

            tagsShipments.SaveTags(shipment.ID);

            Response.Redirect(UrlsData.AP_Shipments());
        }



        /// <summary>
        /// Handles the SelectedDateChanged event of the rdpShipmentDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs"/> instance containing the event data.</param>
        protected void rdpShipmentDate_SelectedDateChanged(object sender,
                                                          Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ucShipmentProducts.ShipmentDate = rdpCreatedAt.SelectedDate;
        }



        /// <summary>
        /// Handles the OrderProductsChanged event of the ucShipmentProducts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ucShipmentProducts_OrderProductsChanged(object sender, EventArgs e)
        {
            lrlShipmentAmount.Text =
                ucShipmentProducts.ShipmentProductsList.Sum(shipmentProduct => shipmentProduct.TotalAmount).ToString("F");
            ucShipmentInvoices.ShipmentAmount = ucShipmentProducts.ShipmentProductsList.Sum(shipmentProduct => shipmentProduct.TotalAmount);
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbBuyerCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbBuyerCompany_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbBuyerCompanyLegalAccount.SelectedId = Guid.Empty;
            dcbBuyerCompanyLegalAccount.SelectedText = string.Empty;
            dcbBuyerCompanyLegalAccount.Filters.Clear();
            dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                        {
                                                            Name = "IsActive",
                                                            DbType = DbType.Boolean,
                                                            Value = "TRUE"
                                                        });

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
                dcbBuyerCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                            {Name = "CompanyID", DbType = DbType.Guid, Value = e.Value});
            dcbBuyerCompanyLegalAccount.BindData();

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
            {
                var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(Guid.Parse(e.Value));
                if (companyLegalAccount != null)
                {
                    dcbBuyerCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                    dcbBuyerCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                }
            }


            ucBuyerContact.CompanyId = dcbBuyerCompany.SelectedIdNullable;
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the dcbExecutorCompany control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void dcbExecutorCompany_OnSelectedIndexChanged(object sender,
                                                                 RadComboBoxSelectedIndexChangedEventArgs e)
        {
            dcbExecutorCompanyLegalAccount.SelectedId = Guid.Empty;
            dcbExecutorCompanyLegalAccount.SelectedText = string.Empty;
            dcbExecutorCompanyLegalAccount.Filters.Clear();
            dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                           {
                                                               Name = "IsActive",
                                                               DbType = DbType.Boolean,
                                                               Value = "TRUE"
                                                           });
            dcbExecutorCompanyLegalAccount.Filters.Add(new DictionaryOnDemandComboBox.DictionaryFilterColumn()
                                                           {Name = "CompanyID", DbType = DbType.Guid, Value = e.Value});
            dcbExecutorCompanyLegalAccount.BindData();

            if (!string.IsNullOrEmpty(e.Value) && Guid.Parse(e.Value) != Guid.Empty)
            {
                var companyLegalAccount = DataManager.CompanyLegalAccount.SelectPrimary(Guid.Parse(e.Value));
                if (companyLegalAccount != null)
                {
                    dcbExecutorCompanyLegalAccount.SelectedIdNullable = companyLegalAccount.ID;
                    dcbExecutorCompanyLegalAccount.SelectedText = companyLegalAccount.Title;
                }
            }
        }




        /// <summary>
        /// Handles the OnItemDataBound event of the gridRequirementHistory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void gridShipmentHistory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem) e.Item;
                var data = (DataRowView) e.Item.DataItem;

                var lrlUserFullName = (Literal) item.FindControl("lrlUserFullName");
                var lrlShipmentStatus = (Literal) item.FindControl("lrlShipmentStatus");

                if (!string.IsNullOrEmpty(data["tbl_ShipmentHistory_ShipmentStatusID"].ToString()))
                    lrlShipmentStatus.Text =
                        EnumHelper.GetEnumDescription(
                            (ShipmentStatus) int.Parse(data["tbl_ShipmentHistory_ShipmentStatusID"].ToString()));

                if (!string.IsNullOrEmpty(data["tbl_Contact_UserFullName"].ToString()))
                    lrlUserFullName.Text = string.Format("<a href=\"{0}\">{1}</a>",
                                                         UrlsData.AP_Contact(
                                                             Guid.Parse(data["tbl_Contact_ID"].ToString())),
                                                         data["tbl_Contact_UserFullName"]);
            }
        }



        /// <summary>
        /// Handles the OnClick event of the lbtnSendShipment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbtnSendShipment_OnClick(object sender, EventArgs e)
        {
            //if (chxForBuyer.Checked && ucSendShipmentBuyerContact.SelectedValue.HasValue)
            //    ShipmentNotificationService.SendToContact(_shipmentId, ucSendShipmentBuyerContact.SelectedValue.Value);

            //if (chxForExecutor.Checked && ucSendShipmentExecutorContact.SelectedValue.HasValue)
            //    ShipmentNotificationService.SendToContact(_shipmentId, ucSendShipmentExecutorContact.SelectedValue.Value);

            Response.Redirect(UrlsData.AP_ShipmentEdit(_shipmentId));
        }

        protected void dcbBuyerCompanyLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucShipmentInvoices.BuyerCompanyLegalAccountId = dcbBuyerCompanyLegalAccount.SelectedIdNullable;
        }

        protected void dcbExecutorCompanyLegalAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ucShipmentInvoices.ExecutorCompanyLegalAccountId = dcbExecutorCompanyLegalAccount.SelectedIdNullable;
        }
    }
}