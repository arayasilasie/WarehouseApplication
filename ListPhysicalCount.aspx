<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListPhysicalCount.aspx.cs" Inherits="WarehouseApplication.ListPhysicalCount" %>
<%@ Register src="UserControls/SearchConditionSelector.ascx" tagname="SearchConditionSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="850px">
    <tr>
        <td class="Input" colspan="2">
            <fieldset>
                <legend class="Text">Search Physical Count</legend>
                <uc1:SearchConditionSelector ID="SearchConditionSelector1" runat="server" />
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnSearch" runat="server" Text="  Search  " 
                onclick="btnSearch_Click" />
            <asp:Button ID="btnAdd" runat="server" Text=" Add Physical Count " 
                onclick="btnAdd_Click" />
        </td>
    </tr>
    <tr>
        <td class="Text" colspan="2"><asp:Label ID="lblPhysicalCounts" runat="server" Text="Physical Counts" /></td>
    </tr>
    <tr>
        <td align="left" class="Input" colspan="2">
        <asp:GridView ID="gvPhysicalCount" runat="server" Width="100%" 
          CellPadding="4" CssClass="Grid"
          DataKeyNames="Id" Font-Size="Small" AutoGenerateColumns="False" 
                DataSourceID="xdsPhysicalCountSource" 
                AutoGenerateSelectButton="True" EnableViewState="False" 
                onselectedindexchanged="gvPhysicalCount_SelectedIndexChanged">
          <FooterStyle CssClass="GridHeader"/>
          <HeaderStyle CssClass="GridHeader" />
          <PagerStyle CssClass="GridPager" />
          <SelectedRowStyle CssClass="GridSelectedRow"/>
          <EditRowStyle CssClass="GridRow"/>
          <AlternatingRowStyle CssClass="GridAlternate"/>
          <RowStyle CssClass="GridRow"/>
          <Columns>
               <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
               <asp:BoundField DataField="PhysicalCountDate" HeaderText="Physical Count Date" DataFormatString="{0:dd MMM, yyyy}" />
               <asp:BoundField DataField="IsBeginingCount" HeaderText="Begining Count"/>
          </Columns>
        </asp:GridView>
        <asp:XmlDataSource ID="xdsPhysicalCountSource" runat="server" 
                XPath="/Catalog/PhysicalCount" EnableCaching="False" 
                EnableViewState="False" />
       </td>
    </tr>
    <tr>
        <td class="Input" colspan="2"><asp:button ID="btnOpen" runat="server" Text="Open" 
                Enabled="false" onclick="btnOpen_Click"/></td>
    </tr>
</table>
</asp:Content>
