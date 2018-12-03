<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddCommodityDepositRequest.aspx.cs"  Inherits="WarehouseApplication.WebForm1" Title="Untitled Page" %>
<%@ Register src="UserControls/ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>
<%@ Register src="UserControls/InsertCommodityDepositrequest.ascx" tagname="AddCommodityDepositrequest" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
 
        
    <uc2:AddCommodityDepositrequest ID="AddCommodityDepositrequest1" 
        runat="server" EnableViewState="true"  />

        
</asp:Content>
