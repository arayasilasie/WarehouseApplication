<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddGRN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:800px" >
<tr class="PreviewEditorCaption">
    <td colspan="4" style="widows:250px">Add GRN</td>
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
                </td>
        </tr>
        <tr>
            <td class="style2" >Tracking No. </td>
            <td class="Input">
            <asp:Panel runat="server" Enabled="false" ID="pnl">
                <asp:DropDownList ID="cboGradingCode" runat="server" Width="150px" 
            ValidationGroup="Unloading" 
            AutoPostBack="True" onselectedindexchanged="cboGradingCode_SelectedIndexChanged">
       </asp:DropDownList>
        <asp:RequiredFieldValidator ValidationGroup="GRN" ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="cboGradingCode" ErrorMessage="Please enter Grading Code">*</asp:RequiredFieldValidator>
    </asp:Panel>
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
    <tr>
        <td class="style2">Production Year:</td>
        <td><asp:Label ID="lblProductionYear" runat="server" Text=""></asp:Label></td>
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
        <td>Date Scaled : 
            </td>
        <td><asp:TextBox ID="lblDateScaled" Enabled="false" runat="server"></asp:TextBox></td>
    
    </tr>
    
    <tr>
        <td class="style2">GRN Type</td>
        <td>
            <asp:DropDownList ID="cboGRNType" Width="250px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="GRN" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="PreviewEditorCaption">
<td colspan="2">
GRN service
</td>
</tr>
<tr>
<td>Service : </td>
    
<td><asp:DropDownList ID="cboGRNService" Width="250px" runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvGRNService" ValidationGroup="GRNService" ControlToValidate="cboGRNService" runat="server" 
        ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td>Total Number : (per unit)</td>
<td>
    <asp:TextBox ID="txtTotalNumberPerUnit" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ErrorMessage="*" ValidationGroup="GRNService" ControlToValidate="txtTotalNumberPerUnit" ></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ControlToValidate="txtTotalNumberPerUnit" CssClass="Input" 
            ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
            ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left">


    <asp:Button ID="btnGRNService" runat="server" ValidationGroup="GRNService" CausesValidation="true" Text="Add GRN Service" 
        onclick="btnGRNService_Click" />


</td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvGRNService" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" 
        GridLines="None" 
         onrowdeleting="gvgvGRNService_RowDeleting" CssClass="Grid" Width="700px" 
        AllowPaging="True" PageSize="5" 
        EmptyDataText="No GRN Services" 
         >
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <RowStyle CssClass="GridRow" />
        <Columns>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderText="Service Name" 
                DataField="ServiceName" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Quantity" 
                DataField="Quantity" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField  Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField Visible="false"  ItemStyle-HorizontalAlign="Center" HeaderText="Quantity" 
                DataField="ServiceId" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
           
            <asp:ButtonField  CommandName="Delete"  Text="Remove"  />
            
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
<tr>
<td colspan="4" align="left"></td>
</tr>
    <tr>
        <td>Date GRN Created:</td>
        <td>
            <asp:TextBox ID="txtDateRecived" runat="server" ></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateRecived"
                        ErrorMessage="*" ValidationGroup="GRN"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1"  Type="Date" 
                ControlToValidate="txtDateRecived" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable" 
                ValidationGroup="GRN"></asp:RangeValidator>
           
        </td>
    </tr>
    <tr class="EditorCommand">
    <td colspan="4" align="left">
        <asp:Button ID="btnAdd" runat="server" ValidationGroup="GRN" Text="Add GRN" onclick="btnAdd_Click" CssClass="Forbtn" />
        &nbsp;&nbsp;
        <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" CssClass="Forbtn" runat="server" 
            onclick="btnCancel_Click"  />
        </td>
    </tr>
</table>

