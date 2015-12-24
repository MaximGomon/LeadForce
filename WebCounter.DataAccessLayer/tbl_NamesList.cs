using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof (tbl_NamesListMetaData))]
    public partial class tbl_NamesList
    {
        public class tbl_NamesListMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Имя")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Маска для отчества")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]            
            [ReadOnly(false)]
            public object PatronymicMask { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Пол")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Gender { get; set; }
        }
    }
}
