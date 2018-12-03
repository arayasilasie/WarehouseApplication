<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListSamplingResult.aspx.cs" Inherits="WarehouseApplication.ListSamplingResult" Title="Untitled Page" %>
<%@ Register src="UserControls/UISamplingResultSearch.ascx" tagname="UISamplingResultSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISamplingResultSearch ID="UISamplingResultSearch1" runat="server" />
</asp:Content>
