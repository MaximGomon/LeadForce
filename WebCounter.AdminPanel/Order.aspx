<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="WebCounter.AdminPanel.Order" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderProducts" Src="~/UserControls/Order/OrderProducts.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskList" Src="~/UserControls/Task/TaskList.ascx" %>
<%@ Register TagPrefix="uc" TagName="PriceListComboBox" Src="~/UserControls/Order/PriceListComboBox.ascx" %>
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
                                    <labitec:Tags ID="tagsOrder" ObjectTypeName="tbl_Order" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div class="two-columns">
	                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
						                ValidationGroup="groupOrder" />

		                <div class="left-column">
			                <div class="row">
				                <label>Номер:</label>
				                <asp:Literal runat="server" ID="lrlNumber"/>
			                </div>			
		                </div>
		                <div class="right-column">
			                <div class="row clearfix">
				                <label>Дата заказа:</label>
				
					                <telerik:RadDatePicker runat="server" AutoPostBack="true" MinDate="01.01.1900" ID="rdpOrderDate" ShowPopupOnFocus="true" Width="110px">
						                <DateInput Enabled="true" />
						                <DatePopupButton Enabled="true" />
					                </telerik:RadDatePicker>
					                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpOrderDate" Text="*" ErrorMessage="Вы не ввели дату заказа" ValidationGroup="groupOrder" runat="server" />				
			                </div>			
		                </div>        
                        <div class="left-column">
                            <div class="row row-dictionary">
				                <label>Тип заказа:</label>
				                <uc:DictionaryComboBox ID="dcbOrderType" DictionaryName="tbl_OrderType" AutoPostBack="true" ValidationGroup="groupOrder" ValidationErrorMessage="Вы не выбрали тип заказа" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
			                </div>
                        </div>
                        <div class="right-column">
                            <div class="row">
				                <label>Состояние заказа:</label>
				                <asp:DropDownList runat="server" ID="ddlOrderStatus" CssClass="select-text" />
				                <asp:RequiredFieldValidator ID="rfvOrderstatus" ControlToValidate="ddlOrderStatus" ValidationGroup="groupOrder" Text="*" ErrorMessage="Вы не выбрали состояние заказа" runat="server" />
			                </div>
                        </div>
		                <div class="clear"></div>
		                <div class="row">
			                <label>Примечание:</label>
			                <asp:TextBox runat="server" ID="txtNote" CssClass="area-text" Width="630px" Height="30px" TextMode="MultiLine" />
		                </div>
		                <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			                <Tabs>
				                <telerik:RadTab Text="Основные данные" />
				                <telerik:RadTab Text="Продукты" />
                                <telerik:RadTab Text="Задачи" />
			                </Tabs>
		                </telerik:RadTabStrip>
		                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			                <telerik:RadPageView ID="RadPageView1" runat="server">
				                <div class="left-column">
					                <div class="row">
						                <label>Заказчик:</label>
						                <uc:DictionaryComboBox ID="dcbBuyerCompany" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
					                <div class="row">
						                <label>Контакт заказчика:</label>
						                <uc:ContactComboBox ID="ucBuyerContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
				                </div>
				                <div class="right-column">
					                <div class="row">
						                <label>Исполнитель:</label>
						                <uc:DictionaryComboBox ID="dcbExecutorCompany" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
					                <div class="row">
						                <label>Контакт исполнителя:</label>
						                <uc:ContactComboBox ID="ucExecutorContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
				                </div>
				                <div class="clear"></div>
				                <h3>Финансы</h3>
				                <div class="row row-order">
					                <label>Заказано:</label>
					                <asp:Literal runat="server" ID="lrlOrdered" Text="0" />
                                    <label class="label-padding-left">Оплачено:</label>
					                <asp:Literal runat="server" ID="lrlPaid" Text="0" />
                                    <label class="label-padding-left">Отгружено:</label>
					                <asp:Literal runat="server" ID="lrlShipped" Text="0" />
				                </div>				                                
				                <asp:Panel runat="server" ID="plExpirationDate">                    
                                    <h3>Сроки заказа</h3>
                                    <div class="left-column">
					                    <div class="row">
						                    <label style="width:160px">Начало срока действия:</label>
						                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpExpirationDateBegin" ShowPopupOnFocus="true" Width="110px">
							                    <DateInput Enabled="true" />
							                    <DatePopupButton Enabled="true" />
						                    </telerik:RadDatePicker>                        
					                    </div>
                                    </div>
                                    <div class="right-column">
					                    <div class="row">
						                    <label style="width:160px">Окончание срока действия:</label>                        
						                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpExpirationDateEnd" ShowPopupOnFocus="true" Width="110px">
							                    <DateInput Enabled="true" />
							                    <DatePopupButton Enabled="true" />
						                    </telerik:RadDatePicker>
					                    </div>
                                    </div>
                                    <div class="clear"></div>
				                </asp:Panel>				
                				
				                <asp:Panel runat="server" ID="plDeliveryDate">
                                    <div class="left-column">
					                    <div class="row">
						                    <label style="width:160px">Дата доставки, план:</label>                        
						                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpPlannedDeliveryDate" ShowPopupOnFocus="true" Width="110px">
							                    <DateInput Enabled="true" />
							                    <DatePopupButton Enabled="true" />
						                    </telerik:RadDatePicker>
					                    </div>
                                    </div>
                                    <div class="right-column">
					                    <div class="row">
						                    <label style="width:160px">Дата доставки, факт:</label>                        
						                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpActualDeliveryDate" ShowPopupOnFocus="true" Width="110px">
							                    <DateInput Enabled="true" />
							                    <DatePopupButton Enabled="true" />
						                    </telerik:RadDatePicker>
					                    </div>
                                    </div>
                                    <div class="clear"></div>
				                </asp:Panel>
				
				                <div class="clear"></div>
				                <h3>Связи заказа</h3>
				                <div class="row row-dictionary">
					                <label>Прайс-лист:</label>
                                    <uc:PriceListComboBox runat="server" ID="ucPriceList" ShowEmpty="true" />					
				                </div>
			                </telerik:RadPageView>
			                <telerik:RadPageView ID="RadPageView2" runat="server">
				                <uc:OrderProducts runat="server" ID="ucOrderProducts" />
			                </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server">
				                <uc:TaskList runat="server" ID="ucTaskList" />
			                </telerik:RadPageView>
		                </telerik:RadMultiPage>
		                <br/>
		                <div class="buttons">
			                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupOrder" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		                </div>
	                </div>
            </td>
        </tr>
    </table>	
</asp:Content>
