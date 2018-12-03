<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SamplingTicket.aspx.cs" Inherits="WarehouseApplication.SamplingTicket" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
    <uc1:Messages ID="Messages" runat="server" />  <br />
        </ContentTemplate>
</asp:UpdatePanel>   

<div>
<table><tr><td>
    <asp:Label ID="lblTrackNo" runat="server" Text="Tracking No:" Width="100px"></asp:Label><asp:TextBox 
        ID="txtTrackNo" runat="server" Height="22px" Width="168px"></asp:TextBox></td>
       <td> &nbsp;&nbsp;&nbsp; <asp:Label ID="lblSampleCode" runat="server" Text="Sample Code:"></asp:Label>&nbsp;<asp:TextBox 
        ID="txtSampleCode" runat="server" Height="22px" Width="168px"></asp:TextBox></td>
</tr>
       <tr>
        <td><asp:Label ID="lblByStatus" runat="server" Text="Sample Status:" Width="100px"></asp:Label>
        <asp:DropDownList
        ID="cboSampleStatus" runat="server" Height="22px" Width="168px" 
                AppendDataBoundItems="True"></asp:DropDownList></td>
       </tr>
 </table>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
    <ProgressTemplate>
      <a style=" font-family:Agency FB; font-size:14pt; color:Green; font-weight:lighter">Loading...</a>
    </ProgressTemplate>
    </asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
 <ContentTemplate>
<asp:LinkButton ID="lbtnReloadForResample" runat="server" 
                                                    onclick="lbtnReload_Click">Search</asp:LinkButton>
            <asp:GridView ID="gvSampleTicketList" runat="server"  
                         AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                         GridLines="None" BorderColor="Black" BorderStyle="Solid" 
                         BorderWidth="1px" AutoGenerateSelectButton="True"  >
                     <EmptyDataTemplate>
                         The list is Empty
                     </EmptyDataTemplate>
                     <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                     <RowStyle BackColor="#DAE1CC" />
                     <Columns>
                 
                            <asp:TemplateField HeaderText="" Visible="False">
                            <ItemTemplate>
                                    <asp:Label ID="lblArrivalID" runat="server" Text='<%# Bind("ArrivalID") %>'></asp:Label>
                                </ItemTemplate> 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                            <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate> 
                                </asp:TemplateField>
                                
                           
                            <asp:TemplateField HeaderText="Tracking No." >
                                                <ItemTemplate>
                                    <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Bind("TrackingNo") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sample Code" >
                                                <ItemTemplate>
                                    <asp:Label ID="lblPreSampleCode" runat="server" Text='<%# Bind("SampleCode") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblSamplingStatusID" runat="server" Text='<%# Bind("SamplingStatusID") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" >
                                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>                
                            <asp:TemplateField HeaderText="Created Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreateTimestamp" runat="server" Text='<%# Bind("CreateTimestamp") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='AddSamplingResultNew.aspx?SampleTicketId=<%# Eval("ID") %>'
                        style='<%# Convert.ToBoolean("True")?"display: block;": "display: none;" %>'
                        >Edit</a>
                                </ItemTemplate>       
                            </asp:TemplateField>--%>
                       
               
                     </Columns>
                     <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" 
                         BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" 
                         Font-Names="Verdana" Font-Size="0.8em" />
                     <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" 
                         Font-Names="Verdana" Font-Size="0.8em" />
                     <EditRowStyle BackColor="#7C6F57" />
                     <AlternatingRowStyle BackColor="White" />
                     </asp:GridView>
                     <asp:Button ID="btnGetSampleTicket" runat="server" Text="Get Sample Ticket" 
                     BackColor="#88AB2D" ForeColor="White" Width="121px" BorderStyle="None"
            onclick="btnGetSampleTicket_Click" Height="22px" />
                         <asp:Button ID="btnEdit" runat="server" Visible="False" 
         BackColor="#88AB2D" BorderStyle="None" ForeColor="White" Height="22px" 
         onclick="btnEditSampleTicket_Click" Text="Edit Sample Ticket" Width="121px" />
                         </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>
