<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MassMails.aspx.cs" Inherits="WebCounter.AdminPanel.MassMails" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#asideFilters input').first().parent().find('label').css('font-weight', 'bold');
        });
        function Checked(element) {
            $('#asideFilters input:radio').parent().find('label').css('font-weight', 'normal');
            $(element).parent().find('label').css('font-weight', 'bold');
        }        
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
<table class="MassMail-Page"><tr>
<td width="195px" valign="top" ID="leftColumn" runat="server">

    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsMassMails" GridControlID="gridMassMails" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersMassMails" GridControlID="gridMassMails" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Статус рассылки">
                    <ContentTemplate>
                        <ul id="asideFilters">
                            <li><asp:RadioButton ID="Plan" runat="server" GroupName="filters" Text="Запланированы" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="Complete" runat="server" GroupName="filters" Text="Проведены" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="Cancel" runat="server" GroupName="filters" Text="Отменены" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="All" runat="server" GroupName="filters" Text="Все рассылки" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                        </ul>
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>        
        <uc:LeftColumn runat="server" />
    </div>
</td>
<td valign="top" width="100%">
    <labitec:Search ID="searchMassMails" GridControlID="gridMassMails" OnDemand="True" SearchBy="Name" runat="server" />
    <div class="add-block clearfix">
            <div>
                <telerik:RadButton ID="lbAddFile" OnClick="lbAdd_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="Static" runat="server" />
            </div>  
    </div>    

    <labitec:Grid ID="gridMassMails" TableName="tbl_MassMail" Fields="MassMailStatusID,MailDate" SearchControlID="searchMassMails" AccessCheck="true" OnItemDataBound="gridMassMails_OnItemDataBound" TagsControlID="tagsMassMails" FiltersControlID="filtersMassMails" ClassName="WebCounter.AdminPanel.MassMails" ShowPadding="false" ShowSelectCheckboxes="true" Toolbar="false" ShowHeader="false"  runat="server">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Name" VerticalAlign="Top" runat="server">
                <ItemTemplate>
                <asp:HyperLink id="spanName" class="span-name product" runat="server" />
                <div id="Div1" class="span-url" runat="server" style="float:left">Статус: <asp:Literal ID="litStatus" runat="server" /></div>
                <div id="Div2" class="span-url" runat="server" style="float:right">Дата рассылки: <asp:Literal ID="lMailDate" runat="server" Text="не установлена"/></div>
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
</td></tr></table>
</asp:Content>