<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Welcome.ascx.cs" Inherits="Labitec.LeadForce.Portal.Shared.UserControls.Welcome" %>

<div class="welcome-container">
    <p>Вы получили приглашение от <asp:Literal runat="server" ID="lrlCreatorFullName" />, компания <asp:Literal runat="server" ID="lrlCreatorCompany1" /> для входа в портал сервиса <b><u>LeadForce</u></b>! Для доступа в качестве логина Вам необходимо использовать Ваш email: <asp:Literal runat="server" ID="lrlMyEmail" /></p>
    <br/>
    <p>Для начала работы Вам необходимо указать ваш пароль: <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" ValidationGroup="startWork" /><asp:RequiredFieldValidator ID="valRequirePassword" CssClass="required" runat="server" SetFocusOnError="true" Text="*" ControlToValidate="txtPassword" ValidationGroup="StartWork" /><telerik:RadButton runat="server" ID="rbtnStartWork" Skin="Windows7" Text="Начать работу" ValidationGroup="StartWork" OnClick="rbtnStartWork_OnClick" /></p>
    <br/>
    <p>Вы сможете использовать LeadForce для эффективных коммуникаций с компанией <asp:Literal runat="server" ID="lrlCreatorCompany2" />, включая:</p>
    <ul>
        <li>Планирование совместных встреч и мероприятий</li>
        <li>Просмотр текущих заказов и счетов</li>
        <li>Доступ к базе знаний и службе поддержки</li>
    </ul>
    <br/>
    <p><b>Краткая справка о сервисе LeadForce:</b></p>
    <br/>
    <p><b>LeadForce</b> – это первое российское решение, предназначенное для автоматизации маркетинга. Специальные аналитические возможности позволяют решению LeadForce детально отслеживать активность пользователя на сайте, в том числе выделять тех посетителей, кто уже готов к продаже.</p>
    <br/>
    <p><b>Возможности LeadForce Marketing включают:</b></p>    
    <ol>
        <li>Учет и мониторинг клиентов</li>
        <li>Поддержка процессов развитие клиентов</li>
        <li>Гибкие возможности e-mail маркетинга</li>
        <li>Интерактивные online формы для взаимодействия с клиентами</li>
        <li>Управление маркетинговыми материалами</li>
    </ol>
    <br/>
    <p>Важно, что LeadForce – это Интернет решение, которое может быть встроено в любой существующий web сайт. Кроме того, Ваши сотрудники смогут получить доступ к информации через Интернет. Установки какого-либо специального программного обеспечения на рабочие места сотрудников или сервер не требуется.</p>
    <br/>
    <p>Подробнее: <a href="http://www.leadforce.ru" target="_blank">www.leadforce.ru</a></p>
</div>