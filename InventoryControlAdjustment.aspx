<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="InventoryControlAdjustment.aspx.cs" Inherits="WarehouseApplication.InventoryControlAdjustment" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .align
        {
            text-align: left;
        }
        .label
        {
            display: block;
            font-size:small;
           font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            width: 659px;
        }
        .style2
        {
            text-align: left;
            width: 526px;
            font-family: Verdana;
        }
        .style3
        {
            width: 526px;
        }
        .style4
        {
            width: 866px;
        }
        .style5
        {
            width: 878px;
        }
        .style6
        {
            width: 878px;
            height: 74px;
        }
    </style>
    <script type="text/javascript">
        function disEnable() {

        }
        function CheckGrids() {
            var pnmessage = document.getElementById('<%= pnmessage.ClientID %>');
            pnmessage.style.display = "none";

            var txtvalidater = document.getElementById('<%= txtvalidater.ClientID %>');
            txtvalidater.value = '';
            var tbl = document.getElementById('<%= gvInventoryInspectors.ClientID %>');
            var isCoffee = document.getElementById('hdfIsCoffee').value;
            if (tbl == null && !isCoffee) {
                document.getElementById('spanMessage').innerHTML = 'Please add atleast one Inspector!';
                document.getElementById('<%= drpInventoryInspector.ClientID %>').focus();
                return false;
            }
            var emptyCtrl = null;
            var msg = '';
            var checkBoxes2 = tbl.getElementsByTagName('input');
            var atleastOneInspectorAdded = false;
            for (var k = 0; k < checkBoxes2.length; k++) {
                var row = checkBoxes2[k].parentNode.parentNode;
                var dt = row.getElementsByTagName('input');
                var label = row.getElementsByTagName('span');
                var insName = '';
                var atleastOneInspectorAdded = false;
                for (var j = 0; j < label.length; j++) {
                    if (label[j].id.endsWith('lblInspectorName') || label[j].id.endsWith('lblInspectorName_0')) {
                        insName = label[j].innerText;
                        atleastOneInspectorAdded = true;
                    }
                }
                for (var c = 0; c < dt.length; c++) {
                    if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtPosition') || dt[c].id.endsWith('txtPosition_0')) &&
                            dt[c].value == '') {
                        msg = msg + '\tPlease enter position for the Inspector:' + insName + ';';
                        if (emptyCtrl == null) emptyCtrl = dt[c];
                        if (msg != '') msg += '\n';
                    }
                }
            }

            if (!atleastOneInspectorAdded && !isCoffee) {
                msg = 'Please add atleast one Inspector!';
                if (emptyCtrl == null) {
                    emptyCtrl = document.getElementById('<%= drpInventoryInspector.ClientID %>');
                }
            }

            if (msg != '') {
                emptyCtrl.focus();
                document.getElementById('spanMessage').innerHTML = msg;
                //alert(msg);
                return false;
            }
            /////////////////////////////////////////////////////////////////////////////
            tbl = document.getElementById('<%= gvInventoryDetail.ClientID %>');
            if (tbl == null && !isCoffee) {
                document.getElementById('spanMessage').innerHTML = 'One or more inventory detail required!';
                return false;
            }
            checkBoxes2 = tbl.getElementsByTagName('input');
            var physicalCount = false;
            var physicalWeight = false;
            var atleastOneInventoryDetailExits = false;
            for (var l = 0; (l < checkBoxes2.length) && !(physicalCount && physicalWeight); l++) {
                var row = checkBoxes2[l].parentNode.parentNode;
                var dt = row.getElementsByTagName('input');
                physicalCount = false;
                physicalWeight = false;
                for (var c = 0; (c < dt.length) && !(physicalCount && physicalWeight); c++) {

                    if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtPhysicalCount') || dt[c].id.endsWith('txtPhysicalCount_0')) &&
                            dt[c].value != '') {
                        atleastOneInventoryDetailExits = true;
                        physicalCount = true;
                    }
                    else if (emptyCtrl == null && dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtPhysicalCount') || dt[c].id.endsWith('txtPhysicalCount_0'))) {
                        atleastOneInventoryDetailExits = true;
                        emptyCtrl = dt[c];
                    }
                    if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtPhysicalWeight') || dt[c].id.endsWith('txtPhysicalWeight_0')) &&
                            dt[c].value != '') {
                        atleastOneInventoryDetailExits = true;
                        physicalWeight = true;
                    }
                    else if (emptyCtrl == null && dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtPhysicalWeight') || dt[c].id.endsWith('txtPhysicalWeight_0'))) {
                        atleastOneInventoryDetailExits = true;
                        emptyCtrl = dt[c];
                    }
                }
            }

            if (!atleastOneInventoryDetailExits && !isCoffee) {
                document.getElementById('spanMessage').innerHTML = 'One or more inventory detail required!';
                return false;
            }
            else if ((!physicalWeight || !physicalCount) && !isCoffee) {
                emptyCtrl.focus();
                document.getElementById('spanMessage').innerHTML = 'Physical count and weight required for at least one Stack ';
                //alert(msg);
                return false;
            }

            document.getElementById('spanMessage').innerHTML = '';
            txtvalidater.value = 'valid';
            if (Page_ClientValidate('<%= btnSave.ValidationGroup %>')) {
                $.blockUI({ message: $('#divMessage') });
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <div id="InventoryAdjustmentForm" class="form" style="width: 62%; margin-left: 22%;
            padding-top: 5px;" align="left">
            <asp:UpdatePanel ID="upMessage" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <span id='spanMessage' style='font-family: Verdana; font-size: small; color: Red'>
                        </span>
                        <asp:Panel ID="pnmessage" runat="server">
                            <uc1:Messages ID="Messages1" runat="server" />
                        </asp:Panel>
                        <asp:TextBox ID="txtvalidater" runat="server" Style="display: none;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtvalidater"
                            ErrorMessage="" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="formHeader" align="center">
                <asp:Label ID="lblShedentry" runat="server" ForeColor="White" Text="Shed Inventory Control"
                    CssClass="style1"></asp:Label></div>
            <div style="border: solid 1px #A5CBB0; background-color: transparent;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" class="style3">
                    <ContentTemplate>
                        <div style="margin-top: 7px;">
                            <asp:Label ID="lblShedNo" runat="server" Text="Shed No:" Width="175px" Height="16px"></asp:Label>
                            <asp:DropDownList ID="drpShed" runat="server" Height="16px" Visible="true" Width="247px"
                                OnSelectedIndexChanged="drpShed_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqShed" runat="server" ControlToValidate="drpShed"
                                ErrorMessage="Please select Shed!" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                TargetControlID="reqShed">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div style="margin-top: 7px;">
                            <hr style="width: 620px" />
                        </div>
                        <div style="margin-top: 7px;">
                            <asp:Label ID="lblLIC" runat="server" Text="Lead Inventory Controller:" Width="175px"></asp:Label>
                            <asp:DropDownList ID="drpLIC" runat="server" Height="20px" Width="247px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="drpLIC_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpLIC"
                                ErrorMessage="Please select Lead Inventory Controller!" ValidationGroup="save"
                                Display="None">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator2">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <%--</td>
        </tr>
        <tr>
         <td>--%>
                        <div style="margin-top: 7px;">
                            <asp:Label ID="lblInventoryReason" runat="server" Text="Inventory Reason:" Width="175px"></asp:Label>
                            <%--</td>
         <td class="style3">--%>
                            <asp:DropDownList ID="drpInventoryReason" runat="server" Height="18px" Width="247px"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpInventoryReason"
                                ErrorMessage="Please select Inventory Reason" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator3">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div style="margin-top: 7px;">
                            <asp:Label ID="lblInventoryControlDate" runat="server" Width="175px" Text="Inventory Control Date : "></asp:Label>
                            <asp:TextBox ID="txtInventoryControlDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtInventoryControlDate_CalendarExtender" runat="server"
                                Enabled="True" TargetControlID="txtInventoryControlDate">
                            </cc1:CalendarExtender>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtInventoryControlDate"
                                ErrorMessage="Please select current date for Inventory Control Date!" Type="Date"
                                MinimumValue="1/1/1900" Display="None" ValidationGroup="save">*</asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="RegularExpressionValidator10_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RangeValidator2">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInventoryControlDate"
                                ErrorMessage="Please select Inventory Control Date" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div style="margin-top: 7px; border-color: Gray; border-style: solid; border-width: thin;
                            width: 600px;">
                            <asp:GridView ID="gvInventoryDetail" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCCC" BorderStyle="None" CssClass="label" ForeColor="Black"
                                PageSize="30" Width="600px" EmptyDataText="There are no stacks available " 
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="PSA Occurred" Visible="true">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="rdoPSAstack" runat="server" Checked="false" OnCheckedChanged="rdoPSAstack_OnCheckedChanged" 
                                                onclick="disEnable()" ClientIDMode="Predictable" AutoPostBack="True"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="StackID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("StackID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Stack No" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStackNo" runat="server" Text='<%# Bind("StackNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="CommodityGradeID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodityGradeID" runat="server" Text='<%# Bind("CommodityGradeID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="System Count" 
                                        Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystemCount" runat="server" Text='<%# Bind("SystemCount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100Px" HeaderText="System Weight" 
                                        Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystemWeight" runat="server" Text='<%# Bind("SystemWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Total Deposit Weight" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPhysicalCount" runat="server" Height="22px" Width="125px"></asp:TextBox>
                                            <asp:CompareValidator ID="cvphysicalCount" runat="server" ErrorMessage="only non-negative Decimal number allowed"
                                                ControlToValidate="txtPhysicalCount" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual"
                                                Display="None" ValueToCompare="0">*</asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender ID="vcephysicalCount" runat="server" Enabled="True"
                                                TargetControlID="cvphysicalCount">
                                            </cc1:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                        <ControlStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Total Delivery Weight" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPhysicalWeight" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="cvphysicalWeight" runat="server" ErrorMessage="only non-negative Decimal number allowed"
                                                ControlToValidate="txtPhysicalWeight" Type="Double" ValidationGroup="save" Operator="GreaterThanEqual"
                                                Display="None" ValueToCompare="0">*</asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender ID="vcephysicalweight" runat="server" Enabled="True"
                                                TargetControlID="cvphysicalWeight">
                                            </cc1:ValidatorCalloutExtender>
                                        </ItemTemplate>
                                        <ControlStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="isCoffee" Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfIsCoffee" runat="server" Value='<%# Bind("isCoffee") %>'/>
                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#88AB2D" Font-Bold="false" Font-Size="small" 
                                    ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="margin-top: 7px;">
                    <hr />
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-top: 7px;">
                            <asp:Label ID="lblInventoryInspector" runat="server" Text="Operation Controller:"
                                Width="175px"></asp:Label>
                            <asp:DropDownList ID="drpInventoryInspector" runat="server" Height="18px" Width="247px"
                                AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpInventoryInspector"
                                ErrorMessage="Please select first" ValidationGroup="addIns" Display="None">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator4">
                            </cc1:ValidatorCalloutExtender>
                            <asp:Button ID="btnAddInventroyinspector" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                CssClass="style1" ForeColor="White" OnClick="btnAddInventroyinspector_Click"
                                Text="Add" Width="100px" ValidationGroup="addIns" />
                        </div>
                        <div style="margin-top: 7px; border-color: Gray; border-style: solid; border-width: thin;
                            width: 600px;">
                            <asp:GridView ID="gvInventoryInspectors" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" CssClass="label" ForeColor="Black"
                                GridLines="Both" PageSize="30" Width="600px" EmptyDataText="Please add Inventory Inspectors"
                                ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="" Visible="true">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lbtnRemoveInsp" runat="server" 
                                            OnClick="lbtnRemoveInsp_Click" >Remove</asp:LinkButton>--%>
                                            <a id="linkRemoveInsp" onserverclick="lbtnRemoveInsp_Click" href='#' inspectorid='<%# Eval("InspectorID") %>'
                                                runat="server">Remove</a>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="InventoryID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInventoryID" runat="server" Text='<%# Bind("InventoryID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="InspectorID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInspectorID" runat="server" Text='<%# Bind("InspectorID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Inspector Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInspectorName" runat="server" Text='<%# Bind("InspectorName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Position" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPosition" runat="server" Height="22px" Width="125px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ControlStyle Width="150px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#88AB2D" Font-Bold="false" Font-Size="small" 
                                    ForeColor="White" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upButtons" runat="server">
                    <ContentTemplate>
                        <div id='divMessage' class='divMessage' style="display: none;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/saving.gif" AlternateText="Saving..." />
                        </div>
                        <div class="controlContainer" style="margin-top: 20px;">
                            <div align="center" style="float: left; margin-left: 100px;">
                                <asp:Button ID="btnSave" runat="server" BackColor="#88AB2D" BorderStyle="None" CssClass="style1"
                                    ForeColor="White" OnClick="btnSave_Click" Text="Save" ValidationGroup="save"
                                    OnClientClick='CheckGrids();' Width="100px" />
                            </div>
                            <div align="center" style="float: left; margin-left: 20px">
                                <asp:Button ID="btnCancel" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                    CssClass="style1" ForeColor="White" OnClick="btnCancel_Click" Text="Clear" Width="100px" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upButtons">
                    <ProgressTemplate>
                        <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                            Working...</a>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
</asp:Content>
