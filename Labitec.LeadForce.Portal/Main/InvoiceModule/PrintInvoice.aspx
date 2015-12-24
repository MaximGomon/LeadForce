<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintInvoice.aspx.cs" Inherits="Labitec.LeadForce.Portal.Main.InvoiceModule.PrintInvoice" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.1.12.611, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">   
    body { padding: 0px;margin: 0px}
    form, div#content {padding: 0px;height: 100%;margin: 0px}
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width:100%;height:100%">
        <div id="content"><telerik:ReportViewer ID="invoiceReportViewer" runat="server" Width="100%" Height="1100px" /></div>
    </form>
</body>
</html>
