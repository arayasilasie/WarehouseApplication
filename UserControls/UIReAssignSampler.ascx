<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIReAssignSampler.ascx.cs" Inherits="WarehouseApplication.UserControls.ReAssignSampler" %>
  <fieldset style="height:206px; width: 694px;">
     <legend style="width: 235px; margin-top: 0px;" >Re-Assign Sampler</legend>
<table>
<tr>
    <td>Sample Code:</td>
    <td><asp:TextBox ID="txtSampleCode" runat="server"></asp:TextBox></td>
</tr>
<tr> 
    <td>Previous Sampler :</td>
    <td>
        <asp:DropDownList ID="cboPrevSampler" Width="250" runat="server">
        </asp:DropDownList>
    </td>
</tr>
<tr> 
    <td>Previous Sampler :</td>
    <td>
        <asp:DropDownList ID="cboSampler" Width="250" runat="server">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td>Re-assigning  Reason </td>
    <td>
        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="61px" 
            Width="353px"></asp:TextBox></td>
</tr>
<tr>
    <td colspan="2" align="Left">
        <asp:Button ID="btnSave" runat="server" Text="Save" Width="95px" 
            onclick="btnSave_Click"  />
        
        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="95px"  />
        
        </td>
    
</tr>
</table>
</fieldset>