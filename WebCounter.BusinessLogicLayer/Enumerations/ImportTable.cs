using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Import tables
    /// </summary>
    public enum ImportTable
    {
        [Description("Клиентские данные")]
        Company = 1,
        [Description("Контакты")]
        Contact = 2
    }
}