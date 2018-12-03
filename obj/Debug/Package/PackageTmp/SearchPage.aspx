<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="WarehouseApplication.SearchPage" %>
<%@ Register src="UserControls/SearchPageNew.ascx" tagname="SearchPageNew" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<uc1:SearchPageNew ID="SearchPageNew1" runat="server" />
&nbsp;
</asp:Content>
