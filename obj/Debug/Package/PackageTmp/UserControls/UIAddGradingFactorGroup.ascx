<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIAddGradingFactorGroup.ascx.cs" Inherits="WarehouseApplication.UserControls.UIAddGradingFactorGroup" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Add Grading Factors Group</td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
<td  >Grading Factor Group Name&nbsp; :
                 </td>
<td>
    <asp:TextBox ID="txtGradingFactorName" Width="350px" runat="server" 
        TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvGradingFactorName" runat="server" 
        ErrorMessage="*" ControlToValidate="txtGradingFactorName" ></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
    <td class="TextRequired" >Status :</td>
    <td colspan="3" class="Input" >
        <asp:DropDownList ID="cboStatus" Width="200px" runat="server" Enabled="True" 
             >
            <asp:ListItem Value="">Please Select Status</asp:ListItem>
            <asp:ListItem Value="1">Active</asp:ListItem>
            <asp:ListItem Value="0">Cancelled</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvStatus" ControlToValidate="cboStatus" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
</tr>
<tr class="EditorCommand">
<td colspan="4" align="Left" >
        &nbsp;
          </td>
</tr>
<tr class="PreviewEditorCaption">
<td colspan="2"> Select Grading Factors List :</td>
</tr>

<tr>
<td>Grading Factor Name :me :</td>
<td>
    <asp:TextBox ID="txtSearchGradingFactorName" runat="server"></asp:TextBox>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    
    </asp:UpdateProgress>
    </td>
</tr>
<tr>
<td>Grading Factor Type :</td>
<td>
    <asp:DropDownList ID="cboSearchGradingFactorTypeId" runat="server">
        <asp:ListItem Value="" >Please Select Grading Factor Type</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>Status : </td>
<td>
    <asp:DropDownList ID="cboSearchStatus" runat="server" Enabled="false">
            <asp:ListItem Value="" >Please Select Status</asp:ListItem>
            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left"  >
    <asp:Button ID="btnSearch" CausesValidation="false" runat="server" Text="Search" 
        onclick="btnSearch_Click" />
    </td>
</tr>
<tr>
<td colspan="2">
    <asp:UpdatePanel ID="upGFGrids" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<asp:GridView ID="gvGF"  runat="server" AutoGenerateColumns="False" 
        CellPadding="3" ForeColor="#333333" GridLines="None" AllowPaging="True" 
        EmptyDataText="No Matching  Records Found." 
         
        CssClass="Grid" 
        Width="911px" onpageindexchanged="gvGF_PageIndexChanged" onpageindexchanging="gvGF_PageIndexChanging" 
    >
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
        <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
            CssClass="GridSelectedRow" />
        <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
        <RowStyle CssClass="GridRow" />
        <Columns>
                <asp:TemplateField HeaderText="" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="True">
                
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkSelected">
                    </asp:CheckBox>
                </ItemTemplate>
                <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="SelectAll(this);" />
                        </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grading Factor Name " Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblGradingFactorName" Visible="true" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="false">
                    <ItemTemplate>
                       <asp:Label ID="lblGradingTypeId" runat="server" Text='<%# Bind("GradingTypeId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Max. Val" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtMaxValue" Width="50px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Min. Val" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtMinValue" Width="50px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fail Point" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFailPoint" Width="50px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="is Max" Visible="true">
                    <ItemTemplate>
                        <asp:DropDownList ID="cboIsMax" runat="server">
                         <asp:ListItem Value="1">Max</asp:ListItem>
                         <asp:ListItem Value="2">Min</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="In Tot. Value" Visible="True" ItemStyle-HorizontalAlign="Center">   
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkInTotalValue">
                    </asp:CheckBox>
                </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Possible Values" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPossibleValues" Width="100px" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="Status" Visible="true">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                         <asp:ListItem Value="1">Active</asp:ListItem>
                         <asp:ListItem Value="2">Cancelled</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>      
            </Columns>
        <EditRowStyle CssClass="GridSelectedRow" />
        <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>
    </ContentTemplate>
        </asp:UpdatePanel>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left">
    <asp:Button ID="btnSave" runat="server" CausesValidation="false" Text="Save" Width="100" onclick="btnSave_Click" /> </td>
</tr>

</table>