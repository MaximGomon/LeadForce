<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shipment.aspx.cs" Inherits="WebCounter.AdminPanel.Shipment" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ShipmentProducts" Src="~/UserControls/Shipment/ShipmentProducts.ascx" %>
<%@ Register TagPrefix="uc" TagName="ShipmentInvoices" Src="~/UserControls/Shipment/ShipmentInvoices.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/UserControls/Shared/ContentComments.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function ShowSendShipmentRadWindow() {
                $find('<%= rwSendShipment.ClientID %>').show();

            }
            function CloseSendShipmentRadWindow() {
                $find('<%= rwSendShipment.ClientID %>').close();
            }
        </script>
    </telerik:RadScriptBlock>
    
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="rpbLeft" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsShipments" ObjectTypeName="tbl_Shipment" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="true" Text="Отчеты" Visible="false" Value="Reports">
                            <ContentTemplate>
                                <ul class="reports">
                                    <li><asp:HyperLink runat="server" Text="Счет без печати" ID="hlWithoutStamp" Target="_blank" /></li>
                                    <li><asp:HyperLink runat="server" Text="Счет с печатью" ID="hlWithStamp" Target="_blank" /></li>
                                </ul>                                        
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
						            ValidationGroup="groupShipment" />
                        
                    <table>
                        <tr>
                            <td>
                                <div class="row">
				                    <label>Номер:</label>				
			                        <asp:Literal runat="server" ID="lrlNumber"/>
			                    </div>
                            </td>
                            <td>
                                <div style="position: relative" class="row clearfix">
				                    <label>Дата документа:</label>
				                    <telerik:RadDatePicker runat="server" AutoPostBack="True" MinDate="01.01.1900" CssClass="date-picker" ID="rdpCreatedAt" ShowPopupOnFocus="true" Width="110px">
						                    <DateInput Enabled="true" />
						                    <DatePopupButton Enabled="true" />
					                    </telerik:RadDatePicker>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpCreatedAt" Text="*" ErrorMessage="Вы не ввели дату счета" ValidationGroup="groupShipment" runat="server" />
				
			                    </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row row-dictionary">
				                    <label>Тип документа:</label>
				                    <uc:DictionaryComboBox ID="dcbShipmentType" DictionaryName="tbl_ShipmentType" ValidationGroup="groupShipment" ValidationErrorMessage="Вы не выбрали тип документа" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
			                    </div>
                            </td>
                            <td>
                                <div class="row">
				                    <label>Состояние документа:</label>
				                    <asp:DropDownList runat="server" ID="ddlShipmentStatus" CssClass="select-text" />
				                    <asp:RequiredFieldValidator ID="rfvOrderstatus" ControlToValidate="ddlShipmentStatus" ValidationGroup="groupShipment" Text="*" ErrorMessage="Вы не выбрали состояние документа" runat="server" />
			                    </div>
                            </td>
                        </tr>
                    </table>        
		            <div class="row">
			            <label>Примечание:</label>
			            <asp:TextBox runat="server" ID="txtNote" CssClass="area-text" Width="630px" Height="30px" TextMode="MultiLine" />
		            </div>
		            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			            <Tabs>
				            <telerik:RadTab Text="Основные данные" />
                            <telerik:RadTab Text="Продукты" />	
                            <telerik:RadTab Text="История изменений" />			
                            <telerik:RadTab Text="Счета" Value="Invoices" />
			            </Tabs>
		            </telerik:RadTabStrip>
		            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			            <telerik:RadPageView ID="RadPageView1" runat="server">
				            <div class="left-column">
				                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
					                <div class="row">
						                <label>Заказчик:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbBuyerCompany" AutoPostBack="True" OnSelectedIndexChanged="dcbBuyerCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
                                    <div class="row">
						                <label>ЮЛ заказчика:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbBuyerCompanyLegalAccount" AutoPostBack="True" OnSelectedIndexChanged="dcbBuyerCompanyLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>                                
					                <div class="row">
						                <label>Контакт заказчика:</label>
						                <uc:ContactComboBox ID="ucBuyerContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
                                </telerik:RadAjaxPanel>
				            </div>
				            <div class="right-column">
				                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
					                <div class="row">
						                <label>Исполнитель:</label>
						                <uc:DictionaryOnDemandComboBox ID="dcbExecutorCompany" AutoPostBack="true" OnSelectedIndexChanged="dcbExecutorCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>
                                    <div class="row">
						                <label>ЮЛ исполнителя:</label>						
                                        <uc:DictionaryOnDemandComboBox ID="dcbExecutorCompanyLegalAccount" AutoPostBack="True" OnSelectedIndexChanged="dcbExecutorCompanyLegalAccount_OnSelectedIndexChanged" DictionaryName="tbl_CompanyLegalAccount" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
					                </div>                                
					                <div class="row">
						                <label>Контакт исполнителя:</label>
						                <uc:ContactComboBox ID="ucExecutorContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
					                </div>
                                </telerik:RadAjaxPanel>
				            </div>		
                            <div class="clear"></div>
                            <h3>Финансы</h3>
                            <div class="left-column">
                                <div class="row">
						            <label>Сумма документа:</label>
                                    <asp:Literal runat="server" ID="lrlShipmentAmount" Text="0"/>
                                </div>					
                            </div>
                            <div class="right-column">
                                <div class="row">
						            <label>Дата отправки:</label>                        
						            <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpSendDate" ShowPopupOnFocus="true" Width="110px">
							            <DateInput Enabled="true" />
							            <DatePopupButton Enabled="true" />
						            </telerik:RadDatePicker>
					            </div>
                            </div>                            
                            <div class="clear"></div>
				            <h3>Связи документа</h3>
                            <div class="row row-dictionary">
                                <label>Заказы:</label>    
                                <uc:DictionaryOnDemandComboBox ID="dcbOrders" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                            </div>				            
			            </telerik:RadPageView>			
                        <telerik:RadPageView ID="RadPageView2" runat="server">
				            <uc:ShipmentProducts runat="server" ID="ucShipmentProducts" />
			            </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
				            <labitec:Grid ID="gridShipmentHistory" OnItemDataBound="gridShipmentHistory_OnItemDataBound" Toolbar="false" TableName="tbl_ShipmentHistory" Fields="tbl_Contact.ID" ClassName="WebCounter.AdminPanel.ShipmentHistory" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата изменения" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn3" DataField="tbl_Contact.UserFullName" HeaderText="Автор изменения" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn2" DataField="SendDate" HeaderText="Дата отправки" runat="server"/>                                    
                                    <labitec:GridColumn ID="GridColumn5" DataField="ShipmentAmount" HeaderText="Сумма счета" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn6" DataField="ShipmentStatusID" HeaderText="Состояние счета" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlShipmentStatus" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>                                    
                                    <labitec:GridColumn ID="GridColumn8" DataField="Note" HeaderText="Примечание" runat="server"/>
                                </Columns>
                                <Joins>                        
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_ShipmentHistory" TableKey="AuthorID" runat="server" />                
                                </Joins>
                            </labitec:Grid>
                            <asp:Panel runat="server" ID="plComments" Visible="false">
                                <br/><br/>
                                <uc:ContentComments runat="server" ID="ucContentComments" ExpandComments="false" CommentType="tbl_ShipmentComment" EnableHtmlCommentEditor="true" />                    
                            </asp:Panel>
			            </telerik:RadPageView>
                        <telerik:RadPageView runat="server">
                            <uc:ShipmentInvoices runat="server" ID="ucShipmentInvoices" />
                        </telerik:RadPageView>
		            </telerik:RadMultiPage>
		            <br/>
		            <div class="buttons">
			            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupShipment" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <asp:LinkButton ID="lbtnShowSendShipmentRadWindow" OnClientClick="ShowSendShipmentRadWindow();return false;" CssClass="btn" Visible="false" runat="server"><em>&nbsp;</em><span>Направить счет по email</span></asp:LinkButton>
			            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		            </div>
	            </div>
        </td>
    </tr>
</table>	

<telerik:RadWindow ID="rwSendShipment" runat="server" Title="Направить счет" AutoSize="false" Height="170px" Width="900px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>        
        <div class="radwindow-popup-inner send-shipment-popup">                    
            <div class="left-column">
                <div class="row">
                    <label>Заказчику:</label>
                    <asp:CheckBox runat="server" ID="chxForBuyer" Checked="True"/>
                </div>
                <div class="row">
					<label>Контакт заказчика:</label>
					<uc:ContactComboBox ID="ucSendShipmentBuyerContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
				</div>
            </div>
            <div class="right-column">
                <div class="row">
                    <label>Ответственному:</label>
                    <asp:CheckBox runat="server" ID="chxForExecutor" Checked="True" />
                </div>
                <div class="row">
					<label>Контакт исполнителя:</label>
					<uc:ContactComboBox ID="ucSendShipmentExecutorContact" CssClass="select-text" runat="server" FilterByFullName="true"/>
				</div>
            </div>
            <div class="clear"></div>
            <div class="buttons clearfix">                        
				<asp:LinkButton ID="lbtnSendShipment" OnClick="lbtnSendShipment_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Направить счет</span></asp:LinkButton>
				<a href="javascript:;" class="cancel" onclick="CloseSendShipmentRadWindow(); return false;">Отмена</a>
            </div>
        </div>        
    </ContentTemplate>
</telerik:RadWindow>


</asp:Content>
