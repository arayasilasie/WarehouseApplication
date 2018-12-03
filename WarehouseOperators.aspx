<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="WarehouseOperators.aspx.cs" Inherits="WarehouseApplication.WarehouseOperators" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Required
        {
            color: #CC3300;
            font-size: large;
        }
        .style1
        {
            font-size: 12;
        }
        .style2
        {
            color: #CC3300;
            font-size: small;
        }
        .style3
        {
            height: 26px;
        }
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .style4
        {
            width: 1000px;
        }
        .style5
        {
            width: 17px;
        }
        .style8
        {
            width: 9px;
        }
        .style11
        {
            width: 137px;
        }
        .style12
        {
            width: 16px;
        }
        .style13
        {
            width: 132px;
        }
        .style14
        {
            width: 15px;
        }
        .style15
        {
            width: 130px;
        }
        .style16
        {
            width: 131px;
        }
        .style17
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <canter>
     <table bgcolor="White">
                    <tr>
                        <td style="width: 1010px">
                            <uc1:messages ID="Messages" runat="server" />
                        </td>
                    </tr>
                </table>
    <table bgcolor="White">
        
        <tr>
            <td>
                <asp:Label ID="lblWareHouse" runat="server" Text="Ware House :"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblOperatorType" runat="server" Text="Operator Type :"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblShed" runat="server" Text="Shed :"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblOperator" runat="server" Text="Operator :"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="drpWarehouse" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="drpType" AutoPostBack="True" runat="server" 
                    OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="drpShed" AutoPostBack="True" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="drpOperator" AutoPostBack="True" runat="server" 
                    onselectedindexchanged="drpOperator_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                <table>
            <tr>
            <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" OnClick="btnSave_Click" CssClass="style1" ValidationGroup="gin" />
                                    </td>
            <td>
                                    <asp:Button ID="btnDisable" runat="server" Text="Disable" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None"  CssClass="style1" ValidationGroup="gin" 
                                        onclick="btnDisable_Click" />
                </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table bgcolor="White">
                    <tr>
                        <td>
                            <asp:GridView ID="gvWareHouseoperators" DataKeyNames="OperatorId,WarehouseId,Type,ShedName"
                                runat="server" AutoGenerateColumns="False" EnableModelValidation="True" AllowPaging="True"
                                OnRowDataBound="gvWareHouseoperators_RowDataBound" OnPageIndexChanging="gvWareHouseoperators_PageIndexChanging"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" HorizontalAlign="Left" GridLines="Both" CssClass="label"
                                Width="100%">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="ShedName" HeaderText="Shed Name" SortExpression="ShedName" />
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="small" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</canter>
</asp:Content>
