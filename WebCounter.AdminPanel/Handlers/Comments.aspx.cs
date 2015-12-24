using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Linq;
using Microsoft.Security.Application;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;

namespace WebCounter.AdminPanel.Handlers
{
    public partial class Comments : System.Web.UI.Page
    {        
        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<ContentComment> GetComments(string contentId, string commentType)
        {
            try
            {                
                var result = ContentCommentRepository.GetComments(CurrentUser.Instance, Guid.Parse(contentId), commentType.ToEnum<CommentTables>()).OrderByDescending(c => c.CreatedAt).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Comments GetComment({0}, {1})", contentId, commentType), ex);
                return null;
            }
        }



        /// <summary>
        /// Gets the content commentators.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static IEnumerable<ContentCommentAuthor> GetContentCommentAuthors(string contentId, string commentType)
        {
            try
            {
                var result = ContentCommentRepository.SelectContentCommentAuthors(CurrentUser.Instance.SiteID, Guid.Parse(contentId), commentType.ToEnum<CommentTables>());
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Comments GetContentCommentAuthors({0}, {1})", contentId, commentType), ex);
                return null;
            }
        }



        /// <summary>
        /// Leaves the comment.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="destinationUserId">The destination user id.</param>
        /// <param name="replyToCommentId">The reply to comment id.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <param name="isInternal">The is internal.</param>
        /// <returns></returns>
        [WebMethod]
        public static ContentComment LeaveComment(string contentId, string commentText, string destinationUserId, string replyToCommentId, string fileName, string commentType, string isInternal)
        {
            try
            {
                var guidDestinationUserId = !string.IsNullOrEmpty(destinationUserId)
                                                ? (Guid?) Guid.Parse(destinationUserId)
                                                : null;

                var guidReplyToCommentId = !string.IsNullOrEmpty(replyToCommentId)
                                                ? (Guid?)Guid.Parse(replyToCommentId)
                                                : null;
                commentText = commentText.Replace("\n", "#@#");
                commentText = Sanitizer.GetSafeHtmlFragment(commentText);
                commentText = commentText.Replace("\r\n","")
                                        .Replace("<p><br></p>", "")
                                        .Replace("#@#", "\n");

                var publicationCommentMap = ContentCommentRepository.Add(CurrentUser.Instance.SiteID, CurrentUser.Instance.ID, Guid.Parse(contentId),
                                                                         commentText, guidDestinationUserId, guidReplyToCommentId, fileName,
                                                                         commentType.ToEnum<CommentTables>(), bool.Parse(isInternal));

                return publicationCommentMap;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment LeaveComment({0}, {1})", contentId, commentText), ex);
                return null;
            }
        }


        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static string LikeComment(string contentId, string commentId, string commentType)
        {
            try
            {
                var isLike = ContentCommentRepository.LikeComment(CurrentUser.Instance, Guid.Parse(contentId), Guid.Parse(commentId), commentType.ToEnum<CommentTables>());

                return isLike.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment LikeComment({0}, {1}, {2})", contentId, commentId, commentType), ex);
                return null;
            }
        }



        /// <summary>
        /// Checks the official answer.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static string CheckOfficialAnswer(string contentId, string commentId, string commentType)
        {
            try
            {
                ContentCommentRepository.UpdateOfficialAnswers(CurrentUser.Instance.SiteID, Guid.Parse(contentId), commentType.ToEnum<CommentTables>());                
                var guidCommentId = Guid.Parse(commentId);
                var toUpdatePublicationComment = ContentCommentRepository.SelectById(CurrentUser.Instance.SiteID, guidCommentId, commentType.ToEnum<CommentTables>());
                if (toUpdatePublicationComment != null)
                {
                    toUpdatePublicationComment.IsOfficialAnswer = true;
                    ContentCommentRepository.Update(CurrentUser.Instance.SiteID, toUpdatePublicationComment, commentType.ToEnum<CommentTables>());
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment CheckOfficialAnswer({0}, {1}, {2})", contentId, commentId, commentType), ex);
                return "error";
            }
        }



        /// <summary>
        /// Gets the comment text.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static ContentComment GetComment(string commentId, string commentType)
        {
            try
            {
                var result = ContentCommentRepository.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(commentId),
                                                                 commentType.ToEnum<CommentTables>());

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment GetCommentText({0}, {1})", commentId, commentType), ex);
                return null;
            }
        }



        /// <summary>
        /// Updates the comment text.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static string UpdateCommentText(string commentId, string commentText, string commentType)
        {
            try
            {
                var comment = ContentCommentRepository.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(commentId), commentType.ToEnum<CommentTables>());

                if (comment != null && comment.SiteID == CurrentUser.Instance.SiteID)
                {
                    commentText = Sanitizer.GetSafeHtmlFragment(commentText);

                    comment.Comment = commentText;
                    ContentCommentRepository.Update(CurrentUser.Instance.SiteID, comment, commentType.ToEnum<CommentTables>());
                    return comment.Comment.ToHtml();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment UpdateCommentText({0}, {1}, {2})", commentId, commentText, commentType), ex);
                return null;
            }
        }



        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteComment(string commentId, string commentType)
        {
            try
            {
                var contentComment = ContentCommentRepository.SelectById(CurrentUser.Instance.SiteID, Guid.Parse(commentId), commentType.ToEnum<CommentTables>());
                if (contentComment != null && !string.IsNullOrEmpty(contentComment.FileName))
                {
                    var fsp = new FileSystemProvider();
                    fsp.Delete(CurrentUser.Instance.SiteID, EnumHelper.GetEnumDescription(commentType.ToEnum<CommentTables>()), contentComment.FileName, FileType.Attachment);
                }

                ContentCommentRepository.Delete(CurrentUser.Instance.SiteID, Guid.Parse(commentId), commentType.ToEnum<CommentTables>());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ContentComment DeleteComment({0}, {1})", commentId, commentType), ex);
                return false;
            }
        }
    }
}