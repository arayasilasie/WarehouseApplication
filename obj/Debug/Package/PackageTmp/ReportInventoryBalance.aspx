<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ReportInventoryBalance.aspx.cs" Inherits="WarehouseApplication.ReportInventoryBalance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
  
    <div class="container">
    <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <div id="Header" class="formHeader" style="width: 87%; margin-left: 40px; margin-top: 5px;"
            align="center">
            <asp:Label ID="lblDetail" Text="INVENTORY BALANCE" Width="100%" runat="server"></asp:Label>
        </div>
   
    <div style="float: left; width: 87%; margin-left: 40px;">
        <div style="margin-bottom: 10px;">
            <div style="border: solid 1px #88AB2D; height: 70px;">
                <div style="margin-top: 10px; float: left; height: 26px; margin-left: 7px; width:22%">
                    <div style="height: 26px;">
                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Warehouse :"></asp:Label>
                    </div>
                    <div>
                            <asp:DropDownList ID="ddlWarehouse" runat="server" Width="150px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" 
                                AutoPostBack="True">
                                <asp:ListItem Text="Select Warehouse" Value=""></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <div style="margin-top: 10px; margin-left: 7px; float: left;width:22%"">
                    <div style="height: 26px;">
                            <asp:Label ID="lblShed" runat="server" CssClass="label" Text="Shed :"></asp:Label>
                    </div>
                    <div>
                            <asp:DropDownList ID="ddlShed" runat="server" Width="150px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlShed_SelectedIndexChanged" 
                            AutoPostBack="True">
                                <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <div style="margin-top: 10px; float: left; margin-left: 7px; width:22%"">
                    <div style="height: 26px;">
                        <asp:Label ID="lblLIC0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                    </div>
                    <div>
                            <asp:DropDownList ID="ddlLIC" runat="server" Width="150px" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Text="Select LIc" Value=""></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <div style="margin-top: 10px; float: left; margin-left: 20px;width:22%"">
                    <div style="height: 26px;">
                    </div>
                    <div>
                            <asp:Button ID="btnGenerate" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                CssClass="style1" ForeColor="White" Text="Generate" Width="100px" 
                                OnClick="btnGenerate_Click" />
                    </div>
                </div>
               
            </div>
        </div>
        
        <div>   <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Height="800px" Width="850px"
                    ViewerType="AcrobatReader">
                   <%-- <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1">
                    </FlashViewerOptions>--%>
                </ActiveReportsWeb:WebViewer>
           
                </div>
                </div> 
   </ContentTemplate>
   </asp:UpdatePanel>
    </div>
   
</asp:Content>
