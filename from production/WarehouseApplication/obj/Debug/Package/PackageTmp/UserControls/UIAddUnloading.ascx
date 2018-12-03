<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddUnloading.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddUnloading" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <style type="text/css" >
        .EditWidth
        {
        	width:850px;
        }
        
.modalBackground {
	background-color:White;
	filter:alpha(opacity=50);
	opacity:0.5;
}
.popupStyle
        {
        	background-color:White;
        	width:618px
        }
        .modalPopup {
	background-color:#ffffdd;
	border-width:3px;
	border-style:solid;
	border-color:Gray;
	padding:3px;
	width:250px;
}
    </style>
<asp:UpdatePanel ID="updatePanelMain" runat="server" UpdateMode="Always">
<ContentTemplate>
<table class="PreviewEditor EditWidth"   >
<tr class="PreviewEditorCaption">
<td colspan="4">Deposit Information Entry</td>
</tr>
<tr>
<td  colspan="4" class="Message" ><asp:Label ID="lblmsg" runat="server"></asp:Label>
    
    </td>
</tr>
<tr>
    
    <td class="Text"   >
        <asp:Label ID="Label1" runat="server" Width="150px" Text="Grading Code :"></asp:Label></td>
    <td class="Input">
    <asp:Panel runat="server" ID="pnl" Enabled="false">
    
    
       <asp:DropDownList ID="cboGradingCode" runat="server" Width="150px" Enabled="false" 
            
            ValidationGroup="Unloading" 
            onselectedindexchanged="cboGradingCode_SelectedIndexChanged" 
            AutoPostBack="True">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="cboGradingCode" 
            ErrorMessage="Please enter Grading Code" ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </asp:Panel>
    </td>
</tr>
<tr class="EditorAlternate">
    <td >Commodity Grade : </td>
    <td class="Input"><asp:Label ID="lblCommodityGrade" runat="server" Text=""></asp:Label>
   (<asp:Label ID="lblProductionYear" runat="server" Text=""></asp:Label>)
    
    </td>
</tr>
<tr>
<td>Bag Type : </td>
<td>
    <asp:DropDownList ID="cboBagType" Width="150px"  runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvBagType" runat="server" 
        ControlToValidate="cboBagType" ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="cboBagType" ErrorMessage="*"  Display="Dynamic" ValidationGroup="Add"></asp:RequiredFieldValidator>
</td>
</tr>

<tr class="EditorAlternate">
    <td >Total No. of Bags :</td>
    <td class="Input"><asp:TextBox ID="txtNumberOfBags" runat="server">0</asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtNumberOfBags" 
            ErrorMessage="Please enter Total No. bags" ValidationGroup="Save">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Save" runat="server" ValueToCompare="0" ControlToValidate="txtNumberOfBags" 
	ErrorMessage="Must enter positive integers" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
            
 
                 </td>
</tr>
<tr>
<td>Client Acceptance Date:</td>
<td>
    <asp:TextBox ID="lblCADateTime" runat="server" Text="" ></asp:TextBox>
    </td>
</tr>
<tr  class="EditorAlternate">
    <td >Date Deposited :</td>
    <td class="Input" style="vertical-align:bottom"><asp:TextBox ID="txtDateDeposited" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateDeposited_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateDeposited">
        </cc1:CalendarExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  Display="Dynamic"
            runat="server" ControlToValidate="txtDateDeposited" 
            ErrorMessage="Please select date deposited" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                 <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="txtDateDeposited" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
 <asp:CompareValidator ControlToValidate="txtDateDeposited" ControlToCompare="lblCADateTime" 
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Grade Acceptance date is less than the Date Deposited" 
                 Type="Date" ValidationGroup="Add" Operator="GreaterThanEqual"></asp:CompareValidator>  
           
                 </td>
</tr>
<tr>       
           <td>Time Deposited :</td> 
            <td>
                    <asp:TextBox ID="txtTimeDeposited" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeDeposited_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeDeposited" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Save" 
            runat="server" ControlToValidate="txtTimeDeposited" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
      </td>
    
</tr>
</table>
<table class="PreviewEditor EditWidth">
<tr class="PreviewEditorCaption">
<td class="text">Shed </td>
<td class="Text">Stack No. </td>
<td class="Text" align="center">No. of Bags <br />(per Stack)</td>
<td class="Text">Inventory Controller </tr>
</tr>
<tr class="EditorAlternate">
<td class="Input">
<asp:DropDownList ID="cboShed" runat="server" Width="100px" AutoPostBack="True" 
        onselectedindexchanged="cboShed_SelectedIndexChanged" 
        ValidationGroup="Add">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvShed" runat="server" 
        ControlToValidate="cboShed" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
</td>
<td class="Input"><asp:DropDownList ID="cboStackNo" runat="server" Width="200px">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvStack" runat="server" 
        ControlToValidate="cboStackNo" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
    </td>
    
<td ><asp:TextBox ID="txtStackNoBags" Width="100px" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNoBagsPerStack" runat="server" 
        ControlToValidate="txtStackNoBags" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
        <asp:CompareValidator Display="Dynamic" ID="CompareValidator2" ValidationGroup="Add" runat="server" ValueToCompare="0" ControlToValidate="txtStackNoBags" 
	ErrorMessage="Must enter positive integers" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
    </td>
<td ><asp:DropDownList ID="cboUnloadedBy" runat="server" Width="250px">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvIC" runat="server" 
        ControlToValidate="cboUnloadedBy" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
    </td>

<tr>
<td class="Text" style="vertical-align:top ">Remark :</td><td>

<asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
    </td>
<td colspan="2" class="Input">
    &nbsp;</td></tr>
<tr class="EditorAlternate">
<td colspan="4">
    <asp:Button ID="btnAdd" runat="server" Text="Add" 
            onclick="btnAdd_Click" CausesValidation="true" CommandName="Add" 
        Width="83px" ValidationGroup="Add" />
    <asp:HiddenField ID="hfRecivingRequestId" runat="server" />

        <asp:HiddenField ID="hfTrackingNo" runat="server" />
    
    </td>
</tr>
</table>
<table width="800px" >
<tr>
<td colspan="4" class="Input">
    <asp:Label ID="lblMsg2" runat="server" CssClass="Message"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="4">

     <asp:GridView ID="gvStackUnloaded" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" 
        GridLines="None" 
         onrowdeleting="gvStackUnloaded_RowDeleting" CssClass="Grid" Width="700px" 
         AllowPaging="True" PageSize="5" 
         >
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <RowStyle CssClass="GridRow" />
        <Columns>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderText="Stack No" 
                DataField="StackNo" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Number of Bags" 
                DataField="NumberOfbags" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
                ItemStyle-HorizontalAlign="Left" HeaderText="Inventory Controller" 
                DataField="InventoryControllerName" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:ButtonField  CommandName="Delete" Text="Remove"  />
            
        </Columns>
        <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
             CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
        <EditRowStyle BackColor="#7C6F57" />
         <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>

</td>
</tr>

<tr class="EditorCommand">
    <td class="Button" colspan="8" style="text-align:left">
        <asp:Button ID="btnSave" runat="server" Text="Save" 
            onclick="btnSave_Click" Width="95px" style="margin-bottom: 0px; height: 26px;" 
            ValidationGroup="Save" />
            &nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btnClear" runat="server" Text="Cancel" onclick="btnClear_Click" style="margin-bottom: 0px; height: 26px;" 
        Width="95px"
            CausesValidation="False" />    
            
            </td>
            
</tr> 
<asp:Button ID="btnDummy" runat="server" style="display:none" />  
<asp:Button ID="btnOkay" runat="server" style="display:none" />  
<asp:Button ID="btnNotOkay"  onclick="btnNotOkay_Click" runat="server" style="display:none" />  

</table>

  <asp:Panel ID="Panel1" runat="server" UpdateMode="Conditional" Style="display: none" >
    <ContentTemplate>
            <table style="width:550px;background-color:White">
                <tr>
                    <td class="EditorCaption">Warning:</td>
                </tr>
                <tr>
                   <td class="Message"><span id="txtReason" runat="server"/>
                   <asp:Label ID="lblModalMsg" runat="server" Visible="false" Text="Label"></asp:Label>
                   </td>
                </tr>
                <tr>
                    <td class="EditorCommand">
                     <asp:Button ID="OkButton"  runat="server" Text="Yes" /><asp:Button ID="btnCancelPopUp1" Style="display: none"   runat="server" Text="Yes" />
                     <asp:Button ID="btnCancelPopUp" onclick="btnCancelPopUp_Click"    runat="server" Text="No" />
                   </td>
                </tr>
            </table>
     </ContentTemplate>
  
   <div align="center">
   </div>
   </asp:Panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
         TargetControlID="btnSave" 
         PopupControlID="Panel1"
         BackgroundCssClass="modalBackground"
         DropShadow="true"         
         OkControlID="btnSave"
         CancelControlID="btnCancelPopUp1">
</cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server">
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel> 
  


    
