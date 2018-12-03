<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddStack.aspx.cs" Inherits="WarehouseApplication.AddStack" Title="Untitled Page" EnableEventValidation="false" %>
<%@ Register src="UserControls/UIAddStack.ascx" tagname="UIAddStack" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddStack ID="UIAddStack1" runat="server" />
</asp:Content>
