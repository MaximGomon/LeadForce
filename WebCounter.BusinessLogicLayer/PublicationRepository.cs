using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using WebCounter.BusinessLogicLayer.Common;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.BusinessLogicLayer.Files;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class PublicationRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PublicationRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_Publication> SelectAll()
        {
            return _dataContext.tbl_Publication;
        }



        /// <summary>
        /// Selects the by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Publication> SelectBySiteId(Guid siteId)
        {
            return _dataContext.tbl_Publication.Where(c => c.SiteID == siteId);
        }



        /// <summary>
        /// Selects the knowledge base.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public IQueryable<ActivityPublicationMap> SelectKnowledgeBase(Guid siteId, Guid? contactId)
        {
            var query = _dataContext.tbl_Publication.Where(
                            c => c.SiteID == siteId && c.tbl_PublicationType.PublicationKindID == (int) PublicationKind.KnowledgeBase &&
                                 c.tbl_PublicationStatus.isActive != null && (bool) c.tbl_PublicationStatus.isActive)
                                 .Select(p => new ActivityPublicationMap()
                                 {
                                     ID = p.ID,
                                     Date = p.Date,
                                     Title = p.Title,
                                     Noun = p.Noun,
                                     Text = p.Text,
                                     AuthorID = p.AuthorID,
                                     FileName = p.FileName,                                     
                                     Img = p.Img,
                                     PublicationTypeLogo = p.tbl_PublicationType.Logo,
                                     PublicationCategoryID = p.PublicationCategoryID,
                                     Category = p.tbl_PublicationCategory.Title,
                                     Status = p.tbl_PublicationStatus.Title,
                                     SumLike = (int?)p.tbl_PublicationMark.Where(pm => pm.Rank == 1 && pm.PublicationCommentID == null).Sum(pm => pm.Rank) ?? 0,
                                     ContactLike = p.tbl_PublicationMark.Where(pm => contactId.HasValue && pm.UserID == contactId).Select(pm => pm.Rank).FirstOrDefault(),
                                     OfficialComment = p.tbl_PublicationComment.Where(pc => pc.isOfficialAnswer).Select(pc => pc.Comment).FirstOrDefault(),
                                     CommentsCount = p.tbl_PublicationComment.Count,
                                     AccessRecord = p.AccessRecord,
                                     AccessComment = p.AccessComment,
                                     AccessCompanyID = p.AccessCompanyID
                                 });            

            if (contactId.HasValue)
            {
                var dataManager = new DataManager();                
                var contactCompanyId = dataManager.Contact.SelectById(siteId, (Guid)contactId).CompanyID;
                query = query.Where(p => p.AccessRecord == (int)PublicationAccessRecord.Public || p.AccessRecord == (int)PublicationAccessRecord.Anonymous ||
                                                                            (p.AccessRecord == (int)PublicationAccessRecord.Personal && p.AuthorID == contactId) ||
                                                                            (p.AccessRecord == (int)PublicationAccessRecord.Company && p.AccessCompanyID == contactCompanyId));
            }                         
            else
                query = query.Where(p => p.AccessRecord == (int)PublicationAccessRecord.Anonymous);

            return query;
        }



        /// <summary>
        /// Selects the publication map by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public ActivityPublicationMap SelectPublicationMapById(Guid siteId, Guid publicationId, Guid? contactId)
        {
            var publicationMap = _dataContext.tbl_Publication.Where(p => p.SiteID == siteId && p.ID == publicationId).Select(
                    p =>
                    new ActivityPublicationMap()
                    {
                        ID = p.ID,
                        Date = p.Date,
                        Title = p.Title,
                        Text = p.Text,
                        FileName = p.FileName,
                        PublicationTypeLogo = p.tbl_PublicationType.Logo,
                        Category = p.tbl_PublicationCategory.Title,
                        Status = p.tbl_PublicationStatus.Title,
                        SumLike = (int?)p.tbl_PublicationMark.Where(pm => pm.Rank == 1 && pm.PublicationCommentID == null).Sum(pm => pm.Rank) ?? 0,
                        ContactLike = p.tbl_PublicationMark.Where(pm => contactId.HasValue && pm.UserID == contactId).Select(pm => pm.Rank).FirstOrDefault(),
                        OfficialComment = p.tbl_PublicationComment.Where(pc => pc.isOfficialAnswer).Select(pc => pc.Comment).FirstOrDefault(),
                        CommentsCount = p.tbl_PublicationComment.Count
                    }).SingleOrDefault();

            if (publicationMap != null)
                publicationMap.ContactLikeUserText = GetLikeCountWithCase(publicationMap.SumLike ?? 0);

            return publicationMap;
        }



        /// <summary>
        /// Selects the activity ribbon.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="publicationTypeId">The publication type id.</param>
        /// <param name="publicationCategoryId">The publication category id.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <returns></returns>
        public List<ActivityPublicationMap> SelectActivityRibbon(Guid siteId, Guid? contactId, Guid publicationTypeId, Guid publicationCategoryId, string filter, int pageIndex, Guid? portalSettingsId = null)
        {
            var dataManager = new DataManager();
            var publications = _dataContext.tbl_Publication.Where(c => c.SiteID == siteId && c.tbl_PublicationType.PublicationKindID == (int)PublicationKind.Discussion);

            if (portalSettingsId.HasValue)
                publications = publications.Where(p => p.tbl_PublicationStatus.isActive == true);

            if (publicationTypeId != Guid.Empty)
                publications = publications.Where(p => p.tbl_PublicationType.ID == publicationTypeId);

            if (publicationCategoryId != Guid.Empty)
            {                
                var categoryIds = dataManager.PublicationCategory.SelectChilds(siteId, publicationCategoryId).Select(pc => pc.ID).ToList();
                publications = publications.Where(p => categoryIds.Contains(p.PublicationCategoryID) || p.PublicationCategoryID == publicationCategoryId);
            }            

            var publicationsMap = new List<ActivityPublicationMap>();
            const int pageSize = 15;
            var publicationsMapQuerable = publications.Select(
                    p =>
                    new ActivityPublicationMap()
                        {
                            ID = p.ID,
                            AuthorID = p.AuthorID,                            
                            Date = p.Date,
                            Title = p.Title,
                            Text = p.Text,
                            FileName = p.FileName,
                            PublicationTypeLogo = p.tbl_PublicationType.Logo,
                            Category = p.tbl_PublicationCategory.Title,
                            Status = p.tbl_PublicationStatus.Title,
                            SumLike = (int?)p.tbl_PublicationMark.Where(pm => pm.Rank == 1 && pm.PublicationCommentID == null).Sum(pm => pm.Rank) ?? 0,
                            ContactLike = p.tbl_PublicationMark.Where(pm => contactId.HasValue && pm.UserID == contactId).Select(pm => pm.Rank).FirstOrDefault(),
                            OfficialComment = p.tbl_PublicationComment.Where(pc => pc.isOfficialAnswer).Select(pc => pc.Comment).FirstOrDefault(),
                            CommentsCount = p.tbl_PublicationComment.Count,
                            AccessRecord = p.AccessRecord,
                            AccessComment = p.AccessComment,
                            AccessCompanyID = p.AccessCompanyID
                        });

            if (portalSettingsId.HasValue)
            {                
                if (!contactId.HasValue || contactId == Guid.Empty)
                {
                    publicationsMapQuerable = publicationsMapQuerable.Where(p => p.AccessRecord == (int)PublicationAccessRecord.Anonymous);
                }
                else
                {
                    var contact = dataManager.Contact.SelectById(siteId, (Guid) contactId);
                    if (contact != null)
                    {
                        var contactCompanyId = contact.CompanyID;
                        publicationsMapQuerable = publicationsMapQuerable.Where(p => p.AccessRecord == (int)PublicationAccessRecord.Public || p.AccessRecord == (int)PublicationAccessRecord.Anonymous ||
                                                                                (p.AccessRecord == (int)PublicationAccessRecord.Personal && p.AuthorID == contactId) ||
                                                                                (p.AccessRecord == (int)PublicationAccessRecord.Company && p.AccessCompanyID == contactCompanyId));
                    }
                    else                    
                        publicationsMapQuerable = publicationsMapQuerable.Where(p => p.AccessRecord == (int)PublicationAccessRecord.Anonymous);                    
                }
            }

            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter.ToLower())
                {
                    case "top":
                        publicationsMap = publicationsMapQuerable.OrderByDescending(p => p.SumLike).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                        break;
                    case "new":
                        publicationsMap = publicationsMapQuerable.OrderByDescending(p => p.Date).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                        break;
                    case "mypublication":
                        publicationsMap = publicationsMapQuerable.Where(p => p.AuthorID == contactId).OrderByDescending(p => p.Date).Skip(pageIndex * pageSize).Take(pageSize).ToList();
                        break;
                }
            }
            else
                publicationsMap = publicationsMapQuerable.OrderByDescending(p => p.Date).Skip(pageIndex * pageSize).Take(pageSize).ToList();

            var fsp = new FileSystemProvider();

            foreach (var publicationMap in publicationsMap)
            {                                                
                publicationMap.ContactLikeUserText = GetLikeCountWithCase(publicationMap.SumLike ?? 0);
                publicationMap.OfficialComment = publicationMap.OfficialComment.ToHtml();

                publicationMap.PublicationTypeLogo = Settings.DictionaryLogoPath(siteId, "tbl_PublicationType") + publicationMap.PublicationTypeLogo;

                if (publicationMap.Date.HasValue)
                    publicationMap.FormattedDate = ((DateTime)publicationMap.Date).ToString("d MMMM в HH:mm");

                if (!string.IsNullOrEmpty(publicationMap.FileName))
                    publicationMap.FileName = fsp.GetLink(siteId, "Publications", publicationMap.FileName, FileType.Attachment);

                if (portalSettingsId.HasValue)
                    publicationMap.PublicationUrl = UrlsData.LFP_Discussion((Guid)portalSettingsId, publicationMap.ID);
                else
                    publicationMap.PublicationUrl = UrlsData.AP_PublicationEdit(publicationMap.ID, "Materials");
            }            

            return publicationsMap;
        }



        /// <summary>
        /// Searches the publication.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="publicationKind">Kind of the publication.</param>
        /// <param name="text">The text.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="portalSettingsId">The portal settings id.</param>
        /// <param name="publicationType">Type of the publication.</param>
        /// <param name="initUrls">if set to <c>true</c> [init urls].</param>
        /// <param name="isPortal">if set to <c>true</c> [is portal].</param>
        /// <returns></returns>
        public List<ActivityPublicationMap> SearchPublication(Guid siteId, Guid? contactId, int? publicationKind, string text, int pageIndex, out int totalCount, Guid? portalSettingsId = null, Guid? publicationType = null, bool initUrls = true, bool isPortal = false)
        {
            var publicationMaps = new List<ActivityPublicationMap>();
            totalCount = 0;

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SearchPublication", connection) {CommandType = CommandType.StoredProcedure};
                command.Parameters.AddWithValue("@SiteID", siteId);
                command.Parameters.AddWithValue("@ContactID", contactId);
                command.Parameters.AddWithValue("@Word", string.IsNullOrEmpty(text) ? "*" : text);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", 5);
                command.Parameters.AddWithValue("@IsPortal", portalSettingsId.HasValue || isPortal);
                if (publicationKind != -1)
                    command.Parameters.AddWithValue("@PublicationKindID", publicationKind);
                command.Parameters.AddWithValue("@PublicationTypeID", publicationType);

                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var publicationMap = new ActivityPublicationMap
                                                 {
                                                     ID = (Guid) reader["ID"],
                                                     Date = (DateTime) reader["Date"],
                                                     Title = (string) reader["Title"],
                                                     Text = reader["Text"] != DBNull.Value ? (string) reader["Text"] : null,
                                                     Noun = reader["Noun"] != DBNull.Value ? (string)reader["Noun"] : null,
                                                     PublicationKindID = reader["PublicationKindID"] != DBNull.Value ? (int?)reader["PublicationKindID"] : null
                                                 };

                        totalCount = (int) reader["TotalCount"];
                        publicationMaps.Add(publicationMap);
                    }
                    reader.NextResult();
                }
            }

            var dataManager = new DataManager();
            PortalSettingsMap portalSettings = null;
            if (!portalSettingsId.HasValue)            
                portalSettings = dataManager.PortalSettings.SelectMapBySiteId(siteId, true);                            

            foreach (var publicationMap in publicationMaps)
            {
                if (string.IsNullOrEmpty(publicationMap.Noun))
                {
                    publicationMap.Noun = publicationMap.Text;
                    publicationMap.Text = string.Empty;
                }

                publicationMap.Noun = publicationMap.Noun.Truncate(300, true, true);

                if (initUrls)
                {
                    if (portalSettingsId.HasValue)
                    {
                        if (publicationMap.PublicationKindID == (int) PublicationKind.Discussion)
                            publicationMap.PublicationUrl = UrlsData.LFP_Discussion((Guid) portalSettingsId,
                                                                                    publicationMap.ID);
                        else
                            publicationMap.PublicationUrl = UrlsData.LFP_Article((Guid) portalSettingsId,
                                                                                 publicationMap.ID);
                    }
                    else
                        publicationMap.PublicationUrl = UrlsData.AP_PublicationEdit(publicationMap.ID, "Materials");

                    if (portalSettings != null)
                        publicationMap.PortalUrl = string.Concat(Settings.LabitecLeadForcePortalUrl,
                                                                 string.Format("/{0}/KnowledgeBase/Article/{1}",
                                                                               portalSettings.ID, publicationMap.ID));
                }
            }            

            return publicationMaps;
        }



        /// <summary>
        /// Searches the publication for feedback.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="text">The text.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="publicationType">Type of the publication.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public List<ActivityPublicationMap> SearchPublicationForFeedback(Guid siteId, Guid? contactId, string text, int pageIndex, Guid publicationType, out int totalCount)
        {
            var publicationMaps = new List<ActivityPublicationMap>();
            totalCount = 0;

            using (var connection = new SqlConnection(Settings.ADONetConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("SearchPublication", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@SiteID", siteId);
                command.Parameters.AddWithValue("@ContactID", contactId);
                command.Parameters.AddWithValue("@Word", string.IsNullOrEmpty(text) ? "*" : text);
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", 5);
                command.Parameters.AddWithValue("@IsPortal", true);                
                command.Parameters.AddWithValue("@PublicationTypeID", publicationType);

                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var publicationMap = new ActivityPublicationMap
                        {
                            ID = (Guid)reader["ID"],
                            Date = (DateTime)reader["Date"],
                            Title = (string)reader["Title"],
                            Text = reader["Text"] != DBNull.Value ? (string)reader["Text"] : null,
                            Noun = reader["Noun"] != DBNull.Value ? (string)reader["Noun"] : null,
                            PublicationKindID = reader["PublicationKindID"] != DBNull.Value ? (int?)reader["PublicationKindID"] : null
                        };

                        totalCount = (int)reader["TotalCount"];
                        publicationMaps.Add(publicationMap);
                    }
                    reader.NextResult();
                }
            }
        
            foreach (var publicationMap in publicationMaps)
            {
                if (string.IsNullOrEmpty(publicationMap.Noun))
                {
                    publicationMap.Noun = publicationMap.Text;
                    publicationMap.Text = string.Empty;
                }
                publicationMap.Noun = publicationMap.Noun.Truncate(300, true, true);
            }

            return publicationMaps;
        }



        /// <summary>
        /// Gets the like count with case.
        /// </summary>
        /// <param name="likeCount">The like count.</param>
        /// <returns></returns>
        protected string GetLikeCountWithCase(int likeCount)
        {
            string likes = likeCount.ToString();
            string likesCase = "пользователям";
            char lastChar = likes[likes.Length - 1];

            if (likes.Length >= 2 && likes[likes.Length - 2] == '1')
            {            
            }
            else if (lastChar == '1')
                likesCase = "пользователю";            
            else if (lastChar == '2' || lastChar == '3' || lastChar == '4')
                likesCase = "пользователям";            

            return likesCase;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        public tbl_Publication SelectById(Guid siteId, Guid publicationId)
        {
            return _dataContext.tbl_Publication.SingleOrDefault(c => c.SiteID == siteId && c.ID == publicationId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        public tbl_Publication SelectByIdForPortal(Guid siteId, Guid publicationId)
        {
            return _dataContext.tbl_Publication.SingleOrDefault(c => c.SiteID == siteId && c.ID == publicationId && (bool)c.tbl_PublicationStatus.isActive);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="publicationId">The publication id.</param>
        /// <returns></returns>
        public tbl_Publication SelectById(Guid publicationId)
        {
            return _dataContext.tbl_Publication.SingleOrDefault(c => c.ID == publicationId);
        }




        /// <summary>
        /// Selects the name by id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public string SelectNameById(Guid publicationId)
        {
            return _dataContext.tbl_Publication.Where(p => p.ID == publicationId).Select(p => p.Title).SingleOrDefault();
        }



        /// <summary>
        /// Adds the specified publication.
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <returns></returns>
        public tbl_Publication Add(tbl_Publication publication)
        {
            if (publication.ID == Guid.Empty)
                publication.ID = Guid.NewGuid();
            _dataContext.tbl_Publication.AddObject(publication);
            _dataContext.SaveChanges();

            if (publication.tbl_PublicationType.RequestSourceTypeID.HasValue)
            {
                var dataManager = new DataManager();
                
                dataManager.Request.Add(publication.SiteID, publication.ID,
                                        publication.tbl_PublicationType.RequestSourceTypeID, publication.Text, publication.AuthorID, publication.OwnerID);
            }

            return publication;
        }



        /// <summary>
        /// Updates the specified publication.
        /// </summary>
        /// <param name="publication">The publication.</param>
        public void Update(tbl_Publication publication)
        {
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Selects the publications by category id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public List<tbl_Publication> SelectByCategoryId(Guid? categoryId)
        {
            return _dataContext.tbl_Publication.Where(p => p.PublicationCategoryID == categoryId).ToList();
        }



        /// <summary>
        /// Selects the by category id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Publication> SelectByCategoryId(Guid siteId, Guid categoryId)
        {
            var dataManager = new DataManager();
            var categories = dataManager.PublicationCategory.SelectChilds(siteId, categoryId).Select(c => c.ID).ToList();

            return _dataContext.tbl_Publication.Where(p => p.SiteID == siteId && (p.PublicationCategoryID == categoryId || categories.Contains(p.PublicationCategoryID)));
        }



        /// <summary>
        /// Selects the knowledge base by category id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="categoryId">The category id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public IQueryable<ActivityPublicationMap> SelectKnowledgeBaseByCategoryId(Guid siteId, Guid categoryId, Guid? contactId)
        {
            var dataManager = new DataManager();
            var categories = dataManager.PublicationCategory.SelectChilds(siteId, categoryId).Select(c => c.ID).ToList();

            return SelectKnowledgeBase(siteId, contactId).Where(p =>(p.PublicationCategoryID == categoryId || categories.Contains(p.PublicationCategoryID)));
        }



        /// <summary>
        /// Searches the specified category id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public List<tbl_Publication> Search(Guid? categoryId, string searchQuery)
        {
            var ret = categoryId == Guid.Empty ? _dataContext.tbl_Publication.Where(p => p.Text.IndexOf(searchQuery) >= 0 || p.Title.IndexOf(searchQuery)>=0 ||p.Noun.IndexOf(searchQuery)>=0).ToList()
                :
                _dataContext.tbl_Publication.Where(p => p.PublicationCategoryID == categoryId && (p.Text.IndexOf(searchQuery) >= 0 || p.Title.IndexOf(searchQuery) >= 0 || p.Noun.IndexOf(searchQuery) >= 0)).ToList()
                ;
            return ret;
        }



        /// <summary>
        /// Determines whether the specified publication is allow.
        /// </summary>
        /// <param name="publication">The publication.</param>
        /// <param name="contactId">The contact id.</param>
        /// <param name="companyId">The company id.</param>
        /// <returns>
        ///   <c>true</c> if the specified publication is allow; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllow(tbl_Publication publication, Guid? contactId, Guid? companyId)
        {
            if (contactId.HasValue)
            {
                if (publication.AccessRecord == (int)PublicationAccessRecord.Public ||
                    publication.AccessRecord == (int)PublicationAccessRecord.Anonymous ||
                   (publication.AccessRecord == (int)PublicationAccessRecord.Personal && publication.AuthorID == CurrentUser.Instance.ContactID) ||
                   (publication.AccessRecord == (int)PublicationAccessRecord.Company && publication.AccessCompanyID == CurrentUser.Instance.CompanyID))
                {
                    return true;
                }
            }
            else if (publication.AccessRecord == (int)PublicationAccessRecord.Anonymous)
            {
                return true;
            }

            return false;
        }
    }
}