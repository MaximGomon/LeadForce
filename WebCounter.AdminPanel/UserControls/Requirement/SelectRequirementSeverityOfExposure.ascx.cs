using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.Requirement
{
    public partial class SelectRequirementSeverityOfExposure : System.Web.UI.UserControl
    {
        private Guid _selectedCategoryId;
        private Guid _categoryId = Guid.Empty;        
        private readonly DataManager _dataManager = new DataManager();
        private string _validationGroup = String.Empty;
        private string _validationErrorMessage = "Выберите значение";
        private RadTreeView _radRequirementSeverityOfExposureTreeView = new RadTreeView();



        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            rfvRequirementSeverityOfExposures.ErrorMessage = _validationErrorMessage;

            _radRequirementSeverityOfExposureTreeView = ((RadTreeView)radRequirementSeverityOfExposures.Items[0].FindControl("radRequirementSeverityOfExposureTreeView"));
            
            _radRequirementSeverityOfExposureTreeView.Nodes.Clear();

            if (ShowEmpty)
                radRequirementSeverityOfExposures.EmptyMessage = EmptyItemText;
            if (_validationGroup != String.Empty)
            {
                rfvRequirementSeverityOfExposures.ValidationGroup = _validationGroup;
                rfvRequirementSeverityOfExposures.Enabled = true;
            }
            else
                rfvRequirementSeverityOfExposures.Enabled = false;

            var requirementSeverityOfExposures = _dataManager.RequirementSeverityOfExposure.SelectHierarchy(CurrentUser.Instance.SiteID, RequirementTypeId);

            _radRequirementSeverityOfExposureTreeView.DataSource = requirementSeverityOfExposures;         
            _radRequirementSeverityOfExposureTreeView.DataTextField = "Title";
            _radRequirementSeverityOfExposureTreeView.DataFieldID = "ID";
            _radRequirementSeverityOfExposureTreeView.DataValueField = "ID";
            _radRequirementSeverityOfExposureTreeView.DataFieldParentID = "ParentID";
            _radRequirementSeverityOfExposureTreeView.DataBind();            

            if (_selectedCategoryId!=Guid.Empty)
            {
                var selectedCategory = _dataManager.RequirementSeverityOfExposure.SelectById(_selectedCategoryId);

                radRequirementSeverityOfExposures.Items[0].Text = selectedCategory.Title;                                                         
                radRequirementSeverityOfExposures.Items[0].Value = _selectedCategoryId.ToString();
                radRequirementSeverityOfExposures.SelectedValue = _selectedCategoryId.ToString();                

                RadTreeNode rtn = _radRequirementSeverityOfExposureTreeView.FindNodeByValue(_selectedCategoryId.ToString());
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
                return radRequirementSeverityOfExposures.CssClass;
            }
            set
            {
                radRequirementSeverityOfExposures.CssClass = value;
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
                if (!string.IsNullOrEmpty(radRequirementSeverityOfExposures.SelectedValue))
                    return Guid.Parse(radRequirementSeverityOfExposures.SelectedValue);

                return Guid.Empty;
            }
            set
            {
                _selectedCategoryId = value;
            }
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


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? RequirementTypeId
        {
            get
            {                
                return (Guid?)ViewState["RequirementTypeId"];
            }
            set
            {
                ViewState["RequirementTypeId"] = value;
            }
        }
    }
}