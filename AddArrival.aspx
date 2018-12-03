<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddArrival.aspx.cs" Inherits="WarehouseApplication.AddArrivalNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="UserControls/ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>

<%@ Register src="Messages.ascx" tagname="Messages" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="messages.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ValidateRegion(Source, args) {
            var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
            var cboRegion = document.getElementById('<%= cboRegion.ClientID %>');

            if (chkAnswer.checked == true) {
                if (cboRegion.value == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }

        function ValidateZone(Source, args) {
            var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
            var cboZone = document.getElementById('<%= cboZone.ClientID %>');

            if (chkAnswer.checked == true) {
                if (cboZone.value == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }

        function ValidateWoreda(Source, args) {
            var chkAnswer = document.getElementById('<%= chkIsSourceDetermined.ClientID %>');
            var cboWoreda = document.getElementById('<%= cboWoreda.ClientID %>');

            if (chkAnswer.checked == true) {
                if (cboWoreda.value == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        function ValidateCheckBox(Source, args) {
            var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtLN = document.getElementById('<%= txtLicenseNo.ClientID %>');

            if (chkAnswer.checked == false) {
                if (txtLN.value.trim() == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        function ValidatePlaceIssued(Source, args) {
            var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtPI = document.getElementById('<%= txtPlaceIssued.ClientID %>');

            if (chkAnswer.checked == false) {
                if (txtPI.value.trim() == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        function ValidatePlate(Source, args) {
            var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtPN = document.getElementById('<%= txtPlateNo.ClientID %>');
            var txtTPN = document.getElementById('<%= txtTrailerPlateNo.ClientID %>');

            if (chkAnswer.checked == false) {
                if (txtPN.value == "" && txtTPN.value == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        function ValidateDriverName(Source, args) {
            var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtLN = document.getElementById('<%= txtDriverName.ClientID %>');

            if (chkAnswer.checked == false) {
                if (txtLN.value.trim() == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }

        function ValidateTruckAndTrailerInput(Source, args) {
            var isNonTruck = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtPlateNo = document.getElementById('<%= txtPlateNo.ClientID %>');
            var txtTrailerPlateNo = document.getElementById('<%= txtTrailerPlateNo.ClientID %>');
            if (isNonTruck.checked == false) {

                if (txtPlateNo.value == "" && txtTrailerPlateNo.value == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        //
        function ValidateTruckPlomps(Source, args) {
            var cboCommodityType = document.getElementById('<%= cboCommodityType.ClientID %>');
            var SelectedText = cboCommodityType.options[cboCommodityType.selectedIndex].text;

            var isNonTruck = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtPlateNo = document.getElementById('<%= txtPlateNo.ClientID %>');
            var txtNoPlomps = document.getElementById('<%= txtNoPlomps.ClientID %>');
            if (isNonTruck.checked == false) {
                if (txtPlateNo.value.trim() != "")
                    if (txtNoPlomps.value.trim() != "" || SelectedText == "By-Product")
                        args.IsValid = true;
                    else
                        args.IsValid = false;
            }
        }

        function ValidateTrailerPlomps(Source, args) {
            var cboCommodityType = document.getElementById('<%= cboCommodityType.ClientID %>');
            var SelectedText = cboCommodityType.options[cboCommodityType.selectedIndex].text;

            var isNonTruck = document.getElementById('<%= isNonTruck.ClientID %>');
            var txtTrailerPlateNo = document.getElementById('<%= txtTrailerPlateNo.ClientID %>');
            var txtTrailerNoPlomps = document.getElementById('<%= txtTrailerNoPlomps.ClientID %>');
            if (isNonTruck.checked == false) {

                if (txtTrailerPlateNo.value.trim() != "")
                    if (txtTrailerNoPlomps.value.trim() != "" || SelectedText == "By-Product")
                        args.IsValid = true;
                    else
                        args.IsValid = false;
            }
        }


        function changeCoffeeTypeStatus() {
            var cboCommodity = document.getElementById('<%= cboCommodity.ClientID %>');
            var SelectedText = cboCommodity.options[cboCommodity.selectedIndex].text;
            if (SelectedText == "Coffee")
                document.getElementById('<%= cboCommodityType.ClientID %>').disabled = false;
            else {
                document.getElementById('<%= cboCommodityType.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= cboCommodityType.ClientID %>').disabled = true;
            }
        }

        function changeTruckPlompsStatus() {

            var cboCommodityType = document.getElementById('<%= cboCommodityType.ClientID %>');
            var SelectedText = cboCommodityType.options[cboCommodityType.selectedIndex].text;
            var truckPlateNumber = document.getElementById('<%= txtPlateNo.ClientID %>');
            var truckPlomps = document.getElementById('<%= txtNoPlomps.ClientID %>');
            if (truckPlateNumber.value == "" || SelectedText == "By-Product") {
                truckPlomps.value = "";
                truckPlomps.disabled = true;
            }
            else
                truckPlomps.disabled = false;
        }
        function changeTrailerPlompsStatus() {
            var cboCommodityType = document.getElementById('<%= cboCommodityType.ClientID%>');
            var SelectedText = cboCommodityType.options[cboCommodityType.selectedIndex].text;
            var trailerPlateNumber = document.getElementById('<%= txtTrailerPlateNo.ClientID%>');
            var trailerPlomps = document.getElementById('<%= txtTrailerNoPlomps.ClientID %>');

            if (trailerPlateNumber.value.trim() == "" || SelectedText == "By-Product") {
                trailerPlomps.value = "";
                trailerPlomps.disabled = true;
            }
            else
                trailerPlomps.disabled = false;
        }
        function ValidateVoucherNumber(Source, args) {
            var hdnHasVoucher = document.getElementById('<%= hdnHasVoucher.ClientID %>');
            var txtVoucherNo = document.getElementById('<%= txtVoucherNo.ClientID %>');
            if (hdnHasVoucher.value == "True") {
                if (txtVoucherNo.value.trim() == "")
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }
        function ValidateDriverFullName(Source, args) {

            var driverName = document.getElementById('<%= txtDriverName.ClientID %>').value;

            var mySplitResult = driverName.split(" ");

            alert(mySplitResult[0]);
            alert(mySplitResult[1]);
            alert(mySplitResult[3]);
        }
            
                                                                                         
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:HiddenField ID="hdnHasVoucher" runat="server" />
<%--IS SOURCE DETERMINED?--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<div class="container">
    <table style="width:100%">
        <tr><td class="style1"> 
        <uc2:Messages ID="Messages" runat="server" /> </td></tr>
    </table>
<div id="commodityDepositRequestForm" class="form" style="margin-top:5px;"> 
 <div class="formHeader"BackColor="#88AB2D" ForeColor="White" style="border:1px solid #88AB2D;" align="center"> 
     Arrival Registration</div>
  <div style="border: solid 1px #88AB2D; height:inherit;"> 
 <%--IS SOURCE DETERMINED?--%>           
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label2" runat="server" Text="Is Non-truck"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="isNonTruck"   runat="server" AutoPostBack="True" 
                    oncheckedchanged="isNonTruck_CheckedChanged" />
            </div>        
          </div>            

<%--WAREHOUSE--%>          
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label3" runat="server" Text="Warehouse"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label>
                <%--<asp:DropDownList ID="cboWarehouse" runat="server" Enabled="False" Width="172px" Visible="false">
                </asp:DropDownList>--%>
            </div>        
          </div>  
   <%--CLIENT--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label18" runat="server" Text="Client"></asp:Label>
            </div>        
            <div class="rightControl">
                 <asp:Label id="lblClient" runat="server" text="" visible="True" ></asp:Label>
            </div>                      
          </div> 
 <%--CHANGE CLIENT--%>             
          <div class="controlContainer" style="height:33px">    
            <div class="leftControl">   
              <asp:Label ID="Label20" runat="server" Text="Update Client ID"></asp:Label>
            </div>        
            <div class="rightControl">
                 <uc1:ClientSelector ID="ClientSelector1" runat="server" Visible="True" />                 
            </div>                      
          </div>     

 
 <%--COMMODITY--%>          
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label4" runat="server" Text="Commodity"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboCommodity"  CssClass="Input" runat="server" 
                    Width="172px" 
                    onchange="changeCoffeeTypeStatus(),changeTruckPlompsStatus(),changeTrailerPlompsStatus()" AutoPostBack="true"
                    onselectedindexchanged="cboCommodity_SelectedIndexChanged" 
                    onprerender="cboCommodity_PreRender">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                 ValidationGroup="Save"  ControlToValidate="cboCommodity" ForeColor="Tomato"></asp:RequiredFieldValidator>
            </div>        
          </div>
          
          
<%--DOES THE CLIENT HAVE CERTIFICATE ?          
          <div class="controlContainer" style="height:45px">    
            <div class="leftControl"  style="width:44%">   
              <asp:Label ID="Label24" runat="server" Text="Does the Client have Certificate ?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="ChkBxclientcert" runat="server" AutoPostBack="True" 
                    oncheckedchanged="ChkBxclientcert_CheckedChanged" />
            </div>        
          </div>--%>

 <%--CLIENT CERTIFICATE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label21" runat="server" Text="Client Certificate"></asp:Label>
            </div>        
            <div class="rightControl">                
                <asp:ListBox ID="LstBxCert" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" Enabled="false">
                <%--<asp:ListItem Text="Please Select Certificate:"></asp:ListItem>--%>
                </asp:ListBox>
                <asp:CustomValidator ID="CsVClientCert" ValidationGroup="Save" runat="server" Font-Size="Small" 
                Font-Names="Calibri" ClientValidationFunction="ValidateZone" ErrorMessage="*" 
                    ForeColor="Tomato"></asp:CustomValidator>   
            </div>
            
            <div>
            </div>                      
          </div>      

<%--IS COMMODITY SOURCE DETERMINED--%>          
          <div class="controlContainer">    
            <div class="leftControl">    
              <asp:Label ID="Label5" runat="server" Text="Is Commodity Source Determined?"></asp:Label>
            </div>        
            <div class="rightControl" style="height:40px">
                <asp:CheckBox ID="chkIsSourceDetermined" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chkIsSourceDetermined_CheckedChanged" />
            </div>        
          </div>  
           
<%--REGION--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label6" runat="server" Text="Region"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboRegion" runat="server" Width="172px"  
                    onselectedindexchanged="cboRegion_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>               
                <asp:CustomValidator ID="cvReqRegion" ValidationGroup="Save" runat="server" 
                    Font-Size="Small" Font-Names="Calibri" ClientValidationFunction="ValidateRegion"
                 ErrorMessage="*" ForeColor="Tomato">
                 </asp:CustomValidator>                                              
            </div>        
          </div>  
          
<%--ZONE--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label7" runat="server" Text="Zone"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboZone" runat="server" Width="172px" AutoPostBack="True" 
                    onselectedindexchanged="cboZone_SelectedIndexChanged">
                </asp:DropDownList>
                
                <asp:CustomValidator ID="crfvZone" ValidationGroup="Save" runat="server" Font-Size="Small" 
                Font-Names="Calibri" ClientValidationFunction="ValidateZone" ErrorMessage="*" 
                    ForeColor="Tomato"></asp:CustomValidator>                
            </div>        
          </div>
            
<%--WOREDA--%>         
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label8" runat="server" Text="Woreda"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboWoreda" runat="server" Width="172px" ></asp:DropDownList>
               
                <asp:CustomValidator ID="crfvWoreda" ValidationGroup="Save" runat="server" Font-Size="Small" 
                Font-Names="Calibri" ClientValidationFunction="ValidateWoreda" 
                    ErrorMessage="*" ForeColor="Tomato"></asp:CustomValidator>                
            </div>        
          </div>   



<%--PROCESSING CENTER--%>       
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label14" runat="server" Text="Processing Center: " ></asp:Label>
            </div>        
            <div class="rightControl">
                <%--<asp:DropDownList ID="ddlProcessingCenter" runat="server" Width="172px" ></asp:DropDownList>--%>  
                <asp:DropDownList ID="DDLwashingstation" runat="server" Height="25px" 
                    Width="172px" AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select washingstation" Enabled="false"></asp:ListItem>
                </asp:DropDownList>
                
            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Tomato" 
                ControlToValidate="DDLwashingstation" ErrorMessage="*" Display="Dynamic" 
                  ValidationGroup="Save" InitialValue=""></asp:RequiredFieldValidator>  --%>
            </div>        
          </div> 



<%--SPECIFIC AREA--%>       
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label30" runat="server" Text="Specific Area" ></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtSpecificArea" runat="server" Width="168px"></asp:TextBox>
                <%--<asp:CustomValidator ID="crfvSpecificArea" runat="server" ClientValidationFunction="ValidateSpecificArea"
                 ErrorMessage="*" ValidationGroup="Save" ForeColor="Tomato"></asp:CustomValidator>--%>       
            </div>        
          </div> 

<%--PROCESSING CENTER--%>       
          <%--<div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label19" runat="server" Text="Processing Center/PM" ></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="ddlProcessingCenter" runat="server" Width="172px" ></asp:DropDownList> 
                <asp:TextBox ID="TxtBxProcCenter" runat="server" Width="168px"></asp:TextBox>
                                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Tomato" 
                ControlToValidate="TxtBxProcCenter" ErrorMessage="*" Display="Dynamic" 
                  ValidationGroup="Save" InitialValue=""></asp:RequiredFieldValidator>  
            </div>        
          </div> --%> 
                 
<%--CERTIFICATE--%>       
          <%--<div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label19" runat="server" Text="Certificate:" ></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="ddlProcessingCenter" runat="server" Width="172px" AppendDataBoundItems="true">
                <asp:ListItem Text="Please Select Processing Center"></asp:ListItem>
                </asp:DropDownList>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Tomato" 
                ControlToValidate="DDLCert" ErrorMessage="*" Display="Dynamic" 
                    ValidationGroup="Save" InitialValue=""></asp:RequiredFieldValidator>  
            </div>        
          </div> --%>

<%--PRODUCTION YEAR--%> 
           <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label9" runat="server" Text="Production Year"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboProductionYear" runat="server" Width="172px" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Tomato" 
                    ControlToValidate="cboProductionYear" ErrorMessage="*" Display="Dynamic" ValidationGroup="Save" InitialValue=""></asp:RequiredFieldValidator>
            </div>        
          </div>   
 
  <%--NUMBER OF BAGS--%>       
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label31" runat="server" Text="No. of Bags"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtNumberOfBags" runat="server" Width="168px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumberofBags" runat="server" ForeColor="Tomato" 
                    ControlToValidate="txtNumberOfBags" ErrorMessage="*" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" Display="Dynamic" ValidationGroup="Save"  
                    ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" 
                    ControlToValidate="txtNumberOfBags" 
                    ErrorMessage="Please enter positive value." ForeColor="Tomato"></asp:CompareValidator>
            </div>        
          </div> 

<%-- VEHICLE SIZE  --%>
        <div class="controlContainer">
            <div class="leftControl">
                <asp:Label ID="lblhevsize" runat="server" Text="Vehicle Size (M2):"></asp:Label>
            </div>
            <div class="rightControl">
                <asp:DropDownList ID="DDlvehiclesize" runat="server" Width="172px"  AppendDataBoundItems="true">
                    <asp:ListItem Text="Please Select Vehicle Size" Enabled="true"></asp:ListItem>
                </asp:DropDownList>
                
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ControlToValidate="DDlvehiclesize" CssClass="Input" Display="Dynamic" Font-Size="Small"
                ErrorMessage="Please enter numbers only." ToolTip="Only Integers Allowed" ValidationGroup="Save" 
                ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$" 
                    ForeColor="Tomato"></asp:RegularExpressionValidator>--%>   
            </div>
        </div>

           
<%--ESTIMATED WEIGHT IN KG--%>          
           <div class="controlContainer" >    
            <div class="leftControl">   
                <asp:Label ID="Label10" runat="server" Text="Estimated Weight (kg)"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtWeight" runat="server" Width="168px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtWeight" CssClass="Input" Display="Dynamic" Font-Size="Small"
                ErrorMessage="Please enter numbers only." ToolTip="Only Integers Allowed" ValidationGroup="Save" 
                ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,18}(\.\d{1,2})?$" 
                    ForeColor="Tomato"></asp:RegularExpressionValidator>                
            </div>        
          </div>  
                   
<%--DATE RECEIVED--%>          
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label11" runat="server" Text="Date Received"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtArrivalDate" runat="server" Width="168px"></asp:TextBox> 
                <cc1:calendarextender ID="txtArrivalDate_CalendarExtender" runat="server" 
                    TargetControlID="txtArrivalDate">
                </cc1:calendarextender> 
                 <asp:RequiredFieldValidator ID="rvArrivalDate" ValidationGroup="Save" 
                    ControlToValidate="txtArrivalDate" runat="server" ErrorMessage="*" 
                    ForeColor="Tomato"></asp:RequiredFieldValidator>        
                 <asp:RangeValidator ID="RangeValidator2" ValidationGroup="Save" Type="Date" ControlToValidate="txtArrivalDate" Display="Dynamic" ForeColor="Tomato"
                                MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
                                MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
                                runat="server" ErrorMessage="Invalid date."></asp:RangeValidator>
            </div>        
          </div>  
                        
<%--TIME RECEIVED--%>
           <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label12" runat="server" Text="Time Received"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtTimeArrival" runat="server" Width="168px"></asp:TextBox> 

                <cc1:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeArrival">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                    ControlToValidate="txtTimeArrival" Display="None" InvalidValueMessage="Please enter a valid time."
                    SetFocusOnError="True">*</cc1:MaskedEditValidator>
                <cc1:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                </cc1:ValidatorCalloutExtender>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ValidationGroup="Save" ControlToValidate="txtTimeArrival" Display="Dynamic" 
                    ErrorMessage="*" ForeColor="Tomato"></asp:RequiredFieldValidator>
                      
                 <asp:CustomValidator ID="CustomValidator3" ValidationGroup="Save" 
                    runat="server" ErrorMessage="Please enter valid time." ForeColor="Tomato"></asp:CustomValidator>
            </div>        
          </div> 
    <br />                
<br />          
</div>                      
</div>   


<div id="driverForm" class="form" style=" margin-top:5px;">
 <div class="formHeader" align="center" style="border:1px solid #88AB2D;">
     Driver Registration</div>
 <div style="border: solid 1px #88AB2D;">  
<%--IS TRUCK IN COMPOUND--%>             
          <div class="controlContainer" style=" margin-top:3px">    
            <div class="leftControl">   
                <asp:Label ID="Label1" runat="server" Text="Is Truck in Compound?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkIsTruckInCompound" Checked="true" runat="server" />
            </div>        
          </div> 
<%--DRIVER NAME--%>             
          <div class="controlContainer" style=" margin-top:3px">    
            <div class="leftControl">   
                <asp:Label ID="Label15" runat="server" Text="Driver Name"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtDriverName" runat="server" Width="168px"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator4" ValidationGroup="Save" 
                    runat="server" ClientValidationFunction="ValidateDriverName" ErrorMessage="*" 
                    ForeColor="Tomato"></asp:CustomValidator>              
            </div>        
          </div> 
          
  <%--LICENSE NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label16" runat="server" Text="License No."></asp:Label>
            </div>        
            <div class="rightControl">
        <asp:TextBox ID="txtLicenseNo" runat="server" Width="168px"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Save" runat="server" ClientValidationFunction="ValidateCheckBox"
        ErrorMessage="*" ForeColor="Tomato"></asp:CustomValidator>        
            </div>        
          </div> 
          
 <%--PLATE NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label22" runat="server" Text="Place Issued"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtPlaceIssued" runat="server" Width="168px"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                    ClientValidationFunction="ValidatePlaceIssued" ErrorMessage="*" 
                    ValidationGroup="Save" ForeColor="Tomato"></asp:CustomValidator>
            </div>        
          </div>          
                     
 <%--PLACE ISSUED--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label17" runat="server" Text="Truck Plate No."></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtPlateNo" runat="server" Width="168px" onchange="changeTruckPlompsStatus()"> </asp:TextBox>
                <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ForeColor="Tomato" 
                    ClientValidationFunction="ValidateTruckAndTrailerInput" ErrorMessage="Please enter Truck or Trailer number." 
                    ValidationGroup="Save"> </asp:CustomValidator>
            </div>        
          </div> 
          
 <%--TRAILER PLATE NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label23" runat="server" Text="Trailer Plate No."></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtTrailerPlateNo" runat="server" Width="168px" onchange="changeTrailerPlompsStatus()"></asp:TextBox>
            </div>        
          </div>   

 <%--************--%>             
    <br /> <br />           
</div>                    
</div>


<div id="voucherForm" class="form">   
 <div class="formHeader" align="center" style="border:1px solid #88AB2D;"> 
     Voucher Registration  
 </div>
 <div style="border: solid 1px #88AB2D;">  
<%--VOUCHER NUMBER--%>       
          <div class="controlContainer" style="margin-top:3px">    
            <div class="leftControl">   
                <asp:Label ID="Label27" runat="server" Text="Voucher No"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtVoucherNo" runat="server" Width="168px"></asp:TextBox>
                <asp:CustomValidator ID="CustomValidator19" runat="server" ClientValidationFunction="ValidateVoucherNumber"
                 ErrorMessage="*" ValidationGroup="Save"></asp:CustomValidator>        
            </div>        
          </div> 
<%--IS BI PRODUCT--%>         
          <%--<div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label28" runat="server" Text="Is Bi-product?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkIsBiProduct" runat="server" />                   
            </div>        
          </div>--%>  
          
<%--COFFEE TYPE--%>         
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label29" runat="server" Text="Commodity Type"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboCommodityType" runat="server" Width="172px" onchange="changeTruckPlompsStatus(), changeTrailerPlompsStatus()" 
                 DataTextField="CoffeeType" DataValueField="Id" AppendDataBoundItems="true">
                 <asp:ListItem Text="Select Commodity Type" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RFVCoffeeType" runat="server" ValidationGroup="Save" InitialValue=""
                 ControlToValidate="cboCommodityType" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>                  
                 <%--<asp:CustomValidator ID="CustomValidator1R0" runat="server" ClientValidationFunction="ValidateCoffeeType"
                 ErrorMessage="*" ValidationGroup="Save"></asp:CustomValidator>--%>   
            </div>        
          </div>             
          
<%--TRUCK NUMBER PLOMPS--%>       
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label32" runat="server" Text="Truck No. Plomps"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtNoPlomps" runat="server" Width="168px"></asp:TextBox>

                <asp:CustomValidator ID="CustomValidator6" runat="server" 
                    ClientValidationFunction="ValidateTruckPlomps" ErrorMessage="*" 
                    ValidationGroup="Save" ForeColor="Tomato"></asp:CustomValidator> 
                                    
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    ControlToValidate="txtNoPlomps"    Display="Dynamic" 
                    ErrorMessage="Please enter numbers only." 
                    ValidationExpression="[0-9]{0,}" ValidationGroup="Save" ForeColor="Tomato"></asp:RegularExpressionValidator>     
            </div>        
          </div> 
          
<%--TRAILER NUMBER PLOMPS--%>       
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label33" runat="server" Text="Trailer No. Plomps"></asp:Label>
            </div>        
            <div class="rightControl">
                 <asp:TextBox ID="txtTrailerNoPlomps" runat="server" Width="168px"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator7" runat="server"
                    ClientValidationFunction="ValidateTrailerPlomps" ErrorMessage="*" 
                    ValidationGroup="Save" ForeColor="Tomato"></asp:CustomValidator> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtTrailerNoPlomps" Display="Dynamic" ValidationGroup="Save"
                    ErrorMessage="Please enter numbers only." ForeColor="Tomato"
                    ValidationExpression="[0-9]{0,}"></asp:RegularExpressionValidator>                
            </div>        
          </div> 
<%--REMARK--%>
           <div class="controlContainer" style="height:55px;">    
            <div class="leftControl">   
                <asp:Label ID="Label13" runat="server" Text="Remark"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                Width="168px" Rows="3" ></asp:TextBox>                                  
            </div>                   
          </div>
          <br /><br />           
</div>

   <br />          
<%--BUTTONS--%>       
          <div class="controlContainer" style="height:30px; vertical-align:middle; ">    
              <asp:Button ID="btnSave" runat="server" BackColor="#88AB2D" ForeColor="White" BorderStyle="None" 
                   Height="20px" onclick="btnSave_Click" Text="Save" 
                  ValidationGroup="Save" style="margin-right:70px" />
              <asp:Button ID="btnNext" runat="server" BackColor="#88AB2D" ForeColor="White" style="margin-right:70px;" 
                  BorderStyle="None"  Height="20px" Text="Next->>" onclick="btnNext_Click" />
              <asp:Button ID="btnPrintPreArrival" runat="server" BackColor="#88AB2D" ForeColor="White" 
                  BorderStyle="None" Height="20px" 
                  onclick="btnPrintPreArrival_Click" Text="Print Tracking No." />
          </div> 
            <br />                                                                                                                                                                                                                                                                  
</div> 
<script type="text/javascript">
    changeTruckPlompsStatus();
    changeTrailerPlompsStatus();
</script>
</div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
