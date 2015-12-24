using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationMarkRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedPublicationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationMarkRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_PublicationMark> SelectAll()
        {
            return _dataContext.tbl_PublicationMark;
        }


        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        /// <returns></returns>
        public IQueryable<tbl_PublicationMark> SelectByPublicationId(Guid publicationID)
        {
            return _dataContext.tbl_PublicationMark.Where(c => c.PublicationID == publicationID && c.PublicationCommentID == null);
        }



        /// <summary>
        /// Selects the by publication and comment id.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        public IQueryable<tbl_PublicationMark> SelectByPublicationAndCommentId(Guid publicationId, Guid commentId)
        {
            return _dataContext.tbl_PublicationMark.Where(c => c.PublicationID == publicationId && c.PublicationCommentID == commentId);
        }



        /// <summary>
        /// Selects the by publication and user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        public tbl_PublicationMark SelectByPublicationAndUserId(Guid userId, Guid publicationId)
        {
            return _dataContext.tbl_PublicationMark.Where(pm => pm.UserID == userId && pm.PublicationID == publicationId && pm.PublicationCommentID == null).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by publication and publication comment and user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="publicationCommentId">The publication comment id.</param>
        /// <returns></returns>
        public tbl_PublicationMark SelectByPublicationAndPublicationCommentAndUserId(Guid userId, Guid publicationId, Guid publicationCommentId)
        {
            return
                _dataContext.tbl_PublicationMark.Where(
                    pm => pm.UserID == userId && pm.PublicationID == publicationId && pm.PublicationCommentID == publicationCommentId).SingleOrDefault();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="publicationID">The publication ID.</param>
        public void DeleteAll(Guid publicationID)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_PublicationMark WHERE PublicationID = @PublicationID",
                                             new SqlParameter { ParameterName = "PublicationID", Value = publicationID });
        }



        /// <summary>
        /// Adds the specified source publication mark.
        /// </summary>
        /// <param name="publicationMarks">The publication marks.</param>
        public void Add(List<tbl_PublicationMark> publicationMarks)
        {
            foreach (var publicationMark in publicationMarks)
                _dataContext.tbl_PublicationMark.AddObject(new tbl_PublicationMark()
                {
                    ID = publicationMark.ID,
                    PublicationID = publicationMark.PublicationID,
                    UserID = publicationMark.UserID,
                    TypeID = publicationMark.TypeID,
                    Rank = publicationMark.Rank,
                    CreatedAt = publicationMark.CreatedAt,
                    PublicationCommentID = publicationMark.PublicationCommentID
                });

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Adds the specified publication mark.
        /// </summary>
        /// <param name="publicationMark">The publication mark.</param>
        public void Add(tbl_PublicationMark publicationMark)
        {
            _dataContext.tbl_PublicationMark.AddObject(publicationMark);
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Updates the specified publication mark.
        /// </summary>
        /// <param name="publicationMark">The publication mark.</param>
        public void Update(tbl_PublicationMark publicationMark)
        {
            _dataContext.SaveChanges();
        }
    }
}