<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaveWorkflowTemplateElement.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.SaveWorkflowTemplateElement" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateElementConditionEvent" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateElementConditionEvent.ascx" %>
<%@ Register TagPrefix="uc" TagName="WorkflowTemplateElementResult" Src="~/UserControls/WorkflowTemplate/WorkflowTemplateElementResult.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>

<%@ Import Namespace="Telerik.Web.UI" %>

<div class="edit-window two-columns">
    
	<telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" AutoPostBack="True" SelectedIndex="0" runat="server">
		<Tabs>
			<telerik:RadTab Text="Основные данные" />
            <telerik:RadTab Text="Настройка элемента" Value="ElementSettings" Visible="false" />
			<telerik:RadTab Text="Результаты элемента процесса" Value="ResultElementProcess" Visible="false" />
            <telerik:RadTab Text="Параметры элемента" Value="ElementParameters" Visible="false" />
		</Tabs>
	</telerik:RadTabStrip>
    
        <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <div class="left-column">
                    <div class="row">
                        <label>Название:</label>
                        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" ErrorMessage="Вы не ввели 'Название'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Тип элемента:</label>
                        <asp:DropDownList ID="ddlElementType" AutoPostBack="true" OnSelectedIndexChanged="ddlElementType_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlElementType" ErrorMessage="Вы не выбрали 'Тип элемента'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Отразить текущему пользователю:</label>
                        <asp:CheckBox ID="cbShowCurrentUser" runat="server" />
                    </div>
                </div>
                <div class="right-column">

                    <div class="row">
                        <label>Произвольный элемент:</label>
                        <asp:CheckBox ID="cbOptional" AutoPostBack="true" OnCheckedChanged="cbOptional_OnCheckedChanged" runat="server" />
                    </div>
                    <asp:Panel ID="pnlResultName" Visible="false" CssClass="row" runat="server">
                        <label>Название результата:</label>
                        <asp:TextBox ID="txtResultName" CssClass="input-text" runat="server" />
                    </asp:Panel>
                    <div class="row">
                        <label>Допустим произвольный переход:</label>
                        <asp:CheckBox ID="cbAllowOptionalTransfer" runat="server" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="row">
                    <label>Описание:</label>
                    <asp:TextBox ID="txtDescription" CssClass="area-text" TextMode="MultiLine" Width="685px" runat="server" />
                </div>

                <h3>Начало элемента с завершения предыдущего</h3>
                <div class="left-column">
                    <div class="row">
                        <label>Запланировать через:</label>
                        <telerik:RadNumericTextBox ID="txtStartAfter" Type="Number" CssClass="input-text" Value="0" runat="server">
                            <NumberFormat GroupSeparator="" AllowRounding="false" />
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtStartAfter" Display="Dynamic" ErrorMessage="Вы не ввели 'Запланировать через'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="right-column">
                    <div class="row">
                        <label>Период:</label>
                        <asp:DropDownList ID="ddlStartPeriod" CssClass="select-text" runat="server">
                            <asp:ListItem Text="Минуты" Value="0" />
                            <asp:ListItem Text="Часы" Value="1" />
                            <asp:ListItem Text="Дни" Value="2" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlStartPeriod" ErrorMessage="Вы не выбрали 'Период'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="clear"></div>
    
                <asp:Panel ID="pnlTypicalDuration" Visible="false" runat="server">
                    <h3>Длительность</h3>
                    <div class="left-column">
                        <div class="row">
                            <label>Длительность план, часов:</label>
                            <telerik:RadNumericTextBox ID="txtDurationHours" Type="Number" CssClass="input-text" Value="0" runat="server">
                                <NumberFormat GroupSeparator="" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtDurationHours" Display="Dynamic" ErrorMessage="Вы не ввели 'Длительность план, часов'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="right-column">
                        <div class="row">
                            <label>Длительность план, минут:</label>
                            <telerik:RadNumericTextBox ID="txtDurationMinutes" Type="Number" CssClass="input-text" Value="0" runat="server">
                                <NumberFormat GroupSeparator="" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtDurationMinutes" Display="Dynamic" ErrorMessage="Вы не ввели 'Длительность план, минут'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clear"></div>
                </asp:Panel>
    
                <asp:Panel ID="pnlRequiredControl" Visible="false" runat="server">
                    <h3>Дата контроля</h3>
                    <div class="left-column">
                        <div class="row">
                            <label>Запланировать через:</label>
                            <telerik:RadNumericTextBox ID="txtControlAfter" Type="Number" CssClass="input-text" Value="0" runat="server">
                                <NumberFormat GroupSeparator="" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtControlAfter" Display="Dynamic" ErrorMessage="Вы не ввели 'Запланировать через'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="row">
                            <label>Рассчитывать от начала процесса:</label>
                            <asp:CheckBox ID="cbControlFromBeginProccess" runat="server" />
                        </div>
                    </div>
                    <div class="right-column">
                        <div class="row">
                            <label>Период:</label>
                            <asp:DropDownList ID="ddlControlPeriod" CssClass="select-text" runat="server">
                                <asp:ListItem Text="Минуты" Value="0" />
                                <asp:ListItem Text="Часы" Value="1" />
                                <asp:ListItem Text="Дни" Value="2" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlControlPeriod" ErrorMessage="Вы не выбрали 'Период'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="clear"></div>
                </asp:Panel>
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="RadPageView2" runat="server">                
                <asp:Panel ID="pnlMessageSettings" Visible="false" runat="server">                    
                    <%--<uc:SiteActionTemplate runat="server" ID="ucSiteActionTemplate" CurrentSiteActionTemplateCategory="Workflow" ValidationGroup="groupUpdateElement" />--%>
                    <uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" CurrentSiteActionTemplateCategory="Workflow" ValidationGroup="groupUpdateElement" runat="server" />
                    <%--<div class="row">
                        <label>Шаблон сообщения:</label>
                        <uc:DictionaryComboBox ID="dcbMessageSiteActionTemplate" DictionaryName="tbl_SiteActionTemplate" DataTextField="Title" ShowEmpty="true" ValidationGroup="groupUpdateElement" ValidationErrorMessage="Вы не выбрали 'Шаблон сообщения'" runat="server" />
                    </div>--%>
                </asp:Panel>                
                <asp:Panel ID="pnlWaitingEvent" Visible="false" runat="server">
                    <uc:WorkflowTemplateElementConditionEvent ID="ucWorkflowTemplateElementConditionEvent" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlActivitySettings" Visible="false" runat="server">
                    <div class="row">
                        <label>Описание:</label>
                        <asp:TextBox ID="txtContactActivityDescription" CssClass="input-text" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtContactActivityDescription" Display="Dynamic" ErrorMessage="Вы не ввели 'Описание'" ValidationGroup="groupUpdateElement" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTaskSettings" Visible="false" runat="server">
		                <div class="row">
                            <label>Тип:</label>    
                            <uc:DictionaryComboBox ID="dcbTaskType" DictionaryName="tbl_TaskType" ValidationGroup="groupUpdateElement" ValidationErrorMessage="Вы не выбрали тип задачи" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                        </div>
                    <div class="row">
                        <label>Важная задача:</label>
                        <asp:CheckBox ID="cbIsImportantTask" runat="server" />
                    </div>
                    <div class="row">
                        <label>Срочная задача:</label>
                        <asp:CheckBox ID="cbIsUrgentTask" runat="server" />
                    </div>
                </asp:Panel>
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="RadPageView3" runat="server">
                <uc:WorkflowTemplateElementResult ID="ucWorkflowTemplateElementResult" runat="server" />
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="RadPageView4" runat="server">
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        
    <br />
    <div class="buttons clearfix">
	    <asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateElement" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
	    <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
    </div>
</div>