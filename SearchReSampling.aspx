<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SearchReSampling.aspx.cs" Inherits="WarehouseApplication.SearchReSampling" Title="Untitled Page" %>
<%@ Register src="UserControls/UISearchResampling.ascx" tagname="UISearchResampling" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchResampling ID="UISearchResampling1" runat="server" />
</asp:Content>
