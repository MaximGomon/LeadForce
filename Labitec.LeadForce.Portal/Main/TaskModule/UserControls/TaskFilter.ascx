<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskFilter.ascx.cs" Inherits="Labitec.LeadForce.Portal.Main.TaskModule.UserControls.TaskFilter" %>

<telerik:RadScriptBlock runat="server">
<script type="text/javascript">    
    function onDateClick(sender, eventArgs) {
        var radCalendar = $find("<%=rcRangeSelection.ClientID %>");
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");
        var startDate = radCalendar.get_rangeSelectionStartDate();
        var endDate = radCalendar.get_rangeSelectionEndDate();
        if (endDate && startDate){            
            startDatePicker.set_selectedDate(startDate);            
            endDatePicker.set_selectedDate(endDate);
        }
    }
    function OnDateSelected(sender, e) {        
        var radCalendar = $find("<%=rcRangeSelection.ClientID %>");
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");
        var startDate = startDatePicker.get_selectedDate();
        var endDate = endDatePicker.get_selectedDate();        
        if (startDate && endDate)
            radCalendar.set_datesInRange(startDate, endDate);
    }
    function Today() {
        console.log('today');
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");
        startDatePicker.set_selectedDate(new Date());        
        endDatePicker.set_selectedDate(new Date());
    }
    function CurrentWeek() {       
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");    
        var curr = new Date;
        var first = curr.getDate() - curr.getDay() + 1;
        var last = first + 6;
        var firstday = new Date(curr.setDate(first));
        var lastday = new Date(curr.setDate(last));
        startDatePicker.set_selectedDate(firstday);
        endDatePicker.set_selectedDate(lastday);
    }
</script>

<div class="task-filter clearfix">
    <ul class="filters clearfix">
        <li><asp:Image runat="server" ImageUrl="~/App_Themes/Default/images/icoAdd.gif"/><a href="javascript:Today();">Сегодня</a></li>
        <li><asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/icoAdd.gif"/><a href="javascript:CurrentWeek()">Текущая неделя</a></li>        
    </ul>
    <telerik:RadCalendar runat="server" ID="rcRangeSelection" AutoPostBack="false" RangeSelectionMode="ConsecutiveClicks" EnableViewSelector="true">
        <ClientEvents OnDateClick="onDateClick" />
    </telerik:RadCalendar>
    <div class="row">
        <label>Период с</label><telerik:RadDatePicker ID="rdpStartDate" AutoPostBack="true" OnSelectedDateChanged="period_OnSelectedDateChanged" runat="server"><ClientEvents OnDateSelected="OnDateSelected"/></telerik:RadDatePicker>
    </div>
    <div class="row">
        <label>Период по</label><telerik:RadDatePicker ID="rdpEndDate" AutoPostBack="true" OnSelectedDateChanged="period_OnSelectedDateChanged" runat="server"><ClientEvents OnDateSelected="OnDateSelected"/></telerik:RadDatePicker>
    </div>    
</div>
</telerik:RadScriptBlock>