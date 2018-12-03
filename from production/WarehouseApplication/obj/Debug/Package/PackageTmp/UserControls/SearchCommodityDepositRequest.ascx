<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchCommodityDepositRequest.ascx.cs" Inherits="WarehouseApplication.UserControls.SearchCommodityDepositRequest" %>
   <%@ Register src="ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

   <table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" >Search Criteria</td>
</tr>
    <tr>
    <td  colspan="4" class="Message"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        
    </tr>

        <tr>
            <td class="Text">Tracking No :</td>
         <td class="Input"><asp:TextBox ID="txtTrackingNumber" runat="server" Width="230px"></asp:TextBox></td>
        </tr>
        <tr class="EditorAlternate">
            <td class="Text">Voucher No :</td>
            <td class="Input">
                <asp:TextBox ID="txtVoucherNo" runat="server"  Width="223px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="Text">Client Name:</td>
            <td class="Input"><uc1:ClientSelector ID="ClientSelector1" runat="server" /></td>
        </tr>
        <tr class="EditorAlternate">
            <td class="Text">Commodity :</td>
            <td class="Input"> <asp:DropDownList ID="cboCommodity" runat="server" Width="230px"></asp:DropDownList></td>
        </tr>
        <tr>
            <td class="Text">Date Received :</td>
            <td class="Input"><asp:TextBox ID="dtFrom" runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="txtArrivalDate_CalendarExtender" runat="server" 
            TargetControlID="dtFrom"> </cc1:CalendarExtender>
        </cc1:CalendarExtender>
            To:
        <asp:TextBox ID="dtTo" runat="server"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
            TargetControlID="dtTo"> </cc1:CalendarExtender>       
        </td>
        </tr>
        <tr  class="EditorCommand">
        <td align="left" colspan="2"> <asp:Button ID="btnSearch" runat="server"  Text="Search" onclick="btnSearch_Click" /> </td>
        </tr>
        </table>
        <table>
        <tr>
        <td align="left" colspan="2">
        <asp:GridView ID="gvDepositeRequetsList" runat="server" Width="897px" 
                      CellPadding="4" ForeColor="#333333" 
                      GridLines="None"  OnRowCommand="gvDepositeRequetsList_Edit" 
                      DataKeyNames="ID" Font-Size="Small" AutoGenerateColumns="False" 
                AllowPaging="True" 
                onpageindexchanged="gvDepositeRequetsList_PageIndexChanged" 
                onpageindexchanging="gvDepositeRequetsList_PageIndexChanging" CssClass="Grid" 
                       >
                      <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                      <RowStyle CssClass="GridRow" />
                      <Columns>
                         
                           <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                           <asp:BoundField DataField="TrackingNo" HeaderText="Tracking No" />
                            <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" />
                           <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                           <asp:BoundField DataField="CommodityName" HeaderText="Commodity" />
                           <asp:BoundField DataField="DateTimeRecived" HeaderText="Date Recived" />
                           
                           
                           <asp:BoundField />
                     <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Visible="False" Text='<%# Bind("Id") %>'></asp:Label>
                            <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="cmdEdit" runat="server" Text="Edit" />
                        </ItemTemplate>       
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdDriver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="cmdDriver" runat="server" Text="Driver Info." />
                        </ItemTemplate>       
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdVoucher" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="cmdVoucher" runat="server" Text="Voucher" />
                        </ItemTemplate>       
                     </asp:TemplateField>
                         
                      </Columns>
                      <PagerStyle ForeColor="White" Height="15px" HorizontalAlign="Center" CssClass="GridPager" />
                      <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
                          CssClass="GridSelectedRow" />
                      <HeaderStyle Font-Bold="True" Height="15px" ForeColor="White" CssClass="GridHeader" />
                      <EditRowStyle CssClass="GridSelectedRow" />
                      <AlternatingRowStyle CssClass="GridAlternate" />
                  </asp:GridView>

        </td>
        </tr>
       
     
     </table>
  
