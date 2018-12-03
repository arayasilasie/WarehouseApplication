<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditGRN" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
    </style>
    <table class="PreviewEditor" style="width:800px" >
    <tr class="PreviewEditorCaption">
    <td colspan="2">Warehouse Manger GRN Approval </td>
    </tr>
    <tr>
    <td colspan="2"><asp:Label ID="lblmsg" runat="server" CssClass="Message"></asp:Label></td>
    </tr>
        <tr>
            <td class="Message" colspan="2">
                <asp:HiddenField ID="hfReceivigRequestId" runat="server" />
                <asp:HiddenField ID="hfVoucherId" runat="server" />
                <asp:HiddenField ID="hfGRNId" runat="server" />
                </td>
        </tr>
        <tr>
            <td class="Text" style="width:200px" >GRN No. :</td>
            <td class="Input">
                <asp:Label ID="lblGRN" runat="server"></asp:Label>
    </td>
    </tr>
    <tr class="PreviewEditorCaption">
    <td colspan="2">Arrival Information</td>
    </tr>
        <tr  >
        <td >Current Tracking No:</td>
        <td><asp:Label ID="lblCurrentTrackingNo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="EditorAlternate" >
        <td >Original Tracking No:</td>
        <td><asp:Label ID="lblTrackingNo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td >Client : </td>
        <td><asp:Label ID="lblClinet" runat="server" Width="250px"></asp:Label></td>
    </tr>
    <tr>
        <td class="Text">Date Recived:</td>
        <td><asp:Label ID="lblDateRecived" runat="server"  Width="250px" Text=""></asp:Label></td>
    </tr>
    <tr class="PreviewEditorCaption">
    <td colspan="2">
Sampling Information
</td></tr>
    <tr >
        <td>Sample Ticket:</td>
        <td><asp:Label ID="lblSampleTicket" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfSamplingId" runat="server" />
        </td>
        </tr>
        <tr>
        <td>Date Sampled: </td>
        <td>
            <asp:Label ID="lblSampledDate" runat="server"></asp:Label>
        </td>
        </tr>
<tr class="PreviewEditorCaption">
<td colspan="2">
Grading Information
</td>
</tr>
    <tr >
        <td>Code:</td>
        <td><asp:Label ID="lblCode" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfGradingId" runat="server" />
        </td>
        </tr>
        <tr class="EditorAlternate">
        <td>Commodity Grade: </td>
        <td>
            <asp:Label ID="lblCommodityGrade" runat="server"></asp:Label>
            <asp:HiddenField ID="hfCommodityGradeId" runat="server" />
        </td>
    </tr>
    <tr class="EditorAlternate">
        <td >Production Year:</td>
        <td><asp:Label ID="lblProductionYear" runat="server" Text=""></asp:Label></td>
    </tr>
<tr class="PreviewEditorCaption" >
<td colspan="2">Unloading Information</td>
</tr>
    <tr >
        <td>Date Deposited:</td>
        <td><asp:Label ID="lblDateDeposited" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="hfUnloadingId" runat="server" />
            <asp:HiddenField ID="hfWarehouseId" runat="server" />
        </td>
        </tr>
        <tr class="EditorAlternate">
        <td>Total No. Bags: </td>
        <td>
            <asp:Label ID="lblBags" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>Bag Type: </td>
        <td>
            <asp:Label ID="lblBagType" runat="server"></asp:Label>
            <asp:HiddenField ID="hfBagTypeId" runat="server" />
        </td>
    </tr>
    <tr class="PreviewEditorCaption">
    <td colspan="2">
Scaling Information
</td>
</tr>
    <tr >
        <td>Total Gross weight:
        </td>
        <td><asp:Label ID="lblGrossWeight" runat="server"></asp:Label>
            <asp:HiddenField ID="hfScalingId" runat="server" />
        </td>
    </tr>
    <tr class="EditorAlternate">
        <td>Total Net weight:
        </td>
        <td><asp:Label ID="lblNetWeight" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Original Quantity</td>
        <td><asp:Label ID="lblOriginalQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr class="EditorAlternate">
        <td>Current Quantity</td>
        <td><asp:Label ID="lblCurrentQuantity" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>GRN Type</td>
        <td>
            <asp:DropDownList ID="cboGRNType" Width="150px" runat="server" Enabled="False">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr class="EditorAlternate">
    <td class="style1">Client Accepted</td>
    <td class="style1"><asp:CheckBox ID="chkClientAccepted" runat="server" Enabled="False" /></td>
    </tr>
    <tr>
    <td>Client Accepted <span lang="en-us">&nbsp;Date</span></td>
    <td><asp:TextBox ID="txtClientAcceptedTimeStamp" runat="server" Enabled="False"></asp:TextBox>
        <cc1:CalendarExtender ID="txtClientAcceptedTimeStamp_CalendarExtender" 
            runat="server"  TargetControlID="txtClientAcceptedTimeStamp">
        </cc1:CalendarExtender>
        </td>
        
    </tr>
    <tr class="EditorAlternate">
        <td>Manager Approval Date :</td>
        <td>
        <asp:TextBox ID="txtMADate" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="txtMADate">
        </cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="txtMADate" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtMADate" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
         <asp:CompareValidator ControlToValidate="txtMADate"  
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Manger Approved Date can't be less than client accepted Date" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual" 
            ControlToCompare="txtClientAcceptedTimeStamp"></asp:CompareValidator>
        
        
        </td>
    </tr>
    <tr>
        <td>Manager Approval Time :</td>
      <td>
                    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTime_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTime" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
            runat="server" ControlToValidate="txtTime" ErrorMessage="*"></asp:RequiredFieldValidator>
</td>
    </tr>
    
    
    
        <tr class="EditorAlternate">
    <td>GRN Status</td>
        
        <td><asp:HiddenField ID="hfStatus" runat="server" />
            <asp:DropDownList ID="cboStatus" Width="150px" runat="server" 
                onselectedindexchanged="cboStatus_SelectedIndexChanged">
                <asp:ListItem Value="1">New</asp:ListItem>
                <asp:ListItem Value="2">Active</asp:ListItem>
                <asp:ListItem Value="3">Cancelled</asp:ListItem>
                <asp:ListItem Value="4">Client Accepted</asp:ListItem>
                <asp:ListItem Value="5">Client Rejected</asp:ListItem>
                <asp:ListItem Value="6">Manager Approved</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="cboGRNType" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    
    <asp:Panel runat="server" ID="pnl" >
    <tr class="PreviewEditorCaption">
<td colspan="2">
GRN service
</td>
</tr>
<tr>
<td>Service : </td>
    
<td><asp:DropDownList ID="cboGRNService" Width="180px" runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvGRNService" runat="server" 
        ControlToValidate="cboGRNService" ErrorMessage="Please Select Service" 
        ValidationGroup="GRNService"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td>Total Number : (per unit)</td>
<td>
    <asp:TextBox ID="txtTotalNumberPerUnit" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTotalNo" runat="server" 
        ControlToValidate="txtTotalNumberPerUnit" ErrorMessage="Please Enter Number" 
        ValidationGroup="GRNService"></asp:RequiredFieldValidator>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left">


    <asp:Button ID="btnGRNService" runat="server" Text="Add GRN Service" 
        onclick="btnGRNService_Click" ValidationGroup="GRNService" />


    


</td>

</tr>
  <tr>
  <td colspan="2">
  
  
      <asp:GridView ID="gvGRNService" runat="server" AllowPaging="True" 
          AutoGenerateColumns="False" CellPadding="4" CssClass="Grid" 
          EmptyDataText="No GRN Services" ForeColor="#333333" GridLines="None" 
           PageSize="5" Width="700px" onrowdeleting="gvGRNService_RowDeleting" 
          onpageindexchanged="gvGRNService_PageIndexChanged" 
          onpageindexchanging="gvGRNService_PageIndexChanging">
          <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
          <RowStyle CssClass="GridRow" />
          <Columns>
              <asp:TemplateField Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="ServiceName" HeaderStyle-HorizontalAlign="Left" 
                  HeaderText="Service Name">
              <HeaderStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
                  ItemStyle-HorizontalAlign="Center">
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
              <asp:TemplateField Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="lblServiceId" runat="server" Text='<%# Bind("ServiceId") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="lblServiceName" runat="server" Text='<%# Bind("ServiceName") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField Visible="true" HeaderText="Status">
                  <ItemTemplate>
                      <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="ServiceId" HeaderText="Quantity" 
                  ItemStyle-HorizontalAlign="Center" Visible="false">
              <ItemStyle HorizontalAlign="Center" />
              </asp:BoundField>
              <asp:ButtonField CommandName="Delete" Text="Cancel" />
          </Columns>
          <PagerStyle CssClass="GridPager" ForeColor="White" HorizontalAlign="Center" />
          <SelectedRowStyle CssClass="GridSelectedRow" Font-Bold="True" 
              ForeColor="#333333" />
          <HeaderStyle CssClass="GridHeader" Font-Bold="True" ForeColor="White" />
          <EditRowStyle BackColor="#7C6F57" />
          <AlternatingRowStyle CssClass="GridAlternate" />
      </asp:GridView>
  
  
  </td>
  </tr> 
  </asp:Panel> 
    <tr class="EditorCommand">
    <td colspan="2" align="left" >
        <asp:Button ID="btnAdd" runat="server" Text="Update" onclick="btnAdd_Click" CssClass="Forbtn" 
            style="height: 26px" /></td>
    </tr>
</table>
<script type="text/javascript" >
    function ValidateGRNService(Source, args) {
        var txtService = document.getElementById('<%= cboGRNService.ClientID %>');
        alert(txtService.value);
        if (txtService.value != "")
            args.IsValid = false;
        else
            args.IsValid = true; 
        
    }
</script>