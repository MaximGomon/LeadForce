<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.RequirementModule.Requirement" %>
<%@ Register TagPrefix="uc" TagName="RequirementStatus" Src="~/Main/RequirementModule/UserControls/RequirementStatus.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/Shared/UserControls/ContentComments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="b-block">
        <h4 class="top-radius"><asp:Literal runat="server" ID="lrlTitle" /></h4>
        <div class="block-content bottom-radius">            
            <table>
                <tr>
                    <td colspan="2">
                        <uc:RequirementStatus runat="server" ID="ucRequirementStatus" />
                    </td>                    
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="row">
                            <label>Тип требования:</label>
                            <asp:Literal runat="server" ID="lrlRequirementType" />
                        </div>                                                
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <label>Источник:</label>
                            <asp:Literal runat="server" ID="lrlRequest" Text="---" />                            
                        </div>
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="lrlParentRequirement" />
                    </td>
                </tr>                
                <tr>                    
                    <td>
                        <div class="row">
                            <label>Договор:</label>
                            <asp:Literal runat="server" ID="lrlContract" Text="---" />                            
                        </div>
                    </td>
                    <td>
                        <div class="row">
                            <label>Счет:</label>
                            <asp:Literal runat="server" ID="lrlInvoice" Text="---" />                            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <label>Дата реализации, план:</label>
                            <asp:Literal runat="server" ID="lrlRealizationDatePlanned" Text="---" />                            
                        </div>
                    </td>                    
                    <td>
                        <asp:Panel runat="server" ID="plEstimate" CssClass="row" Visible="false">
                            <label>Оценка:</label>
                            <asp:Literal runat="server" ID="lrlEstimate" />
                        </asp:Panel>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <label>Дата реализации, факт:</label>
                            <asp:Literal runat="server" ID="lrlRealizationDateActual" Text="---" />                            
                        </div>
                    </td>
                    <td>
                        <asp:Panel runat="server" ID="plComment" CssClass="row row-normal" Visible="false">
                            <label>Комментарий:</label>
                            <asp:Literal runat="server" ID="lrlComment" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div class="row row-normal">
                <label style="width: 155px">Суть требования:</label>
                <asp:Literal runat="server" ID="lrlShortDescription" />
            </div>
            <uc:ContentComments runat="server" ID="ucContentComments" CommentType="tbl_RequirementComment" />            		    
        </div>
    </div>
</asp:Content>

