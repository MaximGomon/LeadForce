using System.ComponentModel;

namespace WebCounter.BusinessLogicLayer
{
    /// <summary>
    /// Activity types
    /// </summary>
    public enum ActivityType
    {
        [Description("Просмотр страницы")]
        ViewPage = 1,
        [Description("Переход по ссылке")]
        Link = 2,
        [Description("Открытие формы")]
        OpenForm = 3,
        [Description("Заполнение формы")]
        FillForm = 4,
        /*[Description("Событие")]
        Event = 5,*/
        [Description("Пользовательское событие")]
        UserEvent = 6,
        [Description("Сообщение")]
        Impact = 7,
        [Description("Открытие посадочной страницы")]
        OpenLandingPage = 8,
        [Description("Отмена формы")]
        CancelForm = 9,
        [Description("Скачивание файла")]
        DownloadFile = 10,
        [Description("Входящее сообщение")]
        InboxMessage = 11,
        [Description("Возврат сообщения")]
        ReturnMessage = 12
    }
}