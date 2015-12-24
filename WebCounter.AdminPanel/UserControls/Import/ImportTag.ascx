<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportTag.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Import.ImportTag" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>

<h3>Сегментирование</h3>
<telerik:RadGrid ID="rgImportTags"
                    OnNeedDataSource="rgImportTags_OnNeedDataSource"
                    OnItemDataBound="rgImportTags_OnItemDataBound"
                    OnDeleteCommand="rgImportTags_OnDeleteCommand"
                    OnInsertCommand="rgImportTags_OnInsertCommand"
                    OnUpdateCommand="rgImportTags_OnUpdateCommand"
                    AutoGenerateColumns="false"
                    Width="95%"
                    Skin="Windows7"
                    CssClass="grid-default"
                    runat="server">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="EditForms">
        <CommandItemSettings AddNewRecordText="Добавить" ShowRefreshButton="False" />
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Сегмент">
                <ItemTemplate>
                    <asp:Literal ID="litName" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Операция">
                <ItemTemplate>
                    <asp:Literal ID="litOperation" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>      
            <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
			<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
				ConfirmTitle="Удалить" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
				ConfirmDialogWidth="420px" />       
        </Columns>
        <EditFormSettings EditFormType="Template">
            <FormTemplate>
                <div style="padding: 5px">
                    <div class="row">
                        <label>Сегмент:</label>
                        <uc:DictionaryOnDemandComboBox ID="dcbSiteTags" DictionaryName="tbl_SiteTags" DataTextField="Name" AllowCustomText="True" ShowEmpty="true" CssClass="select-text" ValidationGroup="groupAddTag" runat="server"/>
                    </div>
                    <div class="row">
                        <label>Операция:</label>
                        <asp:RadioButtonList ID="rblOperation" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server">
                            <asp:ListItem Text="Добавить" Value="1" Selected="True" />
                            <asp:ListItem Text="Исключить" Value="0" />
                        </asp:RadioButtonList>
                    </div>
                    <br />
			        <div class="buttons clearfix">
				        <asp:LinkButton ID="lbtnSave" ValidationGroup="groupAddTag" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
				        <asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
			        </div>
                </div>
            </FormTemplate>
        </EditFormSettings>
        <NoRecordsTemplate>
            <div style="padding: 10px; text-align: center;">
                Нет данных
            </div>
        </NoRecordsTemplate>
    </MasterTableView>
</telerik:RadGrid>