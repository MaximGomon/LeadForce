<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientBaseGrowth.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.ClientBaseGrowth" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function OnClientClicking(button, args) {
            window.location = button.get_navigateUrl();
            args.set_cancel(true);
        }
    </script>
</telerik:RadScriptBlock>

<ul class="widget-container">
    <li>Новых контактов <%= DataManager.StatisticData.ClientBaseGrowthNewTotal.HtmlValue %><br/>
        Из них
        <ul>
            <li>Регистраций <%= DataManager.StatisticData.ClientBaseGrowthNewRegistered.HtmlValue %></li>
            <li>Из них импортировано <%= DataManager.StatisticData.ClientBaseGrowthNewImported.HtmlValue %></li>
            <li><telerik:RadButton runat="server" ID="rbtnImport" Skin="Windows7" OnClientClicking="OnClientClicking" ButtonType="StandardButton" Text="Импортировать"/></li>
        </ul>
    </li>
    <li>Контактных форм <b><asp:Literal runat="server" ID="lrlTotalForms" /></b> на <b><asp:Literal runat="server" ID="lrlTotalPages" /></b> страниц<br/>
        <asp:Literal runat="server" ID="lrlTitle" />
        <ul>            
            <asp:Literal runat="server" ID="lrlClientBaseGrowthTemplateForm" />
            <asp:Literal runat="server" ID="lrlClientBaseOtherFormsCount" />
        </ul>
    </li>
    <asp:Literal runat="server" ID="lrlMessage" />
</ul>