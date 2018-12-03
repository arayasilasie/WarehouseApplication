<%@ Page Title="Add New Grade Dispute" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddGradeDispute.aspx.cs" Inherits="WarehouseApplication.AddGradeDispute" EnableEventValidation="false" %>
<%@ Register src="UserControls/UIAddGradeDispute.ascx" tagname="UIAddGradeDispute" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddGradeDispute ID="UIAddGradeDispute1" runat="server" />
</asp:Content>
