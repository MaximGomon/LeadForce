<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="WebCounter.AdminPanel.Requirement" %>
<%@ Register Assembly="DirtyPageReminder" Namespace="DingJing" TagPrefix="cc1" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Configuration" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="DictionaryOnDemandComboBox" Src="~/UserControls/Shared/DictionaryOnDemandComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectCategory" Src="~/UserControls/SelectCategory.ascx" %>
<%@ Register TagPrefix="uc" TagName="RequirementStatus" Src="~/UserControls/Requirement/RequirementStatus.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/UserControls/Shared/ContentComments.ascx" %>
<%@ Register TagPrefix="uc" TagName="SearchResultContainer" Src="~/UserControls/Shared/SearchResultContainer.ascx" %>
<%@ Register TagPrefix="uc" TagName="ParentRequirement" Src="~/UserControls/Requirement/ParentRequirement.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/UserControls/Shared/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
    <cc1:DirtyPageReminder ID="dprReminder" runat="server" AlertMessage="Вы действительно хотите покинуть страницу? Вы можете потерять часть ваших данных." />
<telerik:RadCodeBlock runat="server">
    <script src="<%= ResolveUrl("~/Scripts/jquery.tmpl.js")%>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/Search.js")%>" type="text/javascript"></script>    
</telerik:RadCodeBlock>
<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function ShowChooseAnswerRadWindow() {
            $('#txtSearch').val($('#txtShortDescription').val());
            $find('<%= chooseAnswer.ClientID %>').show();
            leadForceSearch.input($('#txtShortDescription').val());
        }        
        function ShowCloseRequestRadWindow() {
            $(window).load(function () {
                $find('<%= rwCloseRequest.ClientID %>').show();
            });            
        }
        function CloseCloseRequestRadWindow() {
            $find('<%= rwCloseRequest.ClientID %>').close();
        }
        function RedirectToRequirements(arg) { window.location = '<%= UrlsData.AP_Requirements() %>'; }
        function Blur(sender, args) {
            $find("<%= ajaxPanel.ClientID%>").ajaxRequestWithTarget("<%= ajaxPanel.UniqueID %>", sender.get_id());
        }        
        function KeyUp(e, value) {            
            var code = e.keyCode ? e.keyCode : e.charCode;
            var evtobj = window.event ? event : e;
            if (code != 13 && !evtobj.altKey && !evtobj.ctrlKey && !evtobj.shiftKey)
                leadForceSearch.input(value);
        }
    </script>    
</telerik:RadScriptBlock>
<telerik:RadWindowManager ID="radWindowManager" Skin="Windows7" runat="server" EnableShadow="true"></telerik:RadWindowManager>
    
<telerik:RadWindow runat="server" Title="Подобрать ответ" Width="780px" Height="410px" ID="chooseAnswer" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner">
            <div class="search-text-container">
                <asp:TextBox ID="txtSearch" ClientIDMode="Static" Width="98%" runat="server" autocomplete="off" onkeyup="KeyUp(event, this.value);" CssClass="input-text" />
            </div>
            <uc:SearchResultContainer ID="SearchResultContainer1" runat="server" PublicationKind="KnowledgeBase" IsSelectAnswer="true" />
            <div class="clear"></div>
            <br/>
            <div class="buttons clearfix">			    
			    <asp:HyperLink runat="server" ID="HyperLink1" CssClass="cancel" NavigateUrl="javascript:;" Text="Отмена" onclick="CloseChooseAnswerRadWindow();" />
		    </div>
         </div> 
    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadWindow runat="server" Title="Закрыть запросы" Width="780px" Height="410px" ID="rwCloseRequest" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="radwindow-popup-inner bottom-buttons">
            <h4>Закрыть связанные запросы?</h4>
            <asp:CheckBoxList runat="server" ID="chxRequestsList" />
            <div class="buttons">
                <asp:LinkButton ID="lbtnCloseRequests" OnClick="lbtnCloseRequests_OnClick" CssClass="btn" CausesValidation="true" runat="server"><em>&nbsp;</em><span>Закрыть запросы</span></asp:LinkButton>
                <asp:HyperLink runat="server" ID="hlCancelCloseRequests" CssClass="cancel" Text="Отмена" />
            </div>
         </div> 
    </ContentTemplate>
</telerik:RadWindow>

<table width="100%">
    <tr valign="top">
        <td width="195px">
            <div class="aside">
                <uc:LeftColumn runat="server" />
            </div>
        </td>
        <td>
            <div class="two-columns requirement">
	                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						                CssClass="validation-summary"
						                runat="server"
						                EnableClientScript="true"
						                HeaderText="Заполните все поля корректно:"
						                ValidationGroup="groupRequirement" />
                    <div class="left-column">
		                <div class="row">
			                <label>Номер:</label>				
			                <asp:Literal runat="server" ID="lrlNumber"/>
		                </div>		
	                </div>
	                <div class="right-column">		    
                        <div class="row row-relative clearfix">
			                <label>Дата регистрации:</label>			
			                <telerik:RadDatePicker runat="server" MinDate="01.01.1900" Enabled="false" CssClass="date-picker" ID="rdpCreatedAt" ShowPopupOnFocus="true" Width="110px">
				                <DateInput Enabled="false" />
				                <DatePopupButton Enabled="false" />
			                </telerik:RadDatePicker>			
		                </div>
	                </div>
                    <div class="clearfix"></div>    
                    <uc:RequirementStatus runat="server" ID="ucRequirementStatus" />
                    <div class="row">
                        <label>Ответственный</label>
                        <uc:ContactComboBox ID="ucResponsible" CssClass="select-text" runat="server" FilterByFullName="true"/>
                    </div>
                    <div class="left-column">                
                        <div class="row">
                            <label>Дата реализации, план</label>
                            <telerik:RadDateTimePicker runat="server" MinDate="01.01.1900" Enabled="true" CssClass="datetime-picker" ID="rdpRealizationDatePlanned" ShowPopupOnFocus="true">
				                <DateInput Enabled="true" />
				                <DatePopupButton Enabled="true" />
			                </telerik:RadDateTimePicker>
                        </div>
                    </div>
                    <div class="right-column">                
                        <div class="row">
                            <label>Дата реализации, факт</label>
                            <telerik:RadDateTimePicker runat="server" MinDate="01.01.1900" CssClass="datetime-picker" ID="rdpRealizationDateActual" ShowPopupOnFocus="true">
				                <DateInput Enabled="true" />
				                <DatePopupButton Enabled="true" />
			                </telerik:RadDateTimePicker>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <telerik:RadTabStrip ID="RadTabStrip1" MultiPageID="RadMultiPage1" CausesValidation="false" SelectedIndex="0" runat="server">
		                <Tabs>
		                    <telerik:RadTab Text="Суть требования" />            
			                <telerik:RadTab Text="Классификация" />
		                    <telerik:RadTab Text="Отзыв клиента" />
                            <telerik:RadTab Text="Оценка и оплата" />            
                            <telerik:RadTab Text="История требования" />
		                </Tabs>
	                </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div class="row">
		                        <label>Суть требования:</label>
                                <asp:TextBox runat="server" ID="txtShortDescription" ClientIDMode="Static" MaxLength="2048" CssClass="area-text" Width="725px" Height="30px" TextMode="MultiLine" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" ControlToValidate="txtShortDescription" Text="*" ErrorMessage="Вы не ввели суть требования" ValidationGroup="groupRequirement" runat="server" />        
                                <asp:CustomValidator ID="CustomValidator1" runat="server" Text="*" ErrorMessage="Длина сути требования должна быть не более 2048 символов" ControlToValidate="txtShortDescription" ValidationGroup="groupRequirement"
	                                 SetFocusOnError="true" ClientValidationFunction="CheckMaxLength" ></asp:CustomValidator>
                                <script type="text/javascript">
                                    function CheckMaxLength(sender, args) { if (args.Value.length >= 2048) args.IsValid = false; }
                                </script>
                            </div>            
                            <asp:Panel runat="server" ID="plComment" Visible="false">
                                <div class="row row-html-editor clearfix">
                                    <label>Требование подробно</label>
                                    <uc:HtmlEditor runat="server" ID="ucComment" Module="Requirements" Width="735px" />
                                </div>
                                <div class="row">
                                    <label>Комментарий</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Width="725px" ID="txtComment" ClientIDMode="Static" CssClass="area-text" />
                                </div>
                                <div class="left-column">
                                    <div class="row">
                                        <label>Официальный ответ</label>
                                        <asp:CheckBox runat="server" ID="chxIsOfficialAnswer" />                    
                                    </div>
                                </div>    
                                <div class="right-column">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" CssClass="row">                    
                                        <label style="width: 60px;vertical-align: top">Файл:</label>                        
                                        <telerik:RadAsyncUpload ID="rauFileUpload" Width="250px" runat="server" MaxFileSize="5242880"  Visible="false"
                                                AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                                    Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
                                        <telerik:RadButton ID="rbtnAddFile" runat="server" Skin="Windows7" Text="Добавить файл" OnClick="rbtnAddFile_OnClick" />                    
                                    </telerik:RadAjaxPanel>
                                </div>
                                <div class="clear"></div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="plComments" Visible="false">                
                                <uc:ContentComments runat="server" ID="ucContentComments" ExpandComments="true" CommentType="tbl_RequirementComment" EnableHtmlCommentEditor="true" />
                                <br/>
                            </asp:Panel>
                            <a href="javascript:;" onclick="ShowChooseAnswerRadWindow()" class="btn"><em>&nbsp;</em><span>Подобрать ответ</span></a>
                        </telerik:RadPageView>
		                <telerik:RadPageView ID="RadPageView2" runat="server">
		                    <h3>Источник требования</h3>
                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
                                <div class="left-column">                
                                    <div class="row">
                                        <label>Тип запроса</label>                        
                                        <uc:DictionaryComboBox ID="dcbRequestSourceType" AutoPostBack="true" OnSelectedIndexChanged="dcbRequestSourceType_OnSelectedIndexChanged" DictionaryName="tbl_RequestSourceType" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                    <div class="row row-relative">
                                        <label>Запрос</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbRequests" AutoPostBack="true" OnSelectedIndexChanged="dcbRequests_OnSelectedIndexChanged" Include="tbl_RequestSourceType" Mask="#RequestType № #Number от #CreatedAt" DictionaryName="tbl_Request" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                        <asp:HyperLink runat="server" ID="hlGoToRequest" Target="_blank" ToolTip="Перейти к запросу" Visible="false" CssClass="go-to" ImageUrl="~/App_Themes/Default/images/icoView.png" />
                                    </div>                
                                    <div class="row">
                                        <label>Продукт</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbProducts"  DictionaryName="tbl_Product" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                </div>
                                <div class="right-column">                    
                                    <div class="row">
                                        <label>Компания</label>
                                        <uc:DictionaryOnDemandComboBox ID="dcbCompany" AutoPostBack="True" OnSelectedIndexChanged="dcbCompany_OnSelectedIndexChanged" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                    </div>
                                    <div class="row">
                                        <label>Контакт</label>
                                        <uc:ContactComboBox ID="ucContact" CssClass="select-text" runat="server" FilterByFullName="true" AutoPostBack="true" OnSelectedIndexChanged="ucContact_OnSelectedIndexChanged" />
                                    </div>                    
                                    <div class="row">
                                        <label>Серийный номер</label>
                                        <asp:TextBox runat="server" ID="txtProductSeriesNumber" CssClass="input-text" />
                                    </div>
                                </div>
                            </telerik:RadAjaxPanel>
                            <div class="clear"></div>
                            <h3>Классификация</h3>
                            <div class="left-column">
                                <div class="row">
                                    <label>Тип требования</label>
                                    <uc:DictionaryComboBox ID="dcbRequirementType" SelectDefaultValue="true" AutoPostBack="true" OnSelectedIndexChanged="dcbRequirementType_OnSelectedIndexChanged" DictionaryName="tbl_RequirementType" ValidationGroup="groupRequirement" ValidationErrorMessage="Вы не выбрали тип требования" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Степень воздействия</label>                    
                                    <uc:DictionaryComboBox ID="dcbRequirementSeverityOfExposure" DictionaryName="tbl_RequirementSeverityOfExposure" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Уровень обслуживания</label>
                                    <uc:DictionaryComboBox ID="dcbServiceLevel" AutoPostBack="true" OnSelectedIndexChanged="dcbServiceLevel_OnSelectedIndexChanged" DictionaryName="tbl_ServiceLevel" ValidationGroup="groupRequirement" ValidationErrorMessage="Вы не выбрали уровень обслуживания" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>                
                            </div>
                            <div class="right-column">
                                <div class="row">
                                    <label>Приоритет</label>
                                    <uc:DictionaryComboBox ID="dcbRequirementPriority" DictionaryName="tbl_RequirementPriority" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Сложность</label>
                                    <uc:DictionaryComboBox ID="dcbRequirementComplexity" DictionaryName="tbl_RequirementComplexity" DataTextField="Title" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Категория</label>
                                    <uc:SelectCategory runat="server" ID="ucPublicationCategory" CategoryType="Publication" ShowEmpty="true" CssClass="select-text" />
                                </div>
                            </div>
                            <div class="clear"></div>            		    
                            <div class="row">
                                <label>Родительское требование</label>
                                <uc:ParentRequirement runat="server" ID="ucParentRequirment" />                
                            </div>            
                            <h3>Связанные запросы</h3>
                            <labitec:Grid ID="gridRequests" TableName="tbl_Request" ClassName="WebCounter.AdminPanel.RequirementRequests" Fields="tbl_Contact.ID, tbl_Company.ID" Export="true" runat="server" OnItemDataBound="gridRequests_OnItemDataBound">
                                <Columns>
                                    <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn2" DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn3" DataField="ReactionDatePlanned" HeaderText="Дата реакции, план" runat="server"/>            
                                    <labitec:GridColumn ID="GridColumn4" DataField="tbl_RequestSourceType.Title" HeaderText="Тип источника" runat="server"/>
                                    <labitec:GridColumn ID="GridColumn5" DataField="RequeststatusID" HeaderText="Состояние запроса" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlRequestStatus" runat="server"/>
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn6" DataField="tbl_Company.Name" HeaderText="Компания" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlCompanyName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn7" DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server">
                                        <ItemTemplate>
                                            <asp:Literal ID="lrlUserFullName" runat="server" />
                                        </ItemTemplate>
                                    </labitec:GridColumn>
                                    <labitec:GridColumn ID="GridColumn8" DataField="tbl_ServiceLevel.Title" HeaderText="Уровень обслуживания" runat="server"/>
                                </Columns>    
                                <Joins>        
                                    <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Request" TableKey="RequestSourceTypeID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_ServiceLevel" JoinTableKey="ID" TableName="tbl_Request" TableKey="ServiceLevelID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Company" JoinTableKey="ID" TableName="tbl_Request" TableKey="CompanyID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Request" TableKey="ContactID" runat="server" />
                                    <labitec:GridJoin ID="GridJoin5" JoinTableName="tbl_RequestToRequirement" JoinTableKey="RequestID" TableName="tbl_Request" TableKey="ID" runat="server" />
                                </Joins>
                            </labitec:Grid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">            
                            <h3>Отзыв клиента</h3>
                            <div class="row">
                                <label>Полнота реализации:</label>
                                <uc:DictionaryComboBox runat="server" ID="dcbRequirementImplementationComplete" DictionaryName="tbl_RequirementImplementationComplete" DataTextField="Title" ShowEmpty="true" />
                            </div>
                            <div class="row">
                                <label>Скорость/сроки:</label>
                                <uc:DictionaryComboBox runat="server" ID="dcbRequirementSpeedTime" DictionaryName="tbl_RequirementSpeedTime" DataTextField="Title" ShowEmpty="true" />
                            </div>
                            <div class="row">
                                <label>Удовлетворенность:</label>
                                <uc:DictionaryComboBox runat="server" ID="dcbRequirementSatisfaction" DictionaryName="tbl_RequirementSatisfaction" DataTextField="Title" ShowEmpty="true" />
                            </div>
                            <div class="row">
		                        <label>Комментарий к оценке:</label>
                                <asp:TextBox runat="server" ID="txtEstimationComment" CssClass="area-text" Width="642px" Height="30px" TextMode="MultiLine" />
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server">            
                            <telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
                                <h3>Внутренняя оценка</h3>
                                <div class="left-column">
				                    <div class="row">
					                    <label>Количество:</label>
					                    <telerik:RadNumericTextBox runat="server" ID="rntxtInternalQuantity" EmptyMessage="" Value="0" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
						                    <ClientEvents OnBlur="Blur"/>
					                    </telerik:RadNumericTextBox>	
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="rntxtInternalQuantity" CssClass="required" Text="*" ErrorMessage="Вы не ввели количество" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
                                </div>                
                                <div class="right-column">
                                    <div class="row">
					                    <label>Единица измерения:</label>
					                    <uc:DictionaryComboBox runat="server" ID="dcbInternalUnit" DictionaryName="tbl_Unit" DataTextField="Title" ShowEmpty="true" />
				                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="row">
		                            <label>Комментарий:</label>
                                    <asp:TextBox runat="server" ID="txtEstimateCommentInternal" CssClass="area-text" Width="650px" Height="30px" TextMode="MultiLine" />
                                </div>
                                <h3>Оплата требования</h3>
                                <div class="row">
                                    <label>Договор/спецификация</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbContract" Mask="Договор № #Number от #CreatedAt" DictionaryName="tbl_Contract" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Заказ</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbOrder" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <div class="row">
                                    <label>Счет</label>
                                    <uc:DictionaryOnDemandComboBox ID="dcbInvoice" DictionaryName="tbl_Invoice" DataTextField="Number" ShowEmpty="true" CssClass="select-text" runat="server"/>
                                </div>
                                <h3>Оценка для клиента</h3>
                                <div class="row">
				                    <label>Продукт:</label>
				                    <uc:DictionaryOnDemandComboBox runat="server" ID="dcbEvaluationRequirementsProducts" AutoPostBack="true" OnSelectedIndexChanged="dcbEvaluationRequirementsProducts_OnSelectedIndexChanged" DictionaryName="tbl_Product" DataTextField="Title" ShowEmpty="true" CssClass="select-text" />
			                    </div>            
			                    <div class="row">
				                    <label>Произвольный продукт:</label>
				                    <asp:TextBox runat="server" ID="txtAnyProductName" CssClass="input-text" Width="650px" />
			                    </div>						
			                    <div class="left-column">
				                    <div class="row">
					                    <label>Количество:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtQuantity" EmptyMessage="" Value="0" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
						                    <ClientEvents OnBlur="Blur"/>
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtQuantity" CssClass="required" Text="*" ErrorMessage="Вы не ввели количество" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
				                    <div class="row">
					                    <label>Валюта:</label>
					                    <uc:DictionaryComboBox runat="server" ID="dcbCurrency" DictionaryName="tbl_Currency" DataTextField="Name" ShowEmpty="true" />
				                    </div>                            
				                    <div class="row">
					                    <label>Цена в валюте:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyPrice" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
						                    <ClientEvents OnBlur="Blur"/> 
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtCurrencyPrice" CssClass="required" Text="*" ErrorMessage="Вы не ввели цену в валюте" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
				                    <div class="row row-dictionary">
					                    <label>Сумма в валюте:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtCurrencyAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
						                    <ClientEvents OnBlur="Blur"/>
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtCurrencyAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму в валюте" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
			                    </div>
			                    <div class="right-column">
				                    <div class="row">
					                    <label>Единица измерения:</label>
					                    <uc:DictionaryComboBox runat="server" ID="dcbUnit" DictionaryName="tbl_Unit" DataTextField="Title" ShowEmpty="true" />
				                    </div>
				                    <div class="row">
					                    <label>Курс:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtRate" EmptyMessage="" Value="1" MinValue="1" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" />
						                    <ClientEvents OnBlur="Blur"/> 
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtRate" CssClass="required" Text="*" ErrorMessage="Вы не ввели курс" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
				                    <div class="row">
					                    <label>Цена:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtPrice" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
						                    <ClientEvents OnBlur="Blur"/>
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rntxtPrice" CssClass="required" Text="*" ErrorMessage="Вы не ввели цену" ValidationGroup="groupRequirement" runat="server" />
				                    </div>
				                    <div class="row">
					                    <label>Сумма:</label>
					                    <telerik:RadNumericTextBox runat="server" ClientIDMode="Static" ID="rntxtAmount" Value="0" EmptyMessage="" MinValue="0" CssClass="input-text" Type="Number">
						                    <NumberFormat GroupSeparator="" DecimalDigits="2" /> 
						                    <ClientEvents OnBlur="Blur"/>
					                    </telerik:RadNumericTextBox>
					                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rntxtAmount" CssClass="required" Text="*" ErrorMessage="Вы не ввели сумму" ValidationGroup="groupRequirement" runat="server" />
				                    </div>  
			                    </div>			
			                    <div class="clear"></div>
                                <div class="row">
		                            <label>Комментарий:</label>
                                    <asp:TextBox runat="server" ID="txtEstimateCommentForClient" CssClass="area-text" Width="650px" Height="30px" TextMode="MultiLine" />
                                </div>
                            </telerik:RadAjaxPanel>
                        </telerik:RadPageView>        
                        <telerik:RadPageView ID="RadPageView5" runat="server">            
                                <labitec:Grid ID="gridRequirementHistory" OnItemDataBound="gridRequirementHistory_OnItemDataBound" Toolbar="false" TableName="tbl_RequirementHistory" Fields="tbl_Contact.ID, c1.ID" ClassName="WebCounter.AdminPanel.RequirementHistory" runat="server">
                                    <Columns>
                                        <labitec:GridColumn ID="GridColumn9" DataField="CreatedAt" HeaderText="Дата изменения" runat="server"/>
                                        <labitec:GridColumn ID="GridColumn10" DataField="tbl_Contact.UserFullName" HeaderText="Автор изменения" runat="server">
                                            <ItemTemplate>
                                                <asp:Literal ID="lrlUserFullName" runat="server" />
                                            </ItemTemplate>
                                        </labitec:GridColumn>
                                        <labitec:GridColumn ID="GridColumn11" DataField="c1.UserFullName" HeaderText="Ответственный" runat="server">
                                            <ItemTemplate>
                                                <asp:Literal ID="lrlResponsibleUserFullName" runat="server" />
                                            </ItemTemplate>
                                        </labitec:GridColumn>                        
                                        <labitec:GridColumn ID="GridColumn12" DataField="tbl_RequirementStatus.Title" HeaderText="Состояние требования" runat="server"/>
                                    </Columns>
                                    <Joins>        
                                        <labitec:GridJoin ID="GridJoin6" JoinTableName="tbl_RequirementStatus" JoinTableKey="ID" TableName="tbl_RequirementHistory" TableKey="RequirementStatusID" runat="server" />
                                        <labitec:GridJoin ID="GridJoin7" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_RequirementHistory" TableKey="ContactID" runat="server" />
                                        <labitec:GridJoin ID="GridJoin8" JoinTableName="tbl_Contact" JoinTableAs="c1" JoinTableKey="ID" TableKey="ResponsibleID" runat="server" />
                                    </Joins>
                                </labitec:Grid>            
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>        
                    <br/>
	                <div class="buttons">
		                <asp:LinkButton ID="lbtnSave" OnClientClick="if (!Comments.CallLeaveComment($('#textEditor'), $(this).attr('href'))) return false;" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupRequirement" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
		                <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
	                </div>
                </div>
        </td>
    </tr>
</table>

</asp:Content>
