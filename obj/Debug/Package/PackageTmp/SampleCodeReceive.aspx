<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="SampleCodeReceive.aspx.cs" Inherits="WarehouseApplication.SampleCodeReceive" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .align
        {
            text-align: left;
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
    <div class="container">
        <table style="width: 100%">
            <tr>
                <td class="style1">
                    <uc1:Messages ID="Messages" runat="server" />
                </td>
            </tr>
        </table>
        <div id="sampleCodeReceiveForm" class="form" style="width:55%; padding-top: 10%;
            margin-left: 23%;">
            <div class="formHeader" align="center">
                <asp:Label ID="lblInformation" runat="server" Text="Receive Sample Code"></asp:Label>
            </div>
            <div class="formControlHolders">
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblcode" runat="server" Text="Grading Code :"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:Label ID="lblGradingCodeValue" runat="server"></asp:Label>
                    </div>
                </div>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblDateCoded" runat="server" Text="Date Coded:"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:Label ID="lblDateCodedValue" runat="server"></asp:Label>
                    </div>
                </div>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblSampleCodeReceivedDate" runat="server" Text="Grading Code Received Date: "></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:TextBox ID="txtDateRecived" TabIndex="1" runat="server" ValidationGroup="Save"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtDateRecived">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server"
                            ControlToValidate="txtDateRecived" ErrorMessage="Sample Code Received Date is required"
                            ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                        </cc1:ValidatorCalloutExtender>
                        <%-- <asp:RangeValidator ID="RangeValidator1"  Type="Date" 
                        ControlToValidate="txtDateRecived" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable" ValidationGroup="Save">*</asp:RangeValidator>
                                            <cc1:ValidatorCalloutExtender ID="RangeValidator1_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                            </cc1:ValidatorCalloutExtender>--%>
                    </div>
                </div>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblSampleCodeReceivedTime" runat="server" Text="Grading Code Received Time:"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <%-- <asp:TextBox ID="txtTimeRecived" runat="server" TabIndex="2" ValidationGroup="Save"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTimeRecived"
                        ErrorMessage="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                    </cc1:ValidatorCalloutExtender>--%>
                        <asp:TextBox ID="txtTimeRecived" Width="60px" runat="server" ValidationGroup="Save"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server"
                            ControlToValidate="txtTimeRecived" ErrorMessage="Sample Code Received Time is required"
                            ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                        </cc1:ValidatorCalloutExtender>
                        <cc1:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeRecived">
                        </cc1:MaskedEditExtender>
                        <cc1:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                            ControlToValidate="txtTimeRecived" Display="None" InvalidValueMessage="Please enter a valid time."
                            SetFocusOnError="True" ValidationGroup="Save">*</cc1:MaskedEditValidator>
                        <cc1:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <br />
                <%-- --%>
                <div class="controlContainer" align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnSave_Click" ValidationGroup="Save" />
                            </td>
                            <td>
                                <asp:Button ID="btnNext" runat="server" BackColor="#88AB2D" ForeColor="White" Style="margin-right: 70px;"
                                    Width="100px" BorderStyle="None" Height="20px" Text="Next->>" OnClick="btnNext_Click"
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
