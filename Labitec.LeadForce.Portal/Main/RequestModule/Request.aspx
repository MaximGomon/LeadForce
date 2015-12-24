<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.RequestModule.Request" %>
<%@ Register TagPrefix="uc" TagName="NotificationMessage" Src="~/Shared/UserControls/NotificationMessage.ascx" %>
<%@ Register TagPrefix="uc" TagName="HtmlEditor" Src="~/Shared/UserControls/HtmlEditor.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContentComments" Src="~/Shared/UserControls/ContentComments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<div class="b-block">
    <h4 class="top-radius"><asp:Literal runat="server" ID="lrlTitle" /><span><asp:Literal runat="server" ID="lrlStatus" /></span></h4>
    <div class="block-content bottom-radius request">
        <uc:NotificationMessage runat="server" ID="ucNotificationMessage" MessageType="Warning" />
        <asp:Panel runat="server" ID="plViewRequest" Visible="false">
	        <div class="two-columns">
                <div class="left-column">
                    <div class="row">
                        <label>Разместил:</label>
                        <asp:Literal runat="server" ID="lrlContact" />
                    </div>
                    <div class="row">
                        <label>Дата реакции, план:</label>
                        <asp:Literal runat="server" ID="lrlReactionDatePlanned" />
                    </div>                                          
                </div>		
                <div class="left-column">
                    <div class="row">
                        <label>Ответственный:</label>
                        <asp:Literal runat="server" ID="lrlResponsible" />
                    </div>
                    <div class="row">
                        <label>Дата реакции, факт:</label>
                        <asp:Literal runat="server" ID="lrlReactionDateActual" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" Skin="Windows7" CssClass="tabs" SelectedIndex="0" runat="server">
			        <Tabs>
				        <telerik:RadTab Text="Содержание запроса" />				
				        <telerik:RadTab Text="Связанные требования" />
			        </Tabs>
		        </telerik:RadTabStrip>
		        <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			        <telerik:RadPageView ID="RadPageView1" runat="server">
			            <div class="row row-normal">
                            <label>Суть запроса:</label>
                            <asp:Literal runat="server" ID="lrlShortDescription" />
                        </div>
                        <div class="row row-normal">
                            <label>Запрос подробно:</label>
                            <asp:Literal runat="server" ID="lrlLongDescription" />
                        </div>
                        <div class="row row-normal">
                            <label>Файлы запроса:</label>
                            <asp:Literal runat="server" ID="lrlRequestFiles" Text="Отсутствуют" />
                        </div>
                        <div class="row row-normal">
                            <label>Файлы источника:</label>
                            <asp:Literal runat="server" ID="lrlSourceFiles" Text="Отсутствуют" />
                        </div>                	
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <labitec:Grid ID="gridRequirements" TableName="tbl_Requirement" ClassName="Labitec.LeadForce.Portal.RequestRequirements" Fields="tbl_Contact.ID" Export="true" runat="server">
                        <Columns>            
                            <labitec:GridColumn ID="GridColumn1" DataField="Number" HeaderText="Номер" runat="server"/>
                            <labitec:GridColumn ID="GridColumn2" DataField="CreatedAt" HeaderText="Дата регистрации" runat="server"/>            
                            <labitec:GridColumn ID="GridColumn3" DataField="tbl_RequirementStatus.Title" HeaderText="Состояние требования" runat="server"/>
                            <labitec:GridColumn ID="GridColumn4" DataField="ShortDescription" HeaderText="Суть требования" runat="server"/>                                    
                            <labitec:GridColumn ID="GridColumn5" DataField="tbl_Contact.UserFullName" HeaderText="Контакт" runat="server"/>            
                        </Columns>    
                        <Joins>        
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_RequirementStatus" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_RequirementTransition" JoinTableKey="InitialRequirementStatusID" TableName="tbl_Requirement" TableKey="RequirementStatusID" runat="server" />            
                            <labitec:GridJoin ID="GridJoin3" JoinTableName="tbl_Contact" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="ContactID" runat="server" />            
                            <labitec:GridJoin ID="GridJoin4" JoinTableName="tbl_RequestSourceType" JoinTableKey="ID" TableName="tbl_Requirement" TableKey="RequestSourceTypeID" runat="server" />
                        </Joins>
                    </labitec:Grid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
		        <uc:ContentComments runat="server" ID="ucContentComments" CommentType="tbl_RequestComment" />	            		        
           </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="plAddRequest" Visible="false">
            <div class="row">
                <label>Суть запроса:</label>
                <asp:TextBox runat="server" ID="txtShortDescription" ClientIDMode="Static" MaxLength="2048" CssClass="area-text" Width="760px" Height="30px" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="required" Display="Dynamic" ControlToValidate="txtShortDescription" Text="*" ErrorMessage="Вы не ввели суть запроса" ValidationGroup="groupRequest" runat="server" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" Text="*" ErrorMessage="Длина сути запроса должна быть не более 2048 символов" ControlToValidate="txtShortDescription" ValidationGroup="groupRequest"
	                    SetFocusOnError="true" ClientValidationFunction="CheckMaxLength" ></asp:CustomValidator>
                <script type="text/javascript">
                    function CheckMaxLength(sender, args) { if (args.Value.length >= 2048) args.IsValid = false; }
                </script>
            </div>            
            <div class="row row-html-editor clearfix">
                <label>Запрос подробно:</label>                
                <uc:HtmlEditor runat="server" ID="ucLongDescription" IsDeleteEnabled="false" Width="772px" Height="300px" Module="Requests" />
            </div>
            <div class="row">
                <label>Файл:</label>
                <telerik:RadAsyncUpload ID="rauRequestFiles" Width="250px" runat="server" MaxFileSize="5242880" AutoAddFileInputs="true"
                Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
            </div>
            <br/>            
		    <div class="buttons clearfix">
			    <asp:LinkButton ID="lbtnSave" OnClick="lbtnSave_OnClick" CssClass="btn" ValidationGroup="groupRequest" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
			    <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
		    </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
