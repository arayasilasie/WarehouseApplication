<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GINPSA.aspx.cs"
    Inherits="WarehouseApplication.GINPSA" Title="PSA" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
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
            width: 92px;
        }
    </style>
    <script type="text/javascript">
        function dateselect(sender) {
            document.getElementById('spanMessage').innerHTML = "";
            var date = document.getElementById('<%= txtDateIssued.ClientID %>').value;
            var time = document.getElementById('<%= txtDateIssuedTime.ClientID %>').value;
            var today = new Date();
            if (today < new Date(date + ' ' + time)) {
                document.getElementById('spanMessage').innerHTML = "Date time can't be greater than today!";
                document.getElementById('<%= txtDateIssued.ClientID %>').value = "";
                document.getElementById('<%= txtDateIssuedTime.ClientID %>').value = "";
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table bgcolor="White">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 1010px">
                                    <span id='spanMessage' style='font-family: Verdana; font-size: small; color: Red'>
                                    </span>
                                    <uc1:Messages ID="Messages" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table width="1000px" class="style4">
                    <tr>
                        <td colspan="8" style="background-color: #88AB2D">
                            <asp:Label ID="lblInformation" runat="server" ForeColor="white" Text="PUN Information"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <asp:GridView ID="gvPUNInformation" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                HorizontalAlign="Left" GridLines="Both" CssClass="label" Width="100%" ShowFooter="True"
                                OnRowDataBound="gvPUNInformation_RowDataBound">
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
                                    <asp:BoundField DataField="ExpirationDate" DataFormatString="{0:M-dd-yyyy}" HeaderText="Expiration Date"
                                        SortExpression="ExpirationDate">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" Font-Bold="true" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="Small" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="background-color: #88AB2D">
                            <asp:Label ID="lblLoadingRegistration" runat="server" ForeColor="White" Text="PSA Registration"
                                CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                            Working...</a>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td colspan="1">
                                    <asp:Label ID="lblInventoryCoordinatorload" runat="server" Text="LIC :" CssClass="label"
                                        Height="16px" Width="128px"></asp:Label>
                                </td>
                                <%--<td>
                            <asp:Label ID="lblStackNo" runat="server" Text="Stack No :" CssClass="label" 
                                Visible="True"></asp:Label>
                        </td>--%>
                                <td>
                                    <asp:Label ID="lblDateIssued" runat="server" Text="Date Issued :" CssClass="label"
                                        Height="16px" Width="97px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDateIssued0" runat="server" Text="Time Issued :" CssClass="label"
                                        Height="17px" Width="101px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblShedNo" runat="server" Text="Shed No. :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCommoditySymbol" runat="server" Text="Commodity Grade Symbol :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrentBalance" runat="server" Text="Current Balance :"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrentWeight" runat="server" Text="Current Weight :"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="style2"></span>
                                    <asp:DropDownList ID="drpInventoryCoordinatorLoad" AutoPostBack="true" runat="server"
                                        Width="145px" OnSelectedIndexChanged="drpInventoryCoordinatorLoad_SelectedIndexChanged"
                                        CssClass="style1">
                                        <asp:ListItem>[Select LIC]</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <%--<td>
                         <span class="style2">*</span>
                            <asp:DropDownList ID="drpStackNo" runat="server" Width="145px" 
                                CssClass="style1" Visible="True">
                                <asp:ListItem>[Select Stack]</asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                                <td class="style8">
                                    <asp:TextBox ID="txtDateIssued" runat="server" ValidationGroup="date" Width="70px"
                                        ReadOnly="True"></asp:TextBox>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDateIssued"
                                        ErrorMessage="Invalid Date entry" Type="Date" MinimumValue="1/1/1900" ValidationGroup="psa">*</asp:RangeValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator7_ValidatorCalloutExtender"
                                        runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:CalendarExtender ID="txtDateIssued_CalendarExtender" runat="server"
                                        BehaviorID="CalanderDateIssued" Enabled="True" TargetControlID="txtDateIssued">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateIssuedTime" Width="60px" runat="server"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="True"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtDateIssuedTime">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <%--<ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="EarliestTimeExtender1"
                                            ControlToValidate="txtDateIssuedTime" Display="None" InvalidValueMessage="Please enter a valid time."
                                            SetFocusOnError="True">*</ajaxToolkit:MaskedEditValidator>--%>
                                    <%--<ajaxToolkit:ValidatorCalloutExtender ID="MaskedEditValidator4_ValidatorCalloutExtender" 
                                            runat="server" Enabled="True" TargetControlID="MaskedEditValidator4">
                                        </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                ControlToValidate="txtDateIssued" ErrorMessage="Date Issued only accept  date"
                                ForeColor="White" ValidationExpression="^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$"
                                ValidationGroup="psa">*</asp:RegularExpressionValidator>--%>
                                    <%--<ajaxToolkit:ValidatorCalloutExtender ID="RegularExpressionValidator10_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RegularExpressionValidator10">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDateIssuedTime"
                                        ErrorMessage="Date issued is required field" ValidationGroup="psa">*</asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShedNo" ReadOnly="true" runat="server" Width="75px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCommoditySymbol" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentBalance" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentWeight" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <center>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#88AB2D" ForeColor="White"
                                            OnClientClick="dateselect(this);" Width="100px" BorderStyle="None" OnClick="btnSave_Click"
                                            CssClass="style1" ValidationGroup="psa" />
                                        <span class="style1">&nbsp; </span>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print PSA" Visible="false" BackColor="#88AB2D"
                                            ForeColor="White" Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnPrint_Click" />
                                        <span class="style1">&nbsp; </span>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#88AB2D" ForeColor="White"
                                            Width="100px" BorderStyle="None" OnClick="btnCancel_Click" CssClass="style1" />
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
