<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchSamplingTicket.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchSamplingTicket" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr >
<td colspan="2" style="width:475px" >
    <asp:Label CssClass="Message" ID="lblMessage" runat="server" Text=""></asp:Label></td>
</tr>
<tr class="PreviewEditorCaption">
<td colspan="2" style="width:475px" >Search S<span lang="en-us">ampling</span> Information:</td>
</tr>
<tr>
<td class="Text" style="width:130px">Tracking No : </td>
<td><asp:TextBox ID="txtTrackingNo" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="Text" style="width:130px">Sampling Code:</td>
<td><asp:TextBox ID="txtSampleCode" runat="server"></asp:TextBox></td>
</tr>
<tr  class="EditorAlternate">
<td class="Text" >Date Sampled: </td>
<td>
    <asp:TextBox ID="txtStratDate" runat="server"></asp:TextBox> 
    <cc1:CalendarExtender ID="txtStratDate_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtStratDate">
    </cc1:CalendarExtender>
    To :
    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
    <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
        Enabled="True" TargetControlID="txtEndDate">
    </cc1:CalendarExtender>
</td>
</tr>
<tr class="EditorCommand"> 
<td colspan="2" align="left"><asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" /></td>
</tr>
<tr><td></td></tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvScaling" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
        EmptyDataText="No Matching  Records Found." 
        CssClass="Grid" 
        Width="800px" onrowcommand="gvScaling_RowCommand" >
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
            CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
        <EditRowStyle BackColor="#7C6F57" />
        <RowStyle CssClass="GridRow" />
        <Columns>
               <asp:TemplateField HeaderText="" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sampling Code" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblScaleTicketNo" Visible="true" runat="server" Text='<%# Bind("SampleCode") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Tracking No">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Sampled">
                    <ItemTemplate>
                        <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Bind("GeneratedTimeStamp") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="edit" runat="server">Edit</asp:LinkButton>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdPrint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Print" runat="server">Print</asp:LinkButton>
                        </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>
</td>
</tr>
</table>
