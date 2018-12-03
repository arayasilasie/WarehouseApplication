<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SamplingResult.aspx.cs" Inherits="WarehouseApplication.SamplingResult1" Title="Untitled Page" %>
<%@ Register src="UserControls/UISamplingResult.ascx" tagname="UISamplingResult" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    
    <uc1:UISamplingResult ID="UISamplingResult1" runat="server" />
    
</asp:Content>
