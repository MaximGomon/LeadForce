using WebCounter.BusinessLogicLayer.Enumerations;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Configuration
{
    public static class SiteActionTemplates
    {
        public static tbl_SiteActionTemplate InvitationTemplate
        {
            get
            {
                return new tbl_SiteActionTemplate()
                           {
                               Title = "Согласование исполнителя",
                               ActionTypeID = (int)ActionType.EmailToUser,
                               ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                               FromEmail = "info@leadforce.ru",
                               SystemName = "InvitationTemplate",
                               SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                               MessageCaption = "Согласование исполнителя",
                               MessageBody = @"
<p>Добрый день,</p>
<p>Компания #Author.Company# приглашает Вас в качестве исполнителя для участия в задаче: #Task.Title#</p>
<p>Вы можете посмотреть подробную информацию по ссылке: #Portal.Link#</p>
<p>С уважением,
<br/>
#Author.UserFullName#</p>"
                           };
            }
        }


        public static tbl_SiteActionTemplate InvitationMeetingTemplate
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Согласование участника",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "InvitationMeetingTemplate",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Согласование участника",
                    MessageBody = @"
<p>Добрый день,</p>
<p>Компания #Author.Company# приглашает Вас в качестве участника в задаче: #Task.Title#</p>
<p>Вы можете посмотреть подробную информацию по ссылке: #Portal.Link#</p>
<p>С уважением,
<br/>
#Author.UserFullName#</p>"
                };
            }
        }



        public static tbl_SiteActionTemplate ClientInformationTemplate
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Информирование о статусе задачи",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "ClientInformationTemplate",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Информирование о статусе задачи",
                    MessageBody = @"
<p>Добрый день,</p>
<p>#Author.UserFullName#, компания #Author.Company# приглашает Вас ознакомится с ходом работ по задаче: #Task.Title#</p>
<p>Вы можете посмотреть подробную информацию по ссылке: #Portal.Link#</p>
<p>С уважением,<br/>
#Author.UserFullName#</p>"
                };
            }
        }



        public static tbl_SiteActionTemplate PasswordRemindNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Напоминание пароля",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "PasswordRemindNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Напоминание пароля",
                    MessageBody = @"
<p>Добрый день,</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<p>С уважением,<br/>
Администрация портала</p>"
                };
            }
        }


        public static tbl_SiteActionTemplate NewUserRegistrationNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Регистрация нового пользователя",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "NewUserRegistrationNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Регистрация нового пользователя",
                    MessageBody = @"
<p>Добрый день,</p>
<p>Спасибо за регистрацию!</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<p>Для активации Вашей учетной записи нужно перейти по ссылке: #Activation.Url#</p>
<p>С уважением,<br/>
Администрация портала</p>"
                };
            }
        }

        #region Request

        /// <summary>
        /// Gets the new request notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequestRegistrationNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Запрос зарегистрирован",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequestRegistrationNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Запрос зарегистрирован",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>Ваш запрос #Request.ShortDescription# от #Request.CreatedAt# зарегистрирован в компании Лабитек.</p>
<p>Для Вашего запроса установлен следующий срок реакции: #Request.ReactionTime# часов (до #Request.ReactionDatePlanned#).</p>
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }



        /// <summary>
        /// Gets the request new notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequestNewNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Новый запрос",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequestNewNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Новый запрос",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>Запрос #Request.ShortDescription# от #Request.CreatedAt# поступил от #Request.Contact.UserFullName# (компания #Request.Company.Name#).</p>
<p>Для запроса установлен следующий срок реакции: #Request.ReactionTime# часов (до #Request.ReactionDatePlanned#).</p>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<br/>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }



        /// <summary>
        /// Gets the new request notification.
        public static tbl_SiteActionTemplate RequestProcessedNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Запрос обработан",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequestProcessedNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Запрос обработан",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>Ваш запрос #Request.ShortDescription# от #Request.CreatedAt# обработан сотрудником #Request.Responsible.UserFullName#.</p>
<p>На его основе зарегистрированы следующие отдельные требования:</p>
#Requirement.RegisteredList#
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
#Request.Responsible.UserFullName#</p>
<p>P.S. Если Ваш запрос содержал требования, не зарегистрированные в системе, сообщите об этом, зарегистрировав отзыв о работе по ссылке #Portal.Link# с указанием оригинального запроса, а также требования, не отраженного в списке.</p>"
                };
            }
        }


        /// <summary>
        /// Gets the request client inform notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequestClientInformNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Уведомление по запросам",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequestClientInformNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Уведомление по запросам",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>

#Requirement.WorkedList#
<br/>
#Requirement.CompletedList#
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>
<p>P.S. Если в отчете отсутствуют какие-либо Ваши открытые требования, о которых Вы информировали ранее, сообщите об этом, зарегистрировав отзыв о работе по ссылке #Portal.Link# с указанием оригинального запроса, а также требования, не отраженного в списке.</p>"
                };
            }
        }



        /// <summary>
        /// Gets the requirement comment notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequirementCommentNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Требование: добавлен новый комментарий",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequirementCommentNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Требование: добавлен новый комментарий",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>По требованию #Requirement.ShortDescription# есть новый комментарий:</p>
<p>#Requirement.Comment#</p>
<p>#Requirement.Comment.ReplyLink#</p>
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }





        /// <summary>
        /// Gets the request comment notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequestCommentNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Запрос: добавлен новый комментарий",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequestCommentNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Запрос: добавлен новый комментарий",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>По запросу #Request.ShortDescription# есть новый комментарий:</p>
<p>#Request.Comment#</p>
<p>#Request.Comment.ReplyLink#</p>
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }


        /// <summary>
        /// Gets the requirement change responsible notification.
        /// </summary>
        public static tbl_SiteActionTemplate RequirementChangeResponsibleNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Смена ответственного",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "RequirementChangeResponsibleNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Смена ответственного",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>Вы назначены ответственным за требование #Requirement.ShortDescription#.</p>
<br/>
<p>Дополнительную информацию по статусу запросов и требований Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<p>Пароль: #User.Password#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }        



        /// <summary>
        /// Gets the table template.
        /// </summary>
        public static string RequirementTableTemplate
        {
            get
            {
                return @"
<table style='border-collapse:collapse;border: 1px solid #000'>
    <tr>
        <th align='left' style='border: 1px solid #000;padding:5px'>Суть</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Тип</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Состояние</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Оценка</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Комментарий</th>
    </tr>
    #Rows#
</table>";
            }
        }



        /// <summary>
        /// Gets the row template.
        /// </summary>
        public static string RequirementRowTemplate
        {
            get
            {
                return @"
    <tr style='#Style#'>
        <td style='border: 1px solid #000;padding:5px'>#ShortDescription#</td>
        <td style='border: 1px solid #000;padding:5px'>#Type#</td>
        <td style='border: 1px solid #000;padding:5px'>#Status#</td>
        <td style='border: 1px solid #000;padding:5px'>#Quantity# #Unit#<br/>#Amount# #Currency#</td>
        <td style='border: 1px solid #000;padding:5px'>#OfficialComment#</td>
    </tr>";
            }
        }
        #endregion


        #region Invoice

        /// <summary>
        /// Gets the invoicing notification.
        /// </summary>
        public static tbl_SiteActionTemplate InvoicingNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Выставление счета",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "InvoicingNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Выставлен счет",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>Для Вашей компании подготовлен <a href=""#Invoice.PrintVersion.Link#"">счет от компании Лабитек</a>.</p>
<p>Все вопросы Вы можете уточнить у ответственного менеджера #Invoice.Executor.UserFullName# по телефону #Invoice.Executor.Phone#.</p>
<p>Полный реестр счетов Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }


        public static tbl_SiteActionTemplate InvoiceInformClientNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Счета: Информирование клиента",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "InvoiceInformClientNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Счета к оплате",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>На данный момент для Вашей компании есть следующие счета к оплате:</p>
<p>#Invoice.PayableList#</p>
<p>Информацию по оплаченным счетам и закрывющим документам Вы можете найти в Вашем личном кабинете по адресу: #Portal.Link#</p>
<p>Электронный адрес: #User.Email#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }


        /// <summary>
        /// Gets the invoice table template.
        /// </summary>
        public static string InvoiceTableTemplate
        {
            get
            {
                return @"
<table style='border-collapse:collapse;border: 1px solid #000'>
    <tr>
        <th align='left' style='border: 1px solid #000;padding:5px'>Счет</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Сумма, рублей</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Согласованная дата оплаты</th>
        <th align='left' style='border: 1px solid #000;padding:5px'>Примечание</th>        
    </tr>
    #Rows#
</table>";
            }
        }


        public static string InvoiceRowTemplate
        {
            get
            {
                return @"
    <tr>
        <td style='border: 1px solid #000;padding:5px'><a href=""#Invoice.List.PrintVersion.Link#"">Счет #Invoice.List.Number# от #Invoice.List.CreatedAt#</a></td>
        <td style='border: 1px solid #000;padding:5px'>#Invoice.List.Amount#</td>
        <td style='border: 1px solid #000;padding:5px'>#Invoice.List.PaymentDatePlanned#</td>
        <td style='border: 1px solid #000;padding:5px'>#Invoice.List.Note#</td>        
    </tr>";
            }
        }

        public static tbl_SiteActionTemplate InvoiceCommentNotification
        {
            get
            {
                return new tbl_SiteActionTemplate()
                {
                    Title = "Счета: Информирование по комментариям",
                    ActionTypeID = (int)ActionType.EmailToUser,
                    ReplaceLinksID = (int)ReplaceLinks.GoogleLinks,
                    FromEmail = "info@leadforce.ru",
                    SystemName = "InvoiceCommentNotification",
                    SiteActionTemplateCategoryID = (int)SiteActionTemplateCategory.System,
                    MessageCaption = "Новый комментарий",
                    MessageBody = @"
<p>#User.FirstName#, добрый день,</p>
<p>К счету #Invoice.Number# от #Invoice.CreatedAt# есть новый комментарий:</p>
<br/>
<p>#Invoice.Comment#</p>
<br/>
<p>С уважением,<br/>
Компания Лабитек</p>"
                };
            }
        }

        #endregion
    }
}