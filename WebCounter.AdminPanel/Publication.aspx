<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Publication.aspx.cs" Inherits="WebCounter.AdminPanel.Publication" %>

<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="RelatedPublication" Src="~/UserControls/RelatedPublication.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationTerms" Src="~/UserControls/PublicationTerms.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationComment" Src="~/UserControls/PublicationComment.ascx" %>
<%@ Register TagPrefix="uc" TagName="PublicationMark" Src="~/UserControls/PublicationMark.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="Company" Src="~/UserControls/Company/CompanyComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%#ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        function fileUploaded(sender, args) {
            $find('<%= RadAjaxManager.GetCurrent(Page).ClientID %>').ajaxRequest();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <uc:LeftColumn runat="server" />
                </div>
            </td>
            <td>
                <div>
                        <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						CssClass="validation-summary"
						runat="server"
						EnableClientScript="true"
						HeaderText="Заполните все поля корректно:"
						ValidationGroup="valGroupUpdate" />

                    <div class="row row-product-name">
                        <label>Заголовок:</label>
                        <asp:TextBox runat="server" ID="txtTitle" CssClass="input-text"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите наименование'/>
                    </div>
                    <div class="left-column">
		                <div class="row">
			                <label>Код:</label>
                            <asp:TextBox runat="server" ID="txtCode" CssClass="input-text" />
		                </div>
		                <div class="row date-picker-autopostback clearfix">
			                <label>Дата публикации:</label>
			                <div class="date-picker-container">
				                <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpDate" ShowPopupOnFocus="true" Width="110px">
					                <DateInput Enabled="true" />
					                <DatePopupButton Enabled="true" />
				                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdpDate" Text="*" ErrorMessage="Вы не ввели дату" ValidationGroup="valGroupUpdate" runat="server" />
			                </div>
		                </div>
		                <div class="row">
			                <label>Автор:</label>
                            <uc:ContactComboBox ID="ucAuthor" CssClass="select-text" runat="server" ValidationErrorMessage="Вы не выбрали автора" FilterByFullName="true"/>

		                </div>
	                </div>
	                <div class="right-column">		
		                <div class="row">
			                <label>Тип:</label>
		                    <telerik:RadComboBox ID="rcbPublicationType" OnSelectedIndexChanged="rcbPublicationType_IndexChanged" runat="server" CssClass="select-text" EnableLoadOnDemand="true" AutoPostBack="true" skin="Labitec" Width="234px" EnableEmbeddedSkins=false ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None"/>
                            <asp:RequiredFieldValidator ID="rfvrcbPublicationType" InitialValue="Выберите значение" ControlToValidate="rcbPublicationType" Text="*" ErrorMessage="Вы не выбрали тип" ValidationGroup="valGroupUpdate" runat="server" />
                        </div>
                        <div class="row">
			                <label>Статус:</label>            
  		                    <telerik:RadComboBox ID="rcbPublicationStatus" CssClass="select-text" EnableLoadOnDemand="true" skin="Labitec" Width="234px" EnableEmbeddedSkins=false ShowToggleImage="True" ExpandAnimation-Type="None" CollapseAnimation-Type="None" runat="server"/>
                            <asp:RequiredFieldValidator ID="rfvrcbPublicationStatus" InitialValue="Выберите значение" ControlToValidate="rcbPublicationStatus" Text="*" ErrorMessage="Вы не выбрали статус" ValidationGroup="valGroupUpdate" runat="server" />
                        </div>
		                <div class="row">
			                <label>Категория:</label>
                            <uc:SelectCategoryControl runat="server" ID="sccPublicationCategory" CategoryType="Publication" ShowEmpty="true" CssClass="select-text" />
		                </div>
	                </div>
	                <div class="clear"></div>
                    <div class="row">
                        <label>Анонс/сообщение:</label>
                        <asp:TextBox runat="server" ID="txtNoun" CssClass="area-text" TextMode="MultiLine" Rows="5" Width="760px"/>
                    </div>
                <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                --%>
                    <div class="row">
                        <label>Изображение:</label>
                        <telerik:RadAsyncUpload ID="RadUpload1" runat="server" OnFileUploaded="AsyncUpload1_FileUploaded"
                                    MaxFileSize="524288"  AllowedFileExtensions="jpg,png,gif,bmp" OnClientFileUploaded="fileUploaded"
                                    AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                     Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>

                    </div>

                    <div class="row">
                        <telerik:RadBinaryImage runat="server" ID="rbiImage" AutoAdjustImageControlSize="false" Width="90px" Height="110px" />
                    </div>
                    <asp:Panel ID="pFile" Visible="false" runat="server">
                    <div class="row">
                        <label>Дополнительный файл:</label> 
                        <asp:TextBox runat="server" ID="txtFile" CssClass="input-text"/>
                        <asp:LinkButton ID="lbDeleteFile" OnClick="lbDeleteFile_Click" runat="server">Удалить</asp:LinkButton>
                    </div>
                    </asp:Panel> 
                    <asp:Panel ID="pUploadFile" runat="server">
                    </asp:Panel> 
                    <div class="row">
                        <label>Дополнительный файл:</label>
                        <telerik:RadAsyncUpload ID="RadUpload2" runat="server" OnFileUploaded="AsyncUpload2_FileUploaded"
                                    MaxFileSize="524288" OnClientFileUploaded="fileUploaded"
                                    AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                     Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                    </div>
	                <div class="clear"></div>
                    <div class="left-column">
		                <div class="row">
			                <label>Доступ к записи:</label>
                            <asp:DropDownList ID="ddlAccessRecord" runat="server" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlAccessRecord_OnSelectedIndexChanged"/>
		                </div>
                        <asp:Panel runat="server" ID="plCompany" CssClass="row" Visible="false">
                            <label>Компания для доступа:</label>
			                <uc:Company runat="server" ID="ucCompany" />
                        </asp:Panel>
                    </div>
                    <div class="right-column">
		                <div class="row">
			                <label>Доступ для комментирования:</label>
                            <asp:DropDownList ID="ddlAccessComment" runat="server" CssClass="select-text"/>
		                </div>
                    </div>
                    <div class="clear"></div>


                <%--    </ContentTemplate></asp:UpdatePanel>
                --%>	<telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
		                <Tabs>
		                    <telerik:RadTab Text="Текст публикации" />
			                <telerik:RadTab Text="Комментарии" />
			                <telerik:RadTab Text="Отметки" />
			                <telerik:RadTab Text="Связанные записи" />
			                <telerik:RadTab Text="Термины" />
		                </Tabs>
	                </telerik:RadTabStrip>

	                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server" Width="920px">
	                    <telerik:RadPageView runat="server" CssClass="clearfix">
	                        <br/>
	                        <div class="row">
                                <uc:HtmlEditor runat="server" ID="ucHtmlEditor" Width="917px" Height="400px" Module="Publications" />
                            </div>	                        
	                    </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView1" CssClass="clearfix" runat="server">
                            <uc:PublicationComment ID="PublicationComment" runat="server" />                            
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView2" CssClass="clearfix" runat="server">
                            <uc:PublicationMark ID="PublicationMark" runat="server" />
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView3" CssClass="clearfix" runat="server">
                            <uc:RelatedPublication ID="RelatedPublication" runat="server" />
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView4" CssClass="clearfix" runat="server">
                            <uc:PublicationTerms ID="PublicationTerms" runat="server" />
		                </telerik:RadPageView>
	                </telerik:RadMultiPage>
                    <div class="clear"></div><br/>                    
                    <br/>
                    <div class="buttons">
                        <asp:LinkButton ID="BtnUpdate" OnClick="BtnUpdate_Click" CssClass="btn" ValidationGroup="valGroupUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
                        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
                    </div>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>
