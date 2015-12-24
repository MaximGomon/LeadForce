<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Segments.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Segments" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectContacts" Src="~/UserControls/Contact/SelectContacts.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function ShowContactsListRadWindow() { $find('<%= rwContactsList.ClientID %>').show(); }
        function CloseContactsListRadWindow() { $find('<%= rwContactsList.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<table class="smb-files domain" width="100%"><tr>
<td width="195px" valign="top">
<div class="aside">
    <uc:LeftColumn runat="server" />
</div>
</td>
<td valign="top">
<labitec:Search ID="searchSegments" GridControlID="gridSegments" OnDemand="True" SearchBy="Name" runat="server" />        
<div class="add-block  domain clearfix">
        <div>            
            <table width="100%">
                <tr>
                    <td colspan="2">                                         
                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
                                        ValidationGroup="valGroupAdd"
						                />
                    </td>
                </tr>
            <tr>
                <td width="170px"><label>Сегмент:</label></td>
                <td>
                    <asp:TextBox runat="server" ID="txtSegment" Width="400px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSegment" ErrorMessage="Вы не ввели сегмент" ValidationGroup="valGroupAdd" runat="server">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
                <td><label>Описание:</label></td>
                <td style="padding-top: 5px"><asp:TextBox runat="server" ID="txtDescription" Width="100%" TextMode="MultiLine" Height="30px" /></td>
            </tr>   --%>                     
            <tr>                            
                <td style="padding-top: 5px" colspan="2">
	                <div class="buttons clearfix">
	                    <telerik:RadButton OnClick="lbtnAdd_OnClick" ClientIDMode="AutoID" ID="lbtnAdd" Text="Добавить" Skin="Windows7" ValidationGroup="valGroupAdd" runat="server" />                        
	                </div>
                </td>
            </tr>  
        </table>
        </div>  
</div>                                    

<labitec:Grid ID="gridSegments" ShowHeader="false" Fields="Description"  Toolbar="false" SearchControlID="searchSegments" TableName="tbl_SiteTags" OnItemDataBound="gridSegments_OnItemDataBound" AccessCheck="false"  Export="true" runat="server">
    <Columns>
        <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Сегмент" runat="server">
            <ItemTemplate>                
                <div class="span-url"><span style="font-size: 12px;color:#2994CD"><asp:Literal ID="lrlName" runat="server" /></span></div>
                <div class="span-url"><asp:Literal ID="lrlDescription" runat="server" /></div>                                
                <div class="clear"></div>
                <asp:Panel runat="server" ID="plEdit" Visible="False">
                    <table width="100%">
                           <tr>
                                <td colspan="2">                                         
                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                            CssClass="validation-summary"
						                            runat="server"
						                            EnableClientScript="true"
						                            HeaderText="Заполните все поля корректно:"
                                                    ValidationGroup="valGroupUpdate"
						                            />
                                </td>
                            </tr>
                        <tr>
                            <td width="170px"><label>Сегмент:</label></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtSegment" Width="400px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSegment" ErrorMessage="Вы не ввели сегмент" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Описание:</label></td>
                            <td style="padding-top: 5px"><asp:TextBox runat="server" ID="txtDescription" Width="100%" TextMode="MultiLine" Height="30px" /></td>
                        </tr>                         
                        <tr>                            
                            <td style="padding-top: 5px" colspan="2">
	                            <div class="buttons clearfix">
	                                <telerik:RadButton OnClick="lbtnUpdate_OnClick" ClientIDMode="AutoID" ID="lbtnUpdate" Text="Сохранить" Skin="Windows7" ValidationGroup="valGroupUpdate" runat="server" />
                                    <telerik:RadButton ID="lbCancel" ClientIDMode="AutoID" Text="Отмена" Skin="Windows7" runat="server" CssClass="left"/>
	                            </div>
                            </td>
                        </tr>  
                    </table>
                </asp:Panel>
            </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn12" DataField="Description" Width="300px" Height="65px" runat="server" HorizontalAlign="Right">
        <ItemTemplate>
            <div class="span-url">Количество в сегменте: <asp:Literal runat="server" Text="0" ID="lrlCount"/></div>
        </ItemTemplate>
        </labitec:GridColumn>
        <labitec:GridColumn ID="GridColumn3"  DataField="ID" HeaderText="Операции" Width="100px" Height="65px" VerticalAlign="Top" HorizontalAlign="Left" runat="server">                            
            <ItemTemplate>                                
                <asp:LinkButton ID="lbEdit" OnCommand="lbEdit_OnCommand"  CssClass="smb-action" runat="server"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Default/images/icoView.png" AlternateText="Изменить" ToolTip="Изменить" runat="server"/><span style="padding-left: 3px">Изменить</span></asp:LinkButton><br/>
                <asp:LinkButton ID="lbContacts" OnCommand="lbContacts_OnCommand" CssClass="smb-action" runat="server"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Default/images/icoContactList.png" AlternateText="Проверить" ToolTip="КЛиенты" runat="server"/><span style="padding-left: 3px">Клиенты</span></asp:LinkButton><br/>
                <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server" CssClass="smb-action"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /><span style="padding-left: 5px">Удалить</span></asp:LinkButton>
            </ItemTemplate>
        </labitec:GridColumn>            
    </Columns>
</labitec:Grid>
</td>
</tr></table>

<telerik:RadWindow runat="server" Title="Список клиентов" Width="1200px" AutoSize="true" AutoSizeBehaviors="Height" ID="rwContactsList" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>        
            <div class="radwindow-popup-inner">
                <uc:SelectContacts ID="ucSelectContacts" OnSelectedChanged="ucSelectContacts_OnSelectedChanged" runat="server" />
                <br/><br/>                    
                <labitec:Grid ID="gridContacts" TableName="tbl_Contact" AccessCheck="true" OnItemDataBound="gridContacts_OnItemDataBound" ClassName="WebCounter.AdminPanel.SegmentContacts" Export="true" runat="server">
                    <Columns>
                        <labitec:GridColumn ID="GridColumn2" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                        <labitec:GridColumn ID="GridColumn4" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                        <labitec:GridColumn ID="GridColumn5" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                        <labitec:GridColumn ID="GridColumn6" DataField="Email" HeaderText="E-mail" runat="server"/>
                        <labitec:GridColumn ID="GridColumn7" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
                        <labitec:GridColumn ID="GridColumn8" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
                        <labitec:GridColumn ID="GridColumn10" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
                        <labitec:GridColumn ID="GridColumn11" DataField="ID" HeaderText="Действия" Width="100px" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" OnCommand="ibtnDelete_OnCommand" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" ToolTip="Удалить" AlternateText="Удалить" CausesValidation="False" runat="server"/>
                            </ItemTemplate>
                        </labitec:GridColumn>
                    </Columns>
                    <Joins>
                        <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
                        <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
                    </Joins>
                </labitec:Grid>         
                <br/>                                       
                <div class="buttons clearfix">
                    <asp:LinkButton runat="server" ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseContactsListRadWindow();" />
                </div>
            </div>        
        
    </ContentTemplate>
</telerik:RadWindow>