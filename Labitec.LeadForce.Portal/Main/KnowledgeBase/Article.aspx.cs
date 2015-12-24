using System;
using System.Configuration;
using Labitec.LeadForce.Portal.Shared.UserControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon;

namespace Labitec.LeadForce.Portal.Main.KnowledgeBase
{
    public partial class Article : LeadForcePortalBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "База знаний";

            ucPublicationComment.PublicationId = ObjectId;
            ucPublicationCategory.SelectedCategoryChanged += ucPublicationCategory_SelectedCategoryChanged;

            if (!Page.IsPostBack)
                BindData();
        }



        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var publication = DataManager.Publication.SelectByIdForPortal(SiteId, ObjectId);

            if (publication != null)
            {
                Title = string.Concat("База знаний - ", publication.Title);

                if (DataManager.Publication.IsAllow(publication, CurrentUser.Instance != null ? CurrentUser.Instance.ContactID : null, CurrentUser.Instance != null ? CurrentUser.Instance.CompanyID : null))
                {

                    lrlTitle.Text = publication.Title;
                    lrlNoun.Text = publication.Noun;
                    ucPublicationCategory.SelectedCategory = publication.PublicationCategoryID;
                    if (publication.Img != null)
                    {
                        plImage.Visible = true;
                        rbiImage.AlternateText = publication.Title;
                        rbiImage.ToolTip = publication.Title;
                        rbiImage.DataValue = publication.Img;
                    }

                    lrlText.Text = publication.Text.Replace("=\"/files/", "=\"" + Settings.LeadForceSiteUrl + "/files/");
                }
                else
                {
                    plArticle.Visible = false;
                    ucNotificationMessage.MessageType = NotificationMessageType.Warning;
                    ucNotificationMessage.Text = "Данная запись не доступна.";
                }
            }
            else
            {
                plArticle.Visible = false;
                ucNotificationMessage.MessageType = NotificationMessageType.Warning;
                ucNotificationMessage.Text = "Данная запись не доступна.";
            }
        }



        /// <summary>
        /// Handles the SelectedCategoryChanged event of the ucPublicationCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationCategory.SelectedCategoryChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationCategory_SelectedCategoryChanged(object sender, PublicationCategory.SelectedCategoryChangedEventArgs e)
        {
            var categoryId = e.SelectedCategory;
            if (categoryId != Guid.Empty)
                Response.Redirect(UrlsData.LFP_KnowledgeBase(PortalSettingsId, categoryId));
            else
                Response.Redirect(UrlsData.LFP_KnowledgeBase(PortalSettingsId));
        }
    }
}