using System;
using System.Collections.Generic;
using System.Linq;
using Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon;
using Telerik.Web.UI;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;

namespace Labitec.LeadForce.Portal.Main.KnowledgeBase
{
    public partial class KnowledgeBase : LeadForcePortalBasePage
    {
        protected int PageIndex;
        protected Guid CategoryId;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "База знаний";

            var sCategoryId = Page.RouteData.Values["categoryId"] as string;
            if (!string.IsNullOrEmpty(sCategoryId))
            {
                CategoryId = Guid.Parse(sCategoryId);
                ucPublicationCategory.SelectedCategory = CategoryId;
            }

            ucPublicationCategory.SelectedCategoryChanged += ucPublicationCategory_SelectedCategoryChanged;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Handles the SelectedCategoryChanged event of the ucPublicationCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationCategory.SelectedCategoryChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationCategory_SelectedCategoryChanged(object sender, PublicationCategory.SelectedCategoryChangedEventArgs e)
        {
            CategoryId = e.SelectedCategory;
            if (CategoryId != Guid.Empty)            
                Response.Redirect(UrlsData.LFP_KnowledgeBase(PortalSettingsId, CategoryId));
            else
                Response.Redirect(UrlsData.LFP_KnowledgeBase(PortalSettingsId));
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var startRowIndex = PageIndex * radDataPager.PageSize;
            
            var contactId = CurrentUser.Instance != null ? CurrentUser.Instance.ContactID : null;
            List<ActivityPublicationMap> publicationsMap;

            var publicationType = DataManager.PublicationType.SelectByPublicationKindID(((LeadForcePortalBasePage)Page).SiteId,
                                                                           (int)PublicationKind.Discussion).OrderBy(p => p.Order).FirstOrDefault();
            if (publicationType != null)
                ucCreatePublication.SelectedValue = publicationType.ID;
            else
                ucCreatePublication.Visible = false;

            if (CategoryId == Guid.Empty)
            {
                rlvPublications.VirtualItemCount = DataManager.Publication.SelectKnowledgeBase(SiteId, contactId).Count();
                publicationsMap =
                    DataManager.Publication.SelectKnowledgeBase(SiteId, contactId)
                                           .OrderByDescending(p => p.Date)
                                           .Skip(startRowIndex)
                                           .Take(rlvPublications.PageSize).ToList();
            }
            else
            {
                rlvPublications.VirtualItemCount = DataManager.Publication.SelectKnowledgeBaseByCategoryId(SiteId, CategoryId, contactId).Count();
                publicationsMap = DataManager.Publication.SelectKnowledgeBaseByCategoryId(SiteId, CategoryId, contactId)
                                                                    .OrderByDescending(p => p.Date)
                                                                    .Skip(startRowIndex)
                                                                    .Take(radDataPager.PageSize).ToList();
            }

            foreach (var publicationMap in publicationsMap)
            {                
                if (publicationMap.Date.HasValue)
                    publicationMap.FormattedDate = ((DateTime)publicationMap.Date).ToString("d MMMM в HH:mm");
            }

            rlvPublications.DataSource = publicationsMap;

            rlvPublications.DataBind();


            if (radDataPager.PageCount <= 1)
                radDataPager.Visible = false;            
        }



        /// <summary>
        /// Handles the OnPageIndexChanged event of the rlvPublications control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadListViewPageChangedEventArgs"/> instance containing the event data.</param>
        protected void rlvPublications_OnPageIndexChanged(object sender, RadListViewPageChangedEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            BindData();   
        }
    }
}