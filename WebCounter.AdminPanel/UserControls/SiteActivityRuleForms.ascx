<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActivityRuleForms.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActivityRuleForms" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="BodyPrompt" Src="~/UserControls/Widgets/BodyPrompt.ascx" %>

<telerik:RadCodeBlock runat="server">
<script type="text/javascript">
    function OnClientClicking(button, args) {
        window.location = button.get_navigateUrl();
        args.set_cancel(true);
    }
    function AddWufooForm(button, args) {
        ShowWufooFormRadWindow();        
    }
    function openRadWindow(code) {
        var oWnd = radopen("/FormCode.aspx?code=" + code, "RadWindow1");
    }
    function ShowWufooFormRadWindow() {
        $find('<%= rwWufooForm.ClientID %>').show();
        AutoHeight();
    }
    function CloseWufooFormRadWindow() {
        $find('<%= rwWufooForm.ClientID %>').close();
    }
    function AutoHeight() {
        setTimeout('AutoHeightTimeout();', 0);
    }
    function AutoHeightTimeout() {
        var oWnd = $find("<%= rwWufooForm.ClientID %>");
        oWnd.set_height($("#<%= rapWufooRadWindow.ClientID %>").height() + 50);
    }    
    function ShowLoadDataRadWindow() {
        $find('<%= rwLoadData.ClientID %>').show();
    }
    function HideLoadDataRadWindow() {
        $find('<%= rwLoadData.ClientID %>').close();
    }
    function ForceDelete(id) {        
        $find('<%= radAjaxManager.ClientID %>').ajaxRequest("ForceDelete#"+id);
    }
</script>
</telerik:RadCodeBlock>
<table width="100%">
<tr>
<td width="195px" valign="top" ID="leftColumn" runat="server">
<div class="aside" ID="asideDiv" runat="server">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsForms" GridControlID="gridSiteActivityRules" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top">
    <labitec:Search ID="searchSiteActivityRules" GridControlID="gridSiteActivityRules" OnDemand="True" SearchBy="tbl_SiteActivityRules.Name" runat="server" />
    <div class="add-block-buttons">        
        <telerik:RadAjaxPanel runat="server">
            <telerik:RadToolBar ID="rtbFormButtons" runat="server" Skin="Windows7" EnableRoundedCorners="true" EnableShadows="true" OnButtonClick="rtbFormButtons_OnButtonClick">
                <Items>
                 <telerik:RadToolBarSplitButton EnableDefaultButton="True" DefaultButtonIndex="0" Text="Добавить форму">
                    <Buttons>
                        <telerik:RadToolBarButton Text="Добавить форму LeadForce" Value="LeadForce" />
                        <telerik:RadToolBarButton Text="Добавить форму с помощью мастера" Value="Wizard" />
                        <telerik:RadToolBarButton Text="Добавить внешнюю форму" Value="External" />
                        <telerik:RadToolBarButton Text="Добавить форму Wufoo" Value="Wufoo" />
                        <telerik:RadToolBarButton Text="Добавить форму LPgenerator" Value="LPgenerator" />
                    </Buttons>
                </telerik:RadToolBarSplitButton>
                </Items>
            </telerik:RadToolBar>            
        </telerik:RadAjaxPanel>
    </div>
    <div class="clear"></div>    
<div class="smb-file-grid">
<uc:NotificationMessage runat="server" ID="ucLoadDataMessage" MessageType="Success" Style="margin-top: 0" />
<labitec:Grid ID="gridSiteActivityRules" ShowHeader="false" Toolbar="false" AccessCheck="true" Fields="RuleTypeID,Code,FileSize,Description,URL,tbl_RuleTypes.Title,tbl_SiteActivityRules.Name, tbl_SiteActivityRules.WufooRevisionDate" TagsControlID="tagsForms" SearchControlID="searchSiteActivityRules" OnItemDataBound="gridSiteActivityRules_OnItemDataBound"  TableName="tbl_SiteActivityRules" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Name" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
        <ItemTemplate>
            <asp:HyperLink id="spanName" class="span-form-name" runat="server" />
            <div class="span-url">Тип формы: <asp:Literal ID="lType" runat="server" /></div> 
            <div id="spanUrl" class="span-url" runat="server">URL формы: <asp:HyperLink ID="urlLink" runat="server" Target="_blank" /></div>  
            <div class="span-url"><asp:Literal ID="lDescription" runat="server" /></div> 
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="130px" Height="65px" HorizontalAlign="Left" VerticalAlign="Top" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                <asp:LinkButton ID="lbtnLoadData" OnCommand="lbtnLoadData_OnCommand" CssClass="smb-action" runat="server" Visible="False"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDownload2.png" AlternateText="Загрузить" ToolTip="Загрузить" runat="server"/><span style="padding-left: 3px">Загрузить данные</span></asp:LinkButton>
                <asp:LinkButton ID="lbCopy" OnCommand="lbCopy_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Default/images/icoCopy.png" AlternateText="Копировать" ToolTip="Копировать" runat="server"/><span style="padding-left: 3px">Копировать</span></asp:LinkButton>
                <asp:LinkButton ID="lbGetScript" Text="Получить скрипт" CssClass="action-getscript" Visible="false" runat="server"/>
                <asp:LinkButton ID="lbDelete"  Text="Удалить" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" runat="server" CssClass="action-delete"/>
            </ItemTemplate>
        </labitec:GridColumn>
    </Columns>
    <Joins>
        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RuleTypes" JoinTableKey="ID" TableKey="RuleTypeID" runat="server" />
    </Joins>
</labitec:Grid>
</div>
</td>
</tr></table>
<telerik:RadWindow ID="rwLoadData" runat="server" Title="Загрузка данных" Width="230px" Height="120px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        
            <div class="radwindow-popup-inner">
                <telerik:RadDateTimePicker ID="rdtpLoadDataDate" Width="165px" CssClass="datetime-picker" ShowPopupOnFocus="True" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdtpLoadDataDate" ValidationGroup="valGroupLoadData" runat="server">*</asp:RequiredFieldValidator>
                <br /><br />
                <div class="buttons clearfix">
                    <asp:LinkButton ID="lbtnLoadData" OnClick="lbtnLoadData_OnClick" CssClass="btn" ValidationGroup="valGroupLoadData" runat="server"><em>&nbsp;</em><span>Загрузить</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" Text="Отмена" NavigateUrl="javascript:;" onclick="HideLoadDataRadWindow();" />
                </div>
            </div>

    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadWindowManager ID="RadWindowManager1" Width="900px" Height="650px" Title="Получить скрипт" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup radwindow-popup-inner" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" runat="server" />

<telerik:RadWindow ID="rwWufooForm" runat="server" Title="Форма Wufoo" AutoSize="True" AutoSizeBehaviors="Height" Width="500px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel runat="server" ID="rapWufooRadWindow">
            <div class="radwindow-popup-inner bottom-buttons">                
                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" Style="margin-top: 0" />
                <div class="row">
                    <label>Wufoo name:</label>
                    <asp:TextBox runat="server" ID="txtWufooName" CssClass="input-text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWufooName" CssClass="input-text" ValidationGroup="valWufooForm" Text="*" />
                </div>
                <div class="row">
                    <label>Wufoo API Key:</label>
                    <asp:TextBox runat="server" ID="txtWufooAPIKey" CssClass="input-text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWufooAPIKey" CssClass="input-text" ValidationGroup="valWufooForm" Text="*" />
                </div>
                <div class="row">
                    <label>Hash:</label>
                    <asp:TextBox runat="server" ID="txtCode" CssClass="input-text" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCode" CssClass="input-text" ValidationGroup="valWufooForm" Text="*" />
                </div>
                <uc:BodyPrompt runat="server" Code="WufooAddFormInstruction" />
                <br />
                <div class="buttons clearfix">
                    <asp:LinkButton ID="lbntAddWufooForm" OnClick="lbntAddWufooForm_OnClick" CssClass="btn" ValidationGroup="valWufooForm" runat="server"><em>&nbsp;</em><span>Добавить</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" NavigateUrl="javascript:;" onclick="CloseWufooFormRadWindow();" />
                </div>
            </div>            
        </telerik:RadAjaxPanel>
    </ContentTemplate>    
</telerik:RadWindow>