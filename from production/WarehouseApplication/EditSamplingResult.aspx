<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditSamplingResult.aspx.cs" Inherits="WarehouseApplication.EditSamplingResult" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditSamplingResult.ascx" tagname="UIEditSamplingResult" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditSamplingResult ID="UIEditSamplingResult1" runat="server" />
</asp:Content>
