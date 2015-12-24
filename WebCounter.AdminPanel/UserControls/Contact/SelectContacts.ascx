<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectContacts.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Contact.SelectContacts" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function <%= this.ClientID %>_ShowContactsRadWindow() {
            if ($find('<%= rwContacts.ClientID %>') != null) {            
                $find('<%= rwContacts.ClientID %>').show();
                $find('<%= rwContacts.ClientID %>').maximize();
            }
            else {
                setTimeout("<%= this.ClientID %>_ShowContactsRadWindow();", 10);
            }
        }
        function <%= this.ClientID %>_CloseContactsRadWindow() {                        
        function <%= this.ClientID %>_CloseContactsRadWindow() {
                $find('<%= rwContacts.ClientID %>').close();                
            }            
            if ($find('<%= rwContacts.ClientID %>') != null)
                $find('<%= rwContacts.ClientID %>').close();
        }

        function <%= this.ClientID %>_RadWindowAutoSize() {
            if ($find('<%= rwContacts.ClientID %>') != null)
                $find('<%= rwContacts.ClientID %>').autoSize(true);
        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadButton ID="rbAdd" ClientIDMode="AutoID" OnClick="rbAdd_OnClick" Text="Добавить контакты" CausesValidation="False" Skin="Windows7" runat="server" />

<telerik:RadWindow ID="rwContacts" runat="server" Title="Контакты" AutoSize="true" AutoSizeBehaviors="Height" Width="1000px" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup radwindow-popup-inner" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">    
    <ContentTemplate>
        <asp:UpdateProgress ID="SelectContactsUpdateProgress" runat="server">
            <ProgressTemplate>                
                <div class="ajax-loader"></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="radwindow-popup-inner">
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsContacts" GridControlID="gridContacts" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="true" Text="Фильтры">
                            <ContentTemplate>
                                <labitec:Filters ID="filtersContacts" GridControlID="gridContacts" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
            </div>

            <labitec:Grid ID="gridContacts" TableName="tbl_Contact" RememberSelected="True" OnItemDataBound="gridContacts_OnItemDataBound" AccessCheck="true" TagsControlID="tagsContacts" FiltersControlID="filtersContacts" ClassName="WebCounter.AdminPanel.Contacts" Export="False" runat="server">
                <Columns>
                    <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                    <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                    <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                    <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
                    <labitec:GridColumn ID="GridColumn5" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
                    <labitec:GridColumn ID="GridColumn6" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
                    <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
                    <labitec:GridColumn ID="GridColumn11" DataField="ID" HeaderText="Действия" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" ImageUrl="~/App_Themes/Default/images/icoView.png" ToolTip="Карточка контакта" runat="server" />
                        </ItemTemplate>
                    </labitec:GridColumn>
                    <labitec:GridColumn ID="GridColumn9" DataField="IsNameChecked" HeaderText="Корректность имени" DataType="Boolean" Visible="false" runat="server"/>
                    <labitec:GridColumn ID="GridColumn10" DataField="EmailStatusID" HeaderText="Статус Email" Visible="false" runat="server"/>
                </Columns>
                <Joins>
                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
                </Joins>
            </labitec:Grid>
        
            <br />
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div class="buttons clearfix">
		        <asp:LinkButton ID="lbtnSave" CssClass="btn" OnClick="lbtnSave_OnClick" CausesValidation="False" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                <asp:HyperLink ID="hlCancel" NavigateUrl="javascript:;" class="cancel" runat="server">Отмена</asp:HyperLink>
            </div>
            </telerik:RadAjaxPanel>
        </div>
    </ContentTemplate>
</telerik:RadWindow>