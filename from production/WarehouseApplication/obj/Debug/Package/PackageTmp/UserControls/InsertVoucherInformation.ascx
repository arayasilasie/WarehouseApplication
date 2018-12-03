<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertVoucherInformation.ascx.cs" Inherits="WarehouseApplication.UserControls.InsertVoucherInformation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:HiddenField ID="CommodityDepositRequestId" runat="server" />
<asp:HiddenField ID="VoucherId" runat="server" />
<style type="text/css">
    .AdjustWidth
    {
        width: 824px;
    }
    .trWidth
    {
    	width: 100px;
    }
    .style1
    {
        width: 150px;
    }
</style>
<table class="PreviewEditor AdjustWidth" border="0" style="width: 800px; ">
<tr>
<td  colspan="2" class="PreviewEditorCaption">Voucher Information</td>
</tr>
<tr>
<td colspan="2"  class="Text" align="Left">
    <asp:Label ID="lblMessage" runat="server" style="color: #CC0000" 
        CssClass="Message" ></asp:Label></td>
</tr>
<tr class="trWidth" >
    <td class="style1">Voucher No. :</td>
    <td class="Input" >
        <asp:TextBox ID="txtVoucherNo" runat="server" Width="196px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" 
            ControlToValidate="txtVoucherNo" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    
    
</tr>
<tr class="EditorAlternate">
<td class="style1">Is Bi-product?</td>
    
<td><asp:CheckBox ID="chkIsBiProduct" runat="server" /></td>
</tr>
<tr >
    <td class="style1">Coffee Type :</td>
    <td class="Input" style="width: 354px;" >
       
    <asp:DropDownList ID="cboCoffeeType" runat="server" Width="200" 
            DataTextField="CoffeeType" DataValueField="Id">
    </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RFVCoffeeType" runat="server" 
            ControlToValidate="cboCoffeeType" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    
</tr>
<tr class="EditorAlternate">
    <td class="style1">Specific Area :</td>
    <td  class="Input" style="width: 354px;">
        <asp:TextBox ID="txtSpecificArea" runat="server" Width="198px"></asp:TextBox>
        <asp:CustomValidator ID="crfvSpecificArea" runat="server" ClientValidationFunction="ValidateSpecificArea"
    ErrorMessage="*"></asp:CustomValidator>
    </td>
    
</tr>
<tr >
    <td class="style1">No. of Bags :</td>
    <td class="Input" style="width: 354px;">
       
        <asp:TextBox ID="txtNumberOfBags" runat="server" Width="198px" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNumberofBags" runat="server" 
            ControlToValidate="txtNumberOfBags" ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ControlToValidate="txtNoPlomps" CssClass="Input" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
            <asp:CompareValidator
                ID="CompareValidator2" Display="Dynamic"  ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" ControlToValidate="txtNumberOfBags" ErrorMessage="Value Must be greater than 0."></asp:CompareValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="Only Positive Number Accepted" 
            ControlToValidate="txtNumberOfBags" MinimumValue="0" MaximumValue="100000" Type="Integer"></asp:RangeValidator>
            
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="style1">Truck No. Plomps :</td>
    <td class="Input" style="width: 354px;" >
        <asp:TextBox ID="txtNoPlomps" runat="server" Width="50px"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtNoPlomps" CssClass="Input"   Display="Dynamic" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="rfNoPlomps" runat="server"  Display="Dynamic" ControlToValidate="txtNoPlomps" 
            ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr >
    <td class="style1" >Trailer No. Plomps :</td>
    <td  style="width: 354px;" >
            
        <asp:TextBox ID="txtTrailerNoPlomps" runat="server" Width="50px"></asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtTrailerNoPlomps" CssClass="Input"   Display="Dynamic" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvTrailerNoPlomps" runat="server"  Display="Dynamic" ControlToValidate="txtTrailerNoPlomps" 
            ErrorMessage="*"></asp:RequiredFieldValidator>
            
         <br />
            
    </td>
</tr>

<tr class="EditorCommand">
    
<td colspan="2"  align="left" class="Button" >
    <asp:Button ID="btnSave" runat="server" Text="Save" 
        Width="78px" onclick="btnSave_Click" style="height: 26px" />&nbsp;<asp:Button ID="btnNext" runat="server" 
        Text="Next" Width="77px" onclick="btnNext_Click" /><asp:TextBox ID="txtCertificateNo" Visible="false" runat="server" Width="168px"></asp:TextBox></td>
</tr>
</table>
<script type="text/javascript" >
    function ValidateSpecificArea(Source, args) {
        var chkAnswer = document.getElementById('<%= chkIsBiProduct.ClientID %>');
        var txtLN = document.getElementById('<%= txtSpecificArea.ClientID %>');

        if (chkAnswer.checked == false) {
            if (txtLN.value == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
    function ValidatePlomp(Source, args) {
        var chkAnswer = document.getElementById('<%= chkIsBiProduct.ClientID %>');
        var txtLN = document.getElementById('<%= txtSpecificArea.ClientID %>');

        if (chkAnswer.checked == false) {
            if (txtLN.value == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
</script>
