<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddCommodityGradeGradingFactor.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddCommodityGradeGradingFactor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption">
    <td class="Text" colspan="4">Grading Factor</td>
</tr>
<tr>
 <td class="Text" colspan="3">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label>
 </td>
</tr>
<tr class="EditorAlternate">
<td class="Text" >
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity :" Width="169px"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="300px" 
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
            Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
            PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
            ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity">
        </cc1:CascadingDropDown>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="cboCommodity" ErrorMessage="*" 
            Font-Names="Calibri"></asp:RequiredFieldValidator>
            </td>
</tr>
<tr>
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class"></asp:Label>
        &nbsp;:</td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="300px" 
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
            Category="CommodityClass" Enabled="True" LoadingText="Loading Commodity Class..." 
            ParentControlID="cboCommodity" PromptText="Please Select Commodity Class" 
            ServiceMethod="GetCommodityClass" ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
        </cc1:CascadingDropDown>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="cboCommodityClass" Font-Names="Calibri" ErrorMessage="*" 
            Font-Size="Small"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr class="EditorAlternate">
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade"></asp:Label>
            &nbsp;:</td>
            <td class="Input">

<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="300px" 
                    onselectedindexchanged="cboCommodityGrade_SelectedIndexChanged" ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
            Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
            ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
            ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
            TargetControlID="cboCommodityGrade" UseContextKey="True">
        </cc1:CascadingDropDown>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="cboCommodityGrade" Font-Names="Calibri" ErrorMessage="*" 
            Font-Size="Small"></asp:RequiredFieldValidator>
            </td>
        </tr>
<tr class="PreviewEditorCaption">
<td colspan="2" class="Text">Add Grading Factor</td>
</tr>
<tr>
<td class="Text">Grading Factor Name:</td>
<td>
    <asp:TextBox ID="txtFactorName" runat="server"></asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
<td valign="top" class="Text">Possible Values :
</td>
<td class="Input">
    <asp:TextBox ID="txtPossibleValues" runat="server" Height="53px" Width="246px"></asp:TextBox></td>
</tr>
<tr>
<td class="Text">Status : </td>
<td class="Input">
    <asp:DropDownList ID="cboStatus" runat="server">
        <asp:ListItem>Please Select status</asp:ListItem>
        <asp:ListItem Value="1">Active</asp:ListItem>
        <asp:ListItem Value="2">Cancelled</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr  class="EditorAlternate">
<td class="Text">Rank :</td>
<td class="Input">
    <asp:TextBox ID="txtRank" runat="server" Width="50px"></asp:TextBox></td>
</tr>
<tr>
<td Class="Text" colspan="2">Maximum Value : 
    <asp:TextBox ID="txtMaximimValue" CssClass="Input"  runat="server" Width="50px"></asp:TextBox>
    &nbsp;  Value : 
    <asp:TextBox ID="txtMinimumValue" CssClass="Input" runat="server"></asp:TextBox>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Failing Point :</td>
<td>
    <asp:TextBox CssClass="Input" ID="txtFailingPoint" runat="server" Height="22px"></asp:TextBox>
</td>
</tr>
<tr>
<td>Is Max:</td>
<td>
    <asp:DropDownList CssClass="Input" ID="cboIsMax" runat="server">
        <asp:ListItem>Please Select status</asp:ListItem>
        <asp:ListItem Value="1">Equal</asp:ListItem>
        <asp:ListItem Value="2">Greater</asp:ListItem>
        <asp:ListItem Value="3">Lesser</asp:ListItem>
        <asp:ListItem Value="4">Greater or equal to</asp:ListItem>
        <asp:ListItem Value="4">Less or equal to</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Is in Total Value:</td>
<td>
    <asp:CheckBox CssClass="Input" ID="chkIsInTotalVale" runat="server" /></td>
</tr>
<tr class="EditorCommand">
    <td colspan="2" align="left">
        <asp:Button ID="btnSave" Width="75px" runat="server" Text="Save" 
            onclick="btnSave_Click" /></td>
</tr>
</table>