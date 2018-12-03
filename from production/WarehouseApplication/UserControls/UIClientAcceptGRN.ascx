<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIClientAcceptGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIClientAcceptGRN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <style type="text/css">
    .Adj
    {
    	width:125pc;
    }
    </style>
    <table class="PreviewEditor" style="width:850px">
    <tr class="PreviewEditorCaption" >
    <td colspan="2">Client Response Entry</td>
    </tr>
    <tr>
    <td><asp:Label ID="lblmsg" runat="server"></asp:Label>
        <asp:HiddenField ID="hfGRNID" runat="server" />
        </td>
    </tr>
        <tr>
            <td class="Message" colspan="4">
                <asp:HiddenField ID="hfReceivigRequestId" runat="server" />
                <asp:HiddenField ID="hfVoucherId" runat="server" />
                </td>
        </tr>
        <tr>
            <td class="Text" class=Adj" >GRN No : </td>
            <td><asp:Label ID="lblGRN" runat="server"></asp:Label></td>
        </tr>
<tr class="PreviewEditorCaption">
    <td colspan="2">
        Arriva Information</td>

</tr>
    <tr >
        <td class="style2">Tracking No :</td>
        <td><asp:Label ID="lblTrackingNo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
        <td class="style1">Client : </td>
        <td><asp:Label ID="lblClinet" runat="server" Width="250px"></asp:Label></td>
    </tr>

    <tr>
        <td class="Text">Arrival Date :</td>
        <td><asp:Label ID="lblDateRecived" runat="server"  Width="250px" Text=""></asp:Label></td>
    </tr>
<tr class="PreviewEditorCaption">
    <td colspan="4">
        Arrival Information</td>
</tr>
    <tr >
        <td>Sample Ticket:</td>
        <td><asp:Label ID="lblSampleTicket" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfSamplingId" runat="server" />
        </td>
        </tr>
        <tr>
        <td>Date Sampled: </td>
        <td>
            <asp:Label ID="lblSampledDate" runat="server"></asp:Label>
        </td>
        </tr>
<tr class="PreviewEditorCaption">
<td  colspan="2">Grading Information</td>
</tr>
    <tr >
        <td>Code :</td>
        <td><asp:Label ID="lblCode" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfGradingId" runat="server" />
        </td>
        </tr>
        <tr class="EditorAlternate">
        <td>Commodity Grade : </td>
        <td>
            <asp:Label ID="lblCommodityGrade" runat="server"></asp:Label>
            <asp:HiddenField ID="hfCommodityGradeId" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Production Year :</td>
        <td>
            <asp:Label ID="lblProductionYear" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
<tr class="PreviewEditorCaption">
<td colspan="2">Unloading Information
</td>
</tr>
    <tr >
        <td>Date Deposited :</td>
        <td><asp:Label ID="lblDateDeposited" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfUnloadingId" runat="server" />
            <asp:HiddenField ID="hfWarehouseId" runat="server" />
        </td>
        </tr>
        <tr class="EditorAlternate">
        <td>Total No. Bags : </td>
        <td>
            <asp:Label ID="lblBags" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>Bag Type : </td>
        <td>
            <asp:Label ID="lblBagType" runat="server"></asp:Label>
            <asp:HiddenField ID="hfBagTypeId" runat="server" />
        </td>
    </tr>
<tr class="PreviewEditorCaption">
<td  colspan="2">Scaling Information</td>
</tr>
    <tr >
        <td>Total Gross weight :
        </td>
        <td><asp:Label ID="lblGrossWeight" runat="server"></asp:Label>
            <asp:HiddenField ID="hfScalingId" runat="server" />
        </td>
    </tr>
    <tr class="EditorAlternate" >
        <td>Total Net weight :
        </td>
        <td><asp:Label ID="lblNetWeight" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Quantity :</td>
        <td><asp:Label ID="lblOriginalQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
        <td>Current Quantity :</td>
        <td><asp:Label ID="lblCurrentQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>GRN Type :</td>
        <td>
            <asp:DropDownList ID="cboGRNType" Width="150px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="EditorAlternate">
    <td>Client Response :</td>
    <td>
        <asp:DropDownList ID="cboClientAccpted" Width="150px" runat="server">
            <asp:ListItem>Please Select</asp:ListItem>
            <asp:ListItem Value="1">Accepted</asp:ListItem>
            <asp:ListItem Value="0">Rejected</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="cboClientAccpted" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td>GRN Created date:</td>
    <td>
        <asp:TextBox ID="lblGRNCreatedDate" Enabled="false" runat="server"></asp:TextBox>
        
        </td>
    </tr>
    <tr  class="EditorAlternate">
    <td>Client Respose Date :</td>
    <td><asp:TextBox ID="txtClientAcceptedTimeStamp" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="txtClientAcceptedTimeStamp">
        </cc1:CalendarExtender>
        <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtClientAcceptedTimeStamp" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="txtClientAcceptedTimeStamp" ErrorMessage="*"></asp:RequiredFieldValidator>
         <asp:CompareValidator ControlToValidate="txtClientAcceptedTimeStamp"  
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Client Accepted date is less than the GRN Created Date" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual" 
            ControlToCompare="lblGRNCreatedDate"></asp:CompareValidator>
        
        </td>
    </tr>
    <tr>
    <td>Client Response Time :</td>

    
     <td>
                    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTime_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTime" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
            runat="server" ControlToValidate="txtTime" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>

    </tr>
    <tr  class="EditorAlternate">
    <td>GRN Status :</td>
        
        <td>
            <asp:DropDownList ID="cboStatus" Width="150px" runat="server">
                <asp:ListItem Value="1">New</asp:ListItem>
                <asp:ListItem Value="4">Client Accepted</asp:ListItem>
                <asp:ListItem Value="5">Client Rejected</asp:ListItem>
               
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr  class="EditorCommand">
    <td align="left" colspan="2">
        <asp:Button ID="btnAdd" CssClass="Forbtn" runat="server" Text="Save" onclick="btnAdd_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Forbtn" 
            onclick="btnCancel_Click" />
        <asp:HiddenField ID="hfStatus" runat="server" />
        </td>
    </tr>
</table>