<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="AddGradingResultNew.aspx.cs" Inherits="WarehouseApplication.AddGradingResultNew" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<style type="text/css">
.container{ border:none; height:450px;}
#gradingResultForm
{
	width:45%;
	margin-right:100px;
	margin-left:30px;
	float:left;
}
#gradingFactorForm
{
	width:35%;
	float:left;
}
.leftControl
{
	width:35%
}
.rightControl
{
	width:60%
}
.btn
{
	margin-left:100px;
}

</style>

<div class="container">

<div class="messageArea">
<asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label>
    </div>
<%--*************************RECEIVE SAMPLE CODE FORM*****************************--%>
<div class="form" id="gradingResultForm">
 <div class="formHeader" style="padding-left:22%;">
   GRADING RESULT ENTRY FORM     
 </div> 
 <div class="formControlHolders">
<%--GRADING CODE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label15" runat="server" Text="Grading Code"></asp:Label>
            </div>        
            <div class="rightControl">

            <asp:DropDownList ID="cboGradingCode" runat="server" Width="250px"                  
                AutoPostBack="True" >
            </asp:DropDownList>

            </div>        
          </div> 
          
  <%--HAD RECEIVED GRADE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label16" runat="server" Text="Has Received Grade?"></asp:Label>
            </div>        
            <div class="rightControl">

            <asp:CheckBox ID="chkRecivedGrade" 
                runat="server" Checked="True" AutoPostBack="True" />

            </div>        
          </div> 
          
 <%--COMMODITY--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label22" runat="server" Text="Commodity"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:DropDownList ID="cboCommodity" runat="server" Width="250px"
              >
        </asp:DropDownList>
        <%--<cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
            Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
            PromptText="Please Select Commodity" ServiceMethod="GetCommoditiesContext" 
            ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity" 
                    UseContextKey="True">
        </cc1:CascadingDropDown>--%>

            </div>        
          </div>          
                     
 <%--COMMODITY CLASS--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label17" runat="server" Text="Commodity Class"></asp:Label>
            </div>        
            <div class="rightControl">

        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="250px">
        </asp:DropDownList>
        <%--<cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
            Category="CommodityClass" Enabled="True" LoadingText="Loading Commodity Class..." 
            ParentControlID="cboCommodity" PromptText="Please Select Commodity Class" 
            ServiceMethod="GetCommodityClass" ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
        </cc1:CascadingDropDown>--%>

            </div>        
          </div> 
          
 <%--COMMODITY GRADE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label21" runat="server" Text="Commodity Grade"></asp:Label>
            </div>        
            <div class="rightControl">

            <asp:DropDownList ID="cboCommodityGrade" runat="server" Width="250px"                     
                    AutoPostBack="True" ></asp:DropDownList>
            <%--<cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
                Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
                ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
                ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
                TargetControlID="cboCommodityGrade" UseContextKey="True">
            </cc1:CascadingDropDown>--%>

            </div>        
          </div> 
 
  <%--GRADER OR CUPPER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label1" runat="server" Text="Grader/Cupper"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:DropDownList ID="cboGrader" runat="server" Width="250px" >
                    <asp:ListItem Value="">Please Select Grader</asp:ListItem>
                </asp:DropDownList>

            </div>        
          </div>
          
 <%--IS COORDINATOR--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label2" runat="server" Text="Is Coordinator?"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:CheckBox ID="isSupervisor" runat="server" />

            </div>        
          </div>
          
 <%--CODE GENERATED DATE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label3" runat="server" Text="Code Generated Date"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="lblCodeGeneratedDate" runat="server" Enabled="False" Text=""></asp:TextBox>                
            </div>        
          </div>
          
 <%--GRADE COMPLETED DATE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label4" runat="server" Text="Grade Completed Date"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="txtDateRecived" runat="server" ></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                
            </div>        
          </div>
          
 <%--GRADE COMPLETED TIME--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label5" runat="server" Text="Grade Completed Time"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="txtTimeRecived" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>

            </div>        
          </div>
          
 <%--PRODUCTION YEAR--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label6" runat="server" Text="Production Year "></asp:Label>
            </div>        
            <div class="rightControl">

            <asp:DropDownList ID="cboProductionYear" runat="server" ></asp:DropDownList>

            </div>        
          </div>
          
 <%--STATUS--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label7" runat="server" Text="Status "></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:DropDownList ID="cboStatus" runat="server" Width="250px">
                    <asp:ListItem Value="">Please Select Status</asp:ListItem>
                    <asp:ListItem Value="1">New</asp:ListItem>
                    <asp:ListItem Value="5">Moisture Fail</asp:ListItem>
                    <asp:ListItem Value="6">General Rquierment fail</asp:ListItem>
                    <asp:ListItem Value="2">Cancelled</asp:ListItem>
                </asp:DropDownList>

            </div>        
          </div>
          
 <%--REMARK--%>             
          <div class="controlContainer" style="height:50px">    
            <div class="leftControl">   
                <asp:Label ID="Label8" runat="server" Text="Remark"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </div>        
          </div>                                                                               
</div>                             
</div>
<%--*************************END OF RECEIVE SAMPLE CODE FORM*****************************--%>  



<%--*************************GRADING FACTOR FORM*****************************--%>  
<div class="form" id="gradingFactorForm">
<div class="formHeader">
GRADING FACTORS
</div>
<div class="formControlHolders" style="height:300px"></div>
</div>
<%--*************************END OF GRADING FACTOR FORM*****************************--%>  

</div>

<div class="buttonHolder"> 
  <div class="innerButtonHolder"> 
                                                  
     <asp:Button ID="btnSave" class="btn" Width="95px" TabIndex="4" runat="server"  Text="Save" 
      Height="20px" BorderStyle="None" BackColor="#669999" ForeColor="#FFFFCC" />        
              
    <asp:Button ID="btnCancel" class="btn" Width="95px" TabIndex="4" runat="server"  Text="Cancel" 
     Height="20px" BorderStyle="None" BackColor="#669999" ForeColor="#FFFFCC" />        

  </div>
</div>

</asp:Content>
