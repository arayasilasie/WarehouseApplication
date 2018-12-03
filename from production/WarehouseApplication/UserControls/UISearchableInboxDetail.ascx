<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchableInboxDetail.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchableInboxDetail" %>
   <style type="text/css">
   .align
   {
   	text-align:left;
   }
   </style>
   
   <fieldset style="width:370px">
   <legend class="Text" style="color:#424D2D; font-size:medium"  >
       <asp:Label ID="lblInboxItemName" runat="server" Text="Label"></asp:Label>
   </legend>
 <table>
    <tr>
    <td><asp:Label class="Message" runat="server" id="msg" ></asp:Label></td>
    </tr>
    <tr>
        <td>
            Task No.: &nbsp<asp:TextBox ID="txtTaskNo" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </td>
    </tr>
    <tr>
    <td>
        <asp:GridView ID="gvDetail" runat="server" Width="340px" 
            onrowcommand="gvInbox_RowCommand" CssClass="Grid" 
            AutoGenerateColumns="False" AllowPaging="True" 
            onpageindexchanging="gvDetail_PageIndexChanging" 
            EmptyDataText="No Item Found">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
             <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate>
              <asp:Label ID="lblTrackingNo"  runat="server" Text='<%# Bind("TrackNo") %>'></asp:Label>
             </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
                <asp:BoundField DataField="TrackNo" Visible="False" />
                <asp:BoundField HeaderStyle-CssClass="align" ItemStyle-CssClass="align" 
                    DataField="DisplayName" HeaderText="Task No." >
<HeaderStyle CssClass="align"></HeaderStyle>

<ItemStyle CssClass="align"></ItemStyle>
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail" Text="Detail" />
            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
            
        </asp:GridView>
    </td>
    </tr>
       
 </table>
    </fieldset>