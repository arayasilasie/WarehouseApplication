<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListStack.aspx.cs" Inherits="WarehouseApplication.ListStack"  EnableEventValidation="false" Title="Untitled Page" %>
<%@ Register src="UserControls/UISearchStack.ascx" tagname="UISearchStack" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UISearchStack ID="UISearchStack1" runat="server" />
</asp:Content>
