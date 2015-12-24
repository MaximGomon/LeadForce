using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum RegistrationSource
    {
        [Description("Форма на сайте")]
        CounterService = 0,
        [Description("Добавлено вручную")]
        Manual = 1,
        [Description("API")]
        API = 2,
        [Description("Импорт")]
        Import = 3
    }
}
