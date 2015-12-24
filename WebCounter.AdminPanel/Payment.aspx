<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="WebCounter.AdminPanel.Payment" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentStatus" Src="~/UserControls/Payment/PaymentStatus.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentPass" Src="~/UserControls/Payment/PaymentPass.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentPassRuleEdit" Src="~/UserControls/Payment/EditPaymentPassRule.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%# ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript"> 
    var showConfirm = 1;
    function rcbPaymentPassRule_OnClientSelectedIndexChanging(combo, eventArgs) 
    {
        var callBackFn = function (arg) 
        { 
            if (arg == true && showConfirm == 1) 
            { 
                combo.clearSelection(); 
                var item = eventArgs.get_item(); 
             
                //prevent entering the same part after calling the select() method 
                showConfirm = 0; 
                combo.findItemByText(item.get_text()).select(); 
                combo.set_text(item.get_text()); 
                //fire the server-side SelectedIndexChanged event 
                __doPostBack("<%= rcbPaymentPassRule.ClientID %>", '{\"Command\":\"Select\"}'); 
             
                //now we can show the confirm again 
                showConfirm = 1; 
            } 
        } 
     
        if (showConfirm == 1) 
        {
            radconfirm('Вы действительно хотите обновить проводки?',callBackFn,250,120); 
        }

        eventArgs.set_cancel(true);
    }
    function ShowPaymentPassRuleRadWindow() {
        $find('<%= rwPaymentPassRule.ClientID %>').show();

    }

    function ClosePaymentPassRuleRadWindow() {
        $find('<%= rwPaymentPassRule.ClientID %>').close();
    }
    function RadWindowAutoSize() {
        if ($find('<%= rwPaymentPassRule.ClientID %>') != null)
            $find('<%= rwPaymentPassRule.ClientID %>').autoSize(true);
    }
    
    function ShowEditPaymentPassRuleRadWindow() {
        $find('<%= rwEditPaymentPassRule.ClientID %>').show();

    }

    function CloseEditPaymentPassRuleRadWindow() {
        $find('<%= rwEditPaymentPassRule.ClientID %>').close();
    }
    function EditRadWindowAutoSize() {
        if ($find('<%= rwEditPaymentPassRule.ClientID %>') != null)
            $find('<%= rwEditPaymentPassRule.ClientID %>').autoSize(true);
    }

</script>
</telerik:RadCodeBlock>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div class="two-columns">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        	                    <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						                    CssClass="validation-summary"
						                    runat="server"
						                    EnableClientScript="true"
						                    HeaderText="Заполните все поля корректно:"
						                    ValidationGroup="valGroupUpdate" />
                                            <asp:CustomValidator id="cvGroupUpdate" ValidationGroup="valGroupUpdate" runat="server" 
                            Display="None" EnableClientScript="False"></asp:CustomValidator>

                        <h3>Основные данные</h3>
                        <div class="row row-product-name">
                            <label>Назначение:</label>
                            <asp:TextBox runat="server" ID="txtAssignment" CssClass="input-text"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAssignment" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите назначение'/>
                        </div>
                        <div class="left-column">
		                    <div class="row">
			                    <label>Дата план:</label>
			                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpDatePlan" ShowPopupOnFocus="true" Width="110px">
					                    <DateInput Enabled="true" />
					                    <DatePopupButton Enabled="true" />
			                    </telerik:RadDatePicker>
			                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdpDatePlan" Text="*" ErrorMessage="Укажите Дата план" ValidationGroup="valGroupUpdate" runat="server" />
		                    </div>
	                    </div>
	                    <div class="right-column">
		                    <div class="row">
			                    <label>Дата факт:</label>
			                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpDateFact" ShowPopupOnFocus="true" Width="110px">
					                    <DateInput Enabled="true" />
					                    <DatePopupButton Enabled="true" />
			                    </telerik:RadDatePicker>
  		                    </div>
	                    </div>
	                    <div class="clear"></div>
	                    <div class="row">
		                    <label>Тип:</label>
		                    <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_OnSelectedIndexChanged" />
	                    </div>
	                    <uc:PaymentStatus runat="server" ID="ucPaymentStatus" />

	                    <div class="clear"></div>
	                    <h3>Контрагенты оплаты</h3>
                        <div class="left-column">
		                    <div class="row">
			                    <label>Плательщик:</label>
                                <uc:DictionaryOnDemandComboBox ID="dcbPayer" AutoPostBack="True" OnSelectedIndexChanged="dcbPayer_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>            
		                    </div>
		                    <div class="row">
			                    <label>ЮЛ Плательщика:</label>
                                <uc:DictionaryOnDemandComboBox ID="dcbPayerLegalAccount" AutoPostBack="true" OnSelectedIndexChanged="dcbPayerLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
		                    </div>
	                    </div>
	                    <div class="right-column">
		                    <div class="row">
			                    <label>Получатель:</label>
                                <uc:DictionaryOnDemandComboBox ID="dcbRecipient" AutoPostBack="True" OnSelectedIndexChanged="dcbRecipient_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
		                    </div>
		                    <div class="row">
			                    <label>ЮЛ Получателя:</label>
                                <uc:DictionaryOnDemandComboBox ID="dcbRecipientLegalAccount" AutoPostBack="true" OnSelectedIndexChanged="dcbRecipientLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
		                    </div>
	                    </div>
	                    <div class="clear"></div>
	                    <h3>Сумма оплаты</h3>
                        <div class="left-column">
		                    <div class="row">
			                    <label>Валюта:</label>
			                    <uc:DictionaryComboBox runat="server" ID="dcbCurrency" DictionaryName="tbl_Currency" DataTextField="Name" ShowEmpty="true" ValidationGroup="valGroupUpdate" ValidationErrorMessage="Вы не выбрали валюту"/>
		                    </div>                            
		                    <div class="row">
			                    <label>Курс:</label>
			                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCourse" EmptyMessage="" Value="1" MinValue="1" CssClass="input-text" Type="Number" OnTextChanged="rntxtCourse_OnTextChanged" AutoPostBack="true">
				                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
			                    </telerik:RadNumericTextBox>
			                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtCourse" CssClass="required" Text="*" ErrorMessage="Вы не ввели курс" ValidationGroup="valGroupUpdate" runat="server" />
		                    </div>
	                    </div>
	                    <div class="right-column">
		                    <div class="row">
    	                        <label>Сумма в валюте:</label>
		                        <telerik:RadNumericTextBox runat="server" ID="rntxtAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number" OnTextChanged="rntxtAmount_OnTextChanged" AutoPostBack="true">
			                        <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
		                        </telerik:RadNumericTextBox>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму в валюте" ValidationGroup="valGroupUpdate" runat="server" />
                            </div>
		                    <div class="row">
    	                        <label>Сумма:</label>
		                        <telerik:RadNumericTextBox Enabled="false" runat="server" ID="rntxtTotal" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
			                        <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
		                        </telerik:RadNumericTextBox>
		                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtTotal" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму" ValidationGroup="valGroupUpdate" runat="server" />
                            </div>
	                    </div>
	                    <div class="clear"></div>


	                    <telerik:RadTabStrip MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" ID="radTabs">
		                    <Tabs>
			                    <telerik:RadTab Text="Проводки" />
			                    <telerik:RadTab Text="Связи оплаты" />
		                    </Tabs>
	                    </telerik:RadTabStrip>

	                    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <div class="row fio">
		                                    <label>Правило:</label>
                                            <telerik:RadComboBox runat="server" ID="rcbPaymentPassRule" CssClass="select-text" Skin="Labitec" EnableEmbeddedSkins="false" AutoPostBack="true" OnSelectedIndexChanged="rcbPaymentPassRule_OnSelectedIndexChanged" OnClientSelectedIndexChanging="rcbPaymentPassRule_OnClientSelectedIndexChanging"/>
					                        <asp:LinkButton ID="lbtnSelectPaymentPassRule" OnClick="lbtnSelectPaymentPassRule_OnClick"  CssClass="btn" runat="server"><em>&nbsp;</em><span>Выбрать правило</span></asp:LinkButton>
	                                    </div>
                                        <uc:PaymentPass runat="server" ID="ucPaymentPass" />	
		                    </telerik:RadPageView>
		                    <telerik:RadPageView ID="RadPageView2" CssClass="clearfix" runat="server">
                                <div class="left-column">
		                            <div class="row">
			                            <label>Заказ:</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbOrder" AutoPostBack="True" OnSelectedIndexChanged="dcbOrder_OnSelectedIndexChanged" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>            
		                            </div>
	                            </div>
	                            <div class="right-column">
		                            <div class="row">
			                            <label>Счет:</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbInvoice" DictionaryName="tbl_Invoice" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
		                            </div>
	                            </div>
                                <div class="clear"/>
		                    </telerik:RadPageView>
	                    </telerik:RadMultiPage>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                        <br />
                        <div class="buttons">
                            <asp:LinkButton ID="BtnUpdate" OnClick="BtnUpdate_Click" CssClass="btn" ValidationGroup="valGroupUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
                        </div>

                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7" EnableShadow="true"> 
                    </telerik:RadWindowManager> 
                    <telerik:RadWindow ID="rwPaymentPassRule" runat="server" Title="Правила проводки" AutoSize="false" Height="120px" Width="600px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup radwindow-popup-inner" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                            <div class="radwindow-popup-inner">
                                        <div class="row fio">
		                                    <label>Правило:</label>
                                            <telerik:RadComboBox runat="server" ID="rcbSelectPaymentPassRule" AutoPostBack="true" OnSelectedIndexChanged="rcbSelectPaymentPassRule_OnSelectedIndexChanged" CssClass="select-text" Skin="Labitec" EnableEmbeddedSkins="false"/>
	                                    </div>

            
                                <div class="buttons clearfix">                        
					                    <asp:LinkButton ID="lbtnIncludeInRule" OnClick="lbtnIncludeInRule_OnClick"  CssClass="btn" runat="server"><em>&nbsp;</em><span>Включить в правило</span></asp:LinkButton>
					                    <asp:LinkButton ID="lbtnOpenRule" OnClick="lbtnOpenRule_OnClick"  CssClass="btn" runat="server"><em>&nbsp;</em><span>Открыть правило </span></asp:LinkButton>
					                    <asp:LinkButton ID="lbtnAddRule" OnClick="lbtnAddRule_OnClick"  CssClass="btn" runat="server"><em>&nbsp;</em><span>Добавить правило</span></asp:LinkButton>
                                        <a href="javascript:;" class="cancel" onclick="ClosePaymentPassRuleRadWindow(); return false;">Отмена</a>
                                </div>
                            </div>
                            </ContentTemplate></asp:UpdatePanel>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow ID="rwEditPaymentPassRule" runat="server" Title="Правило проводки" AutoSize="true" AutoSizeBehaviors="Height" Width="800px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup radwindow-popup-inner" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
                        <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
                                    <uc:PaymentPassRuleEdit ID="ucPaymentPassRuleEdit" runat="server" />
                                </ContentTemplate></asp:UpdatePanel>
                        </ContentTemplate>
                    </telerik:RadWindow>


            </td>
        </tr>
    </table>
</asp:Content>
