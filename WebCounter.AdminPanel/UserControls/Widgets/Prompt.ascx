<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Prompt.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Widgets.Prompt" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            var lastShownButton;
            function showItemByIndex(index) {
                // gets reference to the rotator object
                var oRotator = $find("<%= rrPrompt.ClientID %>");

                // Sets currently shown item by its index
                oRotator.set_currentItemIndex(index);

                if (lastShownButton)
                    lastShownButton.removeClass("active");


                var currentButton = getButtonByIndex(index);
                console.log(currentButton);
                currentButton.addClass("active");
                lastShownButton = currentButton;
            }

            function OnClientItemShown(oRotator, args) {
                console.log(lastShownButton);
                var currentIndex = args.get_item().get_index();

                if (lastShownButton)
                    lastShownButton.removeClass("active");

                var currentButton = getButtonByIndex(currentIndex);
                currentButton.addClass("active");
                lastShownButton = currentButton;
            }

            function getButtonByIndex(index) {
                var buttonIdSelector = String.format("#Button{0}:first", index);
                var currentButton = $telerik.$(buttonIdSelector);

                return currentButton;
            }

        </script>
    </telerik:RadCodeBlock>    
    
    
    
<telerik:RadRotator ID="rrPrompt"  DataSourceID="edsPrompts" OnDataBound="rrPrompt_OnDataBound" OnClientItemShown="OnClientItemShown" runat="server" FrameDuration="10000" RotatorType="AutomaticAdvance" ScrollDirection="Up" Width="170px">
    <ItemTemplate>
        <div style="width: 170px; height: 200px; padding: 0 6px;">
            <%# Server.HtmlEncode(Eval("Description").ToString())%>
        </div>
    </ItemTemplate>
</telerik:RadRotator>
<asp:Panel ID="plNavigation" CssClass="prompt-navigation-container" runat="server"></asp:Panel>
    
<asp:EntityDataSource ID="edsPrompts" runat="server" AutoGenerateWhereClause="false" EntitySetName="tbl_Term" ConnectionString="name=WebCounterEntities" DefaultContainerName="WebCounterEntities" />