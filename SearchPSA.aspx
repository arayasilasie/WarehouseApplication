<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SearchPSA.aspx.cs" Inherits="WarehouseApplication.SearchPSA" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <uc1:Messages ID="Messages1" runat="server" />
                <asp:Panel ID="pnlMessage" runat="server">
                    <span id='spanMessage' style='font-family: Verdana; font-size: small; color: #006600'>
                    </span>
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Agency FB" Font-Size="14pt"
                        Style="font-family: Verdana; font-size: small; color: #006600"></asp:Label>
                </asp:Panel>
                <br />
                <div id="Header" class="formHeader" style="width:70%; margin-left: 40px; margin-top: 5px;"
                    align="center">
                    <asp:Label ID="lblDetail" Text="Search PSA" Width="100%" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 70%; margin-left: 40px;">
                    <div style="margin-bottom: 10px;">
                        <div style="border: solid 1px #88AB2D; height: 70px;">
                            <div style="margin-top: 10px; float: left; height: 26px; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="PSA No :"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtGINNo" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; margin-left: 7px; float: left;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lbllWareHouseReceipt" runat="server" Text="WareHouse Receipt :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtWHReceiptNo" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 7px;">
                                <div style="height: 26px;">
                                   
                                       <asp:Label ID="Label1" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                   <asp:TextBox ID="txtClientId" runat="server"></asp:TextBox>
                                 
                                </div>
                            </div>
                           <%-- <div style="margin-top: 10px; float: left; margin-left: 20px;">
                                <div style="height: 26px;">
                                <asp:Label ID="lblStatus" runat="server" Text="Status :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                   <asp:DropDownList ID="ddLIC" runat="server" AppendDataBoundItems="True" CssClass="style1"
                                        ValidationGroup="Search" Width="145px">
                                        <asp:ListItem Value="">Select LIC</asp:ListItem>
                                    </asp:DropDownList>
                                   
                                </div>
                            </div>--%>
                            <div style="margin-top: 10px; float: right; margin-right: 10px;">
                                <div style="height: 26px">
                                </div>
                                <div>
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" Text="Search"
                                        Width="100px" onclick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; width: 70%; margin-left: 40px;">
                    <asp:GridView ID="grvPSA" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        GridLines="Vertical" Style="font-size: small" CssClass="label"                 >
                        <EmptyDataTemplate>
                            <asp:Label ID="lbl" runat="server" Text="No PSA ..." /></EmptyDataTemplate>
                        <Columns>
                            
                            <asp:TemplateField HeaderText="PSANumber">
                                <ItemTemplate>
                                    <asp:Label ID="lblGINNumber" runat="server" Text='<%# Bind("GINNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="ClientIDNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblClientIDNo" runat="server" Text='<%# Bind("ClientIDNo") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="WHReceiptNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblWarehouseReceiptNo" runat="server" Text='<%# Bind("WarehouseReceiptNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Symbol">
                                <ItemTemplate>
                                    <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("CommodityName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Weight" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shed">
                                <ItemTemplate>
                                    <asp:Label ID="lblShedNumberr" runat="server" Text='<%# Bind("ShedName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LeadInventoryController") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# (Convert.ToInt16(Eval("GINStatusID"))%11 == 0)? "Approved":"On Process" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        
                            <asp:HyperLinkField DataNavigateUrlFields="GINID,GINStatusID" Text="Print" DataNavigateUrlFormatString="ReportViewer.aspx?GINID={0}&ST={1}"
                                NavigateUrl="javascript:window.open('~/ReportViewer.aspx?<%#Session[%>')" Target="_blank"
                                />
                        
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#e4efd0" />
                    </asp:GridView>
                </div>
              
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

