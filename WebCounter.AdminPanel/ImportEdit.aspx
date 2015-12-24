<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ImportEdit.aspx.cs" Inherits="WebCounter.AdminPanel.ImportEdit" %>
<%@ Register TagPrefix="uc" TagName="ImportTag" Src="~/UserControls/Import/ImportTag.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('<%= RadAjaxManager.GetCurrent(Page).ClientID %>').ajaxRequest();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<table class="smb-files" width="100%">
    <tr>
        <td width="195px" valign="top">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td valign="top" width="100%">
            <asp:Panel ID="pnlWarning" Visible="False" runat="server">
                <div class="warning">Выбран некорректный формат файла.</div>
                <br />
            </asp:Panel>

	        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						        CssClass="validation-summary"
						        runat="server"
						        EnableClientScript="true"
						        HeaderText="Заполните все поля корректно:"
						        ValidationGroup="valSave" />

	        <div class="row">
		        <label>Название:</label>
		        <asp:TextBox ID="txtName" CssClass="input-text" runat="server" />
		        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtName" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="valSave" runat="server" />
	        </div>
            
            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                <ContentTemplate>
	                <div class="row">
		                <label>Тип операции:</label>
	                    <asp:DropDownList ID="ddlType" AutoPostBack="True" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged" CssClass="select-text" runat="server" />
	                </div>
                </ContentTemplate>
            </asp:UpdatePanel>

	        <div class="row">
		        <label>Что импортируем:</label>
                <asp:DropDownList ID="ddlImportTable" CssClass="select-text" runat="server">
                    <Items>
                        <asp:ListItem Value="1">Клиентские данные</asp:ListItem>
                    </Items>
                </asp:DropDownList>
	        </div>


	        <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		        <Tabs>
			        <telerik:RadTab Text="Основные данные" />
			        <telerik:RadTab Text="Правила отображения столбцов" />
		        </Tabs>
	        </telerik:RadTabStrip>

	        <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		        <telerik:RadPageView ID="RadPageView1" CssClass="clearfix" runat="server">
                    <div style="width: 50%; float: left;">
                        <h3>Пример данных</h3>

                        <div class="row">
                            <label>Выбор файла:</label>
                            <telerik:RadAsyncUpload ID="uploadedFile" AllowedFileExtensions=".xls,.xlsx,.csv" MaxFileInputsCount="1" MultipleFileSelection="Disabled" OnClientFileUploaded="fileUploaded" OnFileUploaded="uploadedFile_OnFileUploaded" Skin="Windows7" runat="server" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена" />
                        </div>

                        <asp:UpdatePanel ID="upPreview" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                            <asp:Panel ID="pnlPreview" runat="server">
                                <telerik:RadGrid ID="rgPreview" Visible="false" CssClass="tbl-excel grid-default" Width="95%" Skin="Windows7" runat="server">
                                    <MasterTableView Width="100%" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight="180px" />
                                    </ClientSettings>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:RadGrid>

                                <h3>Столбцы</h3>
                                <telerik:RadGrid ID="rgImportColumns" AutoGenerateColumns="false" OnItemDataBound="rgImportColumns_OnItemDataBound" Width="95%" CssClass="tbl-import-columns grid-default" Skin="Windows7" runat="server">
                                    <MasterTableView DataKeyNames="ID">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Название">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txtName" CssClass="input-text" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName" Text="*" ErrorMessage="Вы не ввели название столбца" ValidationGroup="valSave" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Источник">
                                                <ItemTemplate>
                                                    <span id="spanSource" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Первичный ключ">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbPrimaryKey" runat="server"/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Вторичный ключ">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSecondaryKey" runat="server"/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div style="padding: 10px; text-align: center;">
                                                Нет данных
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <br /><br />
                            </asp:Panel>
                        
                                <uc:ImportTag ID="ucImportTag" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="width: 50%; float: left;">
                        <asp:UpdatePanel ID="upImportArea" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <h3>Область импорта</h3>
                                <asp:Panel ID="pnlExcelSettings" runat="server">
			                        <div class="row">
				                        <label>Вкладка:</label>
                                        <asp:DropDownList ID="ddlSheet" AutoPostBack="true" CssClass="select-text" OnSelectedIndexChanged="ddlSheet_OnSelectedIndexChanged" runat="server" />
			                        </div>
			                        <div class="row">
				                        <label>Строка начала:</label>
				                        <telerik:RadNumericTextBox ID="txtFirstRow" AutoPostBack="true" OnTextChanged="txtFirst_OnTextChanged" EmptyMessage="" MinValue="1" MaxValue="100000000" Value="1" CssClass="input-text" Width="60px" Height="18px" Type="Number" runat="server" >
					                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				                        </telerik:RadNumericTextBox>
				                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstRow" Text="*" ErrorMessage="Вы не ввели строку начала" ValidationGroup="valSave" runat="server" />
			                        </div>
			                        <div class="row">
				                        <label>Столбец начала:</label>
				                        <telerik:RadNumericTextBox ID="txtFirstColumn" AutoPostBack="true" OnTextChanged="txtFirst_OnTextChanged" EmptyMessage="" MinValue="1" MaxValue="100000000" Value="1" CssClass="input-text" Width="60px" Height="18px" Type="Number" runat="server" >
					                        <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
				                        </telerik:RadNumericTextBox>
				                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFirstColumn" Text="*" ErrorMessage="Вы не ввели столбец начала" ValidationGroup="valSave" runat="server" />
			                        </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlCsvSettings" Visible="False" runat="server">
                                    <div class="row">
                                        <label>Разделитель:</label>
                                        <telerik:RadComboBox ID="rcbCsvSeparator" AutoPostBack="True" AllowCustomText="True" OnSelectedIndexChanged="rcbCsvSeparator_OnSelectedIndexChanged" runat="server" />
                                        <%--<telerik:RadTextBox ID="txtCsvSeparator" CssClass="input-text" Text="," MaxLength="1" AutoPostBack="True" OnTextChanged="txtCsvSeparator_OnTextChanged" runat="server" />--%>
                                    </div>
                                </asp:Panel>

                                <div class="row">
                                    <label>Первая строка - заголовок:</label>
                                    <asp:CheckBox ID="cbIsFirstRowAsColumnNames" Checked="true" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </telerik:RadPageView>
		        <telerik:RadPageView ID="RadPageView2" runat="server">
                    <asp:UpdatePanel ID="upImportColumnRules" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <telerik:RadGrid ID="rgImportColumnRules" AutoGenerateColumns="false" OnItemDataBound="rgImportColumnRules_OnItemDataBound" Skin="Windows7" CssClass="grid-default" runat="server">
                                <MasterTableView DataKeyNames="ID">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Модуль LeadForce">
                                            <ItemTemplate>
                                                <span id="spanTableName" runat="server"></span>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Поле LeadForce" DataField="FieldTitle" />
                                        <telerik:GridTemplateColumn HeaderText="Обязательное поле">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbIsRequired" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Поле файла">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlImportColumns" CssClass="select-text" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="230px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Маска" Display="False">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txtSQLCode" CssClass="input-text" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Поле справочника" Display="False">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlImportFieldDictionary" CssClass="select-text" Visible="false" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </telerik:GridTemplateColumn>
                                        <%--<telerik:GridCheckBoxColumn HeaderText="Добавлять в справочник" />--%>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
	        <br/>
	        <div class="buttons">
                <asp:LinkButton ID="lbtnSaveAndImport" OnClick="lbtnSaveAndImport_OnClick" CssClass="btn" ValidationGroup="valSave" runat="server"><em>&nbsp;</em><span>Сохранить и импортировать</span></asp:LinkButton>
		        <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="valSave" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	        </div>
        </td>
    </tr>
</table>
</asp:Content>