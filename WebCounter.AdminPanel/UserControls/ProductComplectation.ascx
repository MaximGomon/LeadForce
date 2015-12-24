<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductComplectation.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ProductComplectation" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	<telerik:RadGrid ID="rgProductComplectation" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgProductComplectation_NeedDataSource" OnDeleteCommand="rgProductComplectation_DeleteCommand"
		OnInsertCommand="rgProductComplectation_InsertCommand" OnUpdateCommand="rgProductComplectation_UpdateCommand" OnItemDataBound="rgProductComplectation_OnItemDataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings CaptionFormatString="Комплектация">
			    <FormTableStyle CssClass="editmode-popup"/>
                <FormTableItemStyle CssClass="editmode-popup-item" />
                <FormTableAlternatingItemStyle CssClass="editmode-popup-item" />
			    <FormTableButtonRowStyle CssClass="editmode-popup-buttons" />
				<EditColumn ButtonType="ImageButton" />
                <PopUpSettings Modal="true" ShowCaptionInEditForm="false" />
			</EditFormSettings>
			<Columns>                                    
				<telerik:GridTemplateColumn HeaderText="Продукт" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("Title").ToString())%>                    
					</ItemTemplate>
					<InsertItemTemplate>
                        <uc:DictionaryComboBox ID="dcbProducts" DictionaryName="tbl_Product" ShowEmpty="true" CssClass="select-text" runat="server" ValidationErrorMessage="Укажите продукт" DataTextField="Title" DataValueField="ID"/>
					</InsertItemTemplate>
					<EditItemTemplate>
                        <uc:DictionaryComboBox ID="dcbProducts" DictionaryName="tbl_Product" ShowEmpty="true" CssClass="select-text" runat="server" ValidationErrorMessage="Укажите продукт" DataTextField="Title" DataValueField="ID"/>
					</EditItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Количество">
					<ItemTemplate>
                        <%# Eval("Quantity")%>
					</ItemTemplate>
					<InsertItemTemplate>
						<telerik:RadNumericTextBox ID="txtQuantity" runat="server" EmptyMessage="0" />
					</InsertItemTemplate>
					<EditItemTemplate>
						<telerik:RadNumericTextBox ID="txtQuantity" runat="server" Value='<%# double.Parse(Eval("Quantity").ToString()) %>' />
					</EditItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Цена">
					<ItemTemplate>
                        <%# ((decimal)Eval("Price")).ToString("F") %>
					</ItemTemplate>
					<InsertItemTemplate>
						<telerik:RadNumericTextBox ID="txtPrice" runat="server" EmptyMessage="0" />
					</InsertItemTemplate>
					<EditItemTemplate>
						<telerik:RadNumericTextBox ID="txtPrice" runat="server" Value='<%# double.Parse(Eval("Price").ToString()) %>' />
					</EditItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>            
		</MasterTableView>
        <ClientSettings>
            <ClientEvents OnPopUpShowing="PopUpShowing" />
        </ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>