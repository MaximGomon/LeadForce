<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Task.aspx.cs" Inherits="WebCounter.AdminPanel.Task" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskMembers" Src="~/UserControls/Task/TaskMembers.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskDurations" Src="~/UserControls/Task/TaskDurations.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskHistories" Src="~/UserControls/Task/TaskHistories.ascx" %>
<%@ Register TagPrefix="uc" TagName="SaveTaskDuration" Src="~/UserControls/Task/SaveTaskDuration.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgressBar" Src="~/UserControls/Task/ProgressBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="MainTaskMember" Src="~/UserControls/Task/MainTaskMember.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%#ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<telerik:RadCodeBlock runat="server">
<script type="text/javascript">    
    function RedirectToTasksList(arg) { window.location = '<%= UrlsData.AP_Tasks() %>';}
    function ShowTaskDurationRadWindow(button, args) { button.set_autoPostBack(false); $find('<%= taskDurationRadWindow.ClientID %>').show(); }
    function CloseRadWindow() { $find('<%= taskDurationRadWindow.ClientID %>').close(); }
    function ShowChargRadWindow() { $find('<%= chargRadWindow.ClientID %>').show(); }
    function CloseChargRadWindow() { $find('<%= chargRadWindow.ClientID %>').close(); }
</script>
</telerik:RadCodeBlock>

<telerik:RadWindowManager ID="radWindowManager" Skin="Windows7" runat="server" EnableShadow="true"></telerik:RadWindowManager>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />    

	        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						        CssClass="validation-summary"
						        runat="server"
						        EnableClientScript="true"
						        HeaderText="Заполните все поля корректно:"
						        ValidationGroup="groupTask" />
	        <div class="two-columns tasks">
   
                <div class="row">
			        <label>Тема:</label>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="area-text" Width="640px" Height="30px" TextMode="MultiLine" />
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели тему" ValidationGroup="groupTask" runat="server" />
		        </div>
                <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			        <Tabs>
				        <telerik:RadTab Text="Основные данные" />
                        <telerik:RadTab Text="Основной участник" Visible="false" />
                        <telerik:RadTab Text="Участники" Visible="false" />
				        <telerik:RadTab Text="История задачи" />
			        </Tabs>
		        </telerik:RadTabStrip>
		        <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			        <telerik:RadPageView ID="RadPageView1" runat="server">

                        <telerik:RadWindow runat="server" Title="Поручить" Width="436px" Height="140px" ID="chargRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
                            <ContentTemplate>              
                                <div class="task-charg-popup">
                                    <asp:UpdatePanel ID="upCharg" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <label>Ответственный:</label>
                                                <uc:ContactComboBox runat="server" ID="ucRadWindowResponsible" FilterByFullName="true" />
                                            </div>
                                            <br/>
                                            <div class="buttons">
			                                    <asp:LinkButton ID="lbtnCharg" OnClick="lbtnCharg_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Поручить</span></asp:LinkButton>
			                                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseChargRadWindow();" />
		                                    </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>

                        <telerik:RadAjaxPanel runat="server" ID="rapMain">
                
                            <telerik:RadWindow runat="server" Title="История исполнения" Width="436px" Height="380px" ID="taskDurationRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
                                <ContentTemplate>                
                                    <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <uc:SaveTaskDuration ID="ucSaveTaskDuration" runat="server" IsNotFromRadGrid="true" />                
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </telerik:RadWindow>                    

                            <div class="left-column">
		                        <div class="row">
                                    <label>Тип:</label>    
                                    <uc:DictionaryComboBox ID="dcbTaskType" AutoPostBack="true" DictionaryName="tbl_TaskType" ValidationGroup="groupTask" ValidationErrorMessage="Вы не выбрали тип задачи" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row date-picker-autopostback clearfix">
				                    <label>Дата начала:</label>
				                    <div class="date-picker-container">
					                    <telerik:RadDateTimePicker ID="rdpStartDate" runat="server" AutoPostBackControl="Both" AutoPostBack="true" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDateTimePicker>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpStartDate" Text="*" ErrorMessage="Вы не ввели дату начала" ValidationGroup="groupTask" runat="server" />
				                    </div>
			                    </div>
                                <div class="row date-picker-autopostback clearfix">
				                    <label>Дата окончания:</label>
				                    <div class="date-picker-container">
					                    <telerik:RadDateTimePicker ID="rdpEndDate" runat="server" AutoPostBackControl="Both" AutoPostBack="true" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDateTimePicker>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdpEndDate" Text="*" ErrorMessage="Вы не ввели дату окончания" ValidationGroup="groupTask" runat="server" />
				                    </div>
			                    </div>
                                <asp:Panel runat="server" ID="plDateOfControl" CssClass="row date-picker-autopostback clearfix">
                                    <label>Дата контроля:</label>	
                                    <div class="date-picker-container">			        
					                    <telerik:RadDateTimePicker ID="rdpDateOfControl" runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDateTimePicker>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdpDateOfControl" Text="*" ErrorMessage="Вы не ввели дату контроля" ValidationGroup="groupTask" runat="server" />
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <label>Важная задача:</label>
                                    <asp:CheckBox runat="server" ID="chxIsImportantTask" />
                                </div>
                                <div class="row">
                                    <label>Длительность план, часов:</label>
                                    <telerik:RadNumericTextBox runat="server" ID="rntxtPlanDurationHours" AutoPostBack="true" OnTextChanged="rntxtPlanDuration_OnTextChanged" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
						                <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
					                </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtPlanDurationHours" Text="*" ErrorMessage="Вы не ввели длительность план, часов" ValidationGroup="groupTask" runat="server" />
                                </div>                        
                            </div>
                            <div class="right-column">
		                        <div class="row row-dictionary">
                                    <label>Ответственный:</label>
                                    <uc:ContactComboBox ID="ucResponsible" AutoPostBack="true" ValidationGroup="groupTask" ValidationErrorMessage="Вы не выбрали ответственного" CssClass="select-text" runat="server" FilterByFullName="true"/>
                                </div>
                                <div class="row">
                                    <label>Автор:</label>
                                    <telerik:RadComboBox runat="server" ID="rcbCreator" Enabled="false" EnableEmbeddedSkins="false" skin="Labitec" Width="234px"/>                            
                                </div>
                                <div class="row date-picker-autopostback clearfix">
                                    <label>Напоминание ответственному:</label>
                                    <div class="date-picker-container">			        
					                    <telerik:RadDateTimePicker ID="rdpResponsibleReminderDate" runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDateTimePicker>
                                    </div>
                                </div>
                                <div class="row date-picker-autopostback clearfix">
                                    <label>Напоминание автору:</label>
                                    <div class="date-picker-container">			        
					                    <telerik:RadDateTimePicker ID="rdpCreatorReminderDate" runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ShowPopupOnFocus="true">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDateTimePicker>
                                    </div>
                                </div>
                                <div class="row">
                                    <label>Срочная задача:</label>
                                    <asp:CheckBox runat="server" ID="chxIsUrgentTask" />
                                </div>
                                <div class="row">
                                    <label>Длительность план, минут:</label>
                                    <telerik:RadNumericTextBox runat="server" ID="rntxtPlanDurationMinutes" AutoPostBack="true" OnTextChanged="rntxtPlanDuration_OnTextChanged" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
						                <NumberFormat GroupSeparator="" DecimalDigits="0" />                             
					                </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rntxtPlanDurationMinutes" Text="*" ErrorMessage="Вы не ввели длительность план, минут" ValidationGroup="groupTask" runat="server" />
                                </div>
                            </div>
                            <div class="clear"></div>
                            <h3>Состояние</h3>
                            <asp:Panel runat="server" ID="plStatuses" CssClass="row row-btn">                    
                                <label>Состояние:</label>
                                <asp:Literal runat="server" ID="lrlTaskStatus" />
                                <telerik:RadButton runat="server" ID="rbtnRun" CommandArgument="Run" Text="Выполнить" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />
                                <telerik:RadButton runat="server" ID="rbtnCancel" CommandArgument="Cancel" Text="Отменить" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />
                                <telerik:RadButton runat="server" ID="rbtnHoldOver" CommandArgument="HoldOver" Text="Отложить" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />
                                <telerik:RadButton runat="server" ID="rbtnAddToPlan" CommandArgument="AddToPlan" Text="Поставить в план" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />
                                <telerik:RadButton runat="server" ID="rbtnCharg" CommandArgument="Charg" Text="Поручить" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />
                                <telerik:RadButton runat="server" ID="rbtnReject" CommandArgument="Reject" Text="Отклонить" Skin="Windows7" Visible="false" OnClick="rbtnStatus_OnClick" />                    
                            </asp:Panel>
                            <asp:Panel runat="server" ID="plResult" Visible="false">
                                <div class="row">
                                    <label>Результат:</label>    
                                    <uc:DictionaryComboBox ID="dcbTaskResult" DictionaryName="tbl_TaskResult" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
			                        <label>Результат подробно:</label>
			                        <asp:TextBox runat="server" ID="txtDetailedResult" CssClass="area-text" Width="630px" Height="30px" TextMode="MultiLine" />
		                        </div>
                            </asp:Panel>
                            <asp:Panel ID="plWorkflowResult" Visible="false" runat="server">
                                <div class="row">
                                    <label>Результат:</label>    
                                    <asp:DropDownList ID="ddlWorkflowResult" CssClass="select-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="" ControlToValidate="ddlWorkflowResult" Text="*" ErrorMessage="Вы не выбрали результат" ValidationGroup="groupTask" runat="server" />
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="plDuration" Visible="false">                        
                                <div class="left-column">
                                    <div class="row">
                                        <label>Длительность факт, часов:</label>
                                        <telerik:RadNumericTextBox runat="server" ID="rntxtActualDurationHours" Enabled="false" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
					                    </telerik:RadNumericTextBox>                                
                                    </div>
                                    <div class="row row-progressbar clearfix">
                                        <label>Процент выполнения:</label>
                                        <telerik:RadNumericTextBox runat="server" Visible="false" ID="rntxtCompletePercentage" MinValue="0" EmptyMessage="" Enabled="false" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" />                             
					                    </telerik:RadNumericTextBox>
                                        <uc:ProgressBar runat="server" ID="ucProgressBar" />
                                    </div>
                                </div>
                                <div class="right-column">
                                    <div class="row">
                                        <label>Длительность факт, минут:</label>
                                        <telerik:RadNumericTextBox runat="server" ID="rntxtActualDurationMinutes" Enabled="false" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="0" />                             
					                    </telerik:RadNumericTextBox>                                
                                    </div>
                                    <div class="row row-rbtn">
                                        <telerik:RadButton runat="server" ID="rbtnNoteTime" Text="Отметить время" Skin="Windows7" OnClientClicked="ShowTaskDurationRadWindow" />
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </asp:Panel>                    
                            <h3>Связи задачи</h3>
                            <asp:Panel runat="server" ID="plProductForPaying" CssClass="row" Visible="false">
                                <label>Продукт для оплаты:</label>    
                                <uc:DictionaryComboBox ID="dcbProducts" DictionaryName="tbl_Product" DataTextField="Title" DataValueField="ID" ShowEmpty="true" CssClass="select-text" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dcbProducts_OnSelectedIndexChanged" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="plOrders" CssClass="row" Visible="false">
                                <label>Заказы:</label>    
                                <uc:DictionaryComboBox ID="dcbOrders" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                            </asp:Panel>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <uc:MainTaskMember runat="server" ID="ucMainTaskMember" />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <uc:TaskMembers runat="server" ID="ucTaskMembers" />                
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server">
                        <uc:TaskDurations runat="server" ID="ucTaskDurations" Visible="true" />
                        <br/>
                        <h3>История изменения</h3>
                        <uc:TaskHistories runat="server" ID="ucTaskHistories" />
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <br/>
		        <div class="buttons">
			        <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupTask" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		        </div>
            </div>    
        </td>
    </tr>
</table>    
</asp:Content>
