<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileSites.aspx.cs" Inherits="WebCounter.AdminPanel.ProfileSites" %>
<%@ Register TagPrefix="uc" TagName="MenuConstructor" Src="~/UserControls/MenuConstructor.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="WidgetSettings" Src="~/UserControls/SiteSettings/WidgetSettings.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function ShowModuleEditionRadWindow() { $find('<%= rwModuleEdition.ClientID %>').show(); }
            function CloseModuleEditionRadWindow() { $find('<%= rwModuleEdition.ClientID %>').close(); }
            function InitModuleEditionRadWindow(accessProfileModuleId) {
                $find('<%= radAjaxManager.ClientID %>').ajaxRequest(accessProfileModuleId);
            }            
        </script>
    </telerik:RadScriptBlock>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Профили">
                            <ContentTemplate>
                                <div class="aside-content">
                                    <div style="position: relative;">
                                        <telerik:RadTextBox ID="txtAccessProfileTitle" CssClass="tbField" Skin="Windows7" Width="106" runat="server" /><telerik:RadButton ID="btnAddProfile" OnClick="btnAddProfile_OnClick" Text="" Skin="Windows7" runat="server" />
                                    </div>
                                    <telerik:RadListBox ID="rlbAccessProfile" OnSelectedIndexChanged="rlbAccessProfile_OnSelectedIndexChanged" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false" AutoPostBack="true" runat="server" />
                                    <asp:Repeater ID="rAccessProfile" runat="server">
                                        <ItemTemplate>
                                            <%# Eval("Title") %>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="access-profiles">
        <asp:Panel runat="server" ID="plProfileInfo">
            <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Success" />
            <div class="row">
                <label>Название профиля:</label>
                <asp:TextBox runat="server" ID="txtProfileTitle" CssClass="input-text" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProfileTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupProfileTitle" runat="server" />
            </div>
            <div class="row">
                <label>Продукт:</label>
                <uc:DictionaryComboBox ID="dcbProduct" DictionaryName="tbl_Product" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
            </div>            
            <h3>Ограничения профиля</h3>
            <div style="width: 450px; float: left;">
                <div class="row">
                    <label>Количество доменов:</label>
                    <telerik:RadNumericTextBox runat="server" ID="rntxtDomainsCount" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
					    <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
				    </telerik:RadNumericTextBox>
                </div>
                <div class="row">
                    <label>Количество email одному контакту:</label>
                    <telerik:RadNumericTextBox runat="server" ID="rntxtEmailPerContactCount" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
					    <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
				    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div style="width: 450px; float: left;">
                <div class="row">
                    <label>Количество активных контактов:</label>
                    <telerik:RadNumericTextBox runat="server" ID="rntxtActiveContactsCount" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
					    <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
				    </telerik:RadNumericTextBox>
                </div>
                <div class="row">
                    <label>Длительность периода, дней:</label>
                    <telerik:RadNumericTextBox runat="server" ID="rntxtDurationPeriod" MinValue="0" EmptyMessage="" CssClass="input-text" Type="Number">
					    <NumberFormat GroupSeparator="" DecimalDigits="0" />                           
				    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div class="clear"></div>

            <div class="row">
                <label>URL страницы контактов:</label>
                <asp:TextBox runat="server" ID="txtContactsPageUrl" CssClass="input-text" Width="670px" />
            </div>
            <asp:LinkButton ID="lbtnSave" style="vertical-align: top" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupProfileTitle" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>        
            <br/><br/>
        </asp:Panel>
        
        
	    <telerik:RadTabStrip ID="TabStrip" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		    <Tabs>
			    <telerik:RadTab Text="Доступ к модулям" />
                <telerik:RadTab Text="Рабочие столы" />
                <telerik:RadTab Text="Настройка интерфейса" />
		    </Tabs>
	    </telerik:RadTabStrip>

	    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		    <telerik:RadPageView ID="RadPageView1" CssClass="clearfix" runat="server">
                <labitec:Grid ID="gridAccessProfileModule" TableName="tbl_AccessProfileModule" Toolbar="false" Fields="tbl_ModuleEdition.ID" OnItemDataBound="gridAccessProfileModule_OnItemDataBound" ClassName="WebCounter.AdminPanel.AccessProfileModule" runat="server">
                    <Columns>
                        <labitec:GridColumn ID="GridColumn1" DataField="tbl_Module.Title" HeaderText="Модуль" runat="server"/>
                        <labitec:GridColumn ID="GridColumn2" DataField="Read" HeaderText="Чтение" HorizontalAlign="Center" Width="100px" runat="server">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbRead" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                            </ItemTemplate>
                        </labitec:GridColumn>
                        <labitec:GridColumn ID="GridColumn3" DataField="Write" HeaderText="Изменение" HorizontalAlign="Center" Width="100px" runat="server">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbWrite" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                            </ItemTemplate>
                        </labitec:GridColumn>
                        <labitec:GridColumn ID="GridColumn4" DataField="Delete" HeaderText="Удаление" HorizontalAlign="Center" Width="100px" runat="server">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbDelete" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                            </ItemTemplate>
                        </labitec:GridColumn>
                        <labitec:GridColumn runat="server" DataField="tbl_ModuleEdition.Title" HeaderText="Редакция">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlSelectEdition" NavigateUrl="javascript:;">Выбрать редакцию</asp:HyperLink>                                
                            </ItemTemplate>
                        </labitec:GridColumn>
                    </Columns>
                    <Joins>
                        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_Module" JoinTableKey="ID" TableKey="ModuleID" runat="server" />
                        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_ModuleEdition" JoinTableKey="ID" TableKey="ModuleEditionID" runat="server" />
                    </Joins>
                </labitec:Grid>
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <uc:MenuConstructor ID="ucMenuConstructor" runat="server" IsSiteProfiles="true" />
            </telerik:RadPageView>
            <telerik:RadPageView runat="server">
                <uc:WidgetSettings runat="server" ID="ucWidgetSettings" />
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <telerik:RadWindow runat="server" Title="Выбрать редакцию" Width="600px" Height="300px" ID="rwModuleEdition" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
        <ContentTemplate>            
            <div class="radwindow-popup-inner bottom-buttons">
                <div class="row">
                    <label>Редакция:</label>
                    <uc:DictionaryComboBox runat="server" ID="dcbModuleEdition" DictionaryName="tbl_ModuleEdition" AutoPostBack="True" OnSelectedIndexChanged="dcbModuleEdition_OnSelectedIndexChanged" />
                </div>
                <asp:Panel runat="server" ID="plOptions" CssClass="row" Visible="false">
                    <label>Опции:</label><br/><br/>
                    <asp:CheckBoxList ID="chxOptions" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="checkbox-list" />
                </asp:Panel>                    
                <div class="buttons">
                    <asp:LinkButton ID="lbtnSelectModuleEdition" CssClass="btn" OnClick="lbtnSelectModuleEdition_OnClick" runat="server"><em>&nbsp;</em><span>Выбрать</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseModuleEditionRadWindow();" />
                </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>