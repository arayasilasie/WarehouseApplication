<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditSamplingResult.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditSamplingResult" %>

<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Edit Sampling Result</td>
</tr>
<tr>
<td colspan="2" class="Message">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
    <td class="Text" style="width:175px">Sample Code :</td>
    <td class="Input">
        <asp:TextBox ID="txtSampleCode" runat="server" Enabled="False" 
            ></asp:TextBox>
                 </td>
</tr>
<tr class="EditorAlternate"> 
    <td class="Text">Sampler :</td>
    <td class="Input">
        <asp:DropDownList ID="cboSampler" Width="250" runat="server" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvSampler" runat="server" ErrorMessage="*" ControlToValidate="cboSampler" ></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
    <td class="Text">Number of Bags : </td>
    <td class="Input">
        <asp:TextBox ID="txtNumberofbags" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNoBags" runat="server" ErrorMessage="*" ControlToValidate="txtNumberofbags" ></asp:RequiredFieldValidator>
        </td>
</tr>
<%--<tr class="EditorAlternate">
    <td class="Text">Number of Separations :</td>
    <td class="Input">
        <asp:TextBox ID="txtNumberOfSeparations" runat="server" Text="1" 
            Enabled="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfNoSeprations" runat="server" ErrorMessage="*" ControlToValidate="txtNumberOfSeparations" ></asp:RequiredFieldValidator>
                 </td>
</tr>--%>
<tr class="EditorAlternate">
    <td class="Text">Sampler Comment :</td>
    <td class="Input">
        <asp:TextBox ID="txtSamplerCommment" TextMode="MultiLine" runat="server" Height="61px" 
            Width="353px"></asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Is Supervisor :</td>
    <td class="Input">
        <asp:CheckBox ID="chkisSupervisor" runat="server" /></td>
</tr>
<tr>
    <td class="Text">Remark :</td>
    <td class="Input">
        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="61px" 
            Width="353px"></asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Status :</td>
    <td class="Input">
    <asp:DropDownList ID="cboStatus" runat="server" Width="150px">
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="2">Approved</asp:ListItem>
        <asp:ListItem Value="3">Cancelled</asp:ListItem>
    </asp:DropDownList>
        </td>
</tr>
<tr class="EditorCommand">
    <td colspan="2" align="Left" class="Button">
        <asp:Button ID="btnSave" runat="server" Text="Update" Width="95px" 
            onclick="btnSave_Click" Visible="false" CommandName="UpdateSamplingResult"  />
        &nbsp;<asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" Width="95px"  />
        
        <asp:HiddenField ID="txtId" runat="server" />
        
        </td>
    
</tr>
<tr>
</tr>
</table>
