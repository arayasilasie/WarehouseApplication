<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddScalingInformation.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddScalingInformation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<style>
.adj  
{

	width:175px;
}
    .style1
    {
        width: 126px;
    }
</style>
    <table class="PreviewEditor" style="width:850px" >
<tr class="PreviewEditorCaption">
<td colspan="4">Weighing&nbsp; Information</td>
</tr>
        <tr>
            <td class="Message" colspan="4">
                <asp:HiddenField ID="hfReceivigRequestId" runat="server" />
                <asp:HiddenField ID="hfTrackingNo" runat="server" />
                <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="style1" ><span lang="en-us">Grading Code</span> : </td>
            <td class="style2">
            <asp:Panel runat="server" ID="pnl" Enabled="false">
                <asp:DropDownList ID="cboGradingCode" runat="server" Width="150px" 
            ValidationGroup="Unloading" 
            AutoPostBack="True" onselectedindexchanged="cboGradingCode_SelectedIndexChanged">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="cboGradingCode" ErrorMessage="Please enter Grading Code">*</asp:RequiredFieldValidator>
   </asp:Panel> </td>
    </tr>
    <tr>
    <td class="style1" >Commodity Grade:</td>
     <td class="style2"><asp:Label ID="lblCG" runat="server" Text="" Visible="true"></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
    <td colspan="2">Truck Information</td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:GridView ID="gvDriverInformation" runat="server" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
            GridLines="None" 
            onselectedindexchanging="gvDriverInformation_SelectedIndexChanging" 
            Width="550px" 
            onselectedindexchanged="gvDriverInformation_SelectedIndexChanged" 
            CssClass="Grid">
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
            <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
                CssClass="GridSelectedRow" />
            <HeaderStyle Font-Bold="True" ForeColor="White" BorderStyle="None" 
                CssClass="GridHeader" />
            <EditRowStyle BackColor="#FFCC66" />
            <RowStyle CssClass="GridRow" />
            <Columns>
             <asp:TemplateField Visible="false" >
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="true" HeaderText="Plate Number">
                <ItemTemplate>
                    <asp:Label ID="lblPlateNumber" runat="server" Text='<%# Bind("PlateNumber") %>'></asp:Label>
 
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="true" HeaderText="Trailer Plate Number">
                <ItemTemplate>
                    <asp:Label ID="lblTrailerPlateNumber" runat="server" Text='<%# Bind("TrailerPlateNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
               <asp:TemplateField Visible="true" HeaderText="">
                <ItemTemplate>
                 <asp:LinkButton ID="lnkSelect" runat="server"  CausesValidation="False" 
                            CommandName="Select" Text="Select"></asp:LinkButton>
                
                
                   </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
      </td>
        
        
</tr>
</table>
<asp:Panel ID="pnAdd" Visible="false" runat="server">
<table class="PreviewEditor" style="width:850px" >
<tr class="PreviewEditorCaption">
<td colspan="2">Add Scaling Information</td>
</tr>
<tr>
<td colspan="2"  >
    <asp:HiddenField ID="hfDriverInformationId" runat="server" />
    <asp:HiddenField ID="hfGradingResultId" runat="server" />
    </td>
</tr>
    <%--<tr  class="Text EditorAlternate">
        <td>Plate No.</td>
        <td>Trailer Plate No.</td>
    </tr>--%>
    <tr class="Text">
        <td><asp:Label ID="lblPlateNo" runat="server" Text="" Visible="false"></asp:Label></td>
        <td><asp:Label ID="lblTrailerNo" runat="server" Text="" Visible="false"></asp:Label></td>
    </tr>
    <tr>
    <td  colspan="2">
    Is Truck Scaled?
        <asp:CheckBox ID="chkTruckScaled"   runat="server" 
             /></td>
    </tr>
    <tr class="EditorAlternate">
    <td class="Text adj">Scale Ticket Number :</td>
    <td><asp:TextBox ID="txtScaleTicket" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtScaleTicket" runat="server" 
            ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td>Date Unloaded:</td>
    <td>
        <asp:Label ID="lblDateUnloaded" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
    <td class="Text">Date Weighed :</td>
    <td><asp:TextBox ID="txtDateWeighed" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateWeighed_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateWeighed"></cc1:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" 
            ControlToValidate="txtDateWeighed" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtDateWeighed" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
            <asp:CompareValidator ControlToValidate="txtDateWeighed"  
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Date Weighed is less than the Date Unloaded" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual"></asp:CompareValidator>
            
        </td>
    </tr>
    <tr >
    <td class="Text">Time Weighed</asp:Label>
        &nbsp;:</td>
     <td>
                    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeArrival_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTime" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
            runat="server" ControlToValidate="txtTime" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
    </tr>  
    <tr class="EditorAlternate" >
        <td class="Text" style="width:auto">Gross Truck Weight <asp:Label ID="lblUnit" runat="server" 
                ForeColor="Red"></asp:Label> :</td>
        <td valign="bottom"><asp:TextBox ID="txtGrossTruckWeight" runat="server" Height="22px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                ControlToValidate="txtGrossTruckWeight" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr >
        <td class="Text" style="width:auto">Truck Weight
            <asp:Label ID="lblUnit1" runat="server" ForeColor="Red"></asp:Label>
            :</td>
        <td><asp:TextBox ID="txtTruckWeight" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtTruckWeight" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator> <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTruckWeight" 
                ControlToCompare="txtGrossTruckWeight" Display="Dynamic" ErrorMessage="Truck Weight can not exceed Gross Truck weight" 
                Operator="LessThan" Type="Double"></asp:CompareValidator>
            <asp:CustomValidator ID="cvTW" runat="server" Display="Dynamic" ClientValidationFunction="ValidateTruckweight" 
                ErrorMessage="Truck Weighht Can't be 0 or empty."></asp:CustomValidator>
            <asp:CustomValidator ID="UnScaledTW" runat="server" Display="Dynamic" ClientValidationFunction="UnScaledValidateTruckweight" 
                ErrorMessage="Truck Weight Should only be 0."></asp:CustomValidator>
        </td>
    </tr>
    <tr class="EditorAlternate">
    <td>Weigher Name:</td>
    <td>
    <asp:DropDownList ID="cboWeigher" runat="server" Width="250px">
    </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvWeigherName" runat="server" 
            ControlToValidate="cboWeigher" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="" >
    <td>Remark :</td>
    <td>
        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox></td>
    </tr>   
    <tr class="EditorCommand">
    <td  colspan="2" align="left" >
        <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" CssClass="Forbtn" />&nbsp;<asp:Button 
            ID="btnClear" runat="server" Text="Clear" onclick="btnClear_Click" CssClass="Forbtn" 
            CausesValidation="False" />    
    </td>
    </tr>
    </table>
    </asp:Panel>
<script type="text/javascript">
    function ValidateTruckweight(Source, args) {
        var chkAnswer = document.getElementById('<%= chkTruckScaled.ClientID %>');
        var txtTW = document.getElementById('<%= txtTruckWeight.ClientID %>');

        if (chkAnswer.checked == true) {
            if (txtTW.value == "" || txtTW.value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
    function UnScaledValidateTruckweight(Source, args) {
        var chkAnswer = document.getElementById('<%= chkTruckScaled.ClientID %>');
        var txtTW = document.getElementById('<%= txtTruckWeight.ClientID %>');

        if (chkAnswer.checked == false) {
            if (txtTW.value == "0")
                args.IsValid = true;
            else
                args.IsValid = false; 
        }
    }
    


</script>