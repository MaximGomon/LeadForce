<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActionTemplate" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function <%= this.ClientID %>_ShowSiteActionTemplateRadWindow(id) {
            $find('<%= RadAjaxManager.GetCurrent(Page).ClientID %>').ajaxRequest(id);
            var rtsTabs = $find("<%= rtsTabs.ClientID %>");
            var multiPage = $find("<%=RadMultiPage1.ClientID %>");
            if (id != '<%= Guid.Empty %>') {multiPage.set_selectedIndex(1);rtsTabs.get_allTabs()[1].select();}
            else { multiPage.set_selectedIndex(0);rtsTabs.get_allTabs()[0].select();}
            $find('<%= rwSiteActionTemplate.ClientID %>').show();
        }
        function <%= this.ClientID %>_OnClientClose() {
            if ($('#silverlightControlHost').length != 0)
                $('#silverlightControlHost').css('height', '600px');
            if ($('#silverlightMapConversionHost').length != 0)
                $('#silverlightMapConversionHost').css('height', '600px');
        }
        function <%= this.ClientID %>_CloseSiteActionTemplateRadWindow() {
            $find('<%= rwSiteActionTemplate.ClientID %>').close();
        }
        function <%= this.ClientID %>_IsEmpty() {
            if ($('#txtTitle').val() && $('#txtTitle').val() != '') return false;            
            if ($('#txtMessageCaption').val() && $('#txtMessageCaption').val() != '') return false;
            if ($('#txtFromEmail').val() && $('#txtFromEmail').val() != '') return false;
            if ($('#txtFromName').val() && $('#txtFromName').val() != '') return false;
            if ($('#txtReplyEmail').val() && $('#txtReplyEmail').val() != '') return false;
            if ($('#txtReplyName').val() && $('#txtReplyName').val() != '') return false;
            if ($('#txtReplyName').val() && $('#txtReplyName').val() != '') return false;
            if ($('#txtToEmail').val() && $('#txtToEmail').val() != '') return false;            
            return true;
        }
        function <%= this.ClientID %>_rblNewTemplateOnClientSelectedIndexChanging(sender, args) {            
            if (args.get_item().get_value() == "Empty" && !<%= this.ClientID %>_IsEmpty()) {
                if (!confirm("Очистить шаблон?")) { args.set_cancel(true); sender.set_autoPostBack(false); } 
                else { args.set_cancel(false); sender.set_autoPostBack(true);}
            } else { args.set_cancel(false); sender.set_autoPostBack(true); }
        }

        function <%= this.ClientID %>_WorkflowCallback(id, name) {
            GetSiteActionTemplate(id, name);
            $find('<%= rwSiteActionTemplate.ClientID %>').close();
        }
    </script>
</telerik:RadScriptBlock>


<asp:Panel ID="pnlSiteActionTemplate" CssClass="row row-siteaction-template clearfix" runat="server">
    <asp:Literal runat="server" ID="lrlLabel" />
    <span style="float:left">
    <asp:Panel runat="server" ID="plLinks">
        <asp:LinkButton ID="lbtnSiteActionTemplate" runat="server" />&nbsp;
        <asp:HyperLink ID="hlGoToTemplate" Text="Перейти к шаблону &rarr;" target="_blank" runat="server" CssClass="goto-template" />
        <asp:TextBox ID="txtSiteActionTemplateId" CssClass="hidden" runat="server" />
        <asp:RequiredFieldValidator ID="rfvTemplateValidator" ControlToValidate="txtSiteActionTemplateId" runat="server">*</asp:RequiredFieldValidator>
    </asp:Panel>
    </span>
</asp:Panel>

<telerik:RadWindow runat="server" Title="Шаблон сообщения" Width="1000px" Height="450px" ID="rwSiteActionTemplate" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>              
        <div class="radwindow-popup-inner siteaction-template">            
                <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			        <Tabs>
				        <telerik:RadTab Text="Новый шаблон" />
                        <telerik:RadTab Text="Основные данные" />
                        <telerik:RadTab Text="Шаблон сообщения" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <div class="new-template">
                            <table>
                                <tr>
                                    <td style="width: 150px;vertical-align: top">
                                        <telerik:RadListBox runat="server" ID="rlbNewTemplate" AutoPostBack="true" Skin="Windows7" OnSelectedIndexChanged="rlbNewTemplate_OnSelectedIndexChanged">
                                            <Items>
                                                <telerik:RadListBoxItem runat="server" Text="Пустой шаблон" Value="Empty" />
                                                <telerik:RadListBoxItem runat="server" Text="Скопировать шаблон" Value="Copy" />
                                                <telerik:RadListBoxItem runat="server" Text="На основе шаблона" Value="BaseOn" />
                                                <telerik:RadListBoxItem runat="server" Text="Выбрать шаблон" Value="Template" />                                                
                                            </Items>
                                        </telerik:RadListBox>
                                    </td>
                                    <td style="width: 805px;vertical-align: top">
                                        <labitec:Grid ID="gridSiteActionTemplates" ModuleName="SiteActionTemplates" Visible="false" AccessCheck="true" PageSize="5" OnItemDataBound="gridSiteActionTemplates_OnItemDataBound" Toolbar="false" TableName="tbl_SiteActionTemplate" ClassName="WebCounter.AdminPanel.SelectSiteActionTemplate" runat="server">
                                            <Columns>
                                                <labitec:GridColumn ID="GridColumn1" DataField="Title" HeaderText="Название шаблона сообщения" runat="server"/>
                                                <labitec:GridColumn ID="GridColumn2" DataField="tbl_ActionTypes.Title" HeaderText="Тип сообщения" runat="server"/>
                                                <labitec:GridColumn ID="GridColumn3" DataField="MessageCaption" HeaderText="Тема сообщения" runat="server"/>
                                                <labitec:GridColumn ID="GridColumn4" HorizontalAlign="Center" DataField="ID" HeaderText="Действие" runat="server">
                                                    <ItemTemplate>                                                   
                                                        <asp:LinkButton runat="server" Text="Выбрать" ID="lbtnSelect" OnClick="lbtnSelect_OnClick" />
                                                        <asp:Image runat="server" ImageUrl="~/App_Themes/Default/images/btnOk.png" Visible="false" Height="16px" ID="imgOk" />
                                                    </ItemTemplate>
                                                </labitec:GridColumn>
                                            </Columns>
                                            <Joins>
                                                <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ActionTypes" JoinTableKey="ID" TableKey="ActionTypeID" runat="server" />
                                            </Joins>
                                        </labitec:Grid>
                                    </td>
                                </tr>                                
                            </table>
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <asp:Panel runat="server" ID="plSiteActionTemplateInfo">
                            <div class="left-column">
                                <div class="row">
                                    <label>Категория шаблона:</label>
                                    <asp:DropDownList runat="server" ID="ddlSiteTemplateCategory" Enabled="false" CssClass="select-text" />
                                </div>
                                <asp:Panel runat="server" ID="plNotBase" Visible="false">
                                    <div class="row">
                                        <label>Базовый шаблон:</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbParentTemplate" DictionaryName="tbl_SiteActionTemplate" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server" />
                                    </div>
                                    <div class="row">
                                        <label>Где используется:</label>
                                        <asp:HyperLink runat="server" ID="hlUsage" target="_blank" />
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <label>Название шаблона сообщения:</label>
                                    <asp:TextBox ID="txtTitle" ClientIDMode="Static" Text='' CssClass="input-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" ValidationGroup="siteActionTemplateUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <label>Тип сообщения:</label>
                                    <asp:DropDownList ID="ddlActionType" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlActionType_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlActionType" ValidationGroup="siteActionTemplateUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <label>Подменять ссылки:</label>
                                    <asp:DropDownList ID="ddlReplaceLinks" ClientIDMode="AutoID" CssClass="select-text" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="right-column">
                                <asp:Panel ID="pToEmail" Visible="false" runat="server">
                                    <div class="row">
                                        <label>Email получателя:</label>
                                        <asp:TextBox ID="txtToEmail" Text='' ClientIDMode="Static" CssClass="input-text" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtToEmail" ValidationGroup="siteActionTemplateUpdate" runat="server">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtToEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="siteActionTemplateUpdate" runat="server" />
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <label>Email отправителя:</label>
                                    <asp:TextBox ID="txtFromEmail" Text='' ClientIDMode="Static" CssClass="input-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtFromEmail" ValidationGroup="siteActionTemplateUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtFromEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="siteActionTemplateUpdate" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Имя отправителя:</label>
                                    <asp:TextBox ID="txtFromName" Text='' ClientIDMode="Static" CssClass="input-text" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Email для ответа:</label>
                                    <asp:TextBox ID="txtReplyEmail" Text='' ClientIDMode="Static" CssClass="input-text" runat="server" />                            
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtReplyEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="siteActionTemplateUpdate" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Имя для ответа:</label>
                                    <asp:TextBox ID="txtReplyName" Text='' ClientIDMode="Static" CssClass="input-text" runat="server" />
                                </div>                                
                            </div>
                            <div class="clear"></div>
                            <div class="row">
                                    <label>Тема сообщения:</label>
                                    <asp:TextBox ID="txtMessageCaption" Text='' ClientIDMode="Static" CssClass="input-text" Width="690px" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtMessageCaption" ValidationGroup="siteActionTemplateUpdate" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <div class="clearfix">
                            <div class="html-editor">
                                <uc:HtmlEditor runat="server" ID="ucHtmlEditor" Content='' Height="250px" Module="SiteActionTemplates" />
                            </div>
                            <div class="html-legend">
                                Редактор поддерживает специальные теги:<br />
                                - Вывод реквизита посетителя #User.Код поля 1#
                                <asp:Repeater ID="rUserColumnValues" runat="server">
                                    <HeaderTemplate><span>(#User.UserFullName# #User.LastName# #User.FirstName# #User.MiddleName# #User.Email# #User.Phone# #User.Score# #Text# #Unsubscribe# #Advert#</HeaderTemplate>
                                    <ItemTemplate>
                                        #User.<%# Eval("Code") %>#
                                    </ItemTemplate>
                                    <FooterTemplate>)</span></FooterTemplate>
                                </asp:Repeater>
                                <br />
                                - Вывод ссылки #Link.Код ссылки 1#
                            </div>
                        </div>                            
                    </telerik:RadPageView>
                </telerik:RadMultiPage>   
                <div class="clear"></div>
                <br/>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                    <telerik:RadCodeBlock runat="server">
                        <div class="buttons clearfix">                        
			                <asp:LinkButton ID="lbtnSave" CssClass="btn" OnClientClick="$(this).find('span').text('Подождите')" OnClick="lbtnSave_OnClick" ValidationGroup="siteActionTemplateUpdate" CausesValidation="true" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                            <a href="javascript:;" class="cancel" onclick='<%= this.ClientID + "_CloseSiteActionTemplateRadWindow();" %>'>Отмена</a>
                        </div>
                    </telerik:RadCodeBlock>
                </telerik:RadAjaxPanel>
        </div>
    </ContentTemplate>
</telerik:RadWindow>