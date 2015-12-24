<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DomainSettings.aspx.cs" Inherits="WebCounter.AdminPanel.DomainSettings" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/UserControls/Shared/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="CheckSite" Src="~/UserControls/SiteSettings/CheckSite.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function ShowCheckDomainRadWindow() { $find('<%= rwCheckDomain.ClientID %>').show(); }
            function CloseCheckDomainRadWindow() { $find('<%= rwCheckDomain.ClientID %>').close(); }
        </script>
    </telerik:RadScriptBlock>    
    <table class="smb-files domain">
        <tr>
            <td width="195px" valign="top" ID="leftColumn" runat="server">
                <div class="aside" ID="asideDiv" runat="server">    
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">
                <labitec:Search ID="searchSiteDomains" GridControlID="gridSiteDomains" OnDemand="True" SearchBy="Domain,Aliases" runat="server" />
                <div class="add-block domain clearfix">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                        CssClass="validation-summary"
						                        runat="server"
						                        EnableClientScript="true"
						                        HeaderText="Заполните все поля корректно:"
                                                ValidationGroup="valGroupAdd"
						                        />
                            </td>
                        </tr>
                        <tr>                    
                            <td width="170px"><label>Ссылка:</label></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDomain" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDomain" ErrorMessage="Вы не ввели ссылку" ValidationGroup="valGroupAdd" runat="server">*</asp:RequiredFieldValidator>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Псевдонимы:</label></td>
                            <td style="padding-top: 5px"><asp:TextBox runat="server" ID="txtAliases" Width="100%" TextMode="MultiLine" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="padding-top: 5px">
	                            <div class="buttons clearfix">
	                                <telerik:RadButton OnClick="lbtnSave_OnClick" ID="lbtnSave" Text="Добавить" Skin="Windows7" ValidationGroup="valGroupAdd" runat="server" />
                                    <telerik:RadButton ID="lbCancel" Text="Отмена" Skin="Windows7" runat="server" CssClass="left"/>
	                            </div>
                            </td>
                        </tr>                        
                    </table>
                </div>                  
            <div class="smb-file-grid">
                <labitec:Grid ID="gridSiteDomains" ShowHeader="false" Toolbar="false" SearchControlID="searchSiteDomains" Fields="ID,StatusID,Aliases,Note" OnItemDataBound="gridSiteDomains_OnItemDataBound" ClassName="WebCounter.AdminPanel.SiteDomain" TableName="tbl_SiteDomain" runat="server">
                    <Columns>
                        <labitec:GridColumn ID="GridColumn1" DataField="Domain" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
                        <ItemTemplate>
                        <div class="grid-left-column">
                            <span class="span-name"><asp:Literal runat="server" ID="lrlSiteDomain" /></span>                            
                            <div class="span-url"><asp:Literal ID="lrlAliases" runat="server" /></div> 
                            <div class="span-url"><asp:Literal ID="lrlNote" runat="server" /></div> 
                        </div>          
                        <div class="grid-right-column">
                            <div id="spanUrl" class="span-url" runat="server">Статус: <asp:Literal runat="server" ID="lrlSiteDomainStatus" /></div>  
                            <span class="smb-file-size"><asp:Literal ID="lFileSize" runat="server" /></span>
                        </div>      
                        <div class="clear"></div>
                        <div class="grid-edit">                            
                            <asp:Panel runat="server" ID="plEdit" Visible="False">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                                    CssClass="validation-summary"
						                                    runat="server"
						                                    EnableClientScript="true"
						                                    HeaderText="Заполните все поля корректно:"
                                                            ValidationGroup="valGroupUpdate"
						                                    />
                                        </td>
                                    </tr>
                                    <tr>                    
                                        <td width="170px"><label>Ссылка:</label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDomain" Width="400px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDomain" ErrorMessage="Вы не ввели ссылку" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><label>Псевдонимы:</label></td>
                                        <td style="padding-top: 5px"><asp:TextBox runat="server" ID="txtAliases" Width="100%" TextMode="MultiLine" Height="30px" /></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td style="padding-top: 5px">
	                                        <div class="buttons clearfix">
	                                            <telerik:RadButton OnClick="lbtnUpdate_OnClick" ClientIDMode="AutoID" ID="lbtnUpdate" Text="Сохранить" Skin="Windows7" ValidationGroup="valGroupUpdate" runat="server" />
                                                <telerik:RadButton ID="lbCancel" Text="Отмена" Skin="Windows7" runat="server"/>
	                                        </div>
                                        </td>
                                    </tr>                                    
                                </table>
                            </asp:Panel>                            
                        </div>
                        </ItemTemplate>
                        </labitec:GridColumn>
                        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" VerticalAlign="Top" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbCheck" OnCommand="lbCheck_OnCommand" CssClass="smb-action" runat="server"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Default/images/icoLike01.png" AlternateText="Проверить" ToolTip="Проверить" runat="server"/><span style="padding-left: 3px">Проверить</span></asp:LinkButton><br/>
                                <asp:LinkButton ID="lbEdit" OnCommand="lbEdit_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:LinkButton><br/>
                                <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
                            </ItemTemplate>
                        </labitec:GridColumn>
                    </Columns>                    
                </labitec:Grid>
            </div>
        </td>
     </tr>
</table>

 <telerik:RadWindow runat="server" Title="Проверка домена" Width="900px" Height="500px" ID="rwCheckDomain" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>        
            <div class="radwindow-popup-inner">
                <uc:CheckSite runat="server" ID="ucCheckSite" />                                
                <div class="buttons clearfix">
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Закрыть" onclick="CloseCheckDomainRadWindow();" />
                </div>
            </div>        
    </ContentTemplate>
</telerik:RadWindow>
</asp:Content>
