<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIReceiveSampleCode.ascx.cs" Inherits="WarehouseApplication.UserControls.UIReceiveSampleCode" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 200px;
    }
</style>
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption">
 <td colspan="2" style="width:250px">Receive Sample Code</td>
</tr>
<tr>
<td colspan="2" style="width:auto" class="Message" >
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></td>
</tr>
<tr class="EditorAlternate">
<td class="style1">Grading Code:</td>
<td>
    <asp:Label ID="lblGradingCode" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="hfId" runat="server" />
    </td>
</tr>
<tr>
    <td>Date Coded:</td>
    <td>
        <asp:TextBox ID="txtDateCoded" Enabled="false" runat="server"></asp:TextBox>
    </td>
</tr>
<tr >
<td class="style1">Sample Code Received Date: </td>
 <td class="Input">
                <asp:TextBox ID="txtDateRecived" TabIndex="1" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtDateRecived"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                
                <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtDateRecived" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
            <asp:CompareValidator ControlToValidate="txtDateRecived"  
                ID="cmpCodeGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Result received date is less than the Code Generated Date" 
                 Type="Date"  Operator="GreaterThanEqual"></asp:CompareValidator>
           
           
                </td>
</tr>
<tr class="EditorAlternate">
<td class="style1">Sample Code Received Time:</td>
 <td class="Input" >
                <asp:TextBox ID="txtTimeRecived" runat="server" TabIndex="2"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTimeRecived"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
</tr>
<tr>
<td class="style1">Remark:</td>
<td>
    <asp:TextBox ID="txtLabTechRemark" TextMode="MultiLine" runat="server" 
        TabIndex="3"></asp:TextBox></td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left" >
    <asp:Button ID="btnSave" Width="95px" style="margin-bottom: 0px; height: 26px;" TabIndex="4"  runat="server"  Text="Save" 
        onclick="btnSave_Click" />
    
    &nbsp;<asp:Button ID="btnClear" runat="server" CausesValidation="false" 
        Text="Next" Width="95px"  style="margin-bottom: 0px; height: 26px;" 
        onclick="btnClear_Click" /></td>
</tr>

</table>