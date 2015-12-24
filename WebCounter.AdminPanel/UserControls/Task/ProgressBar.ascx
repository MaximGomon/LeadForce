<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgressBar.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.ProgressBar" %>

<div class="progressbar-wrapper">
    <asp:Panel runat="server" ID="plProgressBar" CssClass="progressbar-inner" />    
    <span><asp:Literal runat="server" ID="lrlPercents" /></span>                                    
</div>
