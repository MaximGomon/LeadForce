using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum DataBaseStatus
    {
        [Description("Активен")]
        Active = 0,
        [Description("Удален")]
        Deleted = 5
    }
}