<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditScaling.aspx.cs" Inherits="WarehouseApplication.EditScaling" %>
<%@ Register src="UserControls/UIEditScaling.ascx" tagname="UIEditScaling" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditScaling ID="UIEditScaling1" runat="server" />
</asp:Content>
