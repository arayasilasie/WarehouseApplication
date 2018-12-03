<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditGradingResult.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditGradingResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../warehouse.css" rel="stylesheet" type="text/css" />

     <fieldset style="height: auto; width: 500px;">
  
     
        <legend class="Text">Grading Result Received</legend>
        <table style="width: 634px">
        <tr>
             <td class="Text" colspan="3">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td class="Text"><asp:Label ID="lblGradingCode" runat="server" Text="Grading Code"></asp:Label></td>
        <td class="Input">
            <asp:TextBox ID="txtGradingCode" runat="server" Enabled="False"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="250px" 
              >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="cboCommodity" ErrorMessage="*" 
            Font-Names="Calibri"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class"></asp:Label>
        </td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="250px" 
            ></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="cboCommodityClass" Font-Names="Calibri" ErrorMessage="*" 
            Font-Size="Small"></asp:RequiredFieldValidator>
        </td>
        </tr>

        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade"></asp:Label>
            </td>
            <td class="Input">

<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="250px" ></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="cboCommodityGrade" Font-Names="Calibri" ErrorMessage="*" 
            Font-Size="Small"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
                <td class="Text">
            <asp:Label ID="Label2" runat="server" Text="Grade Recived Date "></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtDateRecived" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                </td>
                <td class="Text"><asp:Label ID="Label3" runat="server" Text="Time "></asp:Label></td>
                <td class="Input" >
                <asp:TextBox ID="txtTimeRecived" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="Text">Is Supervisor</td>
            <td class="Input"><asp:CheckBox ID="chkIsSupervisor" runat="server" /></td>
            
        </tr>
        <tr>
          <td class="Text">
            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
            </td>
            <td class="Input">
                <asp:DropDownList ID="cboStatus" runat="server" Width="250px">
                    <asp:ListItem Value="1">New</asp:ListItem>
                    <asp:ListItem Value="2">Approved</asp:ListItem>
                    <asp:ListItem Value="3">Client Accepted</asp:ListItem>
                    <asp:ListItem Value="4">Client Rejected</asp:ListItem>
                    <asp:ListItem Value="5"> Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td class="Text">
            <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="2" class="Button" align="Left">
        <asp:Button ID="btnUpdate" runat="server" Height="30px" Text="Update" Width="125px" onclick="btnSave_Click"  
                />
            <asp:HiddenField ID="hfGradingResultId" runat="server" />
            </td>
            
        </tr>
        <tr>
        <td colspan="3"></td>
         
        </tr>
  </table>
  </fieldset>
