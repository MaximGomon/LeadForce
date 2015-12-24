<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteActivityRuleFiles.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SiteActivityRuleFiles" %>
<%@ Register TagPrefix="uc" TagName="File" Src="~/UserControls/SiteActivityRuleFile.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
    // <![CDATA[
        //var $ = $telerik.$;
        function pageLoad() {
            if (!Telerik.Web.UI.RadAsyncUpload.Modules.FileApi.isAvailable()) {
                $telerik.$(".wrapper").replaceWith(
                    $telerik.$("<span><strong>Your browser does not support Drag and Drop. Please take a look at the info box for additional information.</strong></span>"));
            }
        }
    // ]]> 
        

    </script>
    </telerik:RadCodeBlock>
<table class="smb-files"><tr>
<td width="195px" valign="top" ID="leftColumn" runat="server">
<div class="aside" ID="asideDiv" runat="server">
    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsFiles" GridControlID="gridLinks" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
    </telerik:RadPanelBar>
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top" width="100%">
    <labitec:Search ID="searchLinks" GridControlID="gridLinks" OnDemand="True" SearchBy="Name,Description" runat="server" />
    <div class="add-block big clearfix">
        <table width="100%">
            <tr>
                <td valign="top" width="170px"><label>Выберите файл:</label></td>
                <td><telerik:RadAsyncUpload ID="rauFile" runat="server"  OnFileUploaded="uploadedFile_OnFileUploaded" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/></td>
            </tr>
            <tr>
                <td valign="top"><label>Описание файла:</label></td>
                <td><asp:TextBox ID="txtDescription" Width="100%" TextMode="MultiLine" runat="server" Rows="1"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="padding-top:5px"><telerik:RadButton OnClick="lbAddFile_OnClick" ID="lbAddFile" Text="Добавить" Skin="Windows7" ClientIDMode="Static" runat="server" /></td>
            </tr>
        </table>
    </div>    
<div class="smb-file-grid">
<labitec:Grid ID="gridLinks" PageSize="5" ShowHeader="false" Toolbar="false" AccessCheck="true" Fields="Name,RuleTypeID,Code,FileSize,Description" TagsControlID="tagsFiles" SearchControlID="searchLinks" OnItemDataBound="gridLinks_OnItemDataBound" ClassName="WebCounter.AdminPanel.Files" TableName="tbl_Links" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Name" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
        <ItemTemplate>
        <div class="grid-left-column">
            <asp:HyperLink id="spanName" class="span-name" runat="server" />
            <div id="Div1" class="span-url" runat="server"><asp:Literal ID="lDescription" runat="server" /></div> 
        </div>          
        <div class="grid-right-column">
            <div id="spanUrl" class="span-url" runat="server">Ссылка на файл: <asp:HyperLink ID="urlLink" runat="server" /></div>  
            <span class="smb-file-size"><asp:Literal ID="lFileSize" runat="server" /></span>
        </div>      
        <div class="clear"></div>
        <div class="grid-edit">
        <uc:File ID="ucFile" Visible="false" runat="server" />
        </div>
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" VerticalAlign="Top" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="lbEdit" OnCommand="lbEdit_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:LinkButton><br/>
                <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
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

                
    




                
    



