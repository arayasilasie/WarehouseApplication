<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="AddPhysicalAdddress.aspx.cs" Inherits="WarehouseApplication.AddPhysicalAdddress" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <br />
                    <div>
                        <uc1:Messages ID="Messages1" runat="server" />
                    </div>
                    <div style="margin-left: 25%; width: 43%;">
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
                                                <div id="Div2" style="width: 60%; float: left; margin-top: 0; margin-bottom: 10px;">
                                                    <div class="controlContainer">
                                                        <div class="leftControl" style="width: 30%;">
                                                            <asp:Label ID="Label9" runat="server" Text="Shed"></asp:Label>
                                                        </div>
                                                        <div class="rightControl" style="width: 60%;">
                                                            <asp:DropDownList ID="ddlShedSearch" runat="server" AppendDataBoundItems="True" Width="160px">
                                                                <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlShedSearch"
                                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <%--  --%>
                                                </div>
                                                <div id="Div3" style="width: 35%; float: left; margin-bottom: 10px;">
                                                    <div class="controlContainer">
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                                                CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" ValidationGroup="Search" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="clear: both; margin: 10px;">
                                                    <asp:GridView ID="grvPhysicalAddress" runat="server" AllowPaging="True" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" PageSize="5" AutoGenerateColumns="False"
                                                        DataKeyNames="ID" OnPageIndexChanging="grvPhysicalAddress_PageIndexChanging"
                                                        OnSelectedIndexChanged="grvPhysicalAddress_SelectedIndexChanged">
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lbl" runat="server" Text="No phsical address available."></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Shed">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblShedNumber" Text='<%# Eval("ShedNumber") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPhysicalAddress" Text='<%# Eval("AddressName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Row">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRow" Text='<%# Eval("Row") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Col.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblColumun" Text='<%# Eval("Columun") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Width">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWidth" Text='<%# Eval("Width") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Height">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeight" Text='<%# Eval("Height") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Length">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLengtht" Text='<%# Eval("Length") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--     <asp:TemplateField HeaderText="Capacity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaximumSize" Text='<%# Eval("MaximumSize") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Staus" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStaus" Text='<%# Eval("Status") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                                        Text="Cancel"></asp:LinkButton>
                                                                    <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server"
                                                                        ConfirmText="" Enabled="True" TargetControlID="LinkButton1" DisplayModalPopupID="LinkButton1_ModalPopupExtender">
                                                                    </asp:ConfirmButtonExtender>
                                                                    <asp:ModalPopupExtender ID="LinkButton1_ModalPopupExtender" runat="server" DynamicServicePath=""
                                                                        Enabled="True" TargetControlID="LinkButton1" CancelControlID="btnNo" OkControlID="btnYes"
                                                                        PopupControlID="pnlConfirmation">
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
                    <div id="Div1" class="form" style="float: left; margin-left: 10pX; margin-top: 10pX;
                        height: 290px; margin-left: 25%;">
                        <div class="formHeader" align="center">
                            <asp:Label ID="Label1" runat="server" Text="PHYSICAL ADDRESS"></asp:Label>
                        </div>
                        <div style="border: solid 1px #999933; height: inherit; background-color: White;">
                            <%-- Shed --%>
                            <div class="controlContainer" style="margin-top: 10px;">
                                <div class="leftControl">
                                    <asp:Label ID="Label4" runat="server" Text="Shed"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="ddlShed" runat="server" AppendDataBoundItems="True" Width="175px">
                                        <asp:ListItem Text="Select Shed" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlShed"
                                        Display="Dynamic" ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- address --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtAddress" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtAddress" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%-- Row  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label6" runat="server" Text="Row"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtRow" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtRow" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtRow"
                                        Display="None" ErrorMessage="Please enter a valid number." Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="Save"></asp:CompareValidator>--%>
                                  <%--  <asp:ValidatorCalloutExtender ID="CompareValidator5_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator5">
                                    </asp:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <%-- Column  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label3" runat="server" Text="Column"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtColumn" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtColumn" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                   <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtColumn"
                                        Display="None" ErrorMessage="Please enter a valid number." Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="Save"></asp:CompareValidator>--%>
                                 <%--   <asp:ValidatorCalloutExtender ID="CompareValidator4_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator4">
                                    </asp:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <%-- width  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label5" runat="server" Text="Width"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtWidth" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtWidth" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <%--    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtWidth"
                                        Display="None" ErrorMessage="Please enter a valid number." Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator3_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator3">
                                    </asp:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <%-- Length  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label7" runat="server" Text="Length"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtLength" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtLength" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <%--    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtLength"
                                        Display="None" ErrorMessage="Please enter a valid number." Operator="DataTypeCheck"
                                        Type="Integer" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator2_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator2">
                                    </asp:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <%-- Height  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label8" runat="server" Text="Height"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtHeight" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtHeight" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" ErrorMessage="Please enter a valid number."
                                        Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtHeight" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="CompareValidator1_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator1">
                                    </asp:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <%-- Maximum Size  --%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label09" runat="server" Text="MaximumSize"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtMaximumSize" runat="server"></asp:TextBox>
                               
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ErrorMessage="*" ControlToValidate="txtMaximumSize" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" Display="None" ErrorMessage="Please enter a valid number."
                                        Operator="DataTypeCheck" Type="Double" ControlToValidate="txtMaximumSize" ValidationGroup="Save"></asp:CompareValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                        Enabled="True" TargetControlID="CompareValidator6">
                                    </asp:ValidatorCalloutExtender>
                                     </div>
                            </div>
                            <%--   --%>
                            <div class="controlContainer" style="margin-top: 10px;" align="center">
                                <asp:Button ID="btnAdd" runat="server" BackColor="#88AB2D" BorderStyle="None" ForeColor="White"
                                    Text="Add" Width="70px" OnClick="btnAdd_Click" ValidationGroup="Save" />
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none; width: 300px;
                            background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;">
                            <div class="formHeader">
                                <asp:Label ID="Label10" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
