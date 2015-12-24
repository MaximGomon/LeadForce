<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActivityRule.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActivityRule" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteColumnTooltip" Src="~/UserControls/SiteColumnTooltip.ascx" %>
<%@ Register TagPrefix="uc" TagName="CssEditor" Src="~/UserControls/CssEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="ExternalResources" Src="~/UserControls/Shared/ExternalResources.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script src="<%=ResolveUrl("~/Scripts/jquery.numeric.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        var fullImageHeight;
        var fullImageWidth;

        function HideTooltip() {
            var radToolTip = $find("<%= rttSiteColumn.ClientID %>");
            if (radToolTip != null) radToolTip.hide();

            var radToolTip2 = $find("<%= rrtSiteColumnExternalForms.ClientID %>");
            if (radToolTip2 != null) radToolTip2.hide();
            $find('<%= RadAjaxPanel1.ClientID %>').ajaxRequest();
        }
        function OnClientClicking_Cancel(sender, args) {
            $find('<%= radLoadDataTooltip.ClientID %>').hide();
            args.set_cancel(true);
        }

        function fileUploaded(sender, args) {
            //$find('<%= RadAjaxManager.GetCurrent(Page).ClientID %>').ajaxRequest();
            $find('<%= RadAjaxPanel1.ClientID %>').ajaxRequest();
            $telerik.$(".invalid").html("");
            setTimeout(function () {
                sender.deleteFileInputAt(0);
            }, 10);
        }

        function validationFailed(sender, args) {
            /*$telerik.$(".invalid")
                    .html("Invalid extension, please choose an image file")
                    .get(0).style.display = "block";*/
            sender.deleteFileInputAt(0);
        }

        function pageLoad() {
            $(".numeric").numeric({ decimal: false, negative: false });
        }

        function ImageResize(arg) {
            var height = $('#<%= txtImageHeight.ClientID %>').val();
            var heightPercent = $('#<%= txtImageHeightPercent.ClientID %>').val();
            var width = $('#<%= txtImageWidth.ClientID %>').val();
            var widthPercent = $('#<%= txtImageWidthPercent.ClientID %>').val();

            var percent;
            switch (arg) {
                case 'Height':
                    percent = getPercent(fullImageHeight, height);
                    $('#<%= txtImageHeightPercent.ClientID %>').val(percent);
                    $('#<%= txtImageWidthPercent.ClientID %>').val(percent);
                    $('#<%= txtImageWidth.ClientID %>').val(getPixel(fullImageWidth, percent));
                    break;
                case 'HeightPercent':
                    $('#<%= txtImageHeight.ClientID %>').val(getPixel(fullImageHeight, heightPercent));
                    $('#<%= txtImageWidthPercent.ClientID %>').val(heightPercent);
                    $('#<%= txtImageWidth.ClientID %>').val(getPixel(fullImageWidth, heightPercent));
                    break;
                case 'Width':
                    percent = getPercent(fullImageWidth, width);
                    $('#<%= txtImageWidthPercent.ClientID %>').val(percent);
                    $('#<%= txtImageHeightPercent.ClientID %>').val(percent);
                    $('#<%= txtImageHeight.ClientID %>').val(getPixel(fullImageHeight, percent));
                    break;
                case 'WidthPercent':
                    $('#<%= txtImageWidth.ClientID %>').val(getPixel(fullImageWidth, widthPercent));
                    $('#<%= txtImageHeightPercent.ClientID %>').val(widthPercent);
                    $('#<%= txtImageHeight.ClientID %>').val(getPixel(fullImageHeight, widthPercent));
                    break;
            }
        }

        function getPercent(fullSize, currentSize) {
            var onePercent = fullSize / 100;
            return Math.round(currentSize / onePercent);
        }

        function getPixel(fullSize, percent) {
            var onePercent = fullSize / 100;
            return Math.round(onePercent * percent);
        }
    </script>
</telerik:RadScriptBlock>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" OnAjaxRequest="RadAjaxPanel1_OnAjaxRequest">
      
        <telerik:RadToolTip ID="rttSiteColumn" ManualClose="true" ManualCloseButtonText="закрыть" CssClass="tooltip-padding" AutoCloseDelay="0" Modal="true" ShowEvent="FromCode" Position="Center" RelativeTo="Element" runat="server">
                                        <uc:SiteColumnTooltip ID="SiteColumnTooltip1" OnSaved="SiteColumnTooltip1_OnSaved" runat="server" />
                                    </telerik:RadToolTip>        
        <uc:NotificationMessage runat="server" ID="ucLoadDataMessage" MessageType="Success" Style="margin-top: 0" />
                
        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
                    CssClass="validation-summary"
                    runat="server"
                    EnableClientScript="true"
                    HeaderText="Заполните все поля корректно:"
                    ValidationGroup="valGroupUpdate" />

		<telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			<Tabs>
				<telerik:RadTab Text="Основная информация" />
                <telerik:RadTab Text="Дизайн формы" Value="DesignForm" Visible="false" />
				<telerik:RadTab Text="Шаблон формы" Value="FormTemplate" Visible="false" />
                <telerik:RadTab Text="Ресурсы" Value="ExternalResource" Visible="false" />
                <telerik:RadTab Text="Внешняя форма" Value="ExternalForm" Visible="false" />
			</Tabs>
		</telerik:RadTabStrip>

		<asp:Literal ID="ErrorMessage" runat="server" />
		<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			<telerik:RadPageView ID="RadPageView1" runat="server">
				<div class="row">
					<label>Название правила:</label>
					<asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" ErrorMessage="Вы не ввели название правила" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
				</div>
                <asp:panel CssClass="row" ID="plRuleType" runat="server">				
					<label>Тип правила:</label>
					<asp:DropDownList ID="ddlRuleTypeID" AutoPostBack="true" OnSelectedIndexChanged="ddlRuleTypeID_SelectedIndexChanged" CssClass="select-text" runat="server"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlRuleTypeID" ErrorMessage="Вы не ввели тип правила" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>				
                </asp:panel>
				<div class="row">
					<label>Код:</label>
					<asp:TextBox ID="txtCode" CssClass="input-text" runat="server" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCode" ErrorMessage="Вы не ввели код" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="Неверный формат кода" ControlToValidate="txtCode" ValidationExpression="^[\[\]a-zA-Z0-9_-]+$" ValidationGroup="valGroupUpdate" runat="server" />
				</div>
                <asp:panel CssClass="row" ID="plDescription" visible="false" runat="server">				
					<label>Описание:</label>
                    <asp:TextBox ID="txtDescription" CssClass="area-text" TextMode="MultiLine" Skin="Windows7" runat="server" Width="600px" />
                </asp:panel>
                <asp:Panel runat="server" ID="plWufooForm" Visible="false">
                    <div class="row">
                        <label>Wufoo name:</label>
                        <asp:TextBox runat="server" ID="txtWufooName" CssClass="input-text" ReadOnly="true" />
                    </div>
                    <div class="row">
                        <label>Wufoo API Key:</label>
                        <asp:TextBox runat="server" ID="txtWufooAPIKey" CssClass="input-text" ReadOnly="true" />
                    </div>
                    <div class="row">
                        <label>Дата для загрузки:</label>
                        <telerik:RadDateTimePicker ID="rdtpRevisionDate" Width="165px" CssClass="datetime-picker" ShowPopupOnFocus="True" runat="server" />
                    </div>
                    <div class="row">
                        <label>Частота загрузки:</label>
                        <asp:DropDownList ID="ddlWufooUpdatePeriod" runat="server" CssClass="select-text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlWufooUpdatePeriod" InitialValue="" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage="Вы не выбрали частоту загрузки" />
                    </div>
                </asp:Panel>
                
                <asp:Panel ID="pnlTextButton" CssClass="row" Visible="False" runat="server">
                    <label>Текст кнопки:</label>
                    <asp:TextBox ID="txtTextButton" CssClass="input-text" runat="server" />
                </asp:Panel>

				<asp:Panel ID="pURL" runat="server">
                    <div class="row">
                        <label>Текст сообщения об ошибке:</label>
                        <asp:TextBox runat="server" ID="txtErrorMessage" Width="600px" CssClass="input-text" />
                    </div>
                    
                    <h3>Действие после заполнения</h3>
                    <div class="row">
                        <label>Действие:</label>
                        <asp:DropDownList ID="ddlActionOnFillForm" AutoPostBack="True" OnSelectedIndexChanged="ddlActionOnFillForm_OnSelectedIndexChanged" runat="server" CssClass="select-text" />
                    </div>
                    <asp:Panel ID="pnlActionRedirect" runat="server">
					    <div class="row">
						    <label>Ссылка для перехода:</label>
						    <asp:TextBox ID="txtURL" CssClass="input-text" runat="server" />
					    </div>
					    <div class="row">
						    <label>Передавать поля формы:</label>
						    <asp:CheckBox ID="cbSendFields" runat="server" />
					    </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlActionMessage" runat="server">
                        <div class="row">
                            <label>Сообщение:</label>
                            <asp:TextBox runat="server" ID="txtSuccessMessage" Width="600px" CssClass="input-text" />
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <label>Цели яндекс:</label>
                        <asp:TextBox runat="server" ID="txtYandexGoals" Width="600px" TextMode="MultiLine" CssClass="area-text" />
                    </div>
				</asp:Panel>
				<asp:Panel ID="pFile" Visible="false" runat="server">
					<div class="row">
						<label>Файл:</label>
						<asp:FileUpload ID="fuFile" runat="server" />
					</div>
				</asp:Panel>

				<asp:Panel ID="pFormFields" Visible="false" runat="server">
					<div class="row">
						<label>Ф.И.О.:</label>
						<asp:CheckBox ID="cbUserFullName" runat="server" />
					</div>
					<div class="row">
						<label>Email:</label>
						<asp:CheckBox ID="cbEmail" runat="server" />
					</div>
					<div class="row">
						<label>Телефон:</label>
						<asp:CheckBox ID="cbPhone" runat="server" />
					</div>
					<div class="row additional-columns">
						<h3>Дополнительные реквизиты:</h3>
						<asp:CheckBoxList ID="cblSiteColumns" runat="server"></asp:CheckBoxList>
					</div>
				</asp:Panel>
			</telerik:RadPageView>
            
            <telerik:RadPageView ID="RadPageView4" runat="server">
                <h3>Настройка формы</h3>
                <div class="row row-color-picker clearfix">        
                    <label>Фон формы:</label>
                    <telerik:RadColorPicker ShowIcon="true" ID="rcpBackgroundColor" runat="server" PaletteModes="All" style="margin-left: 4px" />
                </div>
                <div class="row">
                    <label>Оформление формы:</label>
                    <asp:DropDownList ID="ddlSkin" CssClass="select-text" runat="server" />
                </div>
                
                <h3>Стиль инструкций</h3>
                <uc:CssEditor ID="ucCssEditorInstruction" runat="server" />
                
                
                <h3>Стиль полей ввода данных</h3>
                <uc:CssEditor ID="ucCssEditorColumns" runat="server" />
                
                <h3>Стиль кнопки</h3>
                <uc:CssEditor ID="ucCssEditorButton" runat="server" />
            </telerik:RadPageView>

			<telerik:RadPageView ID="RadPageView2" runat="server">
				<div class="row">
					<label>Ширина формы:</label>
					<telerik:RadNumericTextBox ID="txtFormWidth" Type="Number" CssClass="input-text" runat="server">
						<NumberFormat GroupSeparator="" AllowRounding="false" />
					</telerik:RadNumericTextBox>
				</div>
				<div class="row">
					<label>Количество дополнительных полей:</label>
					<telerik:RadNumericTextBox ID="txtCountExtraFields" Type="Number" CssClass="input-text" runat="server">
						<NumberFormat GroupSeparator="" AllowRounding="false" />
					</telerik:RadNumericTextBox>
				</div>

				<asp:Button ID="btnAddGroupFields" OnClick="btnAddGroupFields_OnClick" Text="Добавить группу полей" runat="server" />
				<asp:Button ID="btnAddProfileField" OnClick="btnAddProfileField_OnClick" Text="Добавить поле профиля" runat="server" />
				<asp:Button ID="btnAddFormField" OnClick="btnAddFormField_OnClick" Text="Добавить поле формы" runat="server" />
				<asp:Button ID="btnAddTextField" OnClick="btnAddTextField_OnClick" Text="Добавить текстовый блок" runat="server" />                
                <asp:Button ID="btnAddImageField" OnClick="btnAddImageField_OnClick" Text="Добавить изображение" runat="server" />
			    <asp:DropDownList runat="server" ID="ddlSystemLayouts" CssClass="select-text" AutoPostBack="True" OnSelectedIndexChanged="ddlSystemLayouts_OnSelectedIndexChanged">
			        <asp:ListItem Text="Стандартные компоненты" Value="" />
			        <asp:ListItem Text="Форма обратной связи" Value="11" />
                    <asp:ListItem Text="Пригласить друга" Value="12" />
                </asp:DropDownList>                
				<asp:Button ID="btnDeleteNode" OnClick="btnDeleteNode_OnClick" Text="Удалить" Enabled="false" runat="server" />
				<br /><br />

				<telerik:RadSplitter ID="RadSplitter1" Width="1100" runat="server" VisibleDuringInit="false">
					<telerik:RadPane ID="RadPane1" Width="300" MinWidth="200" runat="server">
						<telerik:RadTreeView ID="rtvFormTree" OnNodeClick="rtvFormTree_OnNodeClick" OnNodeDrop="rtvFormTree_OnNodeDrop" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true" Skin="Windows7" runat="server">
							<DataBindings>
								<telerik:RadTreeNodeBinding Expanded="True" />
							</DataBindings>
						</telerik:RadTreeView>
                          <div style="display: none">
                              <telerik:RadEditor ID="RadEditor1" runat="server" Width="0" Height="0"/>                                            
                        </div>                        
					</telerik:RadPane>
					<telerik:RadSplitBar ID="RadSplitBar1" runat="server"></telerik:RadSplitBar>
					<telerik:RadPane ID="RadPane2" MinWidth="630" runat="server">
						<asp:Panel ID="pFormProperties" Visible="false" CssClass="form-properties" runat="server">
							<asp:Panel ID="pProfileField" runat="server">
								<div class="row">
									<label>Название:</label>
									<asp:DropDownList ID="ddlSiteColumn" OnSelectedIndexChanged="ddlSiteColumn_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server" />
								</div>
							</asp:Panel>
							<asp:Panel ID="pFormField" runat="server">
								<div class="row">
									<label>Название:</label>
									<asp:Literal ID="litLayoutName" runat="server" /> (<asp:LinkButton ID="btnEditSiteColumn" OnCommand="btnEditSiteColumn_OnCommand" Text="Редактировать поле" runat="server" />)
									<asp:HiddenField ID="hdnSiteColumnID" runat="server" />                                    
								</div>
							</asp:Panel>
							<asp:Panel ID="pField" runat="server">
								<div class="row">
									<label>Обязательность:</label>
									<asp:CheckBox ID="cbIsRequired" runat="server"/>
								</div>
								<div class="row">
									<label>Дополнительное:</label>
									<asp:CheckBox ID="cbIsExtraField" runat="server"/>
								</div>
								<div class="row">
									<label>Административное:</label>
									<asp:CheckBox ID="cbIsAdmin" runat="server"/>
								</div>
                                <asp:Panel ID="pnlFormatInputOutput" CssClass="row" Visible="False" runat="server">
                                    <label>Формат ввода/вывода:</label>
                                    <asp:DropDownList ID="ddlFormatInputOutput" CssClass="select-text" runat="server" />
                                </asp:Panel>
								<div class="row">
									<label>CSS стиль:</label>
									<asp:TextBox ID="tbCSSStyle" TextMode="MultiLine" CssClass="area-text" runat="server" />
								</div>
								<asp:Panel ID="pDefaultValue" runat="server">
									<div class="row">
										<label>Значение по умолчанию:</label>
										<asp:TextBox ID="tbDefaultValue" CssClass="input-text" runat="server" />                                                                      
                                         <telerik:RadDatePicker ID="rdpDefaultValue"  Width="110" runat="server" />
                                         <asp:DropDownList ID="ddlDefaultValue" CssClass="select-text" runat="server" />
                                         <asp:CheckBox ID="cbDefaultValue" runat="server" />
									</div>
								</asp:Panel>
							</asp:Panel>                            
							<asp:Panel ID="pGroupFields" runat="server">
								<div class="row">
									<label>Название:</label>
									<asp:TextBox ID="txtLayoutName" CssClass="input-text" runat="server"></asp:TextBox>
								</div>
								<div class="row">
									<label>Формат вывода:</label>
									<asp:DropDownList ID="ddlOutputFormat" CssClass="select-text" runat="server" />
								</div>
								<div class="row">
									<label>Направление:</label>
									<asp:DropDownList ID="ddlOrientation" CssClass="select-text" runat="server" />
								</div>
								<div class="row">
									<label>Формат вывода полей:</label>
									<asp:DropDownList ID="ddlOutputFormatFields" CssClass="select-text" runat="server" />
								</div>
                                <div class="row">
									<label>CSS стиль:</label>
									<asp:TextBox ID="txtGroupCSSStyle" TextMode="MultiLine" CssClass="area-text" runat="server" />
								</div>
                                <asp:Panel ID="plIsUsedForAdditionalDetails" runat="server" CssClass="row" Visible="false">
                                    <label>Использовать для дополнительных реквизитов:</label>
                                    <asp:CheckBox runat="server" ID="chxIsUsedForAdditionalDetails" />
                                </asp:Panel>
							</asp:Panel>
							<asp:Panel ID="pTextBlock" runat="server">
							    <asp:Panel runat="server" CssClass="row" ID="plShowTextBlockInMaster" Visible="false">
                                    <label>Отражать в мастере:</label>
                                    <asp:DropDownList ID="ddlShowTextBlockInMaster" CssClass="select-text" runat="server" />                                
                                </asp:Panel>
								<div class="row">
									<label>Название:</label>
									<asp:TextBox ID="txtLayoutName_TextBlock" CssClass="input-text" runat="server" />
								</div>
								<div class="row row-html-editor clearfix">
									<label>Описание:</label>										
									<telerik:RadEditor ID="tbDescription_TextBlock" runat="server" EnableResize="false" ToolsFile="~/RadEditor/Tools.xml" CssClass="rad-editor"  AutoResizeHeight="false" Width="400" Height="270"/>
								</div>               
                                <asp:Panel runat="server" ID="plIsUsedForErrorMessage" CssClass="row" Visible="False">
                                    <label>Использовать для отражения ошибок:</label>
                                    <asp:CheckBox runat="server" ID="chxIsUsedForErrorMessage"/>
                                </asp:Panel>                 
							</asp:Panel>
                            <asp:Panel ID="pImage" runat="server">
                                <telerik:RadBinaryImage runat="server" ID="rbiImage" ResizeMode="Fit" Width="200" Height="150" /><br />

                                <div class="row">
                                    <label>Изображение:</label>
                                    <telerik:RadAsyncUpload ID="rauImageFile" OnClientFileUploaded="fileUploaded" OnFileUploaded="rauImageFile_OnFileUploaded" AllowedFileExtensions="jpeg,jpg,gif,png,bmp"
                                    OnClientValidationFailed="validationFailed" runat="server" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                                </div>
                                <div class="row">
                                    <label>Высота:</label>
                                    <asp:TextBox ID="txtImageHeight" CssClass="input-text numeric" onkeyup="ImageResize('Height');" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Высота %:</label>
                                    <asp:TextBox ID="txtImageHeightPercent" CssClass="input-text numeric" onkeyup="ImageResize('HeightPercent');" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Ширина:</label>
                                    <asp:TextBox ID="txtImageWidth" CssClass="input-text numeric" onkeyup="ImageResize('Width');" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Ширина %:</label>
                                    <asp:TextBox ID="txtImageWidthPercent" CssClass="input-text numeric" onkeyup="ImageResize('WidthPercent');" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Выравнивание:</label>
                                    <asp:RadioButtonList ID="rblAlign" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server">
                                        <asp:ListItem Text="влево" Value="left" Selected="True" />
                                        <asp:ListItem Text="по центру" Value="center" />
                                        <asp:ListItem Text="вправо" Value="right" />
                                    </asp:RadioButtonList>
                                </div>
                                <div class="row">
                                    <label>Альтернативный текст:</label>
                                    <asp:TextBox ID="txtImageAlternativeText" CssClass="input-text" runat="server" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pFeedbackForm" runat="server" Visible="false">
                                <div class="row">
                                    <label>Режим подачи заявки:</label>
                                    <asp:RadioButtonList runat="server" ID="rblStep" />
                                </div>
                                <div class="row">
                                    <label>База знаний:</label>
                                    <asp:RadioButtonList runat="server" ID="rblKnowledgeBase" />
                                </div>
                                <div class="row">
                                    <label>Типы обращений:</label>                    
                                    <asp:CheckBoxList runat="server" ID="chxPublicationType" RepeatLayout="Table" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pInviteFriendForm" runat="server" Visible="false">
                                <div class="row">
                                    <uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" SiteActionTemplateCategory="Workflow" runat="server" />
                                </div>                                
                                <div class="row">
                                    <label>Процесс:</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbWorkflowTemplate" DictionaryName="tbl_WorkflowTemplate" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>                                 
                            </asp:Panel>
						</asp:Panel>
					</telerik:RadPane>
				</telerik:RadSplitter>
			</telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView5" runat="server">
                <uc:ExternalResources runat="server" ID="ucExternalResources" />
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server">
                <asp:Panel runat="server" ID="plExternalFormUrls">
                    <div class="row">
                        <label>Адрес размещения (URL):</label>
                        <asp:TextBox ID="txtExternalFormUrl" CssClass="input-text" runat="server" />
                        <telerik:RadButton ID="btnGetExternalForm" OnClick="btnGetExternalForm_OnClick" Skin="Windows7" Text="Опросить" runat="server" />
                    </div>
                    <div class="row">
                        <label>Адрес перехода (URL):</label>
                        <asp:TextBox ID="txtRepostURL" CssClass="input-text" runat="server" />
                    </div>
                    <div class="row">
                        <asp:ListView ID="lvExternalForms" OnItemDataBound="lvExternalForms_OnItemDataBound" runat="server">
                            <ItemTemplate>
                                <label><asp:LinkButton ID="btnExternalForm" ClientIDMode="AutoID" OnCommand="btnExternalForm_OnCommand" CommandArgument='<%# Eval("ID") %>' runat="server"><%# Eval("Name") %></asp:LinkButton></label> <asp:Literal ID="lActionURL" runat="server" />
                                <br />
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </asp:Panel>
                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" Style="margin-top: 0" />
                <div class="row">
                    <telerik:RadGrid ID="rgExternalFormFields" OnItemCreated="rgExternalFormFields_OnItemCreated" OnItemDataBound="rgExternalFormFields_OnItemDataBound" AutoGenerateColumns="false" AllowPaging="false" PageSize="9999" GridLines="None" Visible="false" Skin="Windows7" runat="server">
                        <MasterTableView DataKeyNames="ID,SiteActivityRuleExternalFormID">
                            <Columns>
                                <telerik:GridTemplateColumn DataField="Name" HeaderText="Название поля">
                                    <ItemTemplate>                                        
                                        <%# GetLabel((string)Eval("Name")) %>
                                        <asp:TextBox runat="server" ID="txtName" CssClass="input-text" Visible='<%# _ruleTypeId == (int)RuleType.LPgenerator %>' Text='<%# (string)Eval("Name") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Тип поля">
                                    <ItemTemplate>
                                        <asp:Literal ID="lFieldType" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Поле LeadForce">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlColumns" OnSelectedIndexChanged="ddlColumns_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server"/>
                                        <asp:LinkButton ID="lbEditSiteColumn" OnCommand="lbEditSiteColumn_OnCommand" Visible="false" runat="server">изменить</asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <telerik:RadToolTip ID="rrtSiteColumnExternalForms" ManualClose="true" ManualCloseButtonText="закрыть" CssClass="tooltip-padding" AutoCloseDelay="0" Modal="true" ShowEvent="FromCode" Position="Center" RelativeTo="BrowserWindow" runat="server">
                    <uc:SiteColumnTooltip ID="SiteColumnTooltip2" OnSaved="SiteColumnTooltip2_OnSaved" runat="server" />
                </telerik:RadToolTip>
            </telerik:RadPageView>  
		</telerik:RadMultiPage>

		<asp:Panel ID="pLandingPage" Visible="false" runat="server">
			<h3>Посадочная страница</h3>
		</asp:Panel>
        </telerik:RadAjaxPanel>
    
	<br />
	<div class="buttons">
		<asp:LinkButton ID="btnUpdate" CssClass="btn" OnClick="btnUpdate_Click" ValidationGroup="valGroupUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <asp:LinkButton ID="lbtnLoadData" CssClass="btn" runat="server" Visible="false"><em>&nbsp;</em><span>Загрузить данные</span></asp:LinkButton>
		<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	</div>            

<telerik:RadToolTip ID="radLoadDataTooltip" Skin="Windows7" TargetControlID="lbtnLoadData" ShowEvent="OnClick" Position="TopCenter" RelativeTo="Element" AutoCloseDelay="0" runat="server">
     <div style="padding: 10px;">
        <telerik:RadDateTimePicker ID="rdtpLoadDataDate" Width="165px" CssClass="datetime-picker" ShowPopupOnFocus="True" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdtpLoadDataDate" ValidationGroup="valGroupLoadData" runat="server">*</asp:RequiredFieldValidator>
        <br /><br />
        <telerik:RadButton ID="rbtnLoadData" Text="Загрузить" OnClick="rbtnLoadData_OnClick" CausesValidation="True" ValidationGroup="valGroupLoadData" Skin="Windows7" runat="server" />
        <telerik:RadButton ID="RadButton2" Text="Отмена" OnClientClicking="OnClientClicking_Cancel" CausesValidation="False" Skin="Windows7" runat="server" />
    </div>
</telerik:RadToolTip>
