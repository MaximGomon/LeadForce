using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations.Request
{
    public enum OutOfListServiceContacts
    {
        [Description("Отказ")]
        Reject = 1,
        [Description("Соглашение по умолчанию")]
        Default = 2,
        [Description("Текущее соглашение")]
        Current = 3
    }
}
