<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequirementStatus.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Requirement.RequirementStatus" %>

<telerik:RadAjaxPanel runat="server">
    <div class="row row-rbtn">
        <label>Состояние требования</label>
        <asp:Literal runat="server" ID="lrlRequirementStatus" />
        <telerik:RadListView runat="server" ID="rlvRequirementTransitions">
            <ItemTemplate>
                <telerik:RadButton runat="server" ID="rbtnRequirementTransition" CausesValidation="false" Skin="Windows7" ClientIDMode="AutoID" Text='<%# Eval("Title") %>' CommandArgument='<%# Eval("FinalRequirementStatusID") %>' OnClick="rbtnRequirementTransition_OnClick" />
            </ItemTemplate>
        </telerik:RadListView>
    </div>
</telerik:RadAjaxPanel>