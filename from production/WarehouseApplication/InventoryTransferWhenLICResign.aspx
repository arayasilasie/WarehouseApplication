<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="InventoryTransferWhenLICResign.aspx.cs" Inherits="WarehouseApplication.InventoryTransferWhenLICResign" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <br />
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <div style="margin-left: 65px;">
                    <asp:Accordion ID="Accordion1" SuppressHeaderPostbacks="true" runat="server" FramesPerSecond="40"
                        RequireOpenedPane="false" TransitionDuration="250" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" Width="91%">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane1" runat="server">
                                <Header>
                                </Header>
                                <Content>
                                    <asp:Panel ID="PanelSearch" runat="server">
                                        <fieldset style="width: 44%;">
                                            <div id="Div3" class="form" style="float: left; margin-top: 0; margin-bottom: 10px;
                                                height: 43px; width: 77%;">
                                                <div class="controlContainer" style="margin-top: 5px;">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label20" runat="server" Text="Transfer Date"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:TextBox ID="txtTrannsferDateSrch" runat="server" Width="145px" ValidationGroup="Search"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtTrannsferDateSrch_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtTrannsferDateSrch">
                                                        </asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Search"
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
                                            <div style="float: left; margin-bottom: 10px;">
                                                <%-- Search --%>
                                                <div style="margin-top: 5px; height: 22px;">
                                                    <div class="leftControl">
                                                        <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                                            CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" ValidationGroup="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; margin: 10px;">
                                                <asp:GridView ID="grvInvTransferSearch" runat="server" AllowPaging="True" BorderColor="White"
                                                    CellPadding="4" Style="font-size: small" CssClass="label" CellSpacing="1" GridLines="None"
                                                    PageSize="5" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanged="grvInvTransferSearch_SelectedIndexChanged">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text="No inventory transfered."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText=" LIC From" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LIC To" HeaderStyle-Font-Bold="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLIC2" runat="server" Text='<%# Bind("LIC2") %>'></asp:Label>
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
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Select"
                                                                    Text="Cancel"></asp:LinkButton>
                                                                <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" CancelControlID="btnNo"
                                                                    DynamicServicePath="" Enabled="True" OkControlID="btnYes" PopupControlID="pnlConfirmation"
                                                                    TargetControlID="LinkButton2">
                                                                </asp:ModalPopupExtender>
                                                                <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server"
                                                                    ConfirmText="" DisplayModalPopupID="LinkButton1_ModalPopupExtender" Enabled="True"
                                                                    TargetControlID="LinkButton2">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#DAE1CC" />
                                                    <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                                        BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" />
                                                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
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
                <div style="width: 95%; height: 125px; margin-left: 65px;">
                    <div id="Div1" class="form" style="float: left;">
                        <div class="formHeader" align="center">
                            <asp:Label ID="Label1" runat="server" Text="TRANSFER FROM"></asp:Label>
                        </div>
                        <div style="border: solid 1px #88AB2D; height: 80px;">
                            <div class="controlContainer" style="margin-top: 10px;">
                                <div class="leftControl">
                                    <asp:Label ID="Label4" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddLIC" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                        Width="175px" OnSelectedIndexChanged="ddLIC_SelectedIndexChanged" ValidationGroup="Save">
                                        <asp:ListItem Text="Select LIC" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddLIC"
                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label5" runat="server" Text="Date Transfered"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox ID="txtTransferDate" runat="server" Width="168px" ValidationGroup="Save"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTransferDate"
                                        ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:CalendarExtender ID="txtTransferDate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtTransferDate">
                                    </asp:CalendarExtender>
                                    <asp:RangeValidator ID="RangeValidatorDate" ValidationGroup="Save" Type="Date" ControlToValidate="txtTransferDate"
                                        Display="None" ForeColor="Tomato" runat="server" ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                    <asp:ValidatorCalloutExtender ID="RangeValidatorDate_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="RangeValidatorDate">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="float: left; width: 68px; margin-top: 22px; margin-left: 5px; margin-right: 5px;
                        background-image: url('Images/arrow-skip-right-icon.png'); height: 32px;">
                    </div>
                    <div id="Div2" class="form" style="float: left;">
                        <div class="formHeader" align="center">
                            <asp:Label ID="Label2" runat="server" Text="TRANSFER TO"></asp:Label>
                        </div>
                        <div style="border: solid 1px #88AB2D; height: 80px;">
                            <div class="controlContainer" style="margin-top: 10px;">
                                <div class="leftControl">
                                    <asp:Label ID="Label3" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddLIC2" runat="server" AppendDataBoundItems="True" Width="175px"
                                        ValidationGroup="Save">
                                        <asp:ListItem Text="Select LIC" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddLIC2"
                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="controlContainer">
                                <div class="leftControl">
                                </div>
                                <div class="rightControl" style="text-align: right; float: left; width: 43%;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="width: 96%; clear: both; margin-top: 10px;" align="center">
                    <asp:Button ID="btnTransfer" runat="server" BackColor="#88AB2D" BorderStyle="None"
                        CssClass="style1" ForeColor="White" Text="Transfer" ValidationGroup="Save" OnClick="btnTransfer_Click" />
                </div>
                <div style="margin-top: 10px; clear: both; margin-left: 65px;">
                    <asp:GridView ID="grvInvTransferLICResign" runat="server" AutoGenerateColumns="False"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ID"
                        Style="font-size: small" Width="85%" CssClass="label"  OnPageIndexChanging="grvInvTransferLICResign_PageIndexChanging"
                        PageSize="30">
                        <Columns>
                            <asp:TemplateField HeaderText="Symbol">
                                <ItemTemplate>
                                    <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblShedID" runat="server" Text='<%# Bind("ShedID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shed">
                                <ItemTemplate>
                                    <asp:Label ID="lblShed" runat="server" Text='<%# Bind("Shed") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemCount" runat="server" Text='<%# Bind("CurrentBalance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.Weight">
                                <ItemTemplate>
                                    <asp:Label ID="lblSystemWeigh" runat="server" Text='<%# Bind("CurrentWeight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Count">
                                <ItemTemplate>
                                    <asp:TextBox Width="80px" ID="txtPhysicalCount" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtPhysicalCount"
                                        ValidationGroup="Save" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter valid number."
                                        Display="None" ControlToValidate="txtPhysicalCount" ValidationGroup="Save"
                                        Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Weight">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPhysicalWeight" runat="server" Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="Save"
                                        ControlToValidate="txtPhysicalWeight" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid weight."
                                        Display="None" ControlToValidate="txtPhysicalWeight" ValidationGroup="Save"
                                        Type="Double" Operator="DataTypeCheck"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#DAE1CC" />
                        <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                            BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                <div>
                    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none; width: 300px;
                        background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;">
                        <div class="formHeader">
                            <asp:Label ID="Label6" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
