<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditScaling.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditScaling" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 850px;
    }
</style>
<table class="PreviewEditor" style="width:850px"  >
<tr class="PreviewEditorCaption">
    <td colspan="2">Add Scaling Information</td>
</tr>
<tr>
<td class="Message" colspan="2"><asp:Label ID="lblMessage" runat="server"></asp:Label></td>
    
</tr>
    <tr  class="Text EditorAlternate">
        <td>Plate No.</td>
        <td>Trailer Plate No.</td>
    </tr>
    <tr class="Text">
        <td><asp:Label ID="lblPlateNo" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblTrailerNo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
    <td class="Text">Scale Ticket Number :</td>
    <td><asp:TextBox ID="txtScaleTicket" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtScaleTicket" runat="server" 
            ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr >
    <td class="Text">Date Weighed :</td>
    <td><asp:TextBox ID="txtDateWeighed" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateWeighed_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateWeighed"></cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtDateWeighed" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="EditorAlternate">
        <td class="Text">Gross Truck Weight :</td>
        <td><asp:TextBox ID="txtGrossTruckWeight" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtGrossTruckWeight" ErrorMessage="*"></asp:RequiredFieldValidator>
                
        </td>
    </tr>
    <tr>
        <td class="Text">Truck Weight :</td>
        <td><asp:TextBox ID="txtTruckWeight" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtTruckWeight" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
        <tr>
        <td class="Text">Gross Weight :</td>
        <td><asp:TextBox ID="txtGrossWeoght" runat="server" Enabled="False"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtGrossWeoght" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="EditorAlternate">
    <td>Weigher Name:</td>
    <td>
    <asp:DropDownList ID="cboWeigher" runat="server" Width="250px">
    </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvWeigherName" runat="server" 
            ControlToValidate="cboWeigher" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="Text">Status :</td>
        <td>
            <asp:DropDownList ID="cboStatus" runat="server">
                <asp:ListItem>Please Select Status</asp:ListItem>
                <asp:ListItem Value="1">New</asp:ListItem>
                <asp:ListItem Value="2">Approved</asp:ListItem>
                <asp:ListItem Value="3">Cancelled</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="cboStatus" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="EditorAlternate">
    <td>Remark :</td>
    <td>
        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox></td>
    </tr>
    <tr class="EditorCommand">
    <td class="style1" align="left" colspan="2">
        <asp:Button ID="btnAdd" runat="server" Text="Update" onclick="btnAdd_Click" />&nbsp;<asp:HiddenField 
            ID="hfId" runat="server" />
    </td>
    </tr>
   </table>
  
