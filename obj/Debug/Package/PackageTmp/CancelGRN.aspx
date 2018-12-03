<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="CancelGRN.aspx.cs" Inherits="WarehouseApplication.CancelGRN" %>
<%@ Register src="UserControls/UIEditGRN.ascx" tagname="UIEditGRN" tagprefix="uc1" %>
<%@ Register src="UserControls/UICancelGRN.ascx" tagname="UICancelGRN" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc2:UICancelGRN ID="UICancelGRN1" runat="server" />
</asp:Content>
