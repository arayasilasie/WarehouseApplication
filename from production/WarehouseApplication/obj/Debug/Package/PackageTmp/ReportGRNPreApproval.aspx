<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ReportGRNPreApproval.aspx.cs" Inherits="WarehouseApplication.ReportGRNPreApproval" %>
<%@ Register assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" namespace="DataDynamics.ActiveReports.Web" tagprefix="ActiveReportsWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
   


   
    <div class="container">
    <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <div id="Header" class="formHeader" style="width: 95%; margin-left: 20px; margin-top: 5px;"
            align="center">
            <asp:Label ID="lblDetail" Text="GRN PRE APPROVAL REPORT" Width="100%" runat="server"></asp:Label>
        </div>
   
    <div style="float: left; width: 95%; margin-left: 20px;">
        <div style="margin-bottom: 10px;">
            <div style="border: solid 1px #88AB2D; height: 70px;">
                <div style="margin-top: 10px; float: left; height: 26px; margin-left: 20px; width:22%">
                    <div style="height: 26px;">
                        <asp:Label ID="lblLIC0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                    </div>
                    <div>
                     <asp:DropDownList ID="ddlLIC" runat="server" Width="150px" 
                                AppendDataBoundItems="True">
                                <asp:ListItem Text="Select LIc" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                         ErrorMessage="*" Display="Dynamic" ControlToValidate="ddlLIC"></asp:RequiredFieldValidator>
                            
                    </div>
                </div>
                <div style="margin-top: 10px; margin-left: 7px; float: left;width:22%"">
                    <div style="height: 26px;">
                         
                    </div>
                    <div>
                           <asp:Button ID="btnGenerate" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                CssClass="style1" ForeColor="White" Text="Generate" Width="100px" 
                                OnClick="btnGenerate_Click" />
                    </div>
                </div>
              
                <div style="margin-top: 10px; float: left; margin-left: 20px;width:22%"">
                    <div style="height: 26px;">
                    </div>
                    <div>
                            
                    </div>
                </div>
               
            </div>
        </div>
        
        <div>   <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Height="95%" Width="67%"
                    ViewerType="AcrobatReader">
                </ActiveReportsWeb:WebViewer>
           
                </div>
                </div> 
   </ContentTemplate>
   </asp:UpdatePanel>
    </div>
  
  <%--  <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" height="46" 
        width="345">
    </ActiveReportsWeb:WebViewer--%>
  
</asp:Content>
