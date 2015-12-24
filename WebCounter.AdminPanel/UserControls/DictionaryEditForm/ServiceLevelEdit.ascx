<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceLevelEdit.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.DictionaryEditForm.ServiceLevelEdit" %>

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
                                    <asp:RequiredFieldValidator CssClass="required" Display="Dynamic" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupServiceLevel" runat="server" />
                                </td>
				            </tr>
                            <tr>
                                <td><label>Срок реакции, часов:</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtReactionTime" Skin="Windows7" EmptyMessage="" MinValue="0" MaxValue="100000000" DbValue='<%# DataBinder.Eval( Container, "DataItem.ReactionTime" ) %>' Type="Number" runat="server" >
					                    <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Срок ответа, часов:</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtResponseTime" Skin="Windows7" EmptyMessage="" MinValue="0" MaxValue="100000000" DbValue='<%# DataBinder.Eval( Container, "DataItem.ResponseTime" ) %>' Type="Number" runat="server" >
					                    <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Действующий:</label></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chxIsActive" Checked='<%# bool.Parse(string.IsNullOrEmpty(DataBinder.Eval( Container, "DataItem.IsActive" ).ToString()) ? "false" : DataBinder.Eval( Container, "DataItem.IsActive" ).ToString()) %>' />
                                </td>
                            </tr>
                            <tr>
                                <td><label>По умолчанию:</label></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chxIsDefault" Checked='<%# bool.Parse(string.IsNullOrEmpty(DataBinder.Eval( Container, "DataItem.IsDefault" ).ToString()) ? "false" : DataBinder.Eval( Container, "DataItem.IsDefault" ).ToString()) %>' />
                                </td>
                            </tr>
			            </tbody>
                    </table>
                    <asp:Panel runat="server" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <h3>Клиенты</h3>
                        <telerik:RadGrid ID="rgServiceLevelClient" Width="100%" Skin="Windows7" runat="server"                        
                             AllowPaging="True" AllowSorting="true" DataSourceID="edsServiceLevelClient"
                             OnItemCreated="rgServiceLevelClient_OnItemCreated"    
                             OnItemDataBound="rgServiceLevelClient_OnItemDataBound"
                             OnInsertCommand="rgServiceLevelClient_OnInsertCommand"
                             OnUpdateCommand="rgServiceLevelClient_OnUpdateCommand"                                                     
                             OnDeleteCommand="rgServiceLevelClient_OnDeleteCommand"                                                   
                             OnDetailTableDataBind="rgServiceLevelClient_OnDetailTableDataBind"       
                              >                                                        
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="edsServiceLevelClient" DataKeyNames="ID, ClientID" CommandItemDisplay="Top">
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="ID, ServiceLevelClientID" DataSourceID="edsServiceLevelContact" Width="100%"
                                        AllowAutomaticInserts="true" AllowAutomaticUpdates="true" AllowAutomaticDeletes="true"
                                        runat="server" CommandItemDisplay="Top" AutoGenerateColumns="false" Name="ServiceLevelContact"                                        
                                        >
                                        <ParentTableRelation>
                                            <telerik:GridRelationFields DetailKeyField="ServiceLevelClientID" MasterKeyField="ID" />
                                        </ParentTableRelation>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false"/>
                                            <telerik:GridBoundColumn DataField="ServiceLevelClientID" HeaderText="ServiceLevelClientID" SortExpression="ServiceLevelClientID" UniqueName="ServiceLevelClientID" Visible="false"/>
                                            <telerik:GridDropDownColumn ListTextField="UserFullName" DataSourceID="edsContacts"
                                                                        ListValueField="ID" UniqueName="ContactID" SortExpression="UserFullName"
                                                                        HeaderText="Контактное лицо" DropDownControlType="RadComboBox" DataField="ContactID"/>
                                            <telerik:GridTemplateColumn UniqueName="ContactEmail" HeaderText="E-mail">
                                                <ItemTemplate>
                                                    <%# Eval("tbl_Contact.Email") %>
                                                </ItemTemplate>
                                                <InsertItemTemplate></InsertItemTemplate>
                                                <EditItemTemplate></EditItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevelRole" EmptyListItemText="---" EnableEmptyListItem="true"
                                                                        ListValueField="ID" UniqueName="ServiceLevelRoleID" SortExpression="Title"
                                                                        HeaderText="Роль в обслуживании" DropDownControlType="RadComboBox" 
                                                                        DataField="ServiceLevelRoleID"/>                                            
                                            <telerik:GridBoundColumn DataField="Comment" UniqueName="Comment" HeaderText="Комментарий" />
                                            <telerik:GridTemplateColumn HeaderText="" FooterText="" EditFormHeaderTextFormat="<h3>Запросы и требования</h3>" Visible="false">
                                                <EditItemTemplate>                                                    
                                                </EditItemTemplate>
                                                <ItemTemplate/>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridCheckBoxColumn DataField="IsAutomateDownload" UniqueName="IsAutomateDownload" HeaderText="Загружать автоматически" />
                                            <telerik:GridCheckBoxColumn DataField="IsInformByRequest" UniqueName="IsInformByRequest" HeaderText="Информировать по запросам" />
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevelInforms"
                                                                        ListValueField="ID" UniqueName="InformRequestID" SortExpression="Title"
                                                                        HeaderText="Информирование по статусу" DropDownControlType="RadComboBox" 
                                                                        DataField="InformRequestID"/>
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevelIncludeToInform"
                                                                        ListValueField="ID" UniqueName="IncludeToInformID" SortExpression="Title"
                                                                        HeaderText="Включать в информирование" DropDownControlType="RadComboBox" 
                                                                        DataField="IncludeToInformID"/>
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevelInformComment"
                                                                        ListValueField="ID" UniqueName="InformCommentID" SortExpression="Title"
                                                                        HeaderText="Информировать по комментариям" DropDownControlType="RadComboBox" 
                                                                        DataField="InformCommentID"/>                              
                                            <telerik:GridTemplateColumn HeaderText="" FooterText="" EditFormHeaderTextFormat="<h3>Счета и документы</h3>" Visible="false" >
                                                <EditItemTemplate>                                                    
                                                </EditItemTemplate>
                                                <ItemTemplate/>
                                            </telerik:GridTemplateColumn>              
                                            <telerik:GridCheckBoxColumn DataField="IsInformAboutInvoice" Visible="false" UniqueName="IsInformAboutInvoice" HeaderText="Информировать о счете" />
                                            <telerik:GridCheckBoxColumn DataField="IsInformInvoiceComments" Visible="false" UniqueName="IsInformInvoiceComments" HeaderText="Информировать по комментариям" />
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsInvoiceInformCatalog"
                                                                        ListValueField="ID" UniqueName="InvoiceInformCatalogID" SortExpression="Title"
                                                                        HeaderText="Высылать реестр счетов" DropDownControlType="RadComboBox" 
                                                                        DataField="InvoiceInformCatalogID" Visible="false" />
                                            <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsInvoiceInformForm"
                                                                        ListValueField="ID" UniqueName="InvoiceInformFormID" SortExpression="Title"
                                                                        HeaderText="Форма счета" DropDownControlType="RadComboBox" 
                                                                        DataField="InvoiceInformFormID" Visible="false" />                              
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                <HeaderStyle Width="20px"/>
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridButtonColumn Text="Delete" 
                                                ConfirmText="Вы действительно хотите удалить запись?" 
                                                ConfirmTitle="Удаление"
                                                ConfirmDialogHeight="100px"
                                                ConfirmDialogWidth="220px"
                                                CommandName="Delete" ButtonType="ImageButton">
                                                <HeaderStyle Width="20px"/>
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn ButtonType="ImageButton"/>
                                        </EditFormSettings>                                        
                                    </telerik:GridTableView>                                    
                                </DetailTables>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" UniqueName="ID" Visible="false"/>
                                    <telerik:GridBoundColumn DataField="ServiceLevelID" HeaderText="ServiceLevelID" SortExpression="ServiceLevelID" UniqueName="ServiceLevelID" Visible="false"/>
                                    <telerik:GridDropDownColumn ListTextField="Name" DataSourceID="edsClients"
                                        ListValueField="ID" UniqueName="ClientID" SortExpression="Name"
                                        HeaderText="Клиент" DropDownControlType="RadComboBox" DataField="ClientID">
                                    </telerik:GridDropDownColumn>                                    
                                    <telerik:GridDateTimeColumn UniqueName="StartDate" DataField="StartDate" SortExpression="StartDate" HeaderText="Дата начала" />
                                    <telerik:GridDateTimeColumn UniqueName="EndDate" DataField="EndDate" SortExpression="EndDate" HeaderText="Дата окончания" />
                                    <telerik:GridNumericColumn NumericType="Number" DecimalDigits="0" DefaultInsertValue="0"
                                    UniqueName="CountOfServiceContacts" DataField="CountOfServiceContacts"
                                    SortExpression="CountOfServiceContacts" HeaderText="Количество обслуживаемых контактных лиц" />
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevelOutOfListServiceContacts"
                                        ListValueField="ID" UniqueName="OutOfListServiceContactsID" SortExpression="Title"
                                        HeaderText="Обслуживание контактных лиц вне списка" DropDownControlType="DropDownList" DataField="OutOfListServiceContactsID">
                                    </telerik:GridDropDownColumn>
                                    <telerik:GridDropDownColumn ListTextField="Title" DataSourceID="edsServiceLevel"
                                        ListValueField="ID" UniqueName="ClientServiceLevelID" SortExpression="Title"
                                        HeaderText="Уровень обслуживания" DropDownControlType="DropDownList" DataField="ServiceLevelID">
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
                        
                        <asp:EntityDataSource ID="edsInvoiceInformCatalog" runat="server"
                        EntitySetName="tbl_InvoiceInformCatalog"
                        ConnectionString="name=WebCounterEntities"
                        DefaultContainerName="WebCounterEntities" />
                        
                        <asp:EntityDataSource ID="edsInvoiceInformForm" runat="server"
                        EntitySetName="tbl_InvoiceInformForm"
                        ConnectionString="name=WebCounterEntities"
                        DefaultContainerName="WebCounterEntities" />

                        <asp:EntityDataSource ID="edsServiceLevelRole" runat="server"
                        EntitySetName="tbl_ServiceLevelRole"
                        ConnectionString="name=WebCounterEntities"
                        DefaultContainerName="WebCounterEntities" />
                        
                        <asp:EntityDataSource ID="edsServiceLevel" runat="server"
                        EntitySetName="tbl_ServiceLevel"
                        ConnectionString="name=WebCounterEntities"
                        DefaultContainerName="WebCounterEntities" />

                        <asp:EntityDataSource ID="edsServiceLevelClient" runat="server"
                        EntitySetName="tbl_ServiceLevelClient"                                                 
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   

                        <asp:EntityDataSource ID="edsClients" runat="server"
                        EntitySetName="tbl_Company"                         
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />
                        
                        <asp:EntityDataSource ID="edsContacts" runat="server"
                        EntitySetName="tbl_Contact"                         
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />
                        
                        <asp:EntityDataSource ID="edsServiceLevelInforms" runat="server"
                        EntitySetName="tbl_ServiceLevelInform"
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />   
                        
                        <asp:EntityDataSource ID="edsServiceLevelIncludeToInform" runat="server"
                        EntitySetName="tbl_ServiceLevelIncludeToInform"
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />                                              
                        
                        <asp:EntityDataSource ID="edsServiceLevelInformComment" runat="server"
                        EntitySetName="tbl_ServiceLevelInformComment"
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />

                        <asp:EntityDataSource ID="edsServiceLevelOutOfListServiceContacts" runat="server"
                        EntitySetName="tbl_ServiceLevelOutOfListServiceContacts"                         
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities" />                           

                        <asp:EntityDataSource ID="edsServiceLevelContact" runat="server"
                        EntitySetName="tbl_ServiceLevelContact" Include="tbl_Contact"
                        EnableUpdate="True" EnableDelete="True" EnableInsert="True"
                        ConnectionString="name=WebCounterEntities" 
                        DefaultContainerName="WebCounterEntities"
                        Where="it.ServiceLevelClientID = @ServiceLevelClientID"
                        >                            
                            <WhereParameters>
                                <asp:Parameter Name="ServiceLevelClientID" DbType="Guid" />
                            </WhereParameters>   
                        </asp:EntityDataSource>
                        </telerik:RadAjaxPanel>                 
                    </asp:Panel>
			    </td>
		    </tr>
            <tr>
			    <td colspan="1">
			        <telerik:RadButton ID="btnUpdate"  ValidationGroup="groupServiceLevel" Width="17px" OnClick="btnUpdate_OnClick" runat="server"  CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>'>
                        <Icon PrimaryIconCssClass="rbOk" PrimaryIconTop="4" />
                    </telerik:RadButton>
			        <telerik:RadButton ID="btnInsert" ValidationGroup="groupServiceLevel" Width="17px" OnClick="btnInsert_OnClick" runat="server"  CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>'>
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