<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ReceiveSampleCodeNew.aspx.cs" Inherits="WarehouseApplication.ReceiveSampleCodeNew" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<asp:HiddenField ID="hfId" runat="server" />
<style type="text/css">

.form
{
	width:60%;
	margin-left:20%;
}
.btn
{
 margin-left:105px;
}
</style>
<div class="messageArea">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>    
</div>
<div class="container">
<%--*************************RECEIVE SAMPLE CODE FORM*****************************--%>
<div id="receiveSampleCodeForm" class="form">
 <div class="formHeader" style="padding-left:35%">
    RECEIVE Grading CODE     
 </div>
 <div class="formControlHolders">
<%--GRADING CODE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label15" runat="server" Text="Grading Code"></asp:Label>
            </div>        
            <div class="rightControl">
    <asp:Label ID="lblGradingCode" runat="server" Text=""></asp:Label>
            </div>        
          </div> 
          
  <%--DATE CODED--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label16" runat="server" Text="Date Coded"></asp:Label>
            </div>        
            <div class="rightControl">
        <asp:TextBox ID="txtDateCoded" Enabled="false" runat="server" Width="200px"></asp:TextBox>
            </div>        
          </div> 
          
 <%--SAMPLE CODE RECEIVE DATE--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label22" runat="server" Text="Sample Code Received Date"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtDateRecived" TabIndex="1" runat="server" Width="200px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
            </div>        
          </div>          
                     
 <%--SAMPLE CODE RECEIVE TIME--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label17" runat="server" Text="Sample Code Received Time"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtTimeRecived" runat="server" TabIndex="2" Width="200px"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
            </div>        
          </div> 
          
 <%--SAMPLE REMARK--%>             
          <div class="controlContainer" style="height:65px">    
            <div class="leftControl">   
                <asp:Label ID="Label21" runat="server" Text="Remark"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtRemark"  runat="server" Rows="3" Width="200px" TextMode="MultiLine" 
                Height="55px"></asp:TextBox>
            </div>        
          </div> 
           
      </div>                    
</div>
<%--*************************END OF RECEIVE SAMPLE CODE FORM*****************************--%>  

<div class="buttonHolder"> 
  <div class="innerButtonHolder">     
    <asp:Button ID="btnSave" class="btn"  Width="95px" TabIndex="4" runat="server"  Text="Save" 
     Height="20px" BorderStyle="None" BackColor="#669999" ForeColor="#FFFFCC" 
          onclick="btnSave_Click" /> 
  
    <asp:Button ID="btnNext" class="btn" runat="server" CausesValidation="false" Text="Next" Width="95px" 
    Height="20px" BorderStyle="None" BackColor="#669999" ForeColor="#FFFFCC" />                                        

  </div>
</div>
</div>
</asp:Content>
