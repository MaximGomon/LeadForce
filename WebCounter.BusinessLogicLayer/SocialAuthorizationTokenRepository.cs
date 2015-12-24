using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class SocialAuthorizationTokenRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public SocialAuthorizationTokenRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_SocialAuthorizationToken SelectById(Guid id)
        {
            return _dataContext.tbl_SocialAuthorizationToken.SingleOrDefault(s => s.ID == id);
        }



        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="socialAuthorizationToken">The user.</param>
        /// <returns></returns>
        public tbl_SocialAuthorizationToken Add(tbl_SocialAuthorizationToken socialAuthorizationToken)
        {
            if (socialAuthorizationToken.ID == Guid.Empty)
                socialAuthorizationToken.ID = Guid.NewGuid();
            _dataContext.tbl_SocialAuthorizationToken.AddObject(socialAuthorizationToken);
            _dataContext.SaveChanges();

            return socialAuthorizationToken;
        }



        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(Guid id)
        {
            var socialAuthToken = SelectById(id);
            _dataContext.DeleteObject(socialAuthToken);
            _dataContext.SaveChanges();
        }
    }
}