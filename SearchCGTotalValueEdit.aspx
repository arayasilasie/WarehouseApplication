<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SearchCGTotalValueEdit.aspx.cs" Inherits="WarehouseApplication.SearchCGTotalValueEdit" %>
<%@ Register src="UserControls/UIEditCGTotalValue.ascx" tagname="UIEditCGTotalValue" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditCGTotalValue ID="UIEditCGTotalValue1" runat="server" />
</asp:Content>
