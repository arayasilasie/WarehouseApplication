<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchGRN.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchGRN" %>
<%@ Register src="ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>

   <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

   <table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" >Search Criteria</td>
</tr>
<tr>
<td class="Message" colspan="4">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
</tr>
     <tr>
        <td class="Text" >Client ID :</td>
        <td colspan="4">
            <uc1:ClientSelector ID="ClientSelector1" runat="server" />
            </td>
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
     <td class="Text" >Date Deposited :</td>
     
     
    
     <td>From :<asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
             Enabled="True" TargetControlID="txtFrom">
         </cc1:CalendarExtender>
         &nbsp;To : <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" 
             TargetControlID="txtTo">
         </cc1:CalendarExtender>
         </td>

     </tr>
     <tr>
        <td>Status :</td>
        <td><asp:DropDownList ID="cboStatus" runat="server">
            <asp:ListItem>Please Select Status</asp:ListItem>
            <asp:ListItem Value="1">New</asp:ListItem>
            <asp:ListItem Value="2">Active</asp:ListItem>
            <asp:ListItem Value="3">Cancelled</asp:ListItem>
            <asp:ListItem Value="4">Client Accepted</asp:ListItem>
            <asp:ListItem Value="5">Client Rejected</asp:ListItem>
            <asp:ListItem Value="6">Manager Approved</asp:ListItem>
            <asp:ListItem Value="7">Open For Edit</asp:ListItem>
         </asp:DropDownList></td>
         
     </tr>
     <tr>
     <td colspan="4" align="left">
         <asp:Button ID="btnSearch" runat="server" 
             Text="Search" Width="94px" onclick="btnSearch_Click" /></td>
     </tr>
    
     <tr>
     <td colspan="2" >
         <asp:GridView ID="gvGRN" runat="server" Width="850px" 
             AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
             GridLines="None" onrowediting="gvGRN_RowEditing" 
             onrowupdating="gvGRN_RowUpdating" onrowcommand="gvGRN_RowCommand" 
             AllowPaging="True" BorderColor="Black" BorderStyle="Solid" 
             BorderWidth="1px" CssClass="Grid" 
             onpageindexchanged="gvGRN_PageIndexChanged" 
             onpageindexchanging="gvGRN_PageIndexChanging" >
         <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <RowStyle CssClass="GridRow" />
         <Columns>
                  <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Center" HeaderText="GRN No.">
                    <ItemTemplate>
                        <asp:Label ID="lblGRN" runat="server" Text='<%# Bind("GRN") %>'></asp:Label>
                    </ItemTemplate>       

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Commodity Grade">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityGrade" runat="server" Text='<%# Bind("CommodityGrade") %>'></asp:Label>
                    </ItemTemplate>       

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField  ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Original Qty.">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginalQuantity" runat="server" Text='<%# Bind("OriginalQuantity") %>'></asp:Label>
                    </ItemTemplate>       

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Deposited">
                    <ItemTemplate>
                        <asp:Label ID="lblDateDeposited" runat="server" Text='<%# Bind("DateDeposited") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Status" >
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdEdit" CommandName="Edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Edit</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdView" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">View</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdPrint" CommandName="cmdPrint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Print</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdCancel" CommandName="cmdCancel" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Cancel</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
     <%--           <asp:TemplateField HeaderText="" >
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdUpdateGRNNo" CommandName="cmdGRNNoUpdate" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Update GRN No.</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>--%>
         </Columns>
         <PagerStyle ForeColor="White" HorizontalAlign="Center" BorderStyle="Inset" Font-Bold="True" 
             Font-Names="Verdana" Font-Size="0.8em" CssClass="GridPager" />
         <SelectedRowStyle Font-Bold="True" ForeColor="#333333" CssClass="GridSelectedRow" />
         <HeaderStyle Font-Bold="True" ForeColor="White" 
             Font-Names="Verdana" Font-Size="0.8em" CssClass="GridHeader" />
             <EditRowStyle CssClass="GridSelectedRow" />
         <AlternatingRowStyle CssClass="GridAlternate" />
         </asp:GridView></td>
     </tr>
         
     </table>