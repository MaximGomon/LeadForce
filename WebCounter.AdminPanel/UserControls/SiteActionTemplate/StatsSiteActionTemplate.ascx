<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatsSiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.StatsSiteActionTemplate" %>

<div class="row">
    <label>Выслано:</label> <asp:Literal ID="litSending" Text="0" runat="server" />
</div>
<div class="row">
    <label>Результат:</label> <asp:Literal ID="litResponse" Text="0" runat="server" />
</div>
<div class="row">
    <label>Конверсия:</label> <asp:Literal ID="litConversion" Text="0%" runat="server" />
</div>

<h3>Статистика переходов</h3>

<telerik:RadAjaxPanel runat="server">
    <telerik:RadGrid ID="rgStats"
                     AutoGenerateColumns="False"
                     PageSize="20"
                     AllowSorting="True"
                     AllowPaging="True"
                     OnDetailTableDataBind="rgStats_OnDetailTableDataBind"
                     OnNeedDataSource="rgStats_OnNeedDataSource"
                     OnItemDataBound="rgStats_OnItemDataBound"
                     Skin="Windows7"
                     runat="server">
        <MasterTableView DataKeyNames="SiteActivityRuleID,LinkURL">
            <DetailTables>
                <telerik:GridTableView Name="Detail" runat="server">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Посетитель" DataField="tbl_Contact.UserFullName" UniqueName="tbl_Contact.UserFullName" SortExpression="tbl_Contact.UserFullName">
                            <ItemTemplate>
                                <asp:Literal ID="litContact" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderText="Дата отправки сообщения" DataField="tbl_SiteAction.ActionDate" DataFormatString="{0:dd.MM.yyyy HH:mm}" SortExpression="tbl_SiteAction.ActionDate" />
                        <telerik:GridDateTimeColumn HeaderText="Дата перехода по ссылке" DataField="tbl_SiteAction.ResponseDate" DataFormatString="{0:dd.MM.yyyy HH:mm}" SortExpression="tbl_SiteAction.ResponseDate" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Ссылка" UniqueName="Link" SortExpression="LinkURL">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlLink" Target="_blank" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Количество переходов" DataField="Count" />
            </Columns>
            <PagerStyle Mode="NextPrevAndNumeric" ShowPagerText="False" />
        </MasterTableView>
    </telerik:RadGrid>
</telerik:RadAjaxPanel>