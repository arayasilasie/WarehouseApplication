<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListRequestEditForApprovedGRN.aspx.cs" Inherits="WarehouseApplication.ListRequestEditForApprovedGRN" %>
<%@ Register src="UserControls/UIListRequestEditForApprovedGRN.ascx" tagname="UIListRequestEditForApprovedGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIListRequestEditForApprovedGRN ID="UIListRequestEditForApprovedGRN1" 
        runat="server" />
</asp:Content>
