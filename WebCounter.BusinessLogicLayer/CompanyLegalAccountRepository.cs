using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class CompanyLegalAccountRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteActionAttachmentRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public CompanyLegalAccountRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="companyLegalAccountId">The comapny legal account id.</param>
        /// <returns></returns>
        public tbl_CompanyLegalAccount SelectById(Guid companyLegalAccountId)
        {
            return _dataContext.tbl_CompanyLegalAccount.SingleOrDefault(o => o.ID == companyLegalAccountId);
        }



        /// <summary>
        /// Selects the primary.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public tbl_CompanyLegalAccount SelectPrimary(Guid companyId)
        {
            return _dataContext.tbl_CompanyLegalAccount.FirstOrDefault(o => o.CompanyID == companyId && o.IsPrimary && o.IsActive);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="companyLegalAccountId">The comapny legal account id.</param>
        /// <returns></returns>
        public tbl_CompanyLegalAccount SelectById(Guid siteId, Guid companyLegalAccountId)
        {
            return _dataContext.tbl_CompanyLegalAccount.SingleOrDefault(o => o.SiteID == siteId && o.ID == companyLegalAccountId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        public IQueryable<tbl_CompanyLegalAccount> SelectAll(Guid companyId)
        {
            return _dataContext.tbl_CompanyLegalAccount.Where(o => o.CompanyID == companyId);
        }




        /// <summary>
        /// Adds the specified company legal account.
        /// </summary>
        /// <param name="companyLegalAccount">The company legal account.</param>
        /// <returns></returns>
        public tbl_CompanyLegalAccount Add(tbl_CompanyLegalAccount companyLegalAccount)
        {
            companyLegalAccount.ID = Guid.NewGuid();
            _dataContext.tbl_CompanyLegalAccount.AddObject(companyLegalAccount);
            _dataContext.SaveChanges();

            return companyLegalAccount;        
        }



        
        public void Update(List<CompanyLegalAccountMap> toUpdateCompanyLegalAccount, Guid companyId)
        {
            var existsCompanyLegalAccounts = SelectAll(companyId).ToList();
            
            foreach (var updateCompanyLegalAccount in toUpdateCompanyLegalAccount)
            {
                var existCompanyLegalAccount = existsCompanyLegalAccounts.SingleOrDefault(op => op.ID == updateCompanyLegalAccount.ID);

                if (existCompanyLegalAccount == null)
                {
                    _dataContext.tbl_CompanyLegalAccount.AddObject(new tbl_CompanyLegalAccount()
                    {
                        ID = updateCompanyLegalAccount.ID,
                        CompanyID = companyId,
                        SiteID = updateCompanyLegalAccount.SiteID,
                        Title = updateCompanyLegalAccount.Title,
                        OfficialTitle = updateCompanyLegalAccount.OfficialTitle,
                        LegalAddress = updateCompanyLegalAccount.LegalAddress,
                        OGRN = updateCompanyLegalAccount.OGRN,
                        RegistrationDate = updateCompanyLegalAccount.RegistrationDate,
                        INN = updateCompanyLegalAccount.INN,
                        KPP = updateCompanyLegalAccount.KPP,
                        RS = updateCompanyLegalAccount.RS,
                        BankID = updateCompanyLegalAccount.BankID,
                        IsPrimary = updateCompanyLegalAccount.IsPrimary,
                        IsActive = updateCompanyLegalAccount.IsActive,
                        HeadID = updateCompanyLegalAccount.HeadID,
                        HeadSignatureFileName = updateCompanyLegalAccount.HeadSignatureFileName,
                        AccountantID = updateCompanyLegalAccount.AccountantID,
                        AccountantSignatureFileName = updateCompanyLegalAccount.AccountantSignatureFileName,
                        StampFileName = updateCompanyLegalAccount.StampFileName
                    }); 
                }
                else
                {
                    existCompanyLegalAccount.Title = updateCompanyLegalAccount.Title;
                    existCompanyLegalAccount.OfficialTitle = updateCompanyLegalAccount.OfficialTitle;
                    existCompanyLegalAccount.LegalAddress = updateCompanyLegalAccount.LegalAddress;
                    existCompanyLegalAccount.OGRN = updateCompanyLegalAccount.OGRN;
                    existCompanyLegalAccount.RegistrationDate = updateCompanyLegalAccount.RegistrationDate;
                    existCompanyLegalAccount.INN = updateCompanyLegalAccount.INN;
                    existCompanyLegalAccount.KPP = updateCompanyLegalAccount.KPP;
                    existCompanyLegalAccount.RS = updateCompanyLegalAccount.RS;
                    existCompanyLegalAccount.BankID = updateCompanyLegalAccount.BankID;
                    existCompanyLegalAccount.IsPrimary = updateCompanyLegalAccount.IsPrimary;
                    existCompanyLegalAccount.IsActive = updateCompanyLegalAccount.IsActive;
                    existCompanyLegalAccount.HeadID = updateCompanyLegalAccount.HeadID;
                    existCompanyLegalAccount.HeadSignatureFileName = updateCompanyLegalAccount.HeadSignatureFileName;
                    existCompanyLegalAccount.AccountantID = updateCompanyLegalAccount.AccountantID;
                    existCompanyLegalAccount.AccountantSignatureFileName = updateCompanyLegalAccount.AccountantSignatureFileName;
                    existCompanyLegalAccount.StampFileName = updateCompanyLegalAccount.StampFileName;
                }
            }

            foreach (var existsCompanyLegalAccount in existsCompanyLegalAccounts)
            {
                if (toUpdateCompanyLegalAccount.SingleOrDefault(op => op.ID == existsCompanyLegalAccount.ID) == null)
                    _dataContext.tbl_CompanyLegalAccount.DeleteObject(existsCompanyLegalAccount);
            }

            _dataContext.SaveChanges();
        }
    }
}