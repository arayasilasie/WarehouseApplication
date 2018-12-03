<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListRequestCancelForApprovedGRN.aspx.cs" Inherits="WarehouseApplication.ListRequestCancelForApprovedGRN" %>
<%@ Register src="UserControls/UIListRequestCancelForApprovedGRN.ascx" tagname="UIListRequestCancelForApprovedGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIListRequestCancelForApprovedGRN ID="UIListRequestCancelForApprovedGRN1" 
        runat="server" />
</asp:Content>
