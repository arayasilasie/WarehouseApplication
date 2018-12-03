<%@ Page Title="Assign Shed" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="AssignShed.aspx.cs" Inherits="Shed.AssignShed" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
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
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <table style="background-color: #FFFFFF">
        <tr>
            <td>
                <uc1:Messages ID="Messages" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblShed" runat="server" Text="Shed Number:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShed" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" BackColor="#88AB2D" ForeColor="White"
                                Width="80px" BorderStyle="None" CssClass="style1" OnClick="btnUpdate_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" DataKeyNames="ID" GridLines="None" OnSelectedIndexChanged="gvShow_SelectedIndexChanged"
                    OnPageIndexChanging="gvShow_PageIndexChanging" CssClass="label" AllowPaging="True"
                    AllowSorting="True" OnSorting="gvShow_Sorting">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="GRNNo" HeaderText="GRN Number" SortExpression="GRN_Number" />
                        <asp:BoundField DataField="WarehouseReceiptNo" HeaderText="Warehouse Receipt No"
                            SortExpression="WarehouseReceiptNo" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName" />
                        <asp:BoundField DataField="MemberName" HeaderText="Member Name" SortExpression="MemberName" />
                        <asp:BoundField DataField="CommodityName" HeaderText="Commodity Name" SortExpression="CommodityName" />
                        <asp:BoundField DataField="ProductionYear" HeaderText="Production Year" SortExpression="ProductionYear" />
                        <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse Name" SortExpression="WarehouseName" />
                        <asp:BoundField DataField="ShedNumber" HeaderText="Shed Number" SortExpression="ShedNumber" />
                        <asp:CommandField ShowSelectButton="True" />
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
</asp:Content>
