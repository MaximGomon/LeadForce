<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RunCondition.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Wizards.WorkflowTemplateWizard.RunCondition" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $('#<%= lbAddFile.ClientID %>').show();
        }
        
        function fileUploadRemoved(sender, args) {
            $('#<%= lbAddFile.ClientID %>').hide();
        }

        function FilterList(sender, args) {
            var txtFilterText = $('#txtFilterText').val().toLowerCase();
            var listbox = $find("<%= rblViewPages.ClientID %>");
            var items = listbox.get_items();
            for (var i = 0; i < items.get_count(); i++) {
                if (txtFilterText == "")
                    listbox.getItem(i).set_visible(true);
                else {
                    if (listbox.getItem(i).get_text().toLowerCase().indexOf(txtFilterText) != -1)
                        listbox.getItem(i).set_visible(true);
                    else
                        listbox.getItem(i).set_visible(false);
                }
            }
        }
    </script>
</telerik:RadScriptBlock>

<div class="wizard-step">
    <table>
        <tr>
            <td colspan="2">
                <div class="row">
                    <label>Условие:</label>
                    <asp:DropDownList ID="ddlCondition" AutoPostBack="true" OnSelectedIndexChanged="ddlCondition_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCondition" Display="Dynamic" ErrorMessage="Вы не выбрали 'Условие'" CssClass="required" ValidationGroup="RunCondition" runat="server">*</asp:RequiredFieldValidator>
                </div>
                <asp:Panel ID="pnlActivityCount" CssClass="row" Visible="false" runat="server">
                    <label>Кол-во действий:</label>
                    <telerik:RadNumericTextBox ID="txtActivityCount" Type="Number" CssClass="input-text" runat="server">
                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtActivityCount" Display="Dynamic" CssClass="required" ErrorMessage="Вы не ввели 'Кол-во действий'" ValidationGroup="RunCondition" runat="server">*</asp:RequiredFieldValidator>                            
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <telerik:RadDockLayout runat="server" ID="radDockLayout">
                        <telerik:RadDockZone runat="server" ID="radDockZone" Orientation="Horizontal" Skin="Windows7" Width="500px" MinHeight="400px">    
                        </telerik:RadDockZone>
                </telerik:RadDockLayout>
            </td>
            <td valign="top" style="padding-left: 10px">
                <telerik:RadPanelBar ID="rpbActivity" ExpandMode="SingleExpandedItem" Skin="Windows7" Width="300px" Height="400px" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="True" Text="Страницы">
                            <ContentTemplate>
                                <div class="siteactiontemplate-addfile clearfix">
                                    <asp:TextBox ID="txtFilterText" ClientIDMode="Static" Width="229" runat="server" />
                                    <telerik:RadButton ID="rbAddViewPage" OnClick="rbAddViewPage_OnClick" Icon-PrimaryIconCssClass="rbAdd" Icon-PrimaryIconLeft="12" Skin="Windows7" runat="server" />
                                </div>
                                <telerik:RadListBox ID="rblViewPages" OnDropped="rlb_OnDropped" EnableDragAndDrop="True" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="287px" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="True" Text="Формы">
                            <ContentTemplate>
                                <telerik:RadListBox ID="rlbForms" OnDropped="rlb_OnDropped" EnableDragAndDrop="True" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="330px" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="True" Text="Файлы">
                            <ContentTemplate>
                                <div class="siteactiontemplate-addfile clearfix">
                                    <label>Выберите файл:</label><telerik:RadAsyncUpload ID="rauFile" runat="server" Width="288px" OnClientFileUploadRemoved="fileUploadRemoved" OnClientFileUploaded="fileUploaded" MaxFileInputsCount="1" MultipleFileSelection="Disabled" Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                                    <telerik:RadButton ID="lbAddFile" OnClick="lbAddFile_OnClick" Text="Добавить" Skin="Windows7" ClientIDMode="AutoID" style="display:none" runat="server" />
                                </div>
                                <telerik:RadListBox ID="rlbFiles" OnDropped="rlb_OnDropped" EnableDragAndDrop="True" Skin="Windows7" CssClass="radlistbox-noborder" Width="298px" Height="256px" runat="server" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
            </td>
        </tr>
    </table>
    
    <br />
    <div class="buttons clearfix">
        <asp:LinkButton ID="lbtnNext" OnClick="lbtnNext_OnClick" CssClass="btn" runat="server" ValidationGroup="RunCondition"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
    </div>
</div>