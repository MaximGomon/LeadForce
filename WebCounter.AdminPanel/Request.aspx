<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="WebCounter.AdminPanel.Request" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="AssignToRequirements" Src="~/UserControls/Requirement/AssignToRequirements.ascx" %>
<%@ Register TagPrefix="uc" TagName="RegisterComment" Src="~/UserControls/Requirement/RegisterComment.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/UserControls/Shared/ContentComments.ascx" %>
<%@ Register Assembly="DirtyPageReminder" Namespace="DingJing" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<cc1:DirtyPageReminder ID="dprReminder" runat="server" AlertMessage="Вы действительно хотите покинуть страницу? Вы можете потерять часть ваших данных." />
<telerik:RadScriptBlock runat="server">    
    <script type="text/javascript">             
        function EnableDisableEditor() {                        
            if (<%= dcbRequestSource.ClientID %>_SelectedValue() == "")
                <%= ucLongDescription.ClientID %>_EnableEditor(true);
            else
                <%= ucLongDescription.ClientID %>_EnableEditor(false);            
        }

        function GetSelectedText() {
            var text = <%= ucLongDescription.ClientID %>_GetSelection();            
            $("#hfSelection").val(htmlEncode(text));
        }
        function htmlEncode(value){ return $('<div/>').text(value).html();}
        function htmlDecode(value){ return $('<div/>').html(value).text();}
    </script>

</telerik:RadScriptBlock>    
<asp:HiddenField runat="server" ID="hfSelection" ClientIDMode="Static" />
<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <div class="two-columns">
	                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
						                ValidationGroup="groupRequest" />
	                <div class="left-column">
		                <div class="row">
			                <label>Номер:</label>				
			                <asp:Literal runat="server" ID="lrlNumber"/>
		                </div>
                    </div>
                    <div class="right-column">		    
                        <div style="position: relative" class="row clearfix">
			                <label>Дата регистрации:</label>			
			                <telerik:RadDatePicker runat="server" MinDate="01.01.1900" Enabled="false" CssClass="date-picker" ID="rdpCreatedAt" ShowPopupOnFocus="true" Width="110px">
				                <DateInput Enabled="false" />
				                <DatePopupButton Enabled="false" />
			                </telerik:RadDatePicker>			
		                </div>
                    </div>
                    <div class="clear"></div>    
                    <telerik:RadAjaxPanel ID="RadAjaxPanel" runat="server">
                        <div class="row">
                            <label>Состояние запроса</label>
                            <span style="vertical-align: middle"><asp:Literal runat="server" ID="lrlRequestStatus" /></span>
                            <telerik:RadButton ID="rbtnInWork" runat="server" Skin="Windows7" Text="В работу" CommandArgument="InWork" OnClick="rbtnStatus_OnClick" />
                            <telerik:RadButton ID="rbtnDuplicate" runat="server" Skin="Windows7" Text="Дубль" CommandArgument="Duplicate" OnClick="rbtnStatus_OnClick" />
                            <telerik:RadButton ID="rbtnClose" runat="server" Skin="Windows7" Text="Закрыть" CommandArgument="Closed" OnClick="rbtnStatus_OnClick" />
                        </div>            
                        <div class="left-column">
                            <div class="row">
                                <label>Уровень обслуживания</label>
                                <uc:DictionaryOnDemandComboBox ID="dcbServiceLevel" AutoPostBack="true" OnSelectedIndexChanged="dcbServiceLevel_OnSelectedIndexChanged" ValidationGroup="groupRequest" ValidationErrorMessage="Вы не выбрали уровень обслуживания"  DictionaryName="tbl_ServiceLevel" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                            </div>
                        </div>
                        <div class="right-column">
                            <div class="row">
                                <label>Ответственный</label>
                                <uc:ContactComboBox ID="ucResponsible" CssClass="select-text" runat="server" FilterByFullName="true"/>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </telerik:RadAjaxPanel>
                    <div class="left-column">
                        <div class="row">
                            <label>Дата реакции, план</label>
                            <telerik:RadDateTimePicker runat="server" MinDate="01.01.1900" Enabled="false" CssClass="datetime-picker" ID="rdpReactionDatePlanned" ShowPopupOnFocus="true">
				                <DateInput Enabled="false" />
				                <DatePopupButton Enabled="false" />
			                </telerik:RadDateTimePicker>
                        </div>        
	                </div>
	                <div class="right-column">        
                        <div class="row">
                            <label>Дата реакции, факт</label>
                            <telerik:RadDateTimePicker runat="server" Enabled="false" MinDate="01.01.1900" CssClass="datetime-picker" ID="rdpReactionDateActual" ShowPopupOnFocus="true">
				                <DateInput Enabled="false" />
				                <DatePopupButton Enabled="false" />
			                </telerik:RadDateTimePicker>
                        </div>
	                </div>
                    <div class="clearfix"></div>
                    <div class="row">
		                <label>Запрос кратко:</label>
                        <asp:TextBox runat="server" ID="txtShortDescription" ClientIDMode="Static" MaxLength="2048" CssClass="area-text" Width="640px" Height="30px" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtShortDescription" Text="*" ErrorMessage="Вы не ввели суть запроса" ValidationGroup="groupRequest" runat="server" />        
                        <asp:CustomValidator ID="CustomValidator1" runat="server" Text="*" ErrorMessage="Длина сути запроса должна быть не более 2048 символов" ControlToValidate="txtShortDescription" ValidationGroup="groupRequest"
	                            SetFocusOnError="true" ClientValidationFunction="CheckMaxLength" ></asp:CustomValidator>
                        <script type="text/javascript">
                            function CheckMaxLength(sender, args) { if (args.Value.length >= 2048) args.IsValid = false; }
                        </script>
                    </div>
                    <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server" OnClientTabSelected="EnableDisableEditor">
		                <Tabs>
		                    <telerik:RadTab Text="Суть запроса" />
			                <telerik:RadTab Text="Источник запроса" />
                            <telerik:RadTab Text="Требования" />
                            <telerik:RadTab Text="История запроса" />
		                </Tabs>
	                </telerik:RadTabStrip>
	                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">		
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div class="row">
                                <label>Запрос подробно</label>
                                <br/><br/>
                                <uc:HtmlEditor runat="server" ID="ucLongDescription" IsDirtyEnabled="true" Width="940px" Height="300px" Module="Requests" />
                            </div>            
                            <br/>
                            <ul class="buttons-list clearfix">
                                <li><asp:LinkButton ID="lbtnCreateRequirement" Visible="false" OnClientClick="GetSelectedText();" OnClick="lbtnCreateRequirement_OnClick" CssClass="btn" runat="server"><em>&nbsp;</em><span>Создать требование</span></asp:LinkButton></li>
                                <li><uc:AssignToRequirements runat="server" ID="ucAssignToRequiremts" /></li>
                                <li><uc:RegisterComment runat="server" ID="ucRegisterComment" /></li>
                            </ul>            
            
                            <br/><br/>
                            <h3>Файлы источника</h3>
                            <div class="row row-normal">
                                <asp:Literal runat="server" ID="lrlSourceFiles" Text="Файлы отсутствуют" />
                            </div>
                            <h3>Файлы запроса</h3>
                            <div class="row row-normal">
                                <asp:Literal runat="server" ID="lrlRequestFiles" Text="Файлы отсутствуют" />
                            </div>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" CssClass="row">
                                <label>Файл:</label>
                                <telerik:RadAsyncUpload ID="rauRequestFiles" Visible="false" Width="250px" runat="server" MaxFileSize="5242880" AutoAddFileInputs="true"
                                Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                                <telerik:RadButton ID="rbtnAddFile" runat="server" Skin="Windows7" Text="Добавить файл" OnClick="rbtnAddFile_OnClick" />
                            </telerik:RadAjaxPanel>            
                            <asp:Panel runat="server" ID="plComments">
                                <uc:ContentComments runat="server" ID="ucContentComments" CommentType="tbl_RequestComment" EnableHtmlCommentEditor="true" />
                                <br/>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">		    
                            <div class="left-column">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
                                    <div class="row">
                                        <label>Тип запроса</label>
                                        <uc:DictionaryComboBox ID="dcbRequestSourceType" AutoPostBack="true" OnSelectedIndexChanged="dcbRequestSourceType_OnSelectedIndexChanged" DictionaryName="tbl_RequestSourceType" ValidationGroup="groupRequest" ValidationErrorMessage="Вы не выбрали тип источника" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                    <div class="row">
                                        <label>Источник</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbRequestSource" AutoPostBack="true" OnSelectedIndexChanged="dcbRequestSource_OnSelectedIndexChanged" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                </telerik:RadAjaxPanel>
                                <div class="row">
                                    <label>Продукт</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbProducts"  DictionaryName="tbl_Product" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                            </div>
                            <div class="right-column">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server">
                                    <div class="row">
                                        <label>Компания</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbCompany" AutoPostBack="True" OnSelectedIndexChanged="dcbCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                    <div class="row">
                                        <label>Контакт</label>
                                        <uc:ContactComboBox ID="ucContact" CssClass="select-text" runat="server" FilterByFullName="true" AutoPostBack="true" OnSelectedIndexChanged="ucContact_OnSelectedIndexChanged"/>
                                    </div>
                                </telerik:RadAjaxPanel>
                                <div class="row">
                                    <label>Серийный номер</label>
                                    <asp:TextBox runat="server" ID="txtProductSeriesNumber" CssClass="input-text" />
                                </div>
                            </div>
                            <div class="clear"></div>                        
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">            
                            <labitec:Grid ID="gridRequirements" TableName="tbl_Requirement" ClassName="WebCounter.AdminPanel.RequestRequirements" Fields="tbl_Contact.ID, tbl_Company.ID" Export="true" runat="server" OnItemDataBound="gridRequirements_OnItemDataBound">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn2" DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>            
                                    <labitec:GridColumn ID="GridColumn3" DataField="tbl_RequirementStatus.Title" HeaderText="Состояние требования" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn4" DataField="ShortDescription" HeaderText="Суть требования" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn5" DataField="tbl_RequestSourceType.Title" HeaderText="Тип запроса" runat="server" />
                                    <labitec:GridColumn ID="GridColumn6" DataField="tbl_Request.Number" HeaderText="Номер запроса" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn7" DataField="tbl_Company.Name" HeaderText="Компания" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlCompanyName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn8" DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn9" DataField="tbl_ServiceLevel.Title" HeaderText="Уровень обслуживания" runat="server"/>
                                </Columns>    
                                <Joins>        
                                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RequirementStatus" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_ServiceLevel" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ServiceLevelID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="CompanyID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ContactID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_Request" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin7" JoinTableName="tbl_RequestToRequirement" JoinTableKey="RequirementID" TableName="tbl_Requirement" TableKey="ID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin6" JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestSourceTypeID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server">
                            <labitec:Grid ID="gridRequestHistory" OnItemDataBound="gridRequestHistory_OnItemDataBound" Toolbar="false" TableName="tbl_RequestHistory" Fields="tbl_Contact.ID, c1.ID" ClassName="WebCounter.AdminPanel.RequestHistory" runat="server">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn10" DataField="CreatedAt" HeaderText="Дата изменения" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn11" DataField="tbl_Contact.UserFullName" HeaderText="Автор изменения" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn12" DataField="c1.UserFullName" HeaderText="Ответственный" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlResponsibleUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>                        
                                    <labitec:GridColumn ID="GridColumn13" DataField="RequestStatusID" HeaderText="Состояние запроса" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlRequestStatus" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                </Columns>
                                <Joins>                        
                                    <labitec:GridJoin ID="GridJoin9" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_RequestHistory" TableKey="ContactID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin10" JoinTableName="tbl_Contact" JoinTableAs="c1" JoinTableKey="ID" TableKey="ResponsibleID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <br/>
	                <div class="buttons">
		                <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupRequest" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                </div>
        </td>
    </tr>
</table>
</asp:Content>
