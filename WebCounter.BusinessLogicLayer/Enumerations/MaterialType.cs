using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum MaterialType
    {
        [Description("URL")]
        Url = 0,
        [Description("Файл")]
        File = 1,
        [Description("Форма")]
        Form = 2,
        [Description("Шаблон сообщения")]
        ActionTemplate = 3
    }
}