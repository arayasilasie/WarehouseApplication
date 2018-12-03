<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListInbox.aspx.cs" Inherits="WarehouseApplication.ListInbox" %>
<%@ Register src="UserControls/WarehouseInbox.ascx" tagname="WarehouseInbox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:WarehouseInbox ID="WarehouseInbox1" runat="server" />
</asp:Content>
