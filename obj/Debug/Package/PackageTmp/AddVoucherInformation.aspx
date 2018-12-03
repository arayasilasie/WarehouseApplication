<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddVoucherInformation.aspx.cs" Inherits="WarehouseApplication.AddVoucherInformation" Title="Untitled Page" %>
<%@ Register src="UserControls/InsertVoucherInformation.ascx" tagname="InsertVoucherInformation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:InsertVoucherInformation ID="InsertVoucherInformation1" runat="server" />
</asp:Content>
