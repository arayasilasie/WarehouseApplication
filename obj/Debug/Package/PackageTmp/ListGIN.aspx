<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ListGIN.aspx.cs" Inherits="WarehouseApplication.ListGIN" %>
<%@ Register src="UserControls/SearchConditionSelector.ascx" tagname="SearchConditionSelector" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table style="width:850px">
    <tr>
        <td class="Input" colspan="2">
            <fieldset>
                <legend class="Text">Search GIN</legend>
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
        <td class="Text" colspan="2"><asp:Label ID="lblGIN" runat="server" Text="GINs" /></td>
    </tr>
    <tr>
        <td align="left" class="Input" colspan="2">
        <asp:GridView ID="gvGIN" runat="server" Width="100%" 
          CellPadding="4" CssClass="Grid"  
          DataKeyNames="GINId" Font-Size="Small" AutoGenerateColumns="False" 
                DataSourceID="xdsGINSource" EnableViewState="False" 
                onselectedindexchanged="gvGIN_SelectedIndexChanged">
          <FooterStyle CssClass="GridHeader"/>
          <HeaderStyle CssClass="GridHeader" />
          <PagerStyle CssClass="GridPager" />
          <SelectedRowStyle CssClass="GridSelectedRow"/>
          <EditRowStyle CssClass="GridRow"/>
          <AlternatingRowStyle CssClass="GridAlternate"/>
          <RowStyle CssClass="GridRow"/>
          <Columns>
               <asp:BoundField DataField="GINId" HeaderText="GINId" Visible="False" />
               <asp:CommandField ShowSelectButton="True" />
               <asp:TemplateField HeaderText="GIN No.">
                   <ItemTemplate>
                       <asp:Label ID="lblGINNo" runat="server" Text='<%# XPath("@GINNo") %>'></asp:Label>
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
               <asp:TemplateField HeaderText="Client Id">
                   <ItemTemplate>
                       <asp:Label ID="lblClientId" runat="server" 
                           Text='<%# GetClientId(XPath("@ClientId")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Client Name">
                   <ItemTemplate>
                       <asp:Label ID="lblClientName" runat="server" 
                           Text='<%# GetClientName(XPath("@ClientId")) %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="DateLoaded" HeaderText="Date Loaded" DataFormatString="{0:d}" />
               <asp:BoundField DataField="DateWeighed" HeaderText="Date Weighed" DataFormatString="{0:d}" />
          </Columns>
        </asp:GridView>
        <asp:XmlDataSource ID="xdsGINSource" runat="server" 
                XPath="/Catalog/GIN" EnableViewState="False" EnableCaching="False" />
       </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="panCommands" runat="server" Visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnDriverInfo" runat="server" Text="Driver" 
                                onclick="btnDriverInfo_Click" />
                            <asp:Button ID="btnLoadingInfo" runat="server" Text="Loading" 
                                onclick="btnLoadingInfo_Click" />
                            <asp:Button ID="btnScalingInfo" runat="server" Text="Scaling" 
                                onclick="btnScalingInfo_Click" />
                            <asp:Button ID="btnPrintGIN" runat="server" Text="Print GIN" 
                                onclick="btnPrintGIN_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    </table>
</asp:Content>
