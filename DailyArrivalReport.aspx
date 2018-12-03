<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="DailyArrivalReport.aspx.cs" Inherits="WarehouseApplication.DailyArrivalReport" %>
<%@ Register TagPrefix="ActiveReportsWeb" Namespace="DataDynamics.ActiveReports.Web" Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ShowHand
        {
            cursor: pointer;
        }
        .style1
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="1000px" bgcolor="white">
        <tr>
            <td>
                      <span class="style1">
                            <table width="1000px">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="background-color: #88AB2D" colspan="4">
                                                <asp:Label ID="lblSearchArrival" runat="server" Text="Search Daily Arrival" ForeColor="White"
                                                    Width="900px" CssClass="style1"></asp:Label>
                                                <span class="style1">&nbsp; </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFrom" runat="server" Text="From:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateFrom" runat="server" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtDateFrom"></cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTo" runat="server" Width="150px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtTo"></cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                                <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </td>
                            </tr>
                        </table>                
                <%-- </form>--%><span class="style1"> </span>
            </td>
        </tr>
    </table>
</asp:Content>
