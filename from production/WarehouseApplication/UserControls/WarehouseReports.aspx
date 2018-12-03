<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="WarehouseReports.aspx.cs" Inherits="WarehouseApplication.WarehouseReports" %>
<%@ Register src="UserControls/UIGRNSentbyDate.ascx" tagname="UIGRNSentbyDate" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIGRNSentbyDate ID="UIGRNSentbyDate1" runat="server" />
</asp:Content>
