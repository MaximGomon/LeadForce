using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    [Serializable]
    public class ImportTagMap
    {
        public Guid ID { get; set; }
        public Guid ImportID { get; set; }
        public Guid SiteTagID { get; set; }
        public int Operation { get; set; }
    }
}
