<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest="false" AutoEventWireup="True" CodeBehind="WebSite.aspx.cs" Inherits="WebCounter.AdminPanel.WebSite" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ExternalResources" Src="~/UserControls/Shared/ExternalResources.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock runat="server">
    <script type="text/javascript">        
        function EditPage(id, title) {            
            var oWnd = $find("<%= rwWebSitePage.ClientID %>");
            oWnd.show();
            oWnd.maximize();
            oWnd.set_title(title);
            oWnd.setUrl('<%= ResolveUrl("WebSitePage.aspx") %>' + '?id=' + id + '&websiteid=<%= _webSiteId %>');
        }

        function AddPage(button, args) {
            EditPage('<%= Guid.Empty %>', 'Новая страница');
            args.set_cancel(true);
        }
        function refreshGrid() {
            $find("<%= RadAjaxManager.ClientID %>").ajaxRequest("RebindWebPages");    
        }
    </script>
    </telerik:RadScriptBlock>
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						        CssClass="validation-summary"
						        runat="server"
						        EnableClientScript="true"
						        HeaderText="Заполните все поля корректно:"
						        ValidationGroup="groupWebSite" />
            <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			    <Tabs>
				    <telerik:RadTab Text="Основные данные" />                    
				    <telerik:RadTab Text="Структура сайта" />
			        <telerik:RadTab Text="Ресурсы" />
			    </Tabs>
		    </telerik:RadTabStrip>
		    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			    <telerik:RadPageView ID="RadPageView1" runat="server">
			        <div class="row">
                        <label>Название:</label>
                        <asp:TextBox runat="server" ID="txtTitle" CssClass="input-text" Width="500px" />
		                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupWebSite" runat="server" />
                    </div>
                    <div class="row">
                        <label>Описание:</label>
                        <asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" Width="800px" Height="30px" TextMode="MultiLine" />
                    </div>
                    <div class="row">
                        <label>Домен:</label>
                        <uc:DictionaryComboBox runat="server" ID="dcbSiteDomain" DictionaryName="tbl_SiteDomain" CssClass="select-text" DataTextField="Domain" />
                    </div>
                    <div class="row">
                        <label>Иконка:</label>
                        <telerik:RadAsyncUpload ID="rauFavIcon" runat="server" MaxFileSize="5242880" AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                        Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>                                                
                    </div>
                    <div class="row">
                        <label></label>
                        <asp:Image runat="server" ID="imgFavIcon" />
                    </div>
                    <asp:Panel runat="server" ID="plTemporaryAddress" CssClass="row" Visible="false">
                        <label>Временный адрес:</label>
                        <asp:Literal runat="server" ID="lrlTemporaryAddress" />
                    </asp:Panel>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server">
                    <labitec:Search ID="searchWebSitePage" GridControlID="gridWebSitePage" OnDemand="True" SearchBy="Title" runat="server" />
                    <div>
                        <telerik:RadButton ID="rbAdd" runat="server" AutoPostBack="False" Text="" OnClientClicking="AddPage" Skin="Windows7">
                            <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="12" PrimaryIconTop="4"></Icon>
                        </telerik:RadButton>
                    </div>
                    <br/>                                                                           
                <div class="clear"></div>    
                <div class="smb-file-grid">
                    <labitec:Grid ID="gridWebSitePage" ShowHeader="false" Toolbar="false" SearchControlID="searchWebSitePage" Fields="ID, WebSiteElementStatusID" OnItemDataBound="gridWebSitePage_OnItemDataBound" ClassName="WebCounter.AdminPanel.WebSitePage" TableName="tbl_WebSitePage" runat="server">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="Title" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
                            <ItemTemplate>                                
                                <span class="span-name"><asp:Literal runat="server" ID="lrlTitle" /></span>                                                                
                                </ItemTemplate>
                            </labitec:GridColumn>
                            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="120px" Height="65px" HorizontalAlign="Left" VerticalAlign="Middle" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbtnStatus" OnClick="lbtnStatus_OnClick"/>
                                    <asp:HyperLink ID="hlEdit" Text="Изменить" CssClass="action-edit" runat="server"/>
                                    <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>                        
                    </labitec:Grid>
                </div>
            <telerik:RadWindow ID="rwWebSitePage" runat="server" Modal="True" IconUrl="/App_Themes/Default/images/icoDomain2.png" ShowContentDuringLoad="false" Skin="Windows7" Width="400px"
                Height="400px" Title="Telerik RadWindow" Behaviors="Close" VisibleStatusbar="False">
            </telerik:RadWindow>                                        
                </telerik:RadPageView>
                <telerik:RadPageView runat="server">
                    <uc:ExternalResources runat="server" ID="ucExternalResources" />
                </telerik:RadPageView>
            </telerik:RadMultiPage>
            <br/>
		    <div class="buttons">
			    <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupWebSite" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                <asp:LinkButton ID="lbtnSaveAndContinue" OnClick="lbtnSaveAndContinue_OnClick" CssClass="btn" ValidationGroup="groupWebSite" runat="server"><em>&nbsp;</em><span>Сохранить и продолжить</span></asp:LinkButton>
			    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		    </div>
        </td>
    </tr>
</table>    
</asp:Content>
