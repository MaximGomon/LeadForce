<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Domains.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.Domains" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
    function OnClientClicking(button, args) {
        window.location = button.get_navigateUrl();
        args.set_cancel(true);
    }
    </script>
</telerik:RadScriptBlock>

<ul class="domain-stats">
    <li runat="server" ID="IsNotExistsDomains" class="widget-error">У Вас нет добавленных доменов!</li>
    <li runat="server" ID="DomainsUsage">Использовано <b><asp:Literal runat="server" ID="lrlDomainsUsed" /></b> <asp:Literal runat="server" ID="lrlDomainRestriction" /></li>
    <li runat="server" ID="AddDomain"><telerik:RadButton runat="server" ID="rbtnAddDomain" OnClientClicking="OnClientClicking" Skin="Windows7" Text="Добавить" /></li>
    <li runat="server" ID="NoMoreDomains">Свяжитесь с <asp:HyperLink runat="server" ID="hlContactUrl" Target="_blank" Text="отделом продаж" /> для увеличения количества доменов!</li>
    <li runat="server" ID="CounterFoundStats">Счетчик найден на <asp:Literal runat="server" ID="lrlCounterFoundStats" /> страниц</li>
    <li runat="server" ID="Check"><telerik:RadButton runat="server" ID="rbtnCheck" OnClientClicking="OnClientClicking" Text="Проверить" Skin="Windows7" /></li>    
</ul>