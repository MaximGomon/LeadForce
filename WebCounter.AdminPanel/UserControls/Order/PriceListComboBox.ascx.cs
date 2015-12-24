using System;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls.Order
{
    public partial class PriceListComboBox : System.Web.UI.UserControl
    {
        private Guid _selectedId;
        private DataManager dataManager = new DataManager();
        private string _validationGroup = String.Empty;
        private string _validationErrorMessage = "Выберите значение";
        private bool _showEmpty = true;
        public event RadComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged;



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
                rcbDictionary.SelectedIndex = rcbDictionary.Items.FindItemIndexByValue(_selectedId.ToString());
            }
        }



        /// <summary>
        /// Gets or sets the siteID.
        /// </summary>
        /// <value>
        /// The site id.
        /// </value>
        public Guid SiteId
        {
            get
            {
                if (ViewState["SiteId"] == null)
                    return Guid.Empty;
                return (Guid)ViewState["SiteId"];
            }
            set { ViewState["SiteId"] = value; }
        }



        /// <summary>
        /// Gets or sets the type of the price list.
        /// </summary>
        /// <value>
        /// The type of the price list.
        /// </value>
        public PriceListType? PriceListType
        {
            get
            {
                if (ViewState["PriceListType"] == null)
                    return null;
                return (PriceListType)ViewState["PriceListType"];
            }
            set { ViewState["PriceListType"] = value; }
        }



        /// <summary>
        /// Gets or sets the type of the select price list.
        /// </summary>
        /// <value>
        /// The type of the select price list.
        /// </value>
        public PriceListType? SelectPriceListType
        {
            get
            {
                if (ViewState["SelectPriceListType"] == null)
                    return null;
                return (PriceListType)ViewState["SelectPriceListType"];
            }
            set { ViewState["SelectPriceListType"] = value; }
        }



        /// <summary>
        /// Gets or sets the select price list status.
        /// </summary>
        /// <value>
        /// The select price list status.
        /// </value>
        public PriceListStatus? SelectPriceListStatus
        {
            get
            {
                if (ViewState["SelectPriceListStatus"] == null)
                    return null;
                return (PriceListStatus)ViewState["SelectPriceListStatus"];
            }
            set { ViewState["SelectPriceListStatus"] = value; }
        }



        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        /// <value>
        /// The product id.
        /// </value>
        public Guid? ProductId
        {
            get
            {
                if (ViewState["ProductId"] == null)
                    return null;
                return (Guid)ViewState["ProductId"];
            }
            set { ViewState["ProductId"] = value; }
        }



        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate
        {
            get
            {
                if (ViewState["OrderDate"] == null)
                    return null;
                return (DateTime)ViewState["OrderDate"];
            }
            set { ViewState["OrderDate"] = value; }
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
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                rcbDictionary.SelectedIndexChanged += rcbDictionary_SelectedIndexChanged;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            rcbDictionary.Items.Clear();
            if (_validationGroup != String.Empty)            
                rfvDictionary.ValidationGroup = _validationGroup;            
            else            
                rfvDictionary.Enabled = false;            

            rfvDictionary.ErrorMessage = _validationErrorMessage;

            IQueryable<tbl_PriceList> priceList = dataManager.PriceList.SelectAll(SiteId);

            if (PriceListType != null)
                priceList = priceList.Where(pl => pl.PriceListTypeID == (int) PriceListType);
            if (ProductId.HasValue)
                priceList = priceList.Where(pl => pl.tbl_ProductPrice.Where(pp => pp.ProductID == ProductId).Count() > 0);
            if (OrderDate.HasValue)
                priceList = priceList.Where(pl => pl.tbl_ProductPrice.Where(pp => pp.DateFrom <= OrderDate && pp.DateTo >= OrderDate).Count() > 0);

            foreach (var priceListItem in priceList.ToList())
            {
                var priceListTitle = priceListItem.Title + " (" + priceListItem.tbl_PriceListType.Title + ")";
                var rcbItem = new RadComboBoxItem(priceListTitle, priceListItem.ID.ToString());

                if (SelectPriceListType.HasValue && (PriceListType)priceListItem.tbl_PriceListType.ID == SelectPriceListType && !SelectPriceListStatus.HasValue)
                    _selectedId = priceListItem.ID;

                if (SelectPriceListType.HasValue && SelectPriceListStatus.HasValue && (PriceListType)priceListItem.tbl_PriceListType.ID == SelectPriceListType && (PriceListStatus)priceListItem.tbl_PriceListStatus.ID == SelectPriceListStatus)
                    _selectedId = priceListItem.ID;

                rcbDictionary.Items.Add(rcbItem);
            }

            rcbDictionary.DataBind();

            if (ShowEmpty)
            {                
                rcbDictionary.Items.Insert(0, new RadComboBoxItem(EmptyItemText, Guid.Empty.ToString()) { Selected = true });
            }            

            if (_selectedId != Guid.Empty)
            {
                rcbDictionary.SelectedIndex = rcbDictionary.Items.IndexOf(rcbDictionary.Items.FindItemByValue(_selectedId.ToString()));
            }            
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
    }
}