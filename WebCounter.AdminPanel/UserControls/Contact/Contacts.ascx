<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contacts.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Contacts" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsContacts" GridControlID="gridContacts" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                <ContentTemplate>
                    <labitec:Filters ID="filtersContacts" GridControlID="gridContacts" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>                
        </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" ID="ucLeftColumn" />
</div>

<labitec:Grid ID="gridContacts" TableName="tbl_Contact" Fields="tbl_Priorities.Image, tbl_ReadyToSell.Image" AccessCheck="true" TagsControlID="tagsContacts" FiltersControlID="filtersContacts" ClassName="WebCounter.AdminPanel.Contacts" OnItemDataBound="gridContacts_OnItemDataBound" Export="true" RememberSelected="True" ShowDeleteButton="True" ShowRestoreButton="True" OnDeleteAll="gridContacts_OnDeleteAll" OnRestoreAll="gridContacts_OnRestoreAll" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
        <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
        <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
        <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
        <labitec:GridColumn ID="GridColumn5" DataField="tbl_Company.Name" HeaderText="Компания" runat="server"/>
        <labitec:GridColumn ID="GridColumn6" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server">
            <ItemTemplate>
                <asp:Literal ID="litReadyToSell" runat="server" />
                <asp:Image ID="imgReadyToSell" Visible="False" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn11" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server">
            <ItemTemplate>
                <asp:Literal ID="litPriority" runat="server" />
                <asp:Image ID="imgPriority" Visible="False" runat="server"/>
            </ItemTemplate>
        </labitec:GridColumn>        
        <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
        <labitec:GridColumn DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
            <ItemTemplate>
                <div style="text-align: left; padding-left: 15px;"><asp:HyperLink ID="hlEdit" CssClass="smb-action" style="white-space: nowrap" runat="server"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:HyperLink></div>
                <div style="text-align: left; padding-left: 15px;"><asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="Action_OnCommand" CommandName="Delete" CssClass="smb-action" style="white-space: nowrap" Visible="False" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server"/><span style="padding-left: 3px">Удалить</span></asp:LinkButton></div>
                <div style="text-align: left; padding-left: 15px;"><asp:LinkButton ID="lbRestore" OnClientClick="return confirm('Вы действительно хотите восстановить запись?');" OnCommand="Action_OnCommand" CommandName="Restore" CssClass="smb-action" style="white-space: nowrap" Visible="False" runat="server"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Default/images/icoRestore.png" AlternateText="Восстановить" ToolTip="Восстановить" runat="server"/><span style="padding-left: 3px">Восстановить</span></asp:LinkButton></div>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn9" DataField="IsNameChecked" HeaderText="Корректность имени" DataType="Boolean" Visible="false" runat="server"/>
        <labitec:GridColumn ID="GridColumn10" DataField="EmailStatusID" HeaderText="Статус Email" Visible="false" runat="server"/>
    </Columns>
    <Joins>
        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />        
        <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_Company" JoinTableKey="ID" TableKey="CompanyID" runat="server" />
    </Joins>
</labitec:Grid>