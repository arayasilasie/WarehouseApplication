<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddRequestforEditGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddRequestforEditGRN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="2" style="width:475px" >Approved GRN Edit Request</td>
</tr>
<tr>
<td colspan="2"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text" style="width:100px" >GRN No. : </td>
    <td  class="Input" >
        <asp:TextBox ID="txtGRNNo" runat="server" Enabled="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtGRNNo" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
    <td class="Text" >Date Requested : </td>
    <td  class="Input" >
        <asp:TextBox Enabled="True" ID="txtDateRequested" runat="server" Height="22px" Width="128px"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateRequested_CalendarExtender" runat="server" 
             TargetControlID="txtDateRequested">
        </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtDateRequested" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>

<tr class="EditorAlternate">
    <td class="Text" valign="top" >Remark : </td>
    <td  class="Input" >
        <asp:TextBox ID="txtRemark"  runat="server" Height="67px" Width="232px"></asp:TextBox> 
    </td>
</tr>
<tr class="EditorCommand">
   
    <td colspan="2" class="Input" >
        
        <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" 
             Width="75px" />
        
        <asp:HiddenField ID="hfGRNID" runat="server" />
        
    </td>
</tr>
</table>