<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddGradingRecived.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddGradingRecived" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
     .AdjustWidth
    {
        width: 850px;
    }
    .style1
    {
        height: 30px;
    }
</style>
<br />
<table class="PreviewEditor AdjustWidth"   >
<tr class="PreviewEditorCaption AdjustWidth">
             <td class="Text" colspan="4">Grading Result Entry</td>
        </tr>
        <tr>
             <td class="Text" colspan="3">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr class="EditorAlternate">
        <td class="Text" style="width:300px">Grading Code :</td>
        <td class="Input"style="width:550px">
            <asp:DropDownList ID="cboGradingCode" runat="server" Width="300px" 
                onselectedindexchanged="cboGradingCode_SelectedIndexChanged" 
                AutoPostBack="True" >
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                ControlToValidate="cboGradingCode" ></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td colspan="2">Has Received Grade? 
            <asp:CheckBox ID="chkRecivedGrade" 
                runat="server" Checked="True" AutoPostBack="True" 
                oncheckedchanged="chkRecivedGrade_CheckedChanged" /></td>
        </tr>
        <tr class="EditorAlternate">
        <td class="Text">
            Commodity :</asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="300px" onselectedindexchanged="cboCommodity_SelectedIndexChanged" 
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
            Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
            PromptText="Please Select Commodity" ServiceMethod="GetCommoditiesContext" 
            ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity" 
                    UseContextKey="True">
        </cc1:CascadingDropDown>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="cboCommodity" ErrorMessage="*" 
            Font-Names="Calibri"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
        <td  class="Text">
            Commodity Class :</td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="300px" 
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
            Category="CommodityClass" Enabled="True" LoadingText="Loading Commodity Class..." 
            ParentControlID="cboCommodity" PromptText="Please Select Commodity Class" 
            ServiceMethod="GetCommodityClass" ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
        </cc1:CascadingDropDown>
        </td>
        </tr>

        <tr class="EditorAlternate">
        <td class="style1">
           Commodity Grade
            &nbsp;:</td>
            <td class="style1">
<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="300px" 
                    onselectedindexchanged="cboCommodityGrade_SelectedIndexChanged" 
                    AutoPostBack="True" ></asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateCheckBox"
    ErrorMessage="*"></asp:CustomValidator>
                    <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
            Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
            ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
            ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
            TargetControlID="cboCommodityGrade" UseContextKey="True">
        </cc1:CascadingDropDown>

            </td>
        </tr>
                <tr>
                <td class="Text">
           Grader/Cupper :
                    </td>
            <td class="Input">
                <asp:DropDownList ID="cboGrader" runat="server" Width="300px" >
                    <asp:ListItem Value="">Please Select Grader</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboGrader" 
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            </tr>
 
        <tr class="EditorAlternate">
            <td class="Text">Is Coordinator?</td>
            <td class="Input"><asp:CheckBox ID="isSupervisor" runat="server" /></td>
            
        </tr>
        <tr>
            <td>Code Generated Date :</td>
            <td>
               
                <asp:TextBox ID="lblCodeGeneratedDate" runat="server" Enabled="False" Text=""></asp:TextBox>
                
                </td>
        </tr>
        <tr>
                       <td class="Text">
            Grade Completed Date :
                           </td>
            <td class="Input">
                <asp:TextBox ID="txtDateRecived" runat="server" ></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateRecived"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1"  Type="Date" ControlToValidate="txtDateRecived" Display="Dynamic"
          MinimumValue="<%#DateTime.Now.AddYears(-1).ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture) %>" 
          MaximumValue="<%#DateTime.Now.ToShortDateString().ToString(System.Globalization.CultureInfo.InvariantCulture)%>"
           runat="server" ErrorMessage="Future Date isn't Acceptable"></asp:RangeValidator>
            <%--<asp:CompareValidator ControlToValidate="txtDateRecived"  
                ID="cmpCodeGen" runat="server" Display="Dynamic"  
                 ErrorMessage="Result received date is less than the Code Generated Date" 
                 Type="Date"  Operator="GreaterThanEqual"></asp:CompareValidator>--%>
           
           
                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="txtDateRecived" Display="Dynamic" 
                    ErrorMessage="Grade Recieved Date can't be less than Code Genrated Date" 
                    ControlToCompare="lblCodeGeneratedDate" Operator="GreaterThanEqual"></asp:CompareValidator>--%>
           
           
                </td>
                </tr>
                <tr class="EditorAlternate">
                <td class="Text">Grade Completed Time :</td>
                <td class="Input" >
                <asp:TextBox ID="txtTimeRecived" runat="server"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTimeRecived"
                        ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>Production Year :</td>
            <td>
            <asp:DropDownList ID="cboProductionYear" runat="server" ></asp:DropDownList>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidatePY"
    ErrorMessage="*"></asp:CustomValidator>
            </td>
        </tr>
        <tr class="EditorAlternate">
          <td class="Text">
           Status
            &nbsp;:</td>
            <td class="Input">
                <asp:DropDownList ID="cboStatus" runat="server" Width="250px">
                    <asp:ListItem Value="">Please Select Status</asp:ListItem>
                    <asp:ListItem Value="1">New</asp:ListItem>
                    <asp:ListItem Value="5">Moisture Fail</asp:ListItem>
                    <asp:ListItem Value="6">General Rquierment fail</asp:ListItem>
                    <asp:ListItem Value="2">Cancelled</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboStatus" 
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr >
          <td class="Text">
            Remark
            &nbsp;:</td>
            <td class="Input">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="4" class="Button" >
        <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
<ContentTemplate>
            <asp:GridView ID="gvGradingFactors" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" EmptyDataText="No Records Found" ForeColor="#333333" 
                GridLines="None" Width="600px" PageSize="30" CssClass="Grid" 
                onrowdatabound="gvGradingFactors_RowDataBound" >
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle CssClass="GridRow" />
                <Columns>
                    <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblId" runat="server" Visible="false"  Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate> 

<ControlStyle Width="0px"></ControlStyle>
                    </asp:TemplateField>
                     <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblDataType" runat="server" Visible="false"  Text='<%# Bind("DataType") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                     <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblPossibleTypes" runat="server" Visible="false"  Text='<%# Bind("PossibleValues") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                     
                     <asp:TemplateField ControlStyle-Width="0Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblFailPoint" runat="server" Visible="false"  Text='<%# Bind("FailPoint") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                     
                     <asp:TemplateField ControlStyle-Width="0Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblisMax" runat="server" Visible="false"  Text='<%# Bind("isMax") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                      <asp:TemplateField ControlStyle-Width="0Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblisTotalValue" runat="server" Visible="false"  Text='<%# Bind("isInTotalValue") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                     
                     
                    
                    
                    <asp:TemplateField ControlStyle-Width="450Px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"  HeaderText="Grading Factor">
                        <ItemTemplate>
                            <asp:Label ID="lblGradingFactorName" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblGradingFactorName" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </EditItemTemplate>

<ControlStyle Width="450px"></ControlStyle>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField >
                    <asp:TemplateField ControlStyle-Width="50Px"  HeaderText="Result">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGradingFactorValue" runat="server" ></asp:TextBox>
                        </ItemTemplate>

<ControlStyle Width="50px"></ControlStyle>

                    </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblEmpty" CssClass="Message" runat="server" Visible="false"  Text="*"></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="50px"></ControlStyle>
                     </asp:TemplateField>
                 
                 
                </Columns>
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
                <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
                    CssClass="GridSelectedRow" />
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle CssClass="GridAlternate" />

            </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
            </td>
            
        </tr>
        <tr class="EditorCommand">
        <td colspan="4" align="Left">
        <asp:Button ID="btnSave" runat="server" Height="30px" Text="Save" Width="125px" onclick="btnSave_Click"  
                />
                &nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Height="30px" CausesValidation="false" 
        Text="Cancel" Width="95px"   
        onclick="btnClear_Click" />
            </td>
         
        </tr>
  </table>
   <script language="javascript" type="text/javascript">
       function ValidateCheckBox(Source, args) {
           var chkAnswer = document.getElementById('<%= chkRecivedGrade.ClientID %>');
           var txtCG = document.getElementById('<%= cboCommodityGrade.ClientID %>');

           if (chkAnswer.checked == true) {
               if (txtCG.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
       function ValidatePY(Source, args) {
           var chkAnswer = document.getElementById('<%= chkRecivedGrade.ClientID %>');
           var txtCG = document.getElementById('<%= cboProductionYear.ClientID %>');

           if (chkAnswer.checked == true) {
               if (txtCG.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }






       
   </script> 
