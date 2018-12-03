<%@ Page Language="C#" MasterPageFile="~/pTop.Master" MaintainScrollPositionOnPostback="false" AutoEventWireup="true" CodeBehind="AddGRN.aspx.cs" Inherits="WarehouseApplication.AddGRN" Title="Untitled Page" %>
<%@ Register src="UserControls/UIAddGRN.ascx" tagname="UIAddGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddGRN ID="UIAddGRN1" runat="server" />
</asp:Content>
