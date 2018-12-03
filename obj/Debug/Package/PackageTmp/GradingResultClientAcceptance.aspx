<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GradingResultClientAcceptance.aspx.cs" Inherits="WarehouseApplication.GradingResultClientAcceptance" Title="Untitled Page" %>
<%@ Register src="UserControls/UIAcceptGradingResult.ascx" tagname="UIAcceptGradingResult" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAcceptGradingResult ID="UIAcceptGradingResult1" runat="server" />
</asp:Content>
