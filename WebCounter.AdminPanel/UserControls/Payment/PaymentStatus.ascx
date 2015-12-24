<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentStatus.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Payment.PaymentStatus" %>

<telerik:RadAjaxPanel runat="server">
    <div class="row row-rbtn">
        <label>Состояние:</label>
        <asp:Literal runat="server" ID="lrlPaymentStatus" />
        <telerik:RadListView runat="server" ID="rlvPaymentTransitions">
            <ItemTemplate>
                <telerik:RadButton runat="server" ID="rbtnPaymentTransition" CausesValidation="false" Skin="Windows7" ClientIDMode="AutoID" Text='<%# Eval("Title") %>' CommandArgument='<%# Eval("FinalPaymentStatusID") %>' OnClick="rbtnPaymentTransition_OnClick" />
            </ItemTemplate>
        </telerik:RadListView>
    </div>
</telerik:RadAjaxPanel>