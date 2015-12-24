<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParentRequirement.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Requirement.ParentRequirement" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript" language="javascript">
        var oldNode;
        function ClientLoad(sender, eventArgs) {
            var tree = $find('<%= rtvRequirements.ClientID %>');
            var checkedNodes = tree.get_checkedNodes();            
            if (checkedNodes) oldNode = checkedNodes[0];
        }
        function UpdateTreeView(node) {
            if (oldNode == node) return;
            if (oldNode != null) oldNode.set_checked(false);                        
            oldNode = node;
        }
        function ClientNodeChecked(sender, eventArgs) {
            var node = eventArgs.get_node();
            node.set_selected(node.get_checked());
            UpdateTreeView(node);
        }        
        function ClientNodeClicked(sender, eventArgs) {
            var node = eventArgs.get_node();
            var isChecked = node.get_checked();
            node.set_checked(!isChecked);
            node.set_selected(!isChecked);
            UpdateTreeView(node);
        }
        function ShowParentRequirementRadWindow(sender, args) { $find('<%= rwParentRequirement.ClientID %>').show(); }
        function CloseParentRequirementRadWindow() { $find('<%= rwParentRequirement.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<asp:Literal runat="server" ID="lrlParentRequirment" />
<telerik:RadButton runat="server" ID="rbtnSelectParent" Text="Выбрать" OnClientClicked="ShowParentRequirementRadWindow" AutoPostBack="false" Skin="Windows7" />

<telerik:RadWindow runat="server" Title="Родительское требование" Width="800px" Height="600px" ID="rwParentRequirement" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner">
            <telerik:RadAjaxPanel runat="server">
                <div class="assign-to-requirement">
                    <div class="row">
                        <b>Фильтр по статусам:</b><br/><br/><asp:CheckBoxList runat="server" ID="chxlistRequirementStatuses" AutoPostBack="true" OnSelectedIndexChanged="chxlistRequirementStatuses_OnSelectedIndexChanged" CssClass="requirement-status-list clearfix" RepeatLayout="UnorderedList" />
                    </div>
                    <telerik:RadTreeView ID="rtvRequirements" OnClientLoad="ClientLoad" BorderWidth="1px" BorderColor="#C2CEDB" Skin="Windows7" OnClientNodeChecked="ClientNodeChecked" OnClientNodeClicked="ClientNodeClicked" runat="server" OnNodeDataBound="rtvRequirements_OnNodeDataBound"
                CheckBoxes="True" Height="370px" MultipleSelect="false" TriStateCheckBoxes="true" CheckChildNodes="true"  />
                </div>
                <br/>
                <div class="buttons clearfix" style="position: absolute; bottom: 20px">
  	                <asp:LinkButton runat="server" ID="lbtnUpdate" CssClass="btn" OnClick="lbtnUpdate_OnClick"><em>&nbsp;</em><span>Обновить</span></asp:LinkButton>
	                <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" OnClick="lbtnCancel_OnClick" Text="Отмена" />
                </div>
            </telerik:RadAjaxPanel>
        </div> 
    </ContentTemplate>
</telerik:RadWindow>