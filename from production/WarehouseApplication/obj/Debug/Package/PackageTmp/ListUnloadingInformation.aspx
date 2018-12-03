<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListUnloadingInformation.aspx.cs" Inherits="WarehouseApplication.ListUnloadingInformation" Title="Untitled Page" %>
<%@ Register src="UserControls/UISearchUnloadingInformation.ascx" tagname="UISearchUnloadingInformation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchUnloadingInformation ID="UISearchUnloadingInformation1" 
        runat="server" />
</asp:Content>
