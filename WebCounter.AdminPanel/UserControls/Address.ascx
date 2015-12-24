<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Address.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Address" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="row">
            <label>Полный адрес:</label>
            <br/><br/>
            <labitec:AddressComboBox runat="server" ID="lbcAddress" ComboWidth="400" Enabled="true" ComboHeight="200" />
        </div>

        <div class="row">
            <label>Страна:</label>
            <telerik:RadComboBox runat="server" ID="rcbCountry" AutoPostBack="true" AllowCustomText="false" Filter="Contains" Width="231px" MaxHeight="200px" ZIndex="50001" 
            Enabled="false"/>
        </div>
        <div class="row">
            <label>Регион:</label>
            <telerik:RadComboBox runat="server" ID="rcbRegion" AutoPostBack="true" AllowCustomText="false" Filter="Contains" Width="231px" MaxHeight="200px" ZIndex="50001" 
            Enabled="false"/>
        </div>
        <div class="row">
            <label>Область:</label>
            <telerik:RadComboBox runat="server" ID="rcbDistrict" AutoPostBack="true" AllowCustomText="false" Filter="Contains" Width="231px" MaxHeight="200px" ZIndex="50001" 
            Enabled="false"/>
        </div>
        <div class="row">
            <label>Город:</label>
            <telerik:RadComboBox runat="server" ID="rcbCity" Enabled="false" AllowCustomText="false" Filter="Contains" Width="231px" MaxHeight="200px" ZIndex="50001" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>