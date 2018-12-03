<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ReportInventoryTransfer.aspx.cs" Inherits="WarehouseApplication.ReportInventoryTransfer" %>
<%@ Register assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" namespace="DataDynamics.ActiveReports.Web" tagprefix="ActiveReportsWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" height="190px" 
        width="998px" ReportName="rptInventoryTransfer.cs" ViewerType="AcrobatReader">
    </ActiveReportsWeb:WebViewer>
</asp:Content>
