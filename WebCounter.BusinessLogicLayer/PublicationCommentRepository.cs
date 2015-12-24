using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedPublicationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_PublicationComment> SelectAll()
        {
            return _dataContext.tbl_PublicationComment;
        }



        /// <summary>
        /// Selects the site Publication Comment by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_PublicationComment SelectById(Guid id)
        {
            return _dataContext.tbl_PublicationComment.SingleOrDefault(a => a.ID == id);
        }



        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_PublicationComment> SelectByPublicationId(Guid publicationID)
        {
            return _dataContext.tbl_PublicationComment.Where(c => c.PublicationID == publicationID);
        }



        /// <summary>
        /// Selects the by publication id for ribbon.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public List<ActivityCommentMap> SelectByPublicationIdForRibbon(Guid publicationId, Guid? contactId)
        {
            var dataManager = new DataManager();

            var result = SelectByPublicationId(publicationId).Select(
                    pc => new ActivityCommentMap
                    {
                        ID = pc.ID,
                        SiteID = pc.tbl_Publication.SiteID,
                        PublicationID = pc.PublicationID,
                        CreatedAt = pc.CreatedAt,
                        UserFullName = pc.tbl_Contact.UserFullName,
                        Comment = pc.Comment,
                        isOfficialAnswer = pc.isOfficialAnswer,
                        FileName = pc.FileName
                    }).OrderByDescending(pc => pc.CreatedAt).ToList();

            var fsp = new FileSystemProvider();

            foreach (var activityCommentMap in result)
            {
                var publicationMarks = dataManager.PublicationMark.SelectByPublicationAndCommentId(activityCommentMap.PublicationID, activityCommentMap.ID);

                if (publicationMarks.Any())
                {
                    if (publicationMarks.Count(pm => pm.Rank == 1) > 0)
                        activityCommentMap.SumLike = publicationMarks.Where(pm => pm.Rank == 1).Sum(pm => pm.Rank);

                    if (contactId.HasValue)
                        activityCommentMap.ContactLike = publicationMarks.Where(pm => pm.UserID == contactId).Select(pm => pm.Rank).SingleOrDefault();
                }
                activityCommentMap.FormattedDate = ((DateTime)activityCommentMap.CreatedAt).ToString("d MMMM в HH:mm");
                activityCommentMap.Comment = activityCommentMap.Comment.ToHtml();

                if (!string.IsNullOrEmpty(activityCommentMap.FileName))
                    activityCommentMap.VirtualFileName = fsp.GetLink(activityCommentMap.SiteID, "PublicationComments", activityCommentMap.FileName, FileType.Attachment);
            }

            return result;
        }


        /// <summary>
        /// Updates the specified publication comment.
        /// </summary>
        /// <param name="publicationComment">The publication comment.</param>
        public void Update(tbl_PublicationComment publicationComment)
        {
            var updatesitePublicationComment = SelectById(publicationComment.ID);
            updatesitePublicationComment.PublicationID = publicationComment.PublicationID;
            updatesitePublicationComment.UserID = publicationComment.UserID;
            updatesitePublicationComment.isOfficialAnswer = publicationComment.isOfficialAnswer;
            updatesitePublicationComment.Comment = publicationComment.Comment;
            updatesitePublicationComment.CreatedAt = publicationComment.CreatedAt;
            updatesitePublicationComment.FileName = publicationComment.FileName;
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified Publication Comment.
        /// </summary>
        /// <param name="publicationComment">The publication comment.</param>
        public void Delete(tbl_PublicationComment publicationComment)
        {
            var publicationCommentMarks = _dataContext.tbl_PublicationMark.Where(pm => pm.PublicationCommentID == publicationComment.ID);

            foreach (var publicationCommentMark in publicationCommentMarks)
            {
                _dataContext.DeleteObject(publicationCommentMark);
            }

            _dataContext.DeleteObject(publicationComment);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Add the specified Publication Comment.
        /// </summary>
        /// <param name="publicationComment">The publication comment.</param>
        /// <returns></returns>
        public tbl_PublicationComment Add(tbl_PublicationComment publicationComment)
        {
            _dataContext.tbl_PublicationComment.AddObject(publicationComment);
            _dataContext.SaveChanges();
            return publicationComment;
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public ActivityCommentMap Add(Guid siteId, Guid contactId, Guid publicationId, string comment, string fileName)
        {
            var publicationComment = new tbl_PublicationComment
            {
                ID = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                PublicationID = publicationId,
                UserID = contactId,
                Comment = comment,
                isOfficialAnswer = false,
                FileName = fileName
            };

            Add(publicationComment);

            var publicationCommentMap = new ActivityCommentMap
            {
                ID = publicationComment.ID,
                PublicationID = publicationComment.PublicationID,
                CreatedAt = publicationComment.CreatedAt,
                UserFullName = publicationComment.tbl_Contact.UserFullName,
                Comment = publicationComment.Comment.ToHtml(),
                isOfficialAnswer = publicationComment.isOfficialAnswer,
                FormattedDate = ((DateTime)publicationComment.CreatedAt).ToString("d MMMM в HH:mm"),
                FileName = publicationComment.FileName
            };

            var fsp = new FileSystemProvider();
            if (!string.IsNullOrEmpty(publicationCommentMap.FileName))
                publicationCommentMap.VirtualFileName = fsp.GetLink(siteId, "PublicationComments", publicationCommentMap.FileName, FileType.Attachment);

            return publicationCommentMap;
        }
    }
}