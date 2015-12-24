using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace Labitec.LeadForce.Portal.Handlers
{
    public partial class ActivityRibbon : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        [WebMethod]
        public static IEnumerable<ActivityPublicationMap> GetPublications(string siteId, string portalSettingsId, int startIndex, string publicationType, string publicationCategory, string filter)
        {
            try
            {
                var dataManager = new DataManager();
                var publicationTypeId = Guid.Empty;
                var publicationCategoryId = Guid.Empty;

                if (!string.IsNullOrEmpty(publicationType))
                    publicationTypeId = Guid.Parse(publicationType);

                if (!string.IsNullOrEmpty(publicationCategory))
                    publicationCategoryId = Guid.Parse(publicationCategory);

                return dataManager.Publication.SelectActivityRibbon(Guid.Parse(siteId),
                                                                    CurrentUser.Instance == null ? null : CurrentUser.Instance.ContactID,
                                                                    publicationTypeId,
                                                                    publicationCategoryId, filter, startIndex, Guid.Parse(portalSettingsId));   
            }            
            catch(Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetPublications({0}, {1}, {2}, {3}, {4}, {5})", siteId, portalSettingsId, startIndex, publicationType, publicationCategory, filter), ex);
                return null;
            }
        }




        /// <summary>
        /// Searches the publication.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        [WebMethod]
        public static SearchPublicationResult SearchPublication(string siteId, string portalSettingsId , int startIndex, string text)
        {
            try
            {
                var dataManager = new DataManager();
                var totalCount = 0;
                var result = new SearchPublicationResult();
                result.Publications = dataManager.Publication.SearchPublication(Guid.Parse(siteId),
                                                                                CurrentUser.Instance != null ? CurrentUser.Instance.ContactID : null, 
                                                                                null,
                                                                                text, 
                                                                                startIndex,
                                                                                out totalCount,
                                                                                Guid.Parse(portalSettingsId));
                result.TotalCount = totalCount;
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon SearchPublication({0}, {1}, {2}, {3})", siteId, portalSettingsId, startIndex, text), ex);
                return null;
            }            
        }



        /// <summary>
        /// Searches the publication.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="text">The text.</param>        
        /// <param name="publicationType">Type of the publication.</param>
        /// <returns></returns>
        [WebMethod]
        public static SearchPublicationResult SearchPublicationForFeedback(string siteId, int startIndex, string text, string publicationType)
        {
            try
            {
                var dataManager = new DataManager();
                var totalCount = 0;
                var result = new SearchPublicationResult();
                result.Publications = dataManager.Publication.SearchPublicationForFeedback(Guid.Parse(siteId),
                    CurrentUser.Instance != null ? CurrentUser.Instance.ContactID : null,                                                                                
                                                                                text,
                                                                                startIndex,
                                                                                Guid.Parse(publicationType),
                                                                                out totalCount);
                result.TotalCount = totalCount;
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon SearchPublication({0},{1})", startIndex, text), ex);
                return null;
            }
        }



        /// <summary>
        /// Gets the publication.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="publicationId">The publicaiton id.</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetPublication(string siteId, string publicationId)
        {
            try
            {
                var dataManager = new DataManager();
                var result = dataManager.Publication.SelectPublicationMapById(Guid.Parse(siteId),
                                                                              Guid.Parse(publicationId),
                                                                              CurrentUser.Instance != null ? CurrentUser.Instance.ContactID: null);

                return result.Text.Replace("=\"/files/", "=\"" + Settings.LeadForceSiteUrl + "/files/");
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetPublication({0},{1})", siteId, publicationId), ex);
                return null;
            }
        }


        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<ActivityCommentMap> GetComments(string publicationId)
        {
            try
            {
                var dataManager = new DataManager();

                var result = dataManager.PublicationComment.SelectByPublicationIdForRibbon(Guid.Parse(publicationId), CurrentUser.Instance == null
                                                                                                                        ? null
                                                                                                                        : CurrentUser.Instance.ContactID);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetComments({0})", publicationId), ex);
                return null;
            }            
        }



        /// <summary>
        /// Leaves the comment.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        [WebMethod]
        public static ActivityCommentMap LeaveComment(string publicationId, string commentText, string fileName)
        {
            try
            {
                ActivityCommentMap publicationCommentMap = null;
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null)
                {
                    publicationCommentMap = new ActivityCommentMap() { ErrorMessage = "auth" };
                    return publicationCommentMap;
                }

                var dataManager = new DataManager();

                var publication = dataManager.Publication.SelectById(Guid.Parse(publicationId));

                if (publication != null && publication.AccessComment.HasValue)
                {
                    switch ((PublicationAccessComment)publication.AccessComment)
                    {
                        case PublicationAccessComment.Company:
                            publicationCommentMap = new ActivityCommentMap() { ErrorMessage = "access" };
                            return publicationCommentMap;
                        case PublicationAccessComment.Personal:
                            if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID != publication.AuthorID)
                            {
                                publicationCommentMap = new ActivityCommentMap() { ErrorMessage = "access" };
                                return publicationCommentMap;
                            }
                            break;
                    }
                }

                commentText = HttpUtility.HtmlEncode(commentText);

                publicationCommentMap = dataManager.PublicationComment.Add(CurrentUser.Instance.SiteID, (Guid)CurrentUser.Instance.ContactID, Guid.Parse(publicationId), commentText, fileName);

                return publicationCommentMap;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon LeaveComment({0}, {1})", publicationId, commentText), ex);

                return null;
            }            
        }



        /// <summary>
        /// Likes the publication.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        [WebMethod]
        public static string LikePublication(string publicationId)
        {
            try
            {
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null)
                    return "error";

                var dataManager = new DataManager();

                var publicationMark = dataManager.PublicationMark.SelectByPublicationAndUserId((Guid)CurrentUser.Instance.ContactID, Guid.Parse(publicationId));

                if (publicationMark == null)
                {
                    publicationMark = new tbl_PublicationMark
                    {
                        ID = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        PublicationID = Guid.Parse(publicationId),
                        UserID = (Guid)CurrentUser.Instance.ContactID,
                        TypeID = (int)PublicationMarkType.Like,
                        Rank = 1
                    };

                    dataManager.PublicationMark.Add(publicationMark);

                    return "like";
                }

                string result;

                if (publicationMark.Rank == 1)
                {
                    publicationMark.Rank = 0;
                    result = "unlike";
                }
                else
                {
                    publicationMark.Rank = 1;
                    result = "like";
                }

                publicationMark.CreatedAt = DateTime.Now;
                dataManager.PublicationMark.Update(publicationMark);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon LikePublication({0})", publicationId), ex);

                return "error";
            }            
        }



        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        [WebMethod]
        public static string LikeComment(string publicationId, string commentId)
        {
            try
            {
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null)
                    return "error";

                var dataManager = new DataManager();

                var publicationMark = dataManager.PublicationMark.SelectByPublicationAndPublicationCommentAndUserId((Guid)CurrentUser.Instance.ContactID, Guid.Parse(publicationId), Guid.Parse(commentId));

                if (publicationMark == null)
                {
                    publicationMark = new tbl_PublicationMark
                    {
                        ID = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        PublicationID = Guid.Parse(publicationId),
                        PublicationCommentID = Guid.Parse(commentId),
                        UserID = (Guid)CurrentUser.Instance.ContactID,
                        TypeID = (int)PublicationMarkType.Like,
                        Rank = 1
                    };

                    dataManager.PublicationMark.Add(publicationMark);

                    return "like";
                }

                string result;

                if (publicationMark.Rank == 1)
                {
                    publicationMark.Rank = 0;
                    result = "unlike";
                }
                else
                {
                    publicationMark.Rank = 1;
                    result = "like";
                }

                publicationMark.CreatedAt = DateTime.Now;
                dataManager.PublicationMark.Update(publicationMark);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon LikeComment({0}, {1})", publicationId, commentId), ex);

                return "error";
            }            
        }



        /// <summary>
        /// Checks the official answer.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        [WebMethod]
        public static string CheckOfficialAnswer(string publicationId, string commentId)
        {
            try
            {
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null)
                    return "error";

                var dataManager = new DataManager();
                var publicationComments = dataManager.PublicationComment.SelectByPublicationId(Guid.Parse(publicationId)).ToList();
                foreach (var publicationComment in publicationComments)
                {
                    publicationComment.isOfficialAnswer = false;
                    dataManager.PublicationComment.Update(publicationComment);
                }

                var guidCommentId = Guid.Parse(commentId);
                var toUpdatePublicationComment = publicationComments.SingleOrDefault(pc => pc.ID == guidCommentId);
                if (toUpdatePublicationComment != null)
                {
                    toUpdatePublicationComment.isOfficialAnswer = true;
                    dataManager.PublicationComment.Update(toUpdatePublicationComment);
                }

                return string.Empty;   
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon CheckOfficialAnswer({0}, {1})", publicationId, commentId), ex);

                return "error";
            }            
        }
    }

    public class SearchPublicationResult
    {
        public IEnumerable<ActivityPublicationMap> Publications;
        public int TotalCount;
    }
}