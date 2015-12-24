using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.Service.UserControls
{
    public partial class DictionaryComboBox : System.Web.UI.UserControl
    {
        private Guid _selectedId;
        private DataManager dataManager = new DataManager();
        private string _validationGroup = String.Empty;
        private string _validationErrorMessage = "Выберите значение";
        public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged;

        [Serializable]
        public class DictionaryFilterColumn
        {
            public string Name { get; set; }
            public DbType DbType { get; set; }
            public string Value { get; set; }
        }



        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public List<DictionaryFilterColumn> Filters
        {
            get
            {
                if (ViewState["Filters"] == null)
                    ViewState["Filters"] = new List<DictionaryFilterColumn>();

                return (List<DictionaryFilterColumn>)ViewState["Filters"];
            }

            set
            {
                ViewState["Filters"] = value;
            }
        }


        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public bool SelectDefaultValue
        {
            get
            {
                if (ViewState["SelectDefaultValue"] == null)
                    ViewState["SelectDefaultValue"] = false;

                return (bool)ViewState["SelectDefaultValue"];
            }

            set
            {
                ViewState["SelectDefaultValue"] = value;
            }
        }


        [Serializable]
        public class DictionaryOrderColumn
        {
            public string Name { get; set; }
            public string Direction { get; set; }
        }



        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public List<DictionaryOrderColumn> Order
        {
            get
            {
                var s = (List<DictionaryOrderColumn>)ViewState["Order"];
                return (s ?? new List<DictionaryOrderColumn>());
            }

            set
            {
                ViewState["Order"] = value;
            }
        }



        /// <summary>
        /// Gets the RAD combo box client id.
        /// </summary>
        public string RadComboBoxClientId
        {
            get { return rcbDictionary.ClientID; }
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                rcbDictionary.SelectedIndexChanged += rcbDictionary_SelectedIndexChanged;

            edsDictionary.EntitySetName = DictionaryName;
            if (!Page.IsPostBack)
                BindData();

        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the rcbDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void rcbDictionary_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            if (EditLink!="")
            {
                lbtnEditDictionary.Visible = true;
            }
            rcbDictionary.Items.Clear();    

            edsDictionary.EntitySetName = DictionaryName;            

            edsDictionary.WhereParameters.Clear();
            edsDictionary.AutoGenerateWhereClause = true;

            if (Filters.Count > 0)
            {                
                foreach (var filterColumn in Filters)                
                    edsDictionary.WhereParameters.Add(new Parameter(filterColumn.Name, filterColumn.DbType, filterColumn.Value));                                
            }
            if (Order.Count > 0)
            {
                foreach (var orderColumn in Order)
                {
                    edsDictionary.OrderBy += "it." + orderColumn.Name + " " + orderColumn.Direction;
                }                
            }
            
            if (SiteID != Guid.Empty)                                        
                edsDictionary.WhereParameters.Add(new Parameter("SiteID", DbType.Guid , SiteID.ToString()));

            //TODO: Переделать так чтобы проверялось наличие столбца DataBaseStatusID в таблице 
            if (DictionaryName == "tbl_WorkflowTemplate")
                edsDictionary.WhereParameters.Add(new Parameter("DataBaseStatusID", DbType.Int32, ((int)DataBaseStatus.Active).ToString()));


            if (_validationGroup != String.Empty)
            {
                rfvDictionary.ValidationGroup = _validationGroup;
            }
            else
            {
                rfvDictionary.Enabled = false;
            }

            rfvDictionary.ErrorMessage = _validationErrorMessage;            
            rcbDictionary.DataTextField = DataTextField;
            rcbDictionary.DataValueField = DataValueField;

            if (SelectDefaultValue)
                rcbDictionary.ItemDataBound += rcbDictionary_ItemDataBound;

            rcbDictionary.DataBind();

            var selectedValue = rcbDictionary.SelectedValue;

            if (ShowEmpty)
            {
                rcbDictionary.EmptyMessage = EmptyItemText;
                
                rcbDictionary.Items.Insert(0, new RadComboBoxItem(EmptyItemText, Guid.Empty.ToString()) { Selected = true });
            }            

            if (_selectedId != Guid.Empty)
            {
                rcbDictionary.SelectedIndex = rcbDictionary.FindItemIndexByValue(_selectedId.ToString());
            }
            else if (!string.IsNullOrEmpty(selectedValue) && SelectDefaultValue)
                rcbDictionary.SelectedValue = selectedValue;
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rcbDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void rcbDictionary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (((bool)((dynamic)((ICustomTypeDescriptor)e.Item.DataItem).GetPropertyOwner(null)).IsDefault))
            {
                rcbDictionary.ClearSelection();
                rcbDictionary.SelectedValue = e.Item.Value;
            }
        }



        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get
            {
                return rcbDictionary.CssClass;
            }
            set
            {
                rcbDictionary.CssClass = value;
            }
        }



        /// <summary>
        /// Gets or sets a value indicating whether [auto post back].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto post back]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoPostBack
        {
            get { return rcbDictionary.AutoPostBack; }
            set { rcbDictionary.AutoPostBack = value; }
        }



        /// <summary>
        /// Gets or sets the selected category id.
        /// </summary>
        /// <value>
        /// The selected category id.
        /// </value>
        public Guid SelectedId
        {
            get
            {
                if (rcbDictionary.SelectedItem != null && !string.IsNullOrEmpty(rcbDictionary.SelectedItem.Value))
                    return Guid.Parse(rcbDictionary.SelectedItem.Value);

                return Guid.Empty;
            }
            set
            {
                _selectedId = value;
                rcbDictionary.SelectedIndex = rcbDictionary.Items.IndexOf(rcbDictionary.Items.FindItemByValue(_selectedId.ToString()));
            }
        }



        public Guid SelectedValue
        {
            get
            {
                if (rcbDictionary.SelectedItem != null && !string.IsNullOrEmpty(rcbDictionary.SelectedItem.Value))
                    return Guid.Parse(rcbDictionary.SelectedItem.Value);

                return Guid.Empty;
            }
            set
            {
                _selectedId = value;
                rcbDictionary.SelectedValue = _selectedId.ToString();
            }
        }



        public string SelectedText
        {
            get
            {
                if (rcbDictionary.SelectedItem != null && !string.IsNullOrEmpty(rcbDictionary.SelectedItem.Value))
                    return rcbDictionary.SelectedItem.Text;

                return string.Empty;
            }
        }



        /// <summary>
        /// Gets the selected or null.
        /// </summary>
        public Guid? SelectedIdNullable
        {
            get
            {
                if (!string.IsNullOrEmpty(rcbDictionary.SelectedValue) && Guid.Parse(rcbDictionary.SelectedValue) != Guid.Empty)
                    return Guid.Parse(rcbDictionary.SelectedValue);

                return null;
            }
            set
            {
                if (!value.HasValue)
                    _selectedId = Guid.Empty;
                else
                    _selectedId = (Guid)value;

                rcbDictionary.SelectedIndex = rcbDictionary.FindItemIndexByValue(_selectedId.ToString());
            }
        }


        public string Text
        {
            get { return rcbDictionary.Text; }
        }


        /// <summary>
        /// Gets or sets the dictionary name.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public string DictionaryName
        {
            get
            {
                if (ViewState["DictionaryName"] == null)
                    return "";
                return (string)ViewState["DictionaryName"];
            }
            set { ViewState["DictionaryName"] = value; }
        }



        /// <summary>
        /// Gets or sets the datatextfield.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public string DataTextField
        {
            get
            {
                if (ViewState["DataTextField"] == null)
                    return "Title";
                return (string)ViewState["DataTextField"];
            }
            set { ViewState["DataTextField"] = value; }
        }



        /// <summary>
        /// Gets or sets the datavaluefield.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public string DataValueField
        {
            get
            {
                if (ViewState["DataValueField"] == null)
                    return "ID";         
                return (string)ViewState["DataValueField"];
            }
            set { ViewState["DataValueField"] = value; }
        }

        

        private bool _showEmpty = true;
        /// <summary>
        /// Gets or sets a value indicating whether [show empty].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show empty]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowEmpty
        {
            get { return _showEmpty; }
            set { _showEmpty = value; }
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
        /// Gets or sets the empty item text.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string EmptyItemText
        {
            get
            {
                if (ViewState["EmptyItemText"] == null)
                    return "Выберите значение";         
                return (string)ViewState["EmptyItemText"];
            }
            set { ViewState["EmptyItemText"] = value; }
        }



        /// <summary>
        /// Gets or sets the EditLink.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public string EditLink
        {
            get
            {
                if (ViewState["EditLink"] == null)
                    return String.Empty;
                return (string)ViewState["EditLink"];
            }
            set { ViewState["EditLink"] = value; }
        }



        protected void lbtnEditDictionary_Click(object sender, EventArgs e)
        {
            if (rcbDictionary.SelectedValue!=Guid.Empty.ToString())
            {
                Response.Redirect(EditLink.Replace(Guid.Empty.ToString(), rcbDictionary.SelectedValue));
            }
        }


        /// <summary>
        /// Gets or sets the  Validation Group.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationGroup
        {
            get { return _validationGroup; }
            set { _validationGroup = value; }
        }



        /// <summary>
        /// Gets or sets the  Validation Error Message.
        /// </summary>
        /// <value>
        /// The empty item text.
        /// </value>
        public string ValidationErrorMessage
        {
            get { return _validationErrorMessage; }
            set { _validationErrorMessage = value; }
        }



        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public Unit Width
        {
            get { return rcbDictionary.Width; }
            set { rcbDictionary.Width = value; }
        }


        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public Unit MaxHeight
        {
            get { return rcbDictionary.MaxHeight; }
            set { rcbDictionary.MaxHeight = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [allow custom text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow custom text]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowCustomText
        {
            get { return rcbDictionary.AllowCustomText; }
            set { rcbDictionary.AllowCustomText = value; }
        }


        /// <summary>
        /// Gets or sets the skin.
        /// </summary>
        /// <value>
        /// The skin.
        /// </value>
        public string Skin
        {
            get { return rcbDictionary.Skin; }
            set { rcbDictionary.Skin = value; }
        }

        public RadComboBox ComboBox
        {
            get { return rcbDictionary; }
        }
    }
}