<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelatedPublication.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.RelatedPublication" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
	<telerik:RadGrid ID="rgRelatedPublication" Skin="Windows7" Width="100%" runat="server" OnNeedDataSource="rgRelatedPublication_NeedDataSource" OnDeleteCommand="rgRelatedPublication_DeleteCommand"
		OnInsertCommand="rgRelatedPublication_InsertCommand" OnUpdateCommand="rgRelatedPublication_UpdateCommand" OnItemDataBound="rgRelatedPublication_OnItemDataBound">
		<MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" DataKeyNames="ID" InsertItemPageIndexAction="ShowItemOnCurrentPage" EditMode="PopUp">
			<EditFormSettings>
				<EditColumn ButtonType="ImageButton"/>
			</EditFormSettings>
			<Columns>             
            	<telerik:GridTemplateColumn HeaderText="Модуль" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("Module").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>                       
				<telerik:GridTemplateColumn HeaderText="Запись" UniqueName="Title">
					<ItemTemplate>
                        <%# Server.HtmlEncode(Eval("Title").ToString())%>                    
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
            <EditFormSettings EditFormType="Template" InsertCaption="Связанная запись" CaptionFormatString="Связанная запись">
				<PopUpSettings Modal="true" Width="335px" />
					<FormTemplate>
					   <asp:Panel runat="server" ID="plEditForm">
                        <asp:EntityDataSource ID="edsRecords" runat="server" AutoGenerateWhereClause="false" EntitySetName="tbl_Publication" ConnectionString="name=WebCounterEntities" DefaultContainerName="WebCounterEntities" />
                       	<div class="two-columns order-product">   
                            <asp:ValidationSummary ID="ValidationSummary" DisplayMode="BulletList" 
						    CssClass="validation-summary"
						    runat="server"
						    EnableClientScript="true"
						    HeaderText="Заполните все поля корректно:"
						    ValidationGroup="valRelatedPublicationTermGroup" />
                            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server"><TemplateControl>
                            <div class="row">
								<label>Модуль:</label>
                                <telerik:RadComboBox ID="rcbModules"
                                    OnSelectedIndexChanged="rcbModules_IndexChanged"
                                    runat="server"
                                    CssClass="select-text" 
                                    EnableLoadOnDemand="true"
                                    AutoPostBack="true"                                    
                                    />
							</div>
                            <div class="row">
								<label>Запись:</label>
                                <telerik:RadComboBox ID="rcbRecords"   
                                    runat="server"
                                    CssClass="select-text" 
                                    ClientIDMode="Static"
                                    EnableLoadOnDemand="true" 
                                    Filter="Contains"
                                    />
							</div>
                            </TemplateControl></telerik:RadAjaxPanel>
							<div class="buttons clearfix">
								<asp:LinkButton ID="lbtnSave" ValidationGroup="valRelatedPublicationTermGroup" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
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