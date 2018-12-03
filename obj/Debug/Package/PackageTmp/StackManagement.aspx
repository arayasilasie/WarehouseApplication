<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="StackManagement.aspx.cs" Inherits="WarehouseApplication.StackManagement" %>

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
            height: 325px;
            margin-left: 100px;
            width: 80%;
        }
        #LeftForm
        {
            float: left; /* margin-left: 65px;*/
            margin-right: 35px;
            margin-top: 0;
        }
        #RightForm
        {
            float: Left;
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
            height: 22px;
        }
        
        .form2
        {
            width: 47%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
               <br />
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <%--  <br />--%>
                <div style="margin-left: 100px; width: 80%;">
                    <asp:Accordion ID="Accordion1" SuppressHeaderPostbacks="true" runat="server" FramesPerSecond="40"
                        RequireOpenedPane="false" TransitionDuration="250" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected">
                        <Panes>
                            <asp:AccordionPane ID="AccordionPane1" runat="server">
                                <Header>
                                </Header>
                                <Content>
                                    <asp:Panel ID="PanelSearch3" runat="server">
                                        <fieldset>
                                            <div id="Div1" class="form" style="float: left; margin-right: 100px; margin-top: 0;
                                                margin-bottom: 10px">
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label1" runat="server" Text="Shed"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:DropDownList ID="ddlShedSearch" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlShedSearch_SelectedIndexChanged" Width="175px">
                                                            <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlShedSearch"
                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <%--  --%>
                                                <div class="controlContainer" style="margin-top: 10px; margin-left">
                                                    <div class="leftControl">
                                                    </div>
                                                    <div class="rightControl">
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="form" style="float: left; margin-bottom: 10px;">
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label4" runat="server" Text="Physical Address"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:DropDownList ID="ddlPhyAddressSearch" runat="server" AppendDataBoundItems="True"
                                                            Width="175px">
                                                            <asp:ListItem Text="Select Physical Address" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlPhyAddressSearch"
                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <%-- Search --%>
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                    </div>
                                                    <div class="rightControl" style="float: right;">
                                                        <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                                            CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" ValidationGroup="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; margin:1px;">
                                                <asp:GridView ID="grvStacks" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                                                    GridLines="None" PageSize="5" AutoGenerateColumns="False" OnRowDataBound="grvStacks_RowDataBound"
                                                    OnSelectedIndexChanged="grvStacks_SelectedIndexChanged" DataKeyNames="ID" OnPageIndexChanging="grvStacks_PageIndexChanging">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text="No stack available."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="LIC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLIC" Text='<%# Eval("LIC") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shed">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblShedNumber" Text='<%# Eval("ShedNumber") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PhysicalAddress" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhysicalAddress" Text='<%# Eval("PhysicalAddress") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Symbol">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSymbol" Text='<%# Eval("Symbol") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="P.Year">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPYear" Text='<%# Eval("ProductionYear") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> <asp:TemplateField HeaderText="BagType">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBag" Text='<%# Eval("Bag") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="B.Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBeginingBalance" Text='<%# Eval("BeginingBalance") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C.Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurrentBalance" Text='<%# Eval("CurrentBalance") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="B.Weight">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBeginingWeight" Text='<%# Eval("BeginingWeight") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="C.Weight">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurrentWeight" Text='<%# Eval("CurrentWeight") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DateStarted">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateStarted" Text='<%# Eval("DateStarted") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Staus" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStaus" Text='<%# Eval("Status") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkCancel"  runat="server" CausesValidation="False" CommandName="Select"
                                                                    Text="Cancel" onclick="lnkCancel_Click"></asp:LinkButton>
                                                                <asp:ConfirmButtonExtender ID="lnkCancel_ConfirmButtonExtender" runat="server"
                                                                    ConfirmText="" Enabled="True" TargetControlID="lnkCancel" 
                                                                    DisplayModalPopupID="lnkCancel_ModalPopupExtender">
                                                                </asp:ConfirmButtonExtender>
                                                                <asp:ModalPopupExtender ID="lnkCancel_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                                    Enabled="True" TargetControlID="lnkCancel" CancelControlID="btnNo" OkControlID="btnYes"
                                                                    PopupControlID="pnlConfirmation">
                                                                </asp:ModalPopupExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkClose" runat="server" CausesValidation="false" 
                                                                    CommandName="Select" Text="Close" onclick="lnkClose_Click"></asp:LinkButton>
                                                               <asp:ConfirmButtonExtender ID="lnkClose_ConfirmButtonExtender" runat="server"
                                                                    ConfirmText="" Enabled="True" TargetControlID="lnkClose" 
                                                                    DisplayModalPopupID="lnkClose_ModalPopupExtender">
                                                                </asp:ConfirmButtonExtender>
                                                                <asp:ModalPopupExtender ID="lnkClose_ModalPopupExtender" runat="server" 
                                                                    DynamicServicePath="" Enabled="True" TargetControlID="lnkClose"
                                                                     CancelControlID="btnNo2" OkControlID="btnYes2" PopupControlID="pnlConfirmation2">
                                                                </asp:ModalPopupExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="#e4efd0" />
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                </div>
                <div style="margin-left: 90px; margin-bottom: 20;">
                </div>
                <div id="Header" class="formHeader" align="center" style="margin-left: 100px; width: 80%;">
                    <asp:Label ID="Label3" runat="server" Text="STACK MANAGEMENT"></asp:Label>
                </div>
                <div id="MainForm">
                    <div style="border: solid 1px #88AB2D; height: 230px">
                        <div id="LeftForm" class="form2">
                            <%-- LIC --%>
                            <div class="controlContainer" style="margin-top: 10px">
                                <div class="leftControl">
                                    <asp:Label ID="lblLICID" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddLIC" runat="server" Width="170px" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddLIC_SelectedIndexChanged">
                                        <asp:ListItem Text="Select LIC" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="ddLIC" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Shed --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblShedID" runat="server" Text="Shed" ForeColor="Black"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlShedID" runat="server" Width="170px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlShedID_SelectedIndexChanged" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlShedID" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Physical Address --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblPhysicalAddress" runat="server" Text="Physical Address"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlPhysicalAddress" runat="server" Width="170px" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Physical Address" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="ddlPhysicalAddress" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Stack Number--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lbStackNo" runat="server" Text="Stack No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label Width="165px" ID="txtStackNo" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--Date Started --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblDateStarted" runat="server" Text="Date Started"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="165px" ID="txtDateStarted" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtDateStarted_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtDateStarted">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator12"
                                        runat="server" ControlToValidate="txtDateStarted" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorDateStarted" ValidationGroup="Save" Type="Date"
                                        ControlToValidate="txtDateStarted" Display="None" ForeColor="Tomato" runat="server"
                                        ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                    <asp:ValidatorCalloutExtender ID="RangeValidatorDateStarted_ValidatorCalloutExtender"
                                        runat="server" Enabled="True" TargetControlID="RangeValidatorDateStarted">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%-- Begining Weight--%>
                            <div class="controlContainer" style="height: 40px;">
                                <div class="leftControl">
                                    <asp:Label ID="lblBeginingWeight" runat="server" Text="Begining Weight Kg"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="165px" ID="txtBeginingWeight" runat="server">0</asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator10"
                                        runat="server" ControlToValidate="txtBeginingWeight" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ValidationGroup="Save" ID="CompareValidator2" runat="server"
                                        ControlToValidate="txtBeginingWeight" Display="None" ErrorMessage="Please enter valid weight."
                                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                        <div id="RightForm" class="form2">
                            <%-- Commodity --%>
                            <div class="controlContainer" style="margin-top: 10px">
                                <div class="leftControl">
                                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity  "></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlCommodity" runat="server" Width="170px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Commodity" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlCommodity" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- CommodityClass  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblCommodityClass" runat="server" Text="Commodity Class " ForeColor="Black"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlCommodityClass" runat="server" Width="170px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCommodityClass_SelectedIndexChanged" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Commodity Class" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="ddlCommodityClass" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Commodity Grade --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Symbol"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlCommodityGrade" runat="server" Width="170px" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Comodity Symbol" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="ddlCommodityGrade" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Production Year --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblProductionYear" runat="server" Text="Production Year"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlProductionYear" runat="server" Width="170px" AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select Production Year" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="ddlProductionYear" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Bag Type --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblBagType" runat="server" Text="Bag Type"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlBagType" runat="server" Width="170px" AppendDataBoundItems="True">
                                        <asp:ListItem Value="" Text="Select Bag Type"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlBagType" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--Begining Balance--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblBeginingBalance" runat="server" Text="Begining Balance Bags"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="165px" ID="txtBeginingBalance" runat="server">0</asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="txtBeginingBalance" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ValidationGroup="Save" ID="CompareValidator3" runat="server"
                                        ControlToValidate="txtBeginingBalance" Display="None" ErrorMessage="Please enter a valid number"
                                        Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator3">
                                    </asp:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </div>
                        <div style="clear: both; margin-bottom: 10px; width: 100%" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="Add Stack" OnClick="btnSave_Click"
                                BorderStyle="None" CssClass="style1" ForeColor="White" BackColor="#88AB2D" ValidationGroup="Save" />
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none; width: 300px;
                    background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;">
                    <div class="formHeader">
                        <asp:Label ID="Label5" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                    </div>
                    <div style="margin: 20px 20px;">
                        <asp:Label ID="configmMessage" runat="server" Text="Are you sure , You want to Cancel"></asp:Label>
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

             <div>
                <asp:Panel ID="pnlConfirmation2" runat="server" Style="display: none; width: 300px;
                    background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;">
                    <div class="formHeader">
                        <asp:Label ID="Label2" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                    </div>
                    <div style="margin: 20px 20px;">
                        <asp:Label ID="configmMessage2" runat="server" Text="Are you sure , You want to Close"></asp:Label>
                    </div>
                    <div>
                         <div class="controlContainer" style="margin: 20px 20px;">
                            <div style="width: 30%; float: left">
                                <asp:Button ID="btnYes2" runat="server" Text="Yes" Width="60px" />
                            </div>
                            <div style="float: left">
                                <asp:Button ID="btnNo2" runat="server" Text="No" Width="60px" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
