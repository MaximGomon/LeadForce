<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Contact.Contact" %>
<%@ Register TagPrefix="uc" TagName="ContactActivityList" Src="~/UserControls/Contact/ContactActivityList.ascx" %>
<%@ Register TagPrefix="uc" TagName="SiteActionList" Src="~/UserControls/SiteActionList.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactEdit" Src="~/UserControls/Contact/ContactEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="TaskList" Src="~/UserControls/Task/TaskList.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="PopupSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/PopupSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>


<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function <%= this.ClientID %>_ShowWorkflowTemplateRadWindow() {
            var oWnd = $find("<%= rwWorkflowTemplate.ClientID %>");
            oWnd.show();
            //oWnd.center();
        }
        
        function <%= this.ClientID %>_CloseWorkflowTemplateRadWindow() {
            $find('<%= rwWorkflowTemplate.ClientID %>').close();
        }
//        
//        function ShowSiteActionTemplate() {
//            <%--= ucPopupSiteActionTemplate.ClientID %>_ShowSiteActionTemplateRadWindow('<%= Guid.Empty %>');--%>
//        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadWindow ID="rwWorkflowTemplate" runat="server" Title="Запуск процесса" Width="400px" Height="120px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
            <div class="radwindow-popup-inner siteaction-template-popup">
                <telerik:RadAjaxPanel runat="server">
                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                        <div class="row">
                            <uc:DictionaryOnDemandComboBox ID="dcbWorkflowTemplate" Width="365px" DictionaryName="tbl_WorkflowTemplate" DataTextField="Name" ShowEmpty="True" EmptyItemText="Выберите шаблон процесса" ValidationGroup="groupWorkflowTemplate" ValidationErrorMessage="Выберите шаблон процесса" CssClass="select-text" runat="server" />
                        </div>
	                    <div class="buttons">
		                    <asp:LinkButton ID="lbRunWorkflow" OnClick="lbRunWorkflow_OnClick" CssClass="btn" ValidationGroup="groupWorkflowTemplate" runat="server"><em>&nbsp;</em><span>Запустить</span></asp:LinkButton>
		                    <a href="javascript:;" onclick='<%= this.ClientID %>_CloseWorkflowTemplateRadWindow();' class="cancel">Отмена</a>
	                    </div>
                    </telerik:RadCodeBlock>
                </telerik:RadAjaxPanel>
            </div>
    </ContentTemplate>
</telerik:RadWindow>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar2" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsContact" ObjectTypeName="tbl_Contact" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <div>
	            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		            <Tabs>
			            <telerik:RadTab Text="Карточка контакта" />
			            <telerik:RadTab Text="Дополнительные реквизиты" />
			            <telerik:RadTab Text="История сессий" />
			            <telerik:RadTab Text="Лог действий" Value="ContactActivity" />
			            <telerik:RadTab Text="Сообщения" Value="SiteAction" />
                        <telerik:RadTab Text="Задачи" Value="Tasks" />
		            </Tabs>
	            </telerik:RadTabStrip>

	            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		            <telerik:RadPageView ID="RadPageView1" runat="server">
			            <uc:ContactEdit runat="server" ID="ucContactEdit" />
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView2" CssClass="clearfix" runat="server">
			            <labitec:Grid ID="gridContactColumnValues" Fields="tbl_SiteColumns.TypeID, StringValue, DateValue, LogicalValue, tbl_SiteColumnValues.Value" OnItemDataBound="gridContactColumnValues_OnItemDataBound" TableName="tbl_ContactColumnValues" ClassName="WebCounter.AdminPanel.ContactColumnValues" runat="server">
				            <Columns>
					            <labitec:GridColumn ID="GridColumn1" DataField="tbl_ColumnCategories.Title" HeaderText="Категория" runat="server"/>
					            <labitec:GridColumn ID="GridColumn2" DataField="tbl_SiteColumns.Name" HeaderText="Название" runat="server"/>
					            <labitec:GridColumn ID="GridColumn3" DataField="ContactID" HeaderText="Значение" runat="server">
						            <ItemTemplate>
							            <asp:Literal ID="ContactColumnValue" runat="server" EnableViewState="false" />
						            </ItemTemplate>
					            </labitec:GridColumn>
				            </Columns>
				            <Joins>
					            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_SiteColumns" JoinTableKey="ID" TableKey="SiteColumnID" runat="server" />
					            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_ColumnCategories" JoinTableKey="ID" TableName="tbl_SiteColumns" TableKey="CategoryID" runat="server" />
					            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_SiteColumnValues" JoinTableKey="ID" TableKey="SiteColumnValueID" runat="server" />
				            </Joins>
			            </labitec:Grid>
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView3" CssClass="clearfix" runat="server">
			            <div class="aside">
				            <telerik:RadPanelBar ID="RadPanelBar1" Width="189" Skin="Windows7" runat="server">
					            <Items>
						            <telerik:RadPanelItem Expanded="true" Text="Теги">
							            <ContentTemplate>
								            <labitec:Tags ID="tagsContactSessions" GridControlID="gridContactSessions" runat="server" />
							            </ContentTemplate>
						            </telerik:RadPanelItem>
						            <telerik:RadPanelItem Expanded="true" Text="Фильтры">
							            <ContentTemplate>
								            <labitec:Filters ID="filtersContactSessions" GridControlID="gridContactSessions" runat="server" />
							            </ContentTemplate>
						            </telerik:RadPanelItem>
					            </Items>
				            </telerik:RadPanelBar>
			            </div>

			            <labitec:Grid ID="gridContactSessions" OnItemDataBound="gridContactSessions_OnItemDataBound" Fields="tbl_Browsers.Version, tbl_OperatingSystems.Version" TableName="tbl_ContactSessions" ClassName="WebCounter.AdminPanel.ContactSessions" TagsControlID="tagsContactSessions" FiltersControlID="filtersContactSessions" Export="true" runat="server">
				            <Columns>
					            <labitec:GridColumn ID="GridColumn4" DataField="UserSessionNumber" HeaderText="Порядковый номер" DataType="Int32" runat="server">
						            <ItemTemplate>
							            <asp:Literal ID="lUserSessionNumber" runat="server" />
						            </ItemTemplate>
					            </labitec:GridColumn>
					            <labitec:GridColumn ID="GridColumn5" DataField="SessionDate" HeaderText="Дата сессии" DataType="DateTime" runat="server"/>
					            <labitec:GridColumn ID="GridColumn6" DataField="tbl_AdvertisingPlatform.Title" HeaderText="Источник" runat="server" />
					            <labitec:GridColumn ID="GridColumn7" DataField="tbl_AdvertisingType.Title" HeaderText="Среда" runat="server" />
					            <labitec:GridColumn ID="GridColumn8" DataField="tbl_AdvertisingCampaign.Title" HeaderText="Кампания" runat="server" />
					            <labitec:GridColumn ID="GridColumn9" DataField="tbl_Browsers.Name" HeaderText="Браузер" runat="server">
						            <ItemTemplate>
							            <asp:Literal ID="lBrowser" runat="server" />
						            </ItemTemplate>
					            </labitec:GridColumn>
					            <labitec:GridColumn ID="GridColumn10" DataField="tbl_OperatingSystems.Name" HeaderText="Операционная система" runat="server">
						            <ItemTemplate>
							            <asp:Literal ID="lOperatingSystem" runat="server" />
						            </ItemTemplate>
					            </labitec:GridColumn>
				            </Columns>
				            <Joins>
					            <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_AdvertisingPlatform" JoinTableKey="ID" TableKey="AdvertisingPlatformID" runat="server" />
					            <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_AdvertisingType" JoinTableKey="ID" TableKey="AdvertisingTypeID" runat="server" />
					            <labitec:GridJoin ID="GridJoin6" JoinTableName="tbl_AdvertisingCampaign" JoinTableKey="ID" TableKey="AdvertisingCampaignID" runat="server" />
					            <labitec:GridJoin ID="GridJoin7" JoinTableName="tbl_Browsers" JoinTableKey="ID" TableKey="BrowserID" runat="server" />
					            <labitec:GridJoin ID="GridJoin8" JoinTableName="tbl_OperatingSystems" JoinTableKey="ID" TableKey="OperatingSystemID" runat="server" />
				            </Joins>
			            </labitec:Grid>
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView4" CssClass="clearfix" runat="server">
			            <uc:ContactActivityList ID="ContactActivityList1" runat="server" ShowWidgets="false" />
		            </telerik:RadPageView>
		            <telerik:RadPageView ID="RadPageView5" CssClass="clearfix" runat="server">
			            <uc:SiteActionList runat="server" ShowWidgets="false" />
		            </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView6" CssClass="clearfix" runat="server">
		                <uc:TaskList runat="server" ID="ucTaskList" />
		            </telerik:RadPageView>
	            </telerik:RadMultiPage>

	            <br />

	            <telerik:RadToolTip ID="rttSiteActivityRules" ManualClose="true" ManualCloseButtonText="Закрыть" Modal="true" TargetControlID="BtnFillForm" ShowEvent="OnClick" Position="Center" RelativeTo="BrowserWindow" runat="server">
		            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
			            <ContentTemplate>
				            <asp:GridView ID="gvSiteActivityRules" 
								            runat="server" 
								            OnPageIndexChanging="gvSiteActivityRules_PageIndexChanging" 
								            AutoGenerateColumns="False" 
								            Width="600px" 
								            AllowPaging="true" 
								            PageSize="15" 
								            OnSorting="gvSiteActivityRules_Sorting"
								            OnSorted="gvSiteActivityRules_Sorted"
								            CssClass="grid"  
								            GridLines="None"   
								            AllowSorting="true"          
								            >    
					            <Columns>
						            <asp:TemplateField HeaderText="Название правила" ItemStyle-Width="250px" HeaderStyle-CssClass="first" ItemStyle-CssClass="first" SortExpression="Name">
							            <ItemTemplate>
								            <%# Eval("Name").ToString()%>
							            </ItemTemplate>  
						            </asp:TemplateField>   
						            <asp:TemplateField HeaderText="Код" SortExpression="Code">
							            <ItemTemplate>
								            <%# Eval("Code").ToString()%>
							            </ItemTemplate>        
						            </asp:TemplateField> 
						            <asp:TemplateField HeaderText="Действия" HeaderStyle-HorizontalAlign="Center"
							            ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
							            <ItemTemplate>
                                            <a href="javascript:;" onclick="HideTooltip(); WebCounter.SiteID='<%# Eval("SiteID").ToString() %>'; WebCounter.ContactID='<%= _contactID %>'; WebCounter.LG_Form('<%# Eval("Code").ToString()%>','1')">Выбрать</a>
							            </ItemTemplate>
						            </asp:TemplateField>   
					            </Columns>
					            <EmptyDataTemplate>
						            Нет данных.
					            </EmptyDataTemplate>
					            <PagerSettings Mode="Numeric" PreviousPageText="Предыдущая" NextPageText="Следующая" />
					            <PagerStyle CssClass="grid-pager" />
				            </asp:GridView>
			            </ContentTemplate>
		            </asp:UpdatePanel>
	            </telerik:RadToolTip>
                <%--<uc:PopupSiteActionTemplate ID="ucPopupSiteActionTemplate" OnTemplateSaved="ucPopupSiteActionTemplate_OnTemplateSaved" SiteActionTemplateCategory="Personal" runat="server" />--%>
	            <div class="buttons">        
		            <asp:LinkButton ID="BtnFillForm" CssClass="btn" style="margin-right: 10px;" runat="server"><em>&nbsp;</em><span>Заполнить форму</span></asp:LinkButton>
		            <%--<asp:HyperLink ID="hlSendMessage" CssClass="btn" NavigateUrl="javascript:;" onclick="ShowSiteActionTemplate();" style="margin-right: 10px;" runat="server"><em>&nbsp;</em><span>Отправить сообщение</span></asp:HyperLink>--%>
                    <a ID="aWorkflow" href="javascript:;" class="btn" style="margin-right: 10px;" runat="server"><em>&nbsp;</em><span>Запуск процесса</span></a>
		            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupUpdateContact" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		            <div class="clear"></div>
		            <br />
	            </div>
            </div>
        </td>
    </tr>
</table>