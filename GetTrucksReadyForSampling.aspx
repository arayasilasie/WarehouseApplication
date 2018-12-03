<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GetTrucksReadyForSampling.aspx.cs" Inherits="WarehouseApplication.GetTrucksReadyForSampling" %>
<%@ Register src="UserControls/UIAddTrucksForSampling.ascx" tagname="UIAddTrucksForSampling" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddTrucksForSampling ID="UIAddTrucksForSampling1" runat="server" />
</asp:Content>
