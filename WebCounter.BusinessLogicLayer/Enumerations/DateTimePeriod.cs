using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum DateTimePeriod
    {
        [Description("сегодня")]
        Today = 0,
        [Description("вчера")]
        Yesterday = 1,
        [Description("неделя")]
        Week = 2,
        [Description("месяц")]
        Month = 3,
        [Description("квартал")]
        Quarter = 4,
        [Description("год")]
        Year = 5        
    }
}
