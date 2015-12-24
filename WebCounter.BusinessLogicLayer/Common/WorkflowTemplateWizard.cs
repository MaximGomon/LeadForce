using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class WorkflowTemplateWizardTag
    {
        public Guid Id { get; set; }
        public int Operation { get; set; }
        public Guid TagId { get; set; }
    }

    public class WorkflowTemplateWizardRole
    {
        public Guid OldContactRoleID { get; set; }
        public Guid ContactRoleID { get; set; }
        public string OldEmail { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }

    public class WorkflowTemplateWizardConditionEvent
    {
        public int ActivityType { get; set; }
        public string Code { get; set; }
        public int ActualPeriod { get; set; }
    }

    public class WorkflowTemplateWizardActionTemplate
    {
        public Guid Id { get; set; }
        public string MessageBody { get; set; }
    }

    public class WorkflowTemplateWizardMaterial
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public string OldValue { get; set; }
        public string Value { get; set; }
    }
}
