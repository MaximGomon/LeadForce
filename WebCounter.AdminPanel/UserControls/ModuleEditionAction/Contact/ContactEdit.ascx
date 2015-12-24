<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.Contact.ContactEdit" %>

<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/CompanyComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserControls/Address.ascx" %>


<telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
<script type="text/javascript">
    function HideTooltip() {
        var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
        if (tooltip) tooltip.hide();
    }

    function ShowFIOTooltip() {
        var tooltip = $find("rttCheckFIO");
        tooltip.show();
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
				<label>Компания:</label>
				<uc:Company runat="server" ID="ucCompany" AllowCustomText="true" />
			</div>
			<div class="row">
				<label>Ответственный:</label>
				<uc:Contact runat="server" ID="ucOwner" />
			</div>
		</div>
		<div class="right-column">
			<div class="row">
				<label>Должность:</label>
				<asp:TextBox runat="server" CssClass="input-text" ID="txtJobTitle" />
			</div>
            <div class="row">
				<label>Дата рождения:</label>
				<telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpBirthday" Width="100px">
					<DateInput Enabled="true" />
					<DatePopupButton Enabled="true" />
				</telerik:RadDatePicker>
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
                <asp:HyperLink ID="cellularPhoneStatus" Visible="false" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</asp:HyperLink>
			</div>
			<div class="row">
				<label>Email:</label>
				<asp:TextBox ID="txtEmail" CssClass="input-text" runat="server" />
                <asp:HyperLink ID="emailStatus" Visible="false" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</asp:HyperLink>
			</div>
			<div class="row">
				<label>Web сайт:</label>
				<asp:TextBox ID="txtWeb" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Имя Facebook:</label>
				<asp:TextBox ID="txtFacebook" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Имя VKontakte:</label>
				<asp:TextBox ID="txtVKontakte" CssClass="input-text" runat="server" />
			</div>
			<div class="row">
				<label>Имя Twitter:</label>
				<asp:TextBox ID="txtTwitter" CssClass="input-text" runat="server" />
			</div>
		</div>
		<div class="right-column">
		<div class="right-column">
			<h3>Почтовый адрес</h3>
			<uc:Address runat="server" ID="ucPostalAddress" />
		</div>
		</div>
		<div class="clear"></div>
		<div class="left-column">
			<h3>Источник клиента</h3>
			<div class="row">
				<label>Кто рекомендовал:</label>
                <uc:Contact ID="ucReffer" runat="server" />
			</div>   
			<div class="row">
				<label>URL источника:</label>
				<asp:Literal runat="server" ID="lrlRefferURL" />
			</div>
            <div class="row">
				<label>Рекламная площадка:</label>
                <uc:DictionaryComboBox ID="dcbAdvertisingPlatform" AllowCustomText="true" DictionaryName="tbl_AdvertisingPlatform" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
            <div class="row">
				<label>Тип рекламы:</label>
				<uc:DictionaryComboBox ID="dcbAdvertisingType" AllowCustomText="true" DictionaryName="tbl_AdvertisingType" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
            <div class="row">
				<label>Акция:</label>
				<uc:DictionaryComboBox ID="dcbAdvertisingCampaign" AllowCustomText="true" DictionaryName="tbl_AdvertisingCampaign" DaTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			</div>
		</div>
		<div class="right-column">
			<h3>Статистика</h3>
			<div class="row">
				<label>Дата создания:</label>
				<asp:Literal runat="server" ID="lrlCreatedAt" />
			</div>
			<div class="row">
				<label>Последняя активность на сайте:</label>
				<asp:Literal runat="server" ID="lrlLastActivity" />
			</div>
			<div class="row">
				<label>IP пользователя:</label>
				<asp:Literal runat="server" ID="lrlUserIP" />
			</div>
			<div class="row">
				<label>Количество посещений сайта:</label>
				<asp:Literal runat="server" ID="lrlScore" />
			</div>
		</div>
		<div class="clear"></div>        
	</ContentTemplate>
</asp:UpdatePanel>

<telerik:RadToolTipManager ID="rttmCheckFIO" EnableEmbeddedScripts="true" ShowEvent="OnClick" OffsetY="-1" HideEvent="ManualClose" Modal="true"
    runat="server" EnableShadow="true" ManualCloseButtonText="Закрыть" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="Element"
    Position="MiddleRight">                                
</telerik:RadToolTipManager>