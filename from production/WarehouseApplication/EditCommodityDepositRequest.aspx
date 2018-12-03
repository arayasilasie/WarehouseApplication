<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditCommodityDepositRequest.aspx.cs" Inherits="WarehouseApplication.EditCommodityDepositRequest" Title="Untitled Page" %>
<%@ Register src="UserControls/UIEditCommodityDepositrequest.ascx" tagname="UIEditCommodityDepositrequest" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <uc1:UIEditCommodityDepositrequest ID="UIEditCommodityDepositrequest1" 
        runat="server" />
</asp:Content>
