<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DictionaryOnDemandComboBox.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.DictionaryOnDemandComboBox" %>
<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function <%= ClientID %>_SelectedValue() {
            return $find('<%= rcbDictionary.ClientID %>').get_value();
        }
    </script>
</telerik:RadScriptBlock>
<telerik:RadComboBox ID="rcbDictionary" CausesValidation="false" DataSourceID="edsDictionary" runat="server" AllowCustomText="false" Filter="Contains" EnableEmbeddedSkins="false" Width="234px" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="10" ShowMoreResultsBox="true" EnableVirtualScrolling="true" Skin="Labitec" />
<asp:RequiredFieldValidator ID="rfvDictionary" ControlToValidate="rcbDictionary" CssClass="required" Text="*" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue=""/>
<asp:EntityDataSource ID="edsDictionary" runat="server" ConnectionString="name=WebCounterEntities" DefaultContainerName="WebCounterEntities" OnSelecting="edsDictionary_OnSelecting" />
