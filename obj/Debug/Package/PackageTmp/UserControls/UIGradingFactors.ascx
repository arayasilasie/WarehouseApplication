<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIGradingFactors.ascx.cs" Inherits="WarehouseApplication.UserControls.UIGradingFactors" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<table class="PreviewEditor" style="width:900px"  >
<tr class="PreviewEditorCaption">
<td colspan="4" style="width:475px" >Add Grading Factors </td>
</tr>
<tr>
<td colspan="3"> <asp:Label  ID="lblMessage" runat="server" CssClass="Message" Text=""></asp:Label></td>
</tr>
<tr>
<td  class="style1" >Grading Factor Name&nbsp; :
                 </td>
<td class="style1">
    <asp:TextBox ID="txtGradingFactorName" Width="350px" runat="server" 
        TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvGradingFactorName" runat="server" 
        ErrorMessage="*" ControlToValidate="txtGradingFactorName" ></asp:RequiredFieldValidator>
    </td>
</tr>

<tr class="EditorAlternate">
    <td class="style2">Grading Factor Type :</td>
    <td colspan="3" class="style2" ><asp:DropDownList ID="cboGradingFactorType" runat="server"   
                 Width="200px" TabIndex="1" >
        <asp:ListItem Value="">Please Select Grading Factor Type</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfVGradingFactorType" runat="server" 
            ControlToValidate="cboGradingFactorType"  ErrorMessage="*"></asp:RequiredFieldValidator>
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
<asp:Button ID="btnSave" runat="server" Text="Save" 
          onclick="btnSave_Click" style="margin-bottom: 0px; height: 26px;" Width="95px" />&nbsp;
          <asp:Button ID="btnClear" runat="server" Text="Cancel" Width="95px"  style="margin-bottom: 0px; height: 26px;" />
         </td>
</tr>
<tr class="PreviewEditorCaption">
<td colspan="2"> Grading Factors List :</td>
</tr>
<tr>
<td colspan="2">
<asp:Label  ID="lblMessage2" runat="server" CssClass="Message" Text=""></asp:Label>
</td>
</tr>
</tr>
<tr>
<td>Grading Factor Name :</td>
<td>
    <asp:TextBox ID="txtSearchGradingFactorName" runat="server"></asp:TextBox></td>
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
    <asp:DropDownList ID="cboSearchStatus" runat="server">
            <asp:ListItem Value="" >Please Select Status</asp:ListItem>
            <asp:ListItem Value="1">Active</asp:ListItem>
            <asp:ListItem Value="0">Cancelled</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr class="EditorCommand">
<td colspan="2" align="left"  >
    <asp:Button ID="btnSearch" CausesValidation="false" runat="server" Text="Search" 
        onclick="btnSearch_Click" /></td>
</tr>
<tr>
<td colspan="2">
<asp:GridView ID="gvGF"  runat="server" AutoGenerateColumns="False" 
        CellPadding="3" ForeColor="#333333" GridLines="None" AllowPaging="True" 
        EmptyDataText="No Matching  Records Found." 
        onpageindexchanged="gvGF_PageIndexChanged" 
        onpageindexchanging="gvGF_PageIndexChanging" CssClass="Grid" 
        Width="911px" onrowcommand="gvGF_RowCommand" 
        onrowediting="gvGF_RowEditing" onrowupdated="gvGF_RowUpdated" 
        onrowupdating="gvGF_RowUpdating" 
        onrowcancelingedit="gvGF_RowCancelingEdit" Height="317px" 
        onrowdatabound="gvGF_RowDataBound">
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
                    <EditItemTemplate>
                    <asp:Label ID="lblEditId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </EditItemTemplate>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grading Factor Name " Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblGradingFactorName" Visible="true" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                         <asp:TextBox ID="txtGradingFactorName" Width="100px"
                         Runat="server" 
                         Text='<%# Bind("GradingFactorName") %>'></asp:TextBox>
                        </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grading Factor Type">
                    <ItemTemplate>
                        <asp:Label ID="lblGradingFactorType" runat="server" Text='<%# Bind("GradingFactorTypeName") %>'></asp:Label>
                    </ItemTemplate>   
                    <EditItemTemplate>
                     <asp:Label ID="GradingTypeId" Visible="false" runat="server" Text='<%# Bind("GradingTypeId") %>'></asp:Label>
                    <asp:DropDownList ID="cboEditGradingFactorType" runat="server"     
                 Width="200px" TabIndex="1"  >
        <asp:ListItem Value="">Please Select Grading Factor Type</asp:ListItem>
        </asp:DropDownList>
                    </EditItemTemplate>    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate> 
                    <EditItemTemplate>
                        <asp:DropDownList ID="cboEditStatus" runat="server"   SelectedValue='<%# Bind("Status") %>'>
                            <asp:ListItem Value="" >Please Select Status</asp:ListItem>
                            <asp:ListItem Value="Active">Active</asp:ListItem>
                            <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                     </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
               
                
                <asp:CommandField ShowEditButton="True" ItemStyle-CssClass="Menu" CausesValidation="False" />
                
            </Columns>
        <EditRowStyle CssClass="GridSelectedRow" />
        <AlternatingRowStyle CssClass="GridAlternate" />
    </asp:GridView>

</td>
</tr>

</table>

