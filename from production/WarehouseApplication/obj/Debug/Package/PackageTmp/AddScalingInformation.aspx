<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddScalingInformation.aspx.cs" Inherits="WarehouseApplication.AddScalingInformation" Title="Untitled Page" %>
<%@ Register src="UserControls/UIAddScalingInformation.ascx" tagname="UIAddScalingInformation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddScalingInformation ID="UIAddScalingInformation1" runat="server" />
</asp:Content>
