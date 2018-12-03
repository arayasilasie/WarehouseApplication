<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="InventoryControlAdjustmentApprove.aspx.cs" Inherits="WarehouseApplication.InventoryControlAdjustmentApprove" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .align
        {
            text-align: left;
        }
        .label
        {
            display: block;
            font-size:small;
           font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            width: 659px;
        }
        .style2
        {
            text-align: left;
            width: 526px;
            font-family: Verdana;
        }
        .style3
        {
            width: 526px;
        }
        .style4
        {
            width: 866px;
        }
        .style6
        {
            width: 878px;
            height: 74px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div style="background-color: White; width: 850px; margin-top: 7px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <div style="background-color: #88AB2D; text-transform: uppercase;">
                    <asp:Label ID="lblShedentry" runat="server" ForeColor="White" Text="Shed Inventory Control Approval"
                        CssClass="style1"></asp:Label>
                </div>
                <div style="margin-top: 7px;">
                    <asp:Label ID="lblShedNo" runat="server" Text="Shed No:" CssClass="label" Height="16px"
                        Width="181px"></asp:Label>
                    <asp:DropDownList ID="drpShed" runat="server" Height="16px" Visible="true" Width="247px"
                        OnSelectedIndexChanged="drpShed_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvdrpShed" runat="server" ErrorMessage="Please Select shed first"
                        ControlToValidate="drpShed" ValidationGroup="search">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerfvdrpGrade" runat="server" Enabled="True" TargetControlID="rfvdrpShed">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div style="margin-top: 7px;">
                    <asp:Label ID="lblLIC" runat="server" Height="16px" Width="181px" Text="Lead Inventory Controler:"
                        CssClass="label"></asp:Label>
                    <asp:DropDownList ID="drpLIC" runat="server" Height="20px" Width="247px" AppendDataBoundItems="True"
                        OnSelectedIndexChanged="drpLIC_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvdrpLIC" runat="server" ErrorMessage="Please Select LIC first"
                        ControlToValidate="drpLIC" ValidationGroup="search">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="reqvdrpLIC">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div style="margin-top: 7px;">
                    <asp:Label ID="lblInventoryControlDate" Height="16px" Width="181px" runat="server"
                        CssClass="label" Text="Inventory Control Date : "></asp:Label>
                    <asp:DropDownList ID="drpInventoryControlDate" runat="server" Height="18px" AppendDataBoundItems="true"
                        Width="247px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqvdrpInventoryControlDate" runat="server" ErrorMessage="Please Select Inventory Control Date first"
                        ControlToValidate="drpInventoryControlDate" ValidationGroup="search">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                        TargetControlID="reqvdrpInventoryControlDate">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div style="margin-top: 7px; margin-left: 390px;">
                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                        CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" ValidationGroup="search"
                        Width="100px" />
                </div>
                <div style="margin-top: 7px;">
                    <hr />
                </div>
                <div style="margin-top: 7px;">
                    <asp:GridView ID="gvInventoryDetail" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="Solid" CssClass="label" ForeColor="Black"
                        GridLines="Both" PageSize="30" Width="600px" ShowHeaderWhenEmpty="True" EmptyDataText="No Inventory Adjustment Pending">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="StackID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStackID" runat="server" Text='<%# Bind("StackID") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="InventoryID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblInventoryID" runat="server" Text='<%# Bind("InventoryID") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Stack No" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblStackNo" runat="server" Text='<%# Bind("StackNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="CommodityGradeID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityGradeID" runat="server" Text='<%# Bind("CommodityGradeID") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="150Px" HeaderText="Commodity Grade" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityGrade" runat="server" Text='<%# Bind("CommodityGrade") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Production Year" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lbProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="System Count" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemCount" runat="server" Text='<%# Bind("SystemCount") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100Px" HeaderText="System Weight" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemWeight" runat="server" Text='<%# Bind("SystemWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Physical Count" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhysicalCount" runat="server" Text='<%# Bind("PhysicalCount") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100Px" HeaderText="Physical Weight" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblPhysicalWeight" runat="server" Text='<%# Bind("PhysicalWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Adjustment Count" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdjustmentCount" runat="server" Text='<%# Bind("AdjustmentCount") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100Px" HeaderText="Adjustment Weight" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdjustmentWeight" runat="server" Text='<%# Bind("AdjustmentWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100Px" HeaderText="Approve" Visible="true">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="chkApproval" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text="Approve" Value="Approve" />
                                        <asp:ListItem Text="Reject" Value="Reject" />
                                        <asp:ListItem Selected="True" Text="None" Value="None" />
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                                <ControlStyle Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="false" Font-Size="small" 
                            ForeColor="White" />
                    </asp:GridView>
                </div>
                <%--</td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">--%>
                <div style="margin-top: 7px;">
                    <hr />
                </div>
                <%-- </td>
                            </tr>
                            <tr>
                                <td colspan="2">--%>
                <div style="margin-top: 7px;">
                    <center>
                        <asp:Button ID="btnSave" runat="server" BackColor="#88AB2D" BorderStyle="None" CssClass="style1"
                            ForeColor="White" OnClick="btnSave_Click" Text="Save" OnClientClick="if (Page_ClientValidate('<%= btnSave.ValidationGroup %>')) {
                                        $.blockUI({ message: $('#divMessage') });
                                            }" ValidationGroup="save" Width="100px" Enabled="False" />
                    </center>
                </div>
                <%--</td>
                            </tr>
                            <tr>
                                <td class="Text" colspan="2">--%>
                <div style="margin-top: 7px;">
                    <hr />
                </div>
                <div id='divMessage' class='divMessage' style="display: none;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/saving.gif" AlternateText="Saving..." />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                    Working...</a>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
