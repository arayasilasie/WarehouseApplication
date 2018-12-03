<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GRNCancellationRequest.aspx.cs" Inherits="WarehouseApplication.GRNCancellationRequest" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            height: 20px;
        }
        .form
        {
            width: 45%;
        }
        
        .leftControl
        {
            margin-left: 10px;
            margin-right: 10px;
            width: 36%;
            float: left;
        }
        .rightControl
        {
            float: left;
            width: 55%;
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
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <uc1:Messages ID="Messages1" runat="server" />
                <br />
                <div style="margin-left: 40px; width: 70%;">
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
                                            <div id="Div1" class="form" style="float: left; margin-right: 50px; margin-top: 0;
                                                margin-bottom: 10px">
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label4" runat="server" Text="GRN"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:TextBox ID="txtGRN" runat="server" Width="120px"></asp:TextBox>
                                                    </div>
                                                </div>
                                              
                                                <div class="controlContainer" style="margin-top: 10px; margin-left">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:DropDownList ID="ddStatus" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                            Width="123px">
                                                            <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                                            <asp:ListItem Value="1">Requested</asp:ListItem>
                                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                    </div>
                                                    <div class="rightControl">
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="form" style="float: left; margin-bottom: 10px;">
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label5" runat="server" Text="Date Requsted"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:TextBox ID="txtDateIssued" runat="server" ValidationGroup="Search" Width="65px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDateIssued_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtDateIssued">
                                                        </asp:CalendarExtender>
                                                        &nbsp;-
                                                        <asp:TextBox ID="txtDateIssued2" runat="server" ValidationGroup="Search" Width="65px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDateIssued2_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtDateIssued2">
                                                        </asp:CalendarExtender>
                                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtDateIssued2"
                                                            Display="Dynamic" ErrorMessage="!" Operator="DataTypeCheck" Type="Date" ValidationGroup="Search"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtDateIssued"
                                                            Display="Dynamic" ErrorMessage="!" Operator="DataTypeCheck" Type="Date" ValidationGroup="Search"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txtDateIssued2"
                                                            ControlToValidate="txtDateIssued" Display="Dynamic" ErrorMessage="!" Operator="LessThanEqual"
                                                            Type="Date" ValidationGroup="Search"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                        <asp:Label ID="Label7" runat="server" Text="Date Replayed"></asp:Label>
                                                    </div>
                                                    <div class="rightControl">
                                                        <asp:TextBox ID="txtDateApproved" runat="server" ValidationGroup="Search" Width="65px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDateApproved_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtDateApproved">
                                                        </asp:CalendarExtender>
                                                        &nbsp;-
                                                        <asp:TextBox ID="txtDateApproved2" runat="server" ValidationGroup="Search" Width="65px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDateApproved2_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtDateApproved2">
                                                        </asp:CalendarExtender>
                                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtDateApproved2"
                                                            Display="Dynamic" ErrorMessage="!" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtDateApproved"
                                                            Display="Dynamic" ErrorMessage="!" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToCompare="txtDateApproved2"
                                                            ControlToValidate="txtDateApproved" Display="Dynamic" ErrorMessage="!" Operator="LessThanEqual"
                                                            Type="Date"></asp:CompareValidator>
                                                    </div>
                                                </div>
                                             
                                                <div class="controlContainer">
                                                    <div class="leftControl">
                                                    </div>
                                                    <div class="rightControl" style="float: right;">
                                                        <asp:Button ID="btnSrch" runat="server" BackColor="#88AB2D" BorderStyle="None" CssClass="style1"
                                                            ForeColor="White" OnClick="btnSrch_Click" Text="Search" ValidationGroup="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="clear: both; margin: 1px;">
                                                <asp:GridView Width="100%" ID="grvSearch" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="4" DataKeyNames="ID" GridLines="Vertical" Style="font-size: small"
                                                    CssClass="label" AllowPaging="True" 
                                                    onpageindexchanging="grvSearch_PageIndexChanging" 
                                                    onrowdatabound="grvSearch_RowDataBound">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text="There is no data to display." /></EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="GRNNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGRNNo" runat="server" Text='<%# Bind("GRN_Number") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date Requested">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateRequested" runat="server" Text='<%# Bind("DateRequested") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date Replayed">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprovedTimeStamp" runat="server" Text='<%# Bind("ApprovedTimeStamp") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Right" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="RejectionReason" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRejectionReason" runat="server" Text='<%# Bind("RejectionReason") %>'></asp:Label>
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
                <br />
                <div id="Header" class="formHeader" style="width: 70%; margin-left: 40px; margin-top: 5px;"
                    align="center">
                    <asp:Label ID="lblDetail" Text="GRN Cancellation Request" Width="100%" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 70%; margin-left: 40px;">
                    <div style="margin-bottom: 10px;">
                        <div style="border: solid 1px #88AB2D; height: 50px;">
                            <div style="margin-top: 15px; float: left; height: 26px; margin-left: 10px;">
                                <div>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="GRN No :"></asp:Label>
                                </div>
                            </div>
                            <div style="margin-top: 15px; margin-left: 20px; float: left;">
                                <div>
                                    <asp:TextBox ID="txtGRNNo" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 15px; float: left; margin-left: 30px;">
                                <div style="height: 26px;">
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; width: 70%; margin-left: 40px; margin-bottom: 10px;">

                    <asp:GridView Width="100%" ID="grvGRNCancellation" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" DataKeyNames="ID" GridLines="Vertical" Style="font-size: small"
                        CssClass="label" AllowPaging="True">
                        <EmptyDataTemplate>
                            <asp:Label ID="lbl" runat="server" Text="No GRN to Cancel." /></EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="GRNNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblGRNNo" runat="server" Text='<%# Bind("GRN_Number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblClientID" runat="server" Text='<%# Bind("ClientID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client">
                                <ItemTemplate>
                                    <asp:Label ID="lblClient" runat="server" Text='<%# Bind("Client") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="Weight" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWeight" runat="server" Text='<%# Bind("NetWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shed">
                                <ItemTemplate>
                                    <asp:Label ID="lblShedNumberr" runat="server" Text='<%# Bind("ShedNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DateTimeLICSigned" HeaderText=" Approved Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedDat" runat="server" Text='<%# Bind("ApprovedDateTime") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StackID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStackID" runat="server" Text='<%# Bind("StackID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkClose" runat="server" CausesValidation="false" CommandName="Select"
                                        Text="Cancel" OnClick="lnkClose_Click"></asp:LinkButton>
                                    <asp:ConfirmButtonExtender ID="lnkClose_ConfirmButtonExtender" runat="server" ConfirmText=""
                                        Enabled="True" TargetControlID="lnkClose" DisplayModalPopupID="lnkClose_ModalPopupExtender">
                                    </asp:ConfirmButtonExtender>
                                    <asp:ModalPopupExtender ID="lnkClose_ModalPopupExtender" runat="server" DynamicServicePath=""
                                        Enabled="True" TargetControlID="lnkClose" CancelControlID="btnNo2" OkControlID="btnYes2"
                                        PopupControlID="pnlConfirmation2" PopupDragHandleControlID="pnlheader" BackgroundCssClass="modalBackground">
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
                <div style="clear: both; width: 70%; margin-left: 40px;">
                    <asp:Panel ID="pnlReason44" runat="server" Visible="False" >
                        <div style="float: left; width: 100%; height: 110px;">
                            <div style="margin-bottom: 10px;">
                                <div style="border-bottom: solid 1px #adba83; height: 80px;">
                                    <div>
                                        <div style="margin-top: 15px; float: left; height: 26px; margin-left: 10px;">
                                            <div>
                                                <asp:Label ID="Label3" runat="server" Text="Reason :"></asp:Label>
                                            </div>
                                        </div>
                                        <div style="margin-top: 15px; margin-left: 20px; float: left;">
                                            <div>
                                                <asp:TextBox ID="txtReason" runat="server" ValidationGroup="Find" TextMode="MultiLine"
                                                    Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReason"
                                                    Display="Dynamic" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div style="margin-top: 15px; float: left; margin-left: 10px;">
                                            <div style="height: 26px;">
                                                <asp:Button ID="btnSave" runat="server" BackColor="#88AB2D" BorderStyle="None" ForeColor="#F7FFDD"
                                                    Text="Save" ValidationGroup="Save" Width="70px" OnClick="btnSave_Click" Style="height: 22px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div>
                <asp:Panel ID="pnlConfirmation2" runat="server" Style="width: 280px; background-color: White;
                    display: none">
                    <div style="border-width: 2px; border-style: solid; border-color: #FFCC00;">
                        <div style="margin: 20px 20px;">
                            <asp:Label ID="configmMessage2" runat="server" Text="Are you sure, you want to Cancel?"></asp:Label>
                        </div>
                        <div style="margin: 20px 20px;">
                            <div class="controlContainer" style="margin: 15px 50px;">
                                <div style="width: 50%; float: left">
                                    <asp:Button ID="btnYes2" runat="server" Text="Yes" Width="60px" />
                                </div>
                                <div style="float: left">
                                    <asp:Button ID="btnNo2" runat="server" Text="No" Width="60px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
