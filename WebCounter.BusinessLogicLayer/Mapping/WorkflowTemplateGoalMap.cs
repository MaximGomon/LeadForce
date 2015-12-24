using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class WorkflowTemplateGoalMap
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<WorkflowTemplateElementMap> Elements = new List<WorkflowTemplateElementMap>();
    }
}
