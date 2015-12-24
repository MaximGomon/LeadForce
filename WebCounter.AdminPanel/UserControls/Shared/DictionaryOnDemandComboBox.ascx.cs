using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls.Shared
{
    public partial class DictionaryOnDemandComboBox : System.Web.UI.UserControl
    {
        private string _validationGroup = String.Empty;
        private string _validationErrorMessage = "Выберите значение";
        public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged;
        public event RadComboBoxItemsRequestedEventHandler ItemsRequested;

        [Serializable]
        public class DictionaryFilterColumn
        {
            public string Name { get; set; }
            public DbType DbType { get; set; }
            public string Value { get; set; }
            public FilterOperation? Operation { get; set; }
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
        public string Include
        {
            get { return edsDictionary.Include; }
            set {edsDictionary.Include = value;}
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

            if(ItemsRequested != null)
                rcbDictionary.ItemsRequested += rcbDictionary_ItemsRequested;

            if (!string.IsNullOrEmpty(Mask))
                rcbDictionary.ItemDataBound += rcbDictionary_ItemDataBound;

            InitDataSource();
        }



        /// <summary>
        /// Handles the ItemsRequested event of the rcbDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs"/> instance containing the event data.</param>
        void rcbDictionary_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ItemsRequested != null)
                ItemsRequested(this, e);
        }



        /// <summary>
        /// Handles the ItemDataBound event of the rcbDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void rcbDictionary_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (!string.IsNullOrEmpty(Mask) && MaskFilters.Count > 0)
            {
                var entityObject = ((dynamic)((ICustomTypeDescriptor)e.Item.DataItem).GetPropertyOwner(null));
                var text = Mask;
                foreach (KeyValuePair<string, string> filter in MaskFilters)
                {
                    var value = string.Empty;
                    var values = filter.Value.Split('.');
                    var filterValue = entityObject.GetType().GetProperty(values[0]).GetValue(entityObject, null);

                    if (values.Length == 1)
                    {
                        if (filterValue is DateTime)
                            value = ((DateTime) filterValue).ToString("dd.MM.yyyy");
                        else
                            value = filterValue.ToString();
                    }
                    else if (values.Length == 2)
                    {
                        value = filterValue.GetType().GetProperty(values[1]).GetValue(filterValue, null);
                    }

                    text = text.Replace(filter.Key, value);                    
                }

                e.Item.Text = text;
            }            
        }



        /// <summary>
        /// Inits the data source.
        /// </summary>
        public void InitDataSource()
        {
            edsDictionary.EntitySetName = DictionaryName;
            
            rcbDictionary.Enabled = !string.IsNullOrEmpty(DictionaryName);

            if (SiteID != Guid.Empty && !string.IsNullOrEmpty(DictionaryName))
                edsDictionary.Where = string.Format("it.[SiteID] = GUID '{0}' AND it.{1} <> ''", SiteID, DataTextField);

            if (Filters.Count > 0)
            {
                var list = new List<string>();
                foreach (DictionaryFilterColumn dictionaryFilterColumn in Filters)
                {
                    string value;
                    var type = string.Empty;

                    switch (dictionaryFilterColumn.DbType)
                    {
                        case DbType.Boolean:
                            value = dictionaryFilterColumn.Value;
                            break;
                        case DbType.Decimal:
                            value = dictionaryFilterColumn.Value.Replace(',','.') + "M";
                            break;                        
                        case DbType.Int32:
                            value = dictionaryFilterColumn.Value;
                            break;                        
                        case DbType.Guid:
                            value = string.Format("'{0}'", dictionaryFilterColumn.Value);
                            type = "GUID";
                            break;
                        default:
                            value = string.Format("'{0}'", dictionaryFilterColumn.Value);
                            break;
                    }

                    var operation = "=";
                    if (dictionaryFilterColumn.Operation.HasValue)
                    {
                        switch (dictionaryFilterColumn.Operation)
                        {
                            case FilterOperation.Equal:
                                operation = " = ";
                                break;
                            case FilterOperation.NotEqual:
                                operation = " <> ";
                                break;                                
                        }
                    }

                    var query = string.Format("it.{0} {1} {2} {3}", dictionaryFilterColumn.Name, operation, type, value);
                    list.Add(query);
                }

                if (SiteID == Guid.Empty)
                    edsDictionary.Where = string.Join(" AND ", list);
                else
                    edsDictionary.Where = edsDictionary.Where + " AND " + string.Join(" AND ", list);
            }

            //TODO: Переделать так чтобы проверялось наличие столбца DataBaseStatusID в таблице 
            if (DictionaryName == "tbl_WorkflowTemplate")
                edsDictionary.Where = edsDictionary.Where + string.Format(" AND it.DataBaseStatusID = {0}", (int)DataBaseStatus.Active);

            if (_validationGroup != String.Empty)
                rfvDictionary.ValidationGroup = _validationGroup;
            else
                rfvDictionary.Enabled = false;

            rfvDictionary.ErrorMessage = _validationErrorMessage;

            rcbDictionary.DataTextField = DataTextField;
            rcbDictionary.DataValueField = DataValueField;
            
            if (ShowEmpty)
                rcbDictionary.EmptyMessage = EmptyItemText;            
            
        }


        /// <summary>
        /// Handles the OnSelecting event of the edsDictionary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void edsDictionary_OnSelecting(object sender, EventArgs e)
        {
            edsDictionary.Where = edsDictionary.Where.Replace("LIKE '", "LIKE N'");
             var q = edsDictionary.Select;
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
            rcbDictionary.ClearSelection();

            rcbDictionary.Items.Clear();                        
                        
            rcbDictionary.DataBind();            
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
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public string Width
        {
            get
            {
                return rcbDictionary.Width.ToString();
            }
            set
            {
                rcbDictionary.Width = new Unit(value);
            }
        }



        /// <summary>
        /// Gets or sets the enabled property.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public bool Enabled
        {
            get
            {
                return rcbDictionary.Enabled;
            }
            set
            {
                rcbDictionary.Enabled = value;
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
        /// Gets the selected.
        /// </summary>
        public Guid SelectedValue
        {
            get { return Guid.Parse(rcbDictionary.SelectedValue); }
        }



        /// <summary>
        /// Gets the selected.
        /// </summary>
        public string GetClientId
        {
            get { return rcbDictionary.ClientID; }
        }



        /// <summary>
        /// Gets or sets the selected id.
        /// </summary>
        /// <value>
        /// The selected id.
        /// </value>
        public Guid SelectedId
        {
            get
            {
                return !String.IsNullOrEmpty(rcbDictionary.SelectedValue) ? Guid.Parse(rcbDictionary.SelectedValue) : Guid.Empty;
            }
            set
            {
                rcbDictionary.ClearSelection();
                rcbDictionary.SelectedValue = value.ToString();
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
                rcbDictionary.ClearSelection();
                rcbDictionary.SelectedValue = value.ToString();
            }
        }



        /// <summary>
        /// Gets or sets the selected text.
        /// </summary>
        /// <value>
        /// The selected text.
        /// </value>
        public string SelectedText
        {
            get
            {
                return rcbDictionary.Text;
            }
            set
            {
                rcbDictionary.Text = value;
            }
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
        /// Gets or sets the mask.
        /// </summary>
        /// <value>
        /// The mask.
        /// </value>
        public string Mask
        {
            get { return (string) ViewState["Mask"]; }
            set { ViewState["Mask"] = value; }
        }



        /// <summary>
        /// Gets or sets the mask filters.
        /// </summary>
        /// <value>
        /// The mask filters.
        /// </value>
        public Dictionary<string, string> MaskFilters
        {
            get
            {
                if (ViewState["MaskFilters"] == null)
                    ViewState["MaskFilters"] = new Dictionary<string, string>();

                return (Dictionary<string, string>) ViewState["MaskFilters"];
            }
            set { ViewState["MaskFilters"] = value; }
        }        
    }

    public enum FilterOperation
    {
        Equal = 1,
        NotEqual = 2
    }
}