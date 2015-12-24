using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class LinkProcessingMap
    {
        public Guid ID { get; set; }
        public Guid ContactID { get; set; }
        public Guid? SiteActionID { get; set; }
        public Guid? SiteActionTemplateID { get; set; }
        public Guid? SiteActivityRuleID { get; set; }
        public string LinkURL { get; set; }
        public DateTime? ActionLinkDate { get; set; }
        public Guid SiteID { get; set; }
        public int? SiteActivityRuleTypeID { get; set; }
        public string SiteActivityRuleCode { get; set; }
        public string SiteActivityRuleURL { get; set; }
    }
}
