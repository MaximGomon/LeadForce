using System;
using System.Web.UI.WebControls;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SelectCategory : System.Web.UI.UserControl
    {
        private Guid _selectedCategoryId;
        private Guid _categoryId = Guid.Empty;
        private DataManager dataManager = new DataManager();
        private string _validationGroup = String.Empty;

        private RadTreeView _radProductCategoryTreeView = new RadTreeView();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            _radProductCategoryTreeView = ((RadTreeView)radCategories.Items[0].FindControl("radProductCategoryTreeView"));
            
            _radProductCategoryTreeView.Nodes.Clear();

            if (ShowEmpty)
                radCategories.EmptyMessage = EmptyItemText;
            if (_validationGroup != String.Empty)
            {
                rfvCategories.ValidationGroup = _validationGroup;
                rfvCategories.Enabled = true;
            }
            else            
                rfvCategories.Enabled = false;            

            var productCategories = dataManager.ProductCategory.SelectHierarchy(SiteID).ToList();
            var publicationCategories = dataManager.PublicationCategory.SelectHierarchy(SiteID);

            if (SelectDefault && _selectedCategoryId == Guid.Empty)
                switch (CategoryType)
                {
                    case "Product":
                        SelectedCategoryId = productCategories.Where(pc => pc.IsDefault).Select(pc => pc.ID).FirstOrDefault();
                        break;
                    case "Publication":
                        SelectedCategoryId = publicationCategories.Where(pc => pc.IsDefault).Select(pc => pc.ID).FirstOrDefault();
                        break;
                }

            switch (CategoryType)
            {
                case "Product":
                    _radProductCategoryTreeView.DataSource = productCategories;                    
                    break;
                case "Publication":
                    _radProductCategoryTreeView.DataSource = publicationCategories;                    
                    break;
            }

            _radProductCategoryTreeView.DataTextField = "Title";
            _radProductCategoryTreeView.DataFieldID = "ID";
            _radProductCategoryTreeView.DataValueField = "ID";
            _radProductCategoryTreeView.DataFieldParentID = "ParentID";
            _radProductCategoryTreeView.DataBind();            

            if (_selectedCategoryId!=Guid.Empty)
            {
                
                switch (CategoryType)
                {
                    case "Product":
                        var selectedCategory = dataManager.ProductCategory.SelectById(_selectedCategoryId);
                        radCategories.Items[0].Text = selectedCategory.Title;                        
                        break;
                    case "Publication":
                        var selectedCategory2 = dataManager.PublicationCategory.SelectById(_selectedCategoryId);
                        radCategories.Items[0].Text = selectedCategory2.Title;                        
                        break;
                }
                 
                radCategories.Items[0].Value = _selectedCategoryId.ToString();                                    

                radCategories.SelectedValue = _selectedCategoryId.ToString();                

                RadTreeNode rtn = _radProductCategoryTreeView.FindNodeByValue(_selectedCategoryId.ToString());
                rtn.Expanded = true;
                rtn.Selected = true;
                rtn.ExpandParentNodes();

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
                return radCategories.CssClass;
            }
            set
            {
                radCategories.CssClass = value;
            }
        }



        /// <summary>
        /// Gets or sets the selected category id.
        /// </summary>
        /// <value>
        /// The selected category id.
        /// </value>
        public Guid SelectedCategoryId
        {
            get
            {
                if (!string.IsNullOrEmpty(radCategories.SelectedValue))
                    return Guid.Parse(radCategories.SelectedValue);

                return Guid.Empty;
            }
            set
            {
                _selectedCategoryId = value;
            }
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
        /// Gets or sets a value indicating whether [select default].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [select default]; otherwise, <c>false</c>.
        /// </value>
        public bool SelectDefault
        {
            get
            {
                if (ViewState["SelectDefault"] == null)
                    ViewState["SelectDefault"] = false;

                return bool.Parse(ViewState["SelectDefault"].ToString());
            }
            set { ViewState["SelectDefault"] = value; }
        }



        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>
        /// The category id.
        /// </value>
        public Guid CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
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
        /// Gets or sets the category type.
        /// </summary>
        /// <value>
        /// The category type.
        /// </value>
        public string CategoryType
        {
            get
            {
                if (ViewState["CategoryType"] == null)
                    return "Product";
                return (string)ViewState["CategoryType"];
            }
            set { ViewState["CategoryType"] = value; }
        }


        public Unit Width
        {
            get { return radCategories.Width; }
            set { radCategories.Width = value; }
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
    
    }
}