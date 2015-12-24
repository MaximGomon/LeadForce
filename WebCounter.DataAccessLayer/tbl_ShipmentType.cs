using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_ShipmentTypeMetaData))]
    public partial class tbl_ShipmentType
    {
        public class tbl_ShipmentTypeMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object SiteID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Title { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Нумератор")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_Numerator", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object NumeratorID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Значение по умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsDefault { get; set; }
        }
    }
}