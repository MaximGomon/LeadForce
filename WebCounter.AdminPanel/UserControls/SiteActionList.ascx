<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActionList.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActionList" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsSiteAction" GridControlID="gridSiteAction" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                <ContentTemplate>
                    <labitec:Filters ID="filtersSiteAction" GridControlID="gridSiteAction" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" ID="ucLeftColumn" />
</div>

<labitec:Grid ID="gridSiteAction" AccessCheck="true" OnItemDataBound="gridSiteAction_OnItemDataBound" Fields="tbl_SiteActionTemplate.ID, tbl_Contact.ID, tbl_Contact.CreatedAt, c1.ID, c1.CreatedAt, c2.ID, c2.UserFullName, tbl_SourceMonitoring.Name, tbl_SourceMonitoring.ID" TableName="tbl_SiteAction" TagsControlID="tagsSiteAction" FiltersControlID="filtersSiteAction" ClassName="WebCounter.AdminPanel.SiteAction" Export="true" runat="server">
    <Columns>
        <labitec:GridColumn DataField="DirectionID" HeaderText=" " AllowFiltering="false" HorizontalAlign="Center" runat="server">
            <ItemTemplate>
                <asp:Image runat="server" ID="imgDirection" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn DataField="MessageTitle" HeaderText="Тема" Width="100px" runat="server"/>
        <labitec:GridColumn ID="gcActionTemplate" DataField="tbl_SiteActionTemplate.Title" HeaderText="Название шаблона" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hlActionTemplateTitle" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn DataField="tbl_ActionTypes.Title" HeaderText="Тип сообщения" runat="server"/>
        <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Получатель" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lUserFullName" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn> 
        <labitec:GridColumn DataField="c1.UserFullName" HeaderText="Отправитель" Width="100px" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lSenderUserFullName" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn DataField="tbl_ActionStatus.Title" HeaderText="Статус" runat="server"/>
        <labitec:GridColumn DataField="ActionDate" HeaderText="Дата действия" DataType="DateTime" runat="server"/>
        <labitec:GridColumn DataField="Comments" HeaderText="Комментарий" Width="200px" runat="server"/>
    </Columns>
    <Joins>
        <labitec:GridJoin JoinTableName="tbl_SiteActionTemplate" JoinTableKey="ID" TableKey="SiteActionTemplateID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_ActionTypes" JoinTableKey="ID" TableName="tbl_SiteActionTemplate" TableKey="ActionTypeID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableKey="ContactID" runat="server" />        
        <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableAs="c1" JoinTableKey="ID" TableKey="SenderID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_ActionStatus" JoinTableKey="ID" TableKey="ActionStatusID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_SourceMonitoring" JoinTableKey="ID" TableKey="SourceMonitoringID" runat="server" />        
        <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableAs="c2" JoinTableKey="ID" TableName="tbl_SourceMonitoring" TableKey="ReceiverContactID" runat="server" />
    </Joins>
</labitec:Grid>