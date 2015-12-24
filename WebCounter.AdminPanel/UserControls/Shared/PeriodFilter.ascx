<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeriodFilter.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Shared.PeriodFilter" %>

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
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");
        startDatePicker.set_selectedDate(new Date());        
        endDatePicker.set_selectedDate(new Date());
    }
    function CurrentWeek() {       
        var startDatePicker = $find("<%=rdpStartDate.ClientID %>");
        var endDatePicker = $find("<%=rdpEndDate.ClientID %>");                    
        var firstday = new Date(<%= FirstDayOfWeek.Year %>, <%= FirstDayOfWeek.Month - 1 %>, <%= FirstDayOfWeek.Day %>);        
        var lastday = new Date(<%= LastDayOfWeek.Year %>, <%= LastDayOfWeek.Month - 1 %>, <%= LastDayOfWeek.Day %>);        
        startDatePicker.set_selectedDate(firstday);
        endDatePicker.set_selectedDate(lastday);
    }
</script>

<div class="task-filter clearfix">
    <ul class="filters clearfix">
        <li><asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/icoAdd.gif"/><a href="javascript:Today();">Сегодня</a></li>
        <li><asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Default/images/icoAdd.gif"/><a href="javascript:CurrentWeek()">Текущая неделя</a></li>        
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