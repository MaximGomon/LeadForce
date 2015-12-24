<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Materials.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard.Materials" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<asp:Panel ID="pnlValdationForm" Visible="False" runat="server">
    <div class="warning">Заполните все формы в материалах</div>
    <br />
</asp:Panel>

<div class="wizard-step">
    <h3>Материалы</h3>
    
    <telerik:RadGrid ID="rgMaterials" ShowHeader="False" Skin="Windows7"
    OnUpdateCommand="rgMaterials_OnUpdateCommand"
    OnNeedDataSource="rgMaterials_OnNeedDataSource"
    OnItemDataBound="rgMaterials_OnItemDataBound"
    OnEditCommand="rgMaterials_OnEditCommand"
    AutoGenerateColumns="False" runat="server">
        <MasterTableView DataKeyNames="ID" EditMode="InPlace">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Name">
                    <ItemTemplate>
                        <%--<asp:HyperLink ID="hlName" Target="_blank" runat="server" />--%>
                        <b><asp:Literal ID="litName" runat="server" /></b>
                        <br />
                        <asp:Literal ID="litDescription" runat="server" />
                        <div class="span-url" style="padding: 0">Тип: <asp:Literal ID="litType" runat="server" /></div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridTemplateColumn HeaderText="Тип">
                    <ItemTemplate>
                        <asp:Literal ID="litType" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <%--<telerik:GridBoundColumn HeaderText="Описание" DataField="Description" UniqueName="Description" />--%>
                <telerik:GridTemplateColumn HeaderText="Текущая версия">
                    <ItemTemplate>
                        <asp:Literal ID="litValue" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Panel ID="pnlUrl" runat="server">
                            <asp:TextBox ID="txtValue" CssClass="input-text" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtValue" CssClass="required" runat="server">*</asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlFile" runat="server">
                            <table>
                                <tr>
                                    <td valign="top" style="border: 0">
                                        <uc:DictionaryOnDemandComboBox ID="dcbFile" Width="234px" DictionaryName="tbl_Links" DataTextField="Name" ShowEmpty="True" EmptyItemText="Выберите файл" CssClass="select-text" ValidationErrorMessage="Вы не выбрали 'Файл'" ValidationGroup="vgMaterials" runat="server" />
                                    </td>
                                    <td style="border: 0">
                                        <telerik:RadAsyncUpload ID="rauFile" ClientIDMode="AutoID" OnClientFileUploadRemoved="fileUploadRemoved" OnClientFileUploaded="fileUploaded" runat="server" Width="200px" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                                        <telerik:RadButton ID="lbAddFile" OnClick="lbAddFile_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="AutoID" CssClass="btnAddFile" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlForm" runat="server">
                            <uc:DictionaryOnDemandComboBox ID="dcbForm" Width="234px" DictionaryName="tbl_SiteActivityRules" DataTextField="Name" ShowEmpty="True" EmptyItemText="Выберите форму" CssClass="select-text" ValidationErrorMessage="Вы не выбрали 'Форма'" ValidationGroup="vgMaterials" runat="server" />
                        </asp:Panel>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn ItemStyle-Width="80px" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CommandName="Edit" Text="Изменить" CssClass="action-edit2" runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lbUpdate" CommandName="Update" Text="Сохранить" CssClass="action-save" runat="server"/>
                        <asp:LinkButton ID="lbCancel" CommandName="Cancel" Text="Отмена" CssClass="action-delete" runat="server"/>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
             <NoRecordsTemplate>
                 <div style="padding: 10px; text-align: center;">
                     Нет записей.
                 </div>
             </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
    <br />

    <%--<h3>Шаблоны писем</h3>
    <telerik:RadPanelBar ID="rpbActionTemplates" ExpandMode="MultipleExpandedItems" Width="100%" Skin="Windows7" runat="server">
    </telerik:RadPanelBar>--%>
    
    <br />
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="Materials"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>