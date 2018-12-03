<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListGrading.aspx.cs" Inherits="WarehouseApplication.ListGrading" %>
<%@ Register src="UserControls/UISearchGrading.ascx" tagname="UISearchGrading" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchGrading ID="UISearchGrading1" runat="server" />
</asp:Content>
