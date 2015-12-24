using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WebCounter.BusinessLogicLayer.DocumentManagement;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Enumerations.Request;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Services;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class RequestRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RequestRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestId">The request id.</param>
        /// <returns></returns>
        public tbl_Request SelectById(Guid siteId, Guid requestId)
        {
            return _dataContext.tbl_Request.SingleOrDefault(o => o.SiteID == siteId && o.ID == requestId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Request> SelectAll(Guid siteId)
        {
            return _dataContext.tbl_Request.Where(o => o.SiteID == siteId);
        }



        /// <summary>
        /// Adds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public tbl_Request Add(tbl_Request request)
        {
            request.ID = Guid.NewGuid();
            _dataContext.tbl_Request.AddObject(request);
            _dataContext.SaveChanges();

            RequestNotificationService.Register(request);

            AddHistory(new DataManager(), request);

            return request;
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestSourceId">The request source id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        /// <param name="longDescription">The long description.</param>
        /// <param name="authorId">The author id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        public tbl_Request Add(Guid siteId, Guid requestSourceId, Guid? requestSourceTypeId, string longDescription, Guid authorId, Guid? ownerId)
        {
            return Add(siteId, requestSourceId, requestSourceTypeId, string.Empty, longDescription, authorId, ownerId);
        }



        /// <summary>
        /// Adds the specified site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="requestSourceId">The request source id.</param>
        /// <param name="requestSourceTypeId">The request source type id.</param>
        /// <param name="shortDescription">The short description.</param>
        /// <param name="longDescription">The long description.</param>
        /// <param name="authorId">The author id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        public tbl_Request Add(Guid siteId, Guid requestSourceId, Guid? requestSourceTypeId, string shortDescription, string longDescription, Guid authorId, Guid? ownerId)
        {
            if (!requestSourceTypeId.HasValue)
                return null;

            var dataManager = new DataManager();

            var contact = dataManager.Contact.SelectById(siteId, authorId);
             
            var serviceLevel = dataManager.ServiceLevel.SelectForContact(siteId, contact);

            if (serviceLevel == null)
                return null;

            var request = new tbl_Request
            {
                SiteID = siteId,
                CreatedAt = DateTime.Now,
                ServiceLevelID = serviceLevel.ID,
                ContactID = contact.ID,
                CompanyID = contact.CompanyID,
                RequestSourceTypeID = requestSourceTypeId,
                RequestSourceID = requestSourceId,
                RequestStatusID = (int)RequestStatus.New,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                ReactionDateActual = null,
                OwnerID = ownerId
            };

            request.ReactionDatePlanned = request.CreatedAt.AddHours(serviceLevel.ReactionTime);

            var requestSourceType = dataManager.RequestSourceType.SelectById(siteId, (Guid)requestSourceTypeId);
            if (requestSourceType != null)
            {

                var documentNumerator = DocumentNumerator.GetNumber((Guid)requestSourceType.NumeratorID,
                                                                    request.CreatedAt,
                                                                    requestSourceType.tbl_Numerator.Mask, "tbl_Request");
                request.Number = documentNumerator.Number;
                request.SerialNumber = documentNumerator.SerialNumber;
            }

            return Add(request);
        }


        /// <summary>
        /// Updates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>        
        public void Update(tbl_Request request)
        {
            var dataManager = new DataManager();
            var requestInDataBase = dataManager.Request.SelectById(request.SiteID, request.ID);

            _dataContext.SaveChanges();            

            if (requestInDataBase.RequestStatusID == (int)RequestStatus.New && requestInDataBase.RequestStatusID != request.RequestStatusID)
                RequestNotificationService.Process(request);

            if (requestInDataBase.RequestStatusID != request.RequestStatusID || requestInDataBase.ResponsibleID != request.ResponsibleID)
            {
                AddHistory(dataManager, request);
            }
        }



        /// <summary>
        /// Adds the history.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="request">The request.</param>
        protected void AddHistory(DataManager dataManager, tbl_Request request)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null || CurrentUser.Instance == null)
                return;

            var requestHistory = new tbl_RequestHistory
                                     {
                                         RequestID = request.ID,
                                         RequestStatusID = request.RequestStatusID,
                                         ContactID = (Guid) CurrentUser.Instance.ContactID,
                                         ResponsibleID = request.ResponsibleID
                                     };

            dataManager.RequestHistory.Add(requestHistory);
        }
    }
}