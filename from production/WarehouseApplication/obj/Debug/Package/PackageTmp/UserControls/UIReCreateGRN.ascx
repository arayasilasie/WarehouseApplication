<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIReCreateGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIReCreateGRN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption">
    <td colspan="4" style="widows:250px">Re-create GRN</td>
    </tr>
    <tr>
    <td colspan="4"><asp:Label ID="lblmsg" Visible="true" runat="server" Text=""></asp:Label></td>
    </tr>
        <tr>
            <td colspan="4">
                <asp:HiddenField ID="hfReceivigRequestId" runat="server" />
                <asp:HiddenField ID="hfTrackingNo" runat="server" />
                <asp:HiddenField ID="hfVoucherId" runat="server" />
                <asp:HiddenField ID="hfClientId" runat="server" />
                <asp:HiddenField ID="hfGRNID" runat="server" />
                </td>
        </tr>
        <tr>
            <td class="style2" >GRN No:</td>
            <td class="Input">
                <asp:Label ID="lblGRN_Number" runat="server" Text=""></asp:Label>
    </td>
    </tr>
  
<tr class="PreviewEditorCaption">
    <td colspan="4">
Commodity Receiving Information</td>
</tr>

    <tr >
        <td class="style2">Tracking No:</td>
        <td><asp:Label ID="lblTrackingNo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="style1">Client : </td>
        <td class="style5"><asp:Label ID="lblClinet" runat="server" Width="250px"></asp:Label></td>
    </tr>
    <tr>
        <td class="style2">Production Year:</td>
        <td><asp:Label ID="lblProductionYear" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="style2">Date Recived:</td>
        <td colspan="3" class="style4"><asp:Label ID="lblDateRecived" runat="server"  Width="250px" Text=""></asp:Label></td>
    </tr>

<tr class="PreviewEditorCaption">
    <td colspan="4">
Sampling Information</td>
</tr>

    <tr >
        <td class="style2">Sample Ticket:</td>
        <td><asp:Label ID="lblSampleTicket" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfSamplingId" runat="server" />
        </td>
        </tr>
        <tr>
        <td class="style2">Date Sampled: </td>
        <td>
            <asp:Label ID="lblSampledDate" runat="server"></asp:Label>
        </td>
        </tr>
       

<tr class="PreviewEditorCaption">
    <td colspan="4">
Grading Information</td>
</tr>

    <tr >
        <td class="style2">Code:</td>
        <td><asp:Label ID="lblCode" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfGradingId" runat="server" />
        </td>
        </tr>
        <tr>
        <td class="style2">Commodity Grade: </td>
        <td>
            <asp:Label ID="lblCommodityGrade" runat="server"></asp:Label>
            <asp:HiddenField ID="hfCommodityGradeId" runat="server" />
        </td>
    </tr>
 <tr class="PreviewEditorCaption">
    <td colspan="4">
Unloading Information</td>
</tr>

    <tr >
        <td class="style2">Date Deposited:</td>
        <td><asp:Label ID="lblDateDeposited" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfUnloadingId" runat="server" />
            <asp:HiddenField ID="hfWarehouseId" runat="server" />
        </td>
        </tr>
        <tr>
        <td class="style2">Total No. Bags: </td>
        <td>
            <asp:Label ID="lblBags" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>Bag Type: </td>
        <td class="style5">
            <asp:Label ID="lblBagType" runat="server"></asp:Label>
            <asp:HiddenField ID="hfBagTypeId" runat="server" />
        </td>
    </tr>
 <tr class="PreviewEditorCaption">
    <td colspan="4">
Scaling  Information</td>
</tr>

    <tr >
        <td class="style2">Total Gross weight:
        </td>
        <td><asp:Label ID="lblGrossWeight" runat="server"></asp:Label>
            <asp:HiddenField ID="hfScalingId" runat="server" />
        </td>
    </tr>
    <tr >
        <td class="style2">Total Net weight:
        </td>
        <td><asp:Label ID="lblNetWeight" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style2">Original Quantity</td>
        <td><asp:Label ID="lblOriginalQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="style2">Current Quantity</td>
        <td><asp:Label ID="lblCurrentQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="style2">GRN Type</td>
        <td>
            <asp:DropDownList ID="cboGRNType" Width="150px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr class="EditorCommand">
    <td colspan="4" align="left">
        <asp:Button ID="btnAdd" runat="server" Text="Re-create GRN" onclick="btnAdd_Click" /></td>
    </tr>
</table>