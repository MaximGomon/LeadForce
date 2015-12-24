<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentPassRuleEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.DictionaryEditForm.PaymentPassRuleEdit" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Enumerations" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />

<div class="rgEditForm dictionary-edit">
	<table width="100%">
		<tbody>
		    <tr>
			    <td style="vertical-align:top;">
			        <table width="100%">
	                    <tbody>
				            <tr>
					            <td><label>Название:</label></td>
				                <td>
				                    <telerik:RadTextBox ID="txtTitle" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Title" ) %>'/>
                                    <asp:RequiredFieldValidator CssClass="required" Display="Dynamic" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupPaymentPassRule" runat="server" />
                                </td>
				            </tr>
                            <tr>
                                <td><label>Тип:</label></td>
                                <td>
		                            <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="select-text"/>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Активное:</label></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chxIsActive" Checked='<%# bool.Parse(string.IsNullOrEmpty(DataBinder.Eval( Container, "DataItem.IsActive" ).ToString()) ? "false" : DataBinder.Eval( Container, "DataItem.IsActive" ).ToString()) %>' />
                                </td>
                            </tr>
                            <tr>
                                <td><label>Проводить автоматически:</label></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chxIsAutomatic" Checked='<%# bool.Parse(string.IsNullOrEmpty(DataBinder.Eval( Container, "DataItem.IsAutomatic" ).ToString()) ? "false" : DataBinder.Eval( Container, "DataItem.IsAutomatic" ).ToString()) %>' />
                                </td>
                            </tr>
			            </tbody>
                    </table>
                    <asp:Panel runat="server" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <h3>Контрагенты</h3>
                        <telerik:RadGrid ID="rgPaymentPassRuleCompany" Width="100%" Skin="Windows7" runat="server"                        
                             AllowPaging="True" AllowSorting="true" DataSourceID="edsPaymentPassRuleCompany"
                             OnItemDataBound="rgPaymentPassRuleCompany_OnItemDataBound"
                             OnInsertCommand="rgPaymentPassRuleCompany_OnInsertCommand"
                             OnUpdateCommand="rgPaymentPassRuleCompany_OnUpdateCommand"                                                     
                             OnDeleteCommand="rgPaymentPassRuleCompany_OnDeleteCommand"  
                             OnItemCreated="rgPaymentPassRuleCompany_OnItemCreated"                                                 
                              >                                                        
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="edsPaymentPassRuleCompany" DataKeyNames="ID" CommandItemDisplay="Top">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false"/>
                                    <telerik:GridBoundColumn DataField="PaymentPassRuleID" HeaderText="PaymentPassRuleID" SortExpression="PaymentPassRuleID" UniqueName="PaymentPassRuleID" Visible="false"/>
                                    <telerik:GridDropDownColumn ListTextField="Name" DataSourceID="edsPayer"
                                        ListValueField="ID" UniqueName="PayerID" SortExpression="Name"
                                        HeaderText="Плательщик" DropDownControlType="RadComboBox" DataField="PayerID"
                                        ConvertEmptyStringToNull="true" EnableEmptyListItem="true" EmptyListItemText="---">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPayerLegalAccount"
                                        ListValueField="ID" UniqueName="PayerLegalAccountID" SortExpression="Title"
                                        HeaderText="ЮЛ Плательщика" DropDownControlType="RadComboBox" DataField="PayerLegalAccountID"
                                        ConvertEmptyStringToNull="true" EnableEmptyListItem="true" EmptyListItemText="---">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Name" DataSourceID="edsRecipient"
                                        ListValueField="ID" UniqueName="RecipientID" SortExpression="Name"
                                        HeaderText="Получатель" DropDownControlType="RadComboBox" DataField="RecipientID"
                                        ConvertEmptyStringToNull="true" EnableEmptyListItem="true" EmptyListItemText="---">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsRecipientLegalAccount"
                                        ListValueField="ID" UniqueName="RecipientLegalAccountID" SortExpression="Title"
                                        HeaderText="ЮЛ Получателя" DropDownControlType="RadComboBox" DataField="RecipientLegalAccountID"
                                        ConvertEmptyStringToNull="true" EnableEmptyListItem="true" EmptyListItemText="---">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                        <HeaderStyle Width="20px"/>
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn Text="Delete" 
                                        ConfirmDialogType="RadWindow" 
                                        ConfirmText="Вы действительно хотите удалить запись?" 
                                        ConfirmTitle="Удаление"
                                        ConfirmDialogHeight="100px"
                                        ConfirmDialogWidth="220px"
                                        CommandName="Delete" ButtonType="ImageButton">
                                        <HeaderStyle Width="20px"/>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn ButtonType="ImageButton" />
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>   
                        <br/>
                        <h3>Проводки</h3>
                        <telerik:RadGrid ID="rgPaymentPassRulePass" Width="100%" Skin="Windows7" runat="server"                        
                             AllowPaging="True" AllowSorting="true" DataSourceID="edsPaymentPassRulePass"
                             OnItemDataBound="rgPaymentPassRulePass_OnItemDataBound"
                             OnInsertCommand="rgPaymentPassRulePass_OnInsertCommand"
                             OnUpdateCommand="rgPaymentPassRulePass_OnUpdateCommand"                                                     
                             OnDeleteCommand="rgPaymentPassRulePass_OnDeleteCommand"                                                   
       
                              >                                                        
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="edsPaymentPassRulePass" DataKeyNames="ID" CommandItemDisplay="Top">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false"/>
                                    <telerik:GridBoundColumn DataField="PaymentPassRuleID" HeaderText="PaymentPassRuleID" SortExpression="PaymentPassRuleID" UniqueName="PaymentPassRuleID" Visible="false"/>
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentPassCategory"
                                        ListValueField="ID" UniqueName="OutgoPaymentPassCategoryID" SortExpression="Title"
                                        HeaderText="Категория расход" DropDownControlType="RadComboBox" DataField="OutgoPaymentPassCategoryID"
                                        >
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentCFO"
                                        ListValueField="ID" UniqueName="OutgoCFOID" SortExpression="Title"
                                        HeaderText="ЦФО расход" DropDownControlType="RadComboBox" DataField="OutgoCFOID">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentArticle"
                                        ListValueField="ID" UniqueName="OutgoPaymentArticleID" SortExpression="Title"
                                        HeaderText="Статья расход" DropDownControlType="RadComboBox" DataField="OutgoPaymentArticleID">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentPassCategory"
                                        ListValueField="ID" UniqueName="IncomePaymentPassCategoryID" SortExpression="Title"
                                        HeaderText="Категория приход" DropDownControlType="RadComboBox" DataField="IncomePaymentPassCategoryID">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentCFO"
                                        ListValueField="ID" UniqueName="IncomeCFOID" SortExpression="Title"
                                        HeaderText="ЦФО приход" DropDownControlType="RadComboBox" DataField="IncomeCFOID">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsPaymentArticle"
                                        ListValueField="ID" UniqueName="IncomePaymentArticleID" SortExpression="Title"
                                        HeaderText="Статья приход" DropDownControlType="RadComboBox" DataField="IncomePaymentArticleID">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="ldsFormula"
                                        ListValueField="ID" UniqueName="FormulaID" SortExpression="Title"
                                        HeaderText="Формула расчета" DropDownControlType="RadComboBox" DataField="FormulaID">                                        
                                    </telerik:GridDropDownColumn>

                                    

                                    <telerik:GridNumericColumn NumericType="Number" DecimalDigits="2" DefaultInsertValue="0" UniqueName="Value" DataField="Value" SortExpression="Value" HeaderText="Значение для расчета" />

                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                        <HeaderStyle Width="20px"/>
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn Text="Delete" 
                                        ConfirmDialogType="RadWindow" 
                                        ConfirmText="Вы действительно хотите удалить запись?" 
                                        ConfirmTitle="Удаление"
                                        ConfirmDialogHeight="100px"
                                        ConfirmDialogWidth="220px"
                                        CommandName="Delete" ButtonType="ImageButton">
                                        <HeaderStyle Width="20px"/>
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn ButtonType="ImageButton" />
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>    

                        <asp:LinqDataSource ID="ldsFormula" OnSelecting="ldsFormula_OnSelecting" runat="server"></asp:LinqDataSource>

                        <asp:EntityDataSource ID="edsPaymentPassRulePass" runat="server"
                        EntitySetName="tbl_PaymentPassRulePass"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   
                        
                        <asp:EntityDataSource ID="edsPaymentPassCategory" runat="server"
                        EntitySetName="tbl_PaymentPassCategory"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   
                        

                        <asp:EntityDataSource ID="edsPaymentArticle" runat="server"
                        EntitySetName="tbl_PaymentArticle"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        <asp:EntityDataSource ID="edsPaymentCFO" runat="server"
                        EntitySetName="tbl_PaymentCFO"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   


                        <asp:EntityDataSource ID="edsPaymentPassRuleCompany" runat="server"
                        EntitySetName="tbl_PaymentPassRuleCompany"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        <asp:EntityDataSource ID="edsPayer" runat="server"
                        EntitySetName="tbl_Company"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        <asp:EntityDataSource ID="edsPayerLegalAccount" runat="server"
                        EntitySetName="tbl_CompanyLegalAccount"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        <asp:EntityDataSource ID="edsRecipient" runat="server"
                        EntitySetName="tbl_Company"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" /> 
                          
                        <asp:EntityDataSource ID="edsRecipientLegalAccount" runat="server"
                        EntitySetName="tbl_CompanyLegalAccount"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        
                        
                        
                        

                        </telerik:RadAjaxPanel>                 
                    </asp:Panel>
			    </td>
		    </tr>
            <tr>
			    <td colspan="1">
			        <telerik:RadButton ID="btnUpdate"  ValidationGroup="groupPaymentPassRule" Width="17px" OnClick="btnUpdate_OnClick" runat="server"  CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <Icon PrimaryIconCssClass="rbOk" PrimaryIconTop="4" />
                    </telerik:RadButton>
			        <telerik:RadButton ID="btnInsert" ValidationGroup="groupPaymentPassRule" Width="17px" OnClick="btnInsert_OnClick" runat="server"  CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'>
                        <Icon PrimaryIconCssClass="rbOk" PrimaryIconTop="4" />
                    </telerik:RadButton>			        
                    &nbsp;
                    <telerik:RadButton ID="btnCancel" Width="16px" runat="server" CausesValidation="false" CommandName="Cancel">
                        <Icon PrimaryIconCssClass="rbCancel" PrimaryIconTop="4" />
                    </telerik:RadButton>			        
			    </td>
		    </tr>
	    </tbody>
    </table>
</div>