using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class SiteActivityRuleLayoutParams : tbl_SiteActivityRuleLayout
    {
        public string SiteColumnName { get; set; }
        public int? SiteColumnTypeID { get; set; }
    }
}
