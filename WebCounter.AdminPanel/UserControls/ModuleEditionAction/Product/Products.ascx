<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Products.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.Product.Products" %>
<%@ Register TagPrefix="uc" TagName="Product" Src="~/UserControls/ModuleEditionAction/Product/Product.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProductCategories" Src="~/UserControls/ProductCategories.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%# ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
<table class="smb-files"><tr>
<td width="195px" valign="top">
<div class="aside">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top">
<labitec:Search ID="searchProducts" GridControlID="gridProducts" OnDemand="True" SearchBy="Title,Description" runat="server" />
<div class="add-block product clearfix">
        <div>
        <asp:ValidationSummary ID="ValidationAddSummary" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="valGroupAdd" />
            <table width="100%">
                <tr>
                    <td>
                        <div class="row">
                            <label>Название:</label>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="input-text" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                                CssClass="input-text" ValidationGroup="valGroupAdd" Text="*" ErrorMessage='Укажите название' />
                        </div>
                    </td>
                    <td align="left">
                        <div class="row">
                            <label>Артикул:</label>
                            <asp:TextBox runat="server" ID="txtSKU" CssClass="input-text" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSKU"
                                CssClass="input-text" ValidationGroup="valGroupAdd" Text="*" ErrorMessage='Укажите артикул' />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="2">
                        <div class="row">
                            <label>Цена:</label>
                            <asp:TextBox runat="server" ID="txtPrice" CssClass="input-text"/>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtPrice"
                                Operator="DataTypeCheck" Type="Double" CssClass="input-text" ValidationGroup="valGroupAdd"
                                ErrorMessage='Неверный формат цены' Text="*" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrice"
                                CssClass="input-text" ValidationGroup="valGroupAdd" Text="*" ErrorMessage='Укажите цену' />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="2">
                        <div class="row">
                            <label> Описание:</label>
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" Width="71%"
                                Height="45px" TextMode="MultiLine" />
                        </div>
                    </td>
                </tr>
            </table>
            <telerik:RadButton ID="lbAddFile" OnClick="lbAddFile_OnClick" ValidationGroup="valGroupAdd" Text="Добавить" Skin="Windows7" ClientIDMode="Static" runat="server" />
        </div>  
    </div>    
<labitec:Grid ID="gridProducts" ShowHeader="false" Toolbar="false" Fields="Description" AccessCheck="true" OnItemDataBound="gridProducts_OnItemDataBound" TableName="tbl_Product" SearchControlID="searchProducts" runat="server" CssClass="smb-product-grid">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Title" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" runat="server" Height="65px" VerticalAlign="Top">
        <ItemTemplate>
            <asp:HyperLink id="spanName" class="span-name product" runat="server" />
            <div id="spanUrl" class="span-url" runat="server"><asp:Literal ID="lDescription" runat="server"/></div>            
            <uc:Product ID="ucProduct" Visible="false" runat="server" />
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn2" DataField="SKU" Width="100px" Height="65px" runat="server" HorizontalAlign="Right" VerticalAlign="Top">
        <ItemTemplate>
            <div class="top-line" runat="server">Артикул: <asp:Literal ID="lSKU" runat="server"/></div>            
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn4" DataField="Price" Width="100px" Height="65px" runat="server" HorizontalAlign="Right" VerticalAlign="Top">
        <ItemTemplate>
            <div class="top-line blue" runat="server"><asp:Literal ID="lPrice" runat="server"/></div>            
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" HorizontalAlign="Left" runat="server" VerticalAlign="Top">
            <ItemTemplate>
                <asp:LinkButton ID="lbEdit" OnCommand="lbEdit_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:LinkButton><br/>
                <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
            </ItemTemplate>
        </labitec:GridColumn>
    </Columns>
</labitec:Grid>
</td>
</tr></table>
                
    



