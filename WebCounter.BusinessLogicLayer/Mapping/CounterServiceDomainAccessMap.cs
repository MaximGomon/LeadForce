using System;
using System.Collections.Generic;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class CounterServiceDomainAccessMap
    {
        public Guid SiteId { get; set; }
        public bool IsBlockAccessFromDomainsOutsideOfList { get; set; }
        public List<string> Domains { get; set; }
    }
}
