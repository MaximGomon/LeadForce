<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SiteEventTemplate.aspx.cs" Inherits="WebCounter.AdminPanel.SiteEventTemplate" %>
<%@ Register TagPrefix="ext" Namespace="WebCounter.BusinessLogicLayer.Controls" Assembly="WebCounter.BusinessLogicLayer" %>
<%@ Register TagPrefix="uc" TagName="SiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SiteActionTemplate.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="breadcrumbs">Оценка и развитие / Оценка по активности</div>

    <div class="row row-event-name">
        <label>Название события:</label>
        <ext:TextBoxExt ID="txtTitle" Required="true" PlaceHolderText="Название события" CssClass="input-text" ValidationGroup="valGroupUpdate" runat="server" />
    </div>

    <telerik:RadTabStrip MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
        <Tabs>
            <telerik:RadTab Text="Условия события" />
            <telerik:RadTab Text="План сообщений" Visible="False" />
            <telerik:RadTab Text="Изменение реквизитов и баллов" />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
        <telerik:RadPageView runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <h3>Условия события</h3>

                    <ext:DropDownListExt ID="ddlLogicCondition" PlaceHolderText="Условие" CssClass="select-text" OnSelectedIndexChanged="ddlLogicCondition_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </ext:DropDownListExt>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlLogicCondition" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                    <telerik:RadNumericTextBox ID="txtFrequencyPeriod" Type="Number" EmptyMessage="Вызывать не чаще, дней" CssClass="input-text" runat="server">
                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFrequencyPeriod" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                    <span ID="spanActionCount" Visible="false" runat="server">
                        <telerik:RadNumericTextBox ID="txtActionCount" Type="Number" EmptyMessage="Количество действий" CssClass="input-text" runat="server">
                            <NumberFormat GroupSeparator="" AllowRounding="false" />
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" ControlToValidate="txtActionCount" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                    </span>
                    <br /><br />

                    <asp:ListView ID="lvSiteEventTemplateActivity"
                                    OnItemCreated="lvSiteEventTemplateActivity_ItemCreated"
                                    OnItemDataBound="lvSiteEventTemplateActivity_ItemDataBound"
                                    OnItemInserting="lvSiteEventTemplateActivity_ItemInserting"
                                    OnItemDeleting="lvSiteEventTemplateActivity_ItemDeleting"
                                    InsertItemPosition="LastItem"
                                    runat="server">
                        <ItemTemplate>
                            <div class="dyn-section">
                                <asp:LinkButton ID="Delete" ClientIDMode="AutoID" CommandName="Delete" CssClass="btn-delete" runat="server"><em>&nbsp;</em><span>-</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlEventCategories" PlaceHolderText="Категория" ClientIDMode="AutoID" SelectedValue='<%# Eval("EventCategoryID") %>' AutoPostBack="true" CssClass="select-text" runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlEventCategories" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <span ID="spanAction" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlActivityTypes" PlaceHolderText="Тип действия" ClientIDMode="AutoID" AutoPostBack="true" OnSelectedIndexChanged="ddlActivityTypes_SelectedIndexChanged" CssClass="select-text" SelectedValue='<%# Eval("ActivityTypeID") ?? "" %>' runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlActivityTypes" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <telerik:RadComboBox runat="server" ID="rcbActivityCode" skin="Labitec" EnableEmbeddedSkins="false" EmptyMessage="URL или код" ValidationGroup="valGroupUpdate" Width="234px">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="" ControlToValidate="rcbActivityCode" ValidationGroup="valGroupUpdate">*</asp:RequiredFieldValidator>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server" InitialValue="URL или код" ControlToValidate="rcbActivityCode" ValidationGroup="valGroupUpdate">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtActualPeriod" Type="Number" EmptyMessage="Период актуальности, дней" CssClass="input-text" Text='<%# Eval("ActualPeriod") %>' runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" ControlToValidate="txtActualPeriod" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanColumnValue" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlSiteColumns" PlaceHolderText="Реквизит" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlSiteColumns_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlSiteColumns" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlFormulaSiteColumns" ClientIDMode="AutoID" PlaceHolderText="Формула" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlFormulaSiteColumns_SelectedIndexChanged" Visible="false" runat="server"></ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="rfvFormulaSiteColumns" ControlToValidate="ddlFormulaSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>

                                    <telerik:RadTextBox ID="txtValueStringSiteColumns" EmptyMessage="Значение" CssClass="input-text" Visible="false" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvValueStringSiteColumns" Display="Dynamic" ControlToValidate="txtValueStringSiteColumns" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlSiteColumnValues" PlaceHolderText="Значение" CssClass="select-text" Visible="false" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvSiteColumnValues" ControlToValidate="ddlSiteColumnValues" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadDatePicker ID="txtValueDateSiteColumns" Width="110" Visible="false" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvValueDateSiteColumns" ControlToValidate="txtValueDateSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueNumberSiteColumns" Type="Number" CssClass="input-text" EmptyMessage="Значение" Visible="false" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rfvValueNumberSiteColumns" ControlToValidate="txtValueNumberSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanScoreByType" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlSiteActivityScoreType" PlaceHolderText="Реквизит" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlSiteActivityScoreType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlFormulaScoreByType" PlaceHolderText="Формула" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlFormulaScoreByType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueScoreByType" Type="Number" EmptyMessage="Значение" CssClass="input-text" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" ControlToValidate="txtValueScoreByType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanScoreByCharacteristics" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlFormulaScoreByCharacteristics" PlaceHolderText="Формула" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlFormulaScoreByCharacteristics" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueScoreByCharacteristics" Type="Number" EmptyMessage="Значение" CssClass="input-text" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" ControlToValidate="txtValueScoreByCharacteristics" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <div class="dyn-section-insert">
                                <asp:LinkButton ID="Insert" ClientIDMode="AutoID" CommandName="Insert" CssClass="btn" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>+</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlEventCategories" PlaceHolderText="Категория" ClientIDMode="AutoID" AutoPostBack="true" CssClass="select-text" runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlEventCategories" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                <span ID="spanAction" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlActivityTypes" ClientIDMode="AutoID" PlaceHolderText="Тип действия" OnSelectedIndexChanged="ddlActivityTypes_SelectedIndexChanged" CssClass="select-text" runat="server" AutoPostBack="true">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlActivityTypes" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" Display="Dynamic" ControlToValidate="ddlActivityTypes" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadComboBox runat="server" ID="rcbActivityCode" skin="Labitec" EnableEmbeddedSkins="false" EmptyMessage="URL или код" ValidationGroup="valGroup" Width="234px">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="" ControlToValidate="rcbActivityCode" ValidationGroup="valGroup">*</asp:RequiredFieldValidator>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" InitialValue="URL или код" ControlToValidate="rcbActivityCode" ValidationGroup="valGroup">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" InitialValue="" ControlToValidate="rcbActivityCode" ValidationGroup="valGroupUpdate">*</asp:RequiredFieldValidator>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" Display="Dynamic" runat="server" InitialValue="URL или код" ControlToValidate="rcbActivityCode" ValidationGroup="valGroupUpdate">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtActualPeriod" Type="Number" EmptyMessage="Период актуальности, дней" CssClass="input-text" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" ControlToValidate="txtActualPeriod" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" ControlToValidate="txtActualPeriod" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanColumnValue" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlSiteColumns" PlaceHolderText="Реквизит" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlSiteColumns_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlSiteColumns" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" Display="Dynamic" ControlToValidate="ddlSiteColumns" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlFormulaSiteColumns" ClientIDMode="AutoID" PlaceHolderText="Формула" CssClass="select-text" AutoPostBack="true" OnSelectedIndexChanged="ddlFormulaSiteColumns_SelectedIndexChanged" Visible="false" runat="server"></ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="rfvFormulaSiteColumns" ControlToValidate="ddlFormulaSiteColumns" ValidationGroup="valGroup" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvFormulaSiteColumns2" Display="Dynamic" ControlToValidate="ddlFormulaSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtValueStringSiteColumns" EmptyMessage="Значение" CssClass="input-text" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvValueStringSiteColumns" ControlToValidate="txtValueStringSiteColumns" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <asp:RequiredFieldValidator ID="rfvValueStringSiteColumns2" Display="Dynamic" ControlToValidate="txtValueStringSiteColumns" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlSiteColumnValues" PlaceHolderText="Значение" CssClass="select-text" Visible="false" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvSiteColumnValues" ControlToValidate="ddlSiteColumnValues" ValidationGroup="valGroup" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvSiteColumnValues2" Display="Dynamic" ControlToValidate="ddlSiteColumnValues" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadDatePicker ID="txtValueDateSiteColumns" Width="110" Visible="false" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvValueDateSiteColumns" ControlToValidate="txtValueDateSiteColumns" ValidationGroup="valGroup" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvValueDateSiteColumns2" Display="Dynamic" ControlToValidate="txtValueDateSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueNumberSiteColumns" Type="Number" CssClass="input-text" Visible="false" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rfvValueNumberSiteColumns" ControlToValidate="txtValueNumberSiteColumns" ValidationGroup="valGroup" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvValueNumberSiteColumns2" ControlToValidate="txtValueNumberSiteColumns" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanScoreByType" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlSiteActivityScoreType" PlaceHolderText="Реквизит" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ddlSiteActivityScoreType" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" Display="Dynamic" ControlToValidate="ddlSiteActivityScoreType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <ext:DropDownListExt ID="ddlFormulaScoreByType" PlaceHolderText="Формула" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="ddlFormulaScoreByType" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" Display="Dynamic" ControlToValidate="ddlFormulaScoreByType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueScoreByType" Type="Number" EmptyMessage="Значение" CssClass="input-text" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" ControlToValidate="txtValueScoreByType" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" Display="Dynamic" ControlToValidate="txtValueScoreByType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                                <span ID="spanScoreByCharacteristics" Visible="false" runat="server">
                                    <ext:DropDownListExt ID="ddlFormulaScoreByCharacteristics" PlaceHolderText="Формула" CssClass="select-text" runat="server">
                                    </ext:DropDownListExt>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="ddlFormulaScoreByCharacteristics" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" Display="Dynamic" ControlToValidate="ddlFormulaScoreByCharacteristics" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txtValueScoreByCharacteristics" Type="Number" EmptyMessage="Значение" CssClass="input-text" runat="server">
                                        <NumberFormat GroupSeparator="" AllowRounding="false" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" ControlToValidate="txtValueScoreByCharacteristics" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" Display="Dynamic" ControlToValidate="txtValueScoreByCharacteristics" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                </span>
                            </div>
                        </InsertItemTemplate>
                    </asp:ListView>            
                </ContentTemplate>
            </asp:UpdatePanel>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server">
            <telerik:RadAjaxPanel runat="server">
                <h3>План сообщений</h3>                    
                <asp:ListView ID="lvSiteEventActionTemplate"
                                OnItemCreated="lvSiteEventActionTemplate_ItemCreated"
                                OnItemInserting="lvSiteEventActionTemplate_ItemInserting"
                                OnItemDeleting="lvSiteEventActionTemplate_ItemDeleting"
                                InsertItemPosition="LastItem"
                                runat="server">
                    <ItemTemplate>
                        <div class="dyn-section">
                            <asp:LinkButton ID="Delete" ClientIDMode="AutoID" CommandName="Delete" CssClass="btn-delete" runat="server"><em>&nbsp;</em><span>-</span></asp:LinkButton>
                            <%--<ext:DropDownListExt ID="ddlSiteActionTemplates" PlaceHolderText="Сообщение" CssClass="select-text" SelectedValue='<%# Eval("SiteActionTemplateID") ?? "" %>' runat="server">
                            </ext:DropDownListExt>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="ddlSiteActionTemplates" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>--%>
                                
                            <uc:SiteActionTemplate runat="server" ID="ucSiteActionTemplate" CurrentSiteActionTemplateCategory="Event" ShowLabel="false" SelectedSiteActionTemplateId='<%# Eval("SiteActionTemplateID") %>' ValidationGroup="valGroupUpdate" />

                            <telerik:RadNumericTextBox ID="txtStartAfter" Type="Number" EmptyMessage="Запланировать через" CssClass="input-text" Text='<%# Eval("StartAfter") %>' runat="server">
                                <NumberFormat GroupSeparator="" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" ControlToValidate="txtStartAfter" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            <ext:DropDownListExt ID="ddlStartAfterType" PlaceHolderText="Период" CssClass="select-text" SelectedValue='<%# Eval("StartAfterTypeID") ?? "" %>' runat="server">
                            </ext:DropDownListExt>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="ddlStartAfterType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            <div class="clear"></div>                                
                        </div>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <div class="dyn-section-insert">
                            <asp:LinkButton ID="Insert" ClientIDMode="AutoID" CommandName="Insert" CssClass="btn" ValidationGroup="valGroupAction" runat="server"><em>&nbsp;</em><span>+</span></asp:LinkButton>
                            <%--<ext:DropDownListExt ID="ddlSiteActionTemplates" AutoPostBack="true" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlSiteActionTemplates_OnSelectedIndexChanged" PlaceHolderText="Сообщение" CssClass="select-text" runat="server">
                            </ext:DropDownListExt>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="ddlSiteActionTemplates" ValidationGroup="valGroupAction" runat="server">*</asp:RequiredFieldValidator>--%>

                            <uc:SiteActionTemplate runat="server" ID="ucSiteActionTemplate" CurrentSiteActionTemplateCategory="Event" ShowLabel="false" ValidationGroup="valGroupAction" OnSelectedTemplateChanged="ucSiteActionTemplate_OnSelectedTemplateChanged" />

                            <telerik:RadNumericTextBox ID="txtStartAfter" Type="Number" Display="false" EmptyMessage="Запланировать через" CssClass="input-text" runat="server">
                                <NumberFormat GroupSeparator="" AllowRounding="false" />
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" ControlToValidate="txtStartAfter" Enabled="false" ValidationGroup="valGroupAction" runat="server">*</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator47" ControlToValidate="txtStartAfter" Enabled="false" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            <ext:DropDownListExt ID="ddlStartAfterType" PlaceHolderText="Период" style="display:none" CssClass="select-text" runat="server" >
                            </ext:DropDownListExt>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="ddlStartAfterType" Enabled="false" ValidationGroup="valGroupAction" runat="server">*</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator51" ControlToValidate="ddlStartAfterType" Enabled="false" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            <div class="clear"></div>                                
                        </div>                            
                    </InsertItemTemplate>
                </asp:ListView>
            </telerik:RadAjaxPanel>                
        </telerik:RadPageView>
        <telerik:RadPageView runat="server">
            <h3>Изменение реквизитов посетителя</h3>
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:ListView ID="lvSiteActionTemplateUserColumn"
                                    OnItemCreated="lvSiteActionTemplateUserColumn_ItemCreated"
                                    OnItemDataBound="lvSiteActionTemplateUserColumn_ItemDataBound"
                                    OnItemInserting="lvSiteActionTemplateUserColumn_ItemInserting"
                                    OnItemDeleting="lvSiteActionTemplateUserColumn_ItemDeleting"
                                    InsertItemPosition="LastItem"
                                    runat="server">
                        <ItemTemplate>
                            <div class="dyn-section">
                                <asp:LinkButton ID="Delete" ClientIDMode="AutoID" CommandName="Delete" CssClass="btn-delete" runat="server"><em>&nbsp;</em><span>-</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlSiteColumns" PlaceHolderText="Реквизит" OnSelectedIndexChanged="ddlSiteColumns_UserColumn_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" SelectedValue='<%# Eval("SiteColumnID") ?? "" %>' runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="ddlSiteColumns" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadTextBox ID="txtValue" EmptyMessage="Новое значение" CssClass="input-text" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                <ext:DropDownListExt ID="ddlSiteColumnValues" PlaceHolderText="Новое значение" CssClass="select-text" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvSiteColumnValues" ControlToValidate="ddlSiteColumnValues" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadDatePicker ID="txtValueDate" Width="110" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvValueDate" ControlToValidate="txtValueDate" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadNumericTextBox ID="txtValueNumber" Type="Number" CssClass="input-text" Visible="false" runat="server">
                                    <NumberFormat GroupSeparator="" AllowRounding="false" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvValueNumber" ControlToValidate="txtValueNumber" ValidationGroup="valGroupUpdate" Visible="false" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <div class="dyn-section-insert">
                                <asp:LinkButton ID="Insert" ClientIDMode="AutoID" CommandName="Insert" CssClass="btn" ValidationGroup="valGroupColumns" runat="server"><em>&nbsp;</em><span>+</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlSiteColumns"  ClientIDMode="AutoID" PlaceHolderText="Реквизит" OnSelectedIndexChanged="ddlSiteColumns_UserColumn_SelectedIndexChanged" AutoPostBack="true" CssClass="select-text" runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="ddlSiteColumns" ValidationGroup="valGroupColumns" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadTextBox ID="txtValue" EmptyMessage="Новое значение" CssClass="input-text" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="txtValue" ValidationGroup="valGroupColumns" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvValue2" ControlToValidate="txtValue" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <ext:DropDownListExt ID="ddlSiteColumnValues" PlaceHolderText="Новое значение" CssClass="select-text" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvSiteColumnValues" ControlToValidate="ddlSiteColumnValues" ValidationGroup="valGroupColumns" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSiteColumnValues2" ControlToValidate="ddlSiteColumnValues" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadDatePicker ID="txtValueDate" Width="110" Visible="false" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvValueDate" ControlToValidate="txtValueDate" ValidationGroup="valGroupColumns" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvValueDate2" ControlToValidate="txtValueDate" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadNumericTextBox ID="txtValueNumber" Type="Number" CssClass="input-text" Visible="false" runat="server">
                                    <NumberFormat GroupSeparator="" AllowRounding="false" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvValueNumber" ControlToValidate="txtValueNumber" ValidationGroup="valGroupColumns" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvValueNumber2" ControlToValidate="txtValueNumber" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </InsertItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br /><br /><br />

            <h3>Начисление баллов</h3>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:ListView ID="lvSiteEventTemplateScore"
                                    OnItemCreated="lvSiteEventTemplateScore_ItemCreated"
                                    OnItemInserting="lvSiteEventTemplateScore_ItemInserting"
                                    OnItemDeleting="lvSiteEventTemplateScore_ItemDeleting"
                                    InsertItemPosition="LastItem"
                                    runat="server">
                        <ItemTemplate>
                            <div class="dyn-section">
                                <asp:LinkButton ID="Delete" ClientIDMode="AutoID" CommandName="Delete" CssClass="btn-delete" runat="server"><em>&nbsp;</em><span>-</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlSiteActivityScoreType" PlaceHolderText="Тип балла" CssClass="select-text" SelectedValue='<%# Eval("SiteActivityScoreTypeID") ?? "" %>' runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="ddlSiteActivityScoreType" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <ext:DropDownListExt ID="ddlOperation" PlaceHolderText="Операция" CssClass="select-text" SelectedValue='<%# Eval("OperationID") ?? "" %>' runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="ddlOperation" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadNumericTextBox ID="txtScore" Type="Number" CssClass="input-text" EmptyMessage="Начисляемый балл" Text='<%# Eval("Score") %>' runat="server">
                                    <NumberFormat GroupSeparator="" AllowRounding="false" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" ControlToValidate="txtScore" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                        <InsertItemTemplate>
                            <div class="dyn-section-insert">
                                <asp:LinkButton ID="Insert" ClientIDMode="AutoID" CommandName="Insert" CssClass="btn" ValidationGroup="valGroupScore" runat="server"><em>&nbsp;</em><span>+</span></asp:LinkButton>
                                <ext:DropDownListExt ID="ddlSiteActivityScoreType" AutoPostBack="true" ClientIDMode="AutoID" OnSelectedIndexChanged="ddlSiteActivityScoreType_OnSelectedIndexChanged" PlaceHolderText="Тип балла" CssClass="select-text" runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="ddlSiteActivityScoreType" ValidationGroup="valGroupScore" runat="server">*</asp:RequiredFieldValidator>
                                <ext:DropDownListExt ID="ddlOperation" PlaceHolderText="Операция" CssClass="select-text" Visible="false" runat="server">
                                </ext:DropDownListExt>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="ddlOperation" ValidationGroup="valGroupScore" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator47" ControlToValidate="ddlOperation" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                                <telerik:RadNumericTextBox ID="txtScore" Type="Number" CssClass="input-text" EmptyMessage="Начисляемый балл" Visible="false" runat="server">
                                    <NumberFormat GroupSeparator="" AllowRounding="false" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" ControlToValidate="txtScore" ValidationGroup="valGroupScore" runat="server">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" ControlToValidate="txtScore" Display="Dynamic" ValidationGroup="valGroupUpdate" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </InsertItemTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <br />

    <div class="buttons">
        <asp:LinkButton ID="BtnUpdate" OnClick="BtnUpdate_Click" CssClass="btn" ValidationGroup="valGroupUpdate" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
        <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
    </div>
    <script type="text/javascript">        
        function pageLoad() {
            $("input.readonly").each(function () {
                $(this).attr("readonly", "readonly");
            });
        }
    </script>
</asp:Content>