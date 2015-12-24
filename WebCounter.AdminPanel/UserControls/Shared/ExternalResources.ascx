<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalResources.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.ExternalResources" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Common" %>
<%@ Import Namespace="WebCounter.BusinessLogicLayer.Enumerations.WebSite" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
  <script type="text/javascript">
      function PopUpShowing(sender, eventArgs) {
          var popUp = eventArgs.get_popUp();
          $(popUp).css("position", "fixed");
          popUp.style.left = Math.round((($(document).width() - $(popUp).width()) / 2)).toString() + "px";
          popUp.style.top = "20px";
      }	  
  </script>
</telerik:RadCodeBlock>


<telerik:RadAjaxPanel runat="server" ID="ajaxPanel">
	<telerik:RadGrid ID="rgExternalResource" CssClass="grid-default" runat="server" OnItemDataBound="rgExternalResource_OnItemDataBound" OnNeedDataSource="rgExternalResource_NeedDataSource" Skin="Windows7" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"  ShowStatusBar="true" HorizontalAlign="NotSet"
	 OnDeleteCommand="rgExternalResource_DeleteCommand" OnInsertCommand="rgExternalResource_InsertCommand" OnUpdateCommand="rgExternalResource_UpdateCommand">
		<MasterTableView CommandItemDisplay="Top" DataKeyNames="ID" EditMode="PopUp">
			<Columns>
			    <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="Название" />
                <telerik:GridTemplateColumn UniqueName="ExternalResourceTypeID" DataField="ExternalResourceTypeID" HeaderText="Тип">
                    <ItemTemplate>
                        <%# EnumHelper.GetEnumDescription((ExternalResourceType)int.Parse(Eval("ExternalResourceTypeID").ToString()))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ResourcePlaceID" DataField="ResourcePlaceID" HeaderText="Место включения">
                    <ItemTemplate>
                        <%# EnumHelper.GetEnumDescription((ResourcePlace)int.Parse(Eval("ResourcePlaceID").ToString()))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="20px" />
				<telerik:GridButtonColumn ConfirmText="Вы действительно хотите удалить запись?" ItemStyle-Width="20px" ConfirmDialogType="RadWindow"
					ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" ConfirmDialogHeight="100px"
					ConfirmDialogWidth="220px" /> 
			</Columns>
			<EditFormSettings EditFormType="Template" InsertCaption="Ресурс" CaptionFormatString="Ресурс">
				<PopUpSettings Modal="true" Width="780px" Height="420px" />
				<FormTemplate>
					<asp:Panel runat="server" ID="plEditForm" CssClass="radwindow-popup-inner bottom-buttons">
                        <div class="row">
							<label>Название:</label>
							<asp:TextBox runat="server" ID="txtTitle" CssClass="input-text" Width="560px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtTitle" CssClass="required" Text="*" ErrorMessage="Вы не ввели название" ValidationGroup="groupUpdateExternalResource" runat="server" />
						</div>
					    <div class="row">
					        <label>Место включения:</label>
					        <asp:DropDownList ID="ddlResourcePlace" runat="server" CssClass="select-text" />
					    </div>
                        <div class="row">
					        <label>Тип:</label>
					        <asp:DropDownList ID="ddlExternalResourceType" runat="server" CssClass="select-text" />
					    </div>
                        <telerik:RadTabStrip ID="rtsTabs" MultiPageID="RadMultiPage1" SelectedIndex="0" runat="server">
			                <Tabs>
				                <telerik:RadTab Text="Файл" />                    
				                <telerik:RadTab Text="Текст" />
			                    <telerik:RadTab Text="Url" />
			                </Tabs>
		                </telerik:RadTabStrip>
		                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" CssClass="multiPage" runat="server">
			                <telerik:RadPageView ID="RadPageView1" runat="server">
			                    <telerik:RadAsyncUpload ID="rauResourceFile" runat="server" MaxFileSize="5242880" AutoAddFileInputs="false"  MaxFileInputsCount="1" MultipleFileSelection="Disabled"
                                Skin="Windows7" Localization-Select="Выбрать" Localization-Remove="Удалить" Localization-Cancel="Отмена"/>
			                    <asp:Literal runat="server" ID="lrlFileName" />
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server">
                                <asp:TextBox ID="txtResourceText" runat="server" TextMode="MultiLine" CssClass="area-text" Width="725px" Height="130px" />
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <asp:TextBox runat="server" ID="txtResourceUrl" CssClass="input-text" Width="725px" />
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>                        
                        <div class="clear"></div>
						<br/>
						<div class="buttons clearfix">
							<asp:LinkButton ID="lbtnSave" ValidationGroup="groupUpdateExternalResource" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
							<asp:LinkButton runat="server" ID="lbtnCancel" CssClass="cancel" Text="Отмена" CausesValidation="False" CommandName="Cancel" />
						</div>
					</asp:Panel> 
				</FormTemplate>
			</EditFormSettings>
		</MasterTableView>
		<ClientSettings>
			<ClientEvents OnPopUpShowing="PopUpShowing" />
			<Selecting AllowRowSelect="true" />
		</ClientSettings>
	</telerik:RadGrid>
</telerik:RadAjaxPanel>