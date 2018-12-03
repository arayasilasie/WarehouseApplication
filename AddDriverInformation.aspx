<%@ Page Language="C#" MasterPageFile="~/pTop.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AddDriverInformation.aspx.cs" Inherits="WarehouseApplication.WebForm6" Title="Untitled Page" %>
<%@ Register src="UserControls/InsertDriverInformation.ascx" tagname="InsertDriverInformation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:InsertDriverInformation ID="InsertDriverInformation1" runat="server" />
</asp:Content>
