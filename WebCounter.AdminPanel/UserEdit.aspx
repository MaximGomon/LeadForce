<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="WebCounter.AdminPanel.UserEdit" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsUser" ObjectTypeName="tbl_User" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
	            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						            CssClass="validation-summary"
						            runat="server"
						            EnableClientScript="true"
						            HeaderText="Заполните все поля корректно:"
						            ValidationGroup="valSave" />

	            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		            <Tabs>
			            <telerik:RadTab Text="Карточка пользователя" />
		            </Tabs>
	            </telerik:RadTabStrip>

	            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		            <telerik:RadPageView ID="RadPageView1" CssClass="clearfix" runat="server">
                        <h3>Личные данные</h3>
                        <div class="row">
                            <label>Контакт:</label>
                            <telerik:RadComboBox ID="rcbContact" OnItemDataBound="rcbContact_OnItemDataBound" HighlightTemplatedItems="true" Filter="Contains" Width="550px" EnableEmbeddedSkins="false" Skin="Labitec" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                        <li class="col1">Ф.И.О.</li>
                                        <li class="col2">Email</li>
                                        <li class="col3">Телефон</li>
                                    </ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <ul>
                                        <li class="col1"><%# DataBinder.Eval(Container.DataItem, "UserFullName") %></li>
                                        <li class="col2"><%# DataBinder.Eval(Container.DataItem, "Email") %></li>
                                        <li class="col3"><%# DataBinder.Eval(Container.DataItem, "Phone") %></li>
                                    </ul>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rcbContact" InitialValue="Выберите контакт" Text="*" ErrorMessage="Вы не выбрали контакт" ValidationGroup="valSave" runat="server" />
                        </div>
                        <h3>Доступ к системе</h3>
                        <div class="row">
                            <label>Email (логин):</label>
                            <asp:TextBox ID="txtEmail" CssClass="input-text" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail" Text="*" ErrorMessage="Вы не ввели логин" ValidationGroup="valSave" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ErrorMessage="Вы ввели неправильный Email (логин)" ValidationGroup="valSave" runat="server">*</asp:RegularExpressionValidator>
                        </div>
                        <div class="row">
                            <label>Пароль:</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="input-text" autocomplete="off" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword" Text="*" ErrorMessage="Вы не ввели пароль" ValidationGroup="valSave" runat="server" />
                        </div>
                        <div class="row">
                            <label>Активен:</label>
                            <asp:CheckBox ID="cbIsActive" runat="server" />
                        </div>
                        <asp:Panel ID="pnlSite" CssClass="row" Visible="false" runat="server">
                            <label>Сайт:</label>
                            <uc:DictionaryComboBox ID="dcbSite" DictionaryName="tbl_Sites" ValidationGroup="valSave" ValidationErrorMessage="Вы не выбрали сайт" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server" />
                        </asp:Panel>
                        <div class="row">
                            <label>Уровень доступа:</label>
                            <asp:DropDownList ID="ddlAccessLevel" CssClass="select-text" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlAccessLevel" Text="*" ErrorMessage="Вы не ввели пароль" ValidationGroup="valSave" runat="server" />
                        </div>
                        <div class="row">
                            <label>Профиль:</label>
                            <telerik:RadComboBox ID="rcbAccessProfile" Filter="Contains" Width="234px" EnableEmbeddedSkins="false" Skin="Labitec" runat="server" />
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <br />
	            <div class="buttons">
		            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="valSave" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	            </div>
            </td>
        </tr>
    </table>
</asp:Content>