<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainTaskMember.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.MainTaskMember" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderProductsComboBox" Src="~/UserControls/Order/OrderProductsComboBox.ascx" %>

    <div class="left-column">
        <div class="row row-dictionary">
	        <label>Контрагент:</label>
	        <uc:DictionaryComboBox runat="server" ID="dcbContractor" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" Width="235px" AutoPostBack="true" OnSelectedIndexChanged="dcbContractor_OnSelectedIndexChanged" />
        </div>
        <div class="row">
	        <label>Роль:<span class="required-field">*</span></label>
	        <asp:DropDownList runat="server" ID="ddlTaskMemberRole" CssClass="select-text"/>	        
        </div>
    </div>
    <div class="right-column">
        <div class="row">
	        <label>Контакт:<span class="required-field">*</span></label>
	        <uc:ContactComboBox ID="ucContact" CssClass="select-text" runat="server" FilterByFullName="true" AutoPostBack="true" OnSelectedIndexChanged="ucContact_OnSelectedIndexChanged"/>
        </div>
        <div class="row">
	        <label>Статус:</label>
	        <asp:Literal runat="server" ID="lrlTaskMemberStatus" Text="План" />
        </div>
        <div class="row">
			<label>Информирован:</label>
            <asp:CheckBox runat="server" ID="chxIsInformed" />
		</div>
    </div>
    <div class="clear"></div>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <asp:Panel runat="server" ID="plCompanyPay" Visible="false">                                
            <div class="left-column">
                <div class="row row-dictionary">
				    <label>Заказ:</label>
				    <uc:DictionaryComboBox runat="server" ID="dcbOrder" AutoPostBack="true" OnSelectedIndexChanged="dcbOrder_OnSelectedIndexChanged" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" />
			    </div>
            </div>
            <div class="right-column">
                <div class="row row-dictionary">
				    <label>Продукт в заказе:</label>
				    <uc:OrderProductsComboBox runat="server" ID="ucOrderProduct" />
			    </div>
            </div>
            <div class="clear"></div>
        </asp:Panel>
    </telerik:RadAjaxPanel>
	<div class="row">
		<label>Комментарий:</label>
		<asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="area-text" Width="630px" />
	</div>
	<div class="row">
		<label>Комментарий пользователя:</label>
		<asp:TextBox runat="server" ID="txtUserComment" TextMode="MultiLine" CssClass="area-text" Width="630px" />
	</div>