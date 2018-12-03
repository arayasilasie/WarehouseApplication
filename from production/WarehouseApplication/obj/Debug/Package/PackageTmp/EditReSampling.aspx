<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditReSampling.aspx.cs" Inherits="WarehouseApplication.EditReSampling" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditMoisstureReSamplingRequest.ascx" tagname="UIEditMoisstureReSamplingRequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditMoisstureReSamplingRequest ID="UIEditMoisstureReSamplingRequest1" 
        runat="server" />
</asp:Content>
