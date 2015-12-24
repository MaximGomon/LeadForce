<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCategory.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SelectCategory" %>
<telerik:RadScriptBlock runat="server">
<script type="text/javascript">    
    function nodeClicking(sender, args) {
        var comboBox = $find("radCategories");

        var node = args.get_node();

        comboBox.trackChanges();
        comboBox.set_text(node.get_text());
        comboBox.set_value(node.get_value());
        comboBox.get_items().getItem(0).set_text(node.get_text());
        comboBox.get_items().getItem(0).set_value(node.get_value());
        comboBox.commitChanges();
        comboBox.hideDropDown();
    }

    function _stopPropagation(e) {
        if (!e) {
            e = window.event;
        }

        e.cancelBubble = true;
    }

    function OnClientDropDownOpenedHandler(sender, eventArgs) {
        var tree = sender.get_items().getItem(0).findControl("radProductCategoryTreeView");
        var selectedNode = tree.get_selectedNode();
        if (selectedNode) {
            selectedNode.scrollIntoView();
        }
    }
    $(document).ready(function () {
        var templatedd = document.getElementById("radProductCategoryTreeView");
        templatedd.onclick = _stopPropagation; 
    });
</script>
</telerik:RadScriptBlock>
<telerik:RadComboBox ID="radCategories" runat="server" ShowToggleImage="True" EnableEmbeddedSkins="false" skin="Labitec" Width="234px" Style="vertical-align: middle;" OnClientDropDownOpened="OnClientDropDownOpenedHandler" ExpandAnimation-Type="None" CollapseAnimation-Type="None" ClientIDMode="Static">
    <ItemTemplate>
            <telerik:RadTreeView runat="server" ID="radProductCategoryTreeView" OnClientNodeClicking="nodeClicking" Height="140" ClientIDMode="Static" Skin="Windows7" />
    </ItemTemplate>
    <Items>
        <telerik:RadComboBoxItem Text="" />
    </Items>
</telerik:RadComboBox>
<asp:RequiredFieldValidator ID="rfvCategories" ControlToValidate="radCategories" CssClass="input-text" Text="*" ErrorMessage="Вы не выбрали категорию" runat="server" />
