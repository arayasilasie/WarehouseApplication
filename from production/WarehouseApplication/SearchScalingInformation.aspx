<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SearchScalingInformation.aspx.cs" Inherits="WarehouseApplication.SearchScalingInformation" %>
<%@ Register src="UserControls/UISearchScaling.ascx" tagname="UISearchScaling" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchScaling ID="UISearchScaling1" runat="server" />
</asp:Content>
