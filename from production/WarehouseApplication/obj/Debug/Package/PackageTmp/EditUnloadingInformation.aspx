<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditUnloadingInformation.aspx.cs" Inherits="WarehouseApplication.EditUnloadingInformation" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditUnloading.ascx" tagname="UIEditUnloading" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="warehouse.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    
      <uc1:UIEditUnloading ID="UIEditUnloading1" runat="server" />
</asp:Content>
