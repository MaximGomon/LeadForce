<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupSiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.PopupSiteActionTemplate" %>
<%@ Register TagPrefix="uc" TagName="SettingsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SettingsSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="EditorSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/EditorSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SelectSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="StatsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/StatsSiteActionTemplate.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">        
        function <%= this.ClientID %>_ShowSiteActionTemplateRadWindow(id) {
            $find('<%= radAjaxPanel.ClientID %>').ajaxRequest(id);

            var oWnd = $find("<%= rwSiteActionTemplate.ClientID %>");
            oWnd.show();
            //oWnd.moveTo(Math.round((($(document).width() - oWnd.get_width) / 2)), 10);
            oWnd.center();

            <%= ((WebCounter.AdminPanel.UserControls.Shared.HtmlEditor)ucEditorSiteActionTemplate.FindControl("ucHtmlEditor")).ClientID %>_setupEditor();
        }
        
        function <%= this.ClientID %>_CloseSiteActionTemplateRadWindow() {
            $find('<%= rwSiteActionTemplate.ClientID %>').close();
        }
        
        function <%= this.ClientID %>_OnClientClose() {
            if ($('#silverlightControlHost').length != 0)
                $('#silverlightControlHost').css('height', '600px');
            /*if ($('#silverlightMapConversionHost').length != 0)
                $('#silverlightMapConversionHost').css('height', '600px');*/
        }

        function <%= this.ClientID %>_OnResponseEnd(sender, args) {
            <%= ((WebCounter.AdminPanel.UserControls.Shared.HtmlEditor)ucEditorSiteActionTemplate.FindControl("ucHtmlEditor")).ClientID %>_setupEditor();
            <%= this.ClientID %>_AutoHeightRadWindow();
        }

        function <%= this.ClientID %>_AutoHeightRadWindow() {
            setTimeout('<%= this.ClientID %>_AutoHeightTimeout();', 0);
        }
        
        function <%= this.ClientID %>_AutoHeightRadWindowWithFocus(autoCompleteBoxClientId) {
            setTimeout('<%= this.ClientID %>_AutoHeightTimeout("' + autoCompleteBoxClientId + '");', 0);
        }
        
        function <%= this.ClientID %>_AutoHeightTimeout(autoCompleteBoxClientId) {
            var oWnd = $find("<%= rwSiteActionTemplate.ClientID %>");
            oWnd.set_height($("#<%= pnlRadWindow.ClientID %>").height() + 50);
            if (autoCompleteBoxClientId != null)
                $("#" + autoCompleteBoxClientId + "_Input").focus();
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
        <asp:HyperLink ID="hlGoToTemplate" Text="Перейти к шаблону &rarr;" target="_blank" runat="server" CssClass="goto-template" Visible="False" />
        <asp:TextBox ID="txtSiteActionTemplateId" CssClass="hidden" runat="server" />
        <asp:RequiredFieldValidator ID="rfvTemplateValidator" ControlToValidate="txtSiteActionTemplateId" runat="server">*</asp:RequiredFieldValidator>
    </asp:Panel>
    </span>
</asp:Panel>

<telerik:RadWindow ID="rwSiteActionTemplate" runat="server" Title="Шаблон сообщения" Width="985px" Height="480px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <asp:Panel ID="pnlRadWindow" CssClass="radwindow-popup-inner siteaction-template-popup" runat="server">
            <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server">
	            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						            CssClass="validation-summary"
						            runat="server"
						            EnableClientScript="true"
						            HeaderText="Заполните все поля корректно:"
						            ValidationGroup="siteActionTemplateUpdate" />

                <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
                    <Tabs>
                        <telerik:RadTab Text="Шаблон сообщения" Value="Template" />
                        <telerik:RadTab Text="Настройки сообщения" />
                        <telerik:RadTab Text="Статистика откликов" Value="stats" />
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        
                            <telerik:RadPanelBar ID="rpbTemplateMessage" ClientIDMode="Static" ExpandAnimation-Type="None" CollapseAnimation-Type="None" ExpandMode="SingleExpandedItem" Skin="Windows7" Width="935px" CssClass="panelbar" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Expanded="True" Text="Выбор шаблона" Value="SelectTemplate">
                                        <ContentTemplate>
                                            <div class="panelbar-padding">
                                                <uc:SelectSiteActionTemplate ID="ucSelectSiteActionTemplate" OnSelectedChanged="ucSelectSiteActionTemplate_OnSelectedChanged" runat="server" />
                                                <%--<div class="row">
                                                    <label>Загрузить шаблон:</label>
                                                    <asp:RadioButtonList ID="rblActionTemplate" ClientIDMode="AutoID" AutoPostBack="True" OnSelectedIndexChanged="rblActionTemplate_OnSelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server" />
                                                </div>
                                                <telerik:RadRotator ID="rrThumbnails" OnItemDataBound="rrThumbnails_OnItemDataBound" RotatorType="Buttons" WrapFrames="False" CssClass="siteactiontemplate-rotator" Skin="Windows7" Width="904px" Height="144px" ItemWidth="144px" ItemHeight="133px" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbThumbnail" OnCommand="lbThumbnail_OnCommand" CssClass="item" runat="server">
                                                            <asp:Image ID="imgThumbnail" runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:RadRotator>--%>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                    <telerik:RadPanelItem Expanded="True" Text="Шаблон сообщения" Value="TemplateMessage">
                                        <ContentTemplate>
                                            <div class="panelbar-padding">
                                                <uc:EditorSiteActionTemplate ID="ucEditorSiteActionTemplate" ValidationGroup="siteActionTemplateUpdate" EnableClientScript="False" runat="server" />
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelBar>
                    </telerik:RadPageView>
        
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <uc:SettingsSiteActionTemplate ID="ucSettingsSiteActionTemplate" ValidationGroup="siteActionTemplateUpdate" EnableClientScript="False" runat="server" />
                    </telerik:RadPageView>
        
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <uc:StatsSiteActionTemplate ID="ucStatsSiteActionTemplate" runat="server" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                
                <br />
                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                    <div class="buttons clearfix">
                        <asp:LinkButton ID="lbSendTest" CssClass="btn" OnClick="lbSendTest_OnClick" ValidationGroup="siteActionTemplateUpdate" runat="server"><em>&nbsp;</em><span>Протестировать</span></asp:LinkButton>
			            <asp:LinkButton ID="lbtnSave" CssClass="btn" OnClientClick="$(this).find('span').text('Подождите');" OnClick="lbtnSave_OnClick" ValidationGroup="siteActionTemplateUpdate" CausesValidation="true" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <a href="javascript:;" class="cancel" onclick='<%= this.ClientID + "_CloseSiteActionTemplateRadWindow();" %>'>Отмена</a>
                    </div>
                </telerik:RadCodeBlock>
            </telerik:RadAjaxPanel>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>