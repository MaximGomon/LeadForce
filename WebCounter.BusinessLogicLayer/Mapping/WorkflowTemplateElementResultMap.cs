using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementResultMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateElementID { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }
    }
}
