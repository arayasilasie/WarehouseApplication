<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAbsentTrucks.ascx.cs"  Inherits="WarehouseApplication.UserControls.UIAbsentTrucks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Trucks Missing on Sampling Queue </td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
<td colspan="2">

<asp:GridView ID="gvDetail" runat="server" Width="650px" 
             CssClass="Grid" 
            AutoGenerateColumns="False" onrowcommand="gvDetail_RowCommand">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
            
             <asp:TemplateField Visible="False" HeaderText="" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblId"  runat="server" Text='<%# Bind("Id") %>'></asp:Label>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Tracking No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Plate No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblPlateNo"  runat="server" Text='<%# Bind("PlateNo") %>'></asp:Label>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Trailer Plate No." ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrailerPlateNo"  runat="server" Text='<%# Bind("TrailerPlateNo") %>'></asp:Label>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
               <asp:TemplateField Visible="True" HeaderText="Status" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
                   <asp:DropDownList ID="cboRemark" runat="server">
                    <asp:ListItem  Selected="True" Value="1">New</asp:ListItem>
                    <asp:ListItem Value="2">Queue for sampling</asp:ListItem>
                    <asp:ListItem Value="3">Cancel</asp:ListItem>
                   </asp:DropDownList>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              <asp:TemplateField Visible="True" HeaderText="Status" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
                    <asp:LinkButton ID="cmdEdit" CommandName="UpdateStatus" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Update</asp:LinkButton>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
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
    &nbsp;</td>
</tr>
</table>
