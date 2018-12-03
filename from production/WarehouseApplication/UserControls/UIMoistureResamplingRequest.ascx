<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIMoistureResamplingRequest.ascx.cs" Inherits="WarehouseApplication.UserControls.UIMoistureResamplingRequest" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="2">Moisture Re-sampling </td>
</tr>
<tr class="Message">
<td colspan="2">
    <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
</tr>
<tr>
<td class="Text" >Previous Sampling Code :</td>
<td class="Input">
    <asp:DropDownList ID="cboSamplingCode" runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="cboSamplingCode" ErrorMessage="*"></asp:RequiredFieldValidator>
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
<tr class="EditorCommand">
<td colspan="2" align="left">
    <asp:Button ID="btnSave" runat="server" Width="95px" style="margin-bottom: 0px; height: 26px;" Text="Save" onclick="btnSave_Click" /> 
    
    &nbsp;<asp:Button ID="btnClear" runat="server" CausesValidation="false" 
        onclick="btnClear_Click" style="margin-bottom: 0px; height: 26px;" 
        Text="Cancel" Width="95px" />
    
    </td>
</tr>
</table>