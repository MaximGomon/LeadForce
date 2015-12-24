using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_RequirementStatusMetaData))]
    public partial class tbl_RequirementStatus
    {
        public class tbl_RequirementStatusMetaData
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
            [Display(Name = "По умолчанию")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsDefault { get; set; }

            [DataType("Bool")]
            [Display(Name = "Конечное")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsLast { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Роль в обслуживании")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_ServiceLevelRole", "ListValueField", "ID", "ListTextField", "Title")]            
            public object ServiceLevelRoleID { get; set; }
        }
    }
}
