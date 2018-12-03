<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ViewGRN.aspx.cs" Inherits="WarehouseApplication.ViewGRN" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditGRN.ascx" tagname="UIEditGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditGRN ID="UIEditGRN1" runat="server" />
</asp:Content>
