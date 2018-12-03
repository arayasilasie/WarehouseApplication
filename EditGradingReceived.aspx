<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditGradingReceived.aspx.cs" Inherits="WarehouseApplication.EditGradingReceived" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditGradingReceived.ascx" tagname="UIEditGradingReceived" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditGradingReceived ID="UIEditGradingReceived1" runat="server" />
</asp:Content>
