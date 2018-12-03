
<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ImportWareHouseReceipt.aspx.cs"
    Inherits="WarehouseApplication.ImportWareHouseReceipt" Title="Import WareHouse Receipt" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
           
            
            background-color:White;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
      <center>
    <table  class="style1" >
    <tr>
    <td colspan="2" align="left">
    <uc1:Messages ID="Messages" runat="server" />
    </td>
    </tr>
        <tr>
            <td valign="middle">
                <asp:Label ID="lblWareHouseReceiptNo" runat="server" 
                    Text="Ware House Receipt No:"></asp:Label>
            </td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtWareHouseReceiptNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
            </td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDate">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="2">
        <center>
                                <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" OnClick="btnSave_Click" CssClass="style1" ValidationGroup="gin" />
                                <span class="style1">&nbsp; </span>
                                
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" OnClick="btnCancel_Click" CssClass="style1" />
                            </center>
                            </td>
        </tr>
    </table>
    </center>
</asp:Content>
