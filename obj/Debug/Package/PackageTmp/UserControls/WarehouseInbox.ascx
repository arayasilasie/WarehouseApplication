<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WarehouseInbox.ascx.cs" Inherits="WarehouseApplication.UserControls.WarehouseInbox" %>
  <br />
   <fieldset style="width:750px">
   <legend class="Text" style="color:#424D2D; font-size:medium"  >Warehouse Application Inbox</legend>
     <table>
     <tr>
     <td>&nbsp;&nbsp;</td>
     </tr>
   <tr>
   <td>
   <asp:Panel ID="pnlGRNCreation"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="GRN Creation Queue">
   <asp:GridView 
           ID="gvInbox" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            onrowcommand="gvInbox_RowCommand" CssClass="Grid" onselectedindexchanged="gvInbox_SelectedIndexChanged" 
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
</asp:Panel>
</td>
</tr>
     <tr>
     <td>&nbsp;&nbsp;</td>
     </tr>
<tr>
<td>
<asp:Panel ID="pnlGradeDispute"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="Grade Clarification ">
   <asp:GridView 
           ID="gvGradeDispute" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            onrowcommand="gvGradeDispute_RowCommand" CssClass="Grid" 
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
 </asp:Panel>
</td>
</tr> 

     <tr>
     <td>
      <asp:Panel ID="pnlApprovedGRNCancellation"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="Cancel Approved GRN Queue">

     <asp:GridView 
           ID="gvCancelApprovedGRN" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            CssClass="Grid" onrowcommand="gvRequestToCancelApprovedGRN_RowCommand" onselectedindexchanged="gvCancelApprovedGRN_SelectedIndexChanged"  
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" 
                    FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" 
                    FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' 
                    Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
         </asp:Panel> 
         </td>
     </tr>
<tr>
<td>
   <asp:Panel ID="pnlReSampling"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="Moisture Re-Sampling Queue">

    <asp:GridView 
           ID="gvReSampling" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
             CssClass="Grid" onrowcommand="gvReSampling_RowCommand" 
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
</asp:Panel>
</td>
</tr> 

     <tr>
     <td>&nbsp;&nbsp;</td>
     </tr>
<tr>
 <td>
 <asp:Panel ID="pnlEditGRN"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="Edit Approved GRN Queue">
   <asp:GridView 
           ID="gvRequestToEditApprovedGRN" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            CssClass="Grid" onrowcommand="gvRequestToEditApprovedGRN_RowCommand"  
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
</asp:Panel> 
 </td>    
</tr>
<tr>
<td>

</td>
</tr>      
<tr>

<td>
   <asp:Panel ID="pnlGIN"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="   GIN Creation Queue">

 
     <asp:GridView 
           ID="gvGIn" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            onrowcommand="gvGIN_RowCommand" CssClass="Grid" 
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
</asp:Panel>
 </td>
 </tr>
<tr>

<td>
   <asp:Panel ID="pnlGINEdit"   CssClass="Text" Width="700px" ForeColor="#424D2D" runat="server" GroupingText="   GIN Editing Queue">

 
     <asp:GridView 
           ID="gvGINEdit" runat="server" 
            
            AutoGenerateColumns="False" Width="650px" EmptyDataText="No pending Tasks" 
            onrowcommand="gvGINEdit_RowCommand" CssClass="Grid" 
           >
            <RowStyle CssClass="GridRow" />
            <EmptyDataRowStyle CssClass="GridEmpty" />
            <Columns>
            <asp:BoundField DataField="Name" Visible="False"  />
            
            <asp:TemplateField HeaderText="" Visible="false">
            <ItemStyle  />            
            <ItemTemplate>
            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Tasks" HeaderStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="300Px" />            
            <ItemTemplate>
            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Task Count" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left">

<FooterStyle HorizontalAlign="Left"></FooterStyle>

            <ItemStyle Width="150Px" HorizontalAlign="Center" />            
            <ItemTemplate >
            <asp:Label ID="lblCount"  runat="server"  Text='<%# Bind("Count") %>' Width="150Px" ></asp:Label>
            
            </ItemTemplate>
            </asp:TemplateField>
                
            <asp:ButtonField CommandName="ViewDetail" Text="Task List" 
                    ControlStyle-Width="150px" >  
<ControlStyle Width="150px"></ControlStyle>
                </asp:ButtonField>

            </Columns>
            <PagerStyle CssClass="GridPager" />
            <SelectedRowStyle CssClass="GridSelectedRow" />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridAlternate" />
        </asp:GridView>
</asp:Panel>
 </td>
 </tr>
    </table>