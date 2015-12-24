<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsSiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SettingsSiteActionTemplate" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectContacts" Src="~/UserControls/Contact/SelectContacts.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function <%= this.ClientID %>_ShowRecipientRadWindow() {
            $find("<%= rwRecipient.ClientID %>").show();
        }
        
        function <%= this.ClientID %>_CloseRecipientRadWindow() {
            $find("<%= rwRecipient.ClientID %>").close();
        }
        
        function <%= this.ClientID %>_AutoHeight() {
            setTimeout('<%= this.ClientID %>_AutoHeightTimeout();', 0);
        }
        
        function <%= this.ClientID %>_AutoHeightTimeout() {
            var oWnd = $find("<%= rwRecipient.ClientID %>");
            oWnd.set_height($("#<%= pnlRecipientRadWindow.ClientID %>").height() + 50);
            $("#<%= racbRecipientsPopup.ClientID %>_Input").focus();
        }
        

        function <%= this.ClientID %>_ShowRoleDictonaryRadWindow() {
            $find("<%= rwRoleDictonary.ClientID %>").show();
        }
        
        function <%= this.ClientID %>_CloseRoleDictonaryRadWindow() {
            $find("<%= rwRoleDictonary.ClientID %>").close();
        }
        
        function <%= this.ClientID %>_AutoHeightRoleDictonary() {
            setTimeout('<%= this.ClientID %>_AutoHeightTimeoutRoleDictonary();', 0);
        }
        
        function <%= this.ClientID %>_AutoHeightTimeoutRoleDictonary() {
            var oWnd = $find("<%= rwRoleDictonary.ClientID %>");
            oWnd.set_height($("#<%= pnlRoleDictonaryRadWindow.ClientID %>").height() + 50);
        }

        function HideBadDropdown(s, a) {
            $('.racSlide').each(function() {
                if ($(this).position().top == 0 && $(this).position().left == 0)
                    $(this).hide();
            });
        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadWindow ID="rwRecipient" runat="server" Title="Получатели" Width="985px" Height="480px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <asp:Panel ID="pnlRecipientRadWindow" CssClass="radwindow-popup-inner siteaction-template-popup" runat="server">
            <table style="padding-bottom: 15px">
                <tr>
                    <td width="169px">Получатели:</td>
                    <td>
                        <telerik:RadAutoCompleteBox ID="racbRecipientsPopup" OnClientDropDownOpened="HideBadDropdown" InputType="Token" AllowCustomEntry="True" Filter="Contains" Delimiter=";" Skin="Windows7" Width="690px" DropDownWidth="690px" DropDownHeight="300px" OnEntryAdded="racbRecipientsPopup_OnEntryAdded" OnEntryRemoved="racbRecipientsPopup_OnEntryRemoved" runat="server">
                            <WebServiceSettings Method="GetData" Path="~/Handlers/AutoCompleteBoxContactRole.aspx" />
                        </telerik:RadAutoCompleteBox>
                    </td>
                </tr>
            </table>
            
	        <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		        <Tabs>
			        <telerik:RadTab Text="Стандартный адрес" />
			        <telerik:RadTab Text="Произвольный адрес" />
		        </Tabs>
	        </telerik:RadTabStrip>
            
	        <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		        <telerik:RadPageView ID="RadPageView1" runat="server">
                    <div class="row">
                        <label>Роли:</label>
                        <uc:DictionaryOnDemandComboBox ID="dcbContactRolePopup" Width="234px" DictionaryName="tbl_ContactRole" DataTextField="Title" CssClass="select-text" ValidationGroup="vgRolePopup" runat="server" />
                        <telerik:RadButton ID="rbAddRole" OnClick="rbAddRole_OnClick" Text="Добавить" Skin="Windows7" ValidationGroup="vgRolePopup" runat="server" />
                    </div>
                </telerik:RadPageView>
		        <telerik:RadPageView ID="RadPageView2" runat="server">
                    <div class="row">
                        <label>Email:</label>
                        <asp:TextBox ID="txtEmailPopup" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtEmailPopup" ErrorMessage="Вы не ввели 'Email'" ValidationGroup="vgEmailPopup" runat="server">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtEmailPopup" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="vgEmailPopup" runat="server">*</asp:RegularExpressionValidator>
                    </div>
                    <div class="row">
                        <label>Имя:</label>
                        <asp:TextBox ID="txtDisplayNamePopup" CssClass="input-text" runat="server" />&nbsp;&nbsp;&nbsp;
                        <telerik:RadButton ID="rbAddCustom" OnClick="rbAddCustom_OnClick" Text="Добавить" Skin="Windows7" ValidationGroup="vgEmailPopup" runat="server" />
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
                
            <br />
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <div class="buttons clearfix">
		            <asp:LinkButton ID="lbtnSavePopup" OnClick="lbtnSavePopup_OnClick" CssClass="btn" CausesValidation="False" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                    <a href="javascript:;" class="cancel" onclick='<%= this.ClientID + "_CloseRecipientRadWindow();" %>'>Отмена</a>
                </div>
            </telerik:RadCodeBlock>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadWindow ID="rwRoleDictonary" runat="server" Title="Адресная книга" Width="985px" Height="480px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <asp:Panel ID="pnlRoleDictonaryRadWindow" CssClass="radwindow-popup-inner siteaction-template-popup" runat="server">
            <telerik:RadAjaxPanel runat="server">
                <uc:SelectContacts ID="ucSelectContacts" HideButton="True" runat="server" />
                
                <telerik:RadGrid ID="rgRoleDictonary" runat="server" GridLines="None" AutoGenerateColumns="False" AllowAutomaticUpdates="True" ShowStatusBar="true" Skin="Windows7"
                    DataSourceID="edsRoleDictonary" OnDeleteCommand="rgRoleDictonary_OnDeleteCommand" OnItemDataBound="rgRoleDictonary_OnItemDataBound">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="ID" DataSourceID="edsRoleDictonary">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Название" DataField="Title" />
                            <telerik:GridTemplateColumn HeaderText="Тип роли" DataField="RoleTypeID" UniqueName="RoleTypeID">
                                <ItemTemplate>
                                    <asp:Literal ID="litRoleType" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Описание" DataField="Description" />
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Удалить" ConfirmText="Вы действительно хотите удалить запись?" CommandName="Delete" ButtonType="ImageButton" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <EditFormSettings UserControlName="~/UserControls/DictionaryEditForm/ContactRoleEdit.ascx" EditFormType="WebUserControl" />
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:EntityDataSource ID="edsRoleDictonary" ConnectionString="name=WebCounterEntities" DefaultContainerName="WebCounterEntities" EntitySetName="tbl_ContactRole" EnableInsert="True" EnableUpdate="True" EnableDelete="True" runat="server" />

                <br />
                <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                    <div class="buttons clearfix">
		                <asp:LinkButton ID="lbRoleDictonarySave" OnClick="lbRoleDictonarySave_OnClick" CssClass="btn" CausesValidation="False" runat="server"><em>&nbsp;</em><span>Закрыть</span></asp:LinkButton>
                    </div>
                </telerik:RadCodeBlock>
            </telerik:RadAjaxPanel>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadAjaxPanel runat="server">
    <asp:Panel ID="pnlTitle" runat="server">
        <table width="912px">
            <tr>
                <td width="154px">
                    <div class="row">
                        <label style="width: 154px">Название шаблона:</label>
                    </div>
                </td>
                <td>
                    <div class="row">
                        <asp:TextBox ID="txtTitle" CssClass="input-text" Width="717px" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvTitle" ControlToValidate="txtTitle" ErrorMessage="Вы не ввели 'Название шаблона'" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <table width="912px">
        <tr>
            <td width="497px" colspan="2">
                <asp:Panel ID="pnlActionType" CssClass="row" runat="server">
                    <label>Тип сообщения:</label>
                    <asp:DropDownList ID="ddlActionType" ClientIDMode="AutoID" AutoPostBack="True" OnSelectedIndexChanged="ddlActionType_OnSelectedIndexChanged" CssClass="select-text" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvActionType" ControlToValidate="ddlActionType" runat="server">*</asp:RequiredFieldValidator>                                        
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlToEmail" CssClass="row" runat="server">
                    <table>
                        <tr>
                            <td width="169px" valign="top"><telerik:RadButton ID="rbRecipients" Text="Получатели:" OnClick="rbRecipients_OnClick" CausesValidation="False" Skin="Windows7" runat="server" /></td>
                            <td>
                                <telerik:RadAutoCompleteBox ID="racbRecipients" OnClientDropDownOpened="HideBadDropdown" InputType="Token" AllowCustomEntry="True" Filter="Contains" Delimiter=";" Skin="Windows7" Width="690px" DropDownWidth="690px" DropDownHeight="300px" OnEntryAdded="racbRecipients_OnEntryAdded" OnEntryRemoved="racbRecipients_OnEntryRemoved" runat="server">
                                    <WebServiceSettings Method="GetData" Path="~/Handlers/AutoCompleteBoxContactRole.aspx" />
                                </telerik:RadAutoCompleteBox>
                                <asp:RequiredFieldValidator ID="rfvRecipients" ControlToValidate="racbRecipients" Display="Dynamic" ErrorMessage="Вы не выбрали 'Получатели'" runat="server">*</asp:RequiredFieldValidator>
                                <%--<label>Email получателя:</label>--%>
                                <asp:TextBox ID="txtToEmail" CssClass="input-text" Visible="False" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvToEmail" Visible="False" ControlToValidate="txtToEmail" ErrorMessage="Вы не ввели 'Email получателя'" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revToEmail" ErrorMessage="Неверный формат Email." ControlToValidate="txtToEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" Display="Dynamic" runat="server">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="row">
                    <label>Отправитель:</label>
                    <uc:DictionaryOnDemandComboBox ID="dcbContactRole" AutoPostBack="True" Width="234px" DictionaryName="tbl_ContactRole" DataTextField="Title" ShowEmpty="True" EmptyItemText="Конкретные Email и имя" CssClass="select-text" OnSelectedIndexChanged="dcbContactRole_OnSelectedIndexChanged" OnItemsRequested="SettingsSiteActionTemplate_ItemsRequested" ValidationErrorMessage="Вы не выбрали 'Отправитель'" runat="server" />
                    <telerik:RadButton ID="rbRoleDictonay" Text="Адресная книга" Skin="Windows7" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <asp:Panel ID="pnlFrom" runat="server">
            <td>
                
                <div class="row">
                    <label>Email отправителя:</label>
                    <asp:TextBox ID="txtFromEmail" CssClass="input-text" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvFromEmail" Display="Dynamic" ControlToValidate="txtFromEmail" ErrorMessage="Вы не ввели 'Email отправителя'" runat="server">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revFromEmail" Display="Dynamic" ErrorMessage="Неверный формат Email." ControlToValidate="txtFromEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" runat="server">*</asp:RegularExpressionValidator>
                </div>
            </td>
            <td>
                <div class="row">
                    <label>Имя отправителя:</label>
                    <asp:TextBox ID="txtFromName" CssClass="input-text" runat="server" />
                </div>
            </td>
            </asp:Panel>
        </tr>
        <tr ID="trReplyEmailName" runat="server">
            <td>
                <div class="row">
                    <label>Email для ответа:</label>
                    <asp:TextBox ID="txtReplyEmail" CssClass="input-text" runat="server" />                            
                    <asp:RegularExpressionValidator ID="rfvReplyEmail" ErrorMessage="Неверный формат Email." ControlToValidate="txtReplyEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" runat="server" Display="Dynamic">*</asp:RegularExpressionValidator>
                </div>
            </td>
            <td>
                <div class="row">
                    <label>Имя для ответа:</label>
                    <asp:TextBox ID="txtReplyName" CssClass="input-text" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div class="row row-radiobuttonlist">
        <label>Отслеживать переходы по ссылкам:</label>
        <asp:RadioButtonList ID="rblReplaceLinks" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server" />
    </div>
</telerik:RadAjaxPanel>