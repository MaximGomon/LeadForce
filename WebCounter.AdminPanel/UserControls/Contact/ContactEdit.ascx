<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ContactEdit" %>

<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/CompanyComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserControls/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>


<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>

<telerik:RadScriptBlock runat="server">
<script type="text/javascript">
	function HideTooltip() {
		var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
		if (tooltip) tooltip.hide();
	}

	function ShowFIOTooltip() {
		var tooltip = $find("rttCheckFIO");	    
		tooltip.show();
    }

    function confirmCallBackFn(arg) {
        if (arg == true)
            $find('<%= radAjaxManager.ClientID %>').ajaxRequest("AddCompany");
        if (arg == false)
            $find('<%= radAjaxManager.ClientID %>').ajaxRequest("CancelAddCompany");
    }

</script>
</telerik:RadScriptBlock>

<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
		<h3>Основные данные</h3>
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<div class="row fio">
					<label>Ф.И.О.:</label>
					<asp:TextBox runat="server" CssClass="input-text" ID="txtUserFullName" Width="386px" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserFullName" ValidationGroup="groupAddContact" runat="server">*</asp:RequiredFieldValidator>
					<asp:LinkButton ID="lbtnConfirm" CssClass="btn" runat="server" Visible="false"><em>&nbsp;</em><span>Подтвердить корректность</span></asp:LinkButton>
					<asp:ImageButton runat="server" CssClass="img-btn" ID="imgbtnOk" ImageUrl="~/App_Themes/Default/images/btnOk.png" Visible="false" />
				</div>                
			</ContentTemplate>
		</asp:UpdatePanel>
		<div class="left-column">
			<div class="row">
				<label>Тип контакта:</label>
				<asp:DropDownList runat="server" ID="ddlContactType" CssClass="select-text" />
			</div>
			<div class="row row-img">
				<label>Важность:</label>
				<asp:Literal runat="server" ID="lrlPriority" /> <asp:Image ID="imgPriority" Visible="False" runat="server"/>
			</div>			
		</div>
		<div class="right-column">
			<div class="row">
				<label>Ответственный:</label>
				<uc:Contact runat="server" ID="ucOwner" />
			</div>
			<div class="row row-img">
				<label>Готовность к продаже:</label>
				<asp:Literal runat="server" ID="lrlReadyToSell" /> <asp:Image ID="imgReadyToSell" Visible="False" runat="server"/>
			</div>
		</div>
		<div class="clear"></div>
        <div class="row">
            <label>Комментарий:</label>
            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="area-text" ID="txtComment" Width="762px" Height="30px" />
        </div>
		<h3>Место работы</h3>
		<div class="left-column">
			<div class="row">
				<label>Компания:</label>
				<uc:Company runat="server" ID="ucCompany" AllowCustomText="true" />
			</div>
			<div class="row">
				<label>Должность:</label>
				<asp:TextBox runat="server" CssClass="input-text" ID="txtJobTitle" />
			</div>
		</div>
		<div class="right-column">
			<div class="row">
				<label>Функция в компании:</label>
				<asp:DropDownList runat="server" ID="ddlFunctionInCompany" CssClass="select-text" />
			</div>
			<div class="row">
				<label>Уровень должности:</label>
				<asp:DropDownList runat="server" ID="ddlJobLevel" CssClass="select-text" />
			</div>
		</div>
		<div class="clear"></div>
		<div class="left-column">
			<h3>Средства связи</h3>
			<div class="row">
				<label>Телефон:</label>
				<asp:TextBox ID="txtPhone" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Сотовый:</label>
				<asp:TextBox ID="txtCellularPhone" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Статус сотового:</label>
				<asp:DropDownList runat="server" ID="ddlCellularPhoneStatus" CssClass="select-text" />
			</div>				        
			<div class="row">
				<label>Email:</label>
				<asp:TextBox ID="txtEmail" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Статус email:</label>
				<asp:DropDownList runat="server" ID="ddlEmailStatus" CssClass="select-text" />
			</div>
		</div>
		<div class="right-column">
			<h3>Почтовый адрес</h3>
			<uc:Address runat="server" ID="ucPostalAddress" />
		</div>
		<div class="clear"></div>
		<div class="left-column">
			<h3>Маркетинговые данные</h3>
            <div class="row">
				<label>Дата рождения:</label>
				<telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpBirthday" Width="100px">
					<DateInput Enabled="true" />
					<DatePopupButton Enabled="true" />
				</telerik:RadDatePicker>
			</div>
            <div class="row">
				<label>Рекламная площадка:</label>
                <uc:DictionaryComboBox ID="dcbAdvertisingPlatform" AllowCustomText="true" MaxHeight="300px" DictionaryName="tbl_AdvertisingPlatform" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
            <div class="row">
				<label>Тип рекламы:</label>
				<uc:DictionaryComboBox ID="dcbAdvertisingType" AllowCustomText="true" DictionaryName="tbl_AdvertisingType" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
            <div class="row">
				<label>Рекламная кампания:</label>
				<uc:DictionaryComboBox ID="dcbAdvertisingCampaign" AllowCustomText="true" DictionaryName="tbl_AdvertisingCampaign" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
            <div class="row">
                <label>Ключевые слова:</label>
                <asp:Literal runat="server" ID="lrlKeywords" />
            </div>
			<div class="row">
				<label>URL источника:</label>				
                <asp:Label ID="lblRefferURL" runat="server"></asp:Label>
			</div>
			<div class="row">
				<label>Рекомендовал:</label>
                <uc:Contact ID="ucReffer" runat="server" />
			</div>   
		</div>
		<div class="right-column">
			<h3>Статистика</h3>
			<div class="row">
				<label>Дата создания:</label>
				<asp:Literal runat="server" ID="lrlCreatedAt" />
			</div>
			<div class="row">
				<label>Последняя активность:</label>
				<asp:Literal runat="server" ID="lrlLastActivity" />
			</div>
			<div class="row">
				<label>IP пользователя:</label>
				<asp:Literal runat="server" ID="lrlUserIP" />
			</div>
			<div class="row">
				<label>Общий балл:</label>
				<asp:Literal runat="server" ID="lrlScore" />
			</div>
		</div>
		<div class="clear"></div>        
	</ContentTemplate>
</asp:UpdatePanel>

<telerik:RadToolTipManager ID="rttmCheckFIO" EnableEmbeddedScripts="true" ShowEvent="OnClick" HideEvent="ManualClose" Modal="true"
    runat="server" EnableShadow="true" ManualCloseButtonText="Закрыть" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="BrowserWindow" Position="Center">
</telerik:RadToolTipManager>

 <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" HideEvent="LeaveTargetAndToolTip" RelativeTo="Element" Skin="Windows7" HideDelay="3000" Width="800px">
     <TargetControls>
            <telerik:ToolTipTargetControl TargetControlID="lblRefferURL"></telerik:ToolTipTargetControl>
    </TargetControls>
    </telerik:RadToolTipManager>