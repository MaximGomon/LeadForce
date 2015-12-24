using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace Labitec.LeadForce.Portal.Shared.UserControls
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
                var s = (List<DictionaryFilterColumn>)ViewState["Filters"];
                return (s ?? new List<DictionaryFilterColumn>());
            }

            set
            {
                ViewState["Filters"] = value;
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
                    edsDictionary.OrderBy += orderColumn.Name + " " + orderColumn.Direction;
                }                
            }
            
            if (SiteID != Guid.Empty)                                        
                edsDictionary.WhereParameters.Add(new Parameter("SiteID", DbType.Guid , SiteID.ToString()));
            

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
            rcbDictionary.DataBind();
            if (ShowEmpty)
            {
                rcbDictionary.EmptyMessage = EmptyItemText;
                
                rcbDictionary.Items.Insert(0, new RadComboBoxItem(EmptyItemText, Guid.Empty.ToString()) { Selected = true });
            }
            if (_selectedId != Guid.Empty)
            {
                rcbDictionary.SelectedIndex = rcbDictionary.Items.IndexOf(rcbDictionary.Items.FindItemByValue(_selectedId.ToString()));
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
                return Guid.Parse(rcbDictionary.SelectedItem.Value);
            }
            set
            {
                _selectedId = value;
                rcbDictionary.SelectedIndex = rcbDictionary.Items.IndexOf(rcbDictionary.Items.FindItemByValue(_selectedId.ToString()));
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

    }
}