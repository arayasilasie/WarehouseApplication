<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ConsignmentExpiryList.aspx.cs" Inherits="WarehouseApplication.ConsignmentExpiry" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register TagPrefix="ActiveReportsWeb" Namespace="DataDynamics.ActiveReports.Web" Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" %>
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
                        
                        <uc1:Messages ID="Messages" runat="server" />
                        <span class="style1">
                            <table width="1000px">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="background-color: #88AB2D">
                                                <asp:Label ID="lblSearchPUN" runat="server" Text="Search Expired Bonded Yard" ForeColor="White"
                                                    Width="900px" CssClass="style1"></asp:Label>
                                                <span class="style1">&nbsp; </span>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                            <ActiveReportsWeb:WebViewer id="celViewer" runat="server" ViewerType="AcrobatReader"
                                                                Width="990px"  CssClass="ActiveReportViewer" 
            
                                                    PdfExportOptions-Permissions="AllowPrint, AllowModifyContents, AllowCopy, AllowModifyAnnotations, AllowFillIn, AllowAccessibleReaders, AllowAssembly" 
                                                        Visible="False" >
                                                        <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1"></FlashViewerOptions>
                                                </ActiveReportsWeb:WebViewer></td></tr>
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
