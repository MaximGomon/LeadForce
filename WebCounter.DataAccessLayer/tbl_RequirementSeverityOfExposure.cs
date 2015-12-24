using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_RequirementSeverityOfExposureMetaData))]
    public partial class tbl_RequirementSeverityOfExposure
    {
        public class tbl_RequirementSeverityOfExposureMetaData
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
            [Display(Name = "Тип требования")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, 
                "ShowInEdit", true, 
                "Dictionary", "tbl_RequirementType", 
                "ListValueField", "ID", 
                "ListTextField", "Title",
                "AutoPostBack", "True",
                "TargetFilterColumn", "ParentID",
                "FilterByColumn", "RequirementTypeID")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object RequirementTypeID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Родительская степень воздействия")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, 
                "ShowInEdit", true, 
                "Dictionary", "tbl_RequirementSeverityOfExposure", 
                "ListValueField", "ID", 
                "ListTextField", "Title",
                "RelatedFilterColumn", "RequirementTypeID")]
            [ReadOnly(false)]
            public object ParentID { get; set; }
        }
    }
}
