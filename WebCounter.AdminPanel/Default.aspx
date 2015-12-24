<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebCounter.AdminPanel.Default" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="Widgets" Src="~/UserControls/Widgets/Master/MainModule.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    
<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        var chartsRadWindow;
        var ajaxManager;
        var chartLoaded = false;
        function pageLoad() {
            chartsRadWindow = $find('<%= chartsRadWindow.ClientID %>');
            ajaxManager = $find("<%= AjaxManager.ClientID %>");
            if (window.location.href.indexOf("chart") > 0)
                chartLoaded = true;    
        }
        function RadDockPositionChanged(dock) {
            ajaxManager.ajaxRequest('Move');
        }
        function RadDockClientCommand(sender, eventArgs) {
            ajaxManager.ajaxRequest(eventArgs.command.get_name());
        }
        function ShowChartsRadWindow() {
            ajaxManager.ajaxRequest('InitRadWindow');
            chartsRadWindow.show();
        }   
        function onTabSelected(sender, args) {
            if (!chartLoaded && args.get_tab().get_value() == 'Chart') {
                window.location.href = '<%= UrlsData.AP_Home() + "?tab=chart" %>';                
            }
        }

    </script>
</telerik:RadScriptBlock>
<telerik:RadWindow runat="server" Title="Добавить график" Width="900px" Height="350px" ID="chartsRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>              
        <div class="radwindow-popup-inner bottom-buttons">
            <telerik:RadAjaxPanel runat="server">                
                <div class="row">
                    <label>График:</label>
                    <telerik:RadComboBox ID="rcbReports" CausesValidation="false" EmptyMessage="Выберите график" AllowCustomText="false" AutoPostBack="true" ValidationGroup="valSave" OnSelectedIndexChanged="rcbReports_OnSelectedIndexChanged" OnItemDataBound="rcbReports_OnItemDataBound" HighlightTemplatedItems="true" Filter="Contains" Width="690px" EnableEmbeddedSkins="false" Skin="Labitec" runat="server">
                        <HeaderTemplate>
                            <ul class="charts-combobox">
                                <li class="col1">Название</li>
                                <li class="col2">Модуль</li>
                                <li class="col3">Описание</li>
                            </ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <ul class="charts-combobox">
                                <li class="col1"><%# DataBinder.Eval(Container.DataItem, "Title") %></li>
                                <li class="col2"><%# DataBinder.Eval(Container.DataItem, "tbl_Module.Title")%></li>
                                <li class="col3"><%# DataBinder.Eval(Container.DataItem, "Description") %></li>
                            </ul>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvDictionary" ValidationGroup="valSave" ControlToValidate="rcbReports" CssClass="required" Text="*" ErrorMessage="Вы не выбрали значение" runat="server" InitialValue=""/>
                </div>
                <div class="row">
                    <label>Название:</label>
                    <asp:TextBox runat="server" ID="txtName" CssClass="input-text" Width="676px" />
                    <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtName" Text="*" ErrorMessage="Вы не ввели название графика" ValidationGroup="valSave" runat="server" />
                </div>
                <asp:Panel runat="server" ID="plSelectAxis" Visible="false" CssClass="row">
                    <label>Выбор оси:</label>
                    <asp:DropDownList runat="server" ID="ddlSelectAxis" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectAxis_OnSelectedIndexChanged" />
                </asp:Panel>
                <asp:Panel runat="server" ID="plSelectedAxisValues" Visible="false" CssClass="row">
                    <label>Легенда:</label>
                    <br/><br/>
                    <asp:CheckBoxList runat="server" ID="chxlSeries" RepeatLayout="UnorderedList" CssClass="series-list clearfix"/>
                </asp:Panel>
                <div class="buttons">
                    <asp:LinkButton ID="lbtnAdd" OnClick="lbtnAdd_OnClick" CssClass="btn" ValidationGroup="valSave" runat="server"><em>&nbsp;</em><span>Добавить</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="chartsRadWindow.close();" />
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
     <telerik:RadTabStrip ID="RadTabStrip1" OnClientTabSelected="onTabSelected" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" >
		<Tabs>
			<telerik:RadTab Text="Ключевые показатели" />
			<telerik:RadTab Text="Графики" Value="Chart" />
		</Tabs>
	</telerik:RadTabStrip>

	<telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
	    <telerik:RadPageView ID="RadPageView2" runat="server">            
            <asp:Panel runat="server" ID="plShowDomainStatusSettings" CssClass="site-domain-widget clearfix">
                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />                
                <uc:NotificationMessage runat="server" ID="ucNotificationMessageDomainExists" MessageType="Warning" />
                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
                                        ValidationGroup="valGroup"
						                />
                <div class="row">
                    <label>Ссылка:</label>
                    <asp:TextBox runat="server" ID="txtDomain" CssClass="input-text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDomain" Display="Dynamic" ErrorMessage="Вы не ввели ссылку" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                    
                </div>
                <div class="row">
                    <label>Псевдонимы:</label>
                    <asp:TextBox runat="server" ID="txtAliases" CssClass="area-text" Width="700px" TextMode="MultiLine" Height="30px" />                    
                </div>
                <div class="row">
                    <label>Примечание:</label>
                    <asp:Literal runat="server" ID="lrlNote" />
                </div>
                <br/>
	            <div class="buttons">
		            <asp:LinkButton ID="lbtnSaveAndCheckDomain" OnClick="lbtnSaveAndCheckDomain_OnClick" CssClass="btn" runat="server" ValidationGroup="valGroup"><em>&nbsp;</em><span>Проверить и сохранить</span></asp:LinkButton>		            
	            </div>
            </asp:Panel>
	        <uc:Widgets runat="server" ID="ucWidgets" />
        </telerik:RadPageView>
		<telerik:RadPageView ID="RadPageView1" runat="server">
		    <asp:Panel runat="server" ID="plCharts" Visible="false">         
                <div class="buttons right clearfix">
                    <a class="btn" href="javascript:;" onclick="ShowChartsRadWindow()"><em>&nbsp;</em><span>Добавить график</span></a>
                </div>
                <br/>
                <telerik:RadDockLayout runat="server" ID="radDockLayout" OnLoadDockLayout="radDockLayout_OnLoadDockLayout">
                    <telerik:RadDockZone runat="server" ID="radDockZone" Orientation="Horizontal" Skin="Windows7">    
                    </telerik:RadDockZone>
                </telerik:RadDockLayout>
                <asp:Panel runat="server" ID="plEmptyMessage">
                    <p>Для добавления графиков на страницу нажмите на кнопку "Добавить график".</p>
                </asp:Panel>        
            </asp:Panel>
        </telerik:RadPageView>        
    </telerik:RadMultiPage>
</asp:Content>
