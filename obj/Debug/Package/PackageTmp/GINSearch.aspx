<%@ Page Title="GIN Search" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GINSearch.aspx.cs" Inherits="WarehouseApplication.GINSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ShowHand
        {
            cursor: pointer;
        }
        .style1
        {
            font-size: small;
        }
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table class="style1" bgcolor="White">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblGINSearchHeader" Width="800px" BackColor="#88AB2D" Font-Size="12"
                                runat="server" Text="Search Good Issuance Notice" ForeColor="White"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="800px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGIN" runat="server" Text="GIN No :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWareHouseReceipt" runat="server" Text="Warehouse Receipt :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClientId" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text="Status :" CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGINNo" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWareHouseReceipt" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpStatus" runat="server" Width="145px" CssClass="style1">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                            BackColor="#88AB2D" ForeColor="White" Width="100px" BorderStyle="None" CssClass="style1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <hr style="width: 800px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gvSearchGIN" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                ForeColor="Black" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                Width="100%" BorderWidth="1px" DataKeyNames="Id" CssClass="label" GridLines="Vertical"
                                OnRowCommand="gvSearchGIN_RowCommand" 
                                onrowdatabound="gvSearchGIN_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ClientName" CommandName="Search" CommandArgument='<%#Eval("GINNumber")%>'
                                                CssClass="ShowHand" runat="server" Text='<%#Eval("ClientName")%>'>LinkButton </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Symbol">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodityName" Text='<%#Eval("CommodityName")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="WHR No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouseReceiptNo" Text='<%#Eval("WarehouseReceiptNo")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GINNumber" HeaderText="GIN No" />
                                    <asp:TemplateField HeaderText="GIN Status ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGINStatus" Text='<%#Eval("GINStatus")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeightInKg" Text='<%#Eval("WeightInKg")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMeasurement" Text='<%#Eval("Measurement")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusName" Text='<%#Eval("PickupNoticesList[0].StatusName")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptyData" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="None" BorderWidth="0px" Text="There is no data with your selection criteria"></asp:Label>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
