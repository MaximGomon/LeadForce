<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RefusedComment.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.UserControls.RefusedComment" %>

<div class="refused-comment-wrapper">
    <p>Пожалуйста, уточните причины Вашего отказа, чтобы мы могли учесть Ваше мнение в последующих предложениях:</p>
    <br/>
    <asp:TextBox runat="server" ID="txtRefusedComment" TextMode="MultiLine" CssClass="area-text" Width="400px" />
    <br/>
    <div class="buttons clearfix">    
        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn" OnClick="lnkSave_Click"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" OnClientClick="CloseRefusedCommentRadWindow(); return false;">Отмена</asp:LinkButton>
    </div>
</div>