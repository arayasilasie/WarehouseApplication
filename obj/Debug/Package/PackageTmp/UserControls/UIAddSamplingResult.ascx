<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddSamplingResult.ascx.cs" Inherits="WarehouseApplication.UserControls.SamplingResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<table class="PreviewEditor" style="width:825px">
<tr class="PreviewEditorCaption" >
    <td colspan="2" >Add Sampling Result</td>
</tr>
<tr>
<td colspan=2 class="Message">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
    <td class="TextRequired">Sample Code :</td>
    <td class="Input">
   <asp:Panel ID="pnlSC" Enabled="false" runat="server" > 
        <asp:DropDownList ID="cboSampleCode" runat="server"  Width="250" 
            onselectedindexchanged="cboSampleCode_SelectedIndexChanged" 
            AutoPostBack="True" >
        </asp:DropDownList>
           <asp:RequiredFieldValidator ID="rfvSampleCode" runat="server" ErrorMessage="*" ValidationGroup="SubmitAll" ControlToValidate="cboSampleCode"></asp:RequiredFieldValidator>
                 </asp:Panel>
                 </td>
</tr>
<tr class="EditorAlternate"> 
    <td class="TextRequired">Sampler :</td>
    <td class="Input">
        <asp:DropDownList ID="cboSampler" Width="250" runat="server" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvSampler" runat="server" ErrorMessage="*" 
            ControlToValidate="cboSampler" ValidationGroup="Add" ></asp:RequiredFieldValidator>
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="TextRequired">No. of Separations :</td>
    <td class="Input">
        <asp:TextBox ID="txtNumberOfSeparations" runat="server" Text="1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfNoSeprations" runat="server" ErrorMessage="*" 
            ControlToValidate="txtNumberOfSeparations" ValidationGroup="SubmitAll" ></asp:RequiredFieldValidator>
            
            
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtNumberOfSeparations" CssClass="Input"   Display="Dynamic" ValidationGroup="SubmitAll" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
            <asp:CompareValidator
                ID="CompareValidator1" Display="Dynamic" ValidationGroup="SubmitAll" ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" ControlToValidate="txtNumberOfSeparations" ErrorMessage="Value Must be greater than 0."></asp:CompareValidator>
            
                 </td>
</tr>
<tr>
    <td class="Text">Sampler Comment : </td>
    <td class="Input">
        <asp:TextBox ID="txtSamplerCommment" TextMode="MultiLine" runat="server" Height="61px" 
            Width="353px"></asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Is Supervisor :</td>
    <td class="Input">
        <asp:CheckBox ID="chkisSupervisor" runat="server" Checked="True" /></td>
</tr>
<tr >
<td>Sample Generated Date :</td>
<td>
    <asp:TextBox ID="lblSampleGenratedDateTime" runat="server" Enabled="false" ></asp:TextBox>
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="Text"> Result Received Date :</td>
    
         <td  class="Input" abbr="Left" >
     <asp:TextBox ID="txtArrivalDate" runat="server" ></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="txtArrivalDate">
        </cc1:CalendarExtender>
        
         <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="txtArrivalDate"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" Display="Dynamic" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator> 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ValidationGroup="Save"
            ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator>
          <%-- <asp:CompareValidator ControlToValidate="txtArrivalDate" 
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Result received date is less than the Sample Generated Date" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                 
         
             </td>
        
    </td>
</tr>
<tr>

     
          
           <td> Result Received Time :</td> 
            <td>
                    <asp:TextBox ID="txtTimeArrival" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeArrival_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeArrival" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            runat="server" ControlToValidate="txtTimeArrival" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
      </td>
    
</tr>
<tr class="EditorAlternate">
    <td class="Text">Plomps Ok :</td>
    <td class="Input">
        <asp:CheckBox ID="chkPlomps" runat="server" />
        
    </td>
</tr>
<tr >
    <td class="Text">Remark</td>
    <td class="Input">
        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="61px" 
            Width="353px"></asp:TextBox></td>
</tr>
<tr class="PreviewEditorCaption">
<td colspan="2">Sampling Result Detail</td>
</tr>
<tr>
<td>No. Bags : &nbsp;<asp:TextBox ID="txtNumberofbags" runat="server" Width="33px"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator ID="rfVNoBags" runat="server" 
        ControlToValidate="txtNumberofbags" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>
    <asp:Button ID="btnAdd" runat="server" Text="Add " onclick="btnAdd_Click" 
        ValidationGroup="Add" />
</td>
<td>
    
    <asp:RegularExpressionValidator ID="viNumberofBags" runat="server" Display="Dynamic"
        ControlToValidate="txtNumberofbags" CssClass="Input" 
        ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
        ValidationExpression="[0-9]{0,}" ValidationGroup="Add"></asp:RegularExpressionValidator>
        
         <asp:CompareValidator
                ID="CompareValidator2" Display="Dynamic" ValidationGroup="Add" ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" ControlToValidate="txtNumberofbags" ErrorMessage="Value Must be greater than 0."></asp:CompareValidator>
    
</td>
</tr>
<tr>
<td colspan="2">

              <asp:GridView ID="gvSamplingResultDetail" Width="600px" runat="server" 
                AutoGenerateColumns="False" CssClass="Grid" onrowcommand="gvSamplingResultDetail_RowCommand">
                <RowStyle CssClass="GridRow" />
                <Columns>
 <asp:TemplateField HeaderText="Sampling Result Code">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate> 
                    <ItemTemplate>
                        <asp:Label ID="lblSamplingResultCode" runat="server" Text='<%# Bind("SamplingResultCode") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="No Bags">
                    <ItemTemplate>
                        <asp:Label ID="lblNobags" runat="server" Text='<%# Bind("NumberOfBags") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>        
                <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdRemove" CausesValidation="false" CommandName="cmdRemove" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Remove</asp:LinkButton></ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdPrint" Visible="false" CausesValidation="false" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Print</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdSelect" Visible="false" CausesValidation="false" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Print</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
                <SelectedRowStyle CssClass="GridSelectedRow" />
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="GridAlternate" />
            </asp:GridView>
  
  
  
  
  
</td>
</tr>
<tr  class="EditorCommand">
    <td colspan="2" align="Left" class="Input">
        <asp:Button ID="btnSave" runat="server" Text="Save" Width="95px" ValidationGroup="SubmitAll" 
            onclick="btnSave_Click"  />
        
        &nbsp;<asp:Button ID="btnNext" Enabled="false" runat="server" Text="Next" Width="95px" 
            onclick="btnNext_Click" CausesValidation="False"  />
        
        </td>
</tr>

<asp:Panel runat="server" ID="pnlManagerApproval" Width="825px">
<tr  class="EditorAlternate">
<td colspan="2">Manager Approval</td>
</tr>
<tr>
<td>Sampling Result Code:</td>
<td>
    <asp:Label ID="lblSamplingResultCode" runat="server"></asp:Label>
    <asp:HiddenField ID="hfSamplingResultId" runat="server" />
    </td>
</tr>
<tr>
<td>Tracking No:</td>
<td>
    <asp:Label ID="lblResultTrackingNo" runat="server"></asp:Label>
    </td>
</tr>
<tr>
<td class="EditorAlternate">Status:</td>
<td>
    <asp:DropDownList ID="cboResultStatus" runat="server">
        <asp:ListItem Value="">Please Select Status</asp:ListItem>
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="2">Approved</asp:ListItem>
        <asp:ListItem Value="2">Cancelled</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvResultStatus" ValidationGroup="ManagerApproval" ControlToValidate="cboResultStatus" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td>Remark</td>
<td>
    <asp:TextBox ID="txtManagerApproval" TextMode="MultiLine" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvtxtManagerApproval" ValidationGroup="ManagerApproval" runat="server" ControlToValidate="txtManagerApproval" ErrorMessage="*"></asp:RequiredFieldValidator></td>
</tr>
<tr class="EditorCommand">
<td colspan="2"   align="left" >
 <asp:Button ID="btnManagerApprove" ValidationGroup="ManagerApproval" runat="server" Text="Update" Width="95px" 
            onclick="btnManagerApprove_Click"  />
</td>
</tr>

</table>
</asp:Panel>