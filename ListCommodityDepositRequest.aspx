<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListCommodityDepositRequest.aspx.cs" Inherits="WarehouseApplication.ListCommodityDepositRequest" Title="Untitled Page" %>
<%@ Register src="UserControls/ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>
<%@ Register src="UserControls/SearchCommodityDepositRequest.ascx" tagname="SearchCommodityDepositRequest" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 192px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<uc2:SearchCommodityDepositRequest ID="SearchCommodityDepositRequest1" 
            runat="server" />
     
   
</asp:Content>
