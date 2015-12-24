<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="WorkflowTemplate.aspx.cs" Inherits="WebCounter.AdminPanel.WorkflowTemplate" %>
<%@ Register Assembly="DirtyPageReminder" Namespace="DingJing" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateConditionEvent" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateConditionEvent.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateParameter" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateParameter.ascx" %>
<%--<%@ Register TagPrefix="uc" TagName="WorkflowTemplateElement" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateElement.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateElementRelation" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateElementRelation.ascx" %>--%>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateGoal" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateGoal.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateMaterial" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateMaterial.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <cc1:DirtyPageReminder ID="dprReminder" runat="server" AlertMessage="Вы действительно хотите покинуть страницу? Вы можете потерять часть ваших данных." />

    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <script src="<%#ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
    <script src="<%#ResolveUrl("~/Scripts/Silverlight.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var isSLLoaded = false;
        function SLLoaded() {
            $('#lbtnSave').removeClass("btn-disabled");
            isSLLoaded = true;
        }

        var prevStatusValue;
        $(document).ready(function () {
            prevStatusValue = $('#<%# ddlStatus.ClientID %>').val();
            $('#silverlightPageContainer').css('visibility', 'hidden');
            $('#silverlightMapConversion').css('visibility', 'hidden');
        });

        function ConfirmInPlans() {
            if ($('#<%# ddlStatus.ClientID %>').val() == '0') {
                if (confirm("При изменении статуса все процессы запущенные по данному шаблону будут удалены. Продолжить?")) {
                    __doPostBack('ctl00$LabitecPage$ctl02$ContentHolder$ddlStatus', ''); // !!!
                } else {
                    $('#<%# ddlStatus.ClientID %>').val(prevStatusValue);
                }
            }
            else
                __doPostBack('ctl00$LabitecPage$ctl02$ContentHolder$ddlStatus', ''); // !!!

            prevStatusValue = $('#<%# ddlStatus.ClientID %>').val();
        }




        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }

        function ShowSiteActionTemplate(id) {
            <%# ucPopupSiteActionTemplate.ClientID %>_ShowSiteActionTemplateRadWindow(id);
            $('#silverlightControlHost').css('height', '0px');
            if ($('#silverlightMapConversionHost').length != 0)
                $('#silverlightMapConversionHost').css('height', '0px');
        }

        function GetSiteActionTemplate(id, name) {
            $('#silverlightControlHost').css('height', '600px');
            if ($('#silverlightMapConversionHost').length != 0)
                $('#silverlightMapConversionHost').css('height', '600px');
            var slCtl = $("#silverlightWorkflow")[0];
            slCtl.Content.SilverlightMethodsActivity.SetSiteActionTemplate(id, name);
        }

        /*var slCtl = null;
        function pluginLoaded(sender, args) {
            slCtl = sender.getHost();
        }*/
        function OnClientTabSelected(sender, eventArgs) {
            var tab = eventArgs.get_tab();
            //if (tab.get_value() != 'Silverlight' && tab.get_value() != 'SilverlightMapConversion') {
                $('#silverlightControlHost').css('height', '1px');
                $('#RadMultiPage1').css('display', 'block');
                $('#silverlightPageContainer').removeClass('workflow-silverlight-visible');
                $('#silverlightPageContainer').addClass('workflow-silverlight-hidden');
                $('#silverlightPageContainer').css('visibility', 'hidden');

                $('#silverlightMapConversionHost').css('height', '1px');
                $('#silverlightMapConversion').removeClass('workflow-silverlight-visible');
                $('#silverlightMapConversion').addClass('workflow-silverlight-hidden');
                $('#silverlightMapConversion').css('visibility', 'hidden');
            //}
            //else {
                if (tab.get_value() == 'Silverlight') {
                    $('#silverlightControlHost').css('height', '600px');
                    $('#RadMultiPage1').css('display', 'none');
                    $('#silverlightPageContainer').addClass('workflow-silverlight-visible');
                    $('#silverlightPageContainer').css('visibility', 'visible');

                }

                if (tab.get_value() == 'SilverlightMapConversion') {
                    $('#silverlightMapConversionHost').css('height', '600px');
                    $('#RadMultiPage1').css('display', 'none');
                    $('#silverlightMapConversion').addClass('workflow-silverlight-visible');
                    $('#silverlightMapConversion').css('visibility', 'visible');
                }
            //}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar2" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsContact" ObjectTypeName="tbl_WorkflowTemplate" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div>
	            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						            CssClass="validation-summary"
						            runat="server"
						            EnableClientScript="true"
						            HeaderText="Заполните все поля корректно:"
						            ValidationGroup="groupWorkflowTemplate" />

                    <div class="row">
                        <label>Название:</label>
                        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" Width="646px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Название'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                    </div>

        
                    <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" Visible="false" Text="По шаблону есть активные процессы. Редактирование шаблона процесса может привести к нарушению их работы, а также искажению статистических данных. Если Вам нужно изменить схему процесса - рекомендуется создать новый процесс как копию существующего." />

	                <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" OnClientTabSelected="OnClientTabSelected">
		                <Tabs>
			                <telerik:RadTab Text="Основная информация" />
                            <telerik:RadTab Text="Шаблон процесса" Value="Silverlight" />
			                <telerik:RadTab Text="Условие запуска" />
                            <telerik:RadTab Text="Дополнительные настройки" />
                            <telerik:RadTab Text="Цели процесса" />
                            <telerik:RadTab Text="Карта конверсии процессов" Value="SilverlightMapConversion" Visible="False" />
                            <telerik:RadTab Text="Материалы" Value="Materials" Visible="False" />
		                </Tabs>
	                </telerik:RadTabStrip>

                    <telerik:RadMultiPage ID="RadMultiPage1" ClientIDMode="Static" SelectedIndex="0" CssClass="multiPage" runat="server">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div class="left-column">
                                <div class="row">
				                    <label>Автор:</label>
                                    <uc:Contact ID="ucAuthor" FilterByFullName="true" ValidationErrorMessage="Вы не выбрали 'Автор'" ValidationGroup="groupWorkflowTemplate" runat="server" />
			                    </div> 
                                <div class="row">
                                    <label>Модуль:</label>
                                    <asp:DropDownList ID="ddlModule" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlModule" Display="Dynamic" ErrorMessage="Вы не выбрали 'Модуль'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <label>Статус:</label>
                                    <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                                </div>
                            </div>
			                <div class="right-column">
                                <div class="row">
                                    <label>Актуален с:</label>
                                    <telerik:RadDatePicker ID="rdpStartDate" Width="100px" ShowPopupOnFocus="true" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Актуален до:</label>
                                    <telerik:RadDatePicker ID="rdpEndDate" Width="100px" ShowPopupOnFocus="true" runat="server" />
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="row">
                                <label>Описание:</label>
                                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="area-text" Width="635px" />
                            </div>
                            </telerik:RadPageView>

                        <telerik:RadPageView ID="RadPageView5" runat="server" ClientIDMode="static">
                            <%--<uc:WorkflowTemplateElementRelation ID="ucWorkflowTemplateElementRelation" Visible="false" runat="server" />--%>                                                    
                        </telerik:RadPageView>

                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                <div class="row">
                                    <label>Ручной запуск:</label>
                                    <asp:CheckBox ID="cbManualStart" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Способ автоматического запуска:</label>
                                    <asp:DropDownList ID="ddlAutomaticMethod" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlAutomaticMethod_OnSelectedIndexChanged" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlAutomaticMethod" Display="Dynamic" ErrorMessage="Вы не выбрали 'Способ автоматического запуска'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <label>Вызывать не чаще, дней:</label>
                                    <telerik:RadNumericTextBox ID="txtFrequency" Type="Number" CssClass="input-text" Value="0" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtFrequency" Display="Dynamic" ErrorMessage="Вы не ввели 'Вызывать не чаще, дней'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="row">
                                    <label>Не вызывать при активном процессе:</label>
                                    <asp:CheckBox ID="cbDenyMultipleRun" runat="server" />
                                </div>

                                <asp:Panel ID="pnlEditRecord" Visible="false" runat="server">
                                    <h3>Условие запуска</h3>
                                    <div class="row">
                                        <label>Событие:</label>
                                        <asp:DropDownList ID="ddlEvent" CssClass="select-text" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlEvent" Display="Dynamic" ErrorMessage="Вы не выбрали 'Событие'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Panel ID="pnlFilter" CssClass="row row-filter clearfix" Visible="false" runat="server">
                                        <label>Фильтр для модуля:</label>
                                        <telerik:RadFilter ID="RadFilter1" ShowApplyButton="false" Skin="Windows7" runat="server"></telerik:RadFilter>
                                    </asp:Panel>
                                </asp:Panel>

                                <asp:Panel ID="pnlActivityContact" Visible="false" runat="server">
                                    <h3>Условие запуска</h3>
                                    <div class="row">
                                        <label>Условие:</label>
                                        <asp:DropDownList ID="ddlCondition" AutoPostBack="true" OnSelectedIndexChanged="ddlCondition_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCondition" Display="Dynamic" ErrorMessage="Вы не выбрали 'Условие'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Panel ID="pnlActivityCount" CssClass="row" Visible="false" runat="server">
                                        <label>Кол-во действий:</label>
                                        <telerik:RadNumericTextBox ID="txtActivityCount" Type="Number" CssClass="input-text" runat="server">
                                            <NumberFormat GroupSeparator="" AllowRounding="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtActivityCount" Display="Dynamic" ErrorMessage="Вы не ввели 'Кол-во действий'" ValidationGroup="groupWorkflowTemplate" runat="server">*</asp:RequiredFieldValidator>                            
                                    </asp:Panel>
                                    <uc:WorkflowTemplateConditionEvent ID="ucWorkflowTemplateConditionEvent" runat="server" />
                                </asp:Panel>

                                <asp:Panel ID="pnlTimer" Visible="false" runat="server">
                                    <h3>Условие запуска</h3>
                                </asp:Panel>
                                </telerik:RadAjaxPanel>
                        </telerik:RadPageView>
            
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <h3>Параметры процесса</h3>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
                                <uc:WorkflowTemplateParameter ID="ucWorkflowTemplateParameter" runat="server" />
                            </telerik:RadAjaxPanel>
                        </telerik:RadPageView>
            
                        <telerik:RadPageView ID="RadPageView4" runat="server" ClientIDMode="static">
                            <uc:WorkflowTemplateGoal runat="server" ID="ucWorkflowTemplateGoal" />
                        </telerik:RadPageView>
            
                        <telerik:RadPageView ID="RadPageView6" runat="server" ClientIDMode="static">
                            <uc:WorkflowTemplateMaterial ID="ucWorkflowTemplateMaterial" runat="server" />
                        </telerik:RadPageView>
                        <%--<telerik:RadPageView ID="RadPageView4" runat="server">
                            <uc:WorkflowTemplateElement ID="ucWorkflowTemplateElement" runat="server" />
                        </telerik:RadPageView>--%>                        

                    </telerik:RadMultiPage>
        
                    <div id="silverlightPageContainer" class="multiPage workflow-silverlight-hidden">
                        <div id="silverlightControlHost" style="text-align:center;" runat="server" ClientIDMode="Static">                        
                        </div>
                        <script type="text/javascript">
                            document.getElementById('silverlightControlHost').oncontextmenu = disableRightClick;
                            function disableRightClick(e) {
                                if (!e) e = window.event;
                                if (e.preventDefault) {
                                    e.preventDefault();
                                } else {
                                    e.returnValue = false;
                                }
                            }
                        </script>
                    </div>    
        
                    <div id="silverlightMapConversion" class="multiPage workflow-silverlight-hidden">
                        <div id="silverlightMapConversionHost" style="text-align:center;" runat="server" ClientIDMode="Static">                        
                        </div>
                        <script type="text/javascript">
                            document.getElementById('silverlightMapConversionHost').oncontextmenu = disableRightClick;
                            function disableRightClick(e) {
                                if (!e) e = window.event;
                                if (e.preventDefault) {
                                    e.preventDefault();
                                } else {
                                    e.returnValue = false;
                                }
                            }
                        </script>
                    </div>  
        
                    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server">
                        <%--<uc:SiteActionTemplate runat="server" ID="ucSiteActionTemplate" CurrentSiteActionTemplateCategory="Workflow" fromSilverlight="true" />--%>   
                        <uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" SiteActionTemplateCategory="Workflow" ValidationGroup="groupWorkflowTemplate" fromSilverlight="True" runat="server" />
                    </telerik:RadAjaxPanel>
        

                    <asp:TextBox ID="SLChanged" ClientIDMode="Static" style="visibility: hidden;" runat="server" />
                    <asp:HiddenField ID="xmlWorkflow" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="jsonElements" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="jsonElementRelations" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="SLEditing" ClientIDMode="Static" Value="0" runat="server" />
        
	                <br/>
        
	                <div class="buttons">
		                <asp:LinkButton ID="lbtnSave" ClientIDMode="Static" OnClientClick="if (!isSLLoaded) return false; if ($('#SLEditing').val() == '1') { alert('Пожалуйста завершите редактирование шаблона процесса в графическом редакторе.'); return false; };" OnClick="lbtnSave_OnClick" CssClass="btn btn-disabled" ValidationGroup="groupWorkflowTemplate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>