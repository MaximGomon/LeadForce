using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ActivityPublicationMap
    {
        public Guid ID { get; set; }       
        public Guid AuthorID { get; set; }        
        public DateTime? Date { get; set; }
        public string FormattedDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Noun { get; set; }
        public int? PublicationKindID { get; set; }
        public Guid PublicationCategoryID { get; set; }
        public byte[] Img {get;set;}
        public string Category { get; set; }
        public string PublicationTypeLogo { get; set; }
        public string FileName { get; set; }
        public int? ContactLike { get; set; }
        public int CommentsCount { get; set; }
        public string Status { get; set; }
        public string ContactLikeUserText { get; set; }
        public int? SumLike { get; set; }
        public string OfficialComment { get; set; }
        public string PublicationUrl { get; set; }
        public string PortalUrl { get; set; }
        public int? AccessRecord { get; set; }
        public int? AccessComment { get; set; }
        public Guid? AccessCompanyID { get; set; }     
    }
}
