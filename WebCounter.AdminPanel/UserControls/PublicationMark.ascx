<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicationMark.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.PublicationMark" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	
    <telerik:RadToolTipManager ID="RadToolTipManager1" OffsetY="-1" HideEvent="Default"
        Width="250" Height="350" runat="server" EnableShadow="true" OnAjaxUpdate="OnAjaxUpdate" RelativeTo="Element"
        Position="MiddleRight">
    </telerik:RadToolTipManager>

    <telerik:RadGrid ID="rgPublicationMarks" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgPublicationMarks_NeedDataSource" OnDeleteCommand="rgPublicationMarks_DeleteCommand"
		OnItemCommand="rgPublicationMarks_ItemCommand" OnInsertCommand="rgPublicationMarks_InsertCommand" OnUpdateCommand="rgPublicationMarks_UpdateCommand" OnItemDataBound="rgPublicationMarks_DataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings>
				<EditColumn ButtonType="ImageButton"/>
			</EditFormSettings>
			<Columns>                                    
				<telerik:GridTemplateColumn HeaderText="Пользователь">
					<ItemTemplate>
                        <%# Eval("UserName")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridBoundColumn CurrentFilterFunction="NoFilter" DataField="PublicationCommentID" Display="false"
                    DataType="System.Guid" FilterListOptions="VaryByDataType" ForceExtractValue="None"
                    HeaderText="ProductID" ReadOnly="True" SortExpression="PublicationCommentID" UniqueName="PublicationCommentID">
                </telerik:GridBoundColumn>
				<telerik:GridTemplateColumn HeaderText="Комментарий">
					<ItemTemplate>
                        <%#Eval("PublicationComment")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Статус/тип">
					<ItemTemplate>
                        <%# Eval("Type")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Бал">
					<ItemTemplate>
                        <%# Eval("Rank")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn HeaderText="Дата пометки">
					<ItemTemplate>
                        <%# Eval("CreatedAt")%>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
            <EditFormSettings EditFormType="Template" InsertCaption="Термин в публикации" CaptionFormatString="Термин в публикации">
				<PopUpSettings Modal="true" Width="820px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                       	<div class="two-columns order-product">                        						
                            <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						    CssClass="validation-summary"
						    runat="server"
						    EnableClientScript="true"
						    HeaderText="Заполните все поля корректно:"
						    ValidationGroup="valPublicationMarkGroup" />
                            
                            <div class="row">
								<label>Пользователь:</label>
                                <uc:ContactComboBox ID="ucUser" CssClass="select-text" runat="server" FilterByFullName="true" ValidationGroup="valPublicationMarkGroup"/>
							</div>

                            <div class="row">
								<label>Комментарий:</label>
                                <asp:DropDownList ID="ddlComment" runat="server" CssClass="select-text"/>
							</div>
                            <div class="row">
								<label>Тип:</label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="select-text"/>
							</div>
                            <div class="row">
								<label>Бал:</label>
								<telerik:RadNumericTextBox ID="rtbRank" runat="server" CssClass="input-text"><NumberFormat GroupSeparator="" DecimalDigits="0" /> </telerik:RadNumericTextBox>                               
							</div>
                            <div class="row date-picker-autopostback clearfix">
			                    <label>Дата пометки:</label>
			                    <div class="date-picker-container">
				                    <telerik:RadDatePicker runat="server" MinDate="01.01.1900" CssClass="date-picker" ID="rdpDate" ShowPopupOnFocus="true" Width="110px">
					                    <DateInput Enabled="true" />
					                    <DatePopupButton Enabled="true" />
				                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdpDate" Text="*" ErrorMessage="Вы не ввели дату" ValidationGroup="valPublicationMarkGroup" runat="server" />
			                    </div>
		                    </div>

                            <div class="clear"></div>						
							<br/>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="valPublicationMarkGroup" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
								<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
							</div> 
                        </div>
                       </asp:Panel>
                    </FormTemplate>
            </EditFormSettings>
		</MasterTableView>
        <ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>