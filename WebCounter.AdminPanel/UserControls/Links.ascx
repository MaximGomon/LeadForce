<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Links.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Links" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <labitec:Grid ID="gridLinks" AccessCheck="true" OnItemDataBound="gridLinks_OnItemDataBound" TableName="tbl_Links" ClassName="WebCounter.AdminPanel.Links" runat="server">
                <Columns>
                    <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название правила" runat="server"/>                    
                    <labitec:GridColumn ID="GridColumn3" DataField="Code" HeaderText="Код" runat="server"/>  
                    <labitec:GridColumn ID="GridColumn4" DataField="Code" UniqueName="SubstitutionCode" HeaderText="Код для подстановки" runat="server">
                        <ItemTemplate>
                            <asp:Literal ID="lSubstitutionCode" runat="server" />
                        </ItemTemplate>
                    </labitec:GridColumn>
                    <labitec:GridColumn ID="GridColumn5" DataField="URL" HeaderText="Ссылка" runat="server"/>
                    <labitec:GridColumn ID="GridColumn6" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" Width="70px" HorizontalAlign="Center" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" Text="Редактировать" ImageUrl="~/App_Themes/Default/images/icoView.png" runat="server" />
                        </ItemTemplate>
                    </labitec:GridColumn>
                </Columns>                
            </labitec:Grid>
        </td>
    </tr>
</table>