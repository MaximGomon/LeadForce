<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteDomains.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteSettings.SiteDomains" %>

<asp:Panel runat="server" ID="plContainer">    
        <asp:LinkButton ID="lbtnRegisterDomainsByActionLog" OnClick="lbtnRegisterDomainsByActionLog_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Зарегистрировать домены по логу действий</span></asp:LinkButton>
        <br/><br/>
        <h3>Домены</h3>
        <labitec:Grid ID="gridSiteDomains" OnItemDataBound="gridSiteDomains_OnItemDataBound" TableName="tbl_SiteDomain" ClassName="WebCounter.AdminPanel.SiteDomains" runat="server">
            <Columns>        
                <labitec:GridColumn ID="GridColumn2" DataField="Domain" HeaderText="Домен" runat="server"/>
                <labitec:GridColumn ID="GridColumn1" DataField="Aliases" HeaderText="Псевдонимы" runat="server"/>
                <labitec:GridColumn ID="GridColumn3" DataField="StatusID" HeaderText="Статус" runat="server">
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="lrlSiteDomainStatus" />
                    </ItemTemplate>
                </labitec:GridColumn>
                <labitec:GridColumn ID="GridColumn4" DataField="Note" HeaderText="Примечание" runat="server"/>
                <labitec:GridColumn ID="GridColumn6" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" Width="70px" HorizontalAlign="Center" runat="server">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" Text="Редактировать" ImageUrl="~/App_Themes/Default/images/icoView.png" runat="server" />
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="ibDelete" OnCommand="ibDelete_OnCommand" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" ToolTip="Удалить" AlternateText="Удалить" runat="server" />
                    </ItemTemplate>
                </labitec:GridColumn>                
            </Columns>    
        </labitec:Grid>    
</asp:Panel>