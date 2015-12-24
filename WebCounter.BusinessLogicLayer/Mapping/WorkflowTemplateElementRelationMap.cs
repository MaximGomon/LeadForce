using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateElementRelationMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateID { get; set; }
        public Guid StartElementID { get; set; }
        public string StartElementResult { get; set; }
        public Guid EndElementID { get; set; }
    }
}