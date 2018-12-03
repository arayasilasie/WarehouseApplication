<%@ Page Title="GIN Approve" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GINApprove.aspx.cs" Inherits="WarehouseApplication.GINApprove" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
    </style>
    <script type="text/javascript">
        function checkGINs() {
            var globalGIN = '';
            var feturedDate = false;
            var flagGinOrd = false;
            var isNotStacked = true;
            var tbl = document.getElementById('<%= gvApproval.ClientID %>');
            var checkBoxes = new Array();
            var checkBoxes2 = tbl.getElementsByTagName('input');
            for (var k = 0; k < checkBoxes2.length; k++) {
                if (checkBoxes2[k].type == 'checkbox')
                    checkBoxes.push(checkBoxes2[k]);
            }
            //            var emptyCtrl = null;
            //            var msg = '';

            document.getElementById('spanMessage').innerHTML = '';
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked == true) {
                    //alert("ckeckbox Leng : " + checkBoxes.length);
                    var row = checkBoxes[i].parentNode.parentNode.parentNode;
                    var dt = row.getElementsByTagName('SELECT');
                    var txtdt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');
                    label[0].style.backgroundColor = "White";

                    var rowGin = "00";
                    for (var j = 0; j < dt.length; j++) {
                        dt[j].style.backgroundColor = "White";
                        var stkd = false;
                        if
                                ((dt[j].type == 'select-one' || dt[j].type == 'select') &&
                                ((dt[j].id.endsWith('drpLICStatus') || dt[j].id.endsWith('drpLICStatus_0')) ||
                                    (dt[j].id.endsWith('drpClientStatus') || dt[j].id.endsWith('drpClientStatus_0'))) &&
                                (dt[j].value == '1' || dt[j].value == '7' || dt[j].value == '3')) {
                            var isRow = (rowGin == label[0].innerHTML) ? true : false;
                            //(isRow && !isNotStacked) ? isNotStacked = true : isNotStacked = false;

                            //globalGIN += '► ' + label[0].innerHTML + ' \n';
                            //alert(j + ". Row: " + !isRow + " Row-> " + rowGin + "; lbl-> " + label[0].innerHTML + "\nStacked: " + isNotStacked);
                            //alert("Row: " + !isRow+"\nStacked: " + isNotStacked);
                            if (flagGinOrd) {
                                stkd = false;
                                (!isRow || isNotStacked) ? globalGIN += '► ' + label[0].innerHTML + ' \n' : stkd = true;
                            }
                            else {
                                stkd = false;
                                (!isRow || isNotStacked) ? globalGIN += '► ' + label[0].innerHTML + '      ' : stkd = true;

                            }
                            isNotStacked = stkd;
                            label[0].style.backgroundColor = "Red";
                            dt[j].style.backgroundColor = "Red";
                            flagGinOrd = !flagGinOrd;
                        }
                        rowGin = label[0].innerHTML;
                    }
                    isNotStacked = true;

                    for (var v = 0; v < txtdt.length; v++) {
                        if (txtdt[v].type == 'text' && (txtdt[v].id.endsWith('txtDateTimeClientSigned') || txtdt[v].id.endsWith('txtDateTimeClientSigned_0'))) {
                            txtdt[v].style.backgroundColor = "White";


                            var selectedDate = new Date(txtdt[v].value).format('yyyy/MM/dd');  //getDateFromFormat(dt[v].value, 'MM/dd/yyyy');
                            var nowT = new Date().format('yyyy/MM/dd'); //MM/dd/yyyy

                            if ((selectedDate > nowT)) {
                                txtdt[v].style.backgroundColor = "Red";
                                feturedDate = true;
                                //document.getElementById('spanMessage').innerHTML = 'Date Must be Less than the Current Date!!!';

                            }
                            //alert("  date: " + selectedDate + "   compDt: " + nowT);
                        }
                        if (txtdt[v].type == 'text' &&
                                (txtdt[v].id.endsWith('txtDateTimeLICSigned') || txtdt[v].id.endsWith('txtDateTimeLICSigned_0'))) {
                            txtdt[v].style.backgroundColor = "White";
                            document.getElementById('spanMessage').innerHTML = '';

                            // var selectedDate = new Date(txtdt[v].value).format('MM/dd/yyyy');  //getDateFromFormat(dt[v].value, 'MM/dd/yyyy');
                            // var nowT = new Date().format('MM/dd/yyyy');

                            if ((selectedDate > nowT)) {
                                txtdt[v].style.backgroundColor = "Red";
                                feturedDate = true;
                                //document.getElementById('spanMessage').innerHTML = 'Date Must be Less than the Current Date!!!';
                            }
                        }
                    } //for

                    //} 

                } //if
            } //for
            if (feturedDate == true) {
                document.getElementById('spanMessage').innerHTML = 'Can’t set date to future. Can only be today or Preceding’s of today';
            }
            if (globalGIN != '') {
                return confirm('GIN With ID Number:\n' + globalGIN + '\nis\\are NOT Accepted!!!\n ARE YOU SURE !?');
            }
            if (feturedDate == true) {
                return false;
            }
            return true;
        }

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
       // document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "hidden";

        function CheckDate() {
            if (checkGINs() == false)
                return false;
            var tbl = document.getElementById('<%= gvApproval.ClientID %>');
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
                    var row = checkBoxes[i].parentNode.parentNode.parentNode;
                    var dt = row.getElementsByTagName('input');
                    var label = row.getElementsByTagName('span');
                    var gin = '';
                    for (var j = 0; j < label.length; j++) {
                        if (label[j].id.endsWith('lblGINNo') || label[j].id.endsWith('lblGINNo_0')) {
                            gin = label[j].innerText;
                        }
                    }

                    for (var c = 0; c < dt.length; c++) {
                        if (dt[c].type == 'text' && 
                            (dt[c].id.endsWith('txtDateTimeClientSigned') || dt[c].id.endsWith('txtDateTimeClientSigned_0')) && 
                            dt[c].value == '') {
                            msg = msg + '\tDate is not given for selected GIN No: .';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '\n';
                        }
                        if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtTimeClientSigned') || dt[c].id.endsWith('txtTimeClientSigned_0')) && 
                            dt[c].value == '') {
                            msg = msg + '\tTime is not given for selected GIN No: .';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '\n';
                        }

                        if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtDateTimeLICSigned') || dt[c].id.endsWith('txtDateTimeLICSigned_0')) &&
                            dt[c].value == '') {
                            msg = msg + '\tDate is not given for selected GIN No: .';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '\n';
                        }
                        if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtTimeLICSigned') || dt[c].id.endsWith('txtTimeLICSigned_0')) &&
                            dt[c].value == '') {
                            msg = msg + '\tTime is not given for selected GIN No: .';
                            if (emptyCtrl == null) emptyCtrl = dt[c];
                            if (msg != '') msg += '\n';
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
                document.forms[0].elements['<%= btnCancel.ClientID %>'].style.visibility = "visible";
                return true
            }
            else {
                document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "hidden";
                document.forms[0].elements['<%= btnCancel.ClientID %>'].style.visibility = "hidden";
                return false
            }
        }
       

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style1" bgcolor="white">
                <caption>
                    <tr>
                        <td>
                            <br />
                            <asp:Panel ID="pnlMessage" runat="server">
                                <span id='spanMessage' style='font-family: Verdana; font-size: small; color: #006600'>
                                </span>
                                <asp:Label ID="lblMessage" runat="server" Font-Names="Agency FB" Font-Size="14pt"
                                    Style="font-family: Verdana; font-size: small; color: red"></asp:Label>
                            </asp:Panel>
                            <br />
                            <uc1:Messages ID="Messages" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="800px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGIN" runat="server" Text="GIN No :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWareHouseReceipt" runat="server" Text="Warehouse Receipt :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblClientId" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGINNo" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWareHouseReceipt" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpLIC" runat="server" Width="145px" CssClass="style1">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                        BackColor="#88AB2D" ForeColor="White" Width="100px" BorderStyle="None" CssClass="style1" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" BackColor="#88AB2D" ForeColor="White"
                                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnApprove_Click"
                                                        OnClientClick='return CheckDate();' ValidationGroup="App" />                                                    
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#88AB2D" ForeColor="White"
                                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnCancel_Click"
                                                        Visible="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <hr style="width: 900px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvApproval" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Id"
                                GridLines="Vertical" Style="font-size: small" Width="100%" CssClass="label">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <div id="gv">
                                                <asp:CheckBox ID="chkSelect" runat="server" Onclick="ClientCheck();" AutoPostBack="false" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GINNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGINNo" runat="server" Text='<%# Bind("GINNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:BoundField DataField="GINNumber" HeaderText="GINNo" ReadOnly="true" SortExpression="GINNumber">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField Visible="false" HeaderText="WHRNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouseReceiptNo" Text='<%#Eval("PickupNoticesList[0].WarehouseReceiptNo")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClient" Text='<%#Eval("PickupNoticesList[0].ClientName")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Commodity Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodityName" Text='<%#Eval("PickupNoticesList[0].CommodityName")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="P-Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductionYear" Text='<%#Eval("PickupNoticesList[0].ProductionYear")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NetWeight" HeaderText="Weight" ReadOnly="true" SortExpression="NetWeight">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                      <asp:BoundField DataField="DateIssued" HeaderText="Issued Date" ReadOnly="true" SortExpression="DateIssued">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Client Signed" SortExpression="DateTimeClientSigned">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Status
                                                    </td>
                                                    <td>
                                                        Date
                                                    </td>
                                                    <td>
                                                        Time
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="drpClientStatus" Width="70px" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="drpClientStatus_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select" Value="1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Accept" Value="2">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="3">
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateTimeClientSigned" Width="70px" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTimeClientSigned" Width="60px"   runat="server" 
                                                            ValidationGroup="App"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender" runat="server" AcceptAMPM="True"
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeClientSigned">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator" runat="server" ControlExtender="EarliestTimeExtender"
                                                ControlToValidate="txtTimeClientSigned" Display="Dynamic" InvalidValueMessage="Please enter a valid time."
                                                SetFocusOnError="True" ValidationGroup="App"></ajaxToolkit:MaskedEditValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="EarliestTimeValidator_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="EarliestTimeValidator">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeClientSigned" runat="server"
                                                Enabled="True" TargetControlID="txtDateTimeClientSigned">
                                            </ajaxToolkit:CalendarExtender>                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DateTimeClientSigned") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LIC Signed" SortExpression="DateTimeLICSigned">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Status
                                                    </td>
                                                    <td>
                                                        Date
                                                    </td>
                                                    <td>
                                                        Time
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="drpLICStatus" Width="70px" runat="server">
                                                            <asp:ListItem Text="Select" Value="1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Accept" Value="5">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="7">
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateTimeLICSigned"  Width="70px" runat="server" 
                                                            ValidationGroup="App"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTimeLICSigned" Width="60px"  runat="server" 
                                                            ValidationGroup="App"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                                                Enabled="True" Mask="99:99"  MaskType="Time" TargetControlID="txtTimeLICSigned">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                                ControlToValidate="txtTimeLICSigned" Display="None" InvalidValueMessage="Please enter a valid time."
                                                SetFocusOnError="True" ValidationGroup="App"></ajaxToolkit:MaskedEditValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender" 
                                                runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                                            </ajaxToolkit:ValidatorCalloutExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeLICSigned" runat="server"
                                                Enabled="True" TargetControlID="txtDateTimeLICSigned">
                                            </ajaxToolkit:CalendarExtender>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DateTimeLICSigned") %>'></asp:Label>
                                        </EditItemTemplate>
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
                        </td>
                    </tr>
                </caption>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
   <%-- <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowModalPopup"
        PopupControlID="divPopUp" BackgroundCssClass="popUpStyle" PopupDragHandleControlID="panelDragHandle"
        DropShadow="true" />
    <br />--%>
    <%--<div id="divPopUp" style="border-style: solid; border-width: thin; display: none;
        background-color: White; border-color: Black;">
        <asp:Panel runat="Server" ID="panelDragHandle" BackColor="#CCCCCC" BorderColor="Black"
            BorderWidth="1px">
            Approve GIN
        </asp:Panel>
        <asp:Label ID="lblGINNo" runat="server"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSignedByClient" runat="server" Text="Signed By Client"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkSignedByClient" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDateTimeApporved" runat="server" Text="Date Time Apporved"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDateTimeApporved" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeApporved" runat="server"
                        Enabled="True" TargetControlID="txtDateTimeApporved">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDateTimeClientSigned" runat="server" Text="Date Time Client Signed"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDateTimeClientSigned" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtendeDateTimeClientSigned" runat="server"
                        Enabled="True" TargetControlID="txtDateTimeClientSigned">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnClose" runat="server" Text="Close" />
                    </td>
                </tr>
            </table>
        </center>
        <br />
    </div>--%>
</asp:Content>
