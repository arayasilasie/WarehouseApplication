<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UISamplingResultSearch.ascx.cs" Inherits="WarehouseApplication.UserControls.UISamplingSearch" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
   <table class="PreviewEditor"   >
<tr class="PreviewEditorCaption">
<td colspan="2" >Search Criteria</td>
</tr>
     <tr>
     <td class="Text" style="width:150px" >Tracking No : </td>
     <td class="Input"><asp:TextBox ID="txtTrackingNo" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
     <tr>
     <td class="Text">Sampling Code :</td>
     <td class="Input"><asp:TextBox ID="txtSamplingCode" runat="server" Width="100px"></asp:TextBox></td>
     <tr>
     <td class="Button" colspan="2"><asp:Button ID="btnSearch" runat="server" Text="Search" 
             onclick="btnSearch_Click" /></td>
     </tr>
     </table>
     <table>
    
     <tr><td colspan="2"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td></tr>
     <tr><td colspan="2" class="Text">
         <asp:GridView ID="gvGradingResult" runat="server" AllowPaging="True" 
             CellPadding="4" ForeColor="#333333" GridLines="None" Width="631px" 
             onpageindexchanged="gvGradingResult_PageIndexChanged" 
             onpageindexchanging="gvGradingResult_PageIndexChanging" 
             AutoGenerateColumns="False" onrowediting="gvGradingResult_RowEditing" 
             onrowcommand="gvGradingResult_RowCommand">
             <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
             <RowStyle CssClass="GridRow" />
             <EmptyDataRowStyle BackColor="#F8E76B" BorderColor="Black" />
               <Columns>
                <asp:TemplateField HeaderText="" Visible="false">
                      <ItemTemplate>
                        <asp:Label Runat="server" Visible="false" Text='<%# Bind("Id") %>'     ID="lblId"></asp:Label>
                     </ItemTemplate> 
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="TrackingNo" HeaderStyle-HorizontalAlign="Left" Visible="true">
                      <ItemTemplate>
                        <asp:Label Runat="server" Visible="true" Text='<%# Bind("TrackingNo") %>'     ID="lblTrackingNo"></asp:Label>
                     </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Sampling Code" HeaderStyle-HorizontalAlign="Left" Visible="true">
                      <ItemTemplate>
                        <asp:Label Runat="server" Visible="true" Text='<%# Bind("SamplingResultCode") %>'     ID="lblSamplingCode"></asp:Label>
                     </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Is Supervisor" HeaderStyle-HorizontalAlign="Left" Visible="true">
                      <ItemTemplate>
                          <asp:CheckBox ID="chkIsSupervisor" Enabled="false" Checked='<%# Bind("isSupervisor") %>' runat="server" />
                     </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" Visible="true">
                      <ItemTemplate>
                        <asp:Label Runat="server" Visible="true" Text='<%# Bind("Status") %>'     ID="lblStatus"></asp:Label>
                     </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                
                
           <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdEdit" Visible="true" CausesValidation="false" CommandName="Edit"  runat="server">Edit</asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>       
                
                  <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdPrint" Visible="true" CausesValidation="false" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  runat="server">Print</asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>
                   
                   
                  
                
                 
             </Columns>
             <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
             <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
                 CssClass="GridSelectedRow" />
             <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
             <EditRowStyle BackColor="#7C6F57" />
             <AlternatingRowStyle CssClass="GridAlternate" />
         </asp:GridView>
         </td></tr>
     <tr>
     <td colspan="5">
         &nbsp;</td>
     </tr>
     </table>



    
    
    
        