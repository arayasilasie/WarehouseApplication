<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ApproveInventoryTransfer.aspx.cs" Inherits="WarehouseApplication.ApproveInventoryTransfer" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
    </style>
     <div class="container">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <uc1:Messages ID="Messages1" runat="server" />
            </div>
            <br />
            
            <div id="Header" class="formHeader" style="width: 95%;  vertical-align: middle; margin-left:20px;" align="center">
                <asp:Label ID="lblDetail" Text="INVENTORY TRANSFER APPROVAL" Width="100%" runat="server"></asp:Label>
            </div>
            <div style="margin-top: 10px; width: 95%; margin-left:20px;"">
                <asp:GridView ID="grvInvTransferApproval" runat="server" AutoGenerateColumns="False"
                    BorderColor="White" CellPadding="4" DataKeyNames="ID" Style="font-size: small"
                    Width="100%" CssClass="label" CellSpacing="1" GridLines="None" 
                    OnRowCreated="grvInvTransferApproval_RowCreated"
                    OnSelectedIndexChanged="grvInvTransferApproval_SelectedIndexChanged">
                    <EmptyDataTemplate>
                        <asp:Label ID="lbl" runat="server" Text="No inventory transfer to approve." /></EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Symbol" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.Year" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StackID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblStackID" runat="server" Text='<%# Bind("StackID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StackID2" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblStackID2" runat="server" Text='<%# Bind("StackID2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LIC" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shed" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblShed" runat="server" Text='<%# Bind("Shed") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.Count" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSystemCount" runat="server" Text='<%# Bind("SystemCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.Weight" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSystemWeigh" runat="server" Text='<%# Bind("SystemWeight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.Count" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysicalCount" runat="server" Text='<%# Bind("PhysicalCount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.Weight" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysicalWeight" runat="server" Text='<%# Bind("PhysicalWeight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LIC" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLIC2" runat="server" Text='<%# Bind("LIC2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LIC2" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblLICID2" runat="server" Text='<%# Bind("LICID2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shed" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblShed2" runat="server" Text='<%# Bind("Shed2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--    <asp:TemplateField HeaderText="Symbol">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSymbol2" runat="server" Text='<%# Bind("Symbol2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="P.Count" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysicalCount2" runat="server" Text='<%# Bind("PhysicalCountTo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderText="P.Weight">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysicalWeight2" runat="server" Text='<%# Bind("PhysicalWeighTo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Approve" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#DAE1CC" />
                    <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                        BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" />
                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#88AB2D" ForeColor="#CCFFCC" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
