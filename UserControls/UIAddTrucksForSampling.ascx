<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddTrucksForSampling.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddTrucksForSampling" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Get Trucks for Sampling</td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
<td class="Text" style="width:150px"> No. of Truck for Sampling: </td>
<td class="Input" align="left">
    <asp:DropDownList ID="cboNoTrucks" runat="server">
        <asp:ListItem Value="">Please Select No. Trucks</asp:ListItem>
        <asp:ListItem Value="1">1</asp:ListItem>
        <asp:ListItem Value="2">2</asp:ListItem>
        <asp:ListItem Value="3">3</asp:ListItem>
        <asp:ListItem Value="4">4</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfVSelectTrucks" ValidationGroup="Generate" runat="server" ControlToValidate="cboNoTrucks"
        ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr class="EditorCommand">
    
<td colspan="2" align="left" ><asp:Button ID="btnGenerate" CausesValidation="true" CssClass="Forbtn" ValidationGroup="Generate" runat="server" Text="Get Trucks" 
        onclick="btnGenerate_Click" /></td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvDetail" runat="server" Width="650px" 
             CssClass="Grid" 
            AutoGenerateColumns="False">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
            
             <asp:TemplateField Visible="True" HeaderText="Tracking No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Plate No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("PlateNo") %>'></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Trailer Plate No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("TrailerPlateNo") %>'></asp:Label>
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
    
<td colspan="2">
<asp:Button ID="btnPrint" Visible="false" runat="server" Text="Print List " 
        Enabled="False" />
</td>
</tr>
</table>