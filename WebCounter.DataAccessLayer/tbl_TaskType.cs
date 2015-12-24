using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_TaskTypeMetaData))]
    public partial class tbl_TaskType
    {
        public class tbl_TaskTypeMetaData
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
            
            [DataType("Int")]
            [Display(Name = "Стандартная длительность, часов")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object StandardDurationHours { get; set; }

            [DataType("Int")]
            [Display(Name = "Стандартная длительность, минут")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object StandardDurationMinutes { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Категория")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_TaskTypeCategory", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object TaskTypeCategoryID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Корректировать длительность")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_TaskTypeAdjustDuration", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object TaskTypeAdjustDurationID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Публичное мероприятие")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsPublicEvent { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Схема оплаты")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_TaskTypePaymentScheme", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object TaskTypePaymentSchemeID { get; set; }

            [DataType("Bool")]
            [Display(Name = "Оплата за время")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(false)]
            public object IsTimePayment { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Количество участников")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_TaskMembersCount", "ListValueField", "ID", "ListTextField", "Title")]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object TaskMembersCountID { get; set; }

            [DataType("Dictionary")]
            [Display(Name = "Продукт")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true, "Dictionary", "tbl_Product", "ListValueField", "ID", "ListTextField", "Title")]
            [ReadOnly(false)]
            public object ProductID { get; set; }
        }
    }
}