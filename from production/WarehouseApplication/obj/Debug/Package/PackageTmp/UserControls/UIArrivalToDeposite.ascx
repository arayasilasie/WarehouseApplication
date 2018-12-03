<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIArrivalToDeposite.ascx.cs" Inherits="WarehouseApplication.UserControls.UIArrivalToDeposite" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr>
<td colspan="2" style="width:475px" >
    <asp:Label CssClass="Message" ID="lblMessage" runat="server" Text=""></asp:Label></td>
</tr>
<tr class="PreviewEditorCaption">
    <td colspan="2" style="width:475px" >Arrival to Deposit Report:</td>
</tr>
<tr>
<td>Warehouse name : 
    <asp:Label ID="lblWN" runat="server" Text=""></asp:Label> 
    </td>

</tr>
<tr>
<td class="Text" >Date Deposited :
     <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
             Enabled="True" TargetControlID="txtFrom">
         </cc1:CalendarExtender>
         </td>

</tr>
     <tr>
     <td colspan="4" align="left">
         <asp:Button ID="btnSearch" runat="server" 
             Text="Genrate Report" Width="137px" onclick="btnSearch_Click" 
             style="height: 26px" /></td>
     </tr>
</table>