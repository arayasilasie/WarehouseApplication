<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddCommodityGradeGradingFactor.aspx.cs" Inherits="WarehouseApplication.AddCommodityGradeGradingFactor" %>
<%@ Register src="UserControls/UIAddCommodityGradeGradingFactor.ascx" tagname="UIAddCommodityGradeGradingFactor" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIAddCommodityGradeGradingFactor ID="UIAddCommodityGradeGradingFactor1" 
        runat="server" />
</asp:Content>
