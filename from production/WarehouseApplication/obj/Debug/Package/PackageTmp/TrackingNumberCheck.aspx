<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="TrackingNumberCheck.aspx.cs" Inherits="WarehouseApplication.TrackingNumberCheck" %>
<%@ Register src="UserControls/UITrackingNumberCheck.ascx" tagname="UITrackingNumberCheck" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UITrackingNumberCheck ID="UITrackingNumberCheck1" runat="server" />
</asp:Content>
