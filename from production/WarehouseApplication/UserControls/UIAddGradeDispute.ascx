<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddGradeDispute.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddGradeDispute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style=" width:800px" >
<tr class="PreviewEditorCaption">
             <td class="Text" colspan="3">Add Grade Dispute</td>
        </tr>
        <tr>
             <td class="Text" colspan="3">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td class="Text"><asp:Label ID="lblGradingCode" runat="server" 
                Text="Disputed Grading Code"></asp:Label>:</td>
        <td class="Text">
            <asp:DropDownList ID="cboGradingCode" runat="server" Width="300px" 
                onselectedindexchanged="cboGradingCode_SelectedIndexChanged" 
                AutoPostBack="True" >
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                ControlToValidate="cboGradingCode" ></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>Previous Grade:</td>
        <td class="Text">            <asp:Label ID="lblPreviousGrade" runat="server"></asp:Label>
</td>
        </tr>
        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity:"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="300px">
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
            <asp:Label ID="Label1" runat="server" Text="Commodity Class:"></asp:Label>
        </td>
        <td class="Text">
            <asp:DropDownList ID="cboCommodityClass" runat="server" Width="300px">
            </asp:DropDownList>
            <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
                Category="CommodityClass" Enabled="True" 
                LoadingText="Loading Commodity Class..." ParentControlID="cboCommodity" 
                PromptText="Please Select Commodity Class" ServiceMethod="GetCommodityClass" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
            </cc1:CascadingDropDown>
        </td>
        </tr>

        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade:"></asp:Label>
            </td>
            <td class="Input">

                <asp:DropDownList ID="cboCommodityGrade" runat="server" Width="300px">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
                    Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
                    ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
                    ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
                    TargetControlID="cboCommodityGrade">
                </cc1:CascadingDropDown>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="cboCommodityGrade" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
                <td class="Text">
            <asp:Label ID="Label2" runat="server" Text="Dispute Recived Date :"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtDateRecived" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtDateRecived" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="Label3" runat="server" Text="Time "></asp:Label>&nbsp;
                <asp:TextBox ID="txtTimeRecived" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtTimeRecived" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
          <td class="Text">
            <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="4" class="Button" >
        
            <asp:HiddenField ID="hfGradingResultId" runat="server" />
            <asp:HiddenField ID="hfPreviousGradingResult" runat="server" />
            </td>
            
        </tr>
        <tr class="EditorCommand">
        <td colspan="3" align="Left">
        <asp:Button ID="btnSave" runat="server" Height="30px" Text="Save" Width="125px" onclick="btnSave_Click"  
                />
            </td>
         
        </tr>
  </table>