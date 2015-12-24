<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="KnowledgeBase.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.KnowledgeBase.KnowledgeBase" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="PublicationCategory" Src="~/Main/Discussions/UserControls/ActivityRibbon/PublicationCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreatePublication" Src="~/Main/Discussions/UserControls/ActivityRibbon/CreatePublication.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var handlerPath = '<%= ResolveUrl("~/Handlers/ActivityRibbon.aspx") %>';
    </script>    
    <script src="<%= ResolveUrl("~/Scripts/ActivityRibbon.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/jquery.textarea-expander.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="portal-two-column">
        <div class="left-column">
            <uc:PublicationCategory runat="server" ID="ucPublicationCategory" />            
        </div>
        <div class="right-column">
            <div class="b-block">
                <h4 class="top-radius">База знаний</h4>
                <div class="block-content bottom-radius">
                    <uc:CreatePublication runat="server" ID="ucCreatePublication" UseOnlySearch="true" />
                    <script src="<%= ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
                    <br/>
                    <telerik:RadListView runat="server" ID="rlvPublications" ItemPlaceholderID="itemPlaceHolder" OnPageIndexChanged="rlvPublications_OnPageIndexChanged">
                        <LayoutTemplate>
                            <ul class="publications">
                                <li runat="server" ID="itemPlaceHolder" />
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li class="clearfix">
                                <div class="image" runat="server" Visible='<%# Eval("Img") != null %>'>
                                    <telerik:RadBinaryImage runat="server" 
                                                            ID="RadBinaryImage1" 
                                                            DataValue='<%#Eval("Img") %>'
                                                            AutoAdjustImageControlSize="false" 
                                                            Width="125px" 
                                                            ToolTip='<%#Eval("Title") %>'
                                                            AlternateText='<%#Eval("Title") %>' />
                                </div>
                                <div class="description">
                                    <h2><a href='<%# UrlsData.LFP_Article(PortalSettingsId, (Guid)Eval("ID")) %>'><%# Eval("Title") %></a></h2>
                                    <asp:Literal runat="server" Visible='<%# !string.IsNullOrEmpty((string)Eval("Noun")) %>' 
                                                                Text='<%# string.Concat("<p>",Eval("Noun"),"</p>") %>' />
                                        
                                    
                                </div>                                
                                <ul class="operations clearfix">
                                    <li class="like"><a href="javascript:;" onclick='LikePublication(this, "<%# Eval("ID") %>")'><%# Eval("ContactLike") != null && int.Parse(Eval("ContactLike").ToString()) == 1 ? "Больше не нравится" : "Мне нравится"%></a><span>&nbsp;</span></li>                                    
                                    <li class="date"><%# Eval("FormattedDate")%><span>&nbsp;</span></li>
                                    <li class="category"><%# Eval("Category")%></li>
                                </ul>
                            </li>
                        </ItemTemplate>
                        <ItemSeparatorTemplate>
                            <li class="separator">&nbsp;</li>
                        </ItemSeparatorTemplate>
                        <EmptyDataTemplate>
                            База знаний в процессе наполнения.
                        </EmptyDataTemplate>
                    </telerik:RadListView>                    
                    <telerik:RadDataPager ID="radDataPager" 
                                            runat="server"
                                            CssClass="paging clearfix"                                              
                                            AllowSEOPaging="true"                                                                                          
                                            SEOPagingQueryPageKey="p"
                                            PageSize="10"                                               
                                            PagedControlID="rlvPublications" 
                                            EnableEmbeddedSkins="false" 
                                            EnableEmbeddedBaseStylesheet="false">
                        <Fields>
                            <telerik:RadDataPagerTemplatePageField HorizontalPosition="LeftFloat">
                                <PagerTemplate>
                                    <asp:Literal runat="server" Text="&nbsp;" Visible='<%# Container.Owner.CurrentPageIndex == 0 %>' />                                    
                                    <asp:Panel runat="server" Visible='<%# Container.Owner.CurrentPageIndex > 0 %>'>
                                        <a href='<%# "?p=" + Container.Owner.CurrentPageIndex %>'>Предыдущая</a>
                                    </asp:Panel>
                                </PagerTemplate>
                            </telerik:RadDataPagerTemplatePageField>
                            <telerik:RadDataPagerButtonField FieldType="Numeric" HorizontalPosition="NoFloat" PageButtonCount="10"/>                                
                            <telerik:RadDataPagerTemplatePageField HorizontalPosition="RightFloat">
                                <PagerTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Visible='<%#Container.Owner.CurrentPageIndex + 1 < Container.Owner.PageCount %>'>
                                        <a href='<%# "?p=" + (Container.Owner.CurrentPageIndex + 2) %>'>Следующая</a>
                                    </asp:Panel>
                                </PagerTemplate>
                            </telerik:RadDataPagerTemplatePageField>
                        </Fields>    
                    </telerik:RadDataPager>                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
