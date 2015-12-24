using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.DictionaryEditForm
{
    public partial class ServiceLevelEdit : System.Web.UI.UserControl
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
        protected tbl_ServiceLevel ServiceLevel
        {
            get { return (tbl_ServiceLevel) ViewState["ServiceLevel"]; }
            set { ViewState["ServiceLevel"] = value; }
        }


        /// <summary>
        /// Gets or sets the service level client id.
        /// </summary>
        /// <value>
        /// The service level client id.
        /// </value>
        protected Guid ServiceLevelClientId
        {
            get
            {
                if (ViewState["ServiceLevelClientId"] == null)
                    ViewState["ServiceLevelClientId"] = Guid.Empty;

                return (Guid)ViewState["ServiceLevelClientId"];
            }
            set { ViewState["ServiceLevelClientId"] = value; }
        }


        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>
        /// The client id.
        /// </value>
        protected Guid ClientId
        {
            get
            {
                if (ViewState["ClientId"] == null)
                    ViewState["ClientId"] = Guid.Empty;

                return (Guid)ViewState["ClientId"];
            }
            set { ViewState["ClientId"] = value; }
        }



        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            rgServiceLevelClient.Culture = new CultureInfo("ru-RU");

            edsServiceLevel.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsServiceLevelRole.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsClients.Where = string.Format("it.SiteID = GUID '{0}'", CurrentUser.Instance.SiteID);
            edsClients.OrderBy = "it.Name";
            edsContacts.Where = string.Format("it.SiteID = GUID '{0}' AND it.UserFullName <> ''", CurrentUser.Instance.SiteID.ToString());
            edsContacts.OrderBy = "it.UserFullName";

            if (DataItem != null && !(DataItem is GridInsertionObject))
                ServiceLevel = (tbl_ServiceLevel)DataItem;
            
            if (ServiceLevel != null)
                edsServiceLevelClient.Where = string.Format("it.ServiceLevelID = GUID '{0}'", ServiceLevel.ID);
        }


        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DataItem != null && !(DataItem is GridInsertionObject))
                ServiceLevel = (tbl_ServiceLevel)DataItem;
            
            if (ServiceLevel != null)
                edsServiceLevelClient.Where = string.Format("it.ServiceLevelID = GUID '{0}'", ServiceLevel.ID);
        }
 


        /// <summary>
        /// Handles the OnClick event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {        
            var serviceLevel = DataManager.ServiceLevel.SelectById(CurrentUser.Instance.SiteID, ServiceLevel.ID);

            if (serviceLevel != null)
            {                
                serviceLevel.Title = txtTitle.Text;
                serviceLevel.ReactionTime = txtReactionTime.Value.HasValue ? (int)txtReactionTime.Value : 0;
                serviceLevel.ResponseTime = txtResponseTime.Value.HasValue ? (int)txtResponseTime.Value : 0;
                serviceLevel.IsDefault = chxIsDefault.Checked;
                serviceLevel.IsActive = chxIsActive.Checked;

                DataManager.ServiceLevel.Update(serviceLevel);
            }            
        }



        /// <summary>
        /// Handles the OnClick event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnInsert_OnClick(object sender, EventArgs e)
        {            
            var serviceLevel = new tbl_ServiceLevel
                                   {
                                       ID = Guid.NewGuid(),
                                       SiteID = CurrentUser.Instance.SiteID,
                                       Title = txtTitle.Text,
                                       ReactionTime = txtReactionTime.Value.HasValue ? (int) txtReactionTime.Value : 0,
                                       ResponseTime = txtResponseTime.Value.HasValue ? (int) txtResponseTime.Value : 0,
                                       IsActive = chxIsActive.Checked,
                                       IsDefault = chxIsDefault.Checked
                                   };

            DataItem = serviceLevel;

            DataManager.ServiceLevel.Add(serviceLevel);            
        }


        /// <summary>
        /// Handles the OnInsertCommand event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnInsertCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "ServiceLevelContact")
                return;

            var item = e.Item as GridEditableItem;
            var values = new Hashtable();
            item.ExtractValues(values);
            var serviceLevelClient = new tbl_ServiceLevelClient();
            item.UpdateValues(serviceLevelClient);
            serviceLevelClient.ServiceLevelID = ServiceLevel.ID;
            DataManager.ServiceLevelClient.Add(serviceLevelClient);
        }



        /// <summary>
        /// Handles the OnUpdateCommand event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "ServiceLevelContact")
                return;

            var item = e.Item as GridEditableItem;
            var serviceLevelClientId = Guid.Parse(item.GetDataKeyValue("ID").ToString());
            var serviceLevelClient = DataManager.ServiceLevelClient.SelectById(ServiceLevel.ID, serviceLevelClientId);
            item.UpdateValues(serviceLevelClient);
            DataManager.ServiceLevelClient.Update(serviceLevelClient);
        }



        /// <summary>
        /// Handles the OnDeleteCommand event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "ServiceLevelContact")
                return;

            var serviceLevelClientId = Guid.Parse((e.Item as GridDataItem).GetDataKeyValue("ID").ToString());
            var serviceLevelClient = DataManager.ServiceLevelClient.SelectById(ServiceLevel.ID, serviceLevelClientId);
            DataManager.ServiceLevelClient.Delete(serviceLevelClient);
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnItemDataBound(object sender, GridItemEventArgs e)
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
                    if (e.Item.OwnerTableView.Name != "ServiceLevelContact")
                    {
                        var clientServiceLevel =
                            (GridDropDownColumnEditor) (editMan.GetColumnEditor("ClientServiceLevelID"));
                        clientServiceLevel.SelectedValue = ServiceLevel.ID.ToString();
                    }
                }

                if (e.Item.OwnerTableView.Name != "ServiceLevelContact")
                {
                    ((TableRow) editedItem["ServiceLevelID"].Parent).CssClass = "hidden";                    
                    var serviceLevelColumnEditor = (GridTextBoxColumnEditor) (editMan.GetColumnEditor("ServiceLevelID"));
                    serviceLevelColumnEditor.TextBoxControl.Text = ServiceLevel.ID.ToString();

                    var startDateTimeEditor =
                        (GridDateTimeColumnEditor) editedItem.EditManager.GetColumnEditor("StartDate");
                    startDateTimeEditor.PickerControl.Width = new Unit(50, UnitType.Pixel);
                    startDateTimeEditor.PickerControl.CssClass = "date-picker";
                    var endDateTimeEditor = (GridDateTimeColumnEditor) editedItem.EditManager.GetColumnEditor("EndDate");
                    endDateTimeEditor.PickerControl.Width = new Unit(50, UnitType.Pixel);
                    endDateTimeEditor.PickerControl.CssClass = "date-picker";                    
                }
                else
                {
                    ((TableRow)editedItem["ServiceLevelClientID"].Parent).CssClass = "hidden";
                    ((TableRow)editedItem["ContactEmail"].Parent).CssClass = "hidden";
                    
                    var serviceLevelClientColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ServiceLevelClientID"));
                    serviceLevelClientColumnEditor.TextBoxControl.Text = ServiceLevelClientId.ToString();                    
                }
            }
        }



        /// <summary>
        /// Handles the OnDataBound event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                var editedItem = e.Item as GridEditableItem;
                var editMan = editedItem.EditManager;

                var idColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ID"));
                if (string.IsNullOrEmpty(idColumnEditor.TextBoxControl.Text))
                    idColumnEditor.TextBoxControl.Text = Guid.NewGuid().ToString();


                if (e.Item.OwnerTableView.Name != "ServiceLevelContact")
                {
                    var serviceLevelColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ServiceLevelID"));
                    serviceLevelColumnEditor.TextBoxControl.Text = ServiceLevel.ID.ToString();

                    ((GridDropDownListColumnEditor)(editMan.GetColumnEditor("ClientID"))).ComboBoxControl.Filter = RadComboBoxFilter.Contains;

                    var editor = (GridNumericColumnEditor)editMan.GetColumnEditor("CountOfServiceContacts");
                    var cell = (TableCell)editor.NumericTextBox.Parent;
                    var validator = new RequiredFieldValidator
                    {
                        ControlToValidate = editor.NumericTextBox.ID,
                        ErrorMessage = "*",
                        ForeColor = Color.Red
                    };
                    cell.Controls.Add(validator);
                }
                else
                {
                    var serviceLevelClientColumnEditor = (GridTextBoxColumnEditor)(editMan.GetColumnEditor("ServiceLevelClientID"));
                    serviceLevelClientColumnEditor.TextBoxControl.Text = ServiceLevelClientId.ToString();

                    ((TableRow)editedItem["ContactEmail"].Parent).CssClass = "hidden";

                    ((GridDropDownListColumnEditor)(editMan.GetColumnEditor("ContactID"))).ComboBoxControl.Filter = RadComboBoxFilter.Contains;
                    ((GridTextBoxColumnEditor)(editMan.GetColumnEditor("Comment"))).TextBoxControl.TextMode = TextBoxMode.MultiLine;                    
                }
            }
        }
        

        /// <summary>
        /// Handles the OnDetailTableDataBind event of the rgServiceLevelClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridDetailTableDataBindEventArgs"/> instance containing the event data.</param>
        protected void rgServiceLevelClient_OnDetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            ClientId = Guid.Parse(e.DetailTableView.ParentItem.GetDataKeyValue("ClientID").ToString());
            ServiceLevelClientId = Guid.Parse(e.DetailTableView.ParentItem.GetDataKeyValue("ID").ToString());            
        }
    }
}