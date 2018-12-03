<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SearchSampleTicket.aspx.cs" Inherits="WarehouseApplication.SearchSampleTicket" %>
<%@ Register src="UserControls/UISearchSamplingTicket.ascx" tagname="UISearchSamplingTicket" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchSamplingTicket ID="UISearchSamplingTicket1" runat="server" />
</asp:Content>
