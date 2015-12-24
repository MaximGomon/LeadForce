<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Imports.aspx.cs" Inherits="WebCounter.AdminPanel.Imports" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<table><tr>
<td width="195px" valign="top" ID="leftColumn" runat="server">
<div class="aside" ID="asideDiv" runat="server">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top" width="100%">
    <labitec:Search ID="searchImport" GridControlID="gridImports" OnDemand="True" SearchBy="Name" runat="server" />
    <div class="add-block clearfix">
            <div>
                <telerik:RadButton ID="lbAddFile" OnClick="lbAdd_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="Static" runat="server" />
            </div>  
    </div>    

    <labitec:Grid ID="gridImports" SearchControlID="searchImport" Fields="ImportTable, Type" TableName="tbl_Import" OnItemDataBound="gridImports_OnItemDataBound" ClassName="WebCounter.AdminPanel.Imports" Toolbar="false" ShowHeader="false" runat="server" CssClass="smb-product-grid">
        <Columns>
            <labitec:GridColumn DataField="Name" VerticalAlign="Top" runat="server">
                <ItemTemplate>
                <asp:HyperLink id="spanName" class="span-name product" runat="server" />
                <div class="span-url" runat="server" style="float:left">Тип операции: <asp:Literal ID="lType" runat="server" Text="Excel"/></div>
                <div class="span-url" runat="server" style="float:right">Что импортируем: <asp:Literal ID="lImportTable" runat="server" Text="Excel"/></div>
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" runat="server" VerticalAlign="Top">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" OnCommand="lbEdit_OnCommand" CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:LinkButton><br/>
                    <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
                </ItemTemplate>
            </labitec:GridColumn>
        </Columns>
    </labitec:Grid>
</td></tr></table>
</asp:Content>