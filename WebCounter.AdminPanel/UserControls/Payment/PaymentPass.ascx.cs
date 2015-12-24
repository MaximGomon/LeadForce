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
    public partial class PaymentPass : System.Web.UI.UserControl
    {
        [Serializable]
        public class PaymentPassMap
        {
            public Guid ID { get; set; }
            public Guid? OutgoPaymentPassCategoryID { get; set; }
            public string OutgoPaymentPassCategory { get; set; }
            public Guid? OutgoCFOID { get; set; }
            public string OutgoCFO { get; set; }
            public Guid? OutgoPaymentArticleID { get; set; }
            public string OutgoPaymentArticle { get; set; }
            public Guid? IncomePaymentPassCategoryID { get; set; }
            public string IncomePaymentPassCategory { get; set; }
            public Guid? IncomeCFOID { get; set; }
            public string IncomeCFO { get; set; }
            public Guid? IncomePaymentArticleID { get; set; }
            public string IncomePaymentArticle { get; set; }
            public int? FormulaID { get; set; }
            public double? Value { get; set; }
            public double? Amount { get; set; }
        }
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public List<PaymentPassMap> PaymentPasses
        {
            get
            {
                if (ViewState["PaymentPasses"] == null) ViewState["PaymentPasses"] = new List<PaymentPassMap>();
                return (List<PaymentPassMap>)ViewState["PaymentPasses"];
            }
            set { ViewState["PaymentPasses"] = value; }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public double Total
        {
            get
            {
                if (ViewState["Total"] == null) ViewState["Total"] = 0.0;
                return (double) ViewState["Total"];
            }
            set
            {
                ViewState["Total"] = value;
            }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public bool ShowAmount
        {
            get
            {
                if (ViewState["ShowAmount"] == null) ViewState["ShowAmount"] = true;
                return (bool)ViewState["ShowAmount"];
            }
            set
            {
                ViewState["ShowAmount"] = value;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rgPaymentPass.Culture = new CultureInfo("ru-RU");
            
        }

        protected void rgPaymentPass_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                var gridEditFormItem = (GridEditFormItem)e.Item;
                var item = e.Item as GridEditableItem;

                var plAmount = (Panel)gridEditFormItem.FindControl("plAmount");
                plAmount.Visible = ShowAmount;


                var dcbOutgoPaymentPassCategory = (DictionaryComboBox)gridEditFormItem.FindControl("dcbOutgoPaymentPassCategory");
                dcbOutgoPaymentPassCategory.SiteID = CurrentUser.Instance.SiteID;

                var dcbOutgoCFO = (DictionaryComboBox)gridEditFormItem.FindControl("dcbOutgoCFO");
                dcbOutgoCFO.SiteID = CurrentUser.Instance.SiteID;

                var dcbOutgoPaymentArticle = (DictionaryComboBox)gridEditFormItem.FindControl("dcbOutgoPaymentArticle");
                dcbOutgoPaymentArticle.SiteID = CurrentUser.Instance.SiteID;

                var dcbIncomePaymentPassCategory = (DictionaryComboBox)gridEditFormItem.FindControl("dcbIncomePaymentPassCategory");
                dcbIncomePaymentPassCategory.SiteID = CurrentUser.Instance.SiteID;

                var dcbIncomeCFO = (DictionaryComboBox)gridEditFormItem.FindControl("dcbIncomeCFO");
                dcbIncomeCFO.SiteID = CurrentUser.Instance.SiteID;

                var dcbIncomePaymentArticle = (DictionaryComboBox)gridEditFormItem.FindControl("dcbIncomePaymentArticle");
                dcbIncomePaymentArticle.SiteID = CurrentUser.Instance.SiteID;

                var ddlFormula = (DropDownList)gridEditFormItem.FindControl("ddlFormula");
                ddlFormula.Items.Clear();
                foreach (var num in EnumHelper.EnumToList<PaymentPassFormula>())
                {
                    ddlFormula.Items.Add(new ListItem(EnumHelper.GetEnumDescription(num), ((int)num).ToString()));
                }


                var rntxtValue = (RadNumericTextBox)gridEditFormItem.FindControl("rntxtValue");
                var rntxtAmount = (RadNumericTextBox)gridEditFormItem.FindControl("rntxtAmount");
                
                var paymentPass = item.DataItem as PaymentPassMap;
                if (paymentPass != null)
                {

                    if (paymentPass.OutgoPaymentPassCategoryID != null)
                    {
                        dcbOutgoPaymentPassCategory.SelectedId = (Guid)paymentPass.OutgoPaymentPassCategoryID;
                        dcbOutgoCFO.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = (paymentPass.OutgoPaymentPassCategoryID).ToString() });
                        dcbOutgoPaymentArticle.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = (paymentPass.OutgoPaymentPassCategoryID).ToString() });
                    }
                    if (paymentPass.IncomePaymentPassCategoryID != null)
                    {
                        dcbIncomePaymentPassCategory.SelectedId = (Guid)paymentPass.IncomePaymentPassCategoryID;
                        dcbIncomeCFO.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = (paymentPass.IncomePaymentPassCategoryID).ToString() });
                        dcbIncomePaymentArticle.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = (paymentPass.IncomePaymentPassCategoryID).ToString() });
                    }
                    rntxtValue.Value = paymentPass.Value;
                    rntxtAmount.Value = paymentPass.Amount;
                    if (paymentPass.FormulaID!=null)
                    {
                        ddlFormula.SelectedIndex = ddlFormula.FindItemIndexByValue(paymentPass.FormulaID.ToString());
                    }
                }
                

                dcbOutgoPaymentPassCategory.BindData();
                dcbOutgoCFO.BindData();
                dcbOutgoPaymentArticle.BindData();
                dcbIncomePaymentPassCategory.BindData();
                dcbIncomeCFO.BindData();
                dcbIncomePaymentArticle.BindData();

                if (paymentPass != null)
                {
                    if (paymentPass.OutgoCFOID != null)
                    {
                        dcbOutgoCFO.SelectedId = (Guid)paymentPass.OutgoCFOID;
                    }
                    if (paymentPass.OutgoPaymentArticleID != null)
                    {
                        dcbOutgoPaymentArticle.SelectedId = (Guid)paymentPass.OutgoPaymentArticleID;
                    }
                    if (paymentPass.IncomeCFOID != null)
                    {
                        dcbIncomeCFO.SelectedId = (Guid)paymentPass.IncomeCFOID;
                    }
                    if (paymentPass.IncomePaymentArticleID != null)
                    {
                        dcbIncomePaymentArticle.SelectedId = (Guid)paymentPass.IncomePaymentArticleID;
                    }
                }

            }


            //if (e.Item is GridDataItem)
            //{
            //    var item = (GridDataItem) e.Item;
            //    var data = (DataRowView) e.Item.DataItem;
            //    ((Literal)item.FindControl("lrlFormula")).Text = EnumHelper.GetEnumDescription((PaymentPassFormula)int.Parse(data["FormulaID"].ToString()));

            //}
        }

        protected void rgPaymentPass_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPaymentPass.DataSource = ViewState["PaymentPasses"];
        }

        protected void rgPaymentPass_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var id = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            ((List<PaymentPassMap>)ViewState["PaymentPasses"]).Remove(
                ((List<PaymentPassMap>)ViewState["PaymentPasses"]).Where(s => s.ID == id).FirstOrDefault());
        }

        public void BindData()
        {
            rgPaymentPass.DataSource = ViewState["PaymentPasses"];
            rgPaymentPass.DataBind();
        }

        protected void rgPaymentPass_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;

            SaveToViewState(Guid.Empty, item);

            rgPaymentPass.MasterTableView.IsItemInserted = false;
            rgPaymentPass.Rebind();
            
        }

        protected void rgPaymentPass_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            SaveToViewState(Guid.Parse(item.GetDataKeyValue("ID").ToString()), item);
        }

        protected void dcbOutgoPaymentPassCategory_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            var dcbOutgoCFO = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbOutgoCFO");
            var dcbOutgoPaymentArticle = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbOutgoPaymentArticle");
            dcbOutgoCFO.Filters.Clear();
            dcbOutgoCFO.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = e.Value });
            dcbOutgoPaymentArticle.Filters.Clear();
            dcbOutgoPaymentArticle.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = e.Value });
            dcbOutgoCFO.BindData();
            dcbOutgoPaymentArticle.BindData();


        }

        protected void dcbIncomePaymentPassCategory_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var dcbIncomeCFO = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbIncomeCFO");
            var dcbIncomePaymentArticle = (DictionaryComboBox)((DictionaryComboBox)sender).Parent.FindControl("dcbIncomePaymentArticle");
            dcbIncomeCFO.Filters.Clear();
            dcbIncomeCFO.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = e.Value });
            dcbIncomePaymentArticle.Filters.Clear();
            dcbIncomePaymentArticle.Filters.Add(new DictionaryComboBox.DictionaryFilterColumn() { Name = "PaymentPassCategoryID", DbType = DbType.Guid, Value = e.Value });
            dcbIncomeCFO.BindData();
            dcbIncomePaymentArticle.BindData();
            
        }

        protected void rntxtValue_OnTextChanged(object sender, EventArgs e)
        {
            var ddlFormula = (DropDownList)((RadNumericTextBox)sender).Parent.FindControl("ddlFormula");
            var rntxtAmount = (RadNumericTextBox)((RadNumericTextBox)sender).Parent.FindControl("rntxtAmount");
            var rntxtValue = (RadNumericTextBox)((RadNumericTextBox)sender).Parent.FindControl("rntxtValue");
            rntxtAmount.Value = RecalculateAmount(Total, ddlFormula.SelectedIndex, rntxtValue.Value);
        }

        protected void ddlFormula_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlFormula = (DropDownList)((DropDownList)sender).Parent.FindControl("ddlFormula");
            var rntxtAmount = (RadNumericTextBox)((DropDownList)sender).Parent.FindControl("rntxtAmount");
            var rntxtValue = (RadNumericTextBox)((DropDownList)sender).Parent.FindControl("rntxtValue");
            rntxtAmount.Value = RecalculateAmount(Total, ddlFormula.SelectedIndex, rntxtValue.Value);            
        }


        /// <summary>
        /// Saves the state of to view.
        /// </summary>
        /// <param name="orderProductId">The order product id.</param>
        /// <param name="item">The item.</param>
        private void SaveToViewState(Guid orderProductId, GridEditableItem item)
        {
            var paymentPass = ((List<PaymentPassMap>)ViewState["PaymentPasses"]).Where(s => s.ID == orderProductId).FirstOrDefault() ?? new PaymentPassMap();


            var dcbOutgoPaymentPassCategory = (DictionaryComboBox)item.FindControl("dcbOutgoPaymentPassCategory");
            var dcbOutgoCFO = (DictionaryComboBox)item.FindControl("dcbOutgoCFO");
            var dcbOutgoPaymentArticle = (DictionaryComboBox)item.FindControl("dcbOutgoPaymentArticle");
            var dcbIncomePaymentPassCategory = (DictionaryComboBox)item.FindControl("dcbIncomePaymentPassCategory");
            var dcbIncomeCFO = (DictionaryComboBox)item.FindControl("dcbIncomeCFO");
            var dcbIncomePaymentArticle = (DictionaryComboBox)item.FindControl("dcbIncomePaymentArticle");
            var ddlFormula = (DropDownList)item.FindControl("ddlFormula");
            var rntxtValue = (RadNumericTextBox)item.FindControl("rntxtValue");

            paymentPass.OutgoPaymentPassCategoryID = dcbOutgoPaymentPassCategory.SelectedIdNullable;
            paymentPass.OutgoPaymentPassCategory = dcbOutgoPaymentPassCategory.SelectedIdNullable != null
                                                       ? dcbOutgoPaymentPassCategory.Text
                                                       : "";
            paymentPass.OutgoCFOID = dcbOutgoCFO.SelectedIdNullable;
            paymentPass.OutgoCFO = dcbOutgoCFO.SelectedIdNullable != null ? dcbOutgoCFO.Text : "";
            paymentPass.OutgoPaymentArticleID = dcbOutgoPaymentArticle.SelectedIdNullable;
            paymentPass.OutgoPaymentArticle = dcbOutgoPaymentArticle.SelectedIdNullable!=null ? dcbOutgoPaymentArticle.Text : "";

            paymentPass.IncomePaymentPassCategoryID = dcbIncomePaymentPassCategory.SelectedIdNullable;
            paymentPass.IncomePaymentPassCategory = dcbIncomePaymentPassCategory.SelectedIdNullable != null
                                                        ? dcbIncomePaymentPassCategory.Text
                                                        : "";
            paymentPass.IncomeCFOID = dcbIncomeCFO.SelectedIdNullable;
            paymentPass.IncomeCFO = dcbIncomeCFO.SelectedIdNullable != null ? dcbIncomeCFO.Text : "";
            paymentPass.IncomePaymentArticleID = dcbIncomePaymentArticle.SelectedIdNullable;
            paymentPass.IncomePaymentArticle = dcbIncomePaymentArticle.SelectedIdNullable != null
                                                   ? dcbIncomePaymentArticle.Text
                                                   : "";

            paymentPass.FormulaID = int.Parse(ddlFormula.SelectedValue);
            paymentPass.Value = rntxtValue.Value;
            paymentPass.Amount = RecalculateAmount(Total, (int)paymentPass.FormulaID, paymentPass.Value);

            if (paymentPass.ID == Guid.Empty)
            {
                paymentPass.ID = Guid.NewGuid();
                ((List<PaymentPassMap>)ViewState["PaymentPasses"]).Add(paymentPass);
            }
        }




        private double? RecalculateAmount(double? total, int formulaId, double? value)
        {
            double? ret = 0;
            switch (formulaId)
            {
                case (int)PaymentPassFormula.Balance:
                    double? sum = 0.00;
                    if (PaymentPasses!=null)
                    {
                        foreach (var paymentPass in PaymentPasses)
                        {
                            sum += paymentPass.Amount;
                        }
                    }
                    ret = total  - (sum ?? 0) - value;
                    break;
                case (int)PaymentPassFormula.Percent:
                    ret = total * value / 100;
                    break;
                case (int)PaymentPassFormula.FixedSum:
                    ret = value;
                    break;
            }            
            return ret;
        }

    }
}