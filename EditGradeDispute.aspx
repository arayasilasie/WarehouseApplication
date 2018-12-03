<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditGradeDispute.aspx.cs" Inherits="WarehouseApplication.EditGradeDispute"  EnableEventValidation="false"  %>
<%@ Register src="UserControls/UIEditGradeDispute.ascx" tagname="UIEditGradeDispute" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditGradeDispute ID="UIEditGradeDispute1" runat="server" />
</asp:Content>
