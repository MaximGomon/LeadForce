using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_LeadForceEditionMetaData))]
    public partial class tbl_LeadForceEdition
    {
        public class tbl_LeadForceEditionMetaData
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
