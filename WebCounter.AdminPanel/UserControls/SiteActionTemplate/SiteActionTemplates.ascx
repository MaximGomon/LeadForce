<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActionTemplates.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActionTemplates" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>          
                <telerik:RadPanelItem Expanded="true" Text="Фильтр по шаблонам">
                    <ContentTemplate>
                        <div class="dropdown-filter">
                            <asp:DropDownList runat="server" CssClass="select-text" ID="ddlFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_OnSelectedIndexChanged">
                                <asp:ListItem Text="Базовые шаблоны" Value="0"/>
                                <%--<asp:ListItem Text="Для рассылок" Value="1"/>
                                <asp:ListItem Text="Для событий" Value="2"/>
                                <asp:ListItem Text="Для процессов" Value="3"/>--%>
                                <asp:ListItem Text="Системные" Value="4"/>
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
</div>
<div class="grid-container-margin">
    <labitec:Grid ID="gridSiteActionTemplates" TableName="tbl_SiteActionTemplate" AccessCheck="true" ClassName="WebCounter.AdminPanel.SiteActionTemplates" runat="server">
        <Columns>
            <labitec:GridColumn DataField="Title" HeaderText="Название шаблона сообщения" runat="server"/>
        </Columns>
        <Joins>
            <labitec:GridJoin JoinTableName="tbl_ActionTypes" JoinTableKey="ID" TableKey="ActionTypeID" runat="server" />
        </Joins>
    </labitec:Grid>
</div>