<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectRequirementSeverityOfExposure.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Requirement.SelectRequirementSeverityOfExposure" %>
<script type="text/javascript">
    function RequirementSeverityOfExposuresNodeClicking(sender, args) {
        var comboBox = $find("radRequirementSeverityOfExposures");
        var node = args.get_node();
        comboBox.trackChanges();
        comboBox.set_text(node.get_text());
        comboBox.set_value(node.get_value());
        comboBox.get_items().getItem(0).set_text(node.get_text());
        comboBox.get_items().getItem(0).set_value(node.get_value());
        comboBox.commitChanges();
        comboBox.hideDropDown();
    }

    function RequirementSeverityOfExposuresStopPropagation(e) {
        if (!e) { e = window.event;}
        e.cancelBubble = true;
    }
    function RequirementSeverityOfExposuresOnClientDropDownOpenedHandler(sender, eventArgs) {
        var tree = sender.get_items().getItem(0).findControl("radRequirementSeverityOfExposureTreeView");
        var selectedNode = tree.get_selectedNode();
        if (selectedNode) {
            selectedNode.scrollIntoView();
        }
    }
    $(document).ready(function () {
        var templatedd = document.getElementById("radRequirementSeverityOfExposureTreeView");
        templatedd.onclick = RequirementSeverityOfExposuresStopPropagation; 
    });

</script>

<telerik:RadComboBox ID="radRequirementSeverityOfExposures" runat="server" CssClass="select-text" ShowToggleImage="True" EnableEmbeddedSkins="false" skin="Labitec" Width="234px" Style="vertical-align: middle;" OnClientDropDownOpened="RequirementSeverityOfExposuresOnClientDropDownOpenedHandler" ExpandAnimation-Type="None" CollapseAnimation-Type="None" ClientIDMode="Static">
    <ItemTemplate>
            <telerik:RadTreeView runat="server" ID="radRequirementSeverityOfExposureTreeView" OnClientNodeClicking="RequirementSeverityOfExposuresNodeClicking" Height="140" ClientIDMode="Static" Skin="Windows7" />
    </ItemTemplate>
    <Items>
        <telerik:RadComboBoxItem Text="" />
    </Items>
</telerik:RadComboBox>
<asp:RequiredFieldValidator ID="rfvRequirementSeverityOfExposures" ControlToValidate="radRequirementSeverityOfExposures" CssClass="input-text" Text="*" ErrorMessage="Вы не выбрали категорию" runat="server" />