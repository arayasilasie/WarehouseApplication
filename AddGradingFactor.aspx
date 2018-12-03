<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddGradingFactor.aspx.cs" Inherits="WarehouseApplication.AddGradingFactor" %>
<%@ Register src="UserControls/UIGradingFactors.ascx" tagname="UIGradingFactors" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIGradingFactors ID="UIGradingFactors1" runat="server" />
</asp:Content>
