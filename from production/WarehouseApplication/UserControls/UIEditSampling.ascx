<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditSampling.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditSampling" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:850px"  >
<tr class="PreviewEditorCaption">
    <td colspan="2">Add S<span lang="en-us">Sampling</span> Information</td>
</tr>
<tr>
<td class="Message" colspan="2"><asp:Label ID="lblMessage" runat="server"></asp:Label></td>  
</tr>
<tr>
    <td>Arrival Date:</td>
    <td>
    <asp:TextBox ID="lblArrivalDate" runat="server"></asp:TextBox>
   </td>
</tr>

    <tr  class="Text EditorAlternate">
        <td>Sampling Code</td>
        <td><asp:Label ID="lblSampleCode" runat="server"></asp:Label></td>
    </tr>
     <tr>
            <td>Date Code Genrated :</td>
            <td>
                <asp:TextBox ID="txtDateCodeGenrated" runat="server" ></asp:TextBox>
                  <cc1:CalendarExtender ID="txtDateCodeGenrated_CalendarExtender" runat="server" 
            TargetControlID="txtDateCodeGenrated">
        </cc1:CalendarExtender>
        
         <asp:RequiredFieldValidator ID="rvDateCodeGenrated" ValidationGroup="Save" ControlToValidate="txtDateCodeGenrated" runat="server" 
         ErrorMessage="*"></asp:RequiredFieldValidator>
        
         <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="txtDateCodeGenrated" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtDateCodeGenrated" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="lblArrivalDate" ControlToValidate="txtDateCodeGenrated" 
                    ErrorMessage="Sampling Date can't be less than Arrival date" 
                    Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Time Generated:</td>
            <td>     <asp:TextBox ID="txtTimeArrival" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeArrival_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeArrival" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
            runat="server" ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
                <tr class="EditorCommand" >
        <td align="left" colspan="2" >
        <asp:Button ID="btnNext" runat="server" Text="Update" 
           style="margin-bottom: 0px; height: 26px;" 
        Width="95px" onclick="btnNext_Click" />&nbsp;
         
        </td>
        </tr>
</table>