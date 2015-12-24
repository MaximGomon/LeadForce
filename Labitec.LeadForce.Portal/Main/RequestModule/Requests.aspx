<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.RequestModule.Requests" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {            
            $('#requestFilters input').first().parent().find('label').css('font-weight', 'bold');
        });
        function Checked(element) {
            $('#requestFilters input:radio').parent().find('label').css('font-weight', 'normal');            
            $(element).parent().find('label').css('font-weight', 'bold');
        }        
    </script>
        <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Фильтр по статусам">
                    <ContentTemplate>
                        <ul id="requestFilters">
                            <li><asp:RadioButton ID="Open" runat="server" GroupName="filters" Text="Открытые запросы" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="New" runat="server" GroupName="filters" Text="Новые запросы" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="All" runat="server" GroupName="filters" Text="Все запросы" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                        </ul>
                    </ContentTemplate>
                </telerik:RadPanelItem>                
            </Items>
        </telerik:RadPanelBar>
    </div>
    <div class="grid-container-margin">
        <labitec:Grid ID="gridRequests" AccessCheck="true" TableName="tbl_Request" ClassName="Labitec.LeadForce.Portal.Requests" Fields="tbl_Contact.ID" Export="true" runat="server" OnItemDataBound="gridRequests_OnItemDataBound">
            <Columns>
                <labitec:GridColumn DataField="Number" HeaderText="Номер" runat="server"/>
                <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>
                <labitec:GridColumn DataField="ReactionDatePlanned" HeaderText="Дата реакции, план" runat="server"/>                            
                <labitec:GridColumn DataField="RequeststatusID" HeaderText="Состояние запроса" runat="server">
                    <ItemTemplate>
                        <asp:Literal ID="lrlRequestStatus" runat="server"/>
                    </ItemTemplate>
                </labitec:GridColumn>                                
                <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server"/>                            
            </Columns>    
            <Joins>        
                <labitec:GridJoin JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Request" TableKey="RequestSourceTypeID" runat="server" />                
                <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Request" TableKey="ContactID" runat="server" />
            </Joins>
        </labitec:Grid>
    </div>
</asp:Content>
