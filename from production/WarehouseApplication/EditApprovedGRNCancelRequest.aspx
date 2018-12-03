<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditApprovedGRNCancelRequest.aspx.cs" Inherits="WarehouseApplication.EditApprovedGRNCancelRequest" %>
<%@ Register src="UserControls/UIEditApprovedGRNCancelRequest.ascx" tagname="UIEditApprovedGRNCancelRequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditApprovedGRNCancelRequest ID="UIEditApprovedGRNCancelRequest1" 
        runat="server" />
</asp:Content>
