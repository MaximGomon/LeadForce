<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequirementStatus.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.RequirementModule.UserControls.RequirementStatus" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/Shared/UserControls/DictionaryComboBox.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function ShowClientReviewRadWindow() { $find('<%= clientReview.ClientID %>').show(); }
        function CloseClientReviewRadWindow() { $find('<%= clientReview.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <div class="row row-rbtn">
        <label>Состояние:</label>
        <asp:Literal runat="server" ID="lrlRequirementStatus" />
        <telerik:RadListView runat="server" ID="rlvRequirementTransitions">
            <ItemTemplate>
                <telerik:RadButton runat="server" ID="rbtnRequirementTransition" CausesValidation="false" Skin="Windows7" ClientIDMode="AutoID" Text='<%# Eval("Title") %>' CommandArgument='<%# Eval("ID") %>' OnClick="rbtnRequirementTransition_OnClick" />
            </ItemTemplate>
        </telerik:RadListView>
        <telerik:RadButton runat="server" ID="rbtnClientReview" CausesValidation="false" Skin="Windows7" Text='Отзыв' AutoPostBack="false" Visible="false" OnClientClicked="ShowClientReviewRadWindow" />
    </div>
</telerik:RadAjaxPanel>

<telerik:RadWindow runat="server" Title="Оставьте отзыв о нашей работе!" Width="780px" Height="285px" ID="clientReview" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-custom" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <telerik:RadAjaxPanel runat="server">
            <div class="radwindow-custom-inner">
                <div class="row">
                    <label>Полнота реализации:</label>
                    <uc:DictionaryComboBox runat="server" ID="dcbRequirementImplementationComplete" DictionaryName="tbl_RequirementImplementationComplete" DataTextField="Title" ShowEmpty="true" />
                </div>
                <div class="row">
                    <label>Скорость/сроки:</label>
                    <uc:DictionaryComboBox runat="server" ID="dcbRequirementSpeedTime" DictionaryName="tbl_RequirementSpeedTime" DataTextField="Title" ShowEmpty="true" />
                </div>
                <div class="row">
                    <label>Удовлетворенность:</label>
                    <uc:DictionaryComboBox runat="server" ID="dcbRequirementSatisfaction" DictionaryName="tbl_RequirementSatisfaction" DataTextField="Title" ShowEmpty="true" />
                </div>
                <div class="row">
		            <label>Отзыв:</label>
                    <asp:TextBox runat="server" ID="txtEstimationComment" CssClass="area-text" Width="560px" Height="30px" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEstimationComment" CssClass="required" Text="*" ErrorMessage="Вы не ввели комментарий" ValidationGroup="groupReview" runat="server" />
                </div>
                <div class="buttons clearfix">
                    <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupReview" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			        <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseClientReviewRadWindow();" />
		        </div>
             </div> 
          </telerik:RadAjaxPanel>
    </ContentTemplate>
</telerik:RadWindow>