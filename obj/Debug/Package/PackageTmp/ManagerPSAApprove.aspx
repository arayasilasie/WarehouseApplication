<%@ Page Title="PSA Approve" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ManagerPSAApprove.aspx.cs" Inherits="WarehouseApplication.ManagerPSAApprove" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
      
        .label
        {
            display: block;
            font-size:small;
           font-family: 'Verdana';
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">

    <script type="text/javascript">
        function dateselect(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future dates!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))

                //var textBox2 = document.getElementById('<%= txtDateTimeClientSigned.ClientID %>');
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

        if (document.forms[0].elements['<%= btnApprove.ClientID %>'] != null)
            document.forms[0].elements['<%= btnApprove.ClientID %>'].style.visibility = "hidden";

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

        function CheckDate(link) {
            var txtvalidater = document.getElementById('<%= txtvalidater.ClientID %>');
            txtvalidater.value = '';

            var emptyCtrl = null;
            var msg = '';

            var tbl = document.getElementById('<%= gvApproval.ClientID %>');
            var alllist = tbl.getElementsByTagName('input');
            for (var k = 0; k < alllist.length; k++) {
                var rowall = alllist[k].parentNode.parentNode;
                var dtall = rowall.getElementsByTagName('input');
                for (var call = 0; call < dtall.length; call++) {
                    if (dtall[call].type == 'text' &&
                            (dtall[call].id.endsWith('txtStatusCompareManager') || dtall[call].id.endsWith('txtStatusCompareManager_0'))) {
                        dtall[call].value = '0';
                    }
                }
            }

            var row = link.parentNode.parentNode.parentNode;
            var dt = row.getElementsByTagName('input');
            var label = row.getElementsByTagName('span');
            var psa = '';
            for (var j = 0; j < label.length; j++) {
                if (label[j].id.endsWith('lblPSA') || label[j].id.endsWith('lblPSA_0')) {
                    psa = label[j].innerText;
                }
            }

            var today = new Date();
            var date = new Date();
            for (var c = 0; c < dt.length; c++) {
                if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtStatusCompareManager') || dt[c].id.endsWith('txtStatusCompareManager_0'))) {
                    dt[c].value = '1';
                }
                if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtDateTimeManagerSigned') || dt[c].id.endsWith('txtDateTimeManagerSigned_0')) &&
                            dt[c].value == '') {
                    msg = msg + '\tEnter manager signed date associated with the selected PSA No:' + psa;
                    if (emptyCtrl == null) emptyCtrl = dt[c];
                    if (msg != '') msg += '<br />';
                }
                else if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtDateTimeManagerSigned') || dt[c].id.endsWith('txtDateTimeManagerSigned_0')) &&
                            today < new Date(dt[c].value)) {
                    msg = msg + '\tManager signed date cannot be greater than today for PSA No:' + psa;
                    if (emptyCtrl == null) emptyCtrl = dt[c];
                    if (msg != '') msg += '<br />';
                }
                if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtTimeManagerSigned') || dt[c].id.endsWith('txtTimeManagerSigned_0')) &&
                            dt[c].value == '') {
                    msg = msg + '\tEnter manager signed time associated with the selected PSA No:' + psa;
                    if (emptyCtrl == null) emptyCtrl = dt[c];
                    if (msg != '') msg += '<br />';
                }
            }
            if (msg != '') {
                emptyCtrl.focus();
                document.getElementById('spanMessage').innerHTML = msg;
                return false;
            }
            document.getElementById('spanMessage').innerHTML = '';
            txtvalidater.value = 'valid';
            return true;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style1" bgcolor="white">
                <caption>
                    <tr>
                        <td>
                            <br />
                            <span id='spanMessage' style='font-family: Verdana; font-size: small; color: #006600'>
                                </span>
                            <uc1:Messages ID="Messages" runat="server" />
                            <asp:TextBox ID="txtvalidater" runat="server" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtvalidater"
                                ErrorMessage="" ValidationGroup="approve" Display="None">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="800px">
                                <tr>
                                    <td class="style2">
                                        <asp:Label ID="lblPSA" runat="server" Text="PSA No :" CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:Label ID="lblWareHouseReceipt" runat="server" Text="Warehouse Receipt :" 
                                            CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:Label ID="lblClientId" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style2">
                                        <asp:Label ID="lblStatus0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                                    </td>
                                    <td class="style3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPSANo" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWareHouseReceipt" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpLIC" runat="server" Width="145px" CssClass="style1" 
                                            AppendDataBoundItems="True">
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
                                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnApprove_Click" ValidationGroup="approve" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" BackColor="#88AB2D" ForeColor="White"
                                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnCancel_Click" />
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
                                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Id,GINStatusID"
                                GridLines="Vertical" Style="font-size: small" Width="100%" 
                                CssClass="label" onselectedindexchanged="gvApproval_SelectedIndexChanged"
                                onrowdatabound="gvApproval_RowDataBound">
                                <Columns>
                                <%--<asp:CommandField ButtonType="Link" SelectText="Approve" ShowSelectButton="true" />--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div id="gv">
                                             <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" 
                                                         CommandName="Select" 
                                                        OnClientClick='CheckDate(this);' ValidationGroup="approve" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="WHRNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouseReceiptNo" Text='<%#Eval("PickupNoticesList[0].WarehouseReceiptNo")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="LICID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLICID" Text='<%#Eval("LeadInventoryControllerID")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="LICShedID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLICShedID" Text='<%#Eval("LICShedID")%>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PSA">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPSA" Text='<%#Eval("GINNumber")%>'
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
                                    <asp:TemplateField HeaderText="CommodityID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodityGradeID" Text='<%#Eval("PickupNoticesList[0].CommodityGradeID")%>'
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
                                    <asp:TemplateField HeaderText="Client Signed" SortExpression="DateTimeClientSigned">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Status
                                                    </td>
                                                    <td>
                                                        Date Time
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblstatusClient" Width="70px" Text='<%#Eval("GINClientStatusName")%>'
                                                            runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDateTimeClientSigned" Width="70px" Text='<%#Eval("ClientSignedDate")%>'
                                                            runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
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
                                                        Date Time
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    <asp:Label ID="lblstatusLIC" Width="70px" Text='<%#Eval("GINLICStatusName")%>'
                                                            runat="server"></asp:Label>
                                                    
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDateTimeLICSigned" Width="70px" Text='<%#Eval("LICSignedDate")%>' runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manager Approval" SortExpression="DateTimeManagerSigned">
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
                                                        <asp:DropDownList ID="drpManagerStatus" Width="70px" runat="server">
                                                            <asp:ListItem Text="Select" Value="1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Accept" Value="11">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="13">
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtStatusCompareManager" runat="server" Text="0" Style="display: none;"></asp:TextBox>
                                                        <asp:CompareValidator ID="cvdrpManagerStatus" runat="server"  Display="None" ControlToValidate="drpManagerStatus" ValidationGroup="approve"
                                                        ErrorMessage="Please insert Manager signed status" Operator="GreaterThan" Type="Integer" ControlToCompare="txtStatusCompareManager">*
                                                        </asp:CompareValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcedrpManagerStatus" runat="server" Enabled="True"
                                                            TargetControlID="cvdrpManagerStatus">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateTimeManagerSigned" Width="70px" runat="server" ReadOnly="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTimeManagerSigned" Width="60px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender" runat="server" AcceptAMPM="True"
                                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeManagerSigned">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator" runat="server" ControlExtender="EarliestTimeExtender"
                                                ControlToValidate="txtTimeManagerSigned" Display="Dynamic" InvalidValueMessage="Please enter a valid time."
                                                SetFocusOnError="True">

                                            </ajaxToolkit:MaskedEditValidator>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeManagerSigned" runat="server"
                                                Enabled="True" TargetControlID="txtDateTimeManagerSigned">
                                            </ajaxToolkit:CalendarExtender>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DateTimeManagerSigned") %>'></asp:Label>
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
    <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowModalPopup"
        PopupControlID="divPopUp" BackgroundCssClass="popUpStyle" PopupDragHandleControlID="panelDragHandle"
        DropShadow="true" />
    <br />
    <div id="divPopUp" style="border-style: solid; border-width: thin; display: none;
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
    </div>
</asp:Content>
