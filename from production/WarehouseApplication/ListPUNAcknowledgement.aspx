<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListPUNAcknowledgement.aspx.cs" Inherits="WarehouseApplication.ListPUNAcknowledgement" Title="Untitled Page" %>
<%@ Register src="UserControls/SearchConditionSelector.ascx" tagname="SearchConditionSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table style="width:850px">
    <tr>
        <td class="Input" colspan="2">
            <fieldset>
                <legend class="Text">Select Acknowledged Pickup Notice</legend>
                <uc1:SearchConditionSelector ID="SearchConditionSelector1" runat="server" />
            </fieldset>
        
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </td>
    </tr>
    <tr>
        <td class="Text" colspan="2"><asp:Label ID="lblPickupNotice" runat="server" Text="Pickup Notices" /></td>
    </tr>
    <tr>
        <td align="left" class="Input" colspan="2">
        <asp:GridView ID="gvPickupNotice" runat="server" Width="100%" 
          CellPadding="4" CssClass="Grid"  
          DataKeyNames="Id" Font-Size="Small" AutoGenerateColumns="False" 
                DataSourceID="xdsGINProcessSource" EnableViewState="False">
          <FooterStyle CssClass="GridHeader"/>
          <HeaderStyle CssClass="GridHeader" />
          <PagerStyle CssClass="GridPager" />
          <SelectedRowStyle CssClass="GridSelectedRow"/>
          <EditRowStyle CssClass="GridRow"/>
          <AlternatingRowStyle CssClass="GridAlternate"/>
          <RowStyle CssClass="GridRow"/>
          <Columns>
               <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
               <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnLoadTruck" runat="server" CommandName="RegisterTruck" 
                            CommandArgument='<%# XPath("@Id") %>' 
                            Visible='<%# Navigable(XPath("@Status"), "Load", XPath("@BalanceWeight")) %>' 
                            Text="Register Truck" oncommand="btnOpen_Command" />
                    </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Status">
                   <ItemTemplate>
                       <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatusName(XPath("@Status")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Commodity Grade">
                   <ItemTemplate>
                       <asp:Label ID="lblCommodityGrade" runat="server" 
                           Text='<%# GetCommodityGradeName(XPath("@CommodityGradeId")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="AgentName" HeaderText="Pickup Agent" />
               <asp:BoundField DataField="BalanceQuantity" HeaderText="Remaining Quantity" />
               <asp:BoundField DataField="BalanceWeight" HeaderText="Remaining Weight" />
               <asp:BoundField DataField="DateReceived" HeaderText="Date Received" 
                   DataFormatString="{0:d}" />
          </Columns>
        </asp:GridView>
        <asp:XmlDataSource ID="xdsGINProcessSource" runat="server" 
                XPath="/Catalog/GINProcess" EnableViewState="False" EnableCaching="False" />
       </td>
    </tr>
</table>
</asp:Content>
