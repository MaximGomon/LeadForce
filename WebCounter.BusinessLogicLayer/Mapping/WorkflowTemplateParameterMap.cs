using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateParameterMap
    {
        public Guid ID { get; set; }
        public Guid WorkflowTemplateID { get; set; }
        public string Name { get; set; }
        public Guid? ModuleID { get; set; }
        public bool IsSystem { get; set; }
        public string Description { get; set; }
    }
}
