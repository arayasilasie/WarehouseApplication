<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WarehouseApplication.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="550px">
    <tr>
    <td>
        <asp:Label ID="lblErrorMessage" runat="server" Text="Label" CssClass="Message">
        </asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Button ID="btnOk" runat="server" Text="   Ok   " onclick="btnOk_Click" />
    </td>
    </tr>
    </table>

</asp:Content>
