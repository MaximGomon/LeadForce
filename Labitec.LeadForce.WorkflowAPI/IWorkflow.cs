using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebCounter.BusinessLogicLayer.Mapping;

namespace Labitec.LeadForce.WorkflowAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWorkflow
    {
        [OperationContract]
        Dictionary<string, string> GetWorkflowTemplates(Guid siteId);

        [OperationContract]
        WorkflowTemplate GetWorkflowTemplate(Guid siteId, Guid workflowTemplateId);

        [OperationContract]
        IEnumerable<WorkflowTemplateElement> GetWorkflowTemplateElements(Guid workflowTemplateId);

        [OperationContract]
        string GetSiteActionTemplateName(Guid siteActionTemplateId);

        [OperationContract]
        IEnumerable<TaskType> GetTaskTypes(Guid siteId);

        [OperationContract]
        void SaveWorkflowTemplateElements(IEnumerable<WorkflowTemplateElement> workflowTemplateElementList, Guid workflowTemplateId);

        [OperationContract]
        IEnumerable<WorkflowTemplateElementRelation> GetWorkflowTemplateElementRelations(Guid workflowTemplateId);

        [OperationContract]
        void SaveWorkflowTemplateElementRelations(IEnumerable<WorkflowTemplateElementRelation> workflowTemplateElementRelationList, Guid workflowTemplateId);

        [OperationContract]
        void SaveWorkflowTemplate(Guid siteId, Guid workflowTemplateId, string workflowXml);

        [OperationContract]
        IEnumerable<WorkflowTemplateConditionEvent> GetWorkflowTemplateConditionEvents(Guid workflowTemplateElementId);

        [OperationContract]
        IEnumerable<SiteColumn> GetSiteColumns(Guid siteId);

        [OperationContract]
        SiteColumn GetSiteColumn(Guid siteId, Guid siteColumnId);

        [OperationContract]
        IEnumerable<SiteColumnValue> GetSiteColumnValues(Guid siteColumnId);

        [OperationContract]
        IEnumerable<SiteActivityScoreType> GetSiteActivityScoreTypes(Guid siteId);

        [OperationContract]
        Dictionary<string, string> GetCodes(Guid siteId, int activityType);

        [OperationContract]
        IEnumerable<string> GetModules(Guid userId);

        [OperationContract]
        IEnumerable<WorkflowTemplateElementTag> GetWorkflowTemplateElementTags(Guid workflowTemplateElementId);

        [OperationContract]
        Guid AddSiteTag(Guid siteId, Guid userId, string name);

        [OperationContract]
        Dictionary<string, string> GetSiteTags(Guid siteId);

        [OperationContract]
        IEnumerable<WorkflowConversion> GetElementConversion(Guid workflowTemplateId, DateTime? workflowStartDate, DateTime? workflowEndDate, DateTime? activityStartDate, DateTime? activityEndDate);

        [OperationContract]
        IEnumerable<WorkflowConversion> GetRelationConversion(Guid workflowTemplateId, DateTime? workflowStartDate, DateTime? workflowEndDate, DateTime? activityStartDate, DateTime? activityEndDate);

        [OperationContract]
        bool ShowDeletedMessage(IEnumerable<Guid> workflowTemplatesIdList);
    }


    [DataContract]
    public class WorkflowTemplateElement
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int ElementType { get; set; }
        [DataMember]
        public bool Optional { get; set; }
        [DataMember]
        public string ResultName { get; set; }
        [DataMember]
        public bool AllowOptionalTransfer { get; set; }
        [DataMember]
        public bool ShowCurrentUser { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public int StartAfter { get; set; }
        [DataMember]
        public int StartPeriod { get; set; }
        [DataMember]
        public int? DurationHours { get; set; }
        [DataMember]
        public int? DurationMinutes { get; set; }
        [DataMember]
        public int? ControlAfter { get; set; }
        [DataMember]
        public int? ControlPeriod { get; set; }
        [DataMember]
        public bool ControlFromBeginProccess { get; set; }
        [DataMember]
        public Dictionary<string, string> Parameters { get; set; }
        [DataMember]
        public int? Condition { get; set; }
        [DataMember]
        public int? ActivityCount { get; set; }
        [DataMember]
        public IEnumerable<WorkflowTemplateConditionEvent> ConditionEvent { get; set; }
        [DataMember]
        public IEnumerable<WorkflowTemplateElementResult> ElementResult { get; set; }
        [DataMember]
        public IEnumerable<WorkflowTemplateElementTag> Tag { get; set; }
        [DataMember]
        public IEnumerable<WorkflowTemplateElementPeriod> Period { get; set; }
        [DataMember]
        public IEnumerable<WorkflowTemplateElementExternalRequest> ExternalRequest { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }



    [DataContract]
    public class WorkflowTemplate
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string WorkflowXml { get; set; }
    }



    [DataContract]
    public class TaskType
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string Title { get; set; }
    }



    [DataContract]
    public class WorkflowTemplateElementResult
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateElementID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsSystem { get; set; }
    }



    [DataContract]
    public class WorkflowTemplateElementRelation
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateID { get; set; }
        [DataMember]
        public Guid StartElementID { get; set; }
        [DataMember]
        public string StartElementResult { get; set; }
        [DataMember]
        public Guid EndElementID { get; set; }
    }



    [DataContract]
    public class WorkflowTemplateConditionEvent
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid? WorkflowTemplateID { get; set; }
        [DataMember]
        public Guid? WorkflowTemplateElementEventID { get; set; }
        [DataMember]
        public int Category { get; set; }
        [DataMember]
        public int? ActivityType { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public int? ActualPeriod { get; set; }
        [DataMember]
        public string Requisite { get; set; }
        [DataMember]
        public int? Formula { get; set; }
        [DataMember]
        public string Value { get; set; }
    }


    [DataContract]
    public class SiteColumn
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid SiteID { get; set; }
        [DataMember]
        public Guid? SiteActivityRuleID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid CategoryID { get; set; }
        [DataMember]
        public int TypeID { get; set; }
        [DataMember]
        public string Code { get; set; }
    }



    [DataContract]
    public class SiteColumnValue
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid SiteColumnID { get; set; }
        [DataMember]
        public string Value { get; set; }
    }



    [DataContract]
    public class SiteActivityScoreType
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid SiteID { get; set; }
        [DataMember]
        public string Title { get; set; }
    }


    [DataContract]
    public class WorkflowTemplateElementTag
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateElementID { get; set; }
        [DataMember]
        public Guid SiteTagID { get; set; }
        [DataMember]
        public string SiteTagName { get; set; }
        [DataMember]
        public int Operation { get; set; }
    }

    [DataContract]
    public class WorkflowTemplateElementPeriod
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateElementID { get; set; }
        [DataMember]
        public int DayWeek { get; set; }
        [DataMember]
        public TimeSpan? FromTime { get; set; }
        [DataMember]
        public TimeSpan? ToTime { get; set; }
    }


    [DataContract]
    public class WorkflowTemplateElementExternalRequest
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public Guid WorkflowTemplateElementID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }


    [DataContract]
    public class WorkflowConversion
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public double Conversion { get; set; }
    }
}