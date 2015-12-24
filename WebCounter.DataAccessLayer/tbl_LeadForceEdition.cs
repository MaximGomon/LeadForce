using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_LeadForceProductMetaData))]
    public partial class tbl_LeadForceProduct
    {
        public class tbl_LeadForceProductMetaData
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

            [DataType(DataType.MultilineText)]
            [Display(Name = "Описание")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object Description { get; set; }
        }
    }
}
