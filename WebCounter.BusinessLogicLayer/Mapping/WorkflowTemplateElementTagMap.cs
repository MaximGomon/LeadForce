using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementTagMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateElementID { get; set; }
        public Guid SiteTagID { get; set; }
        public string SiteTagName { get; set; }
        public int Operation { get; set; }
    }
}
