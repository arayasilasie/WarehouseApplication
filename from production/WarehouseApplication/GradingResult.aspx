<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" 
    CodeBehind="GradingResult.aspx.cs" Inherits="WarehouseApplication.GradingResult" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Src="UserControls/GradingResultControlNew.ascx" TagName="GradingResultControlNew"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">

        function DrpChange(drp, gr) {
           // alert(gr);
            var cgv = document.getElementById(gr);
            var cdrp = document.getElementById(drp);
            cdrp.value = cgv.value;
            //alert(checkBoxes2.value); //checkBoxes2[0].id + ' ' + checkBoxes2[1].id);// + ' ' + checkBoxes2[2].id + ' ' + checkBoxes2[3].id + ' ');
        }

        function ValueChanged(chk) {
            var drpCommodityClass = document.getElementById('<%= drpCommodityClass.ClientID %>');
            if (chk.checked) {
                drpCommodityClass.disabled = false;
            }
            else {
                drpCommodityClass.disabled = true;
            }

            $.blockUI({ message: $('#divMessage') });
        }

        var gradeMax = null;
        var gradeMin = null;

        $().ready(function () {
            GradeChange(null);
        });

        function ResetMaxMin(ddl) {
            gradeMax = null;
            gradeMin = null;
        }

        function GradeChange(ddl) {
            var grade = document.getElementById('<%= drpGrade.ClientID %>').value;
            if (grade == '') {
                gradeMax = null;
                gradeMin = null;
                return;
            }
            var classID = document.getElementById('<%= drpCommodityClass.ClientID %>').value;
            var factorgroup = document.getElementById('<%= drpCommodityFactorGroup.ClientID %>').value;
            $.ajax({
                type: 'post',
                url: 'GradingResult.aspx/GetMaxMinOfGrade',
                data: "{ grade:'" + grade + "', commodityFactorGroupId: '" + factorgroup + "', classId: '" + classID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: maxMinLoaded,
                error: loadError,
                complete: loadCommplete
            });
        }

        function maxMinLoaded(data) {
            gradeMax = data.d.Max;
            gradeMin = data.d.Min;
        }

        function loadError(xhr, textStatus, error) {
            alert('error');
        }
        function loadCommplete(data) {
        }

        function dateselectDateOfResult(sender) {
            var pnmessage = document.getElementById('<%= pnmessage.ClientID %>');
            pnmessage.style.display = "none";
            var txtvalidater = document.getElementById('<%= txtvalidater.ClientID %>');
            txtvalidater.innerHTML = '';
            txtvalidater.value = '';

            document.getElementById('spanMessage').innerHTML = "";

            var dateCodeGenerate = document.getElementById('<%= lblCodeGeneratedDateValue.ClientID %>').innerText;
            var dateCodeGeneratetime = document.getElementById('<%= lblCodeGeneratedTimeValue.ClientID %>').innerText;

            var dateOfResultReceive = document.getElementById('<%= txtDateRecived.ClientID %>').value;
            var dateOfResultReceivetime = document.getElementById('<%= txtTimeRecived.ClientID %>').value;
            var today = new Date();
            if (new Date(dateCodeGenerate + ' ' + dateCodeGeneratetime) > new Date(dateOfResultReceive + ' ' + dateOfResultReceivetime)) {
                document.getElementById('spanMessage').innerHTML = "Date time Of result received must be greater or equal to Code generated time Date "
                  + dateCodeGenerate + ' ' + dateCodeGeneratetime;
                if (new Date(dateCodeGenerate) > new Date(dateOfResultReceive)) {
                    document.getElementById('<%= txtDateRecived.ClientID %>').focus();
                }
                else {
                    document.getElementById('<%= txtTimeRecived.ClientID %>').focus();
                }
                sender.disabled = false;
                
                return false;
            }
            else if (today < new Date(dateOfResultReceive + ' ' + dateOfResultReceivetime)) {
                document.getElementById('spanMessage').innerHTML = "Date time Of result received can't be greater than today!";
                document.getElementById('<%= txtDateRecived.ClientID %>').focus();
                document.getElementById('<%= txtTimeRecived.ClientID %>').focus();
                sender.disabled = false;
                return false;
            }

            return CheckGrids();

        }

        function CheckGrids() {
            var btnSave = document.getElementById('<%= btnSave.ClientID %>');
            var txtvalidater = document.getElementById('<%= txtvalidater.ClientID %>');
            txtvalidater.innerHTML = '';
            txtvalidater.value = '';

            var tbl = document.getElementById('<%= gvGradingFactors1.ClientID %>');
            var emptyCtrl = null;
            var msg = '';
            if (tbl == null) {
                document.getElementById('spanMessage').innerHTML = '';
                txtvalidater.value = 'valid';
                txtvalidater.innerHTML = 'valid';
                if (Page_ClientValidate('<%= btnSave.ValidationGroup %>')) {
                    $.blockUI({ message: $('#divMessage') });
                }
                return true;
            }
            var checkBoxes2 = tbl.getElementsByTagName('input');
            var totalSum = 0;
            var preId = '';
            
            for (var k = 0; k < checkBoxes2.length; k++) {
                var row = checkBoxes2[k].parentNode.parentNode;
                var dt = row.getElementsByTagName('input');
                var label = row.getElementsByTagName('span');
                var insName = '';
                var isInTotal = 'false';
               
                for (var j = 0; j < dt.length; j++) {
                    if (dt[j].id.endsWith('txtIsInTotalValue') || dt[j].id.endsWith('txtIsInTotalValue_0')) {
                        isInTotal = dt[j].value;
                       
                    }
                }
                if (isInTotal.toLowerCase() == 'true') {
                    for (var c = 0; c < dt.length; c++) {
                        if (dt[c].type == 'text' &&
                            (dt[c].id.endsWith('txtdrpGradeResult') || dt[c].id.endsWith('txtdrpGradeResult_0')) &&
                            preId != dt[c].id &&
                            dt[c].value != '' && !isNaN(parseFloat(dt[c].value))) {
                            totalSum = totalSum + parseFloat(dt[c].value);
                            preId = dt[c].id;
                            
                        }
                    }
                }
            }
            
            if (gradeMax == null || gradeMin == null) {
                document.getElementById('spanMessage').innerHTML = '';
                txtvalidater.value = 'valid';
                txtvalidater.innerHTML = 'valid';
                if (Page_ClientValidate('<%= btnSave.ValidationGroup %>')) {
                    $.blockUI({ message: $('#divMessage') });
                }
                return true;
            }
            
            if (isNaN(parseInt(gradeMax)) || isNaN(parseInt(gradeMin))) {
                document.getElementById('spanMessage').innerHTML = 'Please first specify the min and max values of the grade!';
                return false;
            }

            
            if (totalSum >= parseInt(gradeMax) || totalSum < parseInt(gradeMin)) {
                document.getElementById('spanMessage').innerHTML = 'Total value for grade factor  (' + totalSum.toString()
                    + ') is outside of range max(' + gradeMax.toString() + ') and min(' + gradeMin.toString() + ')!. For max it is exclusive.';
                return false;
            }

            document.getElementById('spanMessage').innerHTML = '';
            txtvalidater.value = 'valid';
            txtvalidater.innerHTML = 'valid';

            if (Page_ClientValidate('<%= btnSave.ValidationGroup %>')) {
                //sender.style.display = "none";
                document.getElementById('buttons').style.display = "none";
                $.blockUI({ message: $('#divMessage') });
            }

            return true;

        }
    </script>
    <style type="text/css">
        .HiddenCol
        {
            display: none;
        }
        
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
        .label1
        {
            font-size: small;
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
        .style6
        {
            width: 878px;
            height: 74px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td class="style1">
                            <span id='spanMessage' style='font-family: Verdana; font-size: small; color: Red'>
                            </span>
                            <asp:TextBox ID="txtvalidater" runat="server" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtvalidater"
                                ErrorMessage="" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                                <asp:Panel ID="pnmessage" runat="server">
                            <uc1:Messages ID="Messages1" runat="server" /></asp:Panel>
                        </td>
                    </tr>
                </table>
                <div id="gradingResultForm" class="form" style="width: 45%; padding-top: 0%; float: left;">
                    <div class="formHeader" align="center">
                        <asp:Label ID="lblInformation" runat="server" Text="Grading Result Entry"></asp:Label>
                    </div>
                    <div class="formControlHolders">
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGraderCode" runat="server" Text="Grading Code : " CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblGradingCodeValue" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGraderCupper" runat="server" Text="Grader/Cupper : " CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblGraderCupperValue" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblCodeGeneratedDate" runat="server" Text="Code Generated Date : "
                                    CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblCodeGeneratedDateValue" runat="server" CssClass="label1"></asp:Label>
                                <asp:Label ID="lblCodeGeneratedTimeValue" runat="server" CssClass="label1"></asp:Label>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblCommodity" runat="server" Text="Commodity : " CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtCommodity" runat="server" Width="200px" ReadOnly="true" 
                                    Enabled="False" BorderStyle="None" CssClass="label"></asp:TextBox>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label1" runat="server" Text="Is for Deposit? : " CssClass="label"></asp:Label>
                            </div>
                            
                            <div class="rightControl">
                                <asp:RadioButton ID="RdBtndeposit" Text="Deposit" runat="server" 
                                    CssClass="label" Width="87px" GroupName="isdeposit" Enabled="false" />
                            <asp:RadioButton ID="RdBtnexport" Text="Export" runat="server" CssClass="label" 
                                    Width="102px" GroupName="isdeposit" Enabled="false" />
                            </div>
                        </div>

                        <div class="controlContainer" >
                            <div class="leftControl">
                                <asp:Label ID="lblCommodityFactorGroup" runat="server" Text="Grading Factor Group:"
                                    CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpCommodityFactorGroup" runat="server" Height="20px" Width="200px"
                                    OnSelectedIndexChanged="drpCommodityFactorGroup_SelectedIndexChanged" AutoPostBack="True"
                                    AppendDataBoundItems="True" onchange="ResetMaxMin(this);">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqdrpCommodityFactorGroup" runat="server" ErrorMessage="Please Select one value"
                                    ControlToValidate="drpCommodityFactorGroup" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcereqdrpCommodityFactorGroup" runat="server"
                                    Enabled="True" TargetControlID="reqdrpCommodityFactorGroup">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblComodityClass" runat="server" Text="Commodity Class" Visible="true"
                                    CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpCommodityClass" runat="server" Width="200px" OnSelectedIndexChanged="drpCommodityClass_SelectedIndexChanged"
                                    AutoPostBack="True" AppendDataBoundItems="True" Visible="true" Enabled="false" onchange="ResetMaxMin(this);">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdrpCommodityClass" runat="server" ErrorMessage="Please Select one value"
                                    ControlToValidate="drpCommodityClass" ValidationGroup="save" 
                                    Enabled="False">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcedrpCommodityClass" runat="server"
                                    Enabled="True" TargetControlID="rfvdrpCommodityClass">
                                </cc1:ValidatorCalloutExtender>
                                <br />
                                <asp:CheckBox ID="chkChangeClass" runat="server" Text="Change Class" 
                                    onclick="ValueChanged(this);" 
                                    oncheckedchanged="chkChangeClass_CheckedChanged" AutoPostBack="True" />
                            </div>
                        </div> 
                        <%-- --%>
                        <div class="controlContainer" style="margin-top: 15px">
                            <div class="leftControl">
                                <asp:Label ID="lblGrade" runat="server" Text="Grade:" CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpGrade" runat="server" Height="18px" Width="200px" AppendDataBoundItems="True" 
                                 onchange="GradeChange(this);">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdrpGrade" runat="server" ErrorMessage="Please Select one value"
                                    ControlToValidate="drpGrade" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcerfvdrpGrade" runat="server" Enabled="True" TargetControlID="rfvdrpGrade">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="controlContainer" style="margin-top: 15px">
                            <div class="leftControl">
                                <asp:Label ID="lblProductionYear" runat="server" Text="Production Year:" CssClass="label"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpProductionYear" runat="server" Height="18px" Width="200px" AppendDataBoundItems="True" 
                                 >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvdrpProductionYear" runat="server" ErrorMessage="Please Select one value"
                                    ControlToValidate="drpProductionYear" ValidationGroup="save">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvdrpProductionYear">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%-- --%>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGradeCompletedDAte" runat="server" CssClass="label" Text="Grade Completed Date : "></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtDateRecived" runat="server" Width="200px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" Enabled="True"
                                    TargetControlID="txtDateRecived">
                                </cc1:CalendarExtender>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDateRecived"
                                    ErrorMessage="Grade Completed Date only accept  date" Type="Date" MaximumValue="12/30/2018"
                                    MinimumValue="1/1/1900" Display="None" ValidationGroup="save">*</asp:RangeValidator>
                                <cc1:ValidatorCalloutExtender ID="RegularExpressionValidator10_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDateRecived"
                                    ErrorMessage="Grade Completed Date is required field" ValidationGroup="save"
                                    Display="None">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblGradeCompletedTime" runat="server" CssClass="label" Text="Grade Completed Time : "></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtTimeRecived" runat="server" Width="200px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="True"
                                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeRecived">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                    ControlToValidate="txtTimeRecived" Display="None" InvalidValueMessage="Please enter a valid time."
                                    SetFocusOnError="True">*</cc1:MaskedEditValidator>
                                <cc1:ValidatorCalloutExtender ID="MaskedEditValidator4_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="MaskedEditValidator4">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTimeRecived"
                                    ErrorMessage="Grade Completed Time is required field" ValidationGroup="save"
                                    Display="None">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%-- --%>
                        <div id="buttons" style="float: left; margin-left: 20px" align="center">
                            <div style="float: left; margin-left: 1px">
                                <asp:Button ID="btnSave" runat="server" BackColor="#88AB2D" BorderStyle="None" OnClientClick="dateselectDateOfResult(this);"
                                    CssClass="style1" ForeColor="White" OnClick="btnSave_Click" Text="Save" ValidationGroup="save"
                                    Width="100px" />
                            </div>
                            <div align="center" style="float: left; margin-left: 15px">
                                <asp:Button ID="btnNext" runat="server" Text="Next" BackColor="#88AB2D" ForeColor="#FFFFCC"
                                    Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnNext_Click" />
                            </div>
                            <div style="float: left; margin-left: 15px">
                                <asp:Button ID="btnCancel" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                    CssClass="style1" ForeColor="White" OnClick="btnCancel_Click" Text="Clear" Width="100px" />
                            </div>
                        </div>
                        <%-- --%>
                        <div class="controlContainer">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                        Working...</a>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>
                <%-- --%>
                <div class="gridContainer" style="float: left; padding-top: 6%; margin-left: 50px;">
                    <div class="formControlHolders" style="border-width: 2px;">
                        <asp:GridView ID="gvGradingFactors1" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                            CssClass="label" ForeColor="Black" GridLines="Both" OnRowDataBound="gvGradingFactors1_RowDataBound"
                            PageSize="30">
                            <Columns>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="TypeValueType" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeValueType" runat="server" Text='<%# Bind("TypeValueType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="TypeValueType" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPossibleValues" runat="server" Text='<%# Bind("PossibleValues") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="IsInTotalValue" Visible="true"
                                    HeaderStyle-CssClass="HiddenCol" ItemStyle-CssClass="HiddenCol">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIsInTotalValue" runat="server" ReadOnly="true" Text='<%# Bind("isInTotalValue") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="IsSemiWashedFactor" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsSemiWashedFactor" runat="server" Text='<%# Bind("IsSemiWashedFactor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="IsUnderScreenFactor" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsUnderScreenFactor" runat="server" Text='<%# Bind("IsUnderScreenFactor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="UnderScreenLimit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnderScreenLimit" runat="server" Text='<%# Bind("UnderScreenLimit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="IsMoistureContentFactor"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMoistureContentFactor" runat="server" Text='<%# Bind("IsMoistureContentFactor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="IsType" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsType" runat="server" Text='<%# Bind("IsType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="MoistureContentLimit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoistureContentLimit" runat="server" Text='<%# Bind("MoistureContentLimit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                               </asp:TemplateField>
                                <%-- <asp:TemplateField ControlStyle-Width="50Px" HeaderText="MoistureContentLimitexp" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoistureContentLimit" runat="server" Text='<%# Bind("MoistureContentLimitexp") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="MoistureFailFactorGroup"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoistureFailFactorGroup" runat="server" Text='<%# Bind("MoistureFailFactorGroup") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="450Px" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Grading Factor" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradingFactorName" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblGradingFactorName0" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ControlStyle Width="250px" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="HiddenResult" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResult" runat="server" Text='<%# Bind("Result") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField ControlStyle-Width="50Px" HeaderText="Result">
                                    <ItemTemplate>
                                        <uc2:GradingResultControlNew ID="ResultInputControl" runat="server" Visible="true" />
                                    </ItemTemplate>
                                    <ControlStyle Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="small" />
                        </asp:GridView>
                    </div>
                </div>
                <div id='divMessage' class='divMessage' style="display: none;">
                     <asp:Image ID="Image1" runat="server" ImageUrl="Images/saving.gif" AlternateText="Saving..." />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
