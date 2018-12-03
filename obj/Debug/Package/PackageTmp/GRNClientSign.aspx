<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GRNClientSign.aspx.cs" Inherits="WarehouseApplication.GRNClientSign" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
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
            height: 22px;
        }
    </style>

    <script type="text/javascript">

        function dateselect(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))


                var textBox2 = sender._textbox

                textBox2.value = ''

            }
            else {
                var calendarBehavior1 = $find("CalanderDateTimeClientSigned");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy") + " " + now.format("HH:mm:ss")
            }

        }
        //document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "hidden";

        function ClientCheck() {
            var flag = false;
            //  var CountLoad = 0;
            //  var defaults = ['<%=ConfigurationManager.AppSettings["MaximumLoad"] %>']

            for (var i = 0; i < document.forms[0].length; i++) {
                if (document.forms[0].elements[i].id.indexOf('chkSelect') != -1) {
                    if (document.forms[0].elements[i].checked) {
                        flag = true
                    }
                }
            }

            if (flag == true) {
                document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "visible";
                //document.forms[0].elements['<%= btnCancel.ClientID %>'].style.visibility = "visible";

                return true
            }
            else {

                document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "hidden";
                //document.forms[0].elements['<%= btnCancel.ClientID %>'].style.visibility = "hidden";
                //document.forms[0].elements['<%= lblMessage.ClientID %>'].style.visibility = "hidden";
                //document.getElementById("lblMessage").style.display = "none";
                return false
            }
        }
        function hide() {
            //            alert("in");
            document.getElementById("lblMessage").style.visibility = "hidden";
        }


        function CheckDate() {
            var tbl = document.getElementById('<%= grvGRNClientSign.ClientID %>');
            var checkBoxes = new Array();
            var checkBoxes2 = tbl.getElementsByTagName('input');
            for (var k = 0; k < checkBoxes2.length; k++) {
                if (checkBoxes2[k].type == 'checkbox')
                    checkBoxes.push(checkBoxes2[k]);
            }
            var emptyCtrl = null;
            var msg = '';
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked == true) {
                    var row = checkBoxes[i].parentNode.parentNode;
                    var dt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');
                    var grn = '';
                    for (var j = 0; j < label.length; j++) {
                        if (label[j].id.endsWith('lblGRNNo')) {
                            grn = label[j].innerText;
                        }
                    }

                    for (var c = 0; c < dt.length; c++) {
                        if (dt[c].type == 'text' && dt[c].id.endsWith('txtDateTimeLICSigned') && dt[c].value == '') {
                            msg = msg + '\tDate is not given for GRN No: <b>' + grn + '</b>.';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '<br />\n';
                        }
                        if (dt[c].type == 'text' && dt[c].id.endsWith('txtTimeLICSigned') && dt[c].value == '') {
                            msg = msg + '\tTime is not given for GRN No: <b>' + grn + '</b>.';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '<br />\n';
                        }
                    }
                }
            }
            if (msg != '') {
                emptyCtrl.focus();
                document.getElementById('spanMessage').innerHTML = msg;
                //alert(msg);
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <uc1:Messages ID="Messages1" runat="server" />
                <asp:Panel ID="pnlMessage" runat="server">
                    <span id='spanMessage' style='font-family: Verdana; font-size: small; color: #006600'>
                    </span>
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Agency FB" Font-Size="14pt"
                        Style="font-family: Verdana; font-size: small; color: #006600"></asp:Label>
                </asp:Panel>
                <div id="Header" class="formHeader" style="width: 87%; margin-left: 40px; margin-top: 10px;"
                    align="center">
                    <asp:Label ID="lblDetail" Text=" GRN Client Signing" Width="100%" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 87%; margin-left: 40px;">
                    <div style="margin-bottom: 10px;">
                        <div style="border: solid 1px #88AB2D; height: 70px;">
                            <div style="margin-top: 10px; float: left; height: 26px; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="GRN No :"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtGRNNo" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; margin-left: 7px; float: left;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblClientId0" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblLIC0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddLIC" runat="server" AppendDataBoundItems="True" CssClass="style1"
                                        ValidationGroup="Search" Width="145px">
                                        <asp:ListItem Value="">Select LIC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 20px;">
                                <div style="height: 26px;">
                                </div>
                                <div>
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: right; margin-right: 10px;">
                                <div style="height: 26px">
                                </div>
                                <div>
                                    <asp:Button ID="btnApprove" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" OnClick="btnApprove_Click" Text="Signed"
                                        Width="100px" OnClientClick='return CheckDate();' />
                                    <asp:Button ID="btnCancel" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" Text="Cancel" Visible="False" Width="100px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; width: 87%; margin-left: 40px;">
                    <asp:GridView ID="grvGRNClientSign" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="label"
                        DataKeyNames="ID" GridLines="Vertical" Style="font-size: small" AllowPaging="True"
                        OnPageIndexChanging="grvGRNClientSign_PageIndexChanging" OnRowDataBound="grvGRNClientSign_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <%--<div id="gv">--%>
                                    <asp:CheckBox ID="chkSelect" runat="server" Onclick="ClientCheck();" />
                                    <%-- </div>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="Commodity">
                                <ItemTemplate>
                                    <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weight" ItemStyle-HorizontalAlign="Right">
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
                            <asp:TemplateField HeaderText="Client Signed" SortExpression="DateTimeLICSigned">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                Time
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDateTimeLICSigned" runat="server" Width="70px"></asp:TextBox>
                                                <asp:Label Width="70px" Visible="false" Text='<%#Bind("GRNCreatedDate") %>' ID="lblGRNCreatedDate"
                                                    runat="server"></asp:Label>
                                                <asp:RangeValidator ID="DateRangeValidator" ValidationGroup="Save" Type="Date" ControlToValidate="txtDateTimeLICSigned"
                                                    Display="None" ForeColor="Tomato" runat="server" ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="DateRangeValidator">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <td>
                                                    <asp:TextBox ID="txtTimeLICSigned" runat="server" Width="60px"></asp:TextBox>
                                                </td>
                                        </tr>
                                    </table>
                                    <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeLICSigned">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                        ControlToValidate="txtTimeLICSigned" Display="Dynamic" InvalidValueMessage="Please enter a valid time."
                                        SetFocusOnError="True">     
                                    </ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeLICSigned" runat="server"
                                        Enabled="True" TargetControlID="txtDateTimeLICSigned">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#e4efd0" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
