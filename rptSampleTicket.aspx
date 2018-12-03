<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="rptSampleTicket.aspx.cs" Inherits="WarehouseApplication.rptSampleTicket" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">Sampling Ticket
<table>
<tr>
<td>Sampling Code:</td>
<td>
    <asp:Label ID="lblSamplingCode" runat="server" Text=""></asp:Label>
    </td>
</tr>
<tr>
<td>Serial No:</td>
<td>
    <asp:Label ID="lblSerialNo" runat="server" Text=""></asp:Label>
    </td>
</tr>
</table>
</asp:Content>
