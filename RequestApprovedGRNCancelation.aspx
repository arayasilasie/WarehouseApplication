<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="RequestApprovedGRNCancelation.aspx.cs" Inherits="WarehouseApplication.RequestApprovedGRNCancelation" %>
<%@ Register src="UserControls/UIAddApprovedGRNCancelationRequest.ascx" tagname="UIAddApprovedGRNCancelationRequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddApprovedGRNCancelationRequest ID="UIAddApprovedGRNCancelationRequest1" 
        runat="server" />
</asp:Content>
