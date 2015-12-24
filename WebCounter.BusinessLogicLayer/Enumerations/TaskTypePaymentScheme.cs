using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum TaskTypePaymentScheme
    {
        [Description("Нет")]
        None = 1,
        [Description("Оплата за контакт")]
        ContactPayment = 2,
        [Description("Оплата за компанию")]
        CompanyPayment = 3
    }
}