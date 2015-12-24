<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SourceMonitoring.aspx.cs" Inherits="WebCounter.AdminPanel.SourceMonitoring" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Enumerations" %>

<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
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
                                    <labitec:Tags ID="tagsSourceMonitoring" ObjectTypeName="tbl_SourceMonitoring" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div class="two-columns monitoring-edit">
		            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						            CssClass="validation-summary"
						            runat="server"
						            EnableClientScript="true"
						            HeaderText="Заполните все поля корректно:"
						            ValidationGroup="groupSourceMonitoring" />
		            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			            <Tabs>
				            <telerik:RadTab Text="Карточка мониторинга" />            
			            </Tabs>
		            </telerik:RadTabStrip>

		            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			            <telerik:RadPageView ID="RadPageView1" runat="server">
				            <h3>Основные данные</h3>
				            <div class="row">
					            <label>Название:</label>
					            <asp:TextBox ID="txtName" CssClass="input-text" runat="server" Width="600px" />
					            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" CssClass="required" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupSourceMonitoring" runat="server" />
				            </div>
				            <div class="left-column">
					            <div class="row">
						            <label>Тип источника:</label>
						            <asp:DropDownList ID="ddlSourceType" CssClass="select-text" runat="server" />
					            </div>
				            </div>
				            <div class="right-column">
					            <div class="row">
						            <label>Статус:</label>
						            <asp:DropDownList ID="ddlStatus" CssClass="select-text" runat="server" />
					            </div>
				            </div>
				            <div class="clear"></div>
                            <div class="row">
                                <label>Получатель:</label>
                                <uc:ContactComboBox runat="server" ID="ucContact" Width="205px" FilterByFullName="true" />
                            </div>
				            <div class="row">
					            <label>Комментарий:</label>
					            <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="area-text" Width="600px" />
				            </div>
				            <h3>Параметры сервера</h3>
				            <div class="left-column">
					            <div class="row">
						            <label>POP сервер:</label>
						            <asp:TextBox ID="txtPOPHost" CssClass="input-text" runat="server" />
						            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPOPHost" CssClass="required" Text="*" ErrorMessage="Вы не ввели POP сервер" ValidationGroup="groupSourceMonitoring" runat="server" />
					            </div>
					            <div class="row">
						            <label>Логин для POP сервера:</label>
						            <asp:TextBox ID="txtPOPUserName" CssClass="input-text" runat="server" />
						            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPOPUserName" CssClass="input-text" Text="*" ErrorMessage="Вы не ввели Логин для POP сервера" ValidationGroup="groupSourceMonitoring" runat="server" />
					            </div>					
					            <div class="row">
						            <label>Использовать SSL:</label>
						            <asp:CheckBox runat="server" ID="chxIsSsl"/>
					            </div>
					            <div class="row">
						            <label>Оставлять на сервере:</label>
						            <asp:CheckBox runat="server" ID="chxIsLeaveOnServer"/>
					            </div>					
                                <div class="row row-date">
						            <label>Получать сообщения начиная с:</label>
						            <telerik:RadDatePicker runat="server" MinDate="01.01.1900" ID="rdpStartDate" ShowPopupOnFocus="true" Width="50px">
						                <DateInput Enabled="true" />
						                <DatePopupButton Enabled="true" />
					                </telerik:RadDatePicker>
					            </div>
				            </div>
				            <div class="right-column">
					            <div class="row">
						            <label>Порт для POP сервера:</label>
						            <telerik:RadNumericTextBox runat="server" ID="rntxtPopPort" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
							            <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
						            </telerik:RadNumericTextBox>
						            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtPopPort" Text="*" ErrorMessage="Вы не ввели Порт для POP сервера" ValidationGroup="groupSourceMonitoring" runat="server" />
					            </div>
					            <div class="row">
						            <label>Пароль для POP сервера:</label>
						            <asp:TextBox ID="txtPOPPassword" CssClass="input-text" runat="server" TextMode="Password" autocomplete="off" />
						            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPOPPassword" Text="*" ErrorMessage="Вы не ввели Пароль для POP сервера" ValidationGroup="groupSourceMonitoring" runat="server" />

					            </div>					
					            <div class="row">
						            <label>Дата последней загрузки:</label>
						            <asp:Literal runat="server" ID="lrlLastReceivedAt" />
					            </div>					                    
					            <div class="row">
						            <label>Дней до удаления:</label>
						            <telerik:RadNumericTextBox runat="server" ID="rntxtDaysToDelete" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
							            <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
						            </telerik:RadNumericTextBox>
					            </div>
				            </div>
				            <div class="clear"></div>
				            <h3>Правила обработки</h3>				
				            <div class="left-column">
				                <div class="row">
					                <label>Обработка отправителя:</label>
					                <asp:DropDownList ID="ddlSenderProcessing" CssClass="select-text" runat="server" />
				                </div>
					            <div class="row">
						            <label>Обработка возвратов:</label>
						            <asp:DropDownList ID="ddlProcessingOfReturns" CssClass="select-text" runat="server" />
					            </div>
					            <div class="row">
						            <label>Удалять возвраты:</label>
						            <asp:CheckBox runat="server" ID="chxRemoveReturns"/>
					            </div>                    
				            </div>
				            <div class="right-column">
				                <div class="row">
				                    <label>Формировать запрос:</label>
				                    <uc:DictionaryComboBox ID="dcbRequestSourceType" Width="204px" DictionaryName="tbl_RequestSourceType" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
					            <div class="row">
						            <label>Обработка автоответов:</label>
						            <asp:DropDownList ID="ddlProcessingOfAutoReplies" CssClass="select-text" runat="server" />
					            </div>
					            <div class="row">
						            <label>Удалять автоответы:</label>
						            <asp:CheckBox ID="chxRemoveAutoReplies" runat="server" />
					            </div>
				            </div>
				            <div class="clear"></div>
				            <h3>Фильтр по полям</h3>
				            <div class="sourcemonitoring-filters">
					            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
							            <telerik:RadGrid ID="rgSourceMonitoringFilters" Skin="Windows7" Width="780px" runat="server" OnNeedDataSource="rgSourceMonitoringFilters_NeedDataSource" OnDeleteCommand="rgSourceMonitoringFilters_DeleteCommand"
								            OnInsertCommand="rgSourceMonitoringFilters_InsertCommand" OnUpdateCommand="rgSourceMonitoringFilters_UpdateCommand" OnItemDataBound="rgSourceMonitoringFilters_OnItemDataBound">
								            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage">
									            <EditFormSettings>
										            <EditColumn ButtonType="ImageButton"/>
									            </EditFormSettings>
									            <Columns>                                    
										            <telerik:GridTemplateColumn HeaderText="Реквизит источника" UniqueName="SourceProperty">
											            <ItemTemplate>
												            <%# EnumHelper.GetEnumDescription((SourceEmailProperty)int.Parse(Eval("SourcePropertyID").ToString()))%>
											            </ItemTemplate>
											            <InsertItemTemplate>
												            <asp:DropDownList runat="server" ID="ddlSourceProperty" CssClass="select-text"/>                                            
											            </InsertItemTemplate>
											            <EditItemTemplate>
												             <asp:DropDownList runat="server" ID="ddlSourceProperty" CssClass="select-text"/>
											            </EditItemTemplate>
										            </telerik:GridTemplateColumn>
										            <telerik:GridTemplateColumn HeaderText="Маска">
											            <ItemTemplate>
												            <%# Eval("Mask")%>
											            </ItemTemplate>
											            <InsertItemTemplate>
												            <asp:TextBox ID="txtMask" runat="server" Text='' CssClass="input-text" Width="605px" />
											            </InsertItemTemplate>
											            <EditItemTemplate>
												            <asp:TextBox ID="txtMask" runat="server" Text='<%# Bind("Mask") %>' CssClass="input-text" Width="605px" />
											            </EditItemTemplate>
										            </telerik:GridTemplateColumn>
										            <telerik:GridTemplateColumn HeaderText="Действие" UniqueName="MonitoringAction">
											            <ItemTemplate>
												            <%# EnumHelper.GetEnumDescription((MonitoringAction)int.Parse(Eval("MonitoringActionID").ToString()))%>
											            </ItemTemplate>
											            <InsertItemTemplate>
												            <asp:DropDownList runat="server" ID="ddlMonitoringAction" CssClass="select-text"/>                                            
											            </InsertItemTemplate>
											            <EditItemTemplate>
												             <asp:DropDownList runat="server" ID="ddlMonitoringAction" CssClass="select-text"/>
											            </EditItemTemplate>
										            </telerik:GridTemplateColumn>
										            <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
										            <telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
											            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
											            ConfirmDialogWidth="220px" /> 
									            </Columns>
								            </MasterTableView>
							            </telerik:RadGrid>
					             </telerik:RadAjaxPanel>
				            </div>
			            </telerik:RadPageView>
		            </telerik:RadMultiPage>

		            <br/>
		            <div class="buttons">
			            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupSourceMonitoring" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		            </div>
	
	            </div>
            </td>
        </tr>
    </table>
	
</asp:Content>
