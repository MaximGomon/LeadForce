<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActivityRules.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActivityRules" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<script type="text/javascript">
    function openRadWindow(code) {
        var oWnd = radopen("/FormCode.aspx?code=" + code, "RadWindow1");
    }
</script>
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <labitec:Grid ID="gridSiteActivityRules" AccessCheck="true" Fields="RuleTypeID" OnItemDataBound="gridSiteActivityRules_OnItemDataBound" TableName="tbl_SiteActivityRules" ClassName="WebCounter.AdminPanel.SiteActivityRules" runat="server">
                <Columns>
                    <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название правила" runat="server"/>
                    <labitec:GridColumn ID="GridColumn2" DataField="tbl_RuleTypes.Title" HeaderText="Тип правила" runat="server"/>
                    <labitec:GridColumn ID="GridColumn3" DataField="Code" HeaderText="Код" runat="server"/>
                    <labitec:GridColumn ID="GridColumn4" DataField="Code" UniqueName="SubstitutionCode" HeaderText="Код для подстановки" runat="server">
                        <ItemTemplate>
                            <asp:Literal ID="lSubstitutionCode" runat="server" />
                            <a id="aGetCode" href="#" Visible="False" runat="server">Получить код</a>
                        </ItemTemplate>
                    </labitec:GridColumn>
                    <labitec:GridColumn ID="GridColumn5" DataField="URL" HeaderText="Ссылка" runat="server"/>
                    <labitec:GridColumn ID="GridColumn6" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" Width="70px" HorizontalAlign="Center" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" Text="Редактировать" ImageUrl="~/App_Themes/Default/images/icoView.png" runat="server" />
                        </ItemTemplate>
                    </labitec:GridColumn>
                </Columns>
                <Joins>
                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RuleTypes" JoinTableKey="ID" TableKey="RuleTypeID" runat="server" />
                </Joins>
            </labitec:Grid>

            <telerik:RadWindowManager ID="RadWindowManager1" Width="900px" Height="650px" Title="Получить код" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup radwindow-popup-inner" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" runat="server" />
        </td>
    </tr>
</table>