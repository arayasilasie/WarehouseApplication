<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditCommodityDepositrequest.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditCommodityDepositrequest" %>
   <%@ Register src="ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
   

    <fieldset style="height: 464px; width: 709px;">
     <legend style="width: 235px; margin-top: 0px;" >Commodity Deposit Request</legend>
<table  class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
 <td colspan="4"  >Arrival Edit</td>
  </tr>
<tr>
<td colspan="2" class="Message"><asp:Label ID="lblmsg" runat="server"></asp:Label></td>
</tr>
<tr>
<td class="Text" >Client Name:</td>
<td colspan="2"  align="left" class="Input" >
    
    <asp:Label ID="txtClient" runat="server" Text=""></asp:Label>
    
    <asp:HiddenField ID="hfCommodityDepositeId" runat="server" />
    
  
    
    <asp:HiddenField ID="hfClientId" runat="server" />
    
  
    
</tr>
<tr>  </td>
    <td>New Client Id :</td>
    <td>
        <uc1:ClientSelector ID="ClientSelector1" runat="server" />
    </td>
    </tr>
<tr class="EditorAlternate">
    <td class="Text"  >Representative:</td>
    <td colspan="2" align="left" class="Input" ><asp:DropDownList ID="cboReprsentative" runat="server"  Width="200px" 
                 TabIndex="1" ></asp:DropDownList>
    </td>
    
</tr>
<tr>
    <td class="Text" >Commodity:</td>
    <td colspan="2" align="left" class="Input"  >
    <asp:DropDownList ID="cboCommodity" Enabled="false" runat="server" Width="200px"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="cboCommodity" 
            ErrorMessage="*" Font-Names="Calibri" 
            Font-Size="Small"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="Text" >Production Year:</td>
    <td colspan="2"align="left" class="Input" ><asp:DropDownList ID="cboProductionYear" runat="server" Width="200px"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvProductionYear" runat="server" 
            ControlToValidate="cboProductionYear" 
            ErrorMessage="*" Font-Names="Calibri" 
            Font-Size="Small"></asp:RequiredFieldValidator>
    </td>
</tr>

<tr>
<td class="Text" >Region :</td>
<td colspan="2" align="left" class="Input" >

    <asp:DropDownList ID="cboRegion" runat="server"  
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="cboRegion_CascadingDropDown" runat="server" 
            Category="Region" Enabled="True" LoadingText="Loading Regions " 
            PromptText="Please Select Region" ServiceMethod="GetRegions" 
            ServicePath="~\Location.asmx" TargetControlID="cboRegion">
        </cc1:CascadingDropDown>
        

                 </td>
</tr>
<tr class="EditorAlternate">
<td class="Text" >Zone:</td>
<td colspan="2" align="left" class="Input">

   <asp:DropDownList ID="cboZone" runat="server"  
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboZone_CascadingDropDown" runat="server" 
            Category="Zone" Enabled="True" LoadingText="Loading Zone..." 
            ParentControlID="cboRegion" PromptText="Please Select Zone" 
            ServiceMethod="GetZones" ServicePath="~\Location.asmx" TargetControlID="cboZone">
        </cc1:CascadingDropDown>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="cboZone" Font-Names="Calibri" ErrorMessage="*" 
        Font-Size="Small"></asp:RequiredFieldValidator>
                 </td>
</tr>
<tr>
<td class="Text">Woreda:</td>
<td colspan="2" align="left" class="Input">
<asp:DropDownList ID="cboWoreda" runat="server"  ></asp:DropDownList>
            
    
    
        <cc1:CascadingDropDown ID="cboWoreda_CascadingDropDown" runat="server" 
            Category="Woreda" Enabled="True" LoadingText="Loading Woredas" 
            ParentControlID="cboZone" PromptText="Please Select woreda" 
            ServiceMethod="GetWoredas" ServicePath="~\Location.asmx" 
            TargetControlID="cboWoreda" UseContextKey="True">
        </cc1:CascadingDropDown>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="cboWoreda" Font-Names="Calibri" ErrorMessage="*" 
        Font-Size="Small"></asp:RequiredFieldValidator>
                 </td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Estimated Weight (kg) :</td>
<td  align="left" class="Input " > <asp:TextBox ID="txtWeight" runat="server" Width="123px"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtWeight" CssClass="Input" 
            ErrorMessage="Only Decimal Number Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>

</td>
</tr>
<tr>
<td class="Text">Estimated No. of Bags :</td>
<td class="Input"> <asp:TextBox ID="txtNumberOfBags" runat="server" Width="123px"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtNumberOfBags" CssClass="Input" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>

</td>
</tr>
<tr class="EditorAlternate">
    
<td class="Text">  Date Recived:</td>
<td colspan="2" align="left"  class="Input"><asp:TextBox ID="dtDateTimeRecived" runat="server" Width="123px" ></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="dtDateTimeRecived">
        </cc1:CalendarExtender>
    <asp:RequiredFieldValidator ID="DateTimeRecived" runat="server" 
            ControlToValidate="dtDateTimeRecived" Font-Names="Calibri"
            ErrorMessage="*" Font-Size="Small"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="dtDateTimeRecived"
          MinimumValue="<%#DateTime.Now.AddYears(-10).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
            
</td>
</tr>
<tr>
<td>Time Recived:</td>
<td>
                    <asp:TextBox ID="txtTimeArrival" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeArrival_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeArrival" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            runat="server" ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator>

</td>
</tr>
<tr>
<td class="Text">Remark:</td>
<td colspan="2" class="Input">
    <asp:TextBox 
                        ID="txtRemark" runat="server" Height="62px" TextMode="MultiLine" 
                        Width="538px" style="margin-top: 0px"></asp:TextBox>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Status:</td>
<td colspan="2" class="Input" >
    <asp:DropDownList ID="cboStatus" runat="server" Width="200px">
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="0">Cancelled</asp:ListItem>
    </asp:DropDownList>
                 </td>
</tr>


        <tr class="EditorCommand"><td align="Left" colspan="2" >
            <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" />
        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                onclick="btnCancel_Click" CausesValidation="False" />
        </td>
        </tr>
</table>
</fieldset>