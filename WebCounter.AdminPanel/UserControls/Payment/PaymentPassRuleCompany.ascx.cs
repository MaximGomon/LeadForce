using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.UserControls.Payment
{
    public partial class PaymentPassRuleCompany : System.Web.UI.UserControl
    {
        [Serializable]
        public class PaymentPassRuleCompanyMap
        {
            public Guid ID { get; set; }
            public Guid? PayerID { get; set; }
            public string Payer { get; set; }
            public Guid? PayerLegalAccountID { get; set; }
            public string PayerLegalAccount { get; set; }
            public Guid? RecipientID { get; set; }
            public string Recipient { get; set; }
            public Guid? RecipientLegalAccountID { get; set; }
            public string RecipientLegalAccount { get; set; }
        }
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<PaymentPassRuleCompanyMap> PaymentPassRuleCompanies
        {
            get
            {
                if (ViewState["PaymentPassRuleCompanies"] == null) ViewState["PaymentPassRuleCompanies"] = new List<PaymentPassRuleCompanyMap>();
                return (List<PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"];
            }
            set { ViewState["PaymentPassRuleCompanies"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rgPaymentPassRuleCompany.Culture = new CultureInfo("ru-RU");
            
        }

        protected void rgPaymentPassRuleCompany_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;
                var item = e.Item as GridEditableItem;


                var dcbPayer = (DictionaryComboBox)gridEditFormItem.FindControl("dcbPayer");
                dcbPayer.SiteID = CurrentUser.Instance.SiteID;

                var dcbPayerLegalAccount = (DictionaryComboBox)gridEditFormItem.FindControl("dcbPayerLegalAccount");
                dcbPayerLegalAccount.SiteID = CurrentUser.Instance.SiteID;

                var dcbRecipient = (DictionaryComboBox)gridEditFormItem.FindControl("dcbRecipient");
                dcbRecipient.SiteID = CurrentUser.Instance.SiteID;

                var dcbRecipientLegalAccount = (DictionaryComboBox)gridEditFormItem.FindControl("dcbRecipientLegalAccount");
                dcbRecipientLegalAccount.SiteID = CurrentUser.Instance.SiteID;


                var paymentPassRuleCompany = item.DataItem as PaymentPassRuleCompanyMap;
                if (paymentPassRuleCompany != null)
                {
                    if (paymentPassRuleCompany.PayerID != null)
                    {
                        dcbPayer.SelectedId = (Guid)paymentPassRuleCompany.PayerID;
                        dcbPayerLegalAccount.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = (paymentPassRuleCompany.PayerID).ToString() });
                    }
                    if (paymentPassRuleCompany.RecipientID != null)
                    {
                        dcbRecipient.SelectedId = (Guid)paymentPassRuleCompany.RecipientID;
                        dcbRecipientLegalAccount.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = (paymentPassRuleCompany.RecipientID).ToString() });
                    }
                }


                dcbPayer.BindData();
                dcbPayerLegalAccount.BindData();
                dcbRecipient.BindData();
                dcbRecipientLegalAccount.BindData();

                if (paymentPassRuleCompany != null)
                {
                    if (paymentPassRuleCompany.PayerID != null)
                    {
                        dcbPayer.SelectedId = (Guid)paymentPassRuleCompany.PayerID;
                    }
                    if (paymentPassRuleCompany.PayerLegalAccountID != null)
                    {
                        dcbPayerLegalAccount.SelectedId = (Guid)paymentPassRuleCompany.PayerLegalAccountID;
                    }
                    if (paymentPassRuleCompany.RecipientID != null)
                    {
                        dcbRecipient.SelectedId = (Guid)paymentPassRuleCompany.RecipientID;
                    }
                    if (paymentPassRuleCompany.RecipientLegalAccountID != null)
                    {
                        dcbRecipientLegalAccount.SelectedId = (Guid)paymentPassRuleCompany.RecipientLegalAccountID;
                    }
                }

            }


           
        }

        protected void rgPaymentPassRuleCompany_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPaymentPassRuleCompany.DataSource = ViewState["PaymentPassRuleCompanies"];
        }

        protected void rgPaymentPassRuleCompany_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"]).Remove(
                ((List<PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"]).Where(s => s.ID == id).FirstOrDefault());
        }

        public void BindData()
        {
            rgPaymentPassRuleCompany.DataSource = ViewState["PaymentPassRuleCompanies"];
            rgPaymentPassRuleCompany.DataBind();
        }

        protected void rgPaymentPassRuleCompany_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgPaymentPassRuleCompany.MasterTableView.IsItemInserted = false;
            rgPaymentPassRuleCompany.Rebind();
            
        }

        protected void rgPaymentPassRuleCompany_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }





        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="paymentPassRuleCompanyId">The payment pass rule company id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid paymentPassRuleCompanyId, GridEditableItem item)
        {
            var paymentPassRuleCompany = ((List<PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"]).Where(s => s.ID == paymentPassRuleCompanyId).FirstOrDefault() ?? new PaymentPassRuleCompanyMap();


            var dcbPayer = (DictionaryComboBox)item.FindControl("dcbPayer");
            var dcbPayerLegalAccount = (DictionaryComboBox)item.FindControl("dcbPayerLegalAccount");
            var dcbRecipient = (DictionaryComboBox)item.FindControl("dcbRecipient");
            var dcbRecipientLegalAccount = (DictionaryComboBox)item.FindControl("dcbRecipientLegalAccount");

            paymentPassRuleCompany.PayerID = dcbPayer.SelectedIdNullable;
            paymentPassRuleCompany.Payer = dcbPayer.SelectedIdNullable != null
                                                       ? dcbPayer.Text
                                                       : "";
            paymentPassRuleCompany.PayerLegalAccountID = dcbPayerLegalAccount.SelectedIdNullable;
            paymentPassRuleCompany.PayerLegalAccount = dcbPayerLegalAccount.SelectedIdNullable != null ? dcbPayerLegalAccount.Text : "";
            paymentPassRuleCompany.RecipientID = dcbRecipient.SelectedIdNullable;
            paymentPassRuleCompany.Recipient = dcbRecipient.SelectedIdNullable != null ? dcbRecipient.Text : "";

            paymentPassRuleCompany.RecipientLegalAccountID = dcbRecipientLegalAccount.SelectedIdNullable;
            paymentPassRuleCompany.RecipientLegalAccount = dcbRecipientLegalAccount.SelectedIdNullable != null
                                                        ? dcbRecipientLegalAccount.Text
                                                        : "";

            if (paymentPassRuleCompany.ID == Guid.Empty)
            {
                paymentPassRuleCompany.ID = Guid.NewGuid();
                ((List<PaymentPassRuleCompanyMap>)ViewState["PaymentPassRuleCompanies"]).Add(paymentPassRuleCompany);
            }
        }




        protected void dcbPayer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbPayerLegalAccount = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbPayerLegalAccount");
            dcbPayerLegalAccount.Filters.Clear();
            dcbPayerLegalAccount.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = e.Value });
            dcbPayerLegalAccount.BindData();
        }

        protected void dcbRecipient_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbRecipientLegalAccount = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbRecipientLegalAccount");
            dcbRecipientLegalAccount.Filters.Clear();
            dcbRecipientLegalAccount.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "CompanyID", DbType = DbType.Guid, Value = e.Value });
            dcbRecipientLegalAccount.BindData();
            
        }
    }
}