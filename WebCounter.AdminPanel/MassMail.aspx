<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MassMail.aspx.cs" Inherits="WebCounter.AdminPanel.MassMail" %>
<%@ Register TagPrefix="uc" TagName="SettingsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SettingsSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectContacts" Src="~/UserControls/Contact/SelectContacts.ascx" %>
<%@ Register TagPrefix="uc" TagName="EditorSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/EditorSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="SelectSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/SelectSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="StatsSiteActionTemplate" Src="~/UserControls/SiteActionTemplate/StatsSiteActionTemplate.ascx" %>
<%@ Register TagPrefix="uc" TagName="LeftColumn" Src="~/UserControls/Widgets/Master/LeftColumn.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href='<%# ResolveUrl("~/Skins/Labitec/ComboBox.Labitec.css")  %>' rel="stylesheet" type="text/css" />
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnResponseEnd() {
                <%= ((WebCounter.AdminPanel.UserControls.Shared.HtmlEditor)ucEditorSiteActionTemplate.FindControl("ucHtmlEditor")).ClientID %>_setupEditor();
            }

            function OnClientClicking(sender, args) {
                if ($find('<%= rdtpSchedule.ClientID %>').get_selectedDate() == null)
                    args.set_cancel(true);
            }

           function OnClientClicking_Cancel(sender, args) {
               $find('<%= radToolTip.ClientID %>').hide();
               args.set_cancel(true);
           }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
<table><tr>
<td width="195px" valign="top" ID="leftColumn" runat="server">
    <div class="aside">
        <uc:LeftColumn runat="server" />
    </div>
</td>
<td valign="top" width="100%">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                    <uc:SelectContacts ID="ucSelectContacts" OnSelectedChanged="ucSelectContacts_OnSelectedChanged" HideButton="True" runat="server" />
                    </telerik:RadAjaxPanel>


	    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" 
						    CssClass="validation-summary"
						    runat="server"
						    EnableClientScript="true"
						    HeaderText="Заполните все поля корректно:"
                            ValidationGroup="valGroup"
						    />
                        
        <telerik:RadTabStrip ID="radTabStrip" MultiPageID="radMultiPage" CausesValidation="False" SelectedIndex="0" runat="server">
            <Tabs>
                <telerik:RadTab Text="Настройки рассылки" />
                <telerik:RadTab Text="Получатели" />
                <telerik:RadTab Text="Шаблон сообщения" />
                <telerik:RadTab Text="Статистика откликов" Value="Stats" Visible="False" />
            </Tabs>
        </telerik:RadTabStrip>
    
        <telerik:RadMultiPage ID="radMultiPage" SelectedIndex="0" CssClass="multiPage" runat="server">
            <telerik:RadPageView ID="radPageView0" runat="server">
                <table width="912px">
                    <tr>
                        <td width="154px">
                            <div class="row">
                                <label style="width: 154px">Название рассылки:</label>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <asp:TextBox ID="txtName" CssClass="input-text" Width="717px" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtName" ErrorMessage="Вы не ввели 'Название шаблона'" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                </table>

                <asp:Panel ID="pnlStatus" Visible="False" runat="server">
                    <table width="912px">
                        <tr>
                            <td width="497px">
                                <div class="row">
                                    <label>Статус:</label>
                                    <asp:DropDownList ID="ddlStatus" Enabled="False" CssClass="select-text" runat="server"></asp:DropDownList>                                    
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <label>Дата рассылки:</label>
                                    <telerik:RadDateTimePicker ID="rdtpMailDate" Width="175px" CssClass="datetime-picker" ShowPopupOnFocus="True" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtpMailDate" ErrorMessage="Вы не выбрали 'Дата рассылки'" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <uc:SettingsSiteActionTemplate ID="ucSettingsSiteActionTemplate" ShowTitle="False" ShowActionType="False" ValidationGroup="valGroup" runat="server" />
            </telerik:RadPageView>
        
            <telerik:RadPageView ID="radPageView1" runat="server">
                <div class="row">
                    <label>Целевая аудитория:</label>
                    <asp:RadioButtonList ID="rblTargetContacts" AutoPostBack="True" OnSelectedIndexChanged="rblTargetContacts_OnSelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radiobuttonlist-horizontal" runat="server" />
                </div>
            
                <asp:Panel ID="pnlTags" runat="server">
                    <table>
                        <tr>
                            <td valign="top">
                                <div class="row">
                                    <label>Сегмент:</label>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="litTag" runat="server" />
                                <asp:Panel ID="pnlTagsList" CssClass="row" runat="server">
                                    <asp:RadioButtonList ID="rblTags" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table" CssClass="radiobuttonlist-horizontal" runat="server" />                
                                    <asp:RequiredFieldValidator ID="rfvTags" ControlToValidate="rblTags" ErrorMessage="Вы не выбрали 'Сегмент'" ValidationGroup="valGroup" runat="server">*</asp:RequiredFieldValidator>                                    
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            
                <asp:Panel ID="pnlSelectContacts" Visible="False" runat="server">
                    <telerik:RadButton ID="rbAdd" ClientIDMode="AutoID" OnClick="rbAdd_OnClick" Text="Добавить контакты" CausesValidation="False" Skin="Windows7" runat="server" />
                    <br /><br />
                    <labitec:Grid ID="gridContacts" TableName="tbl_Contact" AccessCheck="true" OnItemDataBound="gridContacts_OnItemDataBound" ClassName="WebCounter.AdminPanel.MassMailContacts" Export="true" runat="server">
                        <Columns>
                            <labitec:GridColumn ID="GridColumn1" DataField="CreatedAt" HeaderText="Дата создания" DataType="DateTime" runat="server"/>
                            <labitec:GridColumn ID="GridColumn2" DataField="LastActivityAt" HeaderText="Последняя активность" DataType="DateTime" runat="server"/>
                            <labitec:GridColumn ID="GridColumn3" DataField="UserFullName" HeaderText="Ф.И.О." runat="server"/>
                            <labitec:GridColumn ID="GridColumn4" DataField="Email" HeaderText="E-mail" runat="server"/>
                            <labitec:GridColumn ID="GridColumn5" DataField="tbl_ReadyToSell.Title" HeaderText="Готовность к продаже" runat="server"/>
                            <labitec:GridColumn ID="GridColumn6" DataField="tbl_Priorities.Title" HeaderText="Важность" runat="server"/>
                            <labitec:GridColumn ID="GridColumn8" DataField="Score" HeaderText="Общий балл" DataType="Int32" runat="server"/>
                            <labitec:GridColumn ID="GridColumn11" DataField="ID" HeaderText="Действия" Width="100px" Reorderable="false" Sortable="false" Groupable="false" AllowFiltering="false" HorizontalAlign="Center" runat="server">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnDelete" OnCommand="ibtnDelete_OnCommand" ImageUrl="~/App_Themes/Default/images/icoDelete.gif" ToolTip="Удалить" AlternateText="Удалить" CausesValidation="False" runat="server"/>
                                </ItemTemplate>
                            </labitec:GridColumn>
                        </Columns>
                        <Joins>
                            <labitec:GridJoin ID="GridJoin1" JoinTableName="tbl_ReadyToSell" JoinTableKey="ID" TableKey="ReadyToSellID" runat="server" />
                            <labitec:GridJoin ID="GridJoin2" JoinTableName="tbl_Priorities" JoinTableKey="ID" TableKey="PriorityID" runat="server" />
                        </Joins>
                    </labitec:Grid>
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="radPageView2" runat="server">
                <telerik:RadPanelBar ID="rpbTemplateMessage" ClientIDMode="Static" ExpandAnimation-Type="None" CollapseAnimation-Type="None" ExpandMode="SingleExpandedItem" Skin="Windows7" Width="935px" CssClass="panelbar" runat="server">
                    <Items>
                        <telerik:RadPanelItem Expanded="True" Text="Выбор шаблона" Value="SelectTemplate">
                            <ContentTemplate>
                                <div class="panelbar-padding">
                                    <uc:SelectSiteActionTemplate ID="ucSelectSiteActionTemplate" OnSelectedChanged="ucSelectSiteActionTemplate_OnSelectedChanged" runat="server" />
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem Expanded="True" Text="Шаблон сообщения" Value="TemplateMessage">
                            <ContentTemplate>
                                <div class="panelbar-padding">
                                    <uc:EditorSiteActionTemplate ID="ucEditorSiteActionTemplate" runat="server" />
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="radPageView3" runat="server">
                <div class="row">
                    <label>Количество получателей:</label>
                    <asp:Literal ID="litRecipients" Text="0" runat="server" />
                </div>

                <uc:StatsSiteActionTemplate ID="ucStatsSiteActionTemplate" runat="server" />
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    
        <br />

        <asp:Panel ID="pnlBtnAdd" CssClass="buttons clearfix" Visible="False" runat="server">
            <telerik:RadToolTip ID="radToolTip" Skin="Windows7" ShowEvent="OnClick" TargetControlID="lbtnSchedule" Position="TopCenter" RelativeTo="Element" AutoCloseDelay="0" runat="server">
                <div style="padding: 10px;">
                    <telerik:RadDateTimePicker ID="rdtpSchedule" Width="175px" CssClass="datetime-picker" ShowPopupOnFocus="True" runat="server" />
                    <br /><br />
                    <telerik:RadButton ID="RadButton1" Text="Сохранить" OnClientClicking="OnClientClicking" OnClick="RadButton1_OnClick" CausesValidation="True" Skin="Windows7" runat="server" />
                    <telerik:RadButton ID="RadButton2" Text="Отмена" OnClientClicking="OnClientClicking_Cancel" CausesValidation="False" Skin="Windows7" runat="server" />
                </div>
            </telerik:RadToolTip>
            <asp:LinkButton ID="lbtnBack" CssClass="btn" OnCommand="lbtnBackNext_OnCommand" CommandName="Back" Visible="False" CausesValidation="False" runat="server"><em>&nbsp;</em><span>Назад</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnNext" CssClass="btn" OnCommand="lbtnBackNext_OnCommand" CommandName="Next" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>Далее</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnSendNow" CssClass="btn" OnClick="lbtnSendNow_OnClick" ValidationGroup="valGroup" Visible="False" CausesValidation="True" runat="server"><em>&nbsp;</em><span>Отправить немедленно</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnSchedule" CssClass="btn" ValidationGroup="valGroup" Visible="False" CausesValidation="True" runat="server"><em>&nbsp;</em><span>Запланировать</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnTest" CssClass="btn" OnClick="lbtnTest_OnClick" ValidationGroup="valGroup" Visible="False" CausesValidation="True" runat="server"><em>&nbsp;</em><span>Протестировать</span></asp:LinkButton>            
        </asp:Panel>
        <asp:Panel ID="pnlBtnEdit" CssClass="buttons clearfix" Visible="False" runat="server">
            <asp:LinkButton ID="lbtnSave" CssClass="btn" OnClick="lbtnSave_OnClick" ValidationGroup="valGroup" runat="server"><em>&nbsp;</em><span>Сохранить</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnSendNow2" OnClick="lbtnSendNow_OnClick" CssClass="btn" ValidationGroup="valGroup" OnClientClick="return confirm('Выполнить рассылку немедленно по всему списку получателей?');" runat="server"><em>&nbsp;</em><span>Разослать немедленно</span></asp:LinkButton>
            <asp:LinkButton ID="lbtnCancelMail" CssClass="btn" OnClick="lbtnCancelMail_OnClick" ValidationGroup="valGroup" OnClientClick="return confirm('Отменить рассылку?');" runat="server"><em>&nbsp;</em><span>Отменить рассылку</span></asp:LinkButton>
            <asp:HyperLink runat="server" ID="hlCancel" CssClass="cancel" Text="Отмена" />
        </asp:Panel>
</td></tr></table>
</asp:Content>