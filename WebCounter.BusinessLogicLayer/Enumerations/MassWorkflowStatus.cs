using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum MassWorkflowStatus
    {
        [Description("В планах")]
        InPlans = 0,
        [Description("Исполняется")]
        Active = 1,
        [Description("Завершена")]
        Done = 2,
        [Description("Отменена")]
        Cancelled = 3
    }
}