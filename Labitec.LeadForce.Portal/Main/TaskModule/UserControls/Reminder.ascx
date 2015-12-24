<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reminder.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.UserControls.Reminder" %>

<telerik:RadCodeBlock runat="server">
    <script type="text/javascript">
        function RadButtonOnClientClicking(sender, args) {
            var listbox = $find("<%= rlbRemindersList.ClientID %>");
            if (listbox.get_selectedItems().length == 0)
                args.set_cancel(true);                
        }
        function RadButtonOnClientClicked(sender, args) {
            var listbox = $find("<%= rlbRemindersList.ClientID %>");
            if (listbox.get_selectedItems().length != 0)
                HideNotification();
        }
        function AllRadButtonOnClientClicked(sender, args) {
            HideNotification();
        }
    </script>
</telerik:RadCodeBlock>

<div class="reminder-wrapper">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <telerik:RadListBox runat="server" ID="rlbRemindersList" Skin="Windows7" SelectionMode="Multiple" Width="490px" CssClass="reminder-items">
           <ItemTemplate>
                <div class="listbox-row clearfix">
                    <span class="col1"><%# DataBinder.Eval(Container, "Text")%></span>
                    <span class="col2"><%# DataBinder.Eval(Container, "Attributes['ModuleTitle']") %></span>
                    <span class="col3"><%# DataBinder.Eval(Container, "Attributes['ReminderDate']") %></span>
                </div>
           </ItemTemplate>   
        </telerik:RadListBox>
        <div class="cancel-buttons">
            <telerik:RadButton ID="rbtnCancel" runat="server" Text="Отменить" OnClientClicking="RadButtonOnClientClicking" OnClientClicked="RadButtonOnClientClicked" OnClick="rbtnCancel_OnClick" />
            <telerik:RadButton ID="rbtnCancelAll" runat="server" Text="Отменить все" OnClick="rbtnCancelAll_OnClick" OnClientClicked="AllRadButtonOnClientClicked" />
        </div>
        <div class="postpone-buttons">
            <telerik:RadComboBox runat="server" ID="rcbPosponePeriods" Skin="Windows7" ZIndex="10001">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="5 минут" Value="5" />
                    <telerik:RadComboBoxItem runat="server" Text="10 минут" Value="10" />
                    <telerik:RadComboBoxItem runat="server" Text="15 минут" Value="15" />
                    <telerik:RadComboBoxItem runat="server" Text="30 минут" Value="30" />
                    <telerik:RadComboBoxItem runat="server" Text="1 час" Value="60" />
                    <telerik:RadComboBoxItem runat="server" Text="2 часа" Value="120" />
                    <telerik:RadComboBoxItem runat="server" Text="4 часа" Value="240" />
                    <telerik:RadComboBoxItem runat="server" Text="8 часов" Value="480" />
                    <telerik:RadComboBoxItem runat="server" Text="1 день" Value="1440" />
                    <telerik:RadComboBoxItem runat="server" Text="2 дня" Value="2880" />
                    <telerik:RadComboBoxItem runat="server" Text="3 дня" Value="4320" />
                    <telerik:RadComboBoxItem runat="server" Text="4 дня" Value="5760" />
                    <telerik:RadComboBoxItem runat="server" Text="1 неделя" Value="10080" />
                    <telerik:RadComboBoxItem runat="server" Text="2 недели" Value="20160" />
                    <telerik:RadComboBoxItem runat="server" Text="3 недели" Value="30240" />
                    <telerik:RadComboBoxItem runat="server" Text="1 месяц" Value="0" />                
                </Items>
            </telerik:RadComboBox>
            <telerik:RadButton ID="rbtnPostpone" runat="server" Text="Отложить" OnClientClicking="RadButtonOnClientClicking" OnClientClicked="RadButtonOnClientClicked" OnClick="rbtnPostpone_OnClick" />
            <telerik:RadButton ID="rbtnPostponeAll" runat="server" Text="Отложить все" OnClick="rbtnPostponeAll_OnClick" OnClientClicked="AllRadButtonOnClientClicked" />
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>