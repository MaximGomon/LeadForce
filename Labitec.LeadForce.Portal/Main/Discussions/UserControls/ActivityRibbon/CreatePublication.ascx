<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreatePublication.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.CreatePublication" %>
<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/Shared/UserControls/SelectCategory.ascx" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<link href='<%= ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%= ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>

<script type="text/javascript">
    function ShowAddPublicationRadWindow() {
        if (needLogin == true) { NeedLogin(); return; }
        
        $find('<%= radAjaxManager.ClientID %>').ajaxRequest("Init");
    }
    function InitAndShowPublicationPopup(publicationId) {
        if (needLogin == true) { NeedLogin(); return; }
        
        $find('<%= radAjaxManager.ClientID %>').ajaxRequest(publicationId);
    }

    function CloseUpdatePublicationRadWindow(id) {
        $("#" + id + " .publication-container > h3.title a").html($("#txtTitle").val());        
        $("#" + id + " p.description").html($("#txtComment").val());        
        $("#" + id + " ul.operations span.status").html($("#rcbPublicationStatus_Input").val());
        $("#" + id + " ul.operations li.category").html($("#radCategories_Input").val() + "<span>&nbsp;</span>");
        $find('<%= addPublicationRadWindow.ClientID %>').close();
    }

    function CloseAddPublicationRadWindow(empty) {
        $find('<%= addPublicationRadWindow.ClientID %>').close(); InitCommentInputs(); if (empty) { $("#txtPublicationText").val(''); } }
    function KeyUp(e, value) {
        var code = e.keyCode ? e.keyCode : e.charCode;
        if (code == 13 && value != '') {            
            ShowAddPublicationRadWindow();
        } else {
            var evtobj = window.event ? event : e;
            if (!evtobj.altKey && !evtobj.ctrlKey && !evtobj.shiftKey) {
                leadForceSearch.input(value);
            }
        }
    }
    function ClientTabSelectedHandler(sender, eventArgs) {
        $find('<%= txtPublicationText.ClientID %>').set_emptyMessage(eventArgs.get_tab()._imageElement.alt);
    }    
</script>

</telerik:RadCodeBlock>

<telerik:RadAjaxPanel ID="radAjaxPanel" runat="server">
        <telerik:RadTabStrip ID="rtsPublicationTypes" runat="server" Skin="Windows7" OnClientTabSelected="ClientTabSelectedHandler"/>
        <div class="publication-text-container">
            <telerik:RadTextBox runat="server" ID="txtPublicationText" ClientIDMode="Static" autocomplete="off" onkeyup="KeyUp(event, this.value);" />            
            <telerik:RadCodeBlock runat="server">
                <% if (UseOnlySearch) { %>            
                <a href="javascript:;" onclick="ShowAddPublicationRadWindow()" class="send-request">Нет требуемой информации? Отправьте вопрос в поддержку!</a>
                <% } %>
            </telerik:RadCodeBlock>
        </div>
        <telerik:RadWindow runat="server" Title="Поручить" Width="780px" Height="320px" ID="addPublicationRadWindow" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
            <ContentTemplate>              
                <asp:Panel runat="server" ID="plEditPublication" CssClass="rad-window-popup add-publication">                
                    <asp:UpdatePanel ID="upCreatePublication" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <label>Заголовок:</label>
                                <asp:TextBox runat="server" ID="txtTitle" ClientIDMode="Static" CssClass="input-text" Width="600px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" CssClass="required" ValidationGroup="groupAddPublication" Text="*" ErrorMessage='Укажите заголовок'/>
                            </div>
                            <div class="row">
                                <label>Комментарий:</label>
                                <asp:TextBox runat="server" ID="txtComment" ClientIDMode="Static" CssClass="area-text" TextMode="MultiLine" />
                            </div>
                            <div class="left-column">
                                <div class="row">
                                    <label>Статус:</label>
                                    <telerik:RadComboBox ID="rcbPublicationStatus" ClientIDMode="Static" CssClass="select-text" EnableLoadOnDemand="true" skin="Labitec" Width="234px" EnableEmbeddedSkins="false" ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server" Enabled="false"/>
                                    <asp:RequiredFieldValidator ID="rfvrcbPublicationStatus" InitialValue="Выберите значение" CssClass="required" ControlToValidate="rcbPublicationStatus" Text="*" ErrorMessage="Вы не выбрали статус" ValidationGroup="groupAddPublication" runat="server" />
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
                            <br/>
                            <div id="update-buttons" class="buttons clearfix">
			                    <asp:LinkButton ID="lbtnAddPublication" OnClick="lbtnAddPublication_OnClick" CssClass="btn" ValidationGroup="groupAddPublication" runat="server"></asp:LinkButton>
			                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseAddPublicationRadWindow(false);" />
		                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
</telerik:RadAjaxPanel>
<div id="search-result-container">
    <h3>Результаты поиска:</h3>
    <div id="search-result">
    </div>   
    <div id="more-result">
        <a href="javascript:;" onclick="leadForceSearch.more();">Еще <span></span></a>
    </div>
</div>
<script id="search-item-template" type="text/html">
    <div class="search-item" id="search{%= ID %}">
        <h5><a href="{%= PublicationUrl %}">{%= Title %}</a></h5>
        <p>{%= Noun %}</p>
    </div>
</script>