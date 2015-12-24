using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer.Enumerations
{
    public enum WorkflowTemplateConditionEventCategory
    {
        [Description("Действие")]
        Activity = 0,
        [Description("Значение реквизита")]
        ColumnValue = 1,
        [Description("Балл по поведению")]
        BehaviorScore = 2,
        [Description("Балл по характеристикам")]
        CharacteristicsScore = 3
    }
}