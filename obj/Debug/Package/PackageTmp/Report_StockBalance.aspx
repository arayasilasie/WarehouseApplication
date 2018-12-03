<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="Report_StockBalance.aspx.cs" Inherits="WarehouseApplication.StockBalanceReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div>
        <uc1:Messages ID="Messages1" runat="server" />
    </div>
    <div class="container">
        <%-- <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1">
                    </FlashViewerOptions>--%>
        <div id="StockBalanceReportForm" class="form" style="margin: 0 0 0 0; width: 100%">
            <div class="formHeader" align="center">
                STOCK BALANCE REPORT</div>
            <div id="leftSide" style="float: left; width: 60%;">
                <%-- <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1">
                    </FlashViewerOptions>--%>
                <div class="controlContainer">
                    <div class="leftControl" style="width: 33%">
                        <asp:Label ID="Label15" runat="server" Text="Warehouse"></asp:Label>
                    </div>
                    <div class="rightControl" style="width: 30%">
                        <asp:Label ID="Label5" runat="server" Text="Shed No."></asp:Label>
                    </div>
                    <div class="thirdControl" style="float: left; width: 32%">
                        <asp:Label ID="Label4" runat="server" Text="LIC Name"></asp:Label>
                    </div>
                </div>
                <div class="controlContainer">
                    <div class="leftControl" style="width: 33%">
                        <asp:DropDownList ID="cboWarehouse" runat="server" Width="150px" AppendDataBoundItems="True"
                            OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="Select Warehouse" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboWarehouse"
                            ErrorMessage="*" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                    <div class="rightControl" style="width: 30%">
                        <asp:DropDownList ID="cboShed" runat="server" Width="150px" AppendDataBoundItems="True"
                            OnSelectedIndexChanged="ddlShed_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboShed"
                            ErrorMessage="*" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                    <div class="thirdControl" style="float: left; width: 32%">
                        <asp:DropDownList ID="cboLIC" runat="server" Width="150px" AppendDataBoundItems="True">
                            <asp:ListItem Text="Select LIc" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboLIC"
                            ErrorMessage="*" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div id="rightSide" style="float: left; width: 28%;">
                <div class="controlContainer">
                    <div class="leftControl" style="width: 44%">
                        <asp:Label ID="Label1" runat="server" Text="From"></asp:Label>
                    </div>
                    <div class="rightControl" style="width: 44%">
                        <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                    </div>
                </div>
                <div class="controlContainer">
                    <div class="leftControl" style="width: 44%">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="99%"></asp:TextBox>
                        <cc1:CalendarExtender ID="startDate_CalendarExtender" runat="server" TargetControlID="txtStartDate">
                        </cc1:CalendarExtender>
                    </div>
                    <div class="rightControl" style="width: 44%">
                        <asp:TextBox ID="txtEndDate" runat="server" Width="99%"></asp:TextBox>
                        <cc1:CalendarExtender ID="endDate_CalendarExtender" runat="server" TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </div>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="None" 
                        ErrorMessage="End date should be greater than start date." 
                        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="CompareValidator1">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="buttonHolder" style="margin: 20px 0 0 10px; float: left;">
                <asp:Button ID="btnGenerate" runat="server" BackColor="#88AB2D" BorderStyle="None"
                    CssClass="style1" ForeColor="White" Text="Generate" Width="100px" OnClick="btnGenerate_Click" /></div>
        </div>
        <br />
        <div id="reportViewerHolder" style="clear: both;">
            <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Width="100%" ViewerType="AcrobatReader">
                <%-- <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1">
                    </FlashViewerOptions>--%>
            </ActiveReportsWeb:WebViewer>
        </div>
    </div>
</asp:Content>
