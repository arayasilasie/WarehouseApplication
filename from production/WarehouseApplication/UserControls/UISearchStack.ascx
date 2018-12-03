<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchStack.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchStack" %>
   <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
   <fieldset style="height:auto; width: 600px;">
     <legend style="width: 110px; margin-top: 0px;" class="Text" >Search Stack</legend>
 
<table>
<tr>
<td colspan="2" class="Message">
    <asp:Label ID="lblmsg" runat="server"></asp:Label></td>
</tr>
<tr>
<td class="Text" valign="middle">By Shed :</td>
<td class="Input">
<table>
<tr>
<td>Warehouse :</td>
<td>Shed :</td>
</tr>
<tr>
<td>
    <asp:DropDownList ID="cboWarehouse" Width="150px" runat="server" 
        onselectedindexchanged="cboWarehouse_SelectedIndexChanged" 
        AutoPostBack="True">
    </asp:DropDownList>
    </td>
<td>
    <asp:DropDownList ID="cboShed" Width="150px" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </td>
</tr>
</table>
   </td>
</tr>
<tr>
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity :"></asp:Label>
            </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="350px" 
              >
        </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
                    Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
                    PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
                    ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity">
                </cc1:CascadingDropDown>
            </td>
        </tr>
        <tr>
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class :"></asp:Label>
        </td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="350px" 
            ></asp:DropDownList>
            <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
                Category="CommodityClass" Enabled="True" 
                LoadingText="Loading Commodity Class..." ParentControlID="cboCommodity" 
                PromptText="Please Select Commodity Class" ServiceMethod="GetCommodityClass" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
            </cc1:CascadingDropDown>
        </td>
        </tr>

        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade :"></asp:Label>
            </td>
            <td class="Input">
<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="350px" 
                     ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
            Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
            ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
            ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
            TargetControlID="cboCommodityGrade" UseContextKey="True">
        </cc1:CascadingDropDown>
            </td>
        </tr>
<tr>
    <td class="Text">By Stack Number :</td>
    <td class="Input"><asp:TextBox ID="txtStackNumber" Width="150px" runat="server"></asp:TextBox></td>
</tr>

<tr>
<td colspan="2" align="Left" class="Button">
    <asp:Button ID="btnSave" runat="server" Text="Search" onclick="btnSave_Click" /></td>
</tr>
</table>
</fieldset>
<table>
<tr>
<td><asp:GridView ID="gvStack" runat="server" CellPadding="4" ForeColor="#333333"
        GridLines="None" onselectedindexchanged="gvStack_SelectedIndexChanged" 
        AutoGenerateColumns="False" BorderColor="Black" 
        Height="138px" onpageindexchanging="gvStack_PageIndexChanging" 
        onselectedindexchanging="gvStack_SelectedIndexChanging" Width="714px" 
        CssClass="Grid">
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <RowStyle CssClass="GridRow" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
        <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse" />
        <asp:BoundField DataField="ShedName" HeaderText="Shed Name" />
        <asp:BoundField DataField="CommodityGradeName" HeaderText="Commodity Grade" />
        <asp:BoundField DataField="StackNumber" HeaderText="Stack Number" />
        <asp:BoundField DataField="DateStarted" DataFormatString="{0:d} " 
            HeaderText="Date Started" />
                       <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditScaling" runat="server">Edit</asp:LinkButton>
                        </ItemTemplate>
                </asp:TemplateField>
    </Columns>
    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
    <EditRowStyle BackColor="#7C6F57" />
    <AlternatingRowStyle CssClass="GridAlternate" />
</asp:GridView></td>
</tr>
</table>

