<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddCommodityGradingFactor.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddCommodityGradingFactor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:500px" >Add Commodity Grading Factor.</td>
</tr>
<tr class="EditorAlternate">
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateCheckBox"
    ErrorMessage="Please Select Commodity Or Commodity Grade."></asp:CustomValidator>
    </td>
</tr>



<tr>
<td style="width:250px" class="Text"  >Grading Factor Group Name:
                 </td>
<td>
    <asp:DropDownList ID="cboGradingFactorName" Width="250px" runat="server">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvGradingFactorName" runat="server" 
        ErrorMessage="*" ControlToValidate="cboGradingFactorName" ></asp:RequiredFieldValidator>
    </td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Is for Commodity?</td>
    
<td><asp:CheckBox ID="chkIsCommodity" runat="server" /></td>
</tr>
<tr>
<td class="Text">Commodity: </td>
<td>
                <asp:DropDownList ID="cboCommodity" runat="server" Width="300px" onselectedindexchanged="cboCommodity_SelectedIndexChanged" 
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
            Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
            PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
            ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity">
        </cc1:CascadingDropDown>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Commodity Class: </td>
<td>
<asp:DropDownList ID="cboCommodityClass" runat="server" Width="300px" 
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
            Category="CommodityClass" Enabled="True" LoadingText="Loading Commodity Class..." 
            ParentControlID="cboCommodity" PromptText="Please Select Commodity Class" 
            ServiceMethod="GetCommodityClass" ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
        </cc1:CascadingDropDown>
</td>
</tr>
<tr>
<td class="Text">Commodity Grade : </td>
<td>
<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="300px" 
                    AutoPostBack="True" ></asp:DropDownList>
                                
                    <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
            Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
            ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
            ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
            TargetControlID="cboCommodityGrade" UseContextKey="True">
        </cc1:CascadingDropDown>
</td>
</tr>
<tr class="EditorAlternate">
<td class="Text">Status: </td>
<td>
<asp:DropDownList ID="cboStatus" runat="server" >
            <asp:ListItem Value="" Selected="False" >Please Select Status</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
            <asp:ListItem Value="2" >Cancelled</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="3"> 

    &nbsp;</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left"><asp:Button ID="btnSave" runat="server" Width="100px" Text="Save" 
        onclick="btnSave_Click" /></td>
</tr>
<tr class="EditorAlternate">
<td colspan="2">Search Commodity Grading Factor :</td>
</tr>
<tr>
<td>Commodity : 
</td>
<td>
                <asp:DropDownList ID="cboSearchCommodity" runat="server" Width="300px" onselectedindexchanged="cboCommodity_SelectedIndexChanged" 
              >
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" 
            Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
            PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
            ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboSearchCommodity">
        </cc1:CascadingDropDown>
</td>
</tr>
<tr>
<td>Commodity Class:</td>
<td>
<asp:DropDownList ID="cboSearchCommodityClass" runat="server" Width="300px" 
            ></asp:DropDownList>
        <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" 
            Category="CommodityClass" Enabled="True" LoadingText="Loading Commodity Class..." 
            ParentControlID="cboSearchCommodity" PromptText="Please Select Commodity Class" 
            ServiceMethod="GetCommodityClass" ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboSearchCommodityClass">
        </cc1:CascadingDropDown>


</td>
</tr>
<tr>
<td class="Text">Commodity Grade : </td>
<td>
<asp:DropDownList ID="cboSearchCommodityGrade" runat="server" Width="300px" 
                    AutoPostBack="True" ></asp:DropDownList>
                                
                    <cc1:CascadingDropDown ID="CascadingDropDown3" runat="server" 
            Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
            ParentControlID="cboSearchCommodityClass" PromptText="Please Select Commodity Grade" 
            ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
            TargetControlID="cboSearchCommodityGrade" UseContextKey="True">
        </cc1:CascadingDropDown>
</td>
</tr>
<tr>
<td colspan="2"><asp:Button ID="btnSearch" CausesValidation="false"  runat="server" Text="Search" 
        onclick="btnSearch_Click" /></td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvGroup" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
        EmptyDataText="No Matching  Records Found." 
        CssClass="Grid" 
        Width="800px" onrowediting="gvGroup_RowEditing" 
        onrowupdating="gvGroup_RowUpdating" onrowcommand="gvGroup_RowCommand">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
            CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
        <EditRowStyle BackColor="#7C6F57" />
        <RowStyle CssClass="GridRow" />
        <Columns>
               <asp:TemplateField HeaderText="" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Grading Factor Group" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Bind("GroupName") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grading Factor Group" Visible="True">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grading Factor Group" Visible="True">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnCancel" CommandName="cmdCancel" CausesValidation="false"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server">Cancel</asp:LinkButton>
                    </ItemTemplate>       
                </asp:TemplateField>
               
   
            </Columns>
        <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>
</td>
</tr>
</table>
 <script language="javascript" type="text/javascript">
       function ValidateCheckBox(Source, args) {
           var chkAnswer = document.getElementById('<%= chkIsCommodity.ClientID %>');
           var txtCG = document.getElementById('<%= cboCommodityGrade.ClientID %>');
           var txtC = document.getElementById('<%= cboCommodity.ClientID %>');

           if (chkAnswer.checked == false) {
               if (txtCG.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
           else (chkAnswer.checked == true)
           {
               if (txtC.value == "")
                   args.IsValid = false;
               else
                   args.IsValid = true;
           }
       }
 






       
   </script> 
