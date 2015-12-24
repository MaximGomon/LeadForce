
using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum ImportType
    {
        [Description("Excel")]
        Excel = 0,
        [Description("CSV файл")]
        Csv = 1
    }
}
