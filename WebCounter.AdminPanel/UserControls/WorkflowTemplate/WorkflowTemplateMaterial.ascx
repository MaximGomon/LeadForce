<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="WorkflowTemplateMaterial.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.WorkflowTemplate.WorkflowTemplateMaterial" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $('.btnAddFile').show();
        }
        function fileUploadRemoved(sender, args) {
            $('.btnAddFile').hide();
        }
    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel runat="server">
<telerik:RadGrid ID="rgMaterials"
                 AutoGenerateColumns="False"
                 ShowStatusBar="True"
                 OnNeedDataSource="rgMaterials_OnNeedDataSource"
                 OnItemDataBound="rgMaterials_OnItemDataBound"
                 OnInsertCommand="rgMaterials_OnInsertCommand"
                 OnUpdateCommand="rgMaterials_OnUpdateCommand"
                 OnDeleteCommand="rgMaterials_OnDeleteCommand"
                 Skin="Windows7"
                 runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
        <CommandItemSettings AddNewRecordText="Добавить" ShowRefreshButton="False" />
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Name">
                <ItemTemplate>
                    <asp:HyperLink ID="hlName" Target="_blank" runat="server" />
                    <asp:Literal ID="litName" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Тип">
                <ItemTemplate>
                    <asp:Literal ID="litType" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Описание" DataField="Description" />
            <telerik:GridTemplateColumn HeaderText="Текущая версия">
                <ItemTemplate>
                    <asp:Literal ID="litValue" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Удалить" ConfirmText="Вы действительно хотите удалить запись?" CommandName="Delete" ButtonType="ImageButton" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <EditFormSettings EditFormType="Template">
            <PopUpSettings Width="708px" Modal="True" />
            <FormTemplate>
                <table class="tbl-materials">
                    <tr>
                        <td style="width: 150px">Название:</td>
                        <td>
                            <asp:TextBox ID="txtName" Text='<%# Bind("Name") %>' CssClass="input-text" Width="516px" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="txtName" ValidationGroup="vgMaterials" CssClass="required" runat="server">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Тип:</td>
                        <td>
                            <asp:RadioButtonList ID="rblType" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rblType_OnSelectedIndexChanged" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Описание:</td>
                        <td><asp:TextBox ID="txtDescription" Text='<%# Bind("Description") %>' TextMode="MultiLine" CssClass="area-text" Width="516px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="litValue" Text="URL:" runat="server" /></td>
                        <td>
                            <asp:Panel ID="pnlUrl" runat="server">
                                <asp:TextBox ID="txtValue" Text='<%# Bind("Value") %>' CssClass="input-text" Width="516px" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtValue" ValidationGroup="vgMaterials" CssClass="required" runat="server">*</asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlFile" Visible="False" runat="server">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <uc:DictionaryOnDemandComboBox ID="dcbFile" Width="234px" DictionaryName="tbl_Links" DataTextField="Name" ShowEmpty="True" EmptyItemText="Выберите файл" CssClass="select-text" ValidationErrorMessage="Вы не выбрали 'Файл'" ValidationGroup="vgMaterials" runat="server" />
                                        </td>
                                        <td style="padding-left: 10px">
                                            <telerik:RadAsyncUpload ID="rauFile" ClientIDMode="AutoID" OnClientFileUploadRemoved="fileUploadRemoved" OnClientFileUploaded="fileUploaded" runat="server" Width="200px" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                                            <telerik:RadButton ID="lbAddFile" OnClick="lbAddFile_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="AutoID" CssClass="btnAddFile" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            
                            <asp:Panel ID="pnlForm" Visible="False" runat="server">
                                <uc:DictionaryOnDemandComboBox ID="dcbForm" Width="234px" DictionaryName="tbl_SiteActivityRules" DataTextField="Name" ShowEmpty="True" EmptyItemText="Выберите форму" CssClass="select-text" ValidationErrorMessage="Вы не выбрали 'Форма'" ValidationGroup="vgMaterials" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="buttons clearfix">
	                            <asp:LinkButton ID="lbtnSave" ValidationGroup="vgMaterials" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
	                            <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
                            </div>
                        </td>
                    </tr>
                </table>
            </FormTemplate>
        </EditFormSettings>
    </MasterTableView>
	<ClientSettings>
		<ClientEvents OnPopUpShowing="PopUpShowingTop" />
	</ClientSettings>
</telerik:RadGrid>
</telerik:RadAjaxPanel>