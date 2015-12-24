<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactAdd.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Contact.ContactAdd" %>
<%@ Register TagPrefix="uc" TagName="ContactEdit" Src="~/UserControls/Contact/ContactEdit.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Теги">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsContact" ObjectTypeName="tbl_Contact" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <div class="two-columns">
	            <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		            <Tabs>
			            <telerik:RadTab Text="Карточка контакта" />
		            </Tabs>
	            </telerik:RadTabStrip>
	            <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		            <telerik:RadPageView ID="RadPageView1" runat="server">
			            <uc:ContactEdit runat="server" ID="ucContactEdit" />
		            </telerik:RadPageView>
	            </telerik:RadMultiPage>

                <br/>
                <div class="buttons">
	                <asp:LinkButton ID="btnUpdate" CssClass="btn" OnClick="btnUpdate_Click" ValidationGroup="groupAddContact" runat="server"><em>&nbsp;</em><span>Добавить контакт</span></asp:LinkButton>
	                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
                </div>
            </div>
        </td>
    </tr>
</table>