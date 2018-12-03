<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIGetGradingResults.ascx.cs" Inherits="WarehouseApplication.UserControls.UIGetGradingResults" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

      
<table class="PreviewEditor" style="width:300px"  >
<tr class="PreviewEditorCaption">
<td colspan="2">Search Grading Result</td>
</tr><tr>
            <td class="Text" style="width:150px">Tracking No : </td>
                <td><asp:TextBox ID="txtTrackingNo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
                <td class="Text" >Grading Code : </td>
                <td><asp:TextBox ID="txtGradingCode" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="EditorCommand">
                <td colspan="2" align="left"><asp:Button ID="btnSearch" CssClass="Forbtn" runat="server" Text="Search" 
                    onclick="btnSearch_Click" /></td>
        </tr>
        <tr>
        <td colspan="5">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        </table>
        <table   style="width:800px">
        <tr>
        <td colspan="5">
            <asp:GridView ID="gvGradingResult" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" 
                GridLines="None" onrowcancelingedit="gvGradingResult_RowCancelingEdit" 
                onrowediting="gvGradingResult_RowEditing" 
                onrowcommand="gvGradingResult_RowCommand" CssClass="Grid" >
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle CssClass="GridRow" />
                <Columns>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                                <asp:Label ID="lblId" Runat="server" Visible="true" Text='<%# Bind("Id") %>'    ></asp:Label>
                        </ItemTemplate> 
                        <EditItemTemplate>
                                <asp:Label Runat="server" Visible="true" Text='<%# Bind("Id") %>'     ID="lblId"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Tracking No" Visible="true">
                        <ItemTemplate>
                                <asp:Label Runat="server" Visible="true" Text='<%# Bind("TrackingNo") %>'     ID="lblGradingCode"></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Commodity Grade" Visible="true">
                        <ItemTemplate>
                                <asp:Label Runat="server" Visible="true" Text='<%# Bind("CommodityGradeName") %>'     ID="lblCommodityGradeId"></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>
<%--
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("GradeRecivedTimestamp") %>'></asp:Label>
                        </ItemTemplate>
                
                    </asp:TemplateField>--%>

                    

                                   <asp:CheckBoxField DataField="IsSupervisor" 
                        HeaderText="Is Supervisor" />

                    <asp:TemplateField HeaderText="" Visible="true">
                        <ItemTemplate>
                        <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text="Edit" CommandName="Edit" />
                                  
                        </ItemTemplate> 
                        <EditItemTemplate>
                         <asp:LinkButton ID="cmdEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" Text="Edit" CommandName="Edit" />                       
                        </EditItemTemplate>
                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="true">
                        <ItemTemplate>
                        <asp:LinkButton ID="cmdCA" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Client Acceptance" CommandName="CA" />
                                   
                        </ItemTemplate> 
                    </asp:TemplateField>
                    

                   
           
                </Columns>
                
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
                <SelectedRowStyle Font-Bold="True" CssClass="GridSelectedRow" 
                    HorizontalAlign="Left" VerticalAlign="Bottom" />
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle CssClass="GridAlternate" />
                
            </asp:GridView>
        
        </td>
        </tr>
        </table>

            
