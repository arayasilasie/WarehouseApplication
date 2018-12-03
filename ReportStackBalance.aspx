<%@ Page Title="Report Stack Balance" Language="C#" MasterPageFile="~/pTop.Master"
    AutoEventWireup="true" CodeBehind="ReportStackBalance.aspx.cs" Inherits="WarehouseApplication.ReportStackBalance" %>
    <%@ Register Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table style="background-color: White">
        <tr>
            <td>
                <asp:Label ID="lblInventoryCoordinatorload" runat="server" Text="LIC :" CssClass="label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblShed" runat="server" Text="Shed :" CssClass="label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStackNo" runat="server" Text="Stack No :" CssClass="label"></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="drpInventoryCoordinatorLoad" runat="server" Width="145px" 
                    onselectedindexchanged="drpInventoryCoordinatorLoad_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="drpShed" runat="server" OnSelectedIndexChanged="drpShed_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="drpStackNo" runat="server" Width="145px" CssClass="style1">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                    BackColor="#88AB2D" ForeColor="White" Width="80px" BorderStyle="None" />
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnExcel" runat="server"  Text="Export"
                    BackColor="#88AB2D" ForeColor="White" Width="80px" BorderStyle="None" 
                    onclick="btnExcel_Click" /></td>
        </tr>
    </table>
      <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" height="800px" 
            width="1000px" ViewerType="AcrobatReader" >
<FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1"></FlashViewerOptions>
        </ActiveReportsWeb:WebViewer>
</asp:Content>
