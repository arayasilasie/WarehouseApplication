<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="PreArrival.aspx.cs" Inherits="WarehouseApplication.PreArrival" Title="Untitled Page" %>
<%@ Register src="UserControls/ClientSelector.ascx" tagname="ClientSelector" tagprefix="uc1" %>
<%@ Register src="Messages.ascx" tagname="Messages" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="messages.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function ValidateTruckAndTrailerInput(Source, args) {
        var txtPlateNo = document.getElementById('<%= txtTruckPlateNo.ClientID %>');
        var txtTrailerPlateNo = document.getElementById('<%= txtTrailerPlateNo.ClientID %>');
        if (txtPlateNo.value.trim() == "" && txtTrailerPlateNo.value.trim() == "")
            args.IsValid = false;
        else
            args.IsValid = true;

    }
    function ValidateVoucherInput(Source, args) {
        var txtVoucherNo = document.getElementById('<%= txtVoucherNo.ClientID %>');
        var chkHasVoucher = document.getElementById('<%= chkHasVoucher.ClientID %>');
        if (chkHasVoucher.checked == true) {
            if (txtVoucherNo.value.trim() == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">



<%--*************************PRE ARRIVAL FORM*****************************--%>
<div class="container">

<table>
<tr><td><uc2:messages ID="Messages" runat="server" /></td></tr>        
</table>

<div id="preArrivalForm" class="form" style=" margin: 5% 0 0 20%; width:55%">
 <div class="formHeader" align="center">
 Initial Arrival Registration     
 </div> 
 
 <div style="border:solid 1px #A5CBB0;">
     <%--CLIENT ID--%>             
          <div class="controlContainer"  style="height:60px; margin-top:5px; ">    
            <div class="leftControl">   
                <asp:Label ID="Label15" runat="server" Text="Client Id"></asp:Label>
            </div>        
            <div class="rightControl">
                <uc1:ClientSelector ID="ClientSelector1" runat="server"/>
            </div>        
          </div> 
     <%--HAS VOUCHER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label1" runat="server" Text="Has Voucher?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkHasVoucher" Checked="true" runat="server" />
            </div>        
          </div> 
     <%--VOUCHER NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label16" runat="server" Text="Voucher No.[ WR No.]"></asp:Label>
            </div>        
            <div class="rightControl">
            <asp:TextBox ID="txtVoucherNo" runat="server" Width="168px"></asp:TextBox>
              <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateVoucherInput" 
               Display="Dynamic" ValidationGroup="Save"  ErrorMessage="*" ForeColor="Tomato"></asp:CustomValidator>
            </div>        
          </div> 
          
 <%--TRUCK PLATE NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label22" runat="server" Text="Truck Plate No."></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtTruckPlateNo" runat="server" Width="168px"></asp:TextBox>
            </div>        
          </div>          
                     
 <%--TRAILER PLATE NUMBER--%>             
          <div class="controlContainer">    
            <div class="leftControl">   
                <asp:Label ID="Label17" runat="server" Text="Trailer Plate No."></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:TextBox ID="txtTrailerPlateNo" runat="server" Width="168px"> </asp:TextBox>
                <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ValidationGroup="Save" 
                    ClientValidationFunction="ValidateTruckAndTrailerInput" ErrorMessage="Either Truck No. or Trailer No. should be entered.">
                </asp:CustomValidator>
            </div>        
          </div> 

 <%--HAS VOUCHER--%>             
          <div class="controlContainer" style="height:50px;">    
            <div class="leftControl">   
                <asp:Label ID="Label2" runat="server" Text="Is Truck in Compound?"></asp:Label>
            </div>        
            <div class="rightControl">
                <asp:CheckBox ID="chkIsTruckInCompound" Checked="true" runat="server" />
            </div>        
          </div> 
<br />                 
     <%--BUTTON--%>                     
          <div class="controlContainer" style="margin-top:20px">    
            <div class="leftControl">   
           <asp:Button ID="btnSave" runat="server" Text="Save" 
                     Width="50px" style="" Height="21px" 
                    ValidationGroup="Save" BorderStyle="None" BackColor="#88AB2D" 
                    ForeColor="#FFFFCC" onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnClear" runat="server" Text="Clear" 
                     Width="50px" style="" Height="21px" 
                     BorderStyle="None" BackColor="#88AB2D" 
                    ForeColor="#FFFFCC" onclick="btnClear_Click"/>
            </div>        
            <div class="rightControl">
           <asp:Button ID="btnNext" runat="server" Text="Next-&gt;&gt;" 
                     Width="64px" Height="21px" 
                     BorderStyle="None" BackColor="#88AB2D" 
                    ForeColor="#FFFFCC" onclick="btnNext_Click"/>
            </div>        
          </div> 


</div>                     
</div>
    <%--*************************END OF PRE ARRIVAL FORM*****************************--%>
</div>
</asp:Content>
