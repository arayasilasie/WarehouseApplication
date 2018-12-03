<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddStack.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddStack"  %>
   <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
  
 
<table class="PreviewEditor" style="width:900px" >
<tr class="PreviewEditorCaption" >
<td colspan="2">Add Stack</td>
</tr>
<tr>
<td colspan="2" class="Message">
    <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
</tr>
<tr>
<td class="Text">Warehouse:</td>
<td class="Input">
    <asp:DropDownList ID="cboWarehouse" Width="150px" runat="server" 
        onselectedindexchanged="cboWarehouse_SelectedIndexChanged" 
        AutoPostBack="True">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvWarehouseId" runat="server"  Font-Names="Calibri" 
        ControlToValidate="cboWarehouse" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Shed</td>
<td class="Input">
    <asp:DropDownList ID="cboShed" Width="150px" runat="server" AutoPostBack="True" 
        onselectedindexchanged="cboShed_SelectedIndexChanged">
    </asp:DropDownList>
</td>
</tr>
<tr>
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="350px" 
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
        <tr class="EditorAlternate">
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class"></asp:Label>
        </td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="350px" 
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

        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade"></asp:Label>
            </td>
            <td class="Input">

<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="350px" 
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
        <tr class="EditorAlternate">
            <td>Production Year :</td>
            <td>
            <asp:DropDownList ID="cboProductionYear" runat="server" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="cboProductionYear" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
<tr >
    <td class="Text">Physical Stack Number</td>
    <td class="Input">
        <asp:DropDownList ID="cboStackNumber" runat="server">
        </asp:DropDownList>
        </asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Stack Name</td>
    <td class="Input"><asp:TextBox ID="txtStackNumber" Enabled="false" Width="150px" runat="server"></asp:TextBox></td>
</tr>
<tr >
<td class="Text">Status</td>
<td class="Input">
    <asp:DropDownList ID="cboStatus" Width="150px" runat="server">
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="2">Approved</asp:ListItem>
        <asp:ListItem Value="3">Closed</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Date Started</td>
<td class="Input" ><asp:TextBox ID="txtDateStarted" runat="server"></asp:TextBox>
    <cc1:CalendarExtender ID="txtDateStarted_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtDateStarted">
    </cc1:CalendarExtender>
    </td>
</tr>
<tr >
<td class="Text">Begining Balance(No. Bags)</td>
<td class="Input">
    <asp:TextBox ID="txtBeginingNoBags" Text="0" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="viNumberofBags" runat="server" Display="Dynamic" 
        ControlToValidate="txtBeginingNoBags" CssClass="Input" 
        ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
        ValidationExpression="[0-9]{0,}" ValidationGroup="Add"></asp:RegularExpressionValidator>
    </td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="Left" class="ForBtn">
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" /></td>
</tr>

</table>
