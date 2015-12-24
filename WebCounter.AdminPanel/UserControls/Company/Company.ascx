<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Company.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Company" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="~/UserControls/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/CompanyComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Contact" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskList" Src="~/UserControls/Task/TaskList.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactList" Src="~/UserControls/Contact/ContactList.ascx" %>
<%@ Register TagPrefix="uc" TagName="CompanyLegalAccount" Src="~/UserControls/Company/CompanyLegalAccount.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsCompany" ObjectTypeName="tbl_Company" runat="server" />
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
						            ValidationGroup="groupCompany" />
	            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		            <Tabs>
			            <telerik:RadTab Text="Карточка компании" />
			            <telerik:RadTab Text="Контакты" Value="Contacts" />
			            <telerik:RadTab Text="Задачи" Value="Tasks" />
                        <telerik:RadTab Text="Юридические лица" />
		            </Tabs>
	            </telerik:RadTabStrip>

	            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		            <telerik:RadPageView ID="RadPageView1" runat="server">
			            <h3>Основные данные</h3>
			            <div class="left-column">
				            <div class="row">
					            <label>Наименование:</label>
					            <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
					            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели наименование" ValidationGroup="groupCompany" runat="server" />
				            </div>
				            <div class="row">
					            <label>Тип компании:</label>
					            <asp:DropDownList runat="server" ID="ddlCompanyType" CssClass="select-text" />
				            </div>
				            <div class="row">
					            <label>Важность:</label>
					            <asp:Literal runat="server" ID="lrlPriority" />
				            </div>
				            <div class="row">
					            <label>Размер компании:</label>
					            <asp:DropDownList runat="server" ID="ddlCompanySize" CssClass="select-text" />
				            </div>
				            <div class="row">
					            <label>Статус:</label>
					            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="select-text" />
				            </div>
			            </div>
			            <div class="right-column">
				            <div class="row">
					            <label>Входит в холдинг:</label>
					            <uc:Company runat="server" ID="ucParentCompany" />
				            </div>
				            <div class="row">
					            <label>Ответственный:</label>
					            <uc:Contact runat="server" ID="ucOwner" />
				            </div>
				            <div class="row">
					            <label>Готовность к продаже:</label>
					            <asp:Literal runat="server" ID="lrlReadyToSell" />
				            </div>
				            <div class="row">
					            <label>Отрасль:</label>
					            <asp:DropDownList runat="server" ID="ddlCompanySector" CssClass="select-text" />
				            </div>
			            </div>
			            <div class="clear"></div>
			            <div class="left-column">
				            <h3>Средства связи</h3>
				            <div class="row">
					            <label>Телефон 1:</label>
					            <asp:TextBox ID="txtPhone1" CssClass="input-text" runat="server" />
				            </div>
				            <div class="row">
					            <label>Телефон 2:</label>
					            <asp:TextBox ID="txtPhone2" CssClass="input-text" runat="server" />
				            </div>
				            <div class="row">
					            <label>Факс:</label>
					            <asp:TextBox ID="txtFax" CssClass="input-text" runat="server" />
				            </div>
				            <div class="row">
					            <label>Web:</label>
					            <asp:TextBox ID="txtWeb" CssClass="input-text" runat="server" />
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
				            <h3>Статистика</h3>
				            <div class="row">
					            <label>Дата создания:</label>
					            <asp:Literal runat="server" ID="lrlCreatedAt" />
				            </div>
				            <div class="row">
					            <label>Общий бал:</label>
					            <asp:Literal runat="server" ID="lrlScore" />
				            </div>
				            <div class="row">
					            <label>Балл по поведению:</label>
					            <asp:Literal runat="server" ID="lrlBehaviorScore" />
				            </div>
				            <div class="row">
					            <label>Балл по характеристикам:</label>
					            <asp:Literal runat="server" ID="lrlCharacteristicsScore" />
				            </div>
			            </div>
			            <div class="clear"></div>
			            <div class="left-column">
				            <h3>Местоположение</h3>
				            <uc:Address runat="server" ID="ucLocationAddress" />
			            </div>
			            <div class="right-column">
				            <h3>Почтовый адрес</h3>
				            <uc:Address runat="server" ID="ucPostalAddress" />
			            </div>
			            <div class="clear"></div>
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView4" runat="server">
			            <uc:ContactList runat="server" ID="ucContactList" />
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView2" runat="server">
			            <uc:TaskList runat="server" ID="ucTaskList" />
		            </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
			            <uc:CompanyLegalAccount runat="server" ID="ucCompanyLegalAccount" />
		            </telerik:RadPageView>
	            </telerik:RadMultiPage>
	            <br/>
	            <div class="buttons">
		            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupCompany" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	            </div>
            </div>
        </td>
    </tr>
</table>