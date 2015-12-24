<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MassMail_old.aspx.cs" Inherits="WebCounter.AdminPanel.MassMail_old" %>
<%@ Register TagPrefix="uc" TagName="SiteActionLinkUsers" Src="~/UserControls/SiteActionLinkUsers.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SelectSiteActionTemplate.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //<![CDATA[
        function openTemplateWin()
        {
            var oWnd = radopen(null, "rwTemplate");
        }

        function openUsersWin() {
            var oWnd = radopen(null, "rwUsers");
        }

        function rwTemplate_OnClientClose(oWnd, args)
        {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                var siteActionTemplateId = arg.siteActionTemplateId;
                var siteActionTemplateTitle = arg.siteActionTemplateTitle;

                $('#btnSiteActionTemplate').text(siteActionTemplateTitle);
                $('#txtSiteActionTemplateId').val(siteActionTemplateId);
            }
        }

        function rwUsers_OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                var selected = arg.selected;
                if (selected) {
                    __doPostBack('ctl00$main$BtnRefresh', ''); // !!!
                }
            }
        }
        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">

<div class="aside">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="true" Text="Теги">
                <ContentTemplate>
                    <labitec:Tags ID="tagsMassMail" ObjectTypeName="tbl_MassMail" runat="server" />
                </ContentTemplate>
            </telerik:RadPanelItem>
        </Items>
    </telerik:RadPanelBar>
</div>

<div class="edit-panel">
    <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
        <Tabs>
            <telerik:RadTab Text="Основная информация" />
            <telerik:RadTab Text="Получатели" Value="tab-recipients" />
            <telerik:RadTab Text="Переходы по ссылкам" Value="tab-action-links" />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
        <telerik:RadPageView runat="server">
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                <Windows>
                    <telerik:RadWindow ID="rwTemplate" Width="700" AutoSize="true" AutoSizeBehaviors="Width,Height" Modal="true" Behaviors="Close" Localization-Close="Закрыть" VisibleStatusbar="false" OnClientClose="rwTemplate_OnClientClose" NavigateUrl="~/Handlers/MassMailSelectTemplate.aspx" runat="server"/>
                    <telerik:RadWindow ID="rwUsers" ReloadOnShow="true" Width="800" AutoSize="true" AutoSizeBehaviors="Width,Height" Modal="true" Behaviors="Close" Localization-Close="Закрыть" VisibleStatusbar="false" OnClientClose="rwUsers_OnClientClose" runat="server"/>
                </Windows>
            </telerik:RadWindowManager>
            
            <h3>Основная информация</h3>
            <asp:FormView ID="fvMassMail" OnItemCreated="fvMassMail_ItemCreated" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <label>Название рассылки:</label>
                        <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Дата рассылки:</label>
                        <%# Eval("MailDate") == null ? "" : ((DateTime)Eval("MailDate")).ToString("dd.MM.yyyy HH:mm")%>
                    </div>                    
                    <%--<uc:SiteActionTemplate runat="server" ID="ucSiteActionTemplate" CurrentSiteActionTemplateCategory="MassMail" ValidationGroup="valGroup" />--%>
                    <uc:SelectSiteActionTemplate ID="ucSelectSiteActionTemplate" CurrentSiteActionTemplateCategory="MassMail" ValidationGroup="valGroup" runat="server" />
                    <div class="row">
                        <label>Статус:</label>
                        <asp:DropDownList ID="ddlMassMailStatus" SelectedValue='<%# Eval("tbl_MassMailStatus.ID") ?? "" %>' CssClass="select-text" runat="server">
                            <asp:ListItem Text="" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMassMailStatus" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Фокус группа:</label>
                        <asp:TextBox ID="txtFocusGroup" Text='<%# Eval("FocusGroup") %>' CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFocusGroup" ValidationGroup="valFocusGroup" runat="server">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtFocusGroup" ErrorMessage="Неверный формат фокус группы." ValidationExpression="^[0-9]+$" ValidationGroup="valGroup" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:FormView>
            <asp:Panel ID="pButtons" Visible="true" runat="server">
                <div class="buttons">
                    <asp:LinkButton ID="BtnSendFocusGroup" OnClick="BtnSendFocusGroup_Click" ValidationGroup="valFocusGroup" CssClass="btn" style="margin-right: 10px;" runat="server"><em>&nbsp;</em><span>Разослать фокус группе</span></asp:LinkButton>
                    <asp:LinkButton ID="BtnSend" OnClick="BtnSend_Click" CssClass="btn" runat="server"><em>&nbsp;</em><span>Разослать</span></asp:LinkButton>
                    <div class="clear"></div>
                </div>
                <br /><br />

                <h3>Статистика рассылки</h3>
                <asp:UpdatePanel ID="upStats" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <label>Количество получателей:</label>
                            <asp:Literal ID="litRecipients" Text="0" runat="server" />
                        </div>
                        <div class="row">
                            <label>Выслано писем:</label>
                            <asp:Literal ID="litSending" Text="0" runat="server" />
                        </div>
                        <div class="row">
                            <label>Результат:</label>
                            <asp:Literal ID="litResponse" Text="0" runat="server" />
                        </div>
                        <div class="row">
                            <label>Конверсия:</label>
                            <asp:Literal ID="litConversion" Text="0%" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server">
            <asp:LinkButton ID="BtnAddUsers" CssClass="btn" OnClientClick="openUsersWin(); return false;" runat="server"><em>&nbsp;</em><span>Добавить</span></asp:LinkButton>
            <div class="clear"></div>
            <br />
            <asp:UpdatePanel ID="upUsers" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvContacts"
                                    DataKeyNames="ID" 
                                    runat="server" 
                                    OnPageIndexChanging="gvContacts_PageIndexChanging" 
                                    OnRowDeleting="gvContacts_RowDeleting"
                                    AutoGenerateColumns="False" 
                                    Width="100%" 
                                    AllowPaging="true" 
                                    PageSize="15" 
                                    OnSorting="gvContacts_Sorting"
                                    OnSorted="gvContacts_Sorted"
                                    CssClass="grid"
                                    GridLines="None"
                                    AllowSorting="true" 
                                    >    
                        <Columns>
                            <asp:TemplateField HeaderText="Ф.И.О." ItemStyle-ForeColor="#1272f2" HeaderStyle-CssClass="first" ItemStyle-CssClass="first" SortExpression="UserFullName">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("UserFullName") == null ? "" : Eval("UserFullName").ToString())%>                    
                                </ItemTemplate>          
                            </asp:TemplateField>        
                            <asp:TemplateField HeaderText="E-mail" SortExpression="Email">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("Email") == null ? "" : Eval("Email").ToString())%>                    
                                </ItemTemplate>        
                            </asp:TemplateField>         
                            <asp:TemplateField HeaderText="Готовность к продаже" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_ReadyToSell.Title">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("tbl_ReadyToSell.Title") == null ? "" : Eval("tbl_ReadyToSell.Title").ToString())%>                    
                                </ItemTemplate>        
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Важность" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_Priorities.Title">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("tbl_Priorities.Title") == null ? "" : Eval("tbl_Priorities.Title").ToString())%>                    
                                </ItemTemplate>        
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Статус" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" SortExpression="tbl_Status.Title">
                                <ItemTemplate>
                                    <%# Eval("tbl_Status.Title").ToString()%>               
                                </ItemTemplate>        
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Действия" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
                                <ItemTemplate>     
                                    <asp:LinkButton ID="BtnCalcelUser" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' Text="Отменить" runat="server" />     
                                </ItemTemplate>
                            </asp:TemplateField>      
                        </Columns>
                        <EmptyDataTemplate>
                            Нет данных.
                        </EmptyDataTemplate>
                        <SortedAscendingHeaderStyle CssClass="grid-asc" Font-Bold="true" />
                        <SortedDescendingHeaderStyle CssClass="grid-desc" Font-Bold="true" />
                        <SortedAscendingCellStyle BorderColor="Red" />
                        <PagerSettings Mode="Numeric" PreviousPageText="Предыдущая" NextPageText="Следующая" />
                        <PagerStyle CssClass="grid-pager" />
                    </asp:GridView>
                    <asp:LinkButton ID="BtnRefresh" OnClick="BtnRefresh_Click" Text="" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server">
            <asp:ListView ID="lvSiteActionLink" OnItemDataBound="lvSiteActionLink_ItemDataBound" runat="server">
                <LayoutTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="tbl-action-link">
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td valign="top">
                            <label>Ссылка:</label> <asp:HyperLink ID="hlActionLink" Target="_blank" runat="server" />
                        </td>
                        <td valign="top">
                            <label style="margin-left: 20px;">Количество переходов:</label> <asp:Literal ID="litCountConversions" Text="0" runat="server" />
                        </td>
                        <td valign="top">
                            <uc:SiteActionLinkUsers ID="ucSiteActionLinkUsers" runat="server" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <br />
                    Нет данных.
                    <br />
                </EmptyDataTemplate>
            </asp:ListView>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <br />

    <div class="buttons">
        <asp:LinkButton ID="BtnSave" OnClick="BtnSave_Click" CssClass="btn" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
    </div>
</div>
</asp:Content>