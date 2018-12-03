<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISearchGrading.ascx.cs" Inherits="WarehouseApplication.UserControls.UISearchGrading" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
  <table class="PreviewEditor" style="width:800px"   >
<tr class="PreviewEditorCaption">
<td colspan="2" >Search Criteria</td>
</tr>
<tr>
<td colspan="2" class="Message">
    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
</tr>
<tr class="EditorAlternate">  
<td>Grading Code :</td>
<td><asp:TextBox ID="txtGradingCode" runat="server" Width="100px" ></asp:TextBox></td>
</tr>
     <tr>
     <td class="Text" style="width:150px" >Tracking No : </td>
     <td class="Input"><asp:TextBox ID="txtTrackingNo" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
     <tr class="EditorAlternate">
     <td class="Text">&nbsp;Sampling Code :</td>
     <td class="Input"><asp:TextBox ID="txtSamplingCode" runat="server" Width="100px"></asp:TextBox></td>
     <tr>
     <td>Date Time Requested : </td>
     <td>From:
         <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" 
             Enabled="True" TargetControlID="txtFrom">
         </cc1:CalendarExtender>
         To:
         <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
         <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="True" 
             TargetControlID="txtTo">
         </cc1:CalendarExtender>
     </td>
     </tr>
     <tr>
     <td class="Button" colspan="2"><asp:Button ID="btnSearch" runat="server" Text="Search" 
             onclick="btnSearch_Click" /></td>
     </tr>
     <tr>
     <td colspan="2" ></td>
     </tr>
     <tr>
     <td colspan="2" align="left">
        <asp:GridView ID="gvDetail" runat="server" Width="571px" 
             CssClass="Grid" 
            AutoGenerateColumns="False" onrowcommand="gvDetail_RowCommand">
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" CssClass="GridEmpty" />
            <Columns>
                       <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:BoundField DataField="GradingCode" HeaderStyle-HorizontalAlign="Left"  
                    HeaderText="Grading Code" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>
               
                <asp:BoundField DataField="DateCoded" HeaderStyle-HorizontalAlign="Left"  
                    HeaderText="Date Coded" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:BoundField>

                <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdEdit" CommandName="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text="Edit" />
                        </ItemTemplate>       
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <EditRowStyle CssClass="GridSelectedRow" />
            <AlternatingRowStyle CssClass="GridAlternate" />
            
        </asp:GridView>
     </td>
     </tr>
     </table>
  
