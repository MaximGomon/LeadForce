using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.UserControls
{
    public partial class SelectSiteActionTemplate : System.Web.UI.UserControl
    {
        private DataManager _dataManager = new DataManager();
        private Guid _siteId = CurrentUser.Instance.SiteID;

        public event SelectedChangedEventHandler SelectedChanged;
        public delegate void SelectedChangedEventHandler(object sender, SelectedChangedEventArgs e);
        public class SelectedChangedEventArgs : EventArgs
        {
            public Guid SiteActionTemplateId { get; set; }
        }



        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public Guid SiteActionTemplateId
        {
            get
            {
                if (ViewState["SiteActionTemplateId"] == null)
                    ViewState["SiteActionTemplateId"] = Guid.Empty;

                return (Guid)ViewState["SiteActionTemplateId"];
            }
            set
            {
                ViewState["SiteActionTemplateId"] = value;
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
        /// Inits the data.
        /// </summary>
        public void BindData()
        {
            rblActionTemplate.Items.Clear();

            foreach (var actionTemplate in EnumHelper.EnumToList<ActionTemplate>())
                rblActionTemplate.Items.Add(new ListItem(EnumHelper.GetEnumDescription(actionTemplate), ((int)actionTemplate).ToString()));
            rblActionTemplate.Items.FindByValue("0").Selected = true;

            var siteActionTemplates = _dataManager.SiteActionTemplate.SelectAll(_siteId).Where(a => a.SiteActionTemplateCategoryID == (int)SiteActionTemplateCategory.BaseTemplate);
            rrThumbnails.DataSource = siteActionTemplates;
            rrThumbnails.DataBind();
        }



        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the rblActionTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void rblActionTemplate_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblActionTemplate.SelectedValue.ToEnum<ActionTemplate>())
            {
                case ActionTemplate.PersonalTemplate:
                    var siteActionTemplate_PersonalTemplate = _dataManager.SiteActionTemplate.SelectAll(_siteId).Where(a => a.SiteActionTemplateCategoryID == (int)SiteActionTemplateCategory.BaseTemplate).ToList();
                    if (siteActionTemplate_PersonalTemplate.Count > 0)
                    {
                        rrThumbnails.DataSource = siteActionTemplate_PersonalTemplate;
                        rrThumbnails.DataBind();
                        rrThumbnails.Visible = true;
                    }
                    else
                        rrThumbnails.Visible = false;
                    break;
                case ActionTemplate.Design:
                    var siteActionTemplates_Design = _dataManager.SiteActionTemplate.SelectAllBySiteTemplates();
                    if (siteActionTemplates_Design.Count > 0)
                    {
                        rrThumbnails.DataSource = siteActionTemplates_Design;
                        rrThumbnails.DataBind();
                        rrThumbnails.Visible = true;
                    }
                    else
                        rrThumbnails.Visible = false;
                    break;
                case ActionTemplate.Empty:
                    rrThumbnails.Visible = false;
                    SiteActionTemplateId = Guid.Empty;

                    if (SelectedChanged != null)
                        SelectedChanged(this, new SelectedChangedEventArgs { SiteActionTemplateId = Guid.Empty });
                    break;
            }
        }



        /// <summary>
        /// Handles the OnItemDataBound event of the rrThumbnails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadRotatorEventArgs"/> instance containing the event data.</param>
        protected void rrThumbnails_OnItemDataBound(object sender, RadRotatorEventArgs e)
        {
            var siteActionTemplate = (tbl_SiteActionTemplate)e.Item.DataItem;
            var fileSystemProvider = new FileSystemProvider();

            ((LinkButton)e.Item.FindControl("lbThumbnail")).CommandArgument = siteActionTemplate.ID.ToString();

            ((Image)e.Item.FindControl("imgThumbnail")).ImageUrl = fileSystemProvider.GetLink(siteActionTemplate.SiteID, "SiteActionTemplates", siteActionTemplate.ID + ".png", FileType.Thumbnail);
            ((Image)e.Item.FindControl("imgThumbnail")).ToolTip = siteActionTemplate.Title;
            ((Image)e.Item.FindControl("imgThumbnail")).AlternateText = siteActionTemplate.Title;
        }



        /// <summary>
        /// Handles the OnCommand event of the lbThumbnail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        protected void lbThumbnail_OnCommand(object sender, CommandEventArgs e)
        {
            var siteActionTemplate = _dataManager.SiteActionTemplate.SelectById(e.CommandArgument.ToString().ToGuid());
            if (siteActionTemplate != null)
            {
                SiteActionTemplateId = Guid.Empty;

                if (SelectedChanged != null)
                    SelectedChanged(this, new SelectedChangedEventArgs { SiteActionTemplateId = siteActionTemplate.ID });
            }
        }
    }
}