using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_ColumnTypesExpressionMetaData))]
    public partial class tbl_ColumnTypesExpression
    {
        public class tbl_ColumnTypesExpressionMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Title { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Выражение")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Expression { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Тип")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_ColumnTypes", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object ColumnTypesID { get; set; }
        }
    }
}
