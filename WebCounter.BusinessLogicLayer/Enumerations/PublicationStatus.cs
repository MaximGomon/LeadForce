using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum PublicationStatus
    {
        [Description("Черновик")]
        Draft = 0,
        [Description("Опубликована")]
        Published = 1,
        [Description("Отменена")]
        Cancelled = 2
    }
}