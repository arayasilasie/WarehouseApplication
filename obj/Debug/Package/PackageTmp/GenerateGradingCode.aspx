<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GenerateGradingCode.aspx.cs" Inherits="WarehouseApplication.GenerateGradingCode" Title="Generate Grading Code" EnableSessionState="True" %>
<%@ Register src="UserControls/UIGetGradingCode.ascx" tagname="UIGetGradingCode" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    
    
    
    <uc1:UIGetGradingCode ID="UIGetGradingCode1" runat="server" />
    
    
    
</asp:Content>
