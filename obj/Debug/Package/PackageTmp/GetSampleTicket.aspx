<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GetSampleTicket.aspx.cs" Inherits="WarehouseApplication.GetSampleTicket" Title="Untitled Page" %>
<%@ Register src="UserControls/UIGetSamplingTicketById.ascx" tagname="UIGetSamplingTicketById" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">

 <uc1:UIGetSamplingTicketById ID="UIGetSamplingTicketById1" runat="server" />

</asp:Content>
