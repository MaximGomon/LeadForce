<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortalSettings.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Portal.PortalSettings" %>

<labitec:Grid ID="gridPortalSettings" OnItemDataBound="gridPortalSettings_OnItemDataBound" TableName="tbl_PortalSettings" ClassName="WebCounter.AdminPanel.PortalSettings" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Title" HeaderText="Название портала" runat="server"/>
        <labitec:GridColumn ID="GridColumn2" DataField="Domain" HeaderText="Домен" runat="server"/>        
        <labitec:GridColumn ID="GridColumn5" DataField="ID" HeaderText="Ссылка" UniqueName="PortalLink" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hlPortal" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn6" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" Width="70px" HorizontalAlign="Center" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hlEdit" Text="Редактировать" ImageUrl="~/App_Themes/Default/images/icoView.png" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
    </Columns>    
</labitec:Grid>