<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ReCreateGRN.aspx.cs" Inherits="WarehouseApplication.ReCreateGRN" %>
<%@ Register src="UserControls/UIReCreateGRN.ascx" tagname="UIReCreateGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIReCreateGRN ID="UIReCreateGRN1" runat="server" />
</asp:Content>
