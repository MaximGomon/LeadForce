<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralInformation.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard.GeneralInformation" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<div class="wizard-step">
    <div class="row">
        <label>Название процесса:</label>
        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Вы не ввели 'Название процесса'" ValidationGroup="GeneralInformation" runat="server">*</asp:RequiredFieldValidator>
    </div>
    <div class="row">
        <label>Состояние:</label>
        <asp:DropDownList ID="ddlStatus" CssClass="select-text" runat="server" />
    </div>

    <h3>Роли в процессе</h3>
    <telerik:RadGrid ID="rgContactRoles" AutoGenerateColumns="False" OnNeedDataSource="rgContactRoles_OnNeedDataSource" OnItemDataBound="rgContactRoles_OnItemDataBound" Skin="Windows7" runat="server">
        <MasterTableView DataKeyNames="ContactRoleID, RoleInTemplate">
            <Columns>
                <telerik:GridBoundColumn HeaderText="Роль в шаблоне" DataField="RoleInTemplate" />
                <telerik:GridBoundColumn HeaderText="Описание роли" DataField="Description" />
                <telerik:GridTemplateColumn HeaderText="Роль в процессе" HeaderStyle-Width="550px">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 250px;">
                                    <label>Отправитель:</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbContactRole" AutoPostBack="True" Width="234px" DictionaryName="tbl_ContactRole" DataTextField="Title" ShowEmpty="True" EmptyItemText="Конкретные Email и имя" CssClass="select-text" OnSelectedIndexChanged="dcbContactRole_OnSelectedIndexChanged" OnItemsRequested="dcbContactRole_ItemsRequested" ValidationErrorMessage="Вы не выбрали 'Отправитель'" runat="server" />
                                </td>
                                <td style="width: 250px;">&nbsp;</td>
                            </tr>
                            <tr>
                                <asp:Panel ID="pnlFrom" runat="server">
                                <td>
                                    <label>Email отправителя:</label>
                                    <asp:TextBox ID="txtFromEmail" CssClass="input-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvFromEmail" ControlToValidate="txtFromEmail" ErrorMessage="Вы не ввели 'Email отправителя'" ValidationGroup="GeneralInformation" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revFromEmail" ErrorMessage="Неверный формат Email." ControlToValidate="txtFromEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ValidationGroup="GeneralInformation" runat="server">*</asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <label>Имя отправителя:</label>
                                    <asp:TextBox ID="txtFromName" CssClass="input-text" runat="server" />
                                </td>
                                </asp:Panel>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
             </Columns>
             <NoRecordsTemplate>
                 <div style="padding: 10px; text-align: center;">
                     Нет записей.
                 </div>
             </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
    
    <br />
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="GeneralInformation"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>