﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerForm.aspx.cs" Inherits="WarehouseApplication.ReportViewerForm" %>


<%@ Register assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" namespace="DataDynamics.ActiveReports.Web" tagprefix="ActiveReportsWeb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
        <ActiveReportsWeb:WebViewer ID="wvReportViewer" runat="server" height="100%" 
            width="100%" ViewerType="AcrobatReader" >
<FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1"></FlashViewerOptions>
        </ActiveReportsWeb:WebViewer>
       
    </div>
    </form>
</body>
</html>
