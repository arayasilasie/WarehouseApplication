<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchUnloadingInformation.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchUnloadingInformation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="SearchControl.ascx" tagname="SearchControl" tagprefix="uc1" %>
<table style="width:800px">
<tr>
<td>
    <uc1:SearchControl ID="ucSearchControl" runat="server" />
    </td>
    </tr>
    <tr>
<td valign="bottom">
    <asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" />
    </td>
    
</tr>
<tr>
<td class="Message"> <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
<td colspan="2">
    <asp:GridView ID="gvUnloading" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
        EmptyDataText="No Matching  Records Found." 
        onpageindexchanged="gvUnloading_PageIndexChanged" 
        onpageindexchanging="gvUnloading_PageIndexChanging" CssClass="Grid" 
        onrowdatabound="gvUnloading_RowDataBound" Width="800px" 
        onrowcommand="gvUnloading_RowCommand">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
            CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
        <RowStyle CssClass="GridRow" />
        <Columns>
         <asp:TemplateField  Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("GradingCode") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tracking No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Commodity Grade" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityGrade" runat="server" ></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tot. No Bags" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalNoBags" runat="server" Text='<%# Bind("TotalNumberOfBags") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Deposited" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDateDeposited" runat="server" Text='<%# Bind("DateDeposited") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="cmdRowEdit" runat="server">Edit</asp:LinkButton>
                        </ItemTemplate>
                </asp:TemplateField>
<%--                                   <asp:HyperLinkField DataNavigateUrlFields="ID"

 DataNavigateUrlFormatString="~/EditUnloadingInformation.aspx?id={0}" Text="Edit"
                     NavigateUrl="~/EditGradingResult.aspx" />--%>
                
      
    </Columns>
        <EditRowStyle CssClass="GridAlternate" />
        <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>

</td>
</tr>
</table>