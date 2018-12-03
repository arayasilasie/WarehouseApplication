<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="UpdateGRNNumber.aspx.cs" Inherits="WarehouseApplication.UpdateGRNNumber" %>
<%@ Register src="UserControls/UIUpdateGRNNumber.ascx" tagname="UIUpdateGRNNumber" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIUpdateGRNNumber ID="UIUpdateGRNNumber1" runat="server" />
</asp:Content>
