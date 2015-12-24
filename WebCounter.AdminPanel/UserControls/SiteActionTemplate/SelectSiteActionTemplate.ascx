<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectSiteActionTemplate.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.SelectSiteActionTemplate" %>

<div class="row">
    <label>Загрузить шаблон:</label>
    <asp:RadioButtonList ID="rblActionTemplate" ClientIDMode="AutoID" AutoPostBack="True" OnSelectedIndexChanged="rblActionTemplate_OnSelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server" />
</div>
<telerik:RadRotator ID="rrThumbnails" OnItemDataBound="rrThumbnails_OnItemDataBound" RotatorType="Buttons" WrapFrames="False" CssClass="siteactiontemplate-rotator" Skin="Windows7" Width="904px" Height="144px" ItemWidth="144px" ItemHeight="133px" runat="server">
    <ItemTemplate>
        <asp:LinkButton ID="lbThumbnail" OnCommand="lbThumbnail_OnCommand" CssClass="item" runat="server">
            <asp:Image ID="imgThumbnail" runat="server" />
        </asp:LinkButton>
    </ItemTemplate>
</telerik:RadRotator>