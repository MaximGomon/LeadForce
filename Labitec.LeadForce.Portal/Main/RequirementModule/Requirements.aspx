<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Requirements.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.RequirementModule.Requirements" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/Shared/UserControls/ContactComboBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />        
</asp:Content>
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
                <telerik:RadPanelItem Expanded="true" Text="Фильтр">
                    <ContentTemplate>
                        <ul id="requestFilters">                            
                            <li><asp:RadioButton ID="Open" runat="server" GroupName="filters" Text="Открытые требования" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                            <li><asp:RadioButton ID="All" runat="server" GroupName="filters" Text="Все требования" onclick="Checked(this)" AutoPostBack="true" OnCheckedChanged="filters_OnCheckedChanged" /></li>
                        </ul>
                        <div class="checkbox-filter">
                            <span>Ответственный</span>
                            <uc:ContactComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="ucResponsible_OnSelectedIndexChanged" ID="ucResponsible" Width="168px" SelectResponsibles="true" />
                            <asp:CheckBox runat="server" ID="chxByResponsible" AutoPostBack="true" Text="По проектам ответственного" OnCheckedChanged="chxByResponsible_OnCheckedChanged" Checked="false" />
                        </div>
                    </ContentTemplate>
                </telerik:RadPanelItem>                
            </Items>
        </telerik:RadPanelBar>
    </div>
    <div class="grid-container-margin">
        <labitec:Grid ID="gridRequirements" AccessCheck="true" TableName="tbl_Requirement" ClassName="Labitec.LeadForce.Portal.Requirements" Fields="tbl_Contact.ID" Export="true" runat="server">
        <Columns>            
            <labitec:GridColumn DataField="Number" HeaderText="Номер" runat="server"/>
            <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>            
            <labitec:GridColumn DataField="tbl_RequirementStatus.Title" HeaderText="Состояние требования" runat="server"/>
            <labitec:GridColumn DataField="ShortDescription" HeaderText="Суть требования" runat="server"/>                                    
            <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server"/>            
        </Columns>    
        <Joins>        
            <labitec:GridJoin JoinTableName="tbl_RequirementStatus" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />
            <labitec:GridJoin JoinTableName="tbl_RequirementTransition" JoinTableKey="InitialRequirementStatusID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />            
            <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ContactID" runat="server" />            
            <labitec:GridJoin JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestSourceTypeID" runat="server" />
        </Joins>
    </labitec:Grid>
    </div>
</asp:Content>
