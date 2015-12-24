<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteColumn.aspx.cs" Inherits="WebCounter.AdminPanel.SiteColumn" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr>
            <td width="195px" valign="top" ID="leftColumn" runat="server">
                <div class="aside" ID="asideDiv" runat="server">    
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td valign="top" width="100%">    
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label id="ErrorMessage" runat="server" CssClass="error-message" />
                    <div class="row">
                        <label>Название:</label>
                        <asp:TextBox ID="txtName" CssClass="input-text" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtName" ValidationGroup="valGroupSave" runat="server">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="row">
                        <label>Код:</label>
                        <asp:TextBox ID="txtCode" CssClass="input-text" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtCode" ValidationGroup="valGroupSave" runat="server">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ErrorMessage="Неверный формат кода." ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_-]+$" ValidationGroup="valGroupSave" runat="server" />
                    </div>
                    <div class="row row-dictionary">
                        <label>Категория:</label>
                        <asp:DropDownList ID="ddlCategoryID" CssClass="select-text" runat="server"></asp:DropDownList>
                        <asp:LinkButton runat="server" ID="lbtnEditCategory" CssClass="edit-button"><span>Редактировать</span></asp:LinkButton>
                    </div>
                    <div class="row">
                        <label>Тип:</label>
                        <asp:DropDownList ID="ddTypeID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddTypeID_SelectedIndexChanged" CssClass="select-text"></asp:DropDownList>
                    </div>
                    <asp:GridView ID="gvSiteColumnValues"
                                    Width="480px"
                                    OnRowEditing="gvSiteColumnValues_RowEditing"
                                    OnRowCancelingEdit="gvSiteColumnValues_RowCancelingEdit"
                                    OnRowUpdating="gvSiteColumnValues_RowUpdating" 
                                    OnRowDeleting="gvSiteColumnValues_RowDeleting"
                                    AutoGenerateColumns="False"
                                    ShowHeader="true"
                                    ShowFooter="true"
                                    GridLines="None"
                                    CssClass="grid"
                                    Visible="false"
                                    runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Значение" ItemStyle-Width="230px" HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                <ItemTemplate>
                                    <%# Server.HtmlEncode(Eval("Value").ToString())%>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValue" CssClass="input-text" runat="server" Text='<%# Eval("Value") %>' />
                                    <asp:RequiredFieldValidator ControlToValidate="txtValue" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValue" CssClass="input-text" runat="server" Text="" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtValue" ValidationGroup="valGroupInsert" runat="server">*</asp:RequiredFieldValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Действия" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="last" ItemStyle-CssClass="last">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text="Изменить" />
                                    <span>&nbsp;|&nbsp;</span>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' Text="Удалить" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton runat="server" ID="Update" Text="Обновить" CommandName="Update" ValidationGroup="valGroup" />
                                    <span>&nbsp;|&nbsp;</span>
                                    <asp:LinkButton runat="server" ID="Cancel" Text="Отмена" CommandName="Cancel" />
                                </EditItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <FooterTemplate>
                                    <asp:Button ID="BtnAddValue" OnClick="BtnAddValue_Click" Text="Добавить новое значение" ValidationGroup="valGroupInsert" runat="server" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <br />
                    <div class="buttons">
                        <asp:LinkButton ID="btnSave" CssClass="btn" OnClick="btnSave_Click" ValidationGroup="valGroupSave" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
    <telerik:RadToolTip ID="RadToolTip1" ManualClose="true" CssClass="labitec-dictionary-tooltip" ManualCloseButtonText="Закрыть" Modal="true" TargetControlID="lbtnEditCategory" ShowEvent="OnClick" Position="Center" RelativeTo="BrowserWindow" runat="server">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <labitec:Dictionary ID="lbcDictionaryCategory" runat="server" DataAccessAssemblyName="WebCounter.DataAccessLayer" ConnectionStringName="WebCounterEntities" DefaultContainerName="WebCounterEntities" DictionaryDataset="tbl_ColumnCategories"/>
        </telerik:RadAjaxPanel>
    </telerik:RadToolTip>
</asp:Content>