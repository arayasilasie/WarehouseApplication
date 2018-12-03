<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SLAData.aspx.cs" Inherits="WarehouseApplication.SLAData" %>
<%@ Register src="UserControls/UISLAData.ascx" tagname="UISLAData" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISLAData ID="UISLAData1" runat="server" />
</asp:Content>
