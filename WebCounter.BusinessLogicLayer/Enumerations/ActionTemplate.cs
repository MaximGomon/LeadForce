using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ActionTemplate
    {
        [Description("Личные шаблоны")]
        PersonalTemplate = 0,
        [Description("Дизайнерская библиотека")]
        Design = 1,
        [Description("Пустой")]
        Empty = 2
    }
}