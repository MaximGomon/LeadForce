<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Files.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.File.Files" %>
<%@ Register TagPrefix="uc" TagName="File" Src="~/UserControls/ModuleEditionAction/File/File.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>


    <telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
    // <![CDATA[
        var $ = $telerik.$;
        function pageLoad() {
            if (!Telerik.Web.UI.RadAsyncUpload.Modules.FileApi.isAvailable()) {
                $(".wrapper").replaceWith(
                    $("<span><strong>Your browser does not support Drag and Drop. Please take a look at the info box for additional information.</strong></span>"));
            }
        }
    // ]]> 
        function fileUploaded(sender, args) {
            $('#lbAddFile').show();
        }
        function fileUploadRemoved(sender, args) {
            $('#lbAddFile').hide();
        }

    </script>
    </telerik:RadCodeBlock>
<table class="smb-files"><tr>
<td width="195px" valign="top">
<div class="aside">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top">
    <div class="add-block clearfix">
        <div class="add-block-upload">
            <label>Выберите файл:</label><telerik:RadAsyncUpload ID="rauFile" runat="server" OnClientFileUploadRemoved="fileUploadRemoved" OnClientFileUploaded="fileUploaded" OnFileUploaded="uploadedFile_OnFileUploaded" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
        </div>  
        <div class="left">
            <telerik:RadButton OnClick="lbAddFile_OnClick" ID="lbAddFile" Text="Добавить" Skin="Windows7" ClientIDMode="Static" style="display:none" runat="server" />
        </div>
    </div>    


<labitec:Grid ID="gridLinks" ShowHeader="false" Toolbar="false" AccessCheck="true" Fields="RuleTypeID,Code" OnItemDataBound="gridLinks_OnItemDataBound" TableName="tbl_Links" runat="server" CssClass="smb-file-grid">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Name" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px">
        <ItemTemplate>
        <asp:HyperLink id="spanName" class="span-name" runat="server" />
        <div id="spanUrl" class="span-url" runat="server">Ссылка на файл: <asp:HyperLink ID="urlLink" runat="server" /></div>            
        <uc:File ID="ucFile" Visible="false" runat="server" />
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn2" DataField="FileSize" Width="100px" Height="65px" runat="server" HorizontalAlign="Right">
        <ItemTemplate>
            <span class="smb-file-size"><asp:Literal ID="lFileSize" runat="server" /></span>
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" runat="server" VerticalAlign="Top">
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
</td>
</tr></table>
                
    



