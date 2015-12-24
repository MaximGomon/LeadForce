<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteColumnTooltip.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteColumnTooltip" %>


<telerik:RadAjaxPanel runat="server">
    <div style="padding: 15px">										    
			<asp:Label id="TooltipErrorMessage" runat="server" CssClass="error-message" />
			<div class="row">
				<label>Название:</label>
				<asp:TextBox ID="txtName_SiteColumnValue" CssClass="input-text" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName_SiteColumnValue" ValidationGroup="valGroupTooltip" runat="server">*</asp:RequiredFieldValidator>
			</div>
			<div class="row">
				<label>Код:</label>
				<asp:TextBox ID="txtCode_SiteColumnValue" CssClass="input-text" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCode_SiteColumnValue" ValidationGroup="valGroupTooltip" runat="server">*</asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="Неверный формат кода." ControlToValidate="txtCode_SiteColumnValue" ValidationExpression="^[a-zA-Z0-9_-]+$" ValidationGroup="valGroupTooltip" runat="server" />
			</div>
			<div class="row">
				<label>Категория:</label>
				<asp:DropDownList ID="ddlCategoryID" CssClass="select-text" runat="server"></asp:DropDownList>
			</div>
			<div class="row">
				<label>Тип:</label>
				<asp:DropDownList ID="ddTypeID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddTypeID_SelectedIndexChanged" CssClass="select-text"></asp:DropDownList>
			</div>
			<asp:GridView ID="gvSiteColumnValues"
							Width="480px"
							DataKeyNames="ID"
							OnRowEditing="gvSiteColumnValues_RowEditing"
							OnRowCancelingEdit="gvSiteColumnValues_RowCancelingEdit"
							OnRowUpdating="gvSiteColumnValues_RowUpdating" 
							OnRowDeleting="gvSiteColumnValues_RowDeleting"
							AutoGenerateColumns="False"
							ShowHeader="true"
							ShowFooter="true"
							GridLines="None"
							CssClass="grid"
							Visible="false"
							runat="server">
				<Columns>
					<asp:TemplateField HeaderText="Значение" ItemStyle-Width="230px" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
						<ItemTemplate>
							<%# Server.HtmlEncode(Eval("Value").ToString())%>  
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtValue" CssClass="input-text" runat="server" Text='<%# Eval("Value") %>' />
							<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtValue" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtValue" CssClass="input-text" runat="server" Text="" />
							<asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtValue" ValidationGroup="valGroupInsert" runat="server">*</asp:RequiredFieldValidator>
						</FooterTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Действия" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
						<ItemTemplate>
							<asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Изменить" />
							<span>&nbsp;|&nbsp;</span>
							<asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' Text="Удалить" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton runat="server" ID="Update" Text="Обновить" CommandName="Update" ValidationGroup="valGroup" />
							<span>&nbsp;|&nbsp;</span>
							<asp:LinkButton runat="server" ID="Cancel" Text="Отмена" CommandName="Cancel" />
						</EditItemTemplate>
						<FooterStyle HorizontalAlign="Center" />
						<FooterTemplate>
							<asp:Button ID="BtnAddValue" OnClick="BtnAddValue_Click" Text="Добавить новое значение" ValidationGroup="valGroupInsert" runat="server" />
						</FooterTemplate>
					</asp:TemplateField>
				</Columns>
				<EmptyDataTemplate>
				</EmptyDataTemplate>
			</asp:GridView>
			<br />
			<div class="buttons">
				<asp:LinkButton ID="btnSave" CssClass="btn" OnClick="btnSave_Click" ValidationGroup="valGroupTooltip" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
				<asp:LinkButton ID="btnTooltipCancel" OnClientClick="HideTooltip()" CssClass="cancel" Text="Отмена" runat="server"></asp:LinkButton>
			</div>
        </div>
</telerik:RadAjaxPanel>