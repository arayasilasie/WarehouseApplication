<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIOpenGRNEdit.ascx.cs" Inherits="WarehouseApplication.UserControls.UIOpenGRNEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="2" style="width:475px" >Allow GRN Edit <asp:HiddenField ID="hfGRNID" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
   <asp:HiddenField ID="hfTrackingNo" runat="server" />     
    <asp:HiddenField ID="hfOriginalStatus" runat="server" />
    </td>
</tr>
<tr>
<td colspan="2"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text" style="width:100px" >GRN No. : </td>
    <td  class="Input" >
        <asp:TextBox ID="txtGRNNo" runat="server" Enabled="False"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="Text" >Date Requested : </td>
    <td  class="Input" >
        <asp:TextBox ID="txtDateRequested" runat="server" Height="22px" Width="128px" 
            Enabled="False"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateRequested_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateRequested">
        </cc1:CalendarExtender>
    </td>
</tr>

<tr class="EditorAlternate">
    <td class="Text" valign="top" >Remark : </td>
    <td  class="Input" >
        <asp:TextBox ID="txtRemark"  runat="server" Height="67px" Width="232px" 
            Enabled="False"></asp:TextBox> 
    </td>
</tr>
<tr>
<td class="Text" valign="top" >Status : </td>\
<td>
    <asp:DropDownList ID="cboStatus" runat="server">
            <asp:ListItem>Please Select Status</asp:ListItem>
            <asp:ListItem Value="1">New</asp:ListItem>
            <asp:ListItem Value="2">Approved</asp:ListItem>
            <asp:ListItem Value="3">Cancelled</asp:ListItem>
         </asp:DropDownList>
</td>
</tr>
<tr class="EditorCommand">
   
    <td colspan="2" class="Input" >
        
        <asp:Button ID="btnEdit" runat="server" Text="Open GRN For Edit" onclick="btnAdd_Click" 
            Width="134px" style="height: 26px" />
        
    </td>
</tr>
</table>