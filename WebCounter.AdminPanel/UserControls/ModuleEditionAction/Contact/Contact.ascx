<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.ModuleEditionAction.Contact.Contact" %>
<%@ Register TagPrefix="uc" TagName="ContactEdit" Src="~/UserControls/ModuleEditionAction/Contact/ContactEdit.ascx" %>
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <telerik:RadPanelBar ID="RadPanelBar2" Width="189px" Skin="Windows7" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="true" Text="Сегменты">
                            <ContentTemplate>
                                <labitec:Tags ID="tagsContact" ObjectTypeName="tbl_Contact" SimpleMode="True" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
            </div>
        </td>
        <td>
            <div>
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

	            <br />

	
	            <div class="buttons">        
		            <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupUpdateContact" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		            <div class="clear"></div>
		            <br />
	            </div>
            </div>
        </td>
    </tr>
</table>