<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreatePublication.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ActivityRibbon.CreatePublication" %>
<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/CompanyComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="SearchResultContainer" Src="~/UserControls/Shared/SearchResultContainer.ascx" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<link href='<%= ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%= ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>

<script type="text/javascript">
    function ShowAddPublicationRadWindow() {
        $find('<%= radAjaxManager.ClientID %>').ajaxRequest("Init");        
    }
    function InitAndShowPublicationPopup(publicationId) {
        $find('<%= radAjaxManager.ClientID %>').ajaxRequest(publicationId);
    }

    function CloseUpdatePublicationRadWindow(id) {
        $("#" + id + " .publication-container > h3.title a").html($("#txtTitle").val());        
        $("#" + id + " p.description").html($("#txtComment").val());        
        $("#" + id + " ul.operations span.status").html($("#rcbPublicationStatus_Input").val());
        $("#" + id + " ul.operations li.category").html($("#radCategories_Input").val() + "<span>&nbsp;</span>");                                
        $find('<%= addPublicationRadWindow.ClientID %>').close();
    }
    
    function CloseAddPublicationRadWindow(empty) { $find('<%= addPublicationRadWindow.ClientID %>').close(); InitCommentInputs(); if (empty) { $("#txtPublicationText").val(''); } }
    function KeyUp(e, value) {
        var code = e.keyCode ? e.keyCode : e.charCode;
        if (code == 13 && value != '')
            ShowAddPublicationRadWindow();
        else {
            var evtobj = window.event ? event : e;
            if (!evtobj.altKey && !evtobj.ctrlKey && !evtobj.shiftKey)
                leadForceSearch.input(value);
        }
    }
    function ClientTabSelectedHandler(sender, eventArgs) {
        $find('<%= txtPublicationText.ClientID %>').set_emptyMessage(eventArgs.get_tab()._imageElement.alt);                
    }    
</script>

</telerik:RadCodeBlock>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" CssClass="publication-create clearfix">        
        <telerik:RadTabStrip ID="rtsPublicationTypes" runat="server" Skin="Windows7" OnClientTabSelected="ClientTabSelectedHandler"/>
        <div class="publication-text-container">
            <telerik:RadTextBox runat="server" ID="txtPublicationText" ClientIDMode="Static" Width="100%" autocomplete="off" onkeyup="KeyUp(event, this.value);" />            
        </div>
        <telerik:RadWindow runat="server" Title="Поручить" Width="780px" Height="410px" ID="addPublicationRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
            <ContentTemplate>
                <asp:Panel runat="server" ID="plEditPublication" CssClass="radwindow-popup-inner add-publication">
                    <asp:UpdatePanel ID="upCreatePublication" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <label>Заголовок:</label>
                                <asp:TextBox runat="server" ID="txtTitle" ClientIDMode="Static" CssClass="input-text" Width="600px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" CssClass="input-text" ValidationGroup="groupAddPublication" Text="*" ErrorMessage='Укажите заголовок'/>
                            </div>
                            <div class="row">
                                <label>Комментарий:</label>
                                <asp:TextBox runat="server" ID="txtComment" ClientIDMode="Static" CssClass="area-text" TextMode="MultiLine" />
                            </div>
                            <div class="left-column">
                                <div class="row">
                                    <label>Статус:</label>
                                    <telerik:RadComboBox ID="rcbPublicationStatus" ClientIDMode="Static" CssClass="select-text" EnableLoadOnDemand="true" skin="Labitec" Width="234px" EnableEmbeddedSkins="false" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server"/>
                                    <asp:RequiredFieldValidator ID="rfvrcbPublicationStatus" InitialValue="Выберите значение" ControlToValidate="rcbPublicationStatus" Text="*" ErrorMessage="Вы не выбрали статус" ValidationGroup="groupAddPublication" runat="server" />
                                </div>
                                <div class="row">
                                    <label>Категория:</label>
                                    <uc:SelectCategoryControl runat="server" SelectDefault="true" ID="sccPublicationCategory" ClientIDMode="Static" CategoryType="Publication" ShowEmpty="true" CssClass="select-text" ValidationGroup="groupAddPublication" />
                                </div>
                            </div>
                            <div class="right-column">
                                <div class="row">
                                    <label>Тип:</label>
                                    <telerik:RadComboBox ID="rcbPublicationType" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="rcbPublicationType_OnSelectedIndexChanged" EnableLoadOnDemand="true" skin="Labitec" Width="234px" EnableEmbeddedSkins="false" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Файл:</label>
                                    <telerik:RadAsyncUpload ID="rauFile" Width="250px" runat="server" MaxFileSize="5242880" 
                                                AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                                 Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div class="left-column">
		                        <div class="row">
			                        <label>Доступ к записи:</label>
                                    <asp:DropDownList ID="ddlAccessRecord" runat="server" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlAccessRecord_OnSelectedIndexChanged"/>
		                        </div>
                                <asp:Panel runat="server" ID="plCompany" CssClass="row" Visible="false">
                                    <label>Компания для доступа:</label>
			                        <uc:Company runat="server" ID="ucCompany" />
                                </asp:Panel>
                            </div>
                            <div class="right-column">
		                        <div class="row">
			                        <label>Доступ для комментирования:</label>
                                    <asp:DropDownList ID="ddlAccessComment" runat="server" CssClass="select-text"/>
		                        </div>
                            </div>
                            <div class="clear"></div>
                            <br/>
                            <div id="update-buttons" class="buttons">
			                    <asp:LinkButton ID="lbtnAddPublication" OnClick="lbtnAddPublication_OnClick" CssClass="btn" ValidationGroup="groupAddPublication" runat="server"></asp:LinkButton>
			                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseAddPublicationRadWindow(false);" />
		                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
</telerik:RadAjaxPanel>
<uc:SearchResultContainer runat="server" />