<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchControl.ascx.cs" Inherits="WarehouseApplication.UserControls.SearchControl" %>

  
    
   
     <table class="PreviewEditor" >
     <tr class="PreviewEditorCaption">
        <td colspan="4" >Search Criteria</td>
     </tr>
         <tr>
    <td  colspan="4" class="Message"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        
    </tr>
     <tr>
     <td class="Text" >Tracking No :</td>
     <td class="Input"><asp:TextBox ID="txtTrackingNo" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
     <tr>
     <td class="Text">Grading Code :</td>
     <td class="Input"><asp:TextBox ID="txtCode" runat="server" Width="100px"></asp:TextBox></td>
     </tr>
