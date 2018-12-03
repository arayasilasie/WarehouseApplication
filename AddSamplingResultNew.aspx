<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="AddSamplingResultNew.aspx.cs" Inherits="WarehouseApplication.AddSamplingResultNew" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValueChanged(txtNoOfBagsID, rangValidaterNoOfBagsID, reqValidaterNoOfBagsID, rbSampleStatusID) {
            var txtNoOfBags = document.getElementById('<%= txtNumberOfBags.ClientID %>');
            var reqValidaterNoOfBags = document.getElementById('<%= reqValidaterNoOfBags.ClientID %>');
            var rangValidaterNoOfBags = document.getElementById('<%= rangValidaterNoOfBags.ClientID %>');
            var driverNotFoundSelected = document.getElementById('<%= rbSampleStatus.ClientID %>');
            var drpBagType = document.getElementById('<%= drpBagType.ClientID %>');
            var reqValidatorBagType = document.getElementById('<%= reqValidatorBagType.ClientID %>');
            var txtSamplerComments = document.getElementById('<%= txtSamplerComments.ClientID %>');
            var chkHasChemicalOrPetrol = document.getElementById('<%= chkHasChemicalOrPetrol.ClientID %>');
            var chkHasLiveInsect = document.getElementById('<%= chkHasLiveInsect.ClientID %>');
            var chkHasMoldOrFungus = document.getElementById('<%= chkHasMoldOrFungus.ClientID %>');
            var chkIsPlompOk = document.getElementById('<%= chkIsPlompOk.ClientID %>');
            var reqRemark = document.getElementById('<%= reqRemark.ClientID %>');
            var radio = driverNotFoundSelected.getElementsByTagName("input");
            //alert(radio[2].checked);
            if (radio[2].checked) {
                ValidatorEnable(reqValidaterNoOfBags, false);
                ValidatorEnable(rangValidaterNoOfBags, false);
                ValidatorEnable(reqValidatorBagType, false);
                ValidatorEnable(reqRemark, false);
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
                }
                txtNoOfBags.disabled = true;
                txtSamplerComments.disabled = true;
                chkHasChemicalOrPetrol.disabled = true;
                drpBagType.disabled = true;
                chkHasLiveInsect.disabled = true;
                chkHasMoldOrFungus.disabled = true;
                chkIsPlompOk.disabled = true;
                //
                txtNoOfBags.value = '';
                txtSamplerComments.value = '';
                chkHasChemicalOrPetrol.disabled = true;
                drpBagType.selectedIndex = 0;
                chkHasLiveInsect.checked = false;
                chkHasMoldOrFungus.checked = false;
                chkIsPlompOk.checked = false;
            }
            else {
                ValidatorEnable(reqValidaterNoOfBags, true);
                ValidatorEnable(rangValidaterNoOfBags, true);
                ValidatorEnable(reqValidatorBagType, true);
                txtNoOfBags.disabled = false;
                txtSamplerComments.disabled = false;
                chkHasChemicalOrPetrol.disabled = false;
                drpBagType.disabled = false;
                chkHasLiveInsect.disabled = false;
                chkHasMoldOrFungus.disabled = false;
                chkIsPlompOk.disabled = false;
                chkIsPlompOk.checked = true;
            }
        }

        function dateselectDateOfResult(sender) {
            var txtvalidater = document.getElementById('<%= txtvalidater.ClientID %>');
            txtvalidater.innerHTML = '';
            txtvalidater.value = '';
            document.getElementById('spanMessage').innerHTML = "";
            var dateSampleCodeGenerate = document.getElementById('<%= txtSampleCodeGeneratedDateStamp.ClientID %>').value;
            var timeSampleCodeGenerate = document.getElementById('<%= txtSampleCodeGeneratedTimeStamp.ClientID %>').value;
            var dateOfResultReceive = document.getElementById('<%= txtResultReceivedDate.ClientID %>').value;
            var timeOfResultReceive = document.getElementById('<%= txtResultReceivedTime.ClientID %>').value;
            var today = new Date();
            if (new Date(dateSampleCodeGenerate + ' ' + timeSampleCodeGenerate) > new Date(dateOfResultReceive + ' ' + timeOfResultReceive)) {
                //alert("Date time Of result received must be greater or equal than Sample Code generated time Date " + dateSampleCodeGenerate);
                document.getElementById('spanMessage').innerHTML = "Date time Of result received must be greater or equal to Sample Code generated time Date "
                       + dateSampleCodeGenerate + ' ' + timeSampleCodeGenerate;
                if (new Date(dateSampleCodeGenerate) > new Date(dateOfResultReceive)) {
                    document.getElementById('<%= txtResultReceivedDate.ClientID %>').focus();
                }
                else
                    document.getElementById('<%= txtResultReceivedTime.ClientID %>').focus();
                return false;
            }
            else if (today < new Date(dateOfResultReceive + ' ' + timeOfResultReceive)) {
                document.getElementById('spanMessage').innerHTML = "Date time Of result received can't be greater than today!";
                document.getElementById('<%= txtResultReceivedDate.ClientID %>').focus();
                document.getElementById('<%= txtResultReceivedTime.ClientID %>').focus();
                return false;
            }

            document.getElementById('spanMessage').innerHTML = '';
            txtvalidater.value = 'valid';
            txtvalidater.innerHTML = 'valid';
            if (Page_ClientValidate('<%= btnSaveSampleResult.ValidationGroup %>')) {
                document.getElementById('buttons').style.display = "none";
                $.blockUI({ message: $('#divMessage') });
            }
        }

        function ValudateNoOfBag(sender) {

            var noOfBag = document.getElementById('<%= txtNumberOfBags.ClientID %>').value;
            document.getElementById('spanMessage').innerHTML = "";
            var arrivalNoOfBag = document.getElementById('<%= txtArrivalNoOfBags.ClientID %>').value;
            if (arrivalNoOfBag < noOfBag) {
                //alert("No of bag can't be greater than arrival no of bags " + arrivalNoOfBag);
                document.getElementById('spanMessage').innerHTML = "No of bag can't be greater than arrival no of bags " + arrivalNoOfBag;
                document.getElementById('<%= txtNumberOfBags.ClientID %>').value = "";
            }
        }

        function ValidateRemark(sender) {
            var reqRemark = document.getElementById('<%= reqRemark.ClientID %>');
            if (sender.checked) {

                ValidatorEnable(reqRemark, false);

                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
                }
            }
            else {
                ValidatorEnable(reqRemark, true);
            }

        }
    </script>
    <style type="text/css">
        .style1
        {
            font-size: 12;
        }
        .FormControldiv1
        {
            background-color: #E4EADB;
            height: 40px;
            width: 550px;
        }
        .FormControldiv2
        {
            background-color: White;
            height: 40px;
            width: 550px;
        }
        .FormControldiv3
        {
            height: 90px;
            width: 550px;
            background-color: #E4EADB;
        }
        .middle
        {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <%--MESSAGE AREA--%>
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txtArrivalNoOfBags" runat="server" Text="" Style="display: none;"
                    Width="202px" Height="16px"></asp:TextBox>
                <asp:TextBox ID="txtSampleCodeGeneratedDateStamp" runat="server" Text="" Style="display: none;"
                    Width="202px" Height="16px"></asp:TextBox>
                    <asp:TextBox ID="txtSampleCodeGeneratedTimeStamp" runat="server" Text="" Style="display: none;"
                    Width="202px" Height="16px"></asp:TextBox>
                <span id='spanMessage' style='font-family: Verdana; font-size: small; color: Red'>
                </span>
                <asp:TextBox ID="txtvalidater" runat="server" Style="display: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtvalidater"
                                ErrorMessage="" ValidationGroup="save" Display="None">*</asp:RequiredFieldValidator>
                <%--<div class="MessageArea" style="padding-left:5px; margin-bottom:5px; width:100%; background-color:White"> 
</div>--%>
                <div id="MessageArea">
                    <uc1:Messages ID="Messages" runat="server" style="padding-top: 10px;" />
                </div>
                <div id="samplingResultForm" class="form" style="width: 50%; margin-left: 22%; padding-top: 5px;"
                    align="left">
                    <div class="formHeader" align="center">
                        SAMPLING RESULT REGISTRATION
                    </div>
                    <div style="border: solid 1px #A5CBB0; background-color: transparent;">
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer" style="margin-top: 7px;">
                            <div class="leftControl">
                                <asp:Label ID="lblSampleCode" runat="server" Text="Sample Code: " Width="202px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtSampleCode" runat="server" Visible="True" ReadOnly="true" Width="200px"
                                Enabled="False" BorderStyle="None" CssClass="label" BackColor="White" 
                                    ForeColor="Black"></asp:TextBox>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblSamplerName" runat="server" Text="Sampler Name: " Width="202px"
                                    Height="21px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtSamplerName" runat="server" Height="21px" Width="200px" ReadOnly="True"
                                Enabled="False" BorderStyle="None" CssClass="label" BackColor="White" 
                                    ForeColor="Black"></asp:TextBox>
                            </div>
                        </div>
                        <%--Code Generaded Date--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblCodeGeneratedDateLabel" runat="server" Width="202px" Height="21px"
                                    Text="Sample Code Generated Date: "></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblCodeGeneratedDate" runat="server" Width="200px"></asp:Label>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblResultReceivedDate" runat="server" Width="202px" Height="21px"
                                    Text="Result Received Date: "></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtResultReceivedDate" runat="server" Width="200px" class="TextBox"
                                    ReadOnly="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtResultReceivedDate_CalendarExtender" runat="server"
                                    Enabled="True" TargetControlID="txtResultReceivedDate">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtResultReceivedDate"
                                    ErrorMessage="Result Received Date is required field" ValidationGroup="save"
                                    Display="None">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator8_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="RangeValidator2" ValidationGroup="Save" Type="Date" ControlToValidate="txtResultReceivedDate" Display="Dynamic" ForeColor="Tomato"
                                
                                runat="server" ErrorMessage="Invalid date."></asp:RangeValidator>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblResultReceivedTime" runat="server" Text="Result Received Time: "
                                    Width="202px">
                                </asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtResultReceivedTime" runat="server" Width="200px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="True"
                                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtResultReceivedTime">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender4"
                                    ControlToValidate="txtResultReceivedTime" Display="None" InvalidValueMessage="Please enter a valid time."
                                    SetFocusOnError="True">*</cc1:MaskedEditValidator>
                                <cc1:ValidatorCalloutExtender ID="MaskedEditValidator4_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="MaskedEditValidator4">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer" style="height: 70px;">
                            <div class="leftControl">
                                <asp:Label ID="Label8" runat="server" Width="202px" Text="Sampling Status: " Height="18px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:RadioButtonList RepeatDirection="Vertical" ID="rbSampleStatus" runat="server"
                                    Height="25px" OnSelectedIndexChanged="rdSampleStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="Sample Result Received" Value="2" Selected="True" onclick="ValueChanged(this);"></asp:ListItem>
                                    <asp:ListItem Text="Segregation Requested" Value="3" onclick="ValueChanged(this);"></asp:ListItem>
                                    <asp:ListItem Text="Driver Not Found" Value="4" onclick="ValueChanged(this);"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                            </div>
                            <div class="rightControl">
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblBagType" runat="server" Text="Bag Type: " Width="202px" Height="16px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpBagType" runat="server" Height="21px" Width="200px" Enabled="true"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqValidatorBagType" runat="server" ControlToValidate="drpBagType"
                                    ErrorMessage="Bag Type is Required" ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="reqValidatorBagType">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>

                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label6" runat="server" Text="Sample Type: " Width="202px" Height="16px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="drpSampleType" runat="server" Height="21px" Width="200px" Enabled="true"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpSampleType"
                                    ErrorMessage="Sample Type is Required" ValidationGroup="save" Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="lblNumberOfBags" runat="server" Text="Number Of Bags: " Width="202px"
                                    Height="16px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtNumberOfBags" runat="server" Width="200px" Enabled="true" class="TextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqValidaterNoOfBags" runat="server" ControlToValidate="txtNumberOfBags"
                                    onchange="ValudateNoOfBag(this)" ErrorMessage="Number Of Bags is Required" ValidationGroup="save"
                                    Display="None">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="txtNoOfBags_ValidatorCalloutExtender" runat="server"
                                    Enabled="True" TargetControlID="reqValidaterNoOfBags">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="rangValidaterNoOfBags" runat="server" ControlToValidate="txtNumberOfBags"
                                    ErrorMessage="Invalid Entry!" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                                    ValidationGroup="save" Display="None">*</asp:RangeValidator>
                                <cc1:ValidatorCalloutExtender ID="txtNoOfBags_ValidatorCalloutExtender1" runat="server"
                                    Enabled="True" TargetControlID="rangValidaterNoOfBags">
                                </cc1:ValidatorCalloutExtender>
                                <%--<asp:CompareValidator ID="cvNoOfBags" runat="server" ErrorMessage="Number Of Bags Can't be greater than Arrival No of Bags"
                                    ControlToCompare="txtArrivalNoOfBags" ControlToValidate="txtNumberOfBags" Operator="LessThanEqual"
                                    Type="Integer" ValidationGroup="save" Display="None" SetFocusOnError="True">*
                                </asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                    TargetControlID="cvNoOfBags">
                                </cc1:ValidatorCalloutExtender>--%>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label1" runat="server" Width="202px" Text="Is Plomp Ok: " Height="18px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:CheckBox ID="chkIsPlompOk" runat="server" Checked="true" onclick="ValidateRemark(this);">
                                </asp:CheckBox>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label2" runat="server" Text="Has Live Insects: " Width="202px" Height="18px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:CheckBox ID="chkHasLiveInsect" runat="server"></asp:CheckBox>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label3" runat="server" Width="202px" Text="Has Mold or Fungus: " Height="18px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:CheckBox ID="chkHasMoldOrFungus" runat="server"></asp:CheckBox>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div class="leftControl">
                                <asp:Label ID="Label4" runat="server" Text="Has Chemical or Petrol: " Width="202px"
                                    Height="20px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:CheckBox ID="chkHasChemicalOrPetrol" runat="server"></asp:CheckBox>
                            </div>
                        </div>
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer" style="height: 70px">
                            <div class="leftControl">
                                <asp:Label ID="Label5" runat="server" Width="202px" Style="vertical-align: top" Text="Other Sampler Remarks: "
                                    Height="17px"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtSamplerComments" runat="server" class="TextBox" Height="68px"
                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqRemark" runat="server" ControlToValidate="txtSamplerComments"
                                    onchange="ValudateNoOfBag(this)" ErrorMessage="Remark is required." ValidationGroup="save"
                                    Display="None" Enabled="false">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                    TargetControlID="reqRemark">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <br />
                        <%--VOUCHER NUMBER--%>
                        <div class="controlContainer">
                            <div id="buttons">
                                <div align="center" style="float: left; margin-left: 30px">
                                    <asp:Button ID="btnSaveSampleResult" runat="server" Text="Save" OnClick="btnSaveSampleResult_Click"
                                        CssClass="style1" OnClientClick="dateselectDateOfResult(this);" Width="100px"
                                        ValidationGroup="save" BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" />
                                </div>
                                <div align="center" style="float: left; margin-left: 20px">
                                    <asp:Button ID="btnNext" runat="server" Text="Next" BackColor="#88AB2D" ForeColor="#FFFFCC"
                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnNext_Click" />
                                </div>
                                <div align="center" style="float: left; margin-left: 20px">
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" BackColor="#88AB2D" ForeColor="#FFFFCC"
                                        Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnClear_Click" />
                                </div>
                            </div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                        Working...</a>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </div>
                <div id='divMessage' class='divMessage' style="display: none;">
                    <asp:Image runat="server" ImageUrl="Images/saving.gif" AlternateText="Saving..." />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
