<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="Report_WBServiceProvider.aspx.cs" Inherits="WarehouseApplication.Report_WBServiceProvider" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="ActiveReports.Web, Version=6.0.2250.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="Messages.ascx" tagname="Messages" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<link href="messages.css" rel="stylesheet" type="text/css" />
    <div class="container">                                          
    <div id="WBServiceProviderReportForm" class="form" style=" margin: 0 0 0 0; width:100%; border-bottom:3px solid green; height:100px;">  
     <div class="formHeader" align="center">WEIGH-BRIDG SERVICE PROVIDER REPORT</div>

     <div id="leftSide" style="float:left; width:50%; ">
         <%-- <FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1">
                    </FlashViewerOptions>--%>  
                
          <div class="controlContainer">    
            <div class="leftControl" style="width:33%">   
                <asp:Label ID="Label15" runat="server" Text="Warehouse"></asp:Label>
            </div>  
                  
            <div class="rightControl" style="width:30%">  
                <asp:Label ID="Label5" runat="server" Text="WB-Service provider"></asp:Label>              
            </div>  
            
            <div class="thirdControl" style="float:left; width:32%" align="center">  
                <asp:Label ID="Label16" runat="server" Text="Service Type"></asp:Label>              
            </div>                    
          </div> 
           
          <div class="controlContainer">    
            <div class="leftControl" style="width:33%">   
                <asp:DropDownList ID="cboWarehouse" runat="server" Width="120px" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="cboWarehouse_SelectedIndexChanged">
                    <asp:ListItem Text="Select Warehouse" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="*" ForeColor="Tomato" InitialValue="" ControlToValidate="cboWarehouse"></asp:RequiredFieldValidator>
            </div>        
            <div class="rightControl" style="width:30%">   
                <asp:DropDownList ID="cboWBServiceProvider" runat="server" Width="150px" 
                    AppendDataBoundItems="True" AutoPostBack="True">
                    <asp:ListItem Text="Select Provider" Value="00000000-0000-0000-0000-000000000000"></asp:ListItem>                    
                </asp:DropDownList>
            </div> 
            <div class="thirdControl" style="float:left; width:32%" align="center">                                

                <asp:DropDownList ID="cboServiceType" runat="server" Width="100px" 
                    AppendDataBoundItems="True" AutoPostBack="True">
                    <asp:ListItem Text="Both" Value="0"></asp:ListItem>  
                    <asp:ListItem Text="Unloaded" Value="1"></asp:ListItem> 
                    <asp:ListItem Text="Loaded" Value="2"></asp:ListItem>                   
                </asp:DropDownList>

            </div>                     
          </div>  
                        
     </div>

     <div id="rightSide"  style="float:left; width:28%; ">
            
          <div class="controlContainer">    
            <div class="leftControl" style="width:44%">   
                <asp:Label ID="Label1" runat="server" Text="From"></asp:Label>
            </div>        
            <div class="rightControl" style="width:44%">  
                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>            
            </div>        
          </div>   
             
          <div class="controlContainer">    
            <div class="leftControl" style="width:44%">   
                <asp:TextBox ID="txtStartDate" runat="server" Width="99%"></asp:TextBox> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtStartDate" runat="server" ErrorMessage="*" ForeColor="Tomato">
                </asp:RequiredFieldValidator>
                <cc1:calendarextender ID="startDate_CalendarExtender" runat="server" 
                    TargetControlID="txtStartDate">
                </cc1:calendarextender>
            </div>        
            <div class="rightControl" style="width:44%">  
                <asp:TextBox ID="txtEndDate" runat="server" Width="99%"></asp:TextBox> 
                <cc1:calendarextender ID="endDate_CalendarExtender" runat="server" 
                    TargetControlID="txtEndDate">
                </cc1:calendarextender>                          
            </div>                    
          </div>   
                      
     </div>

     <div id="buttonHolder" style="margin:5px 0 0 10px; float:left;">
     <asp:Button ID="btnGenerate" runat="server" BackColor="#88AB2D" BorderStyle="None"
        CssClass="style1" ForeColor="White" Text="Generate" Width="100px" 
             onclick="btnGenerate_Click" />
             </div>
             <div id="Div1" style="margin:5px 0 0 10px; float:left;">
         <asp:Button ID="Button1" runat="server" BackColor="#88AB2D" BorderStyle="None"
        CssClass="style1" ForeColor="White" Text="Export" Width="100px" 
                     onclick="Button1_Click" /></div>
</div>
        <table style="width:100%">
            <tr><td class="style1"> 
                <uc1:Messages ID="Messages1" runat="server" />
                </td></tr>
        </table>    

         <div id="reportViewerHolder" style="clear:both;">
            <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Width="100%" ViewerType="AcrobatReader">
              </ActiveReportsWeb:WebViewer>  
         </div>
    </div>
</asp:Content>
