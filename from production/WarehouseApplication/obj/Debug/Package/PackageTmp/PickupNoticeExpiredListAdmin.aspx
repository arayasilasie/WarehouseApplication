<%@ Page Title="PickupNotice ExpiredList Admin" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="PickupNoticeExpiredListAdmin.aspx.cs" Inherits="WarehouseApplication.PickupNoticeExpiredListAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <%--   <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true" ID="ScriptManager1" />--%>
                        <uc1:Messages ID="Messages" runat="server" />
                        <span class="style1">
                            <%--  <asp:Button ID="btnShowError" runat="server" OnClick="btnShowError_Click" Text="Show Error Message" />
            <asp:Button ID="btnShowWarning" runat="server" OnClick="btnShowWarning_Click" Text="Show Warning Message" />
            <asp:Button ID="btnShowSuccess" runat="server" OnClick="btnShowSuccess_Click" Text="Show Success Message" />--%></span>
                        <table width="1000px">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="background-color: #88AB2D">
                                                <asp:Label ID="lblSearchPUN" runat="server" Text="Search Pickup Notice" ForeColor="White"
                                                    Width="900px" CssClass="style1"></asp:Label>
                                                <span class="style1">&nbsp; </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblExpirationDateFrom" runat="server" Text="Expiration Date From  :"
                                                    CssClass="style1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExpirationDateTo" runat="server" Text="Expiration Date To  :" CssClass="style1"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtExpirationDateFrom" runat="server" CssClass="style1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtExpirationDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtExpirationDateFrom">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpirationDateTo" runat="server" CssClass="style1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtExpirationDateTo_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtExpirationDateTo">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Print"
                                                    BackColor="#88AB2D" ForeColor="White" Width="100px" BorderStyle="None" CssClass="style1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- </form>--%><span class="style1"> </span>
            </td>
        </tr>
    </table>
</asp:Content>
