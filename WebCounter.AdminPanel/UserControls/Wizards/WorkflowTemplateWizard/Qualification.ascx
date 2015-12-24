<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Qualification.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard.Qualification" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<div class="wizard-step">
    <h3>Сегментирование</h3>

    <telerik:RadGrid ID="rgWorkflowTemplateElement" ShowHeader="False" Skin="Windows7" OnNeedDataSource="rgWorkflowTemplateElement_OnNeedDataSource" AutoGenerateColumns="False" OnItemDataBound="rgWorkflowTemplateElement_OnItemDataBound" runat="server">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <b><asp:Literal ID="workflowElementTemplateName" runat="server" /></b><br />
                        <asp:Literal ID="workflowElementTemplateDescription" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="400px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblOperation" RepeatLayout="Flow" runat="server">
                                        <asp:ListItem Text="включить в" Value="1" />
                                        <asp:ListItem Text="исключить из" Value="0" />
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rblOperation" CssClass="required" ValidationGroup="Qualification" runat="server">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <uc:DictionaryOnDemandComboBox ID="dcbTag" Width="234px" DictionaryName="tbl_SiteTags" DataTextField="Name" CssClass="select-text" ValidationGroup="Qualification" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

    <br />
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="Qualification"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>