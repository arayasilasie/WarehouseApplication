<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListSearchableInboxDetail.aspx.cs" Inherits="WarehouseApplication.ListSearchableInboxDetail" %>
<%@ Register src="~/UserControls/UISearchableInboxDetail.ascx" tagname="UISearchableInboxDetail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchableInboxDetail ID="UISearchalbeInboxDetail1" runat="server" />
</asp:Content>
