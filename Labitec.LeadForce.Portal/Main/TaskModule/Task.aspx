<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Task.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.Task" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="TaskDurations" Src="~/Main/TaskModule/UserControls/TaskDurations.ascx" %>
<%@ Register TagPrefix="uc" TagName="SaveTaskDuration" Src="~/Main/TaskModule/UserControls/SaveTaskDuration.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProgressBar" Src="~/Main/TaskModule/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectMeetingTime" Src="~/Main/TaskModule/UserControls/SelectMeetingTime.ascx" %>
<%@ Register TagPrefix="uc" TagName="RefusedComment" Src="~/Main/TaskModule/UserControls/RefusedComment.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%#ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">   
	function RedirectToTasksList(arg) { window.location = '<%= UrlsData.LFP_Tasks(PortalSettingsId) %>'; }
	function ShowTaskDurationRadWindow(button, args) { button.set_autoPostBack(false); $find('<%= taskDurationRadWindow.ClientID %>').show(); }
	function CloseRadWindow() { $find('<%= taskDurationRadWindow.ClientID %>').close(); }
	function ShowSelectMeetingTimeRadWindow() { $find('<%= selectMeetingTimeRadWindow.ClientID %>').show(); }
	function CloseSelectMeetingTimeRadWindow() { $find('<%= selectMeetingTimeRadWindow.ClientID %>').close(); }
	function ShowRefusedCommentRadWindow() { $find('<%= refusedCommentRadWindow.ClientID %>').show(); }
	function CloseRefusedCommentRadWindow() { $find('<%= refusedCommentRadWindow.ClientID %>').close(); }
	function OnClientClicking(sender, args) {
	    if (needLogin == true) {
	        NeedLogin();
	        args.set_cancel(true);
	    }
	}
	function OnSaveClientClick() {
	    if (needLogin == true) {
	        NeedLogin();
	        return false;
	    }
	    return true;
	}
</script>
</telerik:RadCodeBlock>

<telerik:RadWindowManager ID="radWindowManager" Skin="Windows7" runat="server" EnableShadow="true"></telerik:RadWindowManager>
<div class="b-block">
    <h4 class="top-radius">Тема: <asp:Literal runat="server" ID="lrlTitle" /></h4>
    <div class="block-content bottom-radius">
	<div class="two-columns">   		
        <div class="row">
			<label>Личный комментарий:</label>
			<asp:TextBox runat="server" ID="txtPersonalComment" CssClass="area-text" Width="640px" Height="30px" TextMode="MultiLine" />
		</div>
		<telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" Skin="Windows7" CssClass="tabs" SelectedIndex="0" runat="server">
			<Tabs>
				<telerik:RadTab Text="Основные данные" />				
				<telerik:RadTab Text="История задачи" Value="rtTaskDurations" Visible="false" />
			</Tabs>
		</telerik:RadTabStrip>
		<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			<telerik:RadPageView ID="RadPageView1" runat="server">
				<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
				
					<telerik:RadWindow runat="server" Title="История исполнения" Width="436px" Height="400px" ID="taskDurationRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
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
							<asp:Literal runat="server" ID="lrlTaskType" />
						</div>
						<div class="row">
							<label>Дата начала:</label>
							<asp:Literal runat="server" ID="lrlStartDate" />
						</div>
						<div class="row">
							<label>Дата окончания:</label>
							<asp:Literal runat="server" ID="lrlEndDate" />
						</div>
						<asp:Panel runat="server" ID="plDateOfControl" CssClass="row">
							<label>Дата контроля:</label>	
                            <asp:Literal runat="server" ID="lrlDateOfControl" />
						</asp:Panel>
						<div class="row">
							<label>Важная задача:</label>
							<asp:CheckBox runat="server" ID="chxIsImportantTask" Enabled="false" />
						</div>
						<div class="row">
							<label>Длительность план, часов:</label>
                            <asp:Literal runat="server" ID="lrlPlanDurationHours" />
						</div>                        
					</div>
					<div class="right-column">
						<div class="row">
							<label>Ответственный:</label>
							<asp:Literal runat="server" ID="lrlResponsible" />
						</div>
						<div class="row">
							<label>Автор:</label>
							<asp:Literal runat="server" ID="lrlCreator" />
						</div>
                        <div class="row">&nbsp;</div>
                        <div class="row">&nbsp;</div>
						<div class="row">
							<label>Срочная задача:</label>
							<asp:CheckBox runat="server" ID="chxIsUrgentTask" Enabled="false" />
						</div>
						<div class="row">
							<label>Длительность план, минут:</label>
                            <asp:Literal runat="server" ID="lrlPlanDurationMinutes" />							
						</div>
					</div>
					<div class="clear"></div>                    
					<h3>Состояние</h3>                    
					<div class="row row-btn">
						<label>Состояние:</label>                        
						<asp:Literal runat="server" ID="lrlTaskStatus" />                            
					</div>
                     <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
                         <asp:Literal runat="server" ID="lrlSuccessMessage" />
                         <div class="row row-btn">
                            <label>Статус:</label>
						    <asp:Literal runat="server" ID="lrlTaskMemberStatus" />
                            <telerik:RadButton runat="server" ID="rbtnSelectMeetingTime" Text="Выбрать время" Skin="Windows7" Visible="false" OnClientClicking="OnClientClicking" OnClick="rbtnSelectMeetingTime_OnClick" />
                            <telerik:RadButton runat="server" ID="rbtnRegistration" Text="Зарегистрироваться" Skin="Windows7" Visible="false" OnClientClicking="OnClientClicking" OnClick="rbtnRegistration_OnClick" />
                            <telerik:RadButton runat="server" ID="rbtnConfirm" CommandArgument="Confirm" Text="Подтвердить участие" Skin="Windows7" Visible="false" OnClick="rbtnMemberStatus_OnClick" />
                            <telerik:RadButton runat="server" ID="rbtnNotInterest" CommandArgument="NotInterest" Text="Участие не интересно" Skin="Windows7" Visible="false" OnClick="rbtnMemberStatus_OnClick" />
                            <telerik:RadButton runat="server" ID="rbtnFailureNoWay" CommandArgument="FailureNoWay" Text="Нет возможности" Skin="Windows7" Visible="false" OnClick="rbtnMemberStatus_OnClick" />
                         </div>
                     </telerik:RadAjaxPanel>
                    <div class="row row-btn">
						<label>Информирован:</label>
						<asp:CheckBox runat="server" ID="chxIsInformed" Enabled="false" />
					</div>
					<asp:Panel runat="server" ID="plDuration" Visible="false">                        
						<div class="left-column">
							<div class="row">
								<label>Длительность факт, часов:</label>
                                <asp:Literal runat="server" ID="lrlActualDurationHours" />
							</div>
							<div class="row row-progressbar clearfix">
								<label>Процент выполнения:</label>								
								<uc:ProgressBar runat="server" ID="ucProgressBar" />
							</div>
						</div>
						<div class="right-column">
							<div class="row">
								<label>Длительность факт, минут:</label>
                                <asp:Literal runat="server" ID="lrlActualDurationMinutes" />								
							</div>
                            <asp:Panel runat="server" ID="plNoteTime" CssClass="row row-btn" Visible="false">
                                <telerik:RadButton runat="server" ID="rbtnNoteTime" Text="Отметить время" Skin="Windows7" OnClientClicked="ShowTaskDurationRadWindow" />
                            </asp:Panel>							
						</div>
						<div class="clear"></div>
					</asp:Panel>
				</telerik:RadAjaxPanel>
			</telerik:RadPageView>			
			<telerik:RadPageView ID="rpvTaskDurations" runat="server" Visible="false">
				<uc:TaskDurations runat="server" ID="ucTaskDurations" Visible="true" />				
			</telerik:RadPageView>
		</telerik:RadMultiPage>
		<br/>
		<div class="buttons clearfix">
			<asp:LinkButton ID="lbtnSave" OnClientClick="return OnSaveClientClick();" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupTask" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		</div>
	</div>
    
    <telerik:RadWindow runat="server" Title="Выбор времени" ID="selectMeetingTimeRadWindow" Width="800" Height="590" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
        <ContentTemplate>
            <div class="select-time-scheduler">
                <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc:SelectMeetingTime runat="server" ID="ucSelectMeetingTime" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br/>
		        <div class="buttons">
			        <asp:HyperLink CssClass="btn" runat="server" NavigateUrl="javascript:;" onclick="javascript:CloseSelectMeetingTimeRadWindow();"><em>&nbsp;</em><span>Закрыть</span></asp:HyperLink>		
		        </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow runat="server" Title="Отказ от участия" ID="refusedCommentRadWindow" Width="470" Height="230" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
        <ContentTemplate>
            <div class="refused-comment">
                <asp:UpdatePanel ID="Updatepanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc:RefusedComment runat="server" ID="ucRefusedComment"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    </div>
</div>
</asp:Content>
