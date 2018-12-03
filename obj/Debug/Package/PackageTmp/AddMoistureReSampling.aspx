<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddMoistureReSampling.aspx.cs" Inherits="WarehouseApplication.AddMoistureReSampling" %>
<%@ Register src="UserControls/UIMoistureResamplingRequest.ascx" tagname="UIMoistureResamplingRequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIMoistureResamplingRequest ID="UIMoistureResamplingRequest1" 
        runat="server" />
</asp:Content>
