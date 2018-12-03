<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddSamplingResult.aspx.cs" Inherits="WarehouseApplication.AddSamplingResult" Title="Untitled Page" %>
<%@ Register src="UserControls/UIAddSamplingResult.ascx" tagname="UIAddSamplingResult" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
   
    <uc1:UIAddSamplingResult ID="UIAddSamplingResult1" runat="server" />
   
</asp:Content>
