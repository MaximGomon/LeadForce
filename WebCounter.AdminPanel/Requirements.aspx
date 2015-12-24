<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirements.aspx.cs" Inherits="WebCounter.AdminPanel.Requirements" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function ShowSendReportRadWindow() { $find('<%= rwSendReport.ClientID %>').show(); }
            function CloseSendReportRadWindow() { $find('<%= rwSendReport.ClientID %>').close(); }
        </script>
    </telerik:RadScriptBlock>
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsRequirements" GridControlID="gridRequirements" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersRequirements" GridControlID="gridRequirements" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Быстрые фильтры">
                    <ContentTemplate>
                        <div class="checkbox-filter">
                            <asp:CheckBox runat="server" ID="chxCurrentRequirements" AutoPostBack="true" Text="Текущие требования" OnCheckedChanged="chxFilter_OnCheckedChanged" Checked="true" />
                            Ответственный
                            <uc:ContactComboBox runat="server" AutoPostBack="true" OnSelectedIndexChanged="ucResponsible_OnSelectedIndexChanged" ID="ucResponsible" Width="168px" SelectResponsibles="true" />
                            <asp:CheckBox runat="server" ID="chxByResponsible" AutoPostBack="true" Text="По проектам ответственного" OnCheckedChanged="chxFilter_OnCheckedChanged" Checked="false" />
                            <asp:CheckBox runat="server" ID="chxToPay" AutoPostBack="true" Text="К оплате" OnCheckedChanged="chxFilter_OnCheckedChanged" Checked="false" />
                        </div>
                     </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Отчеты">
                    <ContentTemplate>
                        <ul class="reports">
                            <li><a href="javascript:;" onclick="ShowSendReportRadWindow()">Отчет по статусу работы</a></li>                            
                        </ul>
                     </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
    <labitec:Grid ID="gridRequirements" AccessCheck="true" TableName="tbl_Requirement" AlternativeControlID="rtlRequirements" AlternativeControlType="TreeList" ClassName="WebCounter.AdminPanel.Requirements" Fields="tbl_Contact.ID, tbl_Company.ID, tbl_Requirement.ID, tbl_Requirement.ParentID" TagsControlID="tagsRequirements" FiltersControlID="filtersRequirements" Export="true" runat="server" OnItemDataBound="gridRequirements_OnItemDataBound">
        <Columns>            
            <labitec:GridColumn DataField="Number" HeaderText="Номер" runat="server"/>
            <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>            
            <labitec:GridColumn DataField="tbl_RequirementStatus.Title" HeaderText="Состояние требования" runat="server"/>
            <labitec:GridColumn DataField="ShortDescription" HeaderText="Суть требования" runat="server"/>
            <labitec:GridColumn DataField="tbl_RequestSourceType.Title" HeaderText="Тип запроса" runat="server" />
            <labitec:GridColumn DataField="tbl_Request.Number" HeaderText="Номер запроса" runat="server"/>
            <labitec:GridColumn DataField="tbl_Company.Name" HeaderText="Компания" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlCompanyName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlUserFullName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_ServiceLevel.Title" HeaderText="Уровень обслуживания" runat="server"/>
        </Columns>    
        <Joins>        
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RequirementStatus" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />
            <labitec:GridJoin ID="GridJoin7" JoinTableName="tbl_RequirementTransition" JoinTableKey="InitialRequirementStatusID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />
            <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_ServiceLevel" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ServiceLevelID" runat="server" />
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="CompanyID" runat="server" />
            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ContactID" runat="server" />
            <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_Request" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestID" runat="server" />
            <labitec:GridJoin ID="GridJoin6" JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestSourceTypeID" runat="server" />
        </Joins>
    </labitec:Grid>
    <telerik:RadTreeList runat="server" ID="rtlRequirements" AllowPaging="true" PageSize="20" AllowMultiColumnSorting="true" AllowSorting="true" CssClass="requirements-treelist" AutoGenerateColumns="false" ParentDataKeyNames="tbl_Requirement_ParentID" DataKeyNames="tbl_Requirement_ID" Skin="Windows7" OnItemDrop="rtlRequirements_OnItemDrop">
        <Columns>
            <telerik:TreeListBoundColumn DataField="tbl_Requirement_Number" HeaderText="Номер" UniqueName="tbl_Requirement_Number" />
            <telerik:TreeListDateTimeColumn DataField="tbl_Requirement_CreatedAt" HeaderText="Дата регистрации" UniqueName="tbl_Requirement_CreatedAt" />
            <telerik:TreeListBoundColumn DataField="tbl_RequirementStatus_Title" HeaderText="Состояние требования" UniqueName="tbl_RequirementStatus_Title" />
            <telerik:TreeListBoundColumn DataField="tbl_Requirement_ShortDescription" HeaderText="Суть требования" UniqueName="tbl_Requirement_ShortDescription" />
            <telerik:TreeListBoundColumn DataField="tbl_RequestSourceType_Title" HeaderText="Тип запроса" UniqueName="tbl_RequestSourceType_Title" />
            <telerik:TreeListBoundColumn DataField="tbl_Request_Number" HeaderText="Номер запроса" UniqueName="tbl_Request_Number" />
            <telerik:TreeListBoundColumn DataField="tbl_Company_Name" HeaderText="Компания" UniqueName="tbl_Company_Name" />
            <telerik:TreeListBoundColumn DataField="tbl_Contact_UserFullName" HeaderText="Контакт" UniqueName="tbl_Contact_UserFullName" />
            <telerik:TreeListBoundColumn DataField="tbl_ServiceLevel_Title" HeaderText="Уровень обслуживания" UniqueName="tbl_ServiceLevel_Title" />
            <telerik:TreeListTemplateColumn HeaderText="Действия">
                <HeaderStyle HorizontalAlign="Center" Width="80px"/>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                <ItemTemplate>
                    <asp:HyperLink runat="server" ImageUrl="~/App_Themes/Default/images/icoView.png" NavigateUrl='<%# UrlsData.AP_RequirementEdit((Guid)Eval("tbl_Requirement_ID")) %>' ToolTip="Карточка требования" />                    
                </ItemTemplate>
            </telerik:TreeListTemplateColumn>
        </Columns>
        <ClientSettings AllowItemsDragDrop="true">
            <Selecting AllowItemSelection="True" />            
        </ClientSettings>
    </telerik:RadTreeList>
    <telerik:RadWindow runat="server" Title="Отчет по статусу работы" Width="390px" Height="160px" ID="rwSendReport" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel runat="server">
            <div class="radwindow-popup-inner">
                <div class="row">
                    <telerik:RadDatePicker runat="server" CssClass="date-picker-w145" Width="105px" ShowPopupOnFocus="true" ID="rdpStartDate" Skin="Windows7"/>-&nbsp;<telerik:RadDatePicker runat="server" CssClass="date-picker-w145" Width="105px" ShowPopupOnFocus="true" ID="rdpEndDate" Skin="Windows7"/>
                </div>
                <div class="row">
                    <label style="width: 80px">Компания:</label>
                    <uc:DictionaryComboBox runat="server" ID="dcbCompany" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" Width="255px"  />
                </div>                
                <div class="buttons">
                    <asp:LinkButton ID="lbtnGenerateReport" CssClass="btn" OnClick="lbtnGenerateReport_OnClick" ValidationGroup="groupRequirementReport" CausesValidation="true" runat="server"><em>&nbsp;</em><span>Сгенерировать отчет</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseSendReportRadWindow();" />
                </div>
            </div>
        </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>
</asp:Content>
