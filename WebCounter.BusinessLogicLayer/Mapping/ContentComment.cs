using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ContentComment
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid ContentID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedAt { get; set; }        
        public string UserFullName { get; set; }
        public string DestinationUserFullName { get; set; }
        public string Comment { get; set; }
        public string FormattedDate { get; set; }        
        public bool IsOfficialAnswer { get; set; }
        public bool IsInternal { get; set; }
        public string FileName { get; set; }
        public string VirtualFileName { get; set; }
        public int SumLike { get; set; }
        public bool ContactLike { get; set; }
    }
}