<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GRNAcceptance.aspx.cs" Inherits="WarehouseApplication.GRNAcceptance" Title="Untitled Page" %>
<%@ Register src="UserControls/UIClientAcceptGRN.ascx" tagname="UIClientAcceptGRN" tagprefix="uc1" %>
<%@ Register src="UserControls/UIAcceptGradingResult.ascx" tagname="UIAcceptGradingResult" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    
    <uc1:UIClientAcceptGRN ID="UIClientAcceptGRN1" runat="server" />
    
</asp:Content>
