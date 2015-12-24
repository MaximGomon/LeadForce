<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActionLinkUsers.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActionLinkUsers" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <telerik:RadGrid ID="rgSiteActionLinkUsers" OnItemDataBound="rgSiteActionLinkUsers_ItemDataBound" DataKeyNames="ID" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="10" GridLines="None" Width="700px" runat="server">
            <MasterTableView>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Посетитель" HeaderStyle-Width="300px" DataField="tbl_Contact.UserFullName" UniqueName="tbl_Contact.UserFullName" SortExpression="tbl_Contact.UserFullName">
                        <ItemTemplate>
                            <asp:Literal ID="litContact" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn HeaderText="Дата отправки сообщения" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" DataField="tbl_SiteAction.ActionDate" DataFormatString="{0:dd.MM.yyyy HH:mm}" UniqueName="tbl_SiteAction.ActionDate" SortExpression="tbl_SiteAction.ActionDate" />
                    <telerik:GridDateTimeColumn HeaderText="Дата перехода по ссылке" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" DataField="tbl_SiteAction.ResponseDate" DataFormatString="{0:dd.MM.yyyy HH:mm}" UniqueName="tbl_SiteAction.ResponseDate" SortExpression="tbl_SiteAction.ResponseDate" />
                </Columns>
                <PagerStyle Mode="NumericPages" ShowPagerText="false" />
            </MasterTableView>
        </telerik:RadGrid>
    </ContentTemplate>
</asp:UpdatePanel>