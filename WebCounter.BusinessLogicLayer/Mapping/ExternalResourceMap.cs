using System;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ExternalResourceMap
    {
        public Guid ID { get; set; }
        public Guid DestinationID { get; set; }
        public string Title { get; set; }
        public int ResourcePlaceID { get; set; }
        public int ExternalResourceTypeID { get; set; }
        public string File { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
}
