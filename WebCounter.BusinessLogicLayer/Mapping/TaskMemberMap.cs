using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class TaskMemberMap
    {
        public Guid ID { get; set; }
        public Guid TaskID { get; set; }
        public Guid? ContractorID { get; set; }
        public Guid? ContactID { get; set; }
        public int? TaskMemberRoleID { get; set; }
        public int? TaskMemberStatusID { get; set; }
        public string Comment { get; set; }
        public Guid? OrderID { get; set; }
        public Guid? OrderProductsID { get; set; }
        public string UserComment { get; set; }
        public bool IsInformed { get; set; }
    }
}
