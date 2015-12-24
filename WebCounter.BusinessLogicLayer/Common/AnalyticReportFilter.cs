using System;

namespace WebCounter.BusinessLogicLayer.Common
{
    [Serializable]
    public class AnalyticReportFilter
    {
        public Guid FilterId { get; set; }
        public string ColumnName { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
        public string Query { get; set; }
    }
}
