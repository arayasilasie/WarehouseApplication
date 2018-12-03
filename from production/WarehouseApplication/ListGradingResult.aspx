<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListGradingResult.aspx.cs" Inherits="WarehouseApplication.ListGradingResult" Title="Untitled Page" %>
<%@ Register src="UserControls/UIGetGradingResults.ascx" tagname="UIGetGradingResults" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIGetGradingResults ID="UIGetGradingResults1" runat="server" />
</asp:Content>
