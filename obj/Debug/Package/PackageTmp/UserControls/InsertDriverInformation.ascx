<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertDriverInformation.ascx.cs" Inherits="WarehouseApplication.UserControls.AddDriverInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="CommodityDepositRequestId" runat="server" />
<asp:HiddenField ID="hfTruckRegisterId" runat="server" />
<asp:Panel ID="pnlDriverInfo" runat="server">

<table  class="PreviewEditor" style="width:850px" onload="InitValidators();" >
<tr class="PreviewEditorCaption">
<td colspan="4">Driver Information :</td>
</tr>
<tr>
<td colspan="4" align="Left" >
    <asp:Label CssClass="Message" ID="lblMessage" runat="server" 
        ForeColor="#CC0000"></asp:Label>
        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidatePlate"
    ErrorMessage="Either Plate No. or Trailer Plate No. should be provided."></asp:CustomValidator>
        </td>
</tr>
<tr class="EditorAlternate">
<td colspan="4" class="Text" >Is Non-truck : <asp:CheckBox ID="isNonTruck"   runat="server" /></td>
</tr>
<tr >
    <td class="Text" style="width:175px">Driver Name :</td>
    <td class="style4">
        <asp:TextBox ID="txtDriverName" runat="server" Width="168px"></asp:TextBox>
               <asp:CustomValidator ID="CustomValidator4" ValidationGroup="Driver" runat="server" ClientValidationFunction="ValidateDriverName"
    ErrorMessage="*"></asp:CustomValidator>
    </td>
    <td >&nbsp;</td>
    <td></td>
</tr>
<tr class="EditorAlternate">
    <td class="style1">License No. :</td>
    <td class="style4" >
        <asp:TextBox ID="txtLicenseNo" runat="server" Width="168px"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" ValidationGroup="Driver" runat="server" ClientValidationFunction="ValidateCheckBox"
    ErrorMessage="*"></asp:CustomValidator>
    </td>
    <td class="style2">Place Issued:</td>
    <td class="style3"><asp:TextBox ID="txtPlaceIssued" runat="server" Width="155px"></asp:TextBox>
    <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="Driver" ClientValidationFunction="ValidatePlaceIssued"
    ErrorMessage="*"></asp:CustomValidator>
    </td>
</tr>
<tr>
    <td class="style1">Plate No. :</td>
    <td class="style4" >
        <asp:TextBox ID="txtPlateNo" runat="server" AutoPostBack="true" Width="168px" 
            ontextchanged="txtPlateNo_TextChanged"></asp:TextBox>
    </td>
    <td class="style1">Trailer Plate No.:</td>
    <td class="style3"><asp:TextBox ID="txtTrailerPlateNo" runat="server" Width="154px" 
            AutoPostBack="True" ontextchanged="txtTrailerPlateNo_TextChanged"></asp:TextBox></td>
</tr>
<tr class="EditorAlternate">
<td>Truck Type :</td>
<td >
                   <asp:DropDownList ID="cboTruckType" runat="server" Width="200px">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboTruckType_CascadingDropDown" runat="server" 
                    Category="TruckType" Enabled="True" LoadingText="Loading Truck Type " 
                    PromptText="Please Select Truck Type" ServiceMethod="GetActiveTruckTypes" 
                    ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboTruckType">
                </cc1:CascadingDropDown>  
                </td>  
                <td>
                Trailer Type :
                </td>    
                <td>
                                   <asp:DropDownList ID="cboTruckType2" runat="server" Width="200px">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboTruckType2_CascadingDropDown1" runat="server" 
                    Category="TruckType" Enabled="True" LoadingText="Loading Truck Type " 
                    PromptText="Please Select Truck Type" ServiceMethod="GetActiveTruckTypes" 
                    ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboTruckType2">
                </cc1:CascadingDropDown>  
                </td>                           

</tr>
<tr>
<td>Truck Model :</td>
 <td class="style4">
            <asp:DropDownList ID="cboTruckModel" runat="server" Width="200px">
            </asp:DropDownList>
            <cc1:CascadingDropDown ID="cboTruckModel_CascadingDropDown" runat="server" 
                Category="TruckModel" Enabled="True" 
                LoadingText="Loading Truck Model..." ParentControlID="cboTruckType" 
                PromptText="Please Select Truck Model" ServiceMethod="GetActiveTruckModels" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboTruckModel">
            </cc1:CascadingDropDown>
        </td>
        <td>Trailer Model</td>
        <td>            <asp:DropDownList ID="cboTrailerModel" runat="server" Width="200px">
            </asp:DropDownList>
            <cc1:CascadingDropDown ID="cboTrailerModel_CascadingDropDown2" runat="server" 
                Category="TruckModel" Enabled="True" 
                LoadingText="Loading Trailer Model..." ParentControlID="cboTruckType2" 
                PromptText="Please Select Trailer Model" ServiceMethod="GetActiveTruckModels" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboTrailerModel">
            </cc1:CascadingDropDown></td>
</tr>
<tr class="EditorAlternate">
<td>Truck Model Year :</td>
<td class="style4">

                <asp:DropDownList ID="cboModelYear" runat="server" Width="200px">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboModelYear_CascadingDropDown" runat="server" 
                    Category="ModelYear" Enabled="True" LoadingText="Loading Model Year" 
                    ParentControlID="cboTruckModel" PromptText="Please Select Model Year" 
                    ServiceMethod="GetActiveTruckModelYear" ServicePath="~\UserControls\Commodity.asmx" 
                    TargetControlID="cboModelYear">
                </cc1:CascadingDropDown>

  
                <asp:CustomValidator ID="cvTruckModel" runat="server" ValidationGroup="Driver" ClientValidationFunction="ValidateTruckModel"
                    ErrorMessage="*"></asp:CustomValidator>

  
            </td>
            <td>Trailer Model Year</td>
            <td>                <asp:DropDownList ID="cboTrailerModelYear" runat="server" Width="200px">
                </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboTrailerModelYear_CascadingDropDown3" runat="server" 
                    Category="ModelYear" Enabled="True" LoadingText="Loading Trailer Model Year" 
                    ParentControlID="cboTrailerModel" PromptText="Please Select Trailer Model Year" 
                    ServiceMethod="GetActiveTruckModelYear" ServicePath="~\UserControls\Commodity.asmx" 
                    TargetControlID="cboTrailerModelYear">
                </cc1:CascadingDropDown>
                  <asp:CustomValidator ID="cvTrailerModel" runat="server" ValidationGroup="Driver" ClientValidationFunction="ValidateTrailerModel"
                    ErrorMessage="*"></asp:CustomValidator>
                </td>
</tr>
<tr >
<td class="Text" valign="top">Remark : </td>
<td colspan="3" align="left" class="Input" >
    <asp:TextBox ID="txtRemark"  runat="server" Width="254px" TextMode="MultiLine" 
        Height="55px"></asp:TextBox>
    </td>
</tr>
<tr>
<td colspan="4" align="left" class="Input"  >
    
    &nbsp;</td>
</tr>
<tr class="EditorCommand">
    
<td colspan="4" align="Left" class="Button" >
    <asp:Button ID="btnSave" runat="server" Text="Add" 
        Width="78px" onclick="btnSave_Click" Height="26px" ValidationGroup="Driver" />&nbsp;<asp:Button ID="btnComplete" CausesValidation="false" runat="server" 
        Text="Next" Width="77px" onclick="btnComplete_Click" /></td>
</tr>
</table>
<table>
<tr>
<td  >
</tr>
<tr>
    
<td align="center" class="Button" >
<asp:GridView ID="gvDriverInformation" runat="server" 
        AutoGenerateColumns="False" AllowPaging="True" CellPadding="4" 
        DataKeyNames="Id" GridLines="None" Width="680px" 
        onrowcommand="gvDriverInformation_RowCommand" 
        onrowediting="gvDriverInformation_RowEditing" 
        onrowupdated="gvDriverInformation_RowUpdated" 
        onrowupdating="gvDriverInformation_RowUpdating" 
        onrowcancelingedit="gvDriverInformation_RowCancelingEdit1" 
        onpageindexchanging="gvDriverInformation_PageIndexChanging" PageSize="5" 
        CssClass="Grid">
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <RowStyle CssClass="GridRow" />
    <Columns>
     <asp:TemplateField HeaderText="" Visible="false">
      <ItemTemplate>
                <asp:Label Runat="server" Visible="false" Text='<%# Bind("Id") %>'     ID="lblId"></asp:Label>
     </ItemTemplate> 
     
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Driver Name">
        <ItemTemplate>
             <asp:Label Runat="server" Text='<%# Bind("DriverName") %>'     ID="lblDriverName"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtEditDriverName" Width="100px"
                         Runat="server" 
                         Text='<%# Bind("DriverName") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvEditDriverName" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtEditDriverName">
                            *</asp:RequiredFieldValidator>

       
     
        </EditItemTemplate>
     </asp:TemplateField>  
     <asp:TemplateField HeaderText="License No.">
        <ItemTemplate>
             <asp:Label Runat="server" Text='<%# Bind("LicenseNumber") %>'     ID="lblLicenseNumber"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtLicenseNumber" Width="100px"
                         Runat="server" 
                         Text='<%# Bind("LicenseNumber") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvLicenseNumber" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtLicenseNumber">
                            *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>    
     <asp:TemplateField HeaderText="Place Issued">
        <ItemTemplate>
             <asp:Label Runat="server" Text='<%# Bind("LicenseIssuedPlace") %>'     ID="lblLicensePlaceIssued"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtLicenseIssuedPlace" Width="100px"
                         Runat="server" 
                         Text='<%# Bind("LicenseIssuedPlace") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvLicenseIssuedPlace" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtLicenseIssuedPlace">
                            *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>   
     <asp:TemplateField HeaderText="Plate No.">
        <ItemTemplate>
             <asp:Label Runat="server" Text='<%# Bind("PlateNumber") %>'     ID="lblPlateNumber"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtPlateNumber" Width="100px" 
                         Runat="server" 
                         Text='<%# Bind("PlateNumber") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvPlateNumber" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtPlateNumber">
                            *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>    
     <asp:TemplateField HeaderText="Trailer Plate No.">
        <ItemTemplate>
             <asp:Label Runat="server" Text='<%# Bind("TrailerPlateNumber") %>'     ID="lblTrailerPlateNumber"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtTrailerPlateNumber" Width="100px"
                         Runat="server" 
                         Text='<%# Bind("TrailerPlateNumber") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvTrailerPlateNumber" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtTrailerPlateNumber">
                            *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>   
     <asp:TemplateField HeaderText="Status">
        <ItemTemplate>
            <asp:DropDownList ID="cboStatus" Enabled="false" runat="server" 
                SelectedValue='<%# Bind("Status") %>'>
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="0">Cancelled</asp:ListItem>
            </asp:DropDownList>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:DropDownList ID="cboStatusEdit" runat="server" 
                SelectedValue='<%# Bind("Status") %>'>
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="0">Cancelled</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvStatus" Runat="server" 
                ControlToValidate="cboStatusEdit" ErrorMessage="*"> *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>          
     <asp:TemplateField  HeaderText="Remark">

        <EditItemTemplate>
            <asp:TextBox ID="txtRemark" Width="200px" Height="40px"
                         Runat="server" 
                         Text='<%# Bind("Remark") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                          ID="rfvRemark" 
                          Runat="server" 
                       ErrorMessage="*"
                            ControlToValidate="txtRemark">
                            *</asp:RequiredFieldValidator>

        </EditItemTemplate>
     </asp:TemplateField>      
     <asp:TemplateField HeaderText="" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" CausesValidation="false" CommandName="Edit" runat="server" Text="Edit" />
         
        
                        </ItemTemplate>   
                        <EditItemTemplate>
                            <asp:LinkButton CssClass="Text" ID="btnUpdate" CausesValidation="false" CommandName="Update" runat="server" Text="Update" />
                            <asp:LinkButton CssClass="Text"  ID="btnCancel" CausesValidation="false" CommandName="Cancel" runat="server" Text="Cancel" />


        
                        </EditItemTemplate>    
     </asp:TemplateField>
     
    </Columns>
    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
    <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
        CssClass="GridSelectedRow" />
    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
    <EditRowStyle CssClass="GridSelectedRow" />
    <AlternatingRowStyle CssClass="GridAlternate" />
</asp:GridView>
</td>
</tr>
</table>
</asp:Panel>
   <script language="javascript" type="text/javascript">
       function ValidateCheckBox(Source, args) {
           var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
           var txtLN = document.getElementById('<%= txtLicenseNo.ClientID %>');

           if (chkAnswer.checked == false) {
               if (txtLN.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       function ValidatePlaceIssued(Source, args) {
           var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
           var txtPI = document.getElementById('<%= txtPlaceIssued.ClientID %>');

           if (chkAnswer.checked == false) {
               if (txtPI.value == "")
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
               if (txtPN.value == "" && txtTPN.value== ""  )
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       function ValidateTruckModel(Source, args) {
           var txtPN = document.getElementById('<%= txtPlateNo.ClientID %>');
           var txtTM = document.getElementById('<%= cboModelYear.ClientID %>');
           if (txtPN.value != "") {
               if (txtTM.value == "") {
                   args.IsValid = false;
               }
               else {
                   args.IsValid = true;
               }
           }
           else {
               args.IsValid = true;
           }

       }
       function ValidateTrailerModel(Source, args) {
           var txtPN = document.getElementById('<%= txtTrailerPlateNo.ClientID %>');
           var txtTM = document.getElementById('<%= cboTrailerModelYear.ClientID %>');
           if (txtPN.value != "") {
               if (txtTM.value == "") {
                   args.IsValid = false;
               }
               else {
                   args.IsValid = true;
               }
           }
           else {
               args.IsValid = true;
           }

       }
       function ValidateDriverName(Source, args) {
           var chkAnswer = document.getElementById('<%= isNonTruck.ClientID %>');
           var txtLN = document.getElementById('<%= txtDriverName.ClientID %>');

           if (chkAnswer.checked == false) {
               if (txtLN.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       




       
   </script> 