<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationTypes.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.Discussions.UserControls.ActivityRibbon.PublicationTypes" %>

<div class="publication-types">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadListBox ID="rlbPublicationTypes" 
                            OnSelectedIndexChanged="rlbPublicationTypes_OnSelectedIndexChanged"                        
                            EnableEmbeddedSkins="false" 
                            EnableEmbeddedBaseStylesheet="false" 
                            AutoPostBack="true" 
                            runat="server">
                <ItemTemplate>
                      <%# !string.IsNullOrEmpty((string)DataBinder.Eval(Container, "Attributes['Logo']")) ? string.Format("<img src='{0}' alt='{1}' />", DataBinder.Eval(Container, "Attributes['Logo']"), DataBinder.Eval(Container, "Text")) : "" %> <span><%# DataBinder.Eval(Container, "Text")%></span>
                      <div class="clear"></div>
                </ItemTemplate>
        </telerik:RadListBox>
    </telerik:RadAjaxPanel>
</div>