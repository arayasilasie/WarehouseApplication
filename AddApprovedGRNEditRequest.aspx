<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddApprovedGRNEditRequest.aspx.cs" Inherits="WarehouseApplication.AddApprovedGRNEditRequest" %>
<%@ Register src="UserControls/UIAddRequestforEditGRN.ascx" tagname="UIAddRequestforEditGRN" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddRequestforEditGRN ID="UIAddRequestforEditGRN1" runat="server" />
</asp:Content>
