<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ListInboxDetailNew.aspx.cs" Inherits="WarehouseApplication.ListInboxDetailNew" %>

<%@ Register Src="UserControls/WarehouseInbox.ascx" TagName="WarehouseInbox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--  <uc1:Messages ID="Messages" runat="server" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="container">
        <div id="MainForm" class="form" style="margin: 20px;">
            <div id="Header" class="formHeader" style="width: 80%; padding-left: 0%; padding-top: 5px;
                height: 25px;">
                <asp:Label ID="lblDetail" Width="100%" runat="server" Style="margin-left: 5Px; font-family: 'Times New Roman', Times, serif;
                    font-size: larger; color: #FFFFFF; font-weight: 700; text-align: center;"></asp:Label>
            </div>
            <div style="width: 980px; margin: 10px 0 0 10px">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                            Loading...</a>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <%-- <a style="color: #669999; font-size: larger; margin-bottom: 5px;"><b><asp:Label ID="lblDetail" runat="server" Text=""></asp:Label></b></a>
                        <asp:ImageButton ID="lbtnReload" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                            Width="17px" ToolTip="Reload" />
                        --%>
                        <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" AllowPaging="True" 
                            OnPageIndexChanging="grvDetail_PageIndexChanging" PageSize="45" 
                            onrowcommand="grvDetail_RowCommand" 
                            onselectedindexchanged="grvDetail_SelectedIndexChanged">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#DAE1CC" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Task No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTasks" Width="200px" runat="server" Text='<%# Bind("TaskNo") %>'></asp:Label>
                                        <asp:Label ID="lblTrackingNo" Width="200px" Visible="false" runat="server" Text='<%# Bind("TaskNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:BoundField DataField="TaskNo" Visible="False" />
                                <asp:ButtonField CommandName="Detail" Text="Detail" />
                            </Columns>
                            <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                            <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="0.8em" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
