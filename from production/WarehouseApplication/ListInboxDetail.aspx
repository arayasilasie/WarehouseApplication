<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListInboxDetail.aspx.cs" Inherits="WarehouseApplication.ListInboxDetail" %>
<%@ Register src="UserControls/UIInboxDetail.ascx" tagname="UIInboxDetail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIInboxDetail ID="UIInboxDetail1" runat="server" />
</asp:Content>
