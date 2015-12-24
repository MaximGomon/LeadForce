using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.BusinessLogicLayer.Services;

namespace WebCounter.BusinessLogicLayer
{
    public class ContentCommentRepository
    {
        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <param name="isPortal">if set to <c>true</c> [is portal].</param>
        /// <returns></returns>
        public static int GetCommentsCount(Guid siteId, Guid contentId, CommentTables commentTable, bool isPortal = false)
        {
            int result = 0;

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {                
                connection.Open();
                var query = string.Format("SELECT COUNT(*) AS Cnt FROM {0} WHERE ContentID='{1}' AND SiteID = '{2}' ", commentTable.ToString(), contentId, siteId);

                if (isPortal)
                    query += "AND IsInternal = 0";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = (int) reader["Cnt"];
                        }
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <param name="isPortal">if set to <c>true</c> [is portal].</param>
        /// <returns></returns>
        public static IEnumerable<ContentComment> GetComments(UserMap user, Guid contentId, CommentTables commentTable, bool isPortal = false)
        {
            var result = new List<ContentComment>();

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();
                var query = string.Format("SELECT c.ID, c.ContentID, c.CreatedAt, c.UserID, contact.UserFullName, dcontact.UserFullName as DestinationUserFullName, c.Comment, c.IsInternal, c.IsOfficialAnswer, c.FileName, " +
                                          " (CASE WHEN (SELECT [Rank] FROM {0}Mark WHERE ContentCommentID = c.ID AND UserID = @UserID) = 1 THEN 'true' ELSE 'false' END) AS ContactLike, ISNULL(s.[Rank], 0) AS SumLike " +
                                          "FROM {0} as c " +
                                          "LEFT JOIN tbl_User as u ON c.UserID = u.ID " +
                                          "LEFT JOIN tbl_Contact as contact ON u.ContactID = contact.ID " +
                                          "LEFT JOIN tbl_User as du ON c.DestinationUserID = du.ID " +
                                          "LEFT JOIN tbl_Contact as dcontact ON du.ContactID = dcontact.ID " +
                                          "LEFT JOIN {0}Mark AS m ON c.ID = m.ContentCommentID " +
                                          "LEFT JOIN (SELECT ContentCommentID, SUM([Rank]) as [Rank] " +
                                                      "FROM {0}Mark " +
                                                      "GROUP BY ContentCommentID) s ON c.ID = s.ContentCommentID " +
                                          "WHERE ContentID = @ContentID AND c.SiteID = @SiteID AND (@IsInternal IS NULL OR IsInternal = @IsInternal) ", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContentID", contentId);
                    command.Parameters.AddWithValue("@SiteID", user.SiteID);
                    command.Parameters.AddWithValue("@UserID", user.ID);

                    if (isPortal)
                        command.Parameters.AddWithValue("@IsInternal", false);
                    else
                        command.Parameters.AddWithValue("@IsInternal", DBNull.Value);

                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new ContentComment()
                                               {
                                                   ID = (Guid)reader["ID"],
                                                   ContentID = (Guid)reader["ContentID"],
                                                   UserID = (Guid)reader["UserID"],
                                                   CreatedAt = (DateTime)reader["CreatedAt"],
                                                   FormattedDate = ((DateTime)reader["CreatedAt"]).ToString("d MMMM в HH:mm"),
                                                   UserFullName = reader["UserFullName"] != DBNull.Value ? (string)reader["UserFullName"] : null,
                                                   DestinationUserFullName = reader["DestinationUserFullName"] != DBNull.Value ? (string)reader["DestinationUserFullName"] : null,
                                                   Comment = reader["Comment"] != DBNull.Value ? ((string)reader["Comment"]).ToHtml() : null,
                                                   IsOfficialAnswer = reader["IsOfficialAnswer"] != DBNull.Value && (bool)reader["IsOfficialAnswer"],
                                                   IsInternal = reader["IsInternal"] != DBNull.Value && (bool)reader["IsInternal"],
                                                   FileName = reader["FileName"] != DBNull.Value ? (string)reader["FileName"] : null,
                                                   ContactLike = bool.Parse((string)reader["ContactLike"]),
                                                   SumLike = (int)reader["SumLike"]
                                               });
                            }

                            reader.NextResult();
                        }
                    }
                }
            }

            var fsp = new FileSystemProvider();

            foreach (ContentComment contentComment in result)
            {
                if (!string.IsNullOrEmpty(contentComment.FileName))
                    contentComment.VirtualFileName = fsp.GetLink(user.SiteID, EnumHelper.GetEnumDescription(commentTable), contentComment.FileName, FileType.Attachment);
            }

            return result;
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="destiantionUserId">The destiantion user id.</param>
        /// <param name="replyToCommentId">The reply to comment id.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public static ContentComment Add(Guid siteId, Guid userId, Guid contentId, string commentText, Guid? destiantionUserId, Guid? replyToCommentId, string fileName, CommentTables commentTable, bool isInternal)
        {
            return Add(siteId, userId, contentId, commentText, destiantionUserId, replyToCommentId, fileName, commentTable, false, true, isInternal);
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="destiantionUserId">The destiantion user id.</param>
        /// <param name="replyToCommentId">The reply to comment id.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <returns></returns>
        public static ContentComment Add(Guid siteId, Guid userId, Guid contentId, string commentText, Guid? destiantionUserId, Guid? replyToCommentId, string fileName, CommentTables commentTable)
        {
            return Add(siteId, userId, contentId, commentText, destiantionUserId, replyToCommentId, fileName, commentTable, false, true);
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="destiantionUserId">The destiantion user id.</param>
        /// <param name="replyToCommentId">The reply to comment id.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <param name="isOfficialAnswer">if set to <c>true</c> [is official answer].</param>
        /// <param name="sendNotification">if set to <c>true</c> [send notification].</param>
        /// <param name="isInternal">if set to <c>true</c> [is internal].</param>
        /// <returns></returns>
        public static ContentComment Add(Guid siteId, Guid userId, Guid contentId, string commentText, Guid? destiantionUserId, Guid? replyToCommentId, string fileName, CommentTables commentTable, bool isOfficialAnswer, bool sendNotification, bool isInternal = false)
        {
            if (string.IsNullOrEmpty(commentText.Trim()) && string.IsNullOrEmpty(fileName))
                return null;

            var result = new ContentComment();

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var id = Guid.NewGuid();

                var query = string.Format("INSERT INTO {0} ([ID], [SiteId], [ContentID], [UserID], [DestinationUserID], [ReplyToID], [Comment], [FileName], [IsOfficialAnswer], [IsInternal]) " +
                                          "VALUES (@ID, @SiteID, @ContentID, @UserID, @DestinationUserID, @ReplyToID, @Comment, @FileName, @IsOfficialAnswer, @IsInternal) " +
                                          "SELECT c.ID, c.ContentID, c.UserID, c.CreatedAt, contact.UserFullName, dcontact.UserFullName as DestinationUserFullName, c.Comment, c.IsOfficialAnswer, c.IsInternal, c.FileName " +
                                          "FROM {0} as c " +
                                          "LEFT JOIN tbl_User as u ON c.UserID = u.ID " +                                          
                                          "LEFT JOIN tbl_Contact as contact ON u.ContactID = contact.ID " +
                                          "LEFT JOIN tbl_User as du ON c.DestinationUserID = du.ID " +
                                          "LEFT JOIN tbl_Contact as dcontact ON du.ContactID = dcontact.ID " +
                                          "WHERE c.ID = @ID", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@ContentID", contentId);
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@IsInternal", isInternal);

                    if (destiantionUserId.HasValue)
                        command.Parameters.AddWithValue("@DestinationUserID", destiantionUserId);
                    else
                        command.Parameters.AddWithValue("@DestinationUserID", DBNull.Value);

                    if (replyToCommentId.HasValue)
                        command.Parameters.AddWithValue("@ReplyToID", replyToCommentId);
                    else
                        command.Parameters.AddWithValue("@ReplyToID", DBNull.Value);

                    command.Parameters.AddWithValue("@Comment", commentText);
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@IsOfficialAnswer", isOfficialAnswer);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            result.ID = (Guid)reader["ID"];
                            result.ContentID = (Guid)reader["ContentID"];
                            result.UserID = (Guid)reader["UserID"];
                            result.FormattedDate = ((DateTime)reader["CreatedAt"]).ToString("d MMMM в HH:mm");
                            result.UserFullName = reader["UserFullName"] != DBNull.Value ? (string)reader["UserFullName"] : null;
                            result.DestinationUserFullName = reader["DestinationUserFullName"] != DBNull.Value ? (string)reader["DestinationUserFullName"] : null;
                            result.Comment = reader["Comment"] != DBNull.Value
                                            ? ((string)reader["Comment"]).ToHtml()
                                            : null;
                            result.IsOfficialAnswer = reader["IsOfficialAnswer"] != DBNull.Value && (bool) reader["IsOfficialAnswer"];
                            result.IsInternal = reader["IsInternal"] != DBNull.Value && (bool) reader["IsInternal"];
                            result.FileName = reader["FileName"] != DBNull.Value ? (string)reader["FileName"] : null;
                            result.SumLike = 0;
                            result.ContactLike = false;
                        }
                    }
                }
            }

            var fsp = new FileSystemProvider();
            if (!string.IsNullOrEmpty(result.FileName))
                result.VirtualFileName = fsp.GetLink(siteId, EnumHelper.GetEnumDescription(commentTable), result.FileName, FileType.Attachment); 

            if (sendNotification)
            {
                var dataManager = new DataManager();
                switch (commentTable)
                {
                    case CommentTables.tbl_RequirementComment:
                        RequirementCommentNotificationService.Notify(dataManager.RequirementComment.SelectById(siteId, result.ID));
                        break;
                    case CommentTables.tbl_RequestComment:
                        RequestCommentNotificationService.Notify(dataManager.RequestComment.SelectById(siteId, result.ID));
                        break;
                    case CommentTables.tbl_InvoiceComment:
                        InvoiceCommentNotificationService.Notify(dataManager.InvoiceComment.SelectById(siteId, result.ID));
                        break;
                }
            }

            return result;
        }


        /// <summary>
        /// Updates the official answers.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentTable">The comment table.</param>
        public static void UpdateOfficialAnswers(Guid siteId, Guid contentId, CommentTables commentTable)
        {
            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();
                var query = string.Format("UPDATE {0} " +
                                          "SET IsOfficialAnswer = 0 " +
                                          "  WHERE ContentID='{1}' AND SiteID = '{2}'", commentTable.ToString(),
                                          contentId, siteId);

                var command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <param name="isPortal">if set to <c>true</c> [is portal].</param>
        /// <returns></returns>
        public static ContentComment SelectById(Guid siteId, Guid commentId, CommentTables commentTable, bool isPortal = false)
        {
            var result = new ContentComment();

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Format("SELECT c.ID, c.SiteID, c.ContentID, c.UserID, c.CreatedAt, contact.UserFullName, dcontact.UserFullName as DestinationUserFullName, c.Comment, c.IsOfficialAnswer, c.IsInternal, c.FileName " +
                                          "FROM {0} as c " +
                                          "LEFT JOIN tbl_User as u ON c.UserID = u.ID " +
                                          "LEFT JOIN tbl_Contact as contact ON u.ContactID = contact.ID " +
                                          "LEFT JOIN tbl_User as du ON c.DestinationUserID = du.ID " +
                                          "LEFT JOIN tbl_Contact as dcontact ON du.ContactID = dcontact.ID " +
                                          "WHERE c.ID = @ID AND c.SiteID = @SiteID AND (@IsInternal IS NULL OR IsInternal = @IsInternal)", commentTable.ToString());

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", commentId);
                    command.Parameters.AddWithValue("@SiteID", siteId);

                    if (isPortal)
                        command.Parameters.AddWithValue("@IsInternal", false);
                    else
                        command.Parameters.AddWithValue("@IsInternal", DBNull.Value);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            result.ID = (Guid)reader["ID"];
                            result.SiteID = (Guid)reader["SiteID"];
                            result.ContentID = (Guid)reader["ContentID"];
                            result.UserID = (Guid)reader["UserID"];
                            result.CreatedAt = (DateTime)reader["CreatedAt"];
                            result.FormattedDate = ((DateTime)reader["CreatedAt"]).ToString("d MMMM в HH:mm");
                            result.UserFullName = reader["UserFullName"] != DBNull.Value ? (string)reader["UserFullName"] : null;
                            result.DestinationUserFullName = reader["DestinationUserFullName"] != DBNull.Value ? (string)reader["DestinationUserFullName"] : null;
                            result.Comment = reader["Comment"] != DBNull.Value
                                            ? (string)reader["Comment"]
                                            : null;
                            result.IsOfficialAnswer = reader["IsOfficialAnswer"] != DBNull.Value && (bool)reader["IsOfficialAnswer"];
                            result.IsInternal = reader["IsInternal"] != DBNull.Value && (bool) reader["IsInternal"];
                            result.FileName = reader["FileName"] != DBNull.Value ? (string)reader["FileName"] : null;
                        }
                    }
                }
            }

            var fsp = new FileSystemProvider();
            if (!string.IsNullOrEmpty(result.FileName))
                result.VirtualFileName = fsp.GetLink(siteId, EnumHelper.GetEnumDescription(commentTable), result.FileName, FileType.Attachment); 

            return result;
        }



        /// <summary>
        /// Selects the official comment.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <returns></returns>
        public static string SelectOfficialComment(Guid siteId, Guid contentId, CommentTables commentTable)
        {
            var result = string.Empty;

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Format("SELECT c.Comment " +
                                          "FROM {0} as c " +                                          
                                          "WHERE c.ContentID = @ContentID AND c.SiteID = @SiteID AND c.IsOfficialAnswer = 1", commentTable.ToString());

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContentID", contentId);
                    command.Parameters.AddWithValue("@SiteID", siteId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader["Comment"] != DBNull.Value ? (string) reader["Comment"] : null;
                        }
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Updates the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contentComment">The content comment.</param>
        /// <param name="commentTable">The comment table.</param>
        public static void Update(Guid siteId, ContentComment contentComment, CommentTables commentTable)
        {            
            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();
                
                var query = string.Format("UPDATE {0} " +
                                          "SET Comment = @Comment, IsOfficialAnswer = @IsOfficialAnswer" +
                                          " WHERE ID = @ID AND SiteID = @SiteID", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", contentComment.ID);
                    command.Parameters.AddWithValue("@SiteID", siteId);
                    command.Parameters.AddWithValue("@Comment", contentComment.Comment);
                    command.Parameters.AddWithValue("@IsOfficialAnswer", contentComment.IsOfficialAnswer);

                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Deletes the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentTable">The comment table.</param>
        public static void Delete(Guid siteId, Guid commentId, CommentTables commentTable)
        {
            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Format("DELETE FROM {0} WHERE ID = @ID AND SiteID = @SiteID", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", commentId);
                    command.Parameters.AddWithValue("@SiteID", siteId);

                    command.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="user">The user map.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <param name="commentTable">The comment table.</param>
        public static int LikeComment(UserMap user, Guid contentId, Guid commentId, CommentTables commentTable)
        {
            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Format("IF (SELECT COUNT(*) FROM {0} WHERE ContentID = @ContentID AND SiteID = @SiteID) > 0 " +
                                          "BEGIN " +
                                            "DECLARE @CommentMarkID uniqueidentifier " +
                                            "SELECT @CommentMarkID = ID FROM {0}Mark WHERE ContentCommentID = @CommentID AND UserID = @UserID " +
                                            "IF @CommentMarkID IS NULL " +
                                            "BEGIN " +
                                                "SET @CommentMarkID = newid() " +
                                                "INSERT INTO {0}Mark ([ID], [ContentCommentID], [UserID], [TypeID], [Rank]) " +
                                                "VALUES (@CommentMarkID, @CommentID, @UserID, 0, 1) " +
                                            "END " +
                                            "ELSE " +
                                                "UPDATE {0}Mark " +
                                                "SET Rank = (CASE WHEN Rank = 1 THEN 0 ELSE 1 END) " +
                                                "WHERE ContentCommentID = @CommentID AND UserID = @UserID " +
                                                "SELECT Rank FROM {0}Mark WHERE ID = @CommentMarkID " +
                                          "END " +
                                          "ELSE " +
                                          "SELECT 1", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContentID", contentId);
                    command.Parameters.AddWithValue("@CommentID", commentId);
                    command.Parameters.AddWithValue("@SiteID", user.SiteID);
                    command.Parameters.AddWithValue("@UserID", user.ID);

                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }
        }



        /// <summary>
        /// Selects the comment authors.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contentId">The content id.</param>
        /// <param name="commentTable">The comment table.</param>
        /// <returns></returns>
        public static IEnumerable<ContentCommentAuthor> SelectContentCommentAuthors(Guid siteId, Guid contentId, CommentTables commentTable)
        {
            var result = new List<ContentCommentAuthor>();

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();

                var query = string.Format("SELECT DISTINCT u.ID, contact.UserFullName " +
                                          "FROM {0} as c " +
                                          "LEFT JOIN tbl_User as u ON c.UserID = u.ID " +
                                          "LEFT JOIN tbl_Contact as contact ON u.ContactID = contact.ID " +
                                          "WHERE c.ContentID = @ID AND c.SiteID = @SiteID", commentTable.ToString());
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", contentId);
                    command.Parameters.AddWithValue("@SiteID", siteId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new ContentCommentAuthor()
                                               {
                                                   UserID = (Guid) reader["ID"],
                                                   UserFullName =
                                                       reader["UserFullName"] != DBNull.Value
                                                           ? (string) reader["UserFullName"]
                                                           : null
                                               });
                            }

                            reader.NextResult();
                        }
                    }
                }
            }                       

            return result;
        }
    }
}