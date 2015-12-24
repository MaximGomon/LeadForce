using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_CurrencyMetaData))]
    public partial class tbl_Currency
    {
        public class tbl_CurrencyMetaData
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
            public object Name { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Код")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object NumCode { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Короткое название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object CharCode { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Символ")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Symbol { get; set; }

            [DataType("Bool")]
            [Display(Name = "Базовая валюта")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsBaseCurrency { get; set; }

            [DataType("Bool")]
            [Display(Name = "Обновлять внутренний курс")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsUpdateInternalCourse { get; set; }

            [DataType("Int")]
            [Display(Name = "Процент для внутреннего курса")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object InternalCoursePercent { get; set; }
        }
    }
}
