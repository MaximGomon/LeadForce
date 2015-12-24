using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ScoreCategory
    {
        [Description("По поведению")]
        Behavior = 0,
        [Description("По характеристике")]
        Characteristics = 1
    }
}