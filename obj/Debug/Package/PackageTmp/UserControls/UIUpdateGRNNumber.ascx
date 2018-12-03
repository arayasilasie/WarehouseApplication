<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIUpdateGRNNumber.ascx.cs" Inherits="WarehouseApplication.UserControls.UIUpdateGRNNumber" %>
<table class="PreviewEditor">
<tr class="PreviewEditorCaption">
<td colspan="2">Update GRN Number</td>
</tr>
<tr>
<td colspan="2"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
<td style="width:150px" >New System GRN Number :</td>
<td>
    <asp:Label ID="lblNewGRN" runat="server" Text=""></asp:Label>
 </td>
</tr>
<tr>
<td>Old System GRN Number :</td>
<td>
    <asp:TextBox ID="txtOldSystemGRNNo" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtOldSystemGRNNo" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr  class="EditorCommand">
<td colspan="2" align="left" >
    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
        onclick="btnUpdate_Click" /></td>
</tr>

</table>