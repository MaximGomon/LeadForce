<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Segment.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Segment.Segment" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<table class="smb-files" width="100%"><tr>
<td width="195px" valign="top">
<div class="aside">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top" width="100%">

	<asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="groupSegment" />
    
    <div class="row">
		<label>Сегмент:</label>
		<asp:TextBox runat="server" ID="txtName" CssClass="input-text"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" Text="*" ErrorMessage="Вы не ввели название сегмента" ValidationGroup="groupSegment" runat="server" />
	</div>
    <div class="row">
		<label>Описание:</label>
		<asp:TextBox runat="server" ID="txtDescription" CssClass="area-text" Width="630px" Height="30px" TextMode="MultiLine" />
	</div>
    <div class="row">
        <labitec:Grid ID="gridSegments" TableName="tbl_Contact" RememberSelected="true" AccessCheck="true" ClassName="WebCounter.AdminPanel.SegmentContact"  Export="true" runat="server">
            <Columns>
                <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
                <labitec:GridColumn ID="GridColumn5" DataField="tbl_Company.Name" HeaderText="Компания" runat="server"/>
            </Columns>
            <Joins>
                <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_Company" JoinTableKey="ID" TableKey="CompanyID" runat="server" />
            </Joins>
        </labitec:Grid>
	</div>


	<br />

	<div class="buttons">        
		<asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupSegment" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		<asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		<div class="clear"></div>
		<br />
	</div>
    </td>
</tr></table>
