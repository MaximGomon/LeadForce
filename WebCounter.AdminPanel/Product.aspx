<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebCounter.AdminPanel.Product" %>
<%@ Register TagPrefix="labitec" Namespace="Labitec.UI.Photo.Controls" Assembly="Labitec.UI.Photo" %>
<%@ Register TagPrefix="uc" TagName="SelectCategoryControl" Src="~/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProductComplectation" Src="~/UserControls/ProductComplectation.ascx" %>
<%@ Register TagPrefix="uc" TagName="ProductPrice" Src="~/UserControls/ProductPrice.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
<script src="<%# ResolveUrl("~/Scripts/Common.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <table width="100%">
        <tr valign="top">
            <td width="195px">
                <div class="aside">
                    <telerik:RadPanelBar ID="RadPanelBar1" Width="189px" Skin="Windows7" runat="server">
                        <Items>
                            <telerik:RadPanelItem Expanded="true" Text="Теги">
                                <ContentTemplate>
                                    <labitec:Tags ID="tagsProduct" ObjectTypeName="tbl_Product" runat="server" />
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
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
                        <label>Наименование:</label>
                        <asp:TextBox runat="server" ID="txtTitle" CssClass="input-text"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите наименование'/>
                    </div>
                    <div class="left-column">
		                <div class="row">
			                <label>Артикул:</label>
                            <asp:TextBox runat="server" ID="txtSKU" CssClass="input-text" />
		                </div>
		                <div class="row">
			                <label>Категория:</label>
                            <uc:SelectCategoryControl runat="server" ID="sccCategory" ShowEmpty="true" CssClass="select-text" />
		                </div>
		                <div class="row">
			                <label>Тип:</label>
			                <asp:DropDownList runat="server" ID="ddlProductType" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_OnSelectedIndexChanged" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProductType" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите тип'/>
		                </div>
		                <div class="row">
			                <label>Базовая цена:</label>
			                <asp:TextBox runat="server" ID="txtPrice" CssClass="input-text"/>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtPrice" Operator="DataTypeCheck" Type="Double" CssClass="input-text" ValidationGroup="valGroupUpdate" ErrorMessage='Неверный формат базовай цены' Text="*"  />
		                </div>
	                </div>
	                <div class="right-column">
		                <div class="row">
			                <label>Статус:</label>            
			                <asp:DropDownList runat="server" ID="ddlProductStatus" CssClass="select-text" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProductStatus" CssClass="input-text" ValidationGroup="valGroupUpdate" Text="*" ErrorMessage='Укажите статус'/>
  		                </div>
		                <div class="row">
			                <label>Бренд:</label>
			                <asp:DropDownList runat="server" ID="ddlBrand" CssClass="select-text" />
		                </div>
		                <div class="row">
			                <label>Единица измерения:</label>
			                <asp:DropDownList runat="server" ID="ddlUnit" CssClass="select-text" />
		                </div>
		                <div class="row">
			                <label>Базовая себестоимость:</label>
			                <asp:TextBox runat="server" ID="txtWholesalePrice" CssClass="input-text"/>
                            <asp:CompareValidator ID="cvtxtRetailPrice" runat="server" Display="Dynamic" ControlToValidate="txtWholesalePrice" Operator="DataTypeCheck" Type="Double" CssClass="input-text" ValidationGroup="valGroupUpdate" ErrorMessage='Неверный формат базовай себестоимость' Text="*"  />
		                </div>
	                </div>
	                <div class="clear"></div>

	                <telerik:RadTabStrip MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" ID="radTabs">
		                <Tabs>
			                <telerik:RadTab Text="Основные данные" />
			                <telerik:RadTab Text="Цены и акции" />
			                <telerik:RadTab Text="Фото" />
			                <telerik:RadTab Text="Комплектация" Value="ProductComplectation" />
		                </Tabs>
	                </telerik:RadTabStrip>

	                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
		                <telerik:RadPageView ID="RadPageView1" runat="server">
			                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
				                <ContentTemplate>
					                <h3>Происхождение</h3>
					                <div class="left-column">
						                <div class="row">
							                <label>Основной поставщик:</label>
                                            <uc:DictionaryComboBox ID="dcbSupplier" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server" />
						                </div>
						                <div class="row">
							                <label>Производитель:</label>
                                            <uc:DictionaryComboBox ID="dcbManufacturer" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
						                </div>
					                </div>
					                <div class="right-column">
						                <div class="row">
							                <label>Артикул поставщика:</label>
                                            <asp:TextBox runat="server" ID="txtSupplierSKU" CssClass="input-text" />
						                </div>
						                <div class="row">
							                <label>Страна производства:</label>
                                            <uc:DictionaryComboBox ID="dcbCountry" DictionaryName="tbl_Country" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server" />
						                </div>
					                </div>
					                <div class="clear"></div>
                                    <h3>Описание</h3>
                                    <div class="row">
                                         <uc:HtmlEditor runat="server" ID="ucHtmlEditor" Width="940px" Height="400px" Module="Products" />
                                    </div>
					                <div class="clear"></div>
				                </ContentTemplate>
			                </asp:UpdatePanel>
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView2" CssClass="clearfix" runat="server">
                            <uc:ProductPrice ID="ProductPrice" runat="server" />
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView3" CssClass="clearfix" runat="server">
                            <labitec:Gallery runat="server" ID="labitecImageGallery" />
                            <labitec:UploadPhoto ID="labitecUploadPhoto" runat="server" />
		                </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView4" CssClass="clearfix" runat="server">
                            <uc:ProductComplectation ID="ProductComplectation" runat="server" />
		                </telerik:RadPageView>
	                </telerik:RadMultiPage>
	                <div class="clear"></div>
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
