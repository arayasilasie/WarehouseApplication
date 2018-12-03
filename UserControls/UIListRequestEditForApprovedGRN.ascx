<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIListRequestEditForApprovedGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UIListRequestEditForApprovedGRN" %>
 
 <table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" >Search Criteria</td>
</tr>
<tr class="Message">
<td colspan="4" >
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></td>
</tr>
     <tr  class="EditorAlternate">
     <td class="Text" >Tracking No. :</td>
     <td class="Input"><asp:TextBox ID="txtTrackingNo" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
     <tr>
     <td class="Text">GRN No. :&nbsp;
                 </td>
     <td class="Input"></asp:TextBox>
         <asp:TextBox ID="txtGRN" runat="server" Width="100px"></asp:TextBox>
                 </td>
     </tr>
     <tr  class="EditorAlternate">
     <td class="Text" >Date Requested :</td>
     
     
    
     <td>From :<asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>&nbsp;To : <asp:TextBox ID="txtTo" runat="server"></asp:TextBox></td>

     </tr>
     <tr>
        <td>Status :</td>
        <td><asp:DropDownList ID="cboStatus" runat="server">
            <asp:ListItem>Please Select Status</asp:ListItem>
            <asp:ListItem Value="1">New</asp:ListItem>
            <asp:ListItem Value="2">Approved</asp:ListItem>
            <asp:ListItem Value="3">Cancelled</asp:ListItem>
         </asp:DropDownList></td>
         
     </tr>
     <tr>
     <td colspan="4" align="left">
         <asp:Button ID="btnSearch" runat="server" 
             Text="Search" Width="94px" onclick="btnSearch_Click1" /></td>
     </tr>
      <tr>
     <td colspan="2" >
         <asp:GridView ID="gvGRNEditRequest" runat="server" Width="876px" 
             AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
             GridLines="None"
             
             AllowPaging="True" BorderColor="Black" BorderStyle="Solid" 
             BorderWidth="1px" onrowcommand="gvGRNEditRequest_RowCommand" >
         <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <RowStyle BackColor="#DAE1CC" />
         <Columns>
                  <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking No." >
                    <ItemTemplate>
                        <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GRN No.">
                    <ItemTemplate>
                        <asp:Label ID="lblGRN" runat="server" Text='<%# Bind("GRN_Number") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
     
                <asp:TemplateField HeaderText="Date Requested">
                    <ItemTemplate>
                        <asp:Label ID="lblDateRequested" runat="server" Text='<%# Bind("DateRequested") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdEdit" CommandName="Edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Edit</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
         </Columns>
         <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" 
             BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" 
             Font-Names="Verdana" Font-Size="0.8em" />
         <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
         <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" 
             Font-Names="Verdana" Font-Size="0.8em" />
         <EditRowStyle BackColor="#7C6F57" />
         <AlternatingRowStyle BackColor="White" />
         </asp:GridView></td>
     </tr>
</table>