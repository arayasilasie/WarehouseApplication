<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIGRNSentbyDate.ascx.cs" Inherits="WarehouseApplication.UserControls.UIGRNSentbyDate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="2">GRN's Sent to CD</td>
</tr>
<tr>
<td>Select Date:</td>
<td>     <asp:TextBox ID="txtArrivalDate" runat="server" 
        ontextchanged="txtArrivalDate_TextChanged" ></asp:TextBox>
        <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="txtArrivalDate">
        </cc1:CalendarExtender>
        
         <asp:Button ID="btnView" runat="server" onclick="btnView_Click" 
        Text="View" />
        
         <asp:RequiredFieldValidator ID="rvArrival" ValidationGroup="Save" ControlToValidate="txtArrivalDate" runat="server" 
         ErrorMessage="*"></asp:RequiredFieldValidator>
        
         <asp:RangeValidator ID="RangeValidator1" ValidationGroup="Save" Type="Date" ControlToValidate="txtArrivalDate" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-10).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator> 
        </asp:RequiredFieldValidator></td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvDetail" runat="server" Width="340px" Visible="true"
            CssClass="Grid" 
            AutoGenerateColumns="false">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
             <asp:TemplateField Visible="true" HeaderText="Warehouse name" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("warehousename") %>'></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField Visible="true" HeaderText="No. GRN Sent" ItemStyle-HorizontalAlign="center">
             <ItemTemplate>
              <asp:Label ID="lblcount"  runat="server" Text='<%# Bind("count") %>'></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
            
        </asp:GridView>
        </td>
        </tr>
        <tr>
        <td>Total : 
            <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label></td>
        </tr>
</table>