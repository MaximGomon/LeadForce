<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.InvoiceModule.Invoice" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<telerik:RadScriptBlock runat="server">
<script type="text/javascript">
    function ShowCompanyLegalRadWindow() {
        $find('<%= CompanyLegalAccountRadWindow.ClientID %>').show();
    }
//    function InitCompanyLegalRadWindow(id) {        
//        $find('<%= radAjaxManager.ClientID %>').ajaxRequest(id);
//    }
    function CloseCompanyLegalRadWindow() {
        $find('<%= CompanyLegalAccountRadWindow.ClientID %>').close();
    }
    function OnClientClicking(button, args) {
        window.location = button.get_navigateUrl();
        args.set_cancel(true);
    }
    function ShowPostponePaymentDateRadWindow(button, args) {
        $find('<%= rwPostponePaymentDate.ClientID %>').show();
        args.set_cancel(true);
    }
    function ClosePostponePaymentDateRadWindow() {
        $find('<%= rwPostponePaymentDate.ClientID %>').close();
    }
    function ShowNotFulfilledLiabilitiesRadWindow(button, args) {
        $find('<%= rwNotFulfilledLiabilities.ClientID %>').show();
        args.set_cancel(true);
    }
    function CloseNotFulfilledLiabilitiesRadWindow() {
        $find('<%= rwNotFulfilledLiabilities.ClientID %>').close();
    }    
</script>
</telerik:RadScriptBlock>
<div class="b-block">
    <h4 class="top-radius"><asp:Literal runat="server" ID="lrlTitle" /></h4>
    <div class="block-content bottom-radius request">
        <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Success" />
        <div class="row row-normal">            
            <label style="width: 165px">Состояние счета:</label>
            <asp:Literal runat="server" ID="lrlInvoiceStatus" />
            <span style="width: 20px;display: inline-block"></span>
            <telerik:RadButton runat="server" ID="rbtnPrint" Text="Распечатать" Skin="Windows7" OnClientClicking="OnClientClicking" />
            <span style="width: 5px;display: inline-block"></span>
            <telerik:RadButton runat="server" ID="rbtnPostponePaymentDate" Text="Перенести дату оплаты" Skin="Windows7" OnClientClicking="ShowPostponePaymentDateRadWindow" />
            <span style="width: 5px;display: inline-block"></span>
            <telerik:RadButton runat="server" ID="rbtnNotFulfilledLiabilities" Text="Не выполнены обязательства Исполнителя для оплаты счета" Skin="Windows7" OnClientClicking="ShowNotFulfilledLiabilitiesRadWindow" />
        </div>
        <div class="two-columns">
            <div class="left-column">                
                <div class="row row-normal">
                    <label>На сумму:</label>
                    <asp:Literal runat="server" ID="lrlInvoiceAmount" />
                </div>  
                <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server">                                      
                <div class="row row-normal">
                    <label>Заказчик:</label>
                    <asp:LinkButton runat="server" ID="lbtnBuyerCompanyLegal" ClientIDMode="AutoID" OnClick="lbtnCompanyLegal_OnClick" />                    
                </div>
                </telerik:RadAjaxPanel>
                <div class="row row-normal">
                    <label>Дата оплаты, план:</label>
                    <asp:Literal runat="server" ID="lrlPaymentDatePlanned" />
                </div>
            </div>
            <div class="left-column">
                <div class="row row-normal">
                    <label>Менеджер:</label>
                    <asp:Literal runat="server" ID="lrlExecutorContact" />
                </div>
                <telerik:RadAjaxPanel runat="server">
                <div class="row row-normal">
                    <label>Исполнитель:</label>                    
                    <asp:LinkButton runat="server" ID="lbtnExecutorCompanyLegal" ClientIDMode="AutoID" OnClick="lbtnCompanyLegal_OnClick" />
                </div>
                </telerik:RadAjaxPanel>
                <div class="row row-normal">
                    <label>Дата оплаты, факт:</label>
                    <asp:Literal runat="server" ID="lrlPaymentDateActual" />
                </div>
            </div>
            <div class="clear"></div>
            <table>
                <tr>
                    <td width="165px">
                        <div class="row row-normal">
                            <label>Примечание:</label>
                        </div>
                    </td>
                    <td><asp:Literal runat="server" ID="lrlNote" /></td>
                </tr>
            </table>            
         </div>
         
         <h3>Продукты</h3>
         <table class="table-content">
            <tr>
                <th>№</th>
                <th align="left">Наименование товара</th>
                <th align="left">Единица измерения</th>
                <th align="left">Количество</th>
                <th align="left">Цена</th>
                <th align="left" width="80px">Сумма</th>
            </tr>
            <asp:Repeater runat="server" ID="rprInvoiceProducts">
                <ItemTemplate>
                    <tr>
                        <td align="center"><%# Container.ItemIndex + 1 %></td>
                        <td><%# Eval("AnyProductName")%></td>
                        <td><%# Eval("tbl_Unit.Title") %></td>
                        <td><%# ((decimal)Eval("Quantity")).ToString("F") %></td>
                        <td><%# ((decimal)Eval("Price")).ToString("F") %></td>
                        <td><%# ((decimal)Eval("TotalAmount")).ToString("F") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>            
         </table>
         <table width="100%" class="table-total">
            <tr>
                <td align="right"><b>Итого:</b></td>
                <td width="80px" class="border"><asp:Literal runat="server" ID="lrlInvoiceAmount1" /></td>
            </tr>
            <tr>
                <td align="right"><b>Без НДС:</b></td>
                <td width="80px" class="border">--</td>
            </tr>
            <tr>
                <td align="right"><b>Всего к оплате:</b></td>
                <td width="80px" class="border"><asp:Literal runat="server" ID="lrlInvoiceAmount2" /></td>
            </tr>
         </table>
    </div>
</div>
<telerik:RadWindow runat="server" Title="Юридические реквизиты" Width="800px" Height="320px" ID="CompanyLegalAccountRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel runat="server" ID="radAjaxPanel" OnAjaxSettingCreated="radAjaxPanel_OnAjaxSettingCreated">
        <asp:Panel runat="server" ID="plCompanyLegalAccount" CssClass="rad-window-popup">            
            <div class="row">
                <label>Официальное название:</label>
                <asp:Literal runat="server" ID="lrlCLAOfficialTitle" />
            </div>
            <div class="row">
                <label>Юридический адрес:</label>
                <asp:Literal runat="server" ID="lrlCLALegalAddress" />
            </div>
            <div class="row">
                <label>Дата регистрации:</label>
                <asp:Literal runat="server" ID="lrlCLARegistrationDate" />
            </div>
            <div class="left-column">
                <div class="row">
                    <label>ОГРН:</label>
                    <asp:Literal runat="server" ID="lrlCLAOGRN" />
                </div>
                <div class="row">
                    <label>ИНН:</label>
                    <asp:Literal runat="server" ID="lrlCLAINN" />
                </div>
                <div class="row">
                    <label>КПП:</label>
                    <asp:Literal runat="server" ID="lrlCLAKPP" />
                </div>                
            </div>
            <div class="right-column">
                <div class="row">
                    <label>Р/с:</label>
                    <asp:Literal runat="server" ID="lrlCLARS" />
                </div>
                <div class="row">
                    <label>Банк:</label>
                    <asp:Literal runat="server" ID="lrlCLABank" />
                </div>                
                <div class="row">
                    <label>БИК:</label>
                    <asp:Literal runat="server" ID="lrlCLBik" />
                </div>
                <div class="row">
                    <label>К/с:</label>
                    <asp:Literal runat="server" ID="lrlCLKS" />
                </div>
            </div>
            <div class="clear"></div>
            <div class="buttons">
                <a href="javascript:;" onclick="CloseCompanyLegalRadWindow()">Отмена</a>
            </div>
        </asp:Panel>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow runat="server" Title="Перенос даты оплаты" Width="800px" Height="255px" ID="rwPostponePaymentDate" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <asp:Panel runat="server" CssClass="rad-window-popup">
                <div class="row row-label-200">
                    <label>Планируемая дата оплаты:</label>
                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpInvoicePlannedPaymentDate" ShowPopupOnFocus="true" Width="110px">
					    <DateInput Enabled="true" />
					    <DatePopupButton Enabled="true" />
				    </telerik:RadDatePicker>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpInvoicePlannedPaymentDate" Text="*" ErrorMessage="Вы не ввели дату оплаты" ValidationGroup="groupPostponePaymentDate" runat="server" />
                </div>
                <div class="row row-label-200">
                    <label>Комментарий для менеджера:</label>
                    <asp:TextBox runat="server" ID="txtCommentForManager" TextMode="MultiLine" CssClass="area-text" Width="534px"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCommentForManager" Text="*" ErrorMessage="Вы не ввели комментарий для менеджера" ValidationGroup="groupPostponePaymentDate" runat="server" />
                </div>
                <asp:Panel runat="server" ID="plNote">
                    <p><b>Примечание:</b> дата оплаты указана в соответствии с обязательствами в договоре.</p>
                </asp:Panel>
                <br/>
                <div class="buttons clearfix">    
                    <asp:LinkButton runat="server" ID="lbtnUpdatePostponePaymentDate" CssClass="btn" OnClick="lbtnUpdatePostponePaymentDate_OnClick" ValidationGroup="groupPostponePaymentDate"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                    <a class="cancel" href="javascript:;" onclick="ClosePostponePaymentDateRadWindow();">Отмена</a>
                </div>
            </asp:Panel>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow runat="server" Title="Не выполнены обязательства Исполнителя для оплаты счета" Width="800px" Height="210px" ID="rwNotFulfilledLiabilities" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
            <asp:Panel ID="Panel1" runat="server" CssClass="rad-window-popup">
                <p>Пожалуйста, кратко сообщите для руководства текущие проблемы, чтобы мы смогли максимально быстро из решить:</p>
                <br/>
                <asp:TextBox runat="server" ID="txtNotFulfilledLiabilities" TextMode="MultiLine" CssClass="area-text" Width="754px"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNotFulfilledLiabilities" Text="*" ErrorMessage="Вы не ввели комментарий" ValidationGroup="groupNotFulfilledLiabilities" runat="server" />
                <br/><br/>
                <div class="buttons clearfix">    
                    <asp:LinkButton runat="server" ID="lbtnUpdateNotFulfilledLiabilities" CssClass="btn" OnClick="lbtnUpdateNotFulfilledLiabilities_OnClick" ValidationGroup="groupNotFulfilledLiabilities"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                    <a class="cancel" href="javascript:;" onclick="CloseNotFulfilledLiabilitiesRadWindow();">Отмена</a>
                </div>
            </asp:Panel>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>

</asp:Content>