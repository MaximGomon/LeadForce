using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementExternalRequestMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateElementID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}