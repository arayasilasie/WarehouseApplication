<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditTrucksMissingStatus.aspx.cs" Inherits="WarehouseApplication.EditTrucksMissingStatus" %>
<%@ Register src="UserControls/UIAbsentTrucks.ascx" tagname="UIAbsentTrucks" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAbsentTrucks ID="UIAbsentTrucks1" runat="server" />
</asp:Content>
