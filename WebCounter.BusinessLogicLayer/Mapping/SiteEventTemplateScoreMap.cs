using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class SiteEventTemplateScoreMap
    {
        public Guid ID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteEventTemplateID { get; set; }
        public Guid SiteActivityScoreTypeID { get; set; }
        public int OperationID { get; set; }
        public int Score { get; set; }
    }
}