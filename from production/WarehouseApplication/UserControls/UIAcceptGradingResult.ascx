<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAcceptGradingResult.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAcceptGradingResult" %>
   <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
   <br />
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption"   >
<td colspan="2">Client Grading Result Response</td>
</tr>
<tr>
<td colspan="2" class="Message">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="hfId" runat="server" />
    </td>
</tr>
<tr>
    <td class="Text">Grading Code :</td>
    <td class="Input">
        <asp:Label ID="lblGradeCode" runat="server" Text="Label"></asp:Label>
        
                 </td>
</tr>
<tr  class="EditorAlternate">
    <td class="Text">Client :</td>
    <td class="Input">
        <asp:Label ID="lblClient" runat="server" Text=""></asp:Label>
        
                 </td>
</tr>
<tr>
<td>

    Grading Result Status:</td>
<td>
<asp:DropDownList ID="cboGradingRecivedStatus" Enabled="False" runat="server" Width="250px" 
                    AutoPostBack="True">
                    <asp:ListItem>Please Select Status</asp:ListItem>
                    <asp:ListItem Value="0">New</asp:ListItem>
                    <asp:ListItem Value="1">Approved</asp:ListItem>
                    <asp:ListItem Value="5">Moisture Fail</asp:ListItem>
                    <asp:ListItem Value="6">General Rquierment fail</asp:ListItem>
                    <asp:ListItem Value="2">Cancelled</asp:ListItem>
                </asp:DropDownList>
</td>
</tr>
<tr>
    <td class="Text">Commodity Grade :</td>
    <td class="Input">
        <asp:Label ID="lblCommodityGrade" runat="server" Text=""></asp:Label>
        
                 </td>
</tr>
<tr class="EditorAlternate"> 
    <td class="TextRequired">Status :</td>
    <td class="Input">
            <asp:DropDownList ID="cboAcceptanceStatus" Width="250" runat="server" >
                <asp:ListItem>Please Select Status</asp:ListItem>
                <asp:ListItem Value="3">Accepted By Client</asp:ListItem>
                <asp:ListItem Value="4">Rejected By Client</asp:ListItem>
        </asp:DropDownList>
        
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="cboAcceptanceStatus" ErrorMessage="*"></asp:RequiredFieldValidator>
        
    </td>
</tr>
<tr>
<td style="width:150px">Grading Received Date :</td>
<td>
    <asp:Label ID="lblGradingReceivedDate" runat="server" Text=""></asp:Label></td>
</tr>
<tr class="EditorAlternate">
    <td class="TextRequired">Date of Acceptance : </td>
    <td class="Input">
        <asp:TextBox ID="txtDateOfAcceptance" runat="server" Height="22px"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateOfAcceptance_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateOfAcceptance">
        </cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
            ControlToValidate="txtDateOfAcceptance" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" Type="Date" ControlToValidate="txtDateOfAcceptance" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
        
         <asp:CompareValidator ControlToValidate="txtDateOfAcceptance"  
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Cleint response date is less than the Grade received Date" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual"></asp:CompareValidator>
        </td>
</tr>
<tr >
    <td class="TextRequired">Time of Acceptance :</td>
    <td class="Input">
        <asp:TextBox ID="txtTimeodAcceptance" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeodAcceptance" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            runat="server" ControlToValidate="txtTimeodAcceptance" ErrorMessage="*"></asp:RequiredFieldValidator>
                 </td>
</tr>
<tr class="EditorCommand">
    <td colspan="2" align="left" class="Button">
    
    <asp:Button ID="btnPrint" runat="server" Text="Print" CausesValidation="false" 
            onclick="btnPrint_Click" Width="95px" />&nbsp;
    
        <asp:Button ID="btnSave" runat="server" Text="Update" ToolTip="Update"  Width="95px"  style="margin-bottom: 0px; height: 26px;" 
            onclick="btnSave_Click" />
            
        &nbsp;
                 
        <asp:Button ID="btnNext" runat="server" CausesValidation="false" 
        Text="Next" Width="95px"  style="margin-bottom: 0px; height: 26px;" onclick="btnNext_Click" 
        />    

        
        <asp:HiddenField ID="txtId" runat="server" />
        
        </td>
    
</tr>
</table>