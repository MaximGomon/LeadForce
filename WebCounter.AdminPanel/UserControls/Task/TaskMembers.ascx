<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskMembers.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.TaskMembers" %>
<%@ Register TagPrefix="uc" TagName="DictionaryComboBox" Src="~/UserControls/DictionaryComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="ContactComboBox" Src="~/UserControls/Contact/ContactComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="OrderProductsComboBox" Src="~/UserControls/Order/OrderProductsComboBox.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectEventTime" Src="~/UserControls/Task/SelectEventTime.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectMeetingTime" Src="~/UserControls/Task/SelectMeetingTime.ascx" %>

<telerik:RadScriptBlock runat="server">
    <script type="text/javascript">
        function ShowSelectEventTimeRadWindow() { $find('<%= selectEventTimeRadWindow.ClientID %>').show(); }
        function ShowSelectMeetingTimeRadWindow() { $find('<%= selectMeetingTimeRadWindow.ClientID %>').show(); }
        function CloseSelectEventTimeRadWindow() { $find('<%= selectEventTimeRadWindow.ClientID %>').close(); }
        function CloseSelectMeetingTimeRadWindow() { $find('<%= selectMeetingTimeRadWindow.ClientID %>').close(); }
    </script>
</telerik:RadScriptBlock>

<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<asp:Literal runat="server" ID="lrlSuccessMessage" />
	<asp:Literal runat="server" ID="lrlWarningMessage" />
    <asp:Literal runat="server" ID="lrlUserExistWarning" />
	<telerik:RadGrid ID="rgTaskMembers" runat="server" OnItemDataBound="rgTaskMembers_OnItemDataBound" OnNeedDataSource="rgTaskMembers_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="50" AllowSorting="True" AutoGenerateColumns="False" AllowMultiRowSelection="True" ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgTaskMembers_DeleteCommand" OnInsertCommand="rgTaskMembers_InsertCommand" OnUpdateCommand="rgTaskMembers_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID,ContactID" EditMode="PopUp">
			<Columns>          
				<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />  			
				<telerik:GridTemplateColumn UniqueName="Contractor" HeaderText="Контрагент">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlContractor" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="Contact" HeaderText="Контакт">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlContact" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>				
				<telerik:GridTemplateColumn UniqueName="Order" HeaderText="Заказ">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlOrder" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="OrderProducts" HeaderText="Продукт в заказе">
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlOrderProduct" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="Role" HeaderText="Роль">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlRole" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridTemplateColumn UniqueName="Status" HeaderText="Статус">			
					<ItemTemplate>
						<asp:Literal runat="server" ID="lrlStatus" />
					</ItemTemplate>
				</telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="IsInformed" HeaderText="Информирован">			
					<ItemTemplate>
						<asp:CheckBox runat="server" ID="chxIsInformed" Enabled="false"/>
					</ItemTemplate>
				</telerik:GridTemplateColumn>
				<telerik:GridBoundColumn UniqueName="Comment" HeaderText="Комментарий" DataField="Comment"/>				
				<telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="420px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Участник" CaptionFormatString="Участник">
				<PopUpSettings Modal="true" Width="800px" />
					<FormTemplate>
						<div class="task-popup-member">
                            <div class="left-column">
                                <div class="row row-dictionary">
								    <label>Контрагент:</label>
								    <uc:DictionaryComboBox runat="server" ID="dcbContractor" DictionaryName="tbl_Company" DataTextField="Name" ShowEmpty="true" Width="233px" OnSelectedIndexChanged="dcbContractor_OnSelectedIndexChanged" AutoPostBack="true" />
							    </div>
                                <div class="row">
								    <label>Роль:</label>
								    <asp:DropDownList runat="server" ID="ddlTaskMemberRole" CssClass="select-text"/>
								    <asp:RequiredFieldValidator ID="rfvTaskMemberRole" ControlToValidate="ddlTaskMemberRole" ValidationGroup="groupUpdateTaskMember" Text="*" ErrorMessage="Вы не выбрали роль" runat="server" />
							    </div>
                            </div>
                            <div class="right-column">
                                <div class="row">
								    <label>Контакт:</label>
								    <uc:ContactComboBox ID="ucContact" ValidationGroup="groupUpdateTaskMember" ValidationErrorMessage="Вы не выбрали контакт" CssClass="select-text" runat="server" FilterByFullName="true" OnSelectedIndexChanged="ucContact_OnSelectedIndexChanged" AutoPostBack="true" />
							    </div>
                                <div class="row">
								    <label>Статус:</label>
                                    <asp:Literal runat="server" ID="lrlTaskMemberStatus" Text="План" />
							    </div>                                
                                <div class="row">
								    <label>Информирован:</label>
                                    <asp:CheckBox runat="server" ID="chxIsInformed" />
							    </div>
                            </div>
                            <div class="clear"></div>
                            <asp:Panel runat="server" ID="plContactPay" Visible="true">                                
                                <div class="left-column">
                                    <div class="row row-dictionary">
								        <label>Заказ:</label>
								        <uc:DictionaryComboBox runat="server" ID="dcbOrder" AutoPostBack="true" OnSelectedIndexChanged="dcbOrder_OnSelectedIndexChanged" DictionaryName="tbl_Order" DataTextField="Number" ShowEmpty="true" />
							        </div>
                                </div>
                                <div class="right-column">
                                    <div class="row row-dictionary">
								        <label>Продукт в заказе:</label>
								        <uc:OrderProductsComboBox runat="server" ID="ucOrderProduct" />
							        </div>
                                </div>
                                <div class="clear"></div>
                            </asp:Panel>															
							<div class="row">
								<label>Комментарий:</label>
								<asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="area-text" Width="610px" />
							</div>
							<div class="row">
								<label>Комментарий пользователя:</label>
								<asp:TextBox runat="server" ID="txtUserComment" TextMode="MultiLine" CssClass="area-text" Width="610px" />
							</div>
						<br/>
						<div class="buttons clearfix">
							<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateTaskMember" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
							<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
						</div>
					</div>
				</FormTemplate>
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
	<br/>
    <asp:Panel runat="server" ID="plActions" Visible="true">
        <telerik:RadButton runat="server" ID="rbtnSelectEventTime" Text="Выбрать время" Skin="Windows7" OnClick="rbtnSelectEventTime_OnClick" />
        <telerik:RadButton runat="server" ID="rbtnSelectMeetingTime" Text="Выбрать время" Skin="Windows7" OnClick="rbtnSelectMeetingTime_OnClick" />
	    <telerik:RadButton runat="server" ID="rbtnInviteMembers" Text="Пригласить" OnClick="rbtnAction_OnClick" CommandArgument="2" Skin="Windows7" />
	    <telerik:RadButton runat="server" ID="rbtnInformMembers" Text="Информировать" OnClick="rbtnAction_OnClick" CommandArgument="1" Skin="Windows7"/>
        <telerik:RadButton runat="server" ID="rbtnMemberConfirmed" Text="Подтвердить участие" OnClick="rbtnAction_OnClick" CommandArgument="3" Skin="Windows7"/>
        <telerik:RadButton runat="server" ID="rbtnOrginizerConfirmed" Text="Выбрать исполнителем" OnClick="rbtnAction_OnClick" CommandArgument="4" Skin="Windows7"/>
        <telerik:RadButton runat="server" ID="rbtnParticipated" Text="Принять работу" OnClick="rbtnAction_OnClick" CommandArgument="7" Skin="Windows7"/>     
        <telerik:RadButton runat="server" ID="rbtnRefusedNotInterest" Text="Отказ (не интересно)" OnClick="rbtnAction_OnClick" CommandArgument="5" Skin="Windows7"/>
        <telerik:RadButton runat="server" ID="rbtnRefusedFailureNoWay" Text="Отказ (нет возможности)" OnClick="rbtnAction_OnClick" CommandArgument="6" Skin="Windows7"/>
        <telerik:RadButton runat="server" ID="rbtnParticipatedCanceled" Text="Отменить" OnClick="rbtnAction_OnClick" CommandArgument="9" Skin="Windows7"/>
    </asp:Panel>
</telerik:RadAjaxPanel>

<telerik:RadWindow runat="server" Title="Выбор времени" ID="selectEventTimeRadWindow" Width="800" Height="590" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="select-time-scheduler">
            <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc:SelectEventTime runat="server" ID="ucSelectEventTime" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br/>
		    <div class="buttons">
			    <asp:HyperLink ID="HyperLink1" CssClass="btn" runat="server" NavigateUrl="javascript:;" onclick="javascript:CloseSelectEventTimeRadWindow();"><em>&nbsp;</em><span>Закрыть</span></asp:HyperLink>		
		    </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadWindow runat="server" Title="Выбор времени" ID="selectMeetingTimeRadWindow" Width="800" Height="590" EnableTheming="false" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" CssClass="radwindow-popup" Behaviors="Close,Move" Modal="true" VisibleStatusbar="false" BorderWidth="0px">
    <ContentTemplate>
        <div class="select-time-scheduler">
            <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc:SelectMeetingTime runat="server" ID="ucSelectMeetingTime" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br/>
		    <div class="buttons">
			    <asp:HyperLink ID="HyperLink2" CssClass="btn" runat="server" NavigateUrl="javascript:;" onclick="javascript:CloseSelectMeetingTimeRadWindow();"><em>&nbsp;</em><span>Закрыть</span></asp:HyperLink>		
		    </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>