using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ProductWorkWithComplectation
    {
        [Description("Простой продукт")]
        Simple = 1,
        [Description("Комплект")]
        Package = 2,
        [Description("Сборный продукт")]
        Modular = 3
    }
}