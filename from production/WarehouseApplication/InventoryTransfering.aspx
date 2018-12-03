<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="InventoryTransfering.aspx.cs" Inherits="WarehouseApplication.InventoryTransfering" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        
        #MainForm
        {
            height: 375px; /*	border: solid 1px green;*/
        }
        #LeftForm
        {
            /* border: 1px solid #A5CBB0; */
            float: left;
            margin-left: 65px; /* margin-right: 50px;*/
            margin-top: 0;
        }
        #RightForm
        {
            float: Left;
            margin-left: 1px;
        }
        
        .formHeader
        {
            padding-top: 5px;
            border: solid 1px #88AB2D;
            height: 20px; /*padding-left:30%; */
            margin-bottom: 5px; /*background-image:url('../1divHeaderBg.png');*/
            background-color: #88AB2D;
            color: #CCFFCC;
            vertical-align: middle;
            text-transform: uppercase;
        }
        .accordionHeader
        {
            background-image: url('Images/search-add-icon.png');
            width: 32px;
            height: 32px;
        }
        
        .accordionHeaderSelected
        {
            background-image: url('Images/search-remove-icon.png');
            width: 32px;
            height: 32px;
        }
        .style1
        {
        }
        .style2
        {
            width: 32px;
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <div style="margin-left: 65px;">
                    <asp:Accordion ID="Accordion1" SuppressHeaderPostbacks="true" runat="server" FramesPerSecond="40"
                        RequireOpenedPane="false" TransitionDuration="250" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" Width="92%">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane1" runat="server">
                                <Header>
                                </Header>
                                <Content>
                                    <asp:Panel ID="PanelSearchs" runat="server">
                                        <fieldset>
                                            <div id="Div1" class="form" style="float: left; margin-right: 110px; margin-top: 0;
                                                margin-bottom: 10px">
                                                <div class="controlContainer" style="margin-top: 5px;">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label20" runat="server" Text="Transfer Date"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:TextBox ID="txtTrannsferDateSrch" runat="server" Width="170px" ValidationGroup="Search"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtTrannsferDateSrch_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtTrannsferDateSrch">
                                                        </asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Search"
                                                            ControlToValidate="txtTrannsferDateSrch" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtTrannsferDateSrch"
                                                        Display="None" ErrorMessage="Please enter valid date." Operator="DataTypeCheck"
                                                        Type="Date"></asp:CompareValidator>
                                                    <asp:ValidatorCalloutExtender ID="CompareValidator6_ValidatorCalloutExtender" runat="server"
                                                        Enabled="True" TargetControlID="CompareValidator6">
                                                    </asp:ValidatorCalloutExtender>
                                                </div>
                                            </div>
                                            <div id="Div2" class="form" style="float: left; margin-bottom: 10px;">
                                                <%-- Search --%>
                                                <div class="controlContainer" style="margin-top: 5px;">
                                                    <div class="leftControl">
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                                            CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" ValidationGroup="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; margin: 10px;">
                                                <asp:GridView ID="grvInventoryTransfers" runat="server" AllowPaging="True" BorderColor="White"
                                                    CellPadding="4" Style="font-size: small" Width="100%" CssClass="label" CellSpacing="1"
                                                    GridLines="None" PageSize="5" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCreated="grvInventoryTransfer_RowCreated"
                                                    OnSelectedIndexChanged="grvInventoryTransfer_SelectedIndexChanged">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text="No inventory transfered."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Symbol" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P.Year" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
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
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shed" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShed" runat="server" Text='<%# Bind("Shed") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P.Count" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhysicalCount" runat="server" Text='<%# Bind("PhysicalCount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P.Weight" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhysicalWeight" runat="server" Text='<%# Bind("PhysicalWeight") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LIC" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLIC2" runat="server" Text='<%# Bind("LIC2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
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
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# GetUrl(Eval("ID"))  %>'
                                                                    Text="Print"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                    Text="Cancel"></asp:LinkButton>
                                                                <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" CancelControlID="btnNo"
                                                                    DynamicServicePath="" Enabled="True" OkControlID="btnYes" PopupControlID="pnlConfirmation"
                                                                    TargetControlID="LinkButton1">
                                                                </asp:ModalPopupExtender>
                                                                <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server"
                                                                    ConfirmText="" DisplayModalPopupID="LinkButton1_ModalPopupExtender" Enabled="True"
                                                                    TargetControlID="LinkButton1">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                                        </fieldset>
                                    </asp:Panel>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                </div>
                <div id="MainForm">
                    <div id="LeftForm" class="form">
                        <div class="formHeader" align="center">
                            <asp:Label ID="Label1" runat="server" Text="TRANSFER FROM"></asp:Label>
                        </div>
                        <div style="border: solid 1px #88AB2D; height: 325px;">
                            <%-- LIC --%>
                            <div class="controlContainer" style="margin-top: 10px">
                                <div class="leftControl">
                                    <asp:Label ID="lblLICID" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddLIC" runat="server" Width="175px" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddLIC_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Select LIC" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="ddLIC" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Shed --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblShedID" runat="server" Text="Shed" ForeColor="Black"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlShed" runat="server" Width="175px" AutoPostBack="True" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddlShed_SelectedIndexChanged">
                                        <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        ControlToValidate="ddlShed" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Stack No --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblStackNo" runat="server" Text="Stack No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlStackNo" runat="server" Width="175px" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddlStackNo_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Select Stack No" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ControlToValidate="ddlStackNo" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--Symbol --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblSymbol" runat="server" Text="Symbol"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="txtSymbol" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%-- Production Year--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblProductionYear1" runat="server" Text="ProductionYear"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtProductionYear" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--System Count --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label3" runat="server" Text="System Count Bags"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtSystemCount" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--System Weight --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label6" runat="server" Text="System Weight"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtSystemWeight" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--Physical Count--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label4" runat="server" Text="Physical Count Bags"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtPhysicalCount" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtPhysicalCount" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPhysicalCount"
                                        Display="None" ErrorMessage="Please enter valid number." MaximumValue="2147483647"
                                        MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                    <asp:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="RangeValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%-- Physical Weight--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label7" runat="server" Text="Physical Weight"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtPhysicalWeight" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                        ControlToValidate="txtPhysicalWeight" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPhysicalWeight"
                                        Display="None" ErrorMessage="Please enter valid weight." MaximumValue="9223372036854775807"
                                        MinimumValue="0.0000000001" Type="Double"></asp:RangeValidator>
                                    <asp:ValidatorCalloutExtender ID="RangeValidator2_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%-- Stack No --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label18" runat="server" Text="Transfer Reason"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlReason" runat="server" Width="175px" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Reason" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                        ControlToValidate="ddlReason" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 66px; margin-top: 22px; background-image: url('Images/arrow-skip-right-icon.png');
                        height: 32px;">
                    </div>
                    <div id="RightForm" class="form">
                        <div class="formHeader" align="center">
                            <asp:Label ID="Label2" runat="server" Text="TRANSFER TO"></asp:Label>
                        </div>
                        <div style="border: solid 1px #88AB2D; height: 325px;">
                            <%-- LIC --%>
                            <div class="controlContainer" style="margin-top: 10px">
                                <div class="leftControl">
                                    <asp:Label ID="Label8" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddLIC2" runat="server" Width="175px" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddLIC2_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Select LIC" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddLIC2"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Shed --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label9" runat="server" Text="Shed" ForeColor="Black"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlShed2" runat="server" Width="175px" AutoPostBack="True"
                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlShed2_SelectedIndexChanged">
                                        <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlShed2"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Stack No --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label10" runat="server" Text="Stack No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlStackNo2" runat="server" Width="175px" AppendDataBoundItems="True"
                                        OnSelectedIndexChanged="ddlStackNo2_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Text="Select Stack No" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlShed2"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--Symbol --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label11" runat="server" Text="Symbol"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtSymbol2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%-- Production Year--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label12" runat="server" Text="ProductionYear"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtProductionYear2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--System Count --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label13" runat="server" Text="System Count Bags"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtSystemCount2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--System Weight --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label15" runat="server" Text="System Weight"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="170px" ID="txtSystemWeight2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--Physical Count--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label14" runat="server" Text="Physical Count Bags"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtPhysicalCount2" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPhysicalCount2"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPhysicalCount2"
                                        Display="None" ErrorMessage="Please enter valid number." Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator3">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%-- Physical Weight--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label16" runat="server" Text="Physical Weight"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtPhysicalWeight2" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPhysicalWeight2"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtPhysicalWeight2"
                                        Display="None" ErrorMessage="Please enter valid weight." Operator="DataTypeCheck"
                                        Type="Double" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%-- date transfer--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label17" runat="server" Text=" Transfered Date"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtTransferedDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtTransferedDate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtTransferedDate">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTransferedDate"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorDate" ValidationGroup="Save" Type="Date" ControlToValidate="txtTransferedDate"
                                        Display="None" ForeColor="Tomato" runat="server" ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                    <asp:ValidatorCalloutExtender ID="RangeValidatorDate_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="RangeValidatorDate">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="formFooter" id="footer" style="clear: both; width: 100%;" align="center">
                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer Inventory" BorderStyle="None"
                        ForeColor="White" BackColor="#88AB2D" ValidationGroup="Save" OnClick="btnTransfer_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <div>
                <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none; width: 300px;
                    background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;">
                    <div class="formHeader">
                        <asp:Label ID="Label5" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                    </div>
                    <div style="margin: 20px 20px;">
                        <asp:Label ID="configmMessage" runat="server" Text="Are you sure , you want to cancel?"></asp:Label>
                    </div>
                    <div>
                        <div class="controlContainer" style="margin: 20px 20px;">
                            <div style="width: 30%; float: left">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" Width="60px" />
                            </div>
                            <div style="float: left">
                                <asp:Button ID="btnNo" runat="server" Text="No" Width="60px" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
