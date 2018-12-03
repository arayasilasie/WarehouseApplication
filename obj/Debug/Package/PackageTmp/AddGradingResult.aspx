<%@ Page Language="C#" MasterPageFile="~/pTop.Master" EnableEventValidation="false" AutoEventWireup="false" CodeBehind="AddGradingResult.aspx.cs" Inherits="WarehouseApplication.AddGradingResult" Title="Untitled Page" %>
<%@ Register src="UserControls/UIAddGradingRecived.ascx" tagname="UIAddGradingRecived" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddGradingRecived ID="UIAddGradingRecived1" runat="server" />
</asp:Content>
