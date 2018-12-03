<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditApprovedGRNEditRequest.aspx.cs" Inherits="WarehouseApplication.EditApprovedGRNEditRequest" %>
<%@ Register src="UserControls/UIEditApprovedGRNEditRequest.ascx" tagname="UIEditApprovedGRNEditRequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditApprovedGRNEditRequest ID="UIEditApprovedGRNEditRequest1" 
        runat="server" />
</asp:Content>
