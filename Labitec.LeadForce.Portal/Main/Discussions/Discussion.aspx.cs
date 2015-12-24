using System;
using Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon;
using Labitec.LeadForce.Portal.Shared.UserControls;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using System.Linq;
using WebCounter.BusinessLogicLayer.Enumerations;

namespace Labitec.LeadForce.Portal.Main.Discussions
{
    public partial class Discussion : LeadForcePortalBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Обсуждения";

            ucPublicationComment.PublicationId = ObjectId;
            ucPublicationCategory.SelectedCategoryChanged += ucPublicationCategory_SelectedCategoryChanged;
            ucPublicationTypes.SelectedIndexChanged += ucPublicationTypes_SelectedIndexChanged;

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
                Title = string.Concat("Обсуждения - ", publication.Title);

                if (DataManager.Publication.IsAllow(publication, CurrentUser.Instance != null ? CurrentUser.Instance.ContactID : null, CurrentUser.Instance != null ? CurrentUser.Instance.CompanyID : null))
                {
                    lrlTitle.Text = publication.Title;
                    ucPublicationCategory.SelectedCategory = publication.PublicationCategoryID;
                    ucPublicationTypes.SelectedValue = publication.PublicationTypeID;

                    Session["Portal-Discussion-CategoryId"] = publication.PublicationCategoryID;
                    Session["Portal-Discussion-PublicationTypeId"] = publication.PublicationTypeID;

                    if (publication.Img != null)
                    {
                        plImage.Visible = true;
                        rbiImage.AlternateText = publication.Title;
                        rbiImage.ToolTip = publication.Title;
                        rbiImage.DataValue = publication.Img;
                    }

                    lrlText.Text = publication.Text;

                    var offComment =
                        DataManager.PublicationComment.SelectByPublicationId(publication.ID).FirstOrDefault(
                            pc => pc.isOfficialAnswer);
                    if (offComment != null)
                    {
                        plOffComment.Visible = true;
                        lrlOffComment.Text = offComment.Comment.ToHtml();
                    }
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
            if (Page.IsPostBack)
            {
                Session["Portal-Discussion-CategoryId"] = e.SelectedCategory;
                Response.Redirect(UrlsData.LFP_Discussions(PortalSettingsId));
            }
        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the ucPublicationTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationTypes.SelectedIndexChangedEventArgs"/> instance containing the event data.</param>
        protected void ucPublicationTypes_SelectedIndexChanged(object sender, PublicationTypes.SelectedIndexChangedEventArgs e)
        {
            if (Page.IsPostBack)
            {
                Session["Portal-Discussion-PublicationTypeId"] = e.SelectedValue;
                Response.Redirect(UrlsData.LFP_Discussions(PortalSettingsId));
            }
        }
    }
}