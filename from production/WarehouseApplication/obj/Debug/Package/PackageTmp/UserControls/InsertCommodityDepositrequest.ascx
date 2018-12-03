<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertCommodityDepositrequest.ascx.cs"  Inherits="WarehouseApplication.UserControls.AddCommodityDepositrequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>

<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Arrival Entry</td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr >
<td  class="TextRequired" >Client ID :
                 </td>
<td class="Input">
    <uc1:ClientSelector ID="ClientSelector1" runat="server" />
    </td>
</tr>

<tr class="EditorAlternate">
    <td class="Text">Agent :</td>
    <td colspan="3" class="Input" ><asp:DropDownList CssClass="Input"  ID="cboReprsentative" runat="server"   
                 TabIndex="1" >
        <asp:ListItem>Please Select Agent</asp:ListItem>
        </asp:DropDownList>
    </td>
    
</tr>
<tr>
    <td class="TextRequired" >Warehouse :</td>
    <td colspan="3" class="Input" >
        <asp:DropDownList ID="cboWarehouse" runat="server" Enabled="False" 
             ></asp:DropDownList>
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="TextRequired" >Commodity :</td>
    <td colspan="3" class="Input" ><asp:DropDownList ID="cboCommodity"  CssClass="Input" runat="server" 
             ></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
            ControlToValidate="cboCommodity" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr >
<td class="Text" >Is Commodity Source Determined :</td>
<td class="Input">
    <asp:CheckBox ID="chkIsSourceDetermined" runat="server" 
        oncheckedchanged="chkIsSourceDetermined_CheckedChanged" />
</td>
</tr>
        <tr class="EditorAlternate">
    <td class="Text" >Region :</td>
    <td class="Input">
    <asp:DropDownList ID="cboRegion" runat="server"  
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="cboRegion_CascadingDropDown" runat="server" 
            Category="Region" Enabled="True" LoadingText="Loading Regions " 
            PromptText="Please Select Region" ServiceMethod="GetRegions" 
            ServicePath="~\Location.asmx" TargetControlID="cboRegion">
        </cc1:CascadingDropDown>
        
        <asp:CustomValidator ID="cvReqRegion" ValidationGroup="Save" runat="server" Font-Size="Small" Font-Names="Calibri" ClientValidationFunction="ValidateRegion"
    ErrorMessage="*"></asp:CustomValidator>
        
                     </td>
    </tr>
       <tr >
    <td class="Text" >Zone :</td>
    <td class="Input">
        <asp:DropDownList ID="cboZone" runat="server"  
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboZone_CascadingDropDown" runat="server" 
            Category="Zone" Enabled="True" LoadingText="Loading Zone..." 
            ParentControlID="cboRegion" PromptText="Please Select Zone" 
            ServiceMethod="GetZones" ServicePath="~\Location.asmx" TargetControlID="cboZone">
        </cc1:CascadingDropDown>
                    <asp:CustomValidator ID="crfvZone" ValidationGroup="Save" runat="server" Font-Size="Small" Font-Names="Calibri" ClientValidationFunction="ValidateZone"
    ErrorMessage="*"></asp:CustomValidator>

     </td>
    </tr>

    <tr class="EditorAlternate">
    <td class="Text" >Woreda :</td>
    <td class="Input" ><asp:DropDownList ID="cboWoreda" runat="server"  ></asp:DropDownList>
            
    
    
        <cc1:CascadingDropDown ID="cboWoreda_CascadingDropDown" runat="server" 
            Category="Woreda" Enabled="True" LoadingText="Loading Woredas" 
            ParentControlID="cboZone" PromptText="Please Select woreda" 
            ServiceMethod="GetWoredas" ServicePath="~\Location.asmx" 
            TargetControlID="cboWoreda" UseContextKey="True">
        </cc1:CascadingDropDown>
       <asp:CustomValidator ID="crfvWoreda" ValidationGroup="Save" runat="server" Font-Size="Small" Font-Names="Calibri" ClientValidationFunction="ValidateWoreda"
    ErrorMessage="*"></asp:CustomValidator>

                     </td>
    </tr> 
    
    
    
    
<tr >
    <td class="Text" >Production Year :</td>
    <td colspan="3"  class="Input"><asp:DropDownList ID="cboProductionYear" runat="server" ></asp:DropDownList>
    </td>
</tr>

<tr class="EditorAlternate">
<td class="Text">Estimated Weight (kg) :</td>
<td  class="Input"> <asp:TextBox ID="txtWeight" runat="server" ></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtWeight" CssClass="Input" ValidationGroup="Save"
            ErrorMessage="Only Decimal Number Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
</td>
</tr>
<tr >
<td class="Text" >Estimated No. of Bags :</td>
<td  class="Input"> 
    
                 <asp:TextBox ID="txtNumberOfBags" Text="" runat="server"></asp:TextBox>
                 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtNumberOfBags" CssClass="Input" ValidationGroup="Save"
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
                 </td>
</tr>
<tr class="EditorAlternate">
    
    
<td class="TextRequired">Date Received :</td>
<td colspan="3"  class="Input" >
     <asp:TextBox ID="txtArrivalDate" runat="server" ></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="txtArrivalDate">
        </cc1:CalendarExtender>
        
         <asp:RequiredFieldValidator ID="rvArrival" ValidationGroup="Save" ControlToValidate="txtArrivalDate" runat="server" 
         ErrorMessage="*"></asp:RequiredFieldValidator>
        
         <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="txtArrivalDate" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator>
     </td>
     </tr>
     <tr>       
           <td  class="TextRequired">Time Received :</td> 
            <td>
                    <asp:TextBox ID="txtTimeArrival" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeArrival_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeArrival" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
            runat="server" ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
      </td>
    
</tr>
<tr class="EditorAlternate" >
<td  class="Text">Remark :</td>
<td colspan="3" class="Input">

    <asp:TextBox 
                        ID="txtRemark" runat="server" Height="62px" TextMode="MultiLine" 
                        Width="538px" style="margin-top: 0px"></asp:TextBox>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="4" align="Left" >
<asp:Button ID="btnSave" runat="server" Text="Save" 
          onclick="btnSave_Click" style="margin-bottom: 0px; height: 26px;" 
        Width="95px" ValidationGroup="Save" />&nbsp;
          <asp:Button ID="btnClear" runat="server" CausesValidation="false" 
        Text="Cancel" Width="95px"  style="margin-bottom: 0px; height: 26px;" 
        onclick="btnClear_Click" />
         </td>
</tr>

</table>
<script language="javascript" type="text/javascript">
       function ValidateRegion(Source, args) {
           var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
           var cboRegion = document.getElementById('<%= cboRegion.ClientID %>');
          
           if (chkAnswer.checked == true) {
               if (cboRegion.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       function ValidateZone(Source, args) {
           var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
           var cboZone = document.getElementById('<%= cboZone.ClientID %>');

           if (chkAnswer.checked == true) {
               if (cboZone.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       function ValidateWoreda(Source, args) {
           var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
           var cboWoreda = document.getElementById('<%= cboWoreda.ClientID %>');
        
           if (chkAnswer.checked == true) {
               if (cboWoreda.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
</script>


