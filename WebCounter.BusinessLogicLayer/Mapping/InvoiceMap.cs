using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class InvoiceMap
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid InvoiceTypeID { get; set; }
        public string Note { get; set; }
    }
}
