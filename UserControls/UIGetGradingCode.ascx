<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIGetGradingCode.ascx.cs" Inherits="WarehouseApplication.UserControls.UIGetGradingCode" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
       <br />
        <table class="PreviewEditor" style="width:600px" >
        <tr class="EditorAlternate">
            <td class="TextRequired" colspan="2">Related Sample Code</td>
        </tr>
        <tr >
             <td class="Message" colspan="2">
                 <asp:Label ID="lblMsg"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td class="Text"><asp:Label ID="lblSampleCode" runat="server" Text="Sample Code :"></asp:Label></td>
        <td class="Input">
            <asp:DropDownList ID="cboSampleCode" runat="server" Width="250px" 
                onselectedindexchanged="cboSampleCode_SelectedIndexChanged">

            </asp:DropDownList>
        </td>
        </tr>
        <tr>
            <td>Sampling Result Date:</td>
            <td><asp:TextBox ID="txtSamplingResultDate" Enabled="false" runat="server" ></asp:TextBox></td>
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
                    ControlToCompare="txtSamplingResultDate" 
                    ControlToValidate="txtDateCodeGenrated" Display="Dynamic" 
                    ErrorMessage="Date Code Genrated can't be less than Sampling Result Date" 
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
        <tr>
        <td>
            <asp:HiddenField ID="hfTrackingNo" runat="server" />
            </td>
        </tr>

        </tr>


<asp:Panel ID="pnlGradingDetail" runat="server" Visible="true">
            

         <tr class="EditorAlternate">
            <td class="TextRequired" colspan="2">Select Grader/Cupper</td>
         </tr>
        <tr>
        <td class="Text" >Grader/Cupper :</td>
      <td>
          <asp:DropDownList ID="cboGrader" runat="server" Width="250px">
            </asp:DropDownList>
          <asp:RequiredFieldValidator ID="rfvGrader" ControlToValidate="cboGrader" ValidationGroup="SelectGrader" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
            </tr> 
            <tr>
                    <td>Is Supervisor?</td>
                    <td>
                        <asp:CheckBox ID="chkIsSupervisor" runat="server" /></td>
        </tr>
        <tr>
        <td class="EditorAlternate" colspan="2">
            <asp:Button ID="btnAddGrader" runat="server" Text="Add " 
                CausesValidation="true" ValidationGroup="SelectGrader" onclick="btnAddGrader_Click" /></td>
        </tr>
        <tr>
        <td></td>
        </tr>
        </table>
        <br />
        
        <table class="PreviewEditor" style="width:600px">
        <tr>
        
        <td colspan="2">
            <asp:GridView ID="gvGrader" Width="600px" runat="server" 
                AutoGenerateColumns="False" CssClass="Grid" onrowcommand="gvGrader_RowCommand">
                <RowStyle CssClass="GridRow" />
                <Columns>
                    <asp:BoundField ItemStyle-Width="300px"  HeaderStyle-HorizontalAlign="Left" HeaderText="Grader/Cupper" 
                        DataField="GraderName" >
<ItemStyle Width="300px"></ItemStyle>
                    </asp:BoundField>
                    <asp:CheckBoxField ItemStyle-Width="100px" HeaderText="Supervisor" 
                        DataField="IsSupervisor" >
<ItemStyle Width="100px"></ItemStyle>
                    </asp:CheckBoxField>
                    <asp:ButtonField CommandName="Remove" Text="Remove" />
                </Columns>
                <SelectedRowStyle CssClass="GridSelectedRow" />
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="GridAlternate" />
            </asp:GridView>
            </td>
        </tr>
            
        <tr class="EditorCommand" >
                <td class="Button" colspan="3" style="text-align:left" align=center>
        <asp:Button ID="btnGenerateCode" runat="server" Height="30px" Text="Generate Code" Width="125px" onclick="btnGenerateCode_Click" 
                />
        </td>
        </tr>
            </asp:Panel>
            
  </table>
  <br />
   <table class="PreviewEditor" style="width:600px" >
          <tr class="PreviewEditorCaption">
            <td colspan="2">Generated Code List</tr>
        <tr>
        <td>
            <asp:GridView ID="gvCodeGenerated"  Width="600px" runat="server" 
                AutoGenerateColumns="False" CssClass="Grid" 
                  onrowcommand="gvCodeGenerated_RowCommand">
                <RowStyle CssClass="GridRow" />
                <Columns>
  
                    <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                    <asp:BoundField ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" HeaderText="Code" 
                        DataField="GradingCode" >
<ItemStyle Width="300px"></ItemStyle>
                    </asp:BoundField>
                    <asp:ButtonField CommandName="Print" Text="Print" />
                    
                </Columns>
                <SelectedRowStyle CssClass="GridSelectedRow" />
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="GridAlternate" />
            </asp:GridView>
        </td>
        
        </tr>
        <tr class="EditorCommand" >
        <td align="left" >
        <asp:Button ID="btnNext" runat="server" Text="Next" 
           style="margin-bottom: 0px; height: 26px;" 
        Width="95px" onclick="btnNext_Click"  />&nbsp;
         
        </td>
        </tr>
    </table>