<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterComment.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Requirement.RegisterComment" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript" language="javascript">
        var registerCommentOldNode;
        function RegisterCommentClientLoad(sender, eventArgs) {
            var tree = $find('<%= rtvRequirements.ClientID %>');
            var checkedNodes = tree.get_checkedNodes();
            if (checkedNodes) registerCommentOldNode = checkedNodes[0];
        }
        function RegisterCommentUpdateTreeView(node) {
            if (registerCommentOldNode == node) return;
            if (registerCommentOldNode != null) registerCommentOldNode.set_checked(false);
            registerCommentOldNode = node;
        }
        function RegisterCommentClientNodeChecked(sender, eventArgs) {
            var node = eventArgs.get_node();
            node.set_selected(node.get_checked());
            RegisterCommentUpdateTreeView(node);
        }
        function RegisterCommentClientNodeClicked(sender, eventArgs) {
            var node = eventArgs.get_node();
            var isChecked = node.get_checked();
            node.set_checked(!isChecked);
            node.set_selected(!isChecked);
            RegisterCommentUpdateTreeView(node);
        }
        function ShowRegisterCommentRadWindow(sender, args) {
            var commentEditor = eval('<%= ucComment.ClientID %>' + '_Editor();');
            GetSelectedText();
            $find('<%= rwRegisterComment.ClientID %>').show();
            commentEditor.set_html($('#hfSelection').val());
        }
        function CloseRegisterCommentRadWindow() { $find('<%= rwRegisterComment.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<asp:HyperLink ID="hlRegisterComment" NavigateUrl="javascript:;" onclick="ShowRegisterCommentRadWindow()" CssClass="btn" runat="server"><em>&nbsp;</em><span>Зарегистрировать комментарий</span></asp:HyperLink>

<telerik:RadWindow runat="server" Title="Зарегистрировать комментарий" Width="800px" AutoSize="true" AutoSizeBehaviors="Height" ID="rwRegisterComment" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner">
            <telerik:RadAjaxPanel runat="server">
                <div class="assign-to-requirement">
                    <div class="row">
                        <b>Фильтр по статусам:</b><br/><br/><asp:CheckBoxList runat="server" ID="chxlistRequirementStatuses" AutoPostBack="true" OnSelectedIndexChanged="chxlistRequirementStatuses_OnSelectedIndexChanged" CssClass="requirement-status-list clearfix" RepeatLayout="UnorderedList" />
                    </div>
                    <telerik:RadTreeView ID="rtvRequirements" OnClientLoad="RegisterCommentClientLoad" BorderWidth="1px" BorderColor="#C2CEDB" Skin="Windows7" OnClientNodeChecked="RegisterCommentClientNodeChecked" OnClientNodeClicked="RegisterCommentClientNodeClicked" runat="server" OnNodeDataBound="rtvRequirements_OnNodeDataBound"
                CheckBoxes="True" Height="130px" MultipleSelect="false" TriStateCheckBoxes="true" CheckChildNodes="true"  />
                </div>
                <br/>
                <div class="row">
                    <label>Комментарий</label>
                    <br/><br/>                    
                    <uc:HtmlEditor runat="server" ID="ucComment" Width="793px" Height="285px" Module="Requirements" />
                </div>
                <br/>
                <div class="buttons clearfix" style="position: absolute; bottom: 10px">
  	                <asp:LinkButton runat="server" ID="lbtnUpdate" CssClass="btn" OnClick="lbtnUpdate_OnClick"><em>&nbsp;</em><span>Зарегистрировать</span></asp:LinkButton>
	                <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" OnClick="lbtnCancel_OnClick" Text="Отмена" />
                </div>
            </telerik:RadAjaxPanel>
        </div> 
    </ContentTemplate>
</telerik:RadWindow>