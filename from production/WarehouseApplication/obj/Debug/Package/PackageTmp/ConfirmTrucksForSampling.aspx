<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ConfirmTrucksForSampling.aspx.cs" Inherits="WarehouseApplication.ConfirmTrucksForSampling" %>
<%@ Register src="UserControls/UIConfirmTrucks.ascx" tagname="UIConfirmTrucks" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIConfirmTrucks ID="UIConfirmTrucks1" runat="server" />
</asp:Content>
