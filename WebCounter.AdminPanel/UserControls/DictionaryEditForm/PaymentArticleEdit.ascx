<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentArticleEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.DictionaryEditForm.PaymentArticleEdit" %>
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
					            <td><label>Наименование:</label></td>
				                <td>
				                    <telerik:RadTextBox ID="txtTitle" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Title" ) %>'/>
                                    <asp:RequiredFieldValidator CssClass="required" Display="Dynamic" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupPaymentCFO" runat="server" />
                                </td>
				            </tr>                            
				            <tr>
					            <td><label>Категория проводки:</label></td>
				                <td>
                                    <uc:DictionaryOnDemandComboBox ID="dcbPaymentPassCategory" DictionaryName="tbl_PaymentPassCategory" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server" ValidationGroup="groupPaymentCFO" ValidationErrorMessage="Вы не указали категорию проводки"/>
                                </td>
				            </tr>                            
				            <tr>
					            <td><label>Примечание:</label></td>
				                <td>
				                    <telerik:RadTextBox ID="txtNote" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Note" ) %>' TextMode="MultiLine"/>
                                </td>
				            </tr>                            
			            </tbody>
                    </table>
                    <asp:Panel runat="server" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <h3>Проводки</h3>
                        <telerik:RadGrid ID="rgPaymentPass" DataSourceID="edsPaymentPass" AutoGenerateColumns="false" runat="server" AllowSorting="true" AllowFilteringByColumn="false" AllowCustomPaging="true" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" PagerStyle-ShowPagerText="false" HeaderStyle-HorizontalAlign="Center">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Категория расход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentPassCategory.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="ЦФО расход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentCFO.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Статья расход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentArticle.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Категория приход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentPassCategory1.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="ЦФО приход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentCFO1.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Статья приход">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentArticle1.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Сумма" DataField="Amount" UniqueName="Amount" HeaderStyle-HorizontalAlign="Left" AllowFiltering="false" AllowSorting="false"/>
                                </Columns>
                                <NoRecordsTemplate>
                                    Нет данных
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>    
                        <asp:EntityDataSource ID="edsPaymentPass" runat="server"
                        EntitySetName="tbl_PaymentPass"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" Include="tbl_PaymentCFO,tbl_PaymentCFO1,tbl_PaymentArticle,tbl_PaymentArticle1,tbl_PaymentPassCategory,tbl_PaymentPassCategory1"/>
                        <br/>
                        <h3>Баланс</h3>
                        <telerik:RadGrid ID="rgPaymentBalance" DataSourceID="edsPaymentBalance" AutoGenerateColumns="false" runat="server" AllowSorting="true" AllowFilteringByColumn="false" AllowCustomPaging="true" AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" PagerStyle-ShowPagerText="false">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Категория" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentPassCategory.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="ЦФО" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentCFO.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Статья" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("tbl_PaymentArticle.Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn HeaderText="Дата"  UniqueName="Date" DataField="Date" HeaderStyle-HorizontalAlign="Center"/>
                                    <telerik:GridNumericColumn HeaderText="Баланс план" NumericType="Number" DecimalDigits="2" UniqueName="BalancePlan" DataField="BalancePlan" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridNumericColumn HeaderText="Баланс факт" NumericType="Number" DecimalDigits="2" UniqueName="BalanceFact" DataField="BalanceFact" HeaderStyle-HorizontalAlign="Center" />
                                </Columns>
                                <NoRecordsTemplate>
                                    Нет данных
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>    
                        <asp:EntityDataSource ID="edsPaymentBalance" runat="server"
                        EntitySetName="tbl_PaymentBalance"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" Include="tbl_PaymentCFO,tbl_PaymentArticle,tbl_PaymentPassCategory"/>
                        </telerik:RadAjaxPanel>                 
                    </asp:Panel>
			    </td>
		    </tr>
            <tr>
			    <td colspan="1">
			        <telerik:RadButton ID="btnUpdate"  ValidationGroup="groupPaymentCFO" Width="17px" OnClick="btnUpdate_OnClick" runat="server"  CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <Icon PrimaryIconCssClass="rbOk" PrimaryIconTop="4" />
                    </telerik:RadButton>
			        <telerik:RadButton ID="btnInsert" ValidationGroup="groupPaymentCFO" Width="17px" OnClick="btnInsert_OnClick" runat="server"  CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'>
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