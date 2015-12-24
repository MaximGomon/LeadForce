using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class CompanyLegalAccountMap
    {
        public Guid ID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid SiteID { get; set; }
        public string Title { get; set; }        
        public string OfficialTitle { get; set; }
        public string LegalAddress { get; set; }
        public string OGRN { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string RS { get; set; }
        public Guid? BankID { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public Guid? HeadID { get; set; }
        public string HeadSignatureFileName { get; set; }
        public Guid? AccountantID { get; set; }
        public string AccountantSignatureFileName { get; set; }
        public string StampFileName { get; set;}
    }
}
