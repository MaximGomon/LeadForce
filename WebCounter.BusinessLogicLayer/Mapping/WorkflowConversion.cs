using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Mapping
{
    public class WorkflowConversion
    {
        public Guid Id { get; set; }
        public string Result { get; set; }
        public int Count { get; set; }
        public double Conversion { get; set; }
    }
}
