<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddUnloadingNew.aspx.cs" Inherits="WarehouseApplication.AddUnloadingNew" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="Messages.ascx" tagname="Messages" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />

<style type="text/css">
th
{ 
    text-align:left    
}
grdDisplayInfo
{
    border:1px solid red;
}
.error-message {
background-color:#a90329;
color:#fff;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div class="container">
<table style="width:100%">
    <tr><td class="style1"><uc2:Messages ID="Messages" runat="server" /> </td></tr>
</table>
<%--<div style="height:20px; background-color:White; width:100%; margin-bottom:5px;">
<asp:Label ID="lblmsg" runat="server"></asp:Label></div>--%>    
    <asp:GridView ID="grdDisplayInfo" runat="server" AutoGenerateColumns="false"
        onrowdatabound="grdDisplayInfo_RowDataBound" Width="100%" 
        CssClass="grdDisplayInfo" ForeColor="#333333" 
        GridLines="None" >
        <AlternatingRowStyle BackColor="Transparent" />
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55"  ForeColor="White" />
         <HeaderStyle  BorderStyle="Solid" BorderWidth="1px" BorderColor="White" 
            BackColor="#88AB2D"  ForeColor="White" />
        
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        
        <RowStyle BackColor="Transparent" ForeColor="#669999"  VerticalAlign="Top" HorizontalAlign="Justify"/>     
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" CssClass="gridRow" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
        <columns>
        <asp:BoundField HeaderText="Client Name"  DataField="ClientName" />
        <asp:BoundField HeaderText="Commodity Name" DataField="ComodityName" />
        <asp:BoundField HeaderText="Production Year" DataField="Pro_Year" />
        <asp:BoundField HeaderText="Bag Type" DataField="BagType" />
        <asp:BoundField HeaderText="Truck Plate" DataField="TruckPlate" />
        <asp:BoundField HeaderText="Trailer Plate" DataField="TrailerPlate" />
        <asp:BoundField HeaderText="Grading Code" DataField="GradingCode" />
        <asp:BoundField HeaderText="Tracking No" DataField="TrackingNumber" />

        </columns>
    </asp:GridView>
    
<br />

<%--START OF LEFT FORM--%>

<div id="commodityDepositRequestForm" class="form"> 
    <%--START OF LEFT TOP FORM--%>
<div>

<div class="formHeader" align="center"> 
     GRN INFORMATION</div>

<div style="border: solid 1px #999933;">
           
<%--PLATE NUMBER--%>
      <%--IS GRN BONDED YARD?--%>
          <div class="controlContainer"  style="margin-top:10px">    
            <div class="leftControl">   
              <asp:Label ID="Label17" runat="server" Text="Bonded Yard"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="ChkBondedYard" runat="server" AutoPostBack="true" 
                    oncheckedchanged="ChkBondedYard_CheckedChanged"/>
                    
            </div>  
            
          </div>
           <%--IS GRN BONDED YARD?--%>
          <div class="controlContainer"  style="margin-top:10px"> 
          <div class="leftControl">   
              <asp:Label ID="Label23" runat="server" Text="Warehouse Deposit"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="ChkWHMode" runat="server" AutoPostBack="true" />
                   
            </div>       
          </div>
          </div>
          <br />
 <div class="formHeader" align="center"> 
     ADD SCALING INFORMATION</div>
<div style="border: solid 1px #999933;">
           
<%--PLATE NUMBER--%>
      <%--IS TRUCK SCALED?--%>
          <div class="controlContainer"  style="margin-top:10px">    
            <div class="leftControl">   
              <asp:Label ID="Label9" runat="server" Text="Is Manually scaled?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkIsManuallyScaled" runat="server" AutoPostBack="true" 
                    oncheckedchanged="chkIsManuallyScaled_CheckedChanged"/>
                <%--change the name of checkbox here--%>
            </div>        
          </div>
     
<%--SCALE TICKET NUMBER--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label10" runat="server" Text="Scale Ticket Number"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="txtScaleTicket" runat="server" Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Save" ForeColor="Tomato" 
            ControlToValidate="txtScaleTicket" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>        
          </div>
          
<%--CLIENT ACCEPTANCE DATE--%>          
        <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label1y" runat="server" Text="Client Acceptance Date"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtClientAcceptanceDate" runat="server" Width="150px" Enabled="false"></asp:TextBox>
            </div>        
        </div>          
<%--DATE WEIGHED--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label12" runat="server" Text="Date Weighed"></asp:Label>
          </div>        
          <div class="rightControl">

            <asp:TextBox ID="txtDateWeighed" runat="server" Width="150px"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDateWeighed_CalendarExtender" runat="server" 
                                  Enabled="True" TargetControlID="txtDateWeighed"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                  Display="Dynamic" ValidationGroup="Save"
                                        ControlToValidate="txtDateWeighed" ErrorMessage="*" 
                  ForeColor="Tomato"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator2o"  Type="Date" 
                                ControlToValidate="txtDateWeighed" Display="Dynamic"
                                MinimumValue="<%#DateTime.Now.AddYears(-7).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
                                MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
                                runat="server" ErrorMessage="Future Date isn't Acceptable" ForeColor="Tomato"></asp:RangeValidator>

            <asp:CompareValidator ControlToValidate="txtDateWeighed" ControlToCompare="txtClientAcceptanceDate" 
                                ID="CompareValidator1" runat="server" Display="Dynamic" ForeColor="Tomato"  
                                ErrorMessage="Weighed date should be greater or equal to Client acceptance Date" 
                                Type="Date" ValidationGroup="Add" Operator="GreaterThanEqual"></asp:CompareValidator>                                
          </div>        
        </div>
          
<%--TIME WEIGHED--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label13" runat="server" Text="Time Weighed"></asp:Label>
          </div>        
          <div class="rightControl">
            
            <asp:TextBox ID="txtTimeWeighed" runat="server" Width="150px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureDateFormat="99:99" Enabled="True" 
                    TargetControlID="txtTimeWeighed" AcceptAMPM="True" ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time">
                </cc1:MaskedEditExtender> 
                
                <cc1:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                    ControlToValidate="txtTimeWeighed" Display="None" InvalidValueMessage="Please enter a valid time."
                    SetFocusOnError="True">*</cc1:MaskedEditValidator>
                <cc1:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender"
                    runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                </cc1:ValidatorCalloutExtender>
                                                  
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="Save" 
                  runat="server" ControlToValidate="txtTimeWeighed" 
                  ErrorMessage="*" ForeColor="Tomato"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator1rt" runat="server" Display="Dynamic" ClientValidationFunction="ComparePreviousDateTimesValidation" 
                                 ValidationGroup="Save" ErrorMessage="Time scaled should be after client acceptance time."></asp:CustomValidator>            
          </div>        
        </div>
          
<%--GROSS TRUCK WEIGHT--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label14" runat="server" Text="G-Truck Weight"></asp:Label> 
          </div>        
          <div class="rightControl">

            <asp:TextBox ID="txtGrossTruckWeight" runat="server" Width="120px" onkeyup="CalculateNetWeight()"></asp:TextBox>
            <asp:Label ID="lblUnit1" runat="server" ForeColor="#669999"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ValidationGroup="Save"
                                        ControlToValidate="txtGrossTruckWeight" ErrorMessage="*" ForeColor="Tomato"></asp:RequiredFieldValidator>
              <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Invalid" ControlToValidate="txtGrossTruckWeight"
              MinimumValue="0.00" MaximumValue="100000.00" Type="Double" ValidationGroup="Save" Display="Dynamic" ForeColor="Tomato"></asp:RangeValidator>             
          </div>        
        </div>
          
<%--TRUCK WEIGHT--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label18" runat="server" Text="Truck Weight"></asp:Label>            
          </div>        
          <div class="rightControl">

            <asp:TextBox ID="txtTruckWeight" runat="server" Width="120px" 
                  onkeyup="CalculateNetWeight()" ontextchanged="txtTruckWeight_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:Label ID="lblUnit2" runat="server" ForeColor="#669999"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save" 
                    ErrorMessage="*" ControlToValidate="txtTruckWeight" ForeColor="Tomato"></asp:RequiredFieldValidator>

            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtTruckWeight" 
                                   ControlToCompare="txtGrossTruckWeight" Display="Dynamic" ErrorMessage="Truck Weight can not exceed Gross Truck weight" 
                                   Operator="LessThan" Type="Double" ForeColor="Tomato"></asp:CompareValidator>
            <asp:CustomValidator ID="cvTW" runat="server" Display="Dynamic" ClientValidationFunction="ValidateTruckweight" 
                                 ErrorMessage="No>0"></asp:CustomValidator>
            <asp:CustomValidator ID="UnScaledTW" runat="server" Display="Dynamic" ClientValidationFunction="UnScaledValidateTruckweight" 
                                 ErrorMessage="Truck Weight Should only be 0." ></asp:CustomValidator>
          </div>        
          </div>                                                                      
          
<%--NET WEIGHT--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label4" runat="server" Text="Net Weight"></asp:Label>
          </div>        
          <div class="rightControl">
            <asp:Label ID="lblNetWeight" runat="server" ForeColor="#669999" Font-Bold="true"></asp:Label>
          </div>        
          </div> 
<%--TRUCK TYPE--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label6" runat="server" Text="Truck Type"></asp:Label>
          </div>        
          <div class="rightControl">
            <asp:DropDownList ID="cboTruckType" runat="server" Width="158px" AppendDataBoundItems="true">
            <asp:ListItem Text="Select Truck" Value=""></asp:ListItem>
            </asp:DropDownList>
          </div>        
          </div> 
<%--WEIGHER NAME--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label1" runat="server" Text="Weigher Name"></asp:Label>
          </div>        
          <div class="rightControl">

            <asp:DropDownList ID="cboWeigher" runat="server" Width="158px" AppendDataBoundItems="true">
            <asp:ListItem Text="Select Weigher" Value="-1"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvWeigherName" runat="server" ValidationGroup="Save" 
                                        ControlToValidate="cboWeigher" ErrorMessage="*" 
                  InitialValue="-1" ForeColor="Tomato"></asp:RequiredFieldValidator>
          </div>        
        </div> 
      <%--WB SERVICE PROVIDER--%>
        <div class="controlContainer">    
          <div class="leftControl">   
            <asp:Label ID="Label11" runat="server" Text="WB Service Provider"></asp:Label>
          </div>        
          <div class="rightControl">

            <asp:DropDownList ID="cboWBServiceProvider" runat="server" Width="158px" AppendDataBoundItems="true">
            <asp:ListItem Text="Select WB Service Provider" Value="-1"></asp:ListItem>
            </asp:DropDownList>
              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="cboWeigher" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="xsety1" runat="server" ErrorMessage="*" 
                    ControlToValidate="cboWBServiceProvider" InitialValue="-1" 
                    ValidationGroup="Save" ForeColor="Tomato"></asp:RequiredFieldValidator>
          </div>        
        </div>

      <%--IS SCALE TICKET SIGNED--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label3" runat="server" Text="Is Scale Ticket Signed?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkScaleTicketSigned" runat="server" Checked="true"/>
            </div>        
          </div>          
  <br />                       
     </div>         
<br />
 <asp:Label ID="lblTitle" runat="server" Text="Same grading results after segregation" ForeColor="#669999" Font-Names="Arial Black"></asp:Label>
  <br />
     <asp:Image ID="Image1" runat="server" Height="20px" 
        ImageUrl="~/arrow-join.png" style="margin-Top:-2px;  padding-left:45%"
        Width="25px" />
    <asp:GridView ID="grdSameGradingResults" runat="server" Width="100%" 
        ForeColor="#333333" GridLines="None"  
        DataKeyNames="GradingCode,GradeId,CommodityGradeID,GRNID,TrackingNumber" 
        onrowdatabound="grdSameGradingResults_RowDataBound">
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle  BorderStyle="Solid" BorderWidth="1px" BorderColor="White" 
            BackColor="#88AB2D" Font-Bold="True" ForeColor="White"/>
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Include">
            <ItemTemplate>
            <asp:CheckBox ID="chkChoice" runat="server"/>

            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle CssClass="grdSameGradingResultsRow" BackColor="Transparent"  />
        <AlternatingRowStyle BackColor="Transparent" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
<br />

<div>
       <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" Width="50px" style="" Height="21px" 
         ValidationGroup="Save" BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" onkeyup="CalculateNetWeight();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

       <asp:Button ID="btnPrintGrn" runat="server" Text="Print GRN" CssClass="Forbtn" 
           CausesValidation="False" Width="80px" style="" Height="21px" 
        BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" 
           onclick="btnPrintGrn_Click"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 

       <asp:Button ID="btnClear" runat="server" Text="Reset" CssClass="Forbtn" 
           CausesValidation="False" Width="50px" style="" Height="21px" 
        BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" OnClientClick="window.location.reload()" 
          /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                             
       <asp:Button ID="btnNext" runat="server" Text="Next->>" Height="21px" style="" 
        Width="65px" CausesValidation="False" BorderStyle="None" BackColor="#88AB2D" 
           ForeColor="#FFFFCC"  Enabled="false" onclick="btnNext_Click"/>   

</div>              
     </div>            
     <br />
                            
</div>
    <%--END OF LEFT TOP FORM--%>



<%--START OF RIGHT FORM (SHADE INFO)--%>
<div id="rightForm" class="form">

<%--START OF SCALING FORM--%>                     
    <div id="scalingForm" style="width:100%;">
    
        <div class="formHeader" align="center"> 
         UNLOAD AND RECORD DEPOSIT     
        </div>     
<div style="border: solid 1px #999933;">
<%--DEPOSIT TICKET NUMBER--%>             
          <div class="controlContainer" style="margin-top:10px">    
            <div class="leftControl">   
                <asp:Label ID="Label2" runat="server" Text="Deposit Ticket Number"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtDepositTicketNumber" runat="server" Width="168px"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Save" 
                    ErrorMessage="*" ControlToValidate="txtDepositTicketNumber" 
                    ForeColor="Tomato"></asp:RequiredFieldValidator>
            </div>        
          </div> 
                    
<%--TOTAL NUMBER OF BAGS--%>          
          <div class="controlContainer">    
            <div class="leftControl"  >   
              <asp:Label ID="Label5" runat="server" Text="Total No. of Bags"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtNumberOfBags" runat="server" Width="168px" AutoPostBack="true"
                    onkeyup="CalculateNetWeight()" ontextchanged="txtNumberOfBags_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Save" 
                    ErrorMessage="*" ControlToValidate="txtNumberOfBags" Display="Dynamic" 
                    ForeColor="Tomato"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="adew" runat="server" ErrorMessage="Invalid Entry" MinimumValue="1" Type="Integer"
                MaximumValue="50000" ControlToValidate="txtNumberOfBags" Display="Dynamic" ForeColor="Tomato"></asp:RangeValidator>
            </div>        
          </div>  
                     
    <%--GRN SERVICE [ONLY REBAGING CURENTLY]--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label21" runat="server" Text="Rebagging"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtRebagging" runat="server" Width="168px"></asp:TextBox> 
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Invalid Entry" MinimumValue="1" Type="Integer"
                MaximumValue="50000" ControlToValidate="txtRebagging" Display="Dynamic" 
                    ForeColor="Tomato" ></asp:RangeValidator>                               
            </div>        
          </div>    
          
<%--DATE DEPOSITED--%>
          <div class="controlContainer">    
              <div class="leftControl">   
                <asp:Label ID="Label7" runat="server" Text="Date Deposited"></asp:Label>
              </div>        
              <div class="rightControl">
                <asp:TextBox ID="txtDateDeposited" runat="server" Width="168px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateDeposited_CalendarExtender" runat="server" 
                                      Enabled="True" TargetControlID="txtDateDeposited">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="Save" 
                    ErrorMessage="*" ControlToValidate="txtDateDeposited" ForeColor="Tomato"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1d" ValidationGroup="Save" 
                                    Type="Date" ControlToValidate="txtDateDeposited" Display="Dynamic"
                                    MinimumValue="<%#DateTime.Now.AddYears(-7).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
                                    MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
                                    runat="server" ErrorMessage="Future Date isn't Acceptable" Width="168px" ForeColor="Tomato"></asp:RangeValidator>
                <asp:CompareValidator ControlToValidate="txtDateDeposited" ControlToCompare="txtDateWeighed" 
                                      ID="cmpSampGen" runat="server" Display="Dynamic" ForeColor="Tomato"  
                                      ErrorMessage="Deposit date should be greater or equal to Scale Date" 
                                      Type="Date" ValidationGroup="Add" Operator="GreaterThanEqual"></asp:CompareValidator>
               
              </div>     
          </div>           
    <%--TIME DEPOSITED--%>         
          <div class="controlContainer" style="margin-bottom:10px">    
              <div class="leftControl">   
                <asp:Label ID="Label8" runat="server" Text="Time Deposited"></asp:Label>
              </div>        
              <div class="rightControl">
                <asp:TextBox ID="txtTimeDeposited" runat="server" Width="168px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="txtTimeDeposited_MaskedEditExtender2" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        TargetControlID="txtTimeDeposited" AcceptAMPM="True" 
                                        ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time">
                </cc1:MaskedEditExtender>

                <cc1:MaskedEditValidator ID="MaskedEditValidator14" runat="server" ControlExtender="txtTimeDeposited_MaskedEditExtender2"
                    ControlToValidate="txtTimeDeposited" Display="None" InvalidValueMessage="Please enter a valid time."
                    SetFocusOnError="True">*</cc1:MaskedEditValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                    runat="server" Enabled="True" TargetControlID="MaskedEditValidator14">
                </cc1:ValidatorCalloutExtender>                              
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Save" 
                    ErrorMessage="*" ControlToValidate="txtTimeDeposited" ForeColor="Tomato"></asp:RequiredFieldValidator>

                <asp:CustomValidator ID="CustomValidator1yu" runat="server" Display="Dynamic" ClientValidationFunction="CompareDateTimesValidation" 
                        ErrorMessage="Unloading time should be after scaled time." ValidationGroup="Save"></asp:CustomValidator>
              </div>        
          </div>   

   <%--QUADRANT--%>             
          <div class="controlContainer">    
              <div class="leftControl">   
                <asp:Label ID="Label24" runat="server" Text="Quandrant: "></asp:Label>
              </div>        
              <div class="rightControl">

                  <asp:TextBox ID="TxtQuadrant" runat="server" Width="168px"></asp:TextBox>
                <%--<asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True" 
                      ValidationGroup="Add" AppendDataBoundItems="true" onselectedindexchanged="cboShed_SelectedIndexChanged">
                      <asp:ListItem Text="Select Shade" Value=""></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                 ControlToValidate="TxtQuadrant" ErrorMessage="*" ValidationGroup="Save" 
                      ForeColor="Tomato"></asp:RequiredFieldValidator>
              </div>        

          </div> 

    <%--SHADE--%>             
          <div class="controlContainer">    
              <div class="leftControl">   
                <asp:Label ID="Label15" runat="server" Text="Shed"></asp:Label>
              </div>        
              <div class="rightControl">
                <asp:DropDownList ID="cboShed" runat="server" Width="100px" AutoPostBack="True" 
                      ValidationGroup="Add" AppendDataBoundItems="true" onselectedindexchanged="cboShed_SelectedIndexChanged">
                      <asp:ListItem Text="Select Shade" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvShed" runat="server" InitialValue=""
                 ControlToValidate="cboShed" ErrorMessage="*" ValidationGroup="Save" 
                      ForeColor="Tomato"></asp:RequiredFieldValidator>
                               <asp:CheckBox ID="chkChangeShade" Text="Change" runat="server" 
                      AutoPostBack="True" oncheckedchanged="chkChangeShade_CheckedChanged" />
              </div>        

          </div> 

      <%--INVENTORY CONTROLLER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label22" runat="server" Text="Inventory Controller"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboInventoryController" runat="server" Width="174px" 
                    AppendDataBoundItems="true" Enabled="false" >
                <asp:ListItem Text="Select Inventory Controller" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="xsety" runat="server" ErrorMessage="*" 
                    ControlToValidate="cboInventoryController" InitialValue="-1" 
                    ValidationGroup="Save" ForeColor="Tomato"></asp:RequiredFieldValidator>
            </div>        
          </div>         
  <%--STACK NO.--%>             
          <div class="controlContainer">    
              <div class="leftControl">   
                <asp:Label ID="Label16" runat="server" Text="Stack No."></asp:Label>
              </div>        
              <div class="rightControl">
                <asp:DropDownList ID="cboStackNo" runat="server" Width="174px" AppendDataBoundItems="true">
                <asp:ListItem Text="Select Stack" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="rfvStack" runat="server" ValidationGroup="Save" InitialValue=""
                      ControlToValidate="cboStackNo" ErrorMessage="*" ForeColor="Tomato"></asp:RequiredFieldValidator>--%>
              </div>        
          </div>                      

    <%--LABOURER GROUP--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label19" runat="server" Text="Labourer Group"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:DropDownList ID="cboLabourerGroup" runat="server" Width="174px" AppendDataBoundItems="true">
                <asp:ListItem Text="Select Labourer Group" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="cboUnloadedBy" ErrorMessage="*" ValidationGroup="Add"></asp:RequiredFieldValidator>--%>
                <asp:RequiredFieldValidator ID="xsety0" runat="server" ErrorMessage="*" 
                    ControlToValidate="cboLabourerGroup" InitialValue="-1" ForeColor="Tomato"
                    ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>        
          </div> 
                                 
      <%--SCALING REMARK--%>
        <div class="controlContainer" style="height:40px">    
          <div class="leftControl">   
            <asp:Label ID="Label20" runat="server" Text="Remark"></asp:Label>
          </div>        
          <div class="rightControl">
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="168px"></asp:TextBox>
                &nbsp;</div>        
        </div>   
<br /><br />                    
</div>                                   
    </div> <br /><br />

    <%--END OF SCALING FORM  --%> 

                  
</div>
    <%--END OF RIGHT FORM--%>

<script type="text/javascript">
    function ValidateTruckweight(Source, args) {
        var chkAnswer = document.getElementById('<%= chkIsManuallyScaled.ClientID %>');
        var txtTW = document.getElementById('<%= txtTruckWeight.ClientID %>');

        if (chkAnswer.checked == true) {
            if (txtTW.value == "" || txtTW.value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
    function UnScaledValidateTruckweight(Source, args) {
        var chkAnswer = document.getElementById('<%= chkIsManuallyScaled.ClientID %>');
        var txtTW = document.getElementById('<%= txtTruckWeight.ClientID %>');

        if (chkAnswer.checked == false) {
            if (txtTW.value == "0")
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
    
    function CalculateNetWeight() {
 
        var txtNumberOfBags = document.getElementById('<%= txtNumberOfBags.ClientID %>');
        var txtTruckWeight = document.getElementById('<%= txtTruckWeight.ClientID %>');
        var txtGrossTruckWeight = document.getElementById('<%= txtGrossTruckWeight.ClientID %>');
        var hdnTare = document.getElementById('<%= hdnTare.ClientID %>');
        var lblNetWeight = document.getElementById('<%= lblNetWeight.ClientID %>');
        var decimalNetWeight;
        if (txtNumberOfBags.value != "" && txtTruckWeight.value != "" && txtGrossTruckWeight.value != "" && hdnTare.value != "") {
            decimalNetWeight = ((txtGrossTruckWeight.value - txtTruckWeight.value) - (txtNumberOfBags.value * hdnTare.value));
//            lblNetWeight.innerHTML = (Math.round(decimalNetWeight * Math.pow(10, 2)) / Math.pow(10, 2));
        }
        else
            lblNetWeight.innerHTML = '';
    }
    function ComparePreviousDateTimesValidation(Source, args) {
        var txtClientAcceptanceDateTime = document.getElementById('<%= txtClientAcceptanceDate.ClientID %>');
        var txtDateWeighed = document.getElementById('<%= txtDateWeighed.ClientID %>');
        var txtTimeWeighed = document.getElementById('<%= txtTimeWeighed.ClientID %>');
        var ClientAcceptanceDateTime = txtClientAcceptanceDateTime.value;
        var WeighedDateTime = txtDateWeighed.value + " " + txtTimeWeighed.value;
        var soonerDate = new Date(ClientAcceptanceDateTime);
        var latterDate = new Date(WeighedDateTime);
        var diffrence = parseInt((latterDate.getTime() - soonerDate.getTime()) / 1000 * 60 * 60 * 24);
        if (diffrence < 0)
            args.IsValid = false;
        else
            args.IsValid = true;
    }
    function CompareDateTimesValidation(Source, args) {
        var txtDateWeighed = document.getElementById('<%= txtDateWeighed.ClientID %>');
        var txtTimeWeighed = document.getElementById('<%= txtTimeWeighed.ClientID %>');
        var txtDateDeposited = document.getElementById('<%= txtDateDeposited.ClientID %>');
        var txtTimeDeposited = document.getElementById('<%= txtTimeDeposited.ClientID %>');
        var WeighedDateTime = txtDateWeighed.value + " " + txtTimeWeighed.value;
        var DepositedDateTime = txtDateDeposited.value + " " + txtTimeDeposited.value;
        var soonerDate = new Date(WeighedDateTime);
        var latterDate = new Date(DepositedDateTime);

        var diffrence = parseInt((latterDate.getTime() - soonerDate.getTime()) / 1000 * 60 * 60 * 24);
        if (diffrence < 0)
            args.IsValid = false;
        else
            args.IsValid = true;
    }
      
</script>

    <%--END SCALING OLD FORMAT--%>        
    <asp:HiddenField ID="hdnClientAcceptanceDate" runat="server" />
        <asp:HiddenField ID="hdnTare" runat="server" />
<script type="text/javascript">
    CalculateNetWeight();
</script>   
</div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
