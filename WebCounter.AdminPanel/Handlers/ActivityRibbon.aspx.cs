using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.AdminPanel.Handlers
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



        /// <summary>
        /// Gets the publications.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="publicationType">Type of the publication.</param>
        /// <param name="publicationCategory">The publication category.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<ActivityPublicationMap> GetPublications(int startIndex, string publicationType, string publicationCategory, string filter)
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

                return dataManager.Publication.SelectActivityRibbon(CurrentUser.Instance.SiteID,
                                                                    (Guid)CurrentUser.Instance.ContactID,
                                                                    publicationTypeId,
                                                                    publicationCategoryId, filter, startIndex);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetPublications({0},{1},{2},{3})", startIndex, publicationType, publicationCategory, filter), ex);
                return null;
            }            
        }



        /// <summary>
        /// Searches the publication.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="text">The text.</param>
        /// <param name="publicationKind">Kind of the publication.</param>
        /// <returns></returns>
        [WebMethod]
        public static SearchPublicationResult SearchPublication(int startIndex, string text, string publicationKind)
        {
            try
            {
                var dataManager = new DataManager();
                var totalCount = 0;
                var result = new SearchPublicationResult();
                result.Publications = dataManager.Publication.SearchPublication(CurrentUser.Instance.SiteID, 
                                                                                CurrentUser.Instance.ContactID, 
                                                                                string.IsNullOrEmpty(publicationKind) ? null : (int?)int.Parse(publicationKind), 
                                                                                text, 
                                                                                startIndex, 
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

                var result = dataManager.PublicationComment.SelectByPublicationIdForRibbon(Guid.Parse(publicationId), (Guid)CurrentUser.Instance.ContactID);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetComment({0})", publicationId), ex);
                return null;
            }            
        }



        /// <summary>
        /// Gets the comment text.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCommentText(string commentId)
        {
            try
            {
                var dataManager = new DataManager();

                var result = dataManager.PublicationComment.SelectById(Guid.Parse(commentId)).Comment;

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon GetCommentText({0})", commentId), ex);
                return null;
            }            
        }



        /// <summary>
        /// Updates the comment text.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateCommentText(string commentId, string commentText)
        {
            try
            {
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null || CurrentUser.Instance.AccessLevelID == (int)AccessLevel.Portal)
                    return string.Empty;

                var dataManager = new DataManager();

                var comment = dataManager.PublicationComment.SelectById(Guid.Parse(commentId));

                if (comment != null && comment.tbl_Publication.SiteID == CurrentUser.Instance.SiteID)
                {
                    commentText = HttpUtility.HtmlEncode(commentText);
                    comment.Comment = commentText;
                    dataManager.PublicationComment.Update(comment);

                    return comment.Comment.ToHtml();
                }

                return string.Empty;            
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon UpdateCommentText({0}, {1})", commentId, commentText), ex);
                return null;
            }
        }



        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteComment(string commentId)
        {
            try
            {
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null || CurrentUser.Instance.AccessLevelID == (int)AccessLevel.Portal)
                    return false;

                var dataManager = new DataManager();

                var comment = dataManager.PublicationComment.SelectById(Guid.Parse(commentId));

                if (comment != null && comment.tbl_Publication.SiteID == CurrentUser.Instance.SiteID)
                {
                    dataManager.PublicationComment.Delete(comment);
                    return true;
                }

                return false;    
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ActivityRibbon DeleteComment({0})", commentId), ex);
                return false;
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
                if (CurrentUser.Instance == null || CurrentUser.Instance.ContactID == null)
                    return new ActivityCommentMap();

                var dataManager = new DataManager();

                commentText = HttpUtility.HtmlEncode(commentText);

                var publicationCommentMap = dataManager.PublicationComment.Add(CurrentUser.Instance.SiteID, (Guid)CurrentUser.Instance.ContactID, Guid.Parse(publicationId), commentText, fileName);

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
                return null;
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
                return null;
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