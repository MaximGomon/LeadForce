<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveWorkflowTemplateConditionEvent.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.SaveWorkflowTemplateConditionEvent" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>

<div class="edit-window">
    <div class="row">
        <label>Категория:</label>
        <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="ddlCategory" ErrorMessage="Вы не выбрали категорию" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
    </div>
    <asp:Panel ID="pnlActivity" Visible="false" runat="server">
        <div class="row">
            <label>Тип действия:</label>
            <asp:DropDownList ID="ddlActivityType" OnSelectedIndexChanged="ddlActivityType_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="ddlActivityType" ErrorMessage="Вы не выбрали тип действия" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
        </div>
        <div class="row">
            <label>URL или код:</label>
            <asp:Panel ID="pnlComboBoxCode" CssClass="inline" runat="server">
                <telerik:RadComboBox runat="server" ID="rcbCode" Skin="Labitec" EnableEmbeddedSkins="false" EmptyMessage="URL или код" ValidationGroup="groupUpdateConditionEvent" Width="234px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rcbCode" ErrorMessage="Вы не ввели URL или код" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
            </asp:Panel>
            <asp:Panel ID="pnlTextBoxCode" CssClass="inline" Visible="False" runat="server">
                <asp:TextBox ID="txtCode" CssClass="input-text" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode" ErrorMessage="Вы не ввели URL или код" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlParameter" Visible="False" runat="server">
            <div class="row">
                <label>Параметр:</label>
                <asp:TextBox ID="txtParameter" CssClass="input-text" runat="server" />
            </div>
        </asp:Panel>
        <div class="row">
            <label>Период актуальности, дней:</label>
            <telerik:RadNumericTextBox ID="txtActualPeriod" Type="Number" CssClass="input-text" runat="server">
                <NumberFormat GroupSeparator="" AllowRounding="false" />
            </telerik:RadNumericTextBox>
            <asp:RequiredFieldValidator ControlToValidate="txtActualPeriod" ErrorMessage="Вы не ввели период актуальности" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlColumnValue" Visible="false" runat="server">
        <div class="row">
            <label>Реквизит:</label>
            <uc:DictionaryComboBox ID="dcbSiteColumns" OnSelectedIndexChanged="dcbSiteColumns_OnSelectedIndexChanged" AutoPostBack="true" DictionaryName="tbl_SiteColumns" DataTextField="Name" ShowEmpty="true" ValidationGroup="groupUpdateConditionEvent" ValidationErrorMessage="Вы не выбрали реквизит" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlBehaviorScore" Visible="false" runat="server">
        <div class="row">
            <label>Реквизит:</label>
            <uc:DictionaryComboBox ID="dbcSiteActivityScoreType" DictionaryName="tbl_SiteActivityScoreType" DataTextField="Title" ShowEmpty="true" ValidationGroup="groupUpdateConditionEvent" ValidationErrorMessage="Вы не выбрали реквизит" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlFormula" CssClass="row" Visible="false" runat="server">
        <label>Формула:</label>
        <asp:DropDownList ID="ddlFormula" OnSelectedIndexChanged="ddlFormula_OnSelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server" />
        <asp:RequiredFieldValidator ControlToValidate="ddlFormula" ErrorMessage="Вы не выбрали формулу" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
    </asp:Panel>
    <asp:Panel ID="pnlValue" CssClass="row" Visible="false" runat="server">
        <label>Значение:</label>
        <asp:TextBox ID="txtValue" CssClass="input-text" Visible="false" runat="server" />
        <asp:RequiredFieldValidator ID="reqValue" ControlToValidate="txtValue" ErrorMessage="Вы не ввели значение" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
        <telerik:RadNumericTextBox ID="txtValueNumeric" Type="Number" CssClass="input-text" Visible="false" runat="server">
            <NumberFormat GroupSeparator="" AllowRounding="false" />
        </telerik:RadNumericTextBox>
        <asp:RequiredFieldValidator ID="reqValueNumeric" ControlToValidate="txtValueNumeric" ErrorMessage="Вы не ввели значение" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
        <telerik:RadDatePicker ID="txtValueDate" ShowPopupOnFocus="true" Width="110" Visible="false" runat="server" />
        <asp:RequiredFieldValidator ID="reqValueDate" ControlToValidate="txtValueDate" ErrorMessage="Вы не выбрали значение" ValidationGroup="groupUpdateConditionEvent" runat="server">*</asp:RequiredFieldValidator>
        <uc:DictionaryComboBox ID="txtValueEnum" DictionaryName="tbl_SiteColumnValues" DataTextField="Value" ShowEmpty="true" Visible="false" ValidationGroup="groupUpdateConditionEvent" ValidationErrorMessage="Вы не выбрали значение" runat="server" />
    </asp:Panel>
    <div class="buttons clearfix">
	    <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateConditionEvent" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
	    <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
    </div>
</div>