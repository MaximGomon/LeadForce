<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectMeetingTime.ascx.cs" Inherits="WebCounter.AdminPanel.UserControls.Task.SelectMeetingTime" %>

<%@ Register TagPrefix="scheduler" TagName="AdvancedForm" Src="~/UserControls/Task/SchedulerAdvancedForm/AdvancedForm.ascx" %>

<script src="<%= ResolveUrl("~/Scripts/AdvancedForm.js")%>" type="text/javascript"></script>

<script type="text/javascript">	
//<![CDATA[
    var schedulerTemplates = {};
    function schedulerFormCreated(scheduler, eventArgs) {
        var mode = eventArgs.get_mode();
        if (mode == Telerik.Web.UI.SchedulerFormMode.AdvancedInsert || mode == Telerik.Web.UI.SchedulerFormMode.AdvancedEdit) {
            var formElement = eventArgs.get_formElement();
            var templateKey = scheduler.get_id() + "_" + mode;
            var advancedTemplate = schedulerTemplates[templateKey];
            if (!advancedTemplate) {
                var schedulerElement = scheduler.get_element();
                var isModal = scheduler.get_advancedFormSettings().modal;
                advancedTemplate = new window.SchedulerAdvancedTemplate(schedulerElement, formElement, isModal);
                advancedTemplate.initialize();
                schedulerTemplates[templateKey] = advancedTemplate;
                scheduler.add_disposing(function () { schedulerTemplates[templateKey] = null; });
            }
        }
    }
//]]>
</script>

<telerik:RadScheduler ID="rScheduler"
						  CssClass="select-time-scheduler"
						  AllowDelete="true"
						  AllowInsert="true"
                          AllowEdit="true"
						  Height="485px"                          
						  Width="780px"
						  SelectedView="WeekView"                          
						  HoursPanelTimeFormat="HH:mm"
						  DisplayDeleteConfirmation="false"
                          ValidationGroup="groupAppointmentUpdate"
                          OnClientFormCreated="schedulerFormCreated"                          
						  OnAppointmentUpdate="rScheduler_OnAppointmentUpdate"
                          OnAppointmentInsert="rScheduler_OnAppointmentInsert"
                          OnAppointmentDelete="rScheduler_OnAppointmentDelete"
                          OnAppointmentDataBound="rScheduler_OnAppointmentDataBound"                                                    
                          AppointmentStyleMode="Default"
						  Skin="Windows7" runat="server">
						<AdvancedForm EnableCustomAttributeEditing="true" Modal="false" MaximumHeight="200px" TimeFormat="HH:mm" DateFormat="dd.MM.yyyy" />
						<MultiDayView UserSelectable="false" />
						<DayView UserSelectable="false" />
						<WeekView UserSelectable="true" />
						<MonthView UserSelectable="false" />
						<TimelineView UserSelectable="false" />
                        <AdvancedEditTemplate>
						    <scheduler:AdvancedForm runat="server" ID="AdvancedEditForm1" Mode="Edit" Subject='<%# Bind("Subject") %>'
							    Start='<%# Bind("Start") %>' End='<%# Bind("End") %>' IsResponsibleEnabled="false" />
						</AdvancedEditTemplate>
						<AdvancedInsertTemplate>
							<scheduler:AdvancedForm runat="server" ID="AdvancedInsertForm1" Mode="Insert" Subject='<%# Bind("Subject") %>'
								Start='<%# Bind("Start") %>' End='<%# Bind("End") %>' IsResponsibleEnabled="false" />
						</AdvancedInsertTemplate>
</telerik:RadScheduler>
