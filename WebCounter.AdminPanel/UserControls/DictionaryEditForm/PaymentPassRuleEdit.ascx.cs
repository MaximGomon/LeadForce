using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;
using WebCounter.BusinessLogicLayer.Common;

namespace WebCounter.AdminPanel.UserControls.DictionaryEditForm
{
    public partial class PaymentPassRuleEdit : System.Web.UI.UserControl
    {
        protected DataManager DataManager = new DataManager();

        private object _dataItem = null;

        /// <summary>
        /// Gets or sets the data item.
        /// </summary>
        /// <value>
        /// The data item.
        /// </value>
        public object DataItem
        {
            get { return this._dataItem; }
            set { this._dataItem = value; }
        }



        /// <summary>
        /// Gets the service level.
        /// </summary>
        protected tbl_PaymentPassRule PaymentPassRule
        {
            get { return (tbl_PaymentPassRule) ViewState["PaymentPassRule"]; }
            set { ViewState["PaymentPassRule"] = value; }
        }
        


        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rgPaymentPassRulePass.Culture = new CultureInfo("ru-RU");
            rgPaymentPassRuleCompany.Culture = new CultureInfo("ru-RU");

            edsPaymentArticle.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsPaymentCFO.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsPaymentPassCategory.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsPaymentPassRulePass.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);

            edsPayer.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsPayerLegalAccount.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsRecipient.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsRecipientLegalAccount.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);

            if (DataItem != null && !(DataItem is GridInsertionObject))
                PaymentPassRule = (tbl_PaymentPassRule)DataItem;

            if (PaymentPassRule != null)
            {
                edsPaymentPassRulePass.Where = string.Format("it.PaymentPassRuleID = GUID '{0}'", PaymentPassRule.ID);
                edsPaymentPassRuleCompany.Where = string.Format("it.PaymentPassRuleID = GUID '{0}'", PaymentPassRule.ID);
            }
        }


        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataItem != null && !(DataItem is GridInsertionObject))
                PaymentPassRule = (tbl_PaymentPassRule)DataItem;

            ddlPaymentType.Items.Clear();
            foreach (var mode in EnumHelper.EnumToList<PaymentType>())
                ddlPaymentType.Items.Add(new ListItem(EnumHelper.GetEnumDescription(mode), ((int)mode).ToString()));

            if (PaymentPassRule != null)
            {
                edsPaymentPassRulePass.Where = string.Format("it.PaymentPassRuleID = GUID '{0}'", PaymentPassRule.ID);
                edsPaymentPassRuleCompany.Where = string.Format("it.PaymentPassRuleID = GUID '{0}'", PaymentPassRule.ID);

                //if (int.Parse(ddlPaymentType.SelectedValue) != PaymentPassRule.PaymentTypeID && ddlPaymentType.SelectedValue == "0")
                //{
                //    dcbPaymentPassCategory.SelectedId = PaymentCFO.PaymentPassCategoryID;
                //    var paymentPassCategory =
                //        DataManager.PaymentPassCategory.SelectById(PaymentCFO.PaymentPassCategoryID);
                //    dcbPaymentPassCategory.SelectedText = paymentPassCategory != null
                //                                              ? paymentPassCategory.Title
                //                                              : "";
                //}
            }

        }
 


        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {        
            var paymentPassRule = DataManager.PaymentPassRule.SelectById(CurrentUser.Instance.SiteID, PaymentPassRule.ID);

            if (paymentPassRule != null)
            {                
                paymentPassRule.Title = txtTitle.Text;
                paymentPassRule.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
                paymentPassRule.IsActive = chxIsActive.Checked;
                paymentPassRule.IsAutomatic = chxIsAutomatic.Checked;
                DataManager.PaymentPassRule.Update(paymentPassRule);
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {            
            var paymentPassRule = new tbl_PaymentPassRule
                                   {
                                       ID = Guid.NewGuid(),
                                       SiteID = CurrentUser.Instance.SiteID,
                                       Title = txtTitle.Text,
                                       PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue),
                                       IsActive = chxIsActive.Checked,
                                       IsAutomatic = chxIsAutomatic.Checked
                                   };

            DataItem = paymentPassRule;

            DataManager.PaymentPassRule.Add(paymentPassRule);            
        }


        /// <summary>
        /// Handles the OnInsertCommand event of the rgPaymentPassRulePass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPaymentPassRulePass_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.Item.OwnerTableView.Name == "ServiceLevelContact")
            //    return;

            var item = e.Item as GridEditableItem;
            var values = new Hashtable();
            item.ExtractValues(values);
            var paymentPassRulePass = new tbl_PaymentPassRulePass();
            item.UpdateValues(paymentPassRulePass);
            paymentPassRulePass.PaymentPassRuleID = PaymentPassRule.ID;
            paymentPassRulePass.SiteID = CurrentUser.Instance.SiteID;
            DataManager.PaymentPassRulePass.Add(paymentPassRulePass);
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgPaymentPassRulePass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPaymentPassRulePass_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {

            var item = e.Item as GridEditableItem;
            var paymentPassRulePassId = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var paymentPassRulePass = DataManager.PaymentPassRulePass.SelectById(PaymentPassRule.ID, paymentPassRulePassId);
            item.UpdateValues(paymentPassRulePass);
            paymentPassRulePass.SiteID = CurrentUser.Instance.SiteID;
            DataManager.PaymentPassRulePass.Update(paymentPassRulePass);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgPaymentPassRulePass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgPaymentPassRulePass_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var paymentPassRulePassId = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            var paymentPassRulePass = DataManager.PaymentPassRulePass.SelectById(PaymentPassRule.ID, paymentPassRulePassId);
            DataManager.PaymentPassRulePass.Delete(paymentPassRulePass);
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgPaymentPassRulePass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgPaymentPassRulePass_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var editedItem = e.Item as GridEditableItem;
                var editMan = editedItem.EditManager;

                ((TableRow)editedItem["ID"].Parent).CssClass = "hidden";
                var idColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ID"));
                if (string.IsNullOrEmpty(idColumnEditor.TextBoxControl.Text))
                {
                    idColumnEditor.TextBoxControl.Text = Guid.NewGuid().ToString();
                }

                ((TableRow)editedItem["PaymentPassRuleID"].Parent).CssClass = "hidden";
                var paymentPassRuleEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("PaymentPassRuleID"));
                paymentPassRuleEditor.TextBoxControl.Text = PaymentPassRule.ID.ToString();

                GridDropDownListColumnEditor gddPayer = editMan.GetColumnEditor("OutgoPaymentPassCategoryID") as GridDropDownListColumnEditor;
                gddPayer.ComboBoxControl.AutoPostBack = true;
                gddPayer.ComboBoxControl.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(gddPayer_SelectedIndexChanged);  

            }
        }
        




        public class a
        {
            public int ID  { get; set; }
            public string Title { get; set; }
            
        }

        protected void ldsFormula_OnSelecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            var aa = new List<a> ();
            foreach (var sourceEmailProperty in EnumHelper.EnumToList<PaymentPassFormula>())
            {
                aa.Add(new a(){ID = ((int) sourceEmailProperty),Title = EnumHelper.GetEnumDescription(sourceEmailProperty)});
            }
            e.Result = aa;
        }

        protected void rgPaymentPassRuleCompany_OnItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                

                 var editedItem = e.Item as GridEditableItem;
                    var editMan = editedItem.EditManager;
                 var list=   ((GridDropDownListColumnEditor)(editMan.GetColumnEditor("PayerID"))).ComboBoxControl;

                //attach SelectedIndexChanged event for the dropdown control  
                list.AutoPostBack = true;
                list.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(gddPayer_SelectedIndexChanged);
            }
        }  


        protected void rgPaymentPassRuleCompany_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var editedItem = e.Item as GridEditableItem;
                var editMan = editedItem.EditManager;

                ((TableRow)editedItem["ID"].Parent).CssClass = "hidden";
                var idColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ID"));
                if (string.IsNullOrEmpty(idColumnEditor.TextBoxControl.Text))
                {
                    idColumnEditor.TextBoxControl.Text = Guid.NewGuid().ToString();
                }

                ((TableRow)editedItem["PaymentPassRuleID"].Parent).CssClass = "hidden";
                var paymentPassRuleEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("PaymentPassRuleID"));
                paymentPassRuleEditor.TextBoxControl.Text = PaymentPassRule.ID.ToString();

                GridDropDownListColumnEditor gddPayer = editMan.GetColumnEditor("PayerID") as GridDropDownListColumnEditor;
                gddPayer.ComboBoxControl.AutoPostBack = true;
                gddPayer.ComboBoxControl.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(gddPayer_SelectedIndexChanged);  
            }
        }

        void gddPayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var a = 1;
        }

        protected void rgPaymentPassRuleCompany_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var values = new Hashtable();
            item.ExtractValues(values);
            var paymentPassRuleCompany = new tbl_PaymentPassRuleCompany();
            item.UpdateValues(paymentPassRuleCompany);
            paymentPassRuleCompany.PaymentPassRuleID = PaymentPassRule.ID;
            paymentPassRuleCompany.SiteID = CurrentUser.Instance.SiteID;
            DataManager.PaymentPassRuleCompany.Add(paymentPassRuleCompany);
        }

        protected void rgPaymentPassRuleCompany_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridEditableItem;
            var paymentPassRuleCompanyId = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var paymentPassRuleCompany = DataManager.PaymentPassRuleCompany.SelectById(PaymentPassRule.ID, paymentPassRuleCompanyId);
            item.UpdateValues(paymentPassRuleCompany);
            DataManager.PaymentPassRuleCompany.Update(paymentPassRuleCompany);

        }

        protected void rgPaymentPassRuleCompany_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var paymentPassRuleCompanyId = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            var paymentPassRuleCompany = DataManager.PaymentPassRuleCompany.SelectById(PaymentPassRule.ID, paymentPassRuleCompanyId);
            DataManager.PaymentPassRuleCompany.Delete(paymentPassRuleCompany);

        }

    }
}