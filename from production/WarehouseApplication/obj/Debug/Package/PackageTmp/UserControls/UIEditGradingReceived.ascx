<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UIEditGradingReceived.ascx.cs" Inherits="WarehouseApplication.UserControls.UIEditGradingReceived" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<table class="PreviewEditor" style="width:700px"  >
<tr class="PreviewEditorCaption">
<td  colspan="4">Grading Recived</td>
</tr>
<tr>
             <td class="Text" colspan="4">
                 <asp:Label ID="lblMsg" CssClass="Message"  runat="server"  Text=""></asp:Label></td>
        </tr>
        <tr>
        <td class="Text" style="width: 200px;"><asp:Label ID="lblGradingCode" runat="server" Text="Grading Code :"></asp:Label></td>
        <td class="Input">
            <asp:TextBox ID="txtGradingCode" runat="server" Enabled="False" Width="247px"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity :"></asp:Label>
        </td>
            <td class="Input">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="250px"  
              >
        </asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodity_CascadingDropDown" runat="server" 
                    Category="Commodity" Enabled="True" LoadingText="Loading Commodities " 
                    PromptText="Please Select Commodity" ServiceMethod="GetCommodities" 
                    ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodity">
                </cc1:CascadingDropDown>
            </td>
        </tr>
        <tr>
        <td colspan="2">Has Recieved&nbsp; Grade? 
            <asp:CheckBox ID="chkRecivedGrade" 
                runat="server" AutoPostBack="True" oncheckedchanged="chkRecivedGrade_CheckedChanged" 
                 /></td>
        </tr>
        <tr>
        <td  class="Text">
            <asp:Label ID="Label1" runat="server" Text="Commodity Class :"></asp:Label>
        </td>
        <td class="Text">
        <asp:DropDownList ID="cboCommodityClass" runat="server" Width="250px" 
            ></asp:DropDownList>
            <cc1:CascadingDropDown ID="cboCommodityClass_CascadingDropDown" runat="server" 
                Category="CommodityClass" Enabled="True" 
                LoadingText="Loading Commodity Class..." ParentControlID="cboCommodity" 
                PromptText="Please Select Commodity Class" ServiceMethod="GetCommodityClass" 
                ServicePath="~\UserControls\Commodity.asmx" TargetControlID="cboCommodityClass">
            </cc1:CascadingDropDown>
        </td>
        </tr>

        <tr>
        <td class="Text">
            <asp:Label ID="lblCommodityGrade" runat="server" Text="Commodity Grade :"></asp:Label>
            </td>
            <td class="Input">

<asp:DropDownList ID="cboCommodityGrade" runat="server" Width="250px" AutoPostBack="True" onselectedindexchanged="cboCommodityGrade_SelectedIndexChanged" 
                     ></asp:DropDownList>
                <cc1:CascadingDropDown ID="cboCommodityGrade_CascadingDropDown" runat="server" 
                    Category="CommodityGrade" Enabled="True" LoadingText="Loading Commodity Grades" 
                    ParentControlID="cboCommodityClass" PromptText="Please Select Commodity Grade" 
                    ServiceMethod="GetCommodityGrades" ServicePath="~\UserControls\Commodity.asmx" 
                    TargetControlID="cboCommodityGrade">
                </cc1:CascadingDropDown>
            </td>
        </tr>
        <tr>
                <td class="Text">
            <asp:Label ID="Label2" runat="server" Text="Grade Recived Date :"></asp:Label>
            </td>
            <td class="Input">
                <asp:TextBox ID="txtDateRecived" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateRecived_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDateRecived">
                </cc1:CalendarExtender>
                <asp:Label ID="Label3" runat="server" Text="Time :"></asp:Label>
                <asp:TextBox ID="txtTimeRecived" runat="server" Width="70px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="txtTimeodAcceptance_MaskedEditExtender" 
            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="99:99:99" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
            TargetControlID="txtTimeRecived" AcceptAMPM="True" 
            ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time">
        </cc1:MaskedEditExtender>
                </td>
               
        </tr>
        <tr>
            <td class="Text">Is Coordinator?:</td>
            <td class="Input"><asp:CheckBox ID="isSupervisor" runat="server" /></td>
            
        </tr>
         <tr>
            <td>Production Year :</td>
            <td>
            <asp:DropDownList ID="cboProductionYear" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="cboProductionYear_SelectedIndexChanged" ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="cboProductionYear" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
          <td class="Text">
            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
            &nbsp;:</td>
            <td class="Input">
                <asp:DropDownList ID="cboStatus" runat="server" Width="250px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="cboStatus_SelectedIndexChanged" >
                    <asp:ListItem Value="1">New</asp:ListItem>
                    <asp:ListItem Value="3">Client Accepted</asp:ListItem>
                    <asp:ListItem Value="4">Client Rejected</asp:ListItem>
                    <asp:ListItem Value="5">Moisture Fail</asp:ListItem>
                    <asp:ListItem Value="6">Genral Rquierment fail</asp:ListItem>
                    <asp:ListItem Value="6">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td class="Text">
            <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
            &nbsp;:</td>
            <td class="Input">
                <asp:TextBox ID="txtRemark" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>

        <table>
        <table>
        <tr>
            <td colspan="4" class="Message" >
                <asp:Label ID="lblmsg2" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
        <td colspan="4" class="text" >
        
            <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
         <ContentTemplate>
        <asp:Panel ID="pnlGradingFactorsOld"  runat="server" >
        
            <asp:GridView ID="gvGradingFactors" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" EmptyDataText="No Records Found" ForeColor="#333333" 
                GridLines="None" Width="600px" PageSize="20" 
                onrowediting="gvGradingFactors_RowEditing" 
                onrowcancelingedit="gvGradingFactors_RowCancelingEdit" 
                onrowupdated="gvGradingFactors_RowUpdated" 
                onrowupdating="gvGradingFactors_RowUpdating" CssClass="Grid" >
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle CssClass="GridRow" />
                <Columns>
                    <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblId" runat="server" Visible="false"  Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate> 

<ControlStyle Width="1px"></ControlStyle>
                    </asp:TemplateField>
                     <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblDataType" runat="server" Visible="false"  Text='<%# Bind("DataType") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="1px"></ControlStyle>
                     </asp:TemplateField>
                     <asp:TemplateField ControlStyle-Width="1Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblPossibleTypes" runat="server" Visible="false"  Text='<%# Bind("PossibleValues") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="1px"></ControlStyle>
                     </asp:TemplateField>
                    
                    
                    <asp:TemplateField ControlStyle-Width="450Px"  HeaderText="Grading Factor">
                        <ItemTemplate>
                            <asp:Label ID="lblGradingFactorName" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblGradingFactorName" runat="server" Text='<%# Bind("GradingFactorName") %>'></asp:Label>
                        </EditItemTemplate>

<ControlStyle Width="450px"></ControlStyle>
                    </asp:TemplateField >
                    <asp:TemplateField ControlStyle-Width="50Px"  HeaderText="Result">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGradingFactorValue" Enabled="false" runat="server" Text='<%# Bind("RecivedValue") %>' ></asp:TextBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGradingFactorValue" runat="server" Text='<%# Bind("RecivedValue") %>' ></asp:TextBox>
                        </EditItemTemplate>

<ControlStyle Width="50px"></ControlStyle>

                    </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblEmpty" CssClass="Message" runat="server" Visible="false"  Text="*"></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="50px"></ControlStyle>
                     </asp:TemplateField>
                     <asp:TemplateField ControlStyle-Width="0Px" >
                        <ItemTemplate >
                            <asp:Label ID="lblisTotalValue" runat="server" Visible="false"  Text='<%# Bind("isInTotalValue") %>'></asp:Label>
                        </ItemTemplate>

<ControlStyle Width="0px"></ControlStyle>
                     </asp:TemplateField>
                     

                                      
                <asp:TemplateField ControlStyle-Width="100Px" >
                        <ItemTemplate >
                            <asp:Button ID="btnEdit" Width="50" runat="server" Text="Edit" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button ID="btnEdit" Width="50"  runat="server" Text="Update" CommandName="Update" />
                            <asp:Button ID="btnCancel" Width="50"  runat="server" Text="Cancel" CommandName="Cancel" />

                        </EditItemTemplate>

<ControlStyle Width="100px"></ControlStyle>

                     </asp:TemplateField>
                     
                     
                </Columns>
                <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="GridPager" />
                <SelectedRowStyle Font-Bold="True" ForeColor="#333333" 
                    CssClass="GridSelectedRow" />
                <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="GridHeader" />
                <EditRowStyle BackColor="#FFCC66" />
                <AlternatingRowStyle CssClass="GridAlternate" />

            </asp:GridView>
                  </asp:Panel>
            <asp:Panel ID="pnlGradingFactorsNew" Visible="false"  runat="server" GroupingText="New Grading Factors" >
            <asp:GridView ID="gvGradingFactorsNew" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" EmptyDataText="No Records Found" ForeColor="#333333" 
                GridLines="None" Width="600px" PageSize="20" CssClass="Grid" >
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
          </asp:Panel>
            
            
  
        </ContentTemplate>
           </asp:UpdatePanel>
            </td>
            
        </tr>
              <tr>
        <td colspan="3" align="Left">
        <asp:Button ID="btnUpdate" runat="server" Height="30px" Text="Update" Width="125px" onclick="btnUpdate_Click"  
                />  &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnClear" runat="server" Height="30px" CausesValidation="false" 
        Text="Cancel" Width="95px"  
        onclick="btnClear_Click" />
           <asp:HiddenField ID="hfId" runat="server" />
            </td>
         
        </tr>

  </table>
