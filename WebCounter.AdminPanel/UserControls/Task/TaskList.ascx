<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskList.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.TaskList" %>

<labitec:Grid ID="gridTasks" Toolbar="true" TableName="tbl_Task" Fields="tbl_Contact.ID" Export="true" runat="server" OnItemDataBound="gridTasks_OnItemDataBound">
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
        <labitec:GridColumn ID="GridColumn7" DataField="tbl_TaskResult.Title" HeaderText="Результат" runat="server"/>
    </Columns>    
    <Joins>
        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_TaskResult" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskResultID" runat="server" />   
        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_TaskType" JoinTableKey="ID" TableName="tbl_Task" TableKey="TaskTypeID" runat="server" />                        
        <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Task" TableKey="ResponsibleID" runat="server" />
    </Joins>
</labitec:Grid>