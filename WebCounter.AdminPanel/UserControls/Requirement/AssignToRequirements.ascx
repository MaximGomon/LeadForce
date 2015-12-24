<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignToRequirements.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Requirement.AssignToRequirements" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript" language="javascript">
        function ClientNodeClicked(sender, eventArgs) {
            var node = eventArgs.get_node();
            var isChecked = node.get_checked();
            node.set_checked(!isChecked);
            node.set_selected(!isChecked);
        }
        function ShowAssignToRequirementRadWindow() { $find('<%= rwAssignToRequirement.ClientID %>').show(); }
        function CloseAssignToRequirementRadWindow() { $find('<%= rwAssignToRequirement.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<asp:HyperLink ID="lbtnAssignToRequirment" NavigateUrl="javascript:;" onclick="ShowAssignToRequirementRadWindow()" CssClass="btn" runat="server"><em>&nbsp;</em><span>Связать с требованием</span></asp:HyperLink>
<telerik:RadWindow runat="server" Title="Связать с требованием" Width="800px" Height="600px" ID="rwAssignToRequirement" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner">
            <telerik:RadAjaxPanel runat="server">
                <div class="assign-to-requirement">
                    <div class="row">
                        <b>Фильтр по статусам:</b><br/><br/><asp:CheckBoxList runat="server" ID="chxlistRequirementStatuses" AutoPostBack="true" OnSelectedIndexChanged="chxlistRequirementStatuses_OnSelectedIndexChanged" CssClass="requirement-status-list clearfix" RepeatLayout="UnorderedList" />
                    </div>    
                    <telerik:RadTreeView ID="rtvRequirements" BorderWidth="1px" BorderColor="#C2CEDB" Skin="Windows7" OnClientNodeClicked="ClientNodeClicked" runat="server" OnNodeDataBound="rtvRequirements_OnNodeDataBound"
                CheckBoxes="True" Height="370px" TriStateCheckBoxes="true" CheckChildNodes="true"  />
                </div>
                <br/>
                <div class="buttons clearfix" style="position: absolute; bottom: 20px">
  	                <asp:LinkButton runat="server" ID="lbtnAssign" CssClass="btn" OnClick="lbtnAssign_OnClick"><em>&nbsp;</em><span>Связать</span></asp:LinkButton>
	                <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" OnClick="lbtnCancel_OnClick" Text="Отмена" />
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </ContentTemplate>
</telerik:RadWindow>