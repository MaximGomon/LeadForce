<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="WebCounter.AdminPanel.Tasks" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="TaskFilter" Src="~/UserControls/Task/TaskFilter.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function OnClientAppointmentEditing(sender, args) {            
            args.set_cancel(true);
        } 
        function OnClientAppointmentDoubleClick(sender, eventArgs) {
            var url = '<%= UrlsData.AP_TaskEdit(Guid.Empty).Replace(Guid.Empty.ToString(), "") %>';
            var apt = eventArgs.get_appointment();
            window.location = url + apt.get_attributes().getAttribute("TaskID");
        }        
    </script>
</telerik:RadScriptBlock>
    <div class="aside">
        <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
            <Items>
                <telerik:RadPanelItem Expanded="true" Text="Теги">
                    <ContentTemplate>
                        <labitec:Tags ID="tagsTasks" GridControlID="gridTasks" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                    <ContentTemplate>
                        <labitec:Filters ID="filtersTasks" GridControlID="gridTasks" runat="server" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
                <telerik:RadPanelItem Expanded="true" Text="Фильтр по задачам">
                    <ContentTemplate>
                        <uc:TaskFilter runat="server" ID="ucTaskFilter" />
                    </ContentTemplate>
                </telerik:RadPanelItem>
            </Items>
        </telerik:RadPanelBar>
        <uc:LeftColumn runat="server" />
    </div>
        
    <labitec:Grid ID="gridTasks" TableName="tbl_Task" ClassName="WebCounter.AdminPanel.Task" Fields="tbl_Contact.ID, tbl_TaskType.TaskTypeCategoryID" AlternativeControlID="rScheduler" AlternativeControlType="Scheduler" TagsControlID="tagsTasks" FiltersControlID="filtersTasks" Export="true" runat="server" OnItemDataBound="gridTasks_OnItemDataBound">
        <Columns>
            <labitec:GridColumn DataField="Title" HeaderText="Тема" runat="server"/>
            <labitec:GridColumn DataField="tbl_TaskType.Title" HeaderText="Тип" runat="server"/>
            <labitec:GridColumn DataField="StartDate" HeaderText="Дата начала" runat="server"/>
            <labitec:GridColumn DataField="EndDate" HeaderText="Дата окончания" runat="server"/>
            <labitec:GridColumn DataField="tbl_Contact.UserFullName" HeaderText="Ответственный" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlUserFullName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="TaskStatusID" HeaderText="Состояние" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlTaskStatus" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn DataField="tbl_TaskResult.Title" HeaderText="Результат" runat="server"/>
        </Columns>    
        <Joins>
            <labitec:GridJoin JoinTableName="tbl_TaskResult" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskResultID" runat="server" />   
            <labitec:GridJoin JoinTableName="tbl_TaskType" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskTypeID" runat="server" />                        
            <labitec:GridJoin JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Task" TableKey="ResponsibleID" runat="server" />
        </Joins>
    </labitec:Grid>
    <telerik:RadScheduler ID="rScheduler" 
                          AllowDelete="false"                                                     
                          AllowInsert="false" 
                          OnClientAppointmentEditing="OnClientAppointmentEditing" 
                          OnClientAppointmentDoubleClick="OnClientAppointmentDoubleClick" 
                          OnAppointmentDataBound="rScheduler_OnAppointmentDataBound"                                                                               
                          OnNavigationComplete="rScheduler_OnNavigationComplete"
                          OnAppointmentUpdate="rScheduler_OnAppointmentUpdate"
                          Height="600px"
                          HoursPanelTimeFormat="HH:mm"                          
                          DisplayDeleteConfirmation="false"
                          Skin="Windows7" runat="server">    
    </telerik:RadScheduler>
</asp:Content>