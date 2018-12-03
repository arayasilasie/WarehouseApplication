<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GIN.aspx.cs"
    Inherits="WarehouseApplication.GIN" Title="GIN" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    
  
    <div>
        <div>
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="true" Text="Click here"
                OnClick="LinkButton1_Click" ForeColor="#CCFF99" />
            <asp:Panel ID="Panel1" runat="server" Style="display: none" BackColor="White">
                <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: White;
                    border: solid 1px Gray; color: Black" Height="50">
                    <div>
                        <br />
                        <p>
                            you are changing the LIC it will clear the Stack</p>
                    </div>
                </asp:Panel>
                <div>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="LinkButton1"
                PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="CancelButton"
                DropShadow="true" PopupDragHandleControlID="Panel3" />
            <br />
        </div>
    </div>
    <script type="text/javascript">



        function dateselect(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                var textBox2 = document.getElementById('<%= txtDateLoaded.ClientID %>');

                textBox2.value = ''
            }
            else {
                var calendarBehavior1 = $find("CalanderDateLoaded");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy");
            }

        }
        function ValueChanged(chk) {


            $.blockUI({ message: $('#divMessage') });
        }
        function dateselectDateWeighed(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                var textBox2 = document.getElementById('<%= txtDateWeighted.ClientID %>');

                textBox2.value = ''
            }
            else {
                var calendarBehavior1 = $find("CalanderDateWeighed");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy");
            }

        }
        function dateselectTruckRequestTime(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                var textBox2 = document.getElementById('<%= txtDNSubmitedTime.ClientID %>');

                textBox2.value = ''
            }
            else {
                var calendarBehavior1 = $find("CalanderTruckRequestTime");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy")
            }

        }
        function dateselectAvailableTime(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                var textBox2 = document.getElementById('<%= txtAvailableTime.ClientID %>');

                textBox2.value = ''
            }
            else {
                var calendarBehavior1 = $find("CalanderAvailableTime");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy");
            }

        }
        //        function dateselectDateWeighted(sender, args) {
        //            if (sender._selectedDate > new Date()) {
        //                alert("You cannot select future dates!");
        //                sender._selectedDate = new Date();
        //                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

        //                var textBox2 = document.getElementById('<%= txtDateWeighted.ClientID %>');

        //                textBox2.value = ''
        //            }
        //            else {
        //                var calendarBehavior1 = $find("CalanderDateWeighted");
        //                var d = calendarBehavior1._selectedDate;
        //                var now = new Date();
        //                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy");
        //            }

        //        }
        function dateselectDateIssued(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                var textBox2 = document.getElementById('<%= txtDateIssued.ClientID %>');

                textBox2.value = ''
            }
            else {
                var calendarBehavior1 = $find("CalanderDateIssued");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy");
            }

        }
        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        }

        function Calculate() {

            var txtTruckWeight = document.getElementById('<%= txtTruckWeight.ClientID%>').value;
            var txtGrossWeight = document.getElementById('<%= txtGrossWeight.ClientID%>').value;
            var txtAddWeight = document.getElementById('<%= txtWeightAdjustment.ClientID%>').value;
            var txtNoOfBags = document.getElementById('<%= txtNoOfBags.ClientID%>').value;
            var cal = document.getElementById('<%= drpAdjustmentType.ClientID%>').value;
            var tare = document.getElementById('<%= HiddenFieldTare.ClientID%>').value;

            if (txtTruckWeight != '' && txtGrossWeight != "") {

                //var amt = eval((parseFloat(txtGrossWeight) - parseFloat(txtTruckWeight)) - ((parseFloat(txtNoOfBags) * parseFloat(tare)) / 100));
                var amt = eval((parseFloat(txtGrossWeight) - parseFloat(txtTruckWeight)) - ((parseFloat(txtNoOfBags) * parseFloat(tare))));
                amt = roundNumber(amt, 4);
                document.getElementById('<%= txtNetWeight.ClientID%>').value = amt;
                if (cal == 1) {
                    var netweight = eval(parseFloat(document.getElementById('<%= txtNetWeight.ClientID%>').value) + parseFloat(txtAddWeight));
                    netweight = roundNumber(netweight, 4);
                    document.getElementById('<%= txtNetWeight.ClientID%>').value = netweight;
                }
                else if (cal == -1) {
                    var netweight = eval(parseFloat(document.getElementById('<%= txtNetWeight.ClientID%>').value) - parseFloat(txtAddWeight));
                    netweight = roundNumber(netweight, 4);
                    document.getElementById('<%= txtNetWeight.ClientID%>').value = netweight;
                }
            }

        }

    </script>
                                                     
      
    <table bgcolor="White">
        <tr>
            <td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">  
          <ProgressTemplate>
              <asp:Image ID="imgPleaseWait" ImageUrl="~/Images/pleasewait.gif" AlternateText="Please wait..." runat="server" />         
          </ProgressTemplate>
    </asp:UpdateProgress>       
                <table>
                    <tr>
                        <td style="width: 1010px">
                            <uc1:Messages ID="Messages" runat="server" />
                        </td>
                    </tr>
                </table>
                <table class="style4">
                    <tr>
                        <td colspan="13" style="background-color: #88AB2D">
                            <asp:Label ID="lblInformation" runat="server" ForeColor="white" Text="PUN Information"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <asp:GridView ID="gvPUNInformation" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                HorizontalAlign="Left" GridLines="Both" CssClass="label" Width="100%" OnRowDataBound="gvPUNInformation_RowDataBound"
                                ShowFooter="True">
                                <Columns>
                                    <asp:BoundField DataField="WarehouseReceiptNo" HeaderText="Warehouse Receipt No"
                                        SortExpression="WarehouseReceiptNo">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ClientName" HeaderText="Client Name" SortExpression="ClientName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CommodityName" HeaderText="Commodity Name" SortExpression="CommodityName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RemainingWeight" HeaderText="Remaining Weight" ReadOnly="True"
                                        SortExpression="RemainingWeight">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProductionYear" HeaderText="Production Year" ReadOnly="True"
                                        SortExpression="ProductionYear">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpirationDate" DataFormatString="{0:M-dd-yyyy}" HeaderText="Expiration Date"
                                        SortExpression="ExpirationDate">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Measurement" HeaderText="Unit" ReadOnly="True"
                                        SortExpression="Measurement">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="Small" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13" style="background-color: #88AB2D">
                            <asp:Label ID="lblTruckRegistration" runat="server" ForeColor="white" Text="Truck Registration"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblDriver" runat="server" Text="Driver :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblLicenseNo" runat="server" Text="License No :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIssuedBy" runat="server" Text="Issued By :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblTruckPlateNo" runat="server" Text="Truck Plate No. :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblTrailerPlateNo" runat="server" Text="Trailer Plate No. :" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDriver" runat="server" ValidationGroup="gin"></asp:TextBox>
                        </td>
                        <td class="style12">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDriver"
                                ErrorMessage="Driver only accept character " ForeColor="White" ValidationExpression="[-a-zA-Z ]+"
                                ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDriver"
                                ErrorMessage="Driver is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtLicenseNo" runat="server" CssClass="style1"></asp:TextBox>
                        </td>
                        <td class="style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLicenseNo"
                                ErrorMessage="License is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td class="style15">
                            <asp:TextBox ID="txtIssuedBy" runat="server" CssClass="style1" ValidationGroup="date"></asp:TextBox>
                        </td>
                        <td class="style12">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtIssuedBy"
                                ErrorMessage="Issued by only accept character " ForeColor="White" ValidationExpression="[-a-zA-Z ]+"
                                ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator5_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator5">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIssuedBy"
                                ErrorMessage="Issued by is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTruckPlateNo" runat="server" CssClass="style1"></asp:TextBox>
                        </td>
                        <td class="style8">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTruckPlateNo"
                                ErrorMessage="Truck plate no is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTrailerPlateNo" runat="server" CssClass="style1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13" style="background-color: #88AB2D">
                            <asp:Label ID="lblLoadingRegistration" runat="server" ForeColor="white" Text="Loading Registration"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblTruckRequestTime" runat="server" Text="GIN DN Submitted Time :"
                                CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblTruckRegisterTime" runat="server" Text="Truck Provided Time :"
                                CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblDateLoaded" runat="server" Text="Date Loaded :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblInventoryCoordinatorload" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblDailyLabourersAssociation" runat="server" Text=" Daily Labourers' Association :"
                                CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDNSubmitedTime" Width="70" runat="server" ValidationGroup="date"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtendeTruckRequestTime1" runat="server"
                                            BehaviorID="CalanderTruckRequestTime" OnClientDateSelectionChanged="dateselectTruckRequestTime"
                                            Enabled="True" TargetControlID="txtDNSubmitedTime">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDNSubmitedTimeOnly" Width="60px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style12">
                            <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtDNSubmitedTimeOnly">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                ControlToValidate="txtDNSubmitedTimeOnly" Display="None" InvalidValueMessage="Please enter a valid time."
                                SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Time Is required"
                                ControlToValidate="txtDNSubmitedTimeOnly" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDNSubmitedTime"
                                ErrorMessage="DN submitted time only accept  date" ForeColor="White" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                                ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator6_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator6">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDNSubmitedTime"
                                ErrorMessage="DN submited time is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                        </td>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtAvailableTime" runat="server" ValidationGroup="date" Width="70px"></asp:TextBox>
                                        <span class="style2">
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderAvailableTime" runat="server" Enabled="True"
                                                BehaviorID="CalanderAvailableTime" OnClientDateSelectionChanged="dateselectAvailableTime"
                                                TargetControlID="txtAvailableTime">
                                            </ajaxToolkit:CalendarExtender>
                                        </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAvailableTimeOnly" Width="60px" runat="server"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="True"
                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtAvailableTimeOnly">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                            ControlToValidate="txtAvailableTimeOnly" Display="None" InvalidValueMessage="Please enter a valid time."
                                            SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator1_ValidatorCalloutExtender"
                                            runat="server" Enabled="True" TargetControlID="MaskedEditValidator1">
                                        </ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style14">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAvailableTime"
                                ErrorMessage="Truck provided time is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator6_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <span class="style2">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtAvailableTime"
                                    ErrorMessage="Truck Provided Time only accept  date" ForeColor="White" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator7_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator7">
                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                            </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtAvailableTimeOnly"
                                ErrorMessage="Time is Required" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator17_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator17">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td class="style15">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDateLoaded" runat="server" ValidationGroup="date" Width="70px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender BehaviorID="CalanderDateLoaded" ID="txtDateLoaded_CalendarExtender"
                                            OnClientDateSelectionChanged="dateselect" runat="server" Enabled="True" TargetControlID="txtDateLoaded">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateLoadedTime" Width="60px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style12">
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="True"
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtDateLoadedTime">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="EarliestTimeExtender1"
                                ControlToValidate="txtDateLoadedTime" Display="None" InvalidValueMessage="Please enter a valid time."
                                SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator2_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="MaskedEditValidator2">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Time Is required"
                                ControlToValidate="txtDateLoadedTime" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator14_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator14">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDateLoaded"
                                ErrorMessage="Date loaded is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <span class="style2">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDateLoaded"
                                    ErrorMessage="Date loaded only accept  date" ForeColor="White" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator8_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator8">
                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                            </span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="drpInventoryCoordinatorLoad" AutoPostBack="true" runat="server"
                                Width="145px" OnSelectedIndexChanged="drpInventoryCoordinatorLoad_SelectedIndexChanged"
                                CssClass="style1">
                            </asp:DropDownList>
                        </td>
                        <td colspan="3">
                            <span class="style1">
                                <asp:DropDownList ID="drpDailyLabourersAssociation" runat="server" Width="145px"
                                    CssClass="style1">
                                    <asp:ListItem>[Select Labourers]</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13" style="background-color: #88AB2D">
                            <asp:Label ID="lblStackRegistration" runat="server" ForeColor="white" Text="Stack Registration"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblDateWeighted" runat="server" Text="Date Weighed :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblWBServiceProvider" runat="server" Text="WB Service Provider :"
                                CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblInventoryCoordinator" runat="server" Text="Weigher" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTruckType" runat="server" Text="Truck Type" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:Label ID="lblStackNo" runat="server" Text="Stack No :" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDateWeighted" runat="server" ValidationGroup="date" Width="70px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender BehaviorID="CalanderDateWeighed" ID="CalendarExtender1"
                                            OnClientDateSelectionChanged="dateselectDateWeighed" runat="server" Enabled="True"
                                            TargetControlID="txtDateWeighted">
                                        </ajaxToolkit:CalendarExtender>
                                        <%--  <ajaxToolkit:CalendarExtender ID="txtDateWeighted_CalendarExtender" runat="server"
                                            BehaviorID="CalanderDateWeighted" OnClientDateSelectionChanged="dateselectDateWeighted"
                                            Enabled="True" TargetControlID="txtDateWeighted">
                                        </ajaxToolkit:CalendarExtender>--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateWeightedTime" Width="60px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style12">
                            <span class="style2">
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="True"
                                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtDateWeightedTime">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="EarliestTimeExtender1"
                                    ControlToValidate="txtDateWeightedTime" Display="None" InvalidValueMessage="Please enter a valid time."
                                    SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator3_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="MaskedEditValidator3">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDateWeightedTime"
                                    ValidationGroup="gin" Text="*" ErrorMessage="time is required"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator15_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator15">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDateWeighted"
                                    ErrorMessage="Please enter only  date" ForeColor="White" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>--%>                                <%--  <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator9_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator9">
                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDateWeighted"
                                    ErrorMessage="Date weighed is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="drpWBServiceProvider" runat="server" Width="145px" CssClass="style1">
                                <asp:ListItem>[Select WB ServiceProvider]</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="drpWeigherSupervisor" runat="server" Width="145px" CssClass="style1">
                                <asp:ListItem>[Select Weigher]</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="drpTruckType" runat="server" Width="145px" CssClass="style1">
                                <asp:ListItem>[Select Truck Type]</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="4">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
              <asp:DropDownList ID="drpStackNo" runat="server" Width="145px" CssClass="style1"
                                AutoPostBack="true" OnSelectedIndexChanged="drpStackNo_SelectedIndexChanged"
                                ValidationGroup="gin">
                               
                            </asp:DropDownList>
          </ContentTemplate>
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="drpInventoryCoordinatorLoad" 
                  EventName="SelectedIndexChanged" />
          </Triggers>
      </asp:UpdatePanel>
                                                                                                                                                                                                         
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="lblNoOfBags" runat="server" Text="No of Bags :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2" class="style3">
                            &nbsp;
                        </td>
                        <td colspan="2" class="style3">
                            <asp:Label ID="lblScaleTicketNo" runat="server" Text="Scale Ticket No.  :" CssClass="label"></asp:Label>
                        </td>
                        <td class="style3" colspan="2">
                            <asp:Label ID="lblTruckWeight" runat="server" Text="Truck Weight  :" CssClass="label"></asp:Label>
                        </td>
                        <td class="style3" colspan="2">
                            <asp:Label ID="lblGrossWeight" runat="server" Text="Gross Weight  :" CssClass="label"></asp:Label>
                        </td>
                        <td style="text-align: left" colspan="2">
                            <asp:Label ID="lblLoadingTicket" runat="server" Text="Loading Ticket NO:" CssClass="label"></asp:Label>
                        </td>
                        <td style="text-align: right" colspan="2" class="style3">
                            <asp:Button ID="btnAddStack" runat="server" Text="More Stack" BackColor="#88AB2D"
                                ForeColor="White" Width="100px" BorderStyle="None" OnClick="btnAddStack_Click"
                                CssClass="style1" ValidationGroup="gin" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style17">
                            <asp:TextBox ID="txtNoOfBags" runat="server" onkeyup="Calculate();" CssClass="style1"
                                ValidationGroup="date"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <span class="style2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNoOfBags"
                                    ErrorMessage="Please enter only number" ForeColor="White" ValidationExpression="^[+]?\d*$"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator2_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator2">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNoOfBags"
                                    ErrorMessage="No of Bag is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator10_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </span>
                        </td>
                        <td class="style15">
                            <asp:TextBox ID="txtScaleTicketNo" runat="server" CssClass="style1" ValidationGroup="date"></asp:TextBox>
                        </td>
                        <td class="style14">
                            <span class="style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtScaleTicketNo"
                                    ErrorMessage="Scale Ticket No is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator11_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator11">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </span>
                        </td>
                        <td class="style11">
                            <asp:TextBox ID="txtTruckWeight" runat="server" onkeyup="Calculate();" CssClass="style1"
                                ValidationGroup="date"></asp:TextBox>
                        </td>
                        <td class="style14">
                            <span class="style2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtTruckWeight"
                                    ErrorMessage="Truck weight number or floating" ForeColor="White" ValidationExpression="[-+]?[0-9]*\.?[0-9]*"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator4_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator4">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTruckWeight"
                                    ErrorMessage="Truck weight is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator13_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator13">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </span>
                        </td>
                        <td class="style15">
                            <asp:TextBox ID="txtGrossWeight" runat="server" onkeyup="Calculate();" CssClass="style1"></asp:TextBox>
                        </td>
                        <td class="style12">
                            <span class="style2">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtGrossWeight"
                                    ErrorMessage="Gross weight accept number or floating" ForeColor="White" ValidationExpression="[-+]?[0-9]*\.?[0-9]*"
                                    ValidationGroup="gin">*</asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator3_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RegularExpressionValidator3">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtGrossWeight"
                                    ErrorMessage="Gross weight is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator12_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator12">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </span>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtLoadingTicket" runat="server" CssClass="style1" ValidationGroup="date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <asp:GridView ID="gvStack" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" ForeColor="Black" AutoGenerateColumns="False" PageSize="3"
                                CssClass="label" Width="100%" DataKeyNames="Id,StackID" 
                                OnSelectedIndexChanged="gvStack_SelectedIndexChanged" 
                                onrowcommand="gvStack_RowCommand" onrowcreated="gvStack_RowCreated">
                                <Columns>
                                    <%-- <asp:BoundField HeaderText="Stack No">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="ScaleTicketNumber" HeaderText="Ticket No" SortExpression="ScaleTicketNumber">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NoOfBags" HeaderText="Bags" SortExpression="NoOfBags">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="grossWeight" HeaderText="Gross Weight" SortExpression="grossWeight">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TruckWeight" HeaderText="Truck Weight" SortExpression="TruckWeight">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <%-- <asp:CommandField ShowEditButton="True" />--%>
                                    <asp:CommandField ShowSelectButton="True" />

                                    <asp:ImageField>
                                    </asp:ImageField>

                                </Columns>
                             <%--   <EmptyDataTemplate>
                                    <asp:ImageButton ID="btnStackRemove" runat="server"  OnClientClick = 'return confirm("Are you sure you want to delete this entry?");'
                                        onclick="btnStackRemove_Click" CommandName="DeleteStack" />
                                </EmptyDataTemplate>--%>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="small" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13" style="background-color: #88AB2D">
                            <asp:Label ID="lblScalingRegistration" runat="server" ForeColor="white" Text="GIN"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblAddWeight" runat="server" Text="Operation Type :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblReturnWeight" runat="server" Text="Weight :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblAddWeight0" runat="server" Text="Bag :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblAddWeight1" runat="server" Text="Number of Rebags :" CssClass="label"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblDateIssued" runat="server" Text="Date Issued :" CssClass="label"></asp:Label>
                        </td>
                        <td class="style16">
                            <asp:Label ID="lblNetWeight" runat="server" Text="Net Weight :" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DropDownList ID="drpAdjustmentType" runat="server" Width="145px" CssClass="style1">
                                <asp:ListItem Selected="True" Value="0">[select operation]</asp:ListItem>
                                <asp:ListItem Value="1">Add</asp:ListItem>
                                <asp:ListItem Value="-1">Return</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtWeightAdjustment" runat="server" onkeyup="Calculate();" CssClass="style1">0</asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                ControlToValidate="txtWeightAdjustment" ErrorMessage="Weight only accept number or floating"
                                ForeColor="White" ValidationExpression="[-+]?[0-9]*\.?[0-9]*" ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator11_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator11">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtBagAdjustment" runat="server" CssClass="style1">0</asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                ControlToValidate="txtBagAdjustment" ErrorMessage="Bag only accept number or floating"
                                ForeColor="White" ValidationExpression="[-+]?[0-9]*\.?[0-9]*" ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator12_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator12">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtRebag" runat="server" CssClass="style1">0</asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                ControlToValidate="txtRebag" ErrorMessage="Number of Rebags only accept number or floating"
                                ForeColor="White" ValidationExpression="[-+]?[0-9]*\.?[0-9]*" ValidationGroup="gin">*</asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator13_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator13">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td class="style13">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDateIssued" runat="server" ValidationGroup="date" Width="70px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="txtDateIssued_CalendarExtender" runat="server"
                                            BehaviorID="CalanderDateIssued" OnClientDateSelectionChanged="dateselectDateIssued"
                                            Enabled="True" TargetControlID="txtDateIssued">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateIssuedTime" Width="60px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style5">
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="True"
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtDateIssuedTime">
                            </ajaxToolkit:MaskedEditExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="EarliestTimeExtender1"
                                ControlToValidate="txtDateIssuedTime" Display="None" InvalidValueMessage="Please enter a valid time."
                                SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator4_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="MaskedEditValidator4">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Time is required"
                                ControlToValidate="txtDateIssuedTime" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator16_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator16">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <%-- <asp:BoundField HeaderText="Stack No">
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDateIssued"
                                ErrorMessage="Date issued is required field" ValidationGroup="gin">*</asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                        <td class="style16">
                            <asp:TextBox ID="txtNetWeight" ReadOnly="true" runat="server" CssClass="style1"></asp:TextBox>
                            <input id="HiddenselectedDropdown" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13">
                            <center>
                                <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" OnClick="btnSave_Click" CssClass="style1" ValidationGroup="gin" />
                                <span class="style1">&nbsp; </span>
                                <asp:Button ID="btnPrint" runat="server" Text="Print GIN" Visible="false" BackColor="#88AB2D"
                                    ForeColor="White" Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnPrint_Click" />
                                <span class="style1">&nbsp; </span>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#88AB2D" ForeColor="White"
                                    Width="100px" BorderStyle="None" OnClick="btnCancel_Click" CssClass="style1" />
                            </center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenFieldTare" runat="server" />
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
