<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListPickupNotice.aspx.cs" Inherits="WarehouseApplication.ListPickupNotice" Title="Untitled Page" %>
<%@ Register src="UserControls/SearchConditionSelector.ascx" tagname="SearchConditionSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="850px">
    <tr>
        <td class="Input" colspan="2">
            <fieldset>
                <legend class="Text">Search Pickup Notice</legend>
                <uc1:SearchConditionSelector ID="SearchConditionSelector1" runat="server" />
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" 
                onclick="btnRefresh_Click" />
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
                DataSourceID="xdsPickupNoticeSource" 
                onselectedindexchanged="gvPickupNotice_SelectedIndexChanged" 
                AutoGenerateSelectButton="True" EnableViewState="False">
          <FooterStyle CssClass="GridHeader"/>
          <HeaderStyle CssClass="GridHeader" />
          <PagerStyle CssClass="GridPager" />
          <SelectedRowStyle CssClass="GridSelectedRow"/>
          <EditRowStyle CssClass="GridRow"/>
          <AlternatingRowStyle CssClass="GridAlternate"/>
          <RowStyle CssClass="GridRow"/>
          <Columns>
               <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
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
               <asp:TemplateField HeaderText="Client Name">
                   <ItemTemplate>
                       <asp:Label ID="lblClientName" runat="server" 
                           Text='<%# GetClientName(XPath("@ClientId")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:F4}" />
               <asp:BoundField DataField="Weight" HeaderText="Weight" DataFormatString="{0:F4}"/>
               <asp:BoundField DataField="ExpirationDate" HeaderText="Expiration Date" 
                   DataFormatString="{0:d}" />
               <asp:BoundField DataField="ExpectedPickupDate" 
                   HeaderText="Expected Pickup Date" DataFormatString="{0:d}" />
          </Columns>
        </asp:GridView>
        <asp:XmlDataSource ID="xdsPickupNoticeSource" runat="server" 
                XPath="/Catalog/PickupNotice" EnableCaching="False" 
                EnableViewState="False" />
       </td>
    </tr>
    <tr>
        <td class="Input" colspan="2"><asp:button ID="btnOpen" runat="server" Text="Open" 
                Enabled="false" onclick="btnOpen_Click"/>
            <asp:button ID="btnPrint" 
                runat="server" Text="Print Tracking" 
                Enabled="false" onclick="btnPrint_Click" Width="100px"/>
            <asp:Button ID="btnPrintPUN" runat="server" onclick="btnPrintPUN_Click" 
                Text="Print PUN" Enabled="False" />
        </td>
        <td class="Input" colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="Text"style="width:25%"><asp:Label ID="lblAgents" runat="server" Text="Agents" Visible="false" /></td>
        <td class="Text"style="width:25%"><asp:Label ID="lblWarehouseReceipts" runat="server" Text="Warehouse Receipts" Visible="false" /></td>
    </tr>
    <tr align="top">
        <td style="width:35%" class="Input" align="left">
            <asp:GridView ID="gvAgents" runat="server" Width="100%" 
              CellPadding="4" CssClass="Grid"
              DataKeyNames="Id" Font-Size="Small" AutoGenerateColumns="False" 
                DataSourceID="xdsAgentSource" Visible="False" >
          <FooterStyle CssClass="GridHeader"/>
          <HeaderStyle CssClass="GridHeader" />
          <PagerStyle CssClass="GridPager" />
          <SelectedRowStyle CssClass="GridSelectedRow"/>
          <EditRowStyle CssClass="GridRow"/>
          <AlternatingRowStyle CssClass="GridAlternate"/>
          <RowStyle CssClass="GridRow"/>
              <Columns>
                   <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                   <asp:TemplateField HeaderText="NID Type">
                       <ItemTemplate>
                           <asp:Label ID="lblNIDType" runat="server" 
                               Text='<%# GetNIDType(XPath("@NIDType")) %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="NIDNumber" HeaderText="ID Number" />
                   <asp:BoundField DataField="AgentName" HeaderText="Agent Name" />
              </Columns>
            </asp:GridView>
            <asp:XmlDataSource ID="xdsAgentSource" runat="server" EnableCaching="False" 
                EnableViewState="False" />
        </td>
        <td style="width:65%" class="Input" align="left">
            <asp:GridView ID="gvWarehouseReceipts" runat="server" Width="100%" 
              CellPadding="4" CssClass="Grid" 
              DataKeyNames="Id" Font-Size="Small" AutoGenerateColumns="False" 
                DataSourceID="xdsWarehouseReceiptSource" Visible="False" >
              <FooterStyle CssClass="GridHeader"/>
              <HeaderStyle CssClass="GridHeader" />
              <PagerStyle CssClass="GridPager" />
              <SelectedRowStyle CssClass="GridSelectedRow"/>
              <EditRowStyle CssClass="GridRow"/>
              <AlternatingRowStyle CssClass="GridAlternate"/>
              <RowStyle CssClass="GridRow"/>
              <Columns>
                   <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
                   <asp:BoundField DataField="WarehouseReceiptId" HeaderText="Warehouse Receipt" />
                   <asp:BoundField DataField="GRNNo" HeaderText="GRN No" />
                   <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                   <asp:BoundField DataField="Weight" HeaderText="Weight" />
              </Columns>
            </asp:GridView>
            <asp:XmlDataSource ID="xdsWarehouseReceiptSource" runat="server" 
                EnableCaching="False" EnableViewState="False" /> 
        </td>
    </tr>
</table>
               <!--
               <asp:TemplateField HeaderText="Client">
                   <ItemTemplate>
                       <asp:Label ID="lblClient" runat="server" Text='<%# GetClientName(XPath("@ClientId")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               -->
</asp:Content>
