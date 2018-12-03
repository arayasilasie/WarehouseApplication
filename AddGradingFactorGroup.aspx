<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddGradingFactorGroup.aspx.cs" Inherits="WarehouseApplication.AddGradingFactorGroup" %>
<%@ Register src="UserControls/UIAddGradingFactorGroup.ascx" tagname="UIAddGradingFactorGroup" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddGradingFactorGroup ID="UIAddGradingFactorGroup1" runat="server" />
</asp:Content>
