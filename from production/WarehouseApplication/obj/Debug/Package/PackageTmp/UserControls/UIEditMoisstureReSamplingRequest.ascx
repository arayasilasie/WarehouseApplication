<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditMoisstureReSamplingRequest.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditMoisstureReSamplingRequest" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc2" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="2">Moisture Re-sampling 
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="hfTrackingNo" runat="server" />
            </td>
</tr>
<tr class="Message">
<td colspan="2">
    <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </td>
</tr>
<tr>
<td class="Text" >Previous Sampling Code :</td>
<td class="Input">
    <asp:Label ID="lblSamplingCode" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td class="Text" >Date Time Requested : </td>
<td class="Input">
    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtDate">
    </cc1:CalendarExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtDate" ErrorMessage="*"></asp:RequiredFieldValidator>
    Time:
    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                     <cc1:MaskedEditExtender ID="txtTime_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTime" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtTime" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td>Status : </td>
<td>
    <asp:DropDownList ID="cboStatus" runat="server">
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="2">Approved</asp:ListItem>
        <asp:ListItem Value="3">Cancelled</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left">
    <asp:Button ID="btnSave" runat="server" Text="Update" onclick="btnSave_Click" /></td>
</tr>
</table>