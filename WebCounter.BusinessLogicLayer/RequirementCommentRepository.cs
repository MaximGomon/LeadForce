using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequirementCommentRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementCommentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequirementCommentRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requirementCommentId">The requirement comment id.</param>
        /// <returns></returns>
        public tbl_RequirementComment SelectById(Guid siteId, Guid requirementCommentId)
        {
            return
                _dataContext.tbl_RequirementComment.SingleOrDefault(
                    o => o.SiteID == siteId && o.ID == requirementCommentId);
        }



        /// <summary>
        /// Selects the comment for report.
        /// </summary>
        /// <param name="requirementId">The requirement id.</param>
        /// <returns></returns>
        public string SelectCommentForReport(Guid requirementId)
        {
            var requirement = _dataContext.tbl_Requirement.SingleOrDefault(o => o.ID == requirementId);

            var comment = requirement.tbl_RequirementComment.FirstOrDefault(rc => rc.IsOfficialAnswer == true) ?? new tbl_RequirementComment();
            var comments = comment.Comment;
            var lastComment = requirement.tbl_RequirementComment.OrderByDescending(rc => rc.CreatedAt).FirstOrDefault();
            if (lastComment != null && comments != lastComment.Comment)            
                comments += "<br/>" + lastComment.Comment;            

            return comments;
        } 
    }
}