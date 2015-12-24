using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ActivityCommentMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid PublicationID { get; set; }
        public DateTime CreatedAt { get; set; }        
        public string UserFullName { get; set; }
        public string Comment { get; set; }
        public string FormattedDate { get; set; }
        public int SumLike { get; set; }
        public int? ContactLike { get; set; }
        public bool isOfficialAnswer { get; set; }
        public string FileName { get; set; }
        public string VirtualFileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
