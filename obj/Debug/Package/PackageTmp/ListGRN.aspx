<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListGRN.aspx.cs" Inherits="WarehouseApplication.ListGRN" Title="Untitled Page" %>
<%@ Register src="UserControls/UISearchGRN.ascx" tagname="UISearchGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchGRN ID="UISearchGRN1" runat="server" />
</asp:Content>
