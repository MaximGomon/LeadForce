using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;

namespace WebCounter.AdminPanel.UserControls.ActivityRibbon
{
    public partial class PublicationCategory : System.Web.UI.UserControl
    {
        private readonly DataManager _dataManager = new DataManager();

        public event SelectedCategoryChangedEventHandler SelectedCategoryChanged;
        public delegate void SelectedCategoryChangedEventHandler(object sender, SelectedCategoryChangedEventArgs e);
        public class SelectedCategoryChangedEventArgs : EventArgs
        {
            public Guid SelectedCategory { get; set; }
        }


        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid? SelectedCategory
        {
            get
            {
                if (!string.IsNullOrEmpty(rtvPublicationCategory.SelectedValue))
                    return Guid.Parse(rtvPublicationCategory.SelectedValue);

                return null;
            }            
        }


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
        private void BindData()
        {
            rtvPublicationCategory.DataSource = GetCategories();
            rtvPublicationCategory.DataTextField = "Title";
            rtvPublicationCategory.DataFieldID = "Id";
            rtvPublicationCategory.DataValueField = "Id";
            rtvPublicationCategory.DataFieldParentID = "ParentId";
            rtvPublicationCategory.DataBind();
        }



        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        private List<CategoryItem> GetCategories()
        {
            var categories = _dataManager.PublicationCategory.SelectAll(CurrentUser.Instance.SiteID).OrderBy(pc => pc.Order);
            var result = categories.Select(category => new CategoryItem()
                                                        {
                                                            Id = category.ID,
                                                            ParentId = category.ParentID,
                                                            Title = category.Title,
                                                        }).ToList();

            result.Insert(0, new CategoryItem()
                              {
                                  Id = Guid.Empty,
                                  ParentId = null,
                                  Title = "Любая категория",
                              });

            return result;
        }



        /// <summary>
        /// Handles the OnNodeClick event of the rtvPublicationCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTreeNodeEventArgs"/> instance containing the event data.</param>
        protected void rtvPublicationCategory_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (SelectedCategoryChanged != null)
            {
                var eventArgs = new SelectedCategoryChangedEventArgs {SelectedCategory = Guid.Parse(e.Node.Value)};

                SelectedCategoryChanged(this, eventArgs);
            }
        }
    }
}