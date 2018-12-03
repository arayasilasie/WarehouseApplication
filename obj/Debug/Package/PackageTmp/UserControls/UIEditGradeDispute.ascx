<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditGradeDispute.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditGradeDispute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption">
             <td class="Text" colspan="3">Edit Grade Dispute</td>
        </tr>
        <tr>
             <td class="Text" colspan="3">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td Class="Text"><asp:Label ID="lblGradingCode" runat="server" 
                Text="Disputed Grading Code"></asp:Label>:</td>
        <td Class="Text">
            <asp:Label ID="lblGradeCode" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td Class="Text">Previous Grade:</td>
        <td Class="Text">            <asp:Label ID="lblPreviousGrade" runat="server"></asp:Label>
</td>
        </tr>
        <tr>
        <td Class="Text">
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
        <td  Class="Text">
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
        <td Class="Text">
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
            </td>
        </tr>
        <tr>
                <td Class="Text">
            <asp:Label ID="Label2" runat="server" Text="Dispute Received Date :"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtDateRecived" runat="server" Height="22px" Width="128px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtDateRecived" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="Label3" runat="server" Text="Time "></asp:Label>&nbsp;
                <asp:TextBox ID="txtTimeRecived" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeRecived_MaskedEditExtender" runat="server" 
                    AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time" 
                    TargetControlID="txtTimeRecived">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtTimeRecived" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <td Class="Text">Status:</td>
            <td class="Input">
                <asp:DropDownList ID="cboStatus" runat="server" Width="150px">
                    <asp:ListItem>Please Select Status</asp:ListItem>
                    <asp:ListItem Value="1">New</asp:ListItem>
                    <asp:ListItem Value="2">Approved</asp:ListItem>
                    <asp:ListItem Value="3">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>
        <tr>
          <td Class="Text">
            <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="4" class="Button" >
        
            <asp:HiddenField ID="hfGradingResultId" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfTrackingNo" runat="server" />
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
