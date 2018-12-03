<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UICancelGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UICancelGRN" %>
<%@ Register src="UIEditGRN.ascx" tagname="UIEditGRN" tagprefix="uc1" %>
 <uc1:UIEditGRN ID="UIEditGRN1" runat="server" />
 <table class="PreviewEditor" style="width:800px" >
    <tr class="EditorCommand">
    <td align="left">
        <asp:Button ID="btnCancel" runat="server" Text="Cancel GRN " 
            onclick="btnCancel_Click" />
        <asp:HiddenField ID="hfTrackingNo" runat="server" />
        <%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />--%>
        </td>
    </tr>
    </table>