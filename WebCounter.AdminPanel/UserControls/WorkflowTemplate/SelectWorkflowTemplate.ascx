<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectWorkflowTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.SelectWorkflowTemplate" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function ShowWorkflowTemplateRadWindow() {
            $find('<%= rwWorkflowTemplate.ClientID %>').show();
        }

        function CloseWorkflowTemplateRadWindow() {
            $find('<%= rwWorkflowTemplate.ClientID %>').close();
        }
    </script>
</telerik:RadScriptBlock>

<asp:Panel ID="pnlWorkflowTemplate" CssClass="inline" runat="server">
    <asp:LinkButton ID="lbWorkflowTemplate" OnClick="lbWorkflowTemplate_OnClick" runat="server" />
    <asp:TextBox ID="txtWorkflowTemplateId" CssClass="hidden" runat="server" />
    <asp:RequiredFieldValidator ID="rfvTemplateValidator" ControlToValidate="txtWorkflowTemplateId" ErrorMessage="Вы не выбрали 'Шаблон'" runat="server">*</asp:RequiredFieldValidator>
</asp:Panel>


<telerik:RadWindow runat="server" Title="Шаблон процесса" Width="1000px" ID="rwWorkflowTemplate" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner">
            <labitec:Grid ID="gridWorkflowTemplates" TableName="tbl_WorkflowTemplate" OnItemDataBound="gridWorkflowTemplates_OnItemDataBound" ClassName="WebCounter.AdminPanel.SelectWorkflowTemplates" runat="server">
                <Columns>
                    <labitec:GridColumn ID="GridColumn1" DataField="Name" HeaderText="Название" runat="server"/>
                    <labitec:GridColumn ID="GridColumn4" DataField="Status" HeaderText="Состояние" runat="server">
                        <ItemTemplate>
                            <asp:Literal ID="lrlWorkflowTemplateStatus" runat="server"/>
                        </ItemTemplate>
                    </labitec:GridColumn>
                    <labitec:GridColumn DataField="ID" HeaderText="Действие" Width="100px" HorizontalAlign="Center" Sortable="false" Reorderable="false" Groupable="false" Resizable="false" AllowFiltering="False" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="Выбрать" ID="lbtnSelect" OnClick="lbtnSelect_OnClick" />
                            <asp:Image runat="server" ImageUrl="~/App_Themes/Default/images/btnOk.png" Visible="false" Height="16px" ID="imgOk" />
                        </ItemTemplate>
                    </labitec:GridColumn>
                </Columns>
            </labitec:Grid>
        
            <br />
            <div class="buttons clearfix">                        
		        <asp:LinkButton ID="lbtnSave" CssClass="btn" OnClick="lbtnSave_OnClick" ValidationGroup="siteActionTemplateUpdate" CausesValidation="true" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                <a href="javascript:;" class="cancel" onclick="CloseWorkflowTemplateRadWindow(); return false;">Отмена</a>
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>