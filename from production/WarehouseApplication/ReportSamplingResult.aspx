<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ReportSamplingResult.aspx.cs" Inherits="WarehouseApplication.ReportSamplingResult" %>
<%@ Register assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" namespace="DataDynamics.ActiveReports.Web" tagprefix="ActiveReportsWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" height="46" 
        width="345">
    </ActiveReportsWeb:WebViewer>
</asp:Content>
