<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactRoleEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.DictionaryEditForm.ContactRoleEdit" %>

<style type="text/css">
    .dictionary-edit { margin: 5px; }
    td { padding-bottom: 5px; }
</style>

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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" Display="Dynamic" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupContactRule" runat="server" />
                                </td>
				            </tr>
				            <tr>
					            <td><label>Описание:</label></td>
				                <td>
				                    <telerik:RadTextBox ID="txtDescription" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Description" ) %>' TextMode="MultiLine"/>
                                </td>
				            </tr>
			            </tbody>
                    </table>

	                <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" CausesValidation="False" AutoPostBack="True" OnTabClick="RadTabStrip1_OnTabClick" runat="server">
		                <Tabs>
			                <telerik:RadTab Text="Общий адрес" Value="GeneralEmail" PageViewID="RadPageView1" />
			                <telerik:RadTab Text="Адрес для контакта" Value="ContactRole" PageViewID="RadPageView2" />
			                <telerik:RadTab Text="Адрес для процесса" Value="WorkflowRole" PageViewID="RadPageView2" />
		                </Tabs>
	                </telerik:RadTabStrip>
                    
	                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		                <telerik:RadPageView ID="RadPageView1" runat="server">
			                <table width="100%">
	                            <tbody>
				                    <tr>
					                    <td><label>Email:</label></td>
				                        <td>
				                            <telerik:RadTextBox ID="txtEmail" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Email" ) %>'/>
                                            <asp:RequiredFieldValidator ID="rfvEmail" CssClass="required" Display="Dynamic" ControlToValidate="txtEmail" Text="*" ErrorMessage="Вы не ввели 'Email'" ValidationGroup="groupContactRule" runat="server" />
                                            <asp:RegularExpressionValidator ID="revEmail" CssClass="required" ErrorMessage="Неверный формат Email." ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" Display="Dynamic" ValidationGroup="groupContactRule" runat="server">Неверный формат Email.</asp:RegularExpressionValidator>
                                        </td>
				                    </tr>
				                    <tr>
					                    <td><label>Имя:</label></td>
				                        <td>
				                            <telerik:RadTextBox ID="txtDisplayName" Skin="Windows7" Wrap="false" Width="300px" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.DisplayName" ) %>'/>
                                        </td>
				                    </tr>
			                    </tbody>
                            </table>
		                </telerik:RadPageView>
                        
		                <telerik:RadPageView ID="RadPageView2" runat="server">
		                    <table>
		                        <tbody>
		                            <tr>
                                        <td><label>Метод определения контакта:</label></td>
                                        <td>
			                                <asp:RadioButtonList ID="rblMethodAssigningResponsible" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server">
                                                <asp:ListItem Text="По очереди" Value="0" Selected="True" runat="server" />
                                                <asp:ListItem Text="Наиболее свободного" Value="1" runat="server" />
			                                </asp:RadioButtonList>
                                        </td>
		                            </tr>
                                    <tr>
                                        <td><label>Целевая аудитория:</label></td>
                                        <td>
                                            <asp:RadioButtonList ID="rblTargetContacts" ClientIDMode="AutoID" AutoPostBack="True" OnSelectedIndexChanged="rblTargetContacts_OnSelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server" />
                                        </td>
                                    </tr>
		                        </tbody>
		                    </table>
                            
                            <asp:Panel ID="pnlTags" runat="server">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <div class="row" style="color: #000;">
                                                <label>Сегмент:</label>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Literal ID="litTag" runat="server" />
                                            <asp:Panel ID="pnlTagsList" CssClass="row" runat="server">
                                                <asp:RadioButtonList ID="rblTags" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table" CssClass="radiobuttonlist-horizontal" runat="server" />                
                                                <asp:RequiredFieldValidator ID="rfvTags" ControlToValidate="rblTags" ErrorMessage="Вы не выбрали 'Сегмент'" ValidationGroup="groupContactRule" runat="server">*</asp:RequiredFieldValidator>                                    
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
            
                            <asp:Panel ID="pnlSelectContacts" Visible="False" runat="server">
                                <telerik:RadButton ID="rbAdd" ClientIDMode="AutoID" OnClick="rbAdd_OnClick" Text="Добавить контакты" CausesValidation="False" Skin="Windows7" runat="server" />
                                <br /><br />
                               
                                <labitec:Grid ID="gridContacts" TableName="tbl_Contact" AccessCheck="true" OnItemDataBound="gridContacts_OnItemDataBound" ClassName="WebCounter.AdminPanel.MassMailContacts" Export="true" runat="server">
                                    <Columns>
                                        <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                                        <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn5" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn6" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn7" DataField="tbl_Status.Title" HeaderText="Статус" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn11" DataField="ID" HeaderText="Действия" Width="100px" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDelete" OnCommand="ibtnDelete_OnCommand" CommandName="Delete" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" ToolTip="Удалить" AlternateText="Удалить" CausesValidation="False" runat="server"/>
                                            </ItemTemplate>
                                        </labitec:GridColumn>
                                    </Columns>
                                    <Joins>
                                        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
                                        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
                                        <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Status" JoinTableKey="ID" TableKey="StatusID" runat="server" />
                                    </Joins>
                                </labitec:Grid>
                                
                            </asp:Panel>
		                </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
	        </tr>
            <tr>
			    <td colspan="1">
			        <telerik:RadButton ID="btnUpdate"  ValidationGroup="groupContactRule" Width="17px" OnClick="btnUpdate_OnClick" runat="server" CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <Icon PrimaryIconCssClass="rbOk" PrimaryIconTop="4" />
                    </telerik:RadButton>
			        <telerik:RadButton ID="btnInsert" ValidationGroup="groupContactRule" Width="17px" OnClick="btnInsert_OnClick" runat="server" CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'>
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