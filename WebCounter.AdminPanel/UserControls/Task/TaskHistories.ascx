<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskHistories.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.TaskHistories" %>

<labitec:Grid ID="gridTaskHistories" Toolbar="false" TableName="tbl_TaskHistory" ClassName="WebCounter.AdminPanel.TaskHistories" Fields="tbl_Contact.ID" runat="server" OnItemDataBound="gridTaskHistories_OnItemDataBound">
    <Columns>
        <labitec:GridColumn DataField="CreatedAt" HeaderText="Дата записи" runat="server"/>        
        <labitec:GridColumn DataField="DateStart" HeaderText="Дата начала" runat="server"/>
        <labitec:GridColumn DataField="DateEnd" HeaderText="Дата окончания" runat="server"/>
        <labitec:GridColumn DataField="DateOfControl" HeaderText="Дата контроля" runat="server"/>
        <labitec:GridColumn DataField="IsImportantTask" HeaderText="Важная задача" runat="server" DataType="Boolean"/>
        <labitec:GridColumn DataField="PlanDurationHours" HeaderText="Длительность план, часов" runat="server"/>
        <labitec:GridColumn DataField="PlanDurationMinutes" HeaderText="Длительность план, минут" runat="server"/>        
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
        <labitec:GridColumn DataField="DetailedResult" HeaderText="Результат подробно" runat="server"/>
    </Columns>        
    <Joins>
        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_TaskResult" JoinTableKey="ID" TableName="tbl_TaskHistory" TableKey="TaskResultID" runat="server" />                         
        <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_TaskHistory" TableKey="ResponsibleID" runat="server" />
    </Joins>
</labitec:Grid>