<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductPrice.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ProductPrice" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Enumerations" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	<telerik:RadGrid ID="rgProductPrice" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgProductPrice_NeedDataSource" OnDeleteCommand="rgProductPrice_DeleteCommand"
		OnInsertCommand="rgProductPrice_InsertCommand" OnUpdateCommand="rgProductPrice_UpdateCommand" OnItemDataBound="rgProductPrice_OnItemDataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings CaptionFormatString="Цены и акции">
			    <FormTableStyle CssClass="editmode-popup"/>
			    <FormTableItemStyle CssClass="editmode-popup-item" />
                <FormTableAlternatingItemStyle CssClass="editmode-popup-item" />
			    <FormTableButtonRowStyle CssClass="editmode-popup-buttons" />
				<EditColumn ButtonType="ImageButton" />
                <PopUpSettings Modal="true" ShowCaptionInEditForm="false" Height="425px" />
			</EditFormSettings>
			<Columns>                                    
				<telerik:GridTemplateColumn HeaderText="Прайс лист" UniqueName="PriceListTitle">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("PriceListTitle").ToString())%>
					</ItemTemplate>
					<InsertItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlPriceList" CssClass="select-text"/>   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPriceList" CssClass="input-text" Text="*" ErrorMessage='Укажите прайс лист'/>
					</InsertItemTemplate>
					<EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlPriceList" CssClass="select-text"/>                                            
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPriceList" CssClass="input-text" Text="*" ErrorMessage='Укажите прайс лист'/>                                         
					</EditItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Поставщик" UniqueName="SupplierTitle">
					<ItemTemplate>
                        <%# Eval("SupplierTitle")%>
					</ItemTemplate>
					<InsertItemTemplate>
                        <uc:DictionaryComboBox ID="dcbSupplier" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server" ValidationErrorMessage="Укажите поставщика"/>
					</InsertItemTemplate>
					<EditItemTemplate>
                        <uc:DictionaryComboBox ID="dcbSupplier" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server" ValidationErrorMessage="Укажите поставщика"/>
					</EditItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderText="Дата с" DataField="DateFrom" DataFormatString="{0:dd.MM.yyyy}" UniqueName="DateFrom"/>
                <telerik:GridDateTimeColumn HeaderText="Дата по" DataField="DateTo" DataFormatString="{0:dd.MM.yyyy}" UniqueName="DateTo"/>
                <telerik:GridNumericColumn HeaderText="Количество с" DataField="QuantityFrom" DataType="System.Double" UniqueName="QuantityFrom"/>
                <telerik:GridNumericColumn HeaderText="Количество по" DataField="QuantityTo" DataType="System.Double" UniqueName="QuantityTo"/>
                <telerik:GridNumericColumn HeaderText="Процент скидки" DataField="Discount" DataType="System.Double" UniqueName="Discount"/>
                <telerik:GridNumericColumn HeaderText="Цена" DataField="Price" DataType="System.Double" UniqueName="Price"/>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
		</MasterTableView>
        <ClientSettings>
            <ClientEvents OnPopUpShowing="PopUpShowingTop" />
        </ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>