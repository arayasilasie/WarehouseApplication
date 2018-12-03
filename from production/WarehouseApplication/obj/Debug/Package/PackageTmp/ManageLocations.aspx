<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ManageLocations.aspx.cs" Inherits="WarehouseApplication.ManageLocations" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <uc1:Messages ID="Messages" runat="server" />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upClass" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
        UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="panelForClass" runat="server">
                <div class="formHeader" style="margin-top: 7px; margin-left: 10px; text-align: center;">
                    <asp:Label ID="lblClassHeader" runat="server" ForeColor="white" Text="Manage Locations"
                        CssClass="style1"></asp:Label></div>
                <div style="border: solid 1px #88AB2D; width: 510px; height: 70px; margin-left: 15px;
                    margin-bottom: 10px;">
                    <div style="background-color: #88AB2D; margin-top: 7px;">
                        <asp:Label ID="Label2" runat="server" ForeColor="white" Text="Search Criteria's"
                            CssClass="style1"></asp:Label></div>
                    <div style="float: left; margin-top: 5px; width: 300px;">
                        <asp:Label ID="lblSearchName" runat="server" Text="Name:" Width="100px"></asp:Label>
                        <asp:TextBox ID="txtLocationName" runat="server" Width="168px" ValidationGroup="zone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLocationName"
                            ForeColor="Red" ErrorMessage="Please first enter zone name!" ValidationGroup="search">*</asp:RequiredFieldValidator>
                    </div>
                    <div style="float: left; margin-top: 5px; margin-right:5px;">
                        <asp:RadioButtonList ID="rbZoneWoreda" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Woreda" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Zone" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="float: left; margin-top: 14px;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" BackColor="#88AB2D" ForeColor="White"
                            Width="64px" BorderStyle="None" Height="18px" ValidationGroup="search" 
                            onclick="btnSearch_Click" />
                    </div>
                </div>
                <div style="border: solid 1px #88AB2D; margin-left: 15px; height: 69px;">
                    <div style="height: 33px">
                        <div style="margin-top: 5px; float: left; width: 300px;">
                            <asp:Label ID="lblAddRegion" runat="server" Text="Region Name:" Width="100px"></asp:Label>
                            <asp:TextBox ID="txtAddRegion" runat="server" Width="168px" ValidationGroup="region"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rtxtAddRegion" runat="server" ControlToValidate="txtAddRegion"
                                ForeColor="Red" ErrorMessage="Please first enter region name!" ValidationGroup="region">*</asp:RequiredFieldValidator>
                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True"
                            TargetControlID="rtxtAddRegion">
                        </cc1:ValidatorCalloutExtender>--%>
                        </div>
                        <div style="margin-top: 5px; float: left; margin-bottom: 6px; margin-left: 10px;
                            width: 70px;">
                            <asp:Button ID="btnAddRegion" runat="server" Text="Add" BackColor="#88AB2D" ForeColor="White"
                                Width="66px" BorderStyle="None" Height="22px" ValidationGroup="region" OnClick="btnAddRegion_Click" />
                        </div>
                        <div style="margin-top: 5px; float: left; margin-left: 10px; margin-bottom: 6px;">
                            <asp:Button ID="btnUpdateRegion" runat="server" Text="Update" BackColor="#88AB2D"
                                ForeColor="White" Width="71px" BorderStyle="None" Height="22px" ValidationGroup="region"
                                Visible="false" OnClick="btnUpdateRegion_Click" />
                        </div>
                    </div>
                    <div style="margin-top: 1px; margin-bottom: 6px; clear: both;">
                        <div style="float: left; width: 300px;">
                            <asp:Label ID="lblRegion" runat="server" Text="Select Region: " Width="100px"></asp:Label>
                            <asp:DropDownList ID="cboRegion" runat="server" Height="22px" Width="168px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboRegion_SelectedIndexChanged" AppendDataBoundItems="True">
                            </asp:DropDownList>
                        </div>
                        <div style="float: left; margin-left: 10px; width: 70px;">
                            <asp:Button ID="btnRemoveRegion" runat="server" Text="Cancel" BackColor="#88AB2D"
                                ForeColor="White" Width="66px" BorderStyle="None" Height="22px" ValidationGroup="region"
                                OnClick="btnRemoveRegion_Click" />
                        </div>
                    </div>
                </div>
                <div style="float: left; margin-top: 10px; margin-left: 20px; border: solid 1px #88AB2D;
                    width: 470px;">
                    <div style="margin-top: 5px;">
                        <asp:Label ID="lblZoneRegion" runat="server" Text="Region: " Width="100px"></asp:Label>
                        <asp:DropDownList ID="drpZoneRegion" runat="server" Height="22px" Width="168px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpZoneRegion"
                            ForeColor="Red" ErrorMessage="Please first enter the region for the zone!" ValidationGroup="zone">*</asp:RequiredFieldValidator>
                    </div>
                    <div style="float: left; margin-top: 5px; width: 300px;">
                        <asp:Label ID="lblZone" runat="server" Text="Zone Name:" Width="100px"></asp:Label>
                        <asp:TextBox ID="txtAddZone" runat="server" Width="168px" ValidationGroup="zone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rtxtAddZone" runat="server" ControlToValidate="txtAddZone"
                            ForeColor="Red" ErrorMessage="Please first enter zone name!" ValidationGroup="zone">*</asp:RequiredFieldValidator>
                    </div>
                    <div style="float: left; margin-top: 5px;">
                        <asp:Button ID="btnAddZone" runat="server" Text="Add" BackColor="#88AB2D" ForeColor="White"
                            Width="54px" BorderStyle="None" Height="22px" ValidationGroup="zone" OnClick="btnAddZone_Click" />
                    </div>
                    <div style="float: left; margin-top: 5px; margin-left: 12px; margin-bottom: 6px;
                        margin-right: 5px;">
                        <asp:Button ID="btnUpdateZone" runat="server" Text="Update" BackColor="#88AB2D" ForeColor="White"
                            Width="72px" BorderStyle="None" Height="22px" ValidationGroup="zone" Visible="false"
                            OnClick="btnUpdateZone_Click" />
                    </div>
                    <%--</div>--%>
                    <br />
                    <div style="margin-top: 5px; margin-left: 50px; clear: both;">
                        <asp:GridView ID="gvZone" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" ShowHeaderWhenEmpty="True" Width="286px" AllowPaging="True"
                            OnPageIndexChanging="gvZone_PageIndexChanging" PageSize="15" AutoGenerateSelectButton="True"
                            OnSelectedIndexChanged="gvZone_SelectedIndexChanged" EmptyDataText="The list is Empty"
                            OnRowDeleting="gvZone_RowDeleting">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#DAE1CC" />
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegionID" runat="server" Text='<%# Bind("RegionID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZoneID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZone" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Link" DeleteText="Cancel" ShowDeleteButton="true" ControlStyle-ForeColor="Blue" />
                            </Columns>
                            <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                            <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="0.8em" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
                <div style="float: left; margin-left: 20px; margin-top: 50px; border: solid 1px #88AB2D;
                    width: 470px;">
                    <div style="margin-top: 10px;">
                        <div style="margin-top: 5px;">
                            <asp:Label ID="Label1" runat="server" Text="Region: " Width="100px"></asp:Label>
                            <asp:DropDownList ID="drpWoredaRegion" runat="server" Height="22px" Width="168px"
                                Enabled="false" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpZoneRegion"
                                ForeColor="Red" ErrorMessage="Please first enter the region for the zone!" ValidationGroup="zone">*</asp:RequiredFieldValidator>
                        </div>
                        <div style="margin-top: 5px;">
                            <asp:Label ID="lblWoredaZone" runat="server" Text="Zone: " Width="100px"></asp:Label>
                            <asp:DropDownList ID="drpWoredaZone" runat="server" Height="22px" Width="168px" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpWoredaZone"
                                ForeColor="Red" ErrorMessage="Please first enter the zone of the Woreda!" ValidationGroup="wa">*</asp:RequiredFieldValidator>
                        </div>
                        <div style="float: left;">
                            <asp:Label ID="lblAddWoreda" runat="server" Text="Woreda Name:" Width="100px"></asp:Label>
                            <asp:TextBox ID="txtAddWoreda" runat="server" Width="168px" ValidationGroup="wa"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rtxtAddWoreda" runat="server" ControlToValidate="txtAddWoreda"
                                ForeColor="Red" ErrorMessage="Please first enter Woreda name!" ValidationGroup="wa">*</asp:RequiredFieldValidator>
                            <%--  <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                            TargetControlID="rtxtAddWoreda">
                        </cc1:ValidatorCalloutExtender>--%>
                        </div>
                        <div style="float: left; margin-bottom: 6px;">
                            <asp:Button ID="btnAddWoreda" runat="server" Text="Add" BackColor="#88AB2D" ForeColor="White"
                                Width="63px" BorderStyle="None" Height="22px" ValidationGroup="wa" OnClick="btnAddWoreda_Click" />
                        </div>
                        <div style="float: left; margin-left: 10px; margin-bottom: 6px; margin-right: 5px;">
                            <asp:Button ID="btnUpdateworeda" runat="server" Text="Update" BackColor="#88AB2D"
                                ForeColor="White" Width="76px" BorderStyle="None" Height="22px" ValidationGroup="wa"
                                OnClick="btnUpdateworeda_Click" Visible="false" />
                        </div>
                        <br />
                        <div style="margin-top: 5px; margin-left: 50px; clear: both;">
                            <asp:GridView ID="gvWereda" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="1px" ShowHeaderWhenEmpty="True" EmptyDataText="The list empty" Width="285px"
                                AllowPaging="True" OnPageIndexChanging="gvWereda_PageIndexChanging" PageSize="20"
                                OnRowDeleting="gvWereda_RowDeleting" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvWereda_SelectedIndexChanged">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#DAE1CC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegionID" runat="server" Text='<%# Bind("RegionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblZoneID" runat="server" Text='<%# Bind("ZoneID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWoredaID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Woreda">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWoreda" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" DeleteText="Cancel" ShowDeleteButton="true" ControlStyle-ForeColor="Blue" />
                                </Columns>
                                <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                    BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="0.8em" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
