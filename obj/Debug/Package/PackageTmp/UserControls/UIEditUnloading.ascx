<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditUnloading.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditUnloading" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<style type="text/css">
.forText
{

}

    .modalBackground {
	background-color:White;
	filter:alpha(opacity=80);
	opacity:0.7;
}
.popupStyle
        {
        	background-color:White;
        	width:618px
        }
        .modalPopup {
	background-color:#ffffdd;
	border-width:3px;
	border-style:solid;
	border-color:Gray;
	padding:3px;
	width:250px;
</style>
<%--<asp:UpdatePanel ID="updatePanelMain" runat="server" UpdateMode="Always">
<ContentTemplate>--%>
<table class="PreviewEditor" style="width:900px" >
<tr class="PreviewEditorCaption">
<td colspan="6">Unloading Information</td>
</tr>
<tr>
<td class="Message" colspan="6"><asp:Label ID="lblmsg" runat="server"></asp:Label></td>
</tr>
<tr>
    
    <td class="Text" >Grading Code</td>
    <td class="Input" colspan="2">
        <asp:Label ID="lblGradingCode" runat="server" Width="150px"></asp:Label>
    </td>
    <asp:HiddenField ID="hfRecivingRequestId" runat="server" />

    <td>
    
        <asp:HiddenField ID="hfUnloadingId" runat="server" />
    
    </td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Commodity Grade </td>
    <td class="Input" colspan="5"><asp:Label ID="lblCommodityGrade" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
    <td class="Text">Total No. of Bags</td>
    <td class="Input" colspan="5"><asp:TextBox ID="txtNumberOfBags" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
            ControlToValidate="txtNumberOfBags" ErrorMessage="Please enter Total No. bags"></asp:RequiredFieldValidator>
  <asp:RegularExpressionValidator ID="viNumberofBags" runat="server" Display="Dynamic"
        ControlToValidate="txtNumberOfBags" CssClass="Input" 
        ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" ValidationGroup="Update" 
        ValidationExpression="[0-9]{0,}" ></asp:RegularExpressionValidator>               
             <asp:CompareValidator
                ID="CompareValidator1"   Display="Dynamic" ValidationGroup="Update" ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" ControlToValidate="txtNumberOfBags" ErrorMessage="Value Must be greater than 0."></asp:CompareValidator>     
                 
                 </td>
</tr>
<tr class="EditorAlternate">
    <td class="Text">Date Depsosited</td>
    <td class="Input"  colspan="5"><asp:TextBox ID="txtDateDeposited" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="txtDateDeposited_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="txtDateDeposited">
        </cc1:CalendarExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
            runat="server" ControlToValidate="txtDateDeposited"  ValidationGroup="Update"
            ErrorMessage="Please select date deposited">*</asp:RequiredFieldValidator>
                 </td>
</tr>
<tr>
<td class="Text" >Status</td>
<td  colspan="5">
    <asp:DropDownList ID="cboStatus" runat="server" Width="150px">
        <asp:ListItem Value="1">New</asp:ListItem>
        <asp:ListItem Value="2">Active</asp:ListItem>
        <asp:ListItem Value="3">Rejected</asp:ListItem>
        <asp:ListItem Value="4">Cancelled</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="6" class="PreviewEditorCaption">
Stack Information
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text" valign="bottom">Shed</td>
<td class="Text" valign="bottom">Stack No.</td>
<td class="Text" align="center" valign="bottom">No. Of Bags<br />(per Stack)</td>
<td class="Text" valign="bottom">Inv. Controller</td>
<td class="Text" valign="bottom">Remark:</td>
<td></td>
</tr>
<tr>
<td class="Input">
<asp:DropDownList ID="cboShed" runat="server" Width="100px" AutoPostBack="True" 
        onselectedindexchanged="cboShed_SelectedIndexChanged">
    </asp:DropDownList>
</td>
<td class="Input"><asp:DropDownList ID="cboStackNo" runat="server" Width="100px">
    </asp:DropDownList></td>
    
<td class="style1">

    <asp:TextBox ID="txtStackNoBags" Width="100px" runat="server"></asp:TextBox>
 <asp:CompareValidator
                ID="CompareValidator2" Display="Dynamic" ValidationGroup="Add" ValueToCompare="0" Type="Integer" Operator="GreaterThan" runat="server" ControlToValidate="txtStackNoBags" ErrorMessage="Value Must be greater than 0."></asp:CompareValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
        ControlToValidate="txtStackNoBags" CssClass="Input" 
        ErrorMessage="Only Integers Allowed" ToolTip="Only Integers Allowed" 
        ValidationExpression="[0-9]{0,}" ValidationGroup="Add"></asp:RegularExpressionValidator>

</td>
<td class="style1"><asp:DropDownList ID="cboUnloadedBy" runat="server" Width="150px">
    <asp:ListItem Value="b951e6e0-4242-45d1-9fc7-25c40294eeda">Inve Con1</asp:ListItem>
    </asp:DropDownList></td>
    <td class="Input"><asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
<tr class="EditorCommand">
    <td  colspan="6" align="left">
    <asp:Button ID="btnAdd" CssClass="Forbtn" runat="server" Text="Add" ValidationGroup="Add" 
            onclick="btnAdd_Click" CausesValidation="true"  CommandName="Add" />
    </td>
</tr>

<tr>
<td colspan="6" class="Message">
    <asp:Label ID="lblMsg2" runat="server" CssClass="Message"></asp:Label>
    </td>
</tr>
<tr>
<td colspan="6">

     <asp:GridView ID="gvStackUnloaded" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" 
        GridLines="None" Width="900px" onrowediting="gvStackUnloaded_RowEditing" 
         onrowcancelingedit="gvStackUnloaded_RowCancelingEdit" 
         onrowupdating="gvStackUnloaded_RowUpdating" onrowdatabound="gvStackUnloaded_RowDataBound"  
         >
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
       <AlternatingRowStyle CssClass="GridAlternate" />
        <RowStyle BackColor="#E3EAEB" CssClass="GridRow" />
        <Columns>
        <asp:TemplateField Visible="False" HeaderText="Id"  >
            <ItemTemplate>
                <asp:Label ID="lblId" runat="server" Visible="false"  Text='<%# Bind("Id") %>'></asp:Label>
                    
            </ItemTemplate>
            <ControlStyle CssClass="Text" />

<ControlStyle CssClass="Text"></ControlStyle>
        </asp:TemplateField>
        
         <asp:TemplateField Visible="True" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100px"  HeaderText="Stack No." >
            <ItemTemplate>
                <asp:Label ID="lblStackNo" CssClass="forText"   runat="server" Width="100px" Text='<%# Bind("StackNo") %>'></asp:Label>
            </ItemTemplate>
             <ControlStyle CssClass="Text" />
             <HeaderStyle CssClass="Text" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
        
        <asp:TemplateField Visible="True" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"     HeaderText="Inv. Controller" >
            <ItemTemplate>
                <asp:Label ID="lblInvetoryCont" Width="150px"  runat="server"  Text='<%# Bind("InventoryControllerName") %>'></asp:Label>
            </ItemTemplate>
           <%-- <EditItemTemplate>
             <asp:Label ID="lblUserId" Visible="false"  runat="server"  Text='<%# Bind("UserId") %>'></asp:Label>
            <asp:DropDownList ID="cboUnloadedBy" runat="server" Width="250px">   
    </asp:DropDownList>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboUnloadedBy" ValidationGroup="add"   ErrorMessage="*">
                </asp:RequiredFieldValidator>
            </EditItemTemplate>--%>  
            <ControlStyle CssClass="Text" />
            <HeaderStyle CssClass="Text" />
            <ItemStyle CssClass="Text" />
        </asp:TemplateField>          
        <asp:TemplateField Visible="True" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"    HeaderText="No. of Bags" >
            <ItemTemplate>
                <asp:Label ID="lblNoBag" CssClass="forText" runat="server" Width="150"   Text='<%# Bind("NumberOfbags") %>'></asp:Label>
            </ItemTemplate>
<%--            <EditItemTemplate>
                <asp:TextBox ID="txtNoBag"  runat="server" Text='<%# Bind("NumberOfbags") %>' ></asp:TextBox>
            </EditItemTemplate>--%>
            <HeaderStyle Wrap="False" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>  
        <asp:TemplateField Visible="True" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px"  HeaderText="Status" >
            <ItemTemplate>
                <asp:Label ID="lblStatus"  runat="server" Width="150px" Text='<%# Bind("Status") %>'></asp:Label>
                <asp:Label ID="lblEditstatus" Visible="false"  runat="server" Width="150px" Text='<%# Bind("Status") %>'></asp:Label>
            </ItemTemplate>
           <EditItemTemplate>
                           <asp:Label ID="lblEditstatus" Visible="false"  runat="server" Width="150px" Text='<%# Bind("Status") %>'></asp:Label>

           
                <asp:DropDownList ID="cboStackUnloadedStatus" SelectedValue='<%# Bind("Status") %>'  DataValueField="Status"  runat="server" Width="100px" >
                       <asp:ListItem  Value="New"  >New</asp:ListItem>
                       <asp:ListItem  Value="Cancelled" >Cancelled</asp:ListItem>
                </asp:DropDownList>
     
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboStackUnloadedStatus" ValidationGroup="Update"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            
  
            
<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
            
  
            
        </asp:TemplateField>    
            
        <asp:TemplateField Visible="True"   HeaderText="Remark" >
            <ItemTemplate>
                <asp:Label ID="lblRemark" runat="server"  Text='<%# Bind("Remark") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtStackRemark"   TextMode="MultiLine" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
            </EditItemTemplate>
    

            <HeaderStyle HorizontalAlign="Left" />
    

<ItemStyle Width="250px" HorizontalAlign="Left"></ItemStyle>
        </asp:TemplateField>  
            <asp:TemplateField ShowHeader="False"  ItemStyle-Width="250px">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="false" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                
            
                </ItemTemplate>
                <EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" 
                        CommandName="Update" CausesValidation="true" ValidationGroup="Update"  Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>

<ItemStyle Width="75px" CssClass="Text"></ItemStyle>
<EditItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CausesValidation="false" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                
</EditItemTemplate>

<ItemStyle CssClass="Text" Width="250px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" 
             CssClass="GridPager" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" 
             CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" 
             CssClass="GridHeader" BackColor="#6A7C49" />
        <EditRowStyle BackColor="#FFCC66" CssClass="GridAlternate" />
      
    </asp:GridView>

</td>
</tr>
<tr  class="EditorCommand">
   <td colspan="6"  align="Left">
        <asp:Button ID="btnSave" CssClass="Forbtn" runat="server" Text="Update" ValidationGroup="Update" 
            onclick="btnSave_Click" Height="27px" Width="61px" />
   </td>
</tr>
<asp:Button ID="btnDummy" runat="server" style="display:none" />  
<asp:Button ID="btnOkay" runat="server" style="display:none" />  
<asp:Button ID="btnNotOkay" runat="server" style="display:none" />     
</table>
  <asp:Panel ID="Panel1" runat="server" UpdateMode="Conditional" Style="display: none" >
    <ContentTemplate>
            <table style="width:550px;background-color:White">
                <tr>
                    <td class="EditorCaption">Warning:</td>
                </tr>
                <tr>
                    <td class="Message">
                    <span id="txtReasonPopup" runat="server"/>
                   <asp:Label ID="lblModalMsg" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="EditorCommand">
                     <asp:Button ID="OkButton" runat="server" Text="Yes" />
                     <asp:Button ID="btnCancelPopUp"   runat="server" Text="No" />
                   </td>
                </tr>
            </table>
     </ContentTemplate>
  
   <div align="center">
     
   </div>
   </asp:Panel>
<%--

<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
         TargetControlID="btnSave" 
         PopupControlID="Panel1"
         BackgroundCssClass="modalBackground"
         DropShadow="true"         
         OkControlID="OkButton"
         CancelControlID="btnCancelPopUp">

</cc1:ModalPopupExtender>--%>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>