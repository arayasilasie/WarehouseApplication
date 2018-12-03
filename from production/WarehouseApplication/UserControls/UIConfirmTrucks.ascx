<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIConfirmTrucks.ascx.cs" Inherits="WarehouseApplication.UserControls.UIConfirmTrucks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="UIAbsentTrucks.ascx" tagname="UIAbsentTrucks" tagprefix="uc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Confirm Trucks Queued for Sampling</td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
    
<td colspan="2">
 
    </td>
</tr>
<tr>
<td colspan="2">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <contenttemplate>
<asp:GridView ID="gvDetail" runat="server" Width="850px" 
             CssClass="Grid" 
            AutoGenerateColumns="False" onrowdatabound="gvDetail_RowDataBound">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
             <asp:TemplateField HeaderText="" Visible="true" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate >
                 <asp:CheckBox ID="chkConfirmed" Visible="true"  Checked="true" runat="server" />
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
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
                    <asp:ListItem Value="1"  Selected="True">Ready For Sampling</asp:ListItem>
                    <asp:ListItem Value="2" >Truck Missing on sampling Spot</asp:ListItem>
                    <asp:ListItem Value="3">Other</asp:ListItem>
                   </asp:DropDownList>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="Remark" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
                 <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server"></asp:TextBox>
                 <asp:Label ID="lblerr" ForeColor="Red" Visible="false" runat="server" Text="*"></asp:Label><asp:HiddenField ID="hfmsg" runat="server" />
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             
            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
            
        </asp:GridView>
        </contenttemplate>
       </asp:UpdatePanel>
    </td>
</tr>
<tr>
    
<td colspan="2">
<asp:Button ID="btnConform" Visible="True" runat="server" Text="Confirm" 
        onclick="btnConform_Click" style="height: 26px" />
</td>
</tr>
<tr>
<td>
    
    <uc1:UIAbsentTrucks ID="UIAbsentTrucks1" runat="server" />
    
    </td>
</tr>
<tr class="EditorCommand">
<td align="left" colspan="2">
    <asp:Button ID="btnNext" CssClass="Forbtn" runat="server" Text="Next" 
        onclick="btnNext_Click" /></td>
</tr>
</table>
