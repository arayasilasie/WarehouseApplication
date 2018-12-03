<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="OpenGRNEdit.aspx.cs" Inherits="WarehouseApplication.OpenGRNEdit" %>
<%@ Register src="UserControls/UIOpenGRNEdit.ascx" tagname="UIOpenGRNEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIOpenGRNEdit ID="UIOpenGRNEdit1" runat="server" />
</asp:Content>
