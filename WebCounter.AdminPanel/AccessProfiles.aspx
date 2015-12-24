<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccessProfiles.aspx.cs" Inherits="WebCounter.AdminPanel.AccessProfiles" %>
<%@ Register TagPrefix="uc" TagName="MenuConstructor" Src="~/UserControls/MenuConstructor.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function CloseTooltip() {
        var controller = Telerik.Web.UI.RadToolTipController.getInstance();
        var tooltip = controller.get_activeToolTip();
        if (tooltip)
            tooltip.hide();
        
        setTimeout(function () {
            $find('<%= radAjaxManager.ClientID %>').ajaxRequest("Rebind");
        }, 10);
    }    
</script>                            

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="aside">
                            <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                                <Items>
                                    <telerik:RadPanelItem Expanded="true" Text="Профили">
                                        <ContentTemplate>
                                            <div class="aside-content">
                                                <div style="position: relative;">
                                                    <telerik:RadTextBox ID="txtAccessProfileTitle" CssClass="tbField" Skin="Windows7" Width="106" runat="server" /><telerik:RadButton ID="btnAddProfile" OnClick="btnAddProfile_OnClick" Text="" Skin="Windows7" runat="server" />
                                                </div>
                                                <telerik:RadListBox ID="rlbAccessProfile" OnSelectedIndexChanged="rlbAccessProfile_OnSelectedIndexChanged" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false" AutoPostBack="true" runat="server" />
                                                <asp:Repeater ID="rAccessProfile" runat="server">
                                                    <ItemTemplate>
                                                        <%# Eval("Title") %>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelBar>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <div>
                    <asp:Panel runat="server" CssClass="row" ID="plProfileTitle">
                        <label>Название профиля</label>
                        <asp:TextBox runat="server" ID="txtProfileTitle" CssClass="input-text" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProfileTitle" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupProfileTitle" runat="server" />
                        <asp:LinkButton ID="lbtnSave" style="vertical-align: top" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupProfileTitle" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>        
                        <br/><br/>
                    </asp:Panel>

	                <telerik:RadTabStrip ID="TabStrip" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		                <Tabs>
			                <telerik:RadTab Text="Доступ к модулям" />
                            <telerik:RadTab Text="Доступ к записям" />
                            <telerik:RadTab Text="Рабочие столы" />
		                </Tabs>
	                </telerik:RadTabStrip>

	                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		                <telerik:RadPageView ID="RadPageView1" CssClass="clearfix" runat="server">
                            <labitec:Grid ID="gridAccessProfileModule" Toolbar="false" TableName="tbl_AccessProfileModule" Fields="tbl_AccessProfile.SiteID, tbl_AccessProfileModule.ModuleID" OnItemDataBound="gridAccessProfileModule_OnItemDataBound" ClassName="WebCounter.AdminPanel.AccessProfileModule" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="tbl_Module.Title" HeaderText="Модуль" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn2" DataField="Read" HeaderText="Чтение" HorizontalAlign="Center" Width="100px" runat="server">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbRead" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn3" DataField="Write" HeaderText="Изменение" HorizontalAlign="Center" Width="100px" runat="server">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbWrite" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn4" DataField="Delete" HeaderText="Удаление" HorizontalAlign="Center" Width="100px" runat="server">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDelete" AutoPostBack="true" OnCheckedChanged="cbRights_OnCheckedChanged" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                </Columns>
                                <Joins>
                                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_Module" JoinTableKey="ID" TableKey="ModuleID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_AccessProfile" JoinTableKey="ID" TableKey="AccessProfileID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView2" CssClass="clearfix" runat="server">
                            <telerik:RadToolTipManager ID="editRecordTooltip" EnableEmbeddedScripts="true" ShowEvent="OnClick" OffsetY="-1" HideEvent="ManualClose" Modal="true"
                                Width="470" Height="390" runat="server" EnableShadow="true" ManualCloseButtonText="Закрыть" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="Element"
                                Position="MiddleRight">                                
                            </telerik:RadToolTipManager>

                            <asp:LinkButton ID="btnAddAccessProfileRecord" runat="server"><asp:Image ID="Image1" ImageUrl="App_Themes/Default/images/btnAdd.png" AlternateText="Добавить запись" ImageAlign="Middle" runat="server" /></asp:LinkButton>
                            <br /><br />
                            <labitec:Grid ID="gridAccessProfileRecord" TableName="tbl_AccessProfileRecord" OnItemDataBound="gridAccessProfileRecord_OnItemDataBound" ClassName="WebCounter.AdminPanel.AccessProfileRecord" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn5" DataField="tbl_Module.Title" HeaderText="Модуль" runat="server" />
                                    <labitec:GridColumn ID="GridColumn6" DataField="CompanyRuleID" HeaderText="Правило по компании" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litCompanyRule" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn7" DataField="CompanyID" HeaderText="Компания" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litCompany" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn8" DataField="OwnerRuleID" HeaderText="Правило по ответственному" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litOwnerRule" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn9" DataField="OwnerID" HeaderText="Ответственный" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="litOwner" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn10" DataField="Read" HeaderText="Чтение" DataType="Boolean" HorizontalAlign="Center" Width="100px" runat="server" />
                                    <labitec:GridColumn ID="GridColumn11" DataField="Write" HeaderText="Изменение" DataType="Boolean" HorizontalAlign="Center" Width="100px" runat="server" />
                                    <labitec:GridColumn ID="GridColumn12" DataField="Delete" HeaderText="Удаление" DataType="Boolean" HorizontalAlign="Center" Width="100px" runat="server" />
                                    <labitec:GridColumn ID="GridColumn13" DataField="ID" HeaderText="Действия" HorizontalAlign="Center" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbEdit" runat="server"><asp:Image ID="Image2" ImageUrl="App_Themes/Default/images/icoView.png" AlternateText="Редактировать" ToolTip="Редактировать" runat="server" /></asp:LinkButton>
                                            <asp:LinkButton ID="lbDelete" OnClientClick="return confirm('Вы действительно хотите удалить запись?');" OnCommand="lbDelete_OnCommand" runat="server"><asp:Image ID="Image1" ImageUrl="App_Themes/Default/images/icoDelete.gif" AlternateText="Удалить" ToolTip="Удалить" runat="server" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                </Columns>
                                <Joins>
                                    <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Module" JoinTableKey="ID" TableKey="ModuleID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <uc:MenuConstructor ID="ucMenuConstructor" runat="server" />
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>