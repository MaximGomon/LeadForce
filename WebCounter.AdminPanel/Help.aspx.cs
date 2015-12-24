using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace WebCounter.AdminPanel
{
    public partial class Help : System.Web.UI.Page
    {
        private DataManager dataManager = new DataManager();
        private Guid SiteID;
        protected void Page_Load(object sender, EventArgs e)
        {
                Page.Header.DataBind();
            if (Request.Params["SiteID"] != null && Guid.TryParse(Request.Params["SiteID"], out SiteID))
            {
                if (((TextBox)mainToolBar.Items[0].FindControl("txtSearchQuery")).Text != "")
                {
                    doSearch(Guid.Empty, ((TextBox)mainToolBar.Items[0].FindControl("txtSearchQuery")).Text);
                }
                if (!Page.IsPostBack)
                {
                    var categories = GetCategories(SiteID, null);

                    RadToolBarDropDown rtbd = ((RadToolBarDropDown) mainToolBar.Items[1]);
                    RadToolBarButton newButton = new RadToolBarButton() { Text = "Все блоки", Value = Guid.Empty.ToString() };
                    rtbd.Buttons.Add(newButton);

                    foreach (var categoryItem in categories)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Value = categoryItem.Id.ToString();
                        node.Text = categoryItem.Title;
                        node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;

                        radPublicationCategoryTreeView.Nodes.Add(node);

                        newButton = new RadToolBarButton() { Text = categoryItem.Title, Value = categoryItem.Id.ToString() };
                        rtbd.Buttons.Add(newButton);

                    }
                    radPublicationCategoryTreeView.DataTextField = "Title";
                    radPublicationCategoryTreeView.DataFieldID = "Id";
                    radPublicationCategoryTreeView.DataValueField = "Id";
                    radPublicationCategoryTreeView.DataFieldParentID = "ParentId";
                    radPublicationCategoryTreeView.DataBind();
                }
            }
            //if (CategoryID != Guid.Empty)
            //{
            //    RadTreeNode rtn = radPublicationCategoryTreeView.FindNodeByValue(CategoryID.ToString());
            //    rtn.Expanded = true;
            //    rtn.Selected = true;
            //    rtn.ExpandParentNodes();
            //}


        }

        private void doSearch(Guid categoryID, string text)
        {

//            var publications = dataManager.Publication.Search(categoryID, text).Where(p => p.StatusID == (int)PublicationStatus.Published);
            var publications = dataManager.Publication.Search(categoryID, text);

            rtwSearchResult.Nodes.Clear();
            foreach (var tblPublication in publications)
            {
                RadTreeNode node = new RadTreeNode();
                node.Value = tblPublication.ID.ToString();
                node.Text = tblPublication.Title;
                node.ImageUrl = "~/App_Themes/Default/images/icoHelp.png";
                node.Attributes.Add("Type0", "Publication");
                node.Attributes.Add("Type", "Publication");
                
                rtwSearchResult.Nodes.Add(node);

            }
        }

        protected void mainToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string message = "ButtonClick: " + e.Item.Value;
            string  searchQuery = ((TextBox)mainToolBar.Items[0].FindControl("txtSearchQuery")).Text;
            Guid categoryID = Guid.Parse(e.Item.Value);


            doSearch(categoryID, searchQuery);

        }



        protected void radPublicationCategoryTreeView_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            var categories = GetCategories(SiteID, Guid.Parse(e.Node.Value));

            foreach (var categoryItem in categories)
            {
                RadTreeNode node = new RadTreeNode();
                node.Value = categoryItem.Id.ToString();
                node.Text = categoryItem.Title;
                node.Attributes.Add("Type", "Folder");
                node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
                e.Node.Nodes.Add(node);    
            }

//            var publications = dataManager.Publication.SelectByCategoryId(Guid.Parse(e.Node.Value)).Where(a => a.StatusID == (int)PublicationStatus.Published);
            var publications = dataManager.Publication.SelectByCategoryId(Guid.Parse(e.Node.Value));

            foreach (var publicationItem in publications)
            {
                RadTreeNode node = new RadTreeNode();
                node.Value = publicationItem.ID.ToString();
                node.Text = publicationItem.Title;
                node.ImageUrl = "~/App_Themes/Default/images/icoHelp.png";
                node.Attributes.Add("Type0", "Publication");
                node.Attributes.Add("Type", "Publication");
                e.Node.Nodes.Add(node);
            }

            
        }



        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        private List<CategoryItem> GetCategories(Guid SiteID, Guid? CategoryID)
        {
            var categories = dataManager.PublicationCategory.SelectByParentId(SiteID ,CategoryID);
            List<CategoryItem> ret = categories.Select(category => new CategoryItem()
            {
                Id = category.ID,
                ParentId = category.ParentID,
                Title = category.Title,
            }).ToList();
            return ret;
        }

        public class CategoryItem
        {
            public Guid Id { get; set; }
            public Guid? ParentId { get; set; }
            public string Title { get; set; }
        }

        protected void radPublicationCategoryTreeView_NodeClick(object sender, RadTreeNodeEventArgs e)
        {

            if (e.Node.Attributes["Type0"] == "Publication" || e.Node.Attributes["Type"] == "Publication")
            {
                Guid publicationId = Guid.Parse(e.Node.Value);
                _showPublication(publicationId);
            }
        }


        private void _showPublication(Guid publicationId)
        {
            var publication = dataManager.Publication.SelectById(SiteID, publicationId);
            lTitle.Text = publication.Title;
            lNoun.Text = publication.Noun;
            lText.Text = publication.Text;
            if (publication.Img != null)
            {
                radBinaryImage.DataValue = publication.Img;
                radBinaryImage.Visible = true;
            }
            else
            {
                radBinaryImage.Visible = false;
            }

            //var relatedPublications =
            //    dataManager.RelatedPublication.SelectByPublicationId(publicationId).Where(
            //        a => a.tbl_Publication1.StatusID == (int)PublicationStatus.Published);


            //var relatedPublications =
            //    dataManager.RelatedPublication.SelectByPublicationId(publicationId).Where(a=>a.RelatedPublicationID != null);
            //List<CategoryItem> ret = relatedPublications.Select(category => new CategoryItem()
            //{
            //    Id = category.RelatedPublicationID ?? Guid.Empty,
            //    Title = category.tbl_Publication1.Title
            //}).ToList();
            //rtwRelated.Nodes.Clear();
            //foreach (var categoryItem in ret)
            //{
            //    RadTreeNode node = new RadTreeNode();
            //    node.Value = categoryItem.Id.ToString();
            //    node.Text = categoryItem.Title;
            //    node.ImageUrl = "~/App_Themes/Default/images/icoHelp.png";
            //    node.Attributes.Add("Type0", "Publication");
            //    node.Attributes.Add("Type", "Publication");
            //    rtwRelated.Nodes.Add(node);

            //}
        }
    }
}