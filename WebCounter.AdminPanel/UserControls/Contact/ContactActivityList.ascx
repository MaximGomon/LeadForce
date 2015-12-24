<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactActivityList.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ContactActivityList" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="PeriodFilter" Src="~/UserControls/Shared/PeriodFilter.ascx" %>

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsContactActivity" GridControlID="gridContactActivity" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                <ContentTemplate>
                    <labitec:Filters ID="filtersContactActivity" GridControlID="gridContactActivity" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Expanded="true" Text="Фильтр по датам">
                <ContentTemplate>
                    <uc:PeriodFilter runat="server" ID="ucPeriodFilter" />
                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" ID="ucLeftColumn" />
</div>


<labitec:Grid ID="gridContactActivity" OnItemDataBound="gridContactActivity_OnItemDataBound" Fields="tbl_ContactActivity.ID, tbl_Contact.ID, tbl_Contact.UserFullName, tbl_Contact.CreatedAt, ActivityCode, ActivityTypeID, ContactSessionID, tbl_ContactSessions.UserSessionNumber" TableName="tbl_ContactActivity" AccessCheck="true" TagsControlID="tagsContactActivity" FiltersControlID="filtersContactActivity" ClassName="WebCounter.AdminPanel.ContactActivity" Export="true" runat="server">
    <Columns>
        <labitec:GridColumn DataField="tbl_ContactSessions.UserSessionNumber" HeaderText="Сессия" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnShowSessionInfo" EnableViewState="false" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата действия" DataType="DateTime" runat="server"/>
        <labitec:GridColumn DataField="tbl_ActivityTypes.Title" HeaderText="Тип" runat="server"/>
        <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Посетитель" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lContact" EnableViewState="false" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn DataField="ActivityCode" HeaderText="Описание" runat="server">
            <ItemTemplate>
                <asp:Literal ID="lActivityCode" EnableViewState="false" runat="server" />
            </ItemTemplate>
        </labitec:GridColumn>
    </Columns>
    <Joins>
        <labitec:GridJoin JoinTableName="tbl_ActivityTypes" JoinTableKey="ID" TableKey="ActivityTypeID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableKey="ContactID" runat="server" />
        <labitec:GridJoin JoinTableName="tbl_ContactSessions" JoinTableKey="ID" TableKey="ContactSessionID" runat="server" />
    </Joins>
</labitec:Grid>

<telerik:RadToolTipManager ID="rttmSessionInfo" EnableEmbeddedScripts="true" ShowEvent="OnClick" OffsetY="-1" HideEvent="ManualClose" Modal="true"
    runat="server" EnableShadow="true" ManualCloseButtonText="Закрыть" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="Element"
    Position="MiddleRight">                                
</telerik:RadToolTipManager>