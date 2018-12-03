<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditCGTotalValue.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditCGTotalValue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:700px" >
<tr class="PreviewEditorCaption">
    <td colspan="2">Edit Commodity Grade Grading Value</td>
</tr>
<tr>
    <td class="Text" colspan="4">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label>
    </td>
</tr>
<tr  class="EditorAlternate">
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity :"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="250px"  
              >
        </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
                    Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
                    PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
                    ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity">
                </cc1:CascadingDropDown>
            </td>
</tr>
<tr>
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class :"></asp:Label>
        </td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="250px" 
            ></asp:DropDownList>
            <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
                Category="CommodityClass" Enabled="True" 
                LoadingText="Loading Commodity Class..." ParentControlID="cboCommodity" 
                PromptText="Please Select Commodity Class" ServiceMethod="GetCommodityClass" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
            </cc1:CascadingDropDown>
        </td>
</tr>
<tr  class="EditorAlternate">
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade :"></asp:Label>
            </td>
            <td class="Input">

<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="250px" AutoPostBack="True" onselectedindexchanged="cboCommodityGrade_SelectedIndexChanged" 
                     ></asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
                    Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
                    ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
                    ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
                    TargetControlID="cboCommodityGrade">
                </cc1:CascadingDropDown>
            </td>
        </tr>
<tr>
 <td>Minimum Value :</td>
 <td><asp:TextBox ID="txtMinimumValue" runat="server" Text="" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
         ControlToValidate="txtMinimumValue" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
         ControlToValidate="txtMinimumValue" Display="Dynamic" 
         ErrorMessage="Only Flaoting Number Allowed" 
         ValidationExpression="[-+]?([0-9]*\.[0-9]+|[0-9]+)" ValidationGroup="Save"></asp:RegularExpressionValidator>
    </td>
 </tr>
 <tr class="EditorAlternate">
 <td>Maximum Value</td>
 <td><asp:TextBox ID="txtMaxValue" runat="server" Text="" ></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
         ControlToValidate="txtMaxValue" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
         ControlToValidate="txtMaxValue" Display="Dynamic" 
         ErrorMessage="Only Flaoting Number Allowed" 
         ValidationExpression="[-+]?([0-9]*\.[0-9]+|[0-9]+)" ValidationGroup="Save"></asp:RegularExpressionValidator>
     <asp:CompareValidator ID="CompareValidator1" runat="server" 
         ControlToCompare="txtMaxValue" ControlToValidate="txtMinimumValue" 
         Display="Dynamic" ErrorMessage="Please Check Minimum and Maximum Values" 
         Operator="LessThan" Type="Integer"></asp:CompareValidator>
     <cc1:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" 
         runat="server" Enabled="True" TargetControlID="CompareValidator1">
     </cc1:ValidatorCalloutExtender>
     </td>
 </tr>
 <tr>
 <td>Status :</td>
     
 <td>
     <asp:DropDownList ID="cboStatus" runat="server">
        <asp:ListItem Text="Please Select Status" Value=""></asp:ListItem>
        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
        <asp:ListItem Text="Cancelled" Value="2"></asp:ListItem>
     </asp:DropDownList>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
            ControlToValidate="cboStatus" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
      </td>
 </tr>
 <tr class="EditorCommand">
 <td colspan="2"  >
  <asp:Button ID="btnSave" runat="server" Text="Update" 
          onclick="btnSave_Click" style="margin-bottom: 0px; height: 26px;" 
        Width="95px" ValidationGroup="Save" />
 </td>
 </tr>
</table>