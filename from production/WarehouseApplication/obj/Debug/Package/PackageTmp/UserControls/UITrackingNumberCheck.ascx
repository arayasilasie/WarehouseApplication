<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UITrackingNumberCheck.ascx.cs" Inherits="WarehouseApplication.UserControls.UITrackingNumberCheck" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table class="PreviewEditor" style="width:900px"  >
    <tr class="PreviewEditorCaption">
        <td colspan="2"  style="width:475px" >Trucking No. Status</td>
    </tr>
    <tr>
        <td colspan="2"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label1" runat="server" Text="Tracking No."></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtTrackingNo" Text="" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
    <td>Current Status:</td>
    <td  >
        <asp:Label ID="lblstatus" CssClass="Message"  runat="server" Text=""></asp:Label>
    </td>
    </tr>
    
</table>