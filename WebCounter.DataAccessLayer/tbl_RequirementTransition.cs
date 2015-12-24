using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_RequirementTransitionMetaData))]
    public partial class tbl_RequirementTransition
    {
        public class tbl_RequirementTransitionMetaData
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
            [Display(Name = "Активен")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsActive { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Начальное состояние")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_RequirementStatus", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object InitialRequirementStatusID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Конечное состояние")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_RequirementStatus", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object FinalRequirementStatusID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Доступно для профиля")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_AccessProfile", "ListValueField", "ID", "ListTextField", "Title")]            
            [ReadOnly(false)]
            public object AllowedAccessProfileID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Доступно на портале")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsPortalAllowed { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Тип требования")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_RequirementType", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object RequirementTypeID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Требовать отзыв")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsReviewRequired { get; set; }                        
        }
    }
}