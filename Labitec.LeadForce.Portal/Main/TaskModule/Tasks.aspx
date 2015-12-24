<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.Tasks" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="TaskFilter" Src="~/Main/TaskModule/UserControls/TaskFilter.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function OnClientAppointmentEditing(sender, args) {
            args.set_cancel(true);
        }
        function OnClientAppointmentDoubleClick(sender, eventArgs) {
            var url = '<%= UrlsData.LFP_TaskEdit(Guid.Empty, PortalSettingsId).Replace(Guid.Empty.ToString(), "") %>';
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
    </div>
    <labitec:Grid ID="gridTasks" TableName="tbl_Task" ClassName="Labitec.LeadForce.Portal.Task" Fields="tbl_Contact.ID, tbl_TaskType.TaskTypeCategoryID" AlternativeControlID="rScheduler" AlternativeControlType="Scheduler" TagsControlID="tagsTasks" FiltersControlID="filtersTasks" Export="true" runat="server" OnItemDataBound="gridTasks_OnItemDataBound">
        <Columns>
            <labitec:GridColumn ID="GridColumn1" DataField="Title" HeaderText="Тема" runat="server"/>
            <labitec:GridColumn ID="GridColumn2" DataField="tbl_TaskType.Title" HeaderText="Тип" runat="server"/>
            <labitec:GridColumn ID="GridColumn3" DataField="StartDate" HeaderText="Дата начала" runat="server"/>
            <labitec:GridColumn ID="GridColumn4" DataField="EndDate" HeaderText="Дата окончания" runat="server"/>
            <labitec:GridColumn ID="GridColumn5" DataField="tbl_Contact.UserFullName" HeaderText="Ответственный" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlUserFullName" runat="server" />
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn6" DataField="TaskStatusID" HeaderText="Состояние" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlTaskStatus" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>            
            <labitec:GridColumn ID="GridColumn8" DataField="IsUrgentTask" HeaderText="Статус" runat="server">
                <ItemTemplate>
                    <asp:Literal ID="lrlTaskMemberStatus" runat="server"/>
                </ItemTemplate>
            </labitec:GridColumn>
            <labitec:GridColumn ID="GridColumn7" DataField="tbl_TaskResult.Title" HeaderText="Результат" runat="server"/>
        </Columns>           
        <Joins>
            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_TaskResult" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskResultID" runat="server" />   
            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_TaskType" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskTypeID" runat="server" />                        
            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Task" TableKey="ResponsibleID" runat="server" />
        </Joins>
    </labitec:Grid>
    <telerik:RadScheduler ID="rScheduler" 
                          AllowDelete="false"                                                     
                          AllowInsert="false" 
                          AllowEdit="false"
                          OnClientAppointmentEditing="OnClientAppointmentEditing" 
                          OnClientAppointmentDoubleClick="OnClientAppointmentDoubleClick" 
                          OnAppointmentDataBound="rScheduler_OnAppointmentDataBound"                                                                               
                          OnNavigationComplete="rScheduler_OnNavigationComplete"                          
                          Height="600px"
                          HoursPanelTimeFormat="HH:mm"                          
                          DisplayDeleteConfirmation="false"
                          Skin="Windows7" runat="server">    
    </telerik:RadScheduler>
</asp:Content>
