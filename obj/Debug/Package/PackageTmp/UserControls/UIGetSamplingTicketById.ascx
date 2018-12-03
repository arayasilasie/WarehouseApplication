<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIGetSamplingTicketById.ascx.cs" Inherits="WarehouseApplication.UserControls.UIGetSamplingTicketById" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>



        <table class="PreviewEditor" style="width:500px">
         <tr class="PreviewEditorCaption">
             <td class="Text" colspan="2">Enter Sampler ID</td>
         </tr>
        <tr>
             <td class="Text Message" colspan="2">
                 <asp:Label ID="lblMsg"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td class="Text"><asp:Label ID="lblSamplerCode" runat="server" Text="Sampler Name:"></asp:Label></td>
        <td class="Input">
            <asp:DropDownList ID="cboSampler" runat="server" Width="250px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="cboSampler" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>
            Re-Sampling</td>
              <td><asp:CheckBox ID="chkReSampling" runat="server"></asp:CheckBox></td>
        </tr>
        <tr class="EditorCommand" >
        <td class="Button" colspan="2" align="left">
        <asp:Button ID="btnGenerateSampleTicket" runat="server" Height="30px" 
                Text="Get Sample Ticket" Width="125px" 
                onclick="btnGenerateSampleTicket_Click" />
        </td>
        </tr>
            </table>
 
