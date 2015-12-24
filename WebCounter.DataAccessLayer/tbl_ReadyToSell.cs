using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_ReadyToSellMetaData))]
    public partial class tbl_ReadyToSell
    {
        public class tbl_ReadyToSellMetaData
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

            [DataType("Bool")]
            [Display(Name = "Значение по умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]            
            [ReadOnly(false)]
            public object IsDefault { get; set; }

            [DataType("Int")]
            [Display(Name = "Минимум баллов")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object MinScore { get; set; }


            [DataType("Int")]
            [Display(Name = "Максимум баллов")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object MaxScore { get; set; }


            [DataType("Image")]
            [Display(Name = "Изображение")]
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Image { get; set; }
        }
    }
}
