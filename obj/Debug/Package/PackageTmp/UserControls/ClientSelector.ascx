<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientSelector.ascx.cs" Inherits="WarehouseApplication.UserControls.ClientSelector" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers  >
<asp:AsyncPostBackTrigger ControlID="txtClientId" EventName="TextChanged" />
</Triggers>
<ContentTemplate>
<table  style="width: 558px; background-color:Transparent">
<tr>
<td class="Text" style="width: 129px;">    
<asp:TextBox ID="txtClientId" runat="server" Width="168px" ontextchanged="txtClientId_TextChanged" AutoPostBack="True"></asp:TextBox>
<br /> 
<asp:Label ID="lblMessage" runat="server" Text="" Width="240px"></asp:Label></td>
</td> 
<td>   
</tr>
<tr>
<td colspan="3" >
    
    <asp:HiddenField ID="ClientGUID" runat="server" />
    
    </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>