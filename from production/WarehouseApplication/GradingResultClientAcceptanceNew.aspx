<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GradingResultClientAcceptanceNew.aspx.cs"
    Inherits="WarehouseApplication.GradingResultClientAcceptanceNew" Title="Untitled Page" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form
        {
            width: 55%;
            margin-left: 15%;
        }
        .formHeaderl
        {
            padding-left: 25%;
            background-color: #88AB2D;
        }
        .btn
        {
            margin-left: 50px;
        }
        .innerButtonHolder
        {
            margin-left: 27%;
            width: 45%;
            text-align: center;
            margin-bottom: 0px;
        }
        .style1
        {
            font-size: 12;
        }
        .messages-logo
        {
            height: 32px;
            width: 32px;
            float: left;
            background: url(Images/message_logos.png);
        }
        .messages-text
        {
            margin-left: 40px;
            padding: 6px 0;
        }
        .style2
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function dateselectDateOfAcceptance(sender, args) {

//            var GradingReceivedDateValue = document.getElementById('<%= GradingReceivedDatehiden.ClientID %>').value;
//            var DateOfAcceptance = document.getElementById('<%= txtDateOfAcceptance.ClientID %>').value;
//            var AcceptanceDate = new Date(GradingReceivedDateValue);
//            var ReceivedDate = new Date(DateOfAcceptance);


//            if (AcceptanceDate.getDate() < ReceivedDate.getDate()) {
//                alert("Date Of Acceptance  is less  than Grading Received Date");
//                document.getElementById('<%= txtDateOfAcceptance.ClientID %>').value = "";
//            }
        }
    </script>
    <asp:HiddenField ID="GradingReceivedDatehiden" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="txtId" runat="server" />
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td class="style1">
                            <uc1:Messages ID="Messages" runat="server" />
                        </td>
                    </tr>
                </table>
                <div id="gradingResultForm" class="form" style="width: 75%; padding-top: 10%; float: left;">
                    <div class="formHeader" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Client Grading Result Response"></asp:Label>
                    </div>
                    <div class="formControlHolders">
                        <%--GRADING CODE--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGradingCode" runat="server" Text="Grading Code"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblGradeCode" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <%--CLEINT--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblClient" runat="server" Text="Client"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblClientValue" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--COMMODITY GRADE--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblCommodityGradeValue" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                         <%--Production Year--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblProductionYear" runat="server" Text="Production Year"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblProductionYearValue" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--GRADING RESUTL STATUS--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGradingResultStatus" runat="server" Text="Grading Result Status"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="cboGradingRecivedStatus" runat="server" Width="250px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--Segrigation No (if grading result status value is Segrigation Requested)--%>
                        <asp:Panel runat="server" ID="pnlSegrigationNo" Visible="true">
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblSegrigationNo" runat="server" Text="Segrigation No"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox ID="txtSegrigationNo" runat="server" Width="250px" ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--STATUS--%>
                        <asp:Panel runat="server" ID="pnlSegregationRequested" Visible="true">
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="cboAcceptanceStatus" Width="250" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboAcceptanceStatus_SelectedIndexChanged">
                                        <asp:ListItem>Please Select Status</asp:ListItem>
                                        <asp:ListItem Value="3">Accepted By Client</asp:ListItem>
                                        <asp:ListItem Value="4">Rejected By Client</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--Shed--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblShed" runat="server" Text="Shed"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="drpShed" Width="250" runat="server" OnSelectedIndexChanged="drpShed_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCashReceiptNo" Visible="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--LIC--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblLIC" runat="server" Text="LIC"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="drpLIC" Width="250" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAmount" Visible="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--Security Marshal--%>
                            <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="lblSecurityMarshal" runat="server" Text="Security Marshal"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:DropDownList ID="drpSecurityMarshal" Width="250" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--GRADING RECEIVED DATE--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGradingReceivedDate" runat="server" Text="Grading Received Date"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblGradingReceivedDateValue" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <%--DATE OF ACCEPTANCE--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblDateofAcceptance" runat="server" Text="Date of Acceptance"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtDateOfAcceptance" runat="server" Width="250px"></asp:TextBox>
                                <asp:CompareValidator ControlToValidate="txtDateOfAcceptance"  
                ID="cmpSampGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Cleint response date is less than the Grade received Date" 
                 Type="Date" ValidationGroup="Save" Operator="GreaterThanEqual"></asp:CompareValidator>
                                <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtDateOfAcceptance" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
                                <ajaxToolkit:CalendarExtender ID="txtDateOfAcceptance_CalendarExtender" runat="server"
                                    Enabled="True" TargetControlID="txtDateOfAcceptance" BehaviorID="CalanderDateOfAcceptance"
                                    OnClientDateSelectionChanged="dateselectDateOfAcceptance">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <%--TIME OF ACCEPTANCE--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblTimeofAcceptance" runat="server" Text="Time of Acceptance"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtTimeodAcceptance" runat="server" Width="250px"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="True"
                                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeodAcceptance">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender4"
                                    ControlToValidate="txtTimeodAcceptance" InvalidValueMessage="Please enter valid time."></ajaxToolkit:MaskedEditValidator>
                            </div>
                        </div>
                        <%--***--%>
                        <div>
                            <center>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button  ID="btnPrint" runat="server" Text="Print" CausesValidation="false"
                                                Width="100px" Height="20px" BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC"
                                                OnClick="btnPrint_Click" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button  ID="btnUpdate" runat="server" Text="Update"  CausesValidation="false"
                                                Width="100px" Height="20px" BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC"
                                                OnClick="btnUpdate_Click" />
                                        </td>
                                        <td>
                                        <asp:Button ID="btnNext" runat="server" BackColor="#88AB2D" ForeColor="White" 
                                        Width="100px" BorderStyle="None" Height="20px" Text="Next->>" 
                                        OnClick="btnNext_Click" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
