<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="EnablePUNPrint.aspx.cs" Inherits="WarehouseApplication.EnablePUNPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ShowHand
        {
            cursor: pointer;
        }
        .style1
        {
            font-size: small;
        }        
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <script type="text/javascript">
       
        document.forms[0].elements['<%= btnPrintPUN.ClientID %>'].style.visibility = "hidden";
   
        function ClientCheck() {
            var flag = false;
            for (var i = 0; i < document.forms[0].length; i++) {
                if (document.forms[0].elements[i].id.indexOf('chkSelect') != -1) {
                    if (document.forms[0].elements[i].checked) {
                        flag = true
                    }
                }
            }
            if (flag == true) {
                document.forms[0].elements['<%= btnPrintPUN.ClientID %>'].style.visibility = "visible";
              
                return true
            }
            else {
             
                document.forms[0].elements['<%= btnPrintPUN.ClientID %>'].style.visibility = "hidden";
               
                return false
            }
        }       
    </script>
    <table width="1000px" bgcolor="white">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        
                        <uc1:Messages ID="Messages" runat="server" />
                      
                        <table width="1000px">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="background-color: #88AB2D">
                                                <asp:Label ID="lblSearchPUN" runat="server" Text="Search Pickup Notice" ForeColor="White"
                                                    Width="900px" CssClass="label"></asp:Label>
                                                <span class="style1">&nbsp; </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblWareHouseReceipt" runat="server" Text="WHR #:" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMemberId" runat="server" Text="Member/" 
                                                    CssClass="label"></asp:Label>
                                                     <asp:Label ID="lblClientId" runat="server" Text="Client Id :" 
                                                    CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text="Status :" CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExpirationDateFrom" runat="server" Text="Expire Date From :"
                                                    CssClass="label"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExpirationDateTo" runat="server" Text="Expire Date To  :" 
                                                    CssClass="label"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtWareHouseReceipt" Width="100px" runat="server" CssClass="style1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtClientId" Width="100px" runat="server" CssClass="style1"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpStatus" runat="server" Width="145px" CssClass="style1">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpirationDateFrom" runat="server" CssClass="style1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtExpirationDateFrom_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtExpirationDateFrom">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExpirationDateTo" runat="server" CssClass="style1"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtExpirationDateTo_CalendarExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtExpirationDateTo">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                    BackColor="#88AB2D" ForeColor="White" Width="80px" BorderStyle="None" 
                                                    CssClass="style1" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnPrintPUN" runat="server" OnClick="btnPrintPUN_Click" Text="Print PUN"
                                                    BackColor="#88AB2D" ForeColor="White" Width="80px" BorderStyle="None" 
                                                    CssClass="style1" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Remark:"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="620px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlGrid" ScrollBars="Vertical" Height="800px" runat="server">
                                        <asp:GridView ID="gvSearchPickupNotice" runat="server" AutoGenerateColumns="False"
                                            CellPadding="1" ForeColor="Black" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" DataKeyNames="Id,CommodityGradeID,ProductionYear,WeightInKg,MaxLimit,WarehouseReceiptNo"
                                            OnRowDataBound="gvSearchPickupNotice_RowDataBound" CssClass="label"
                                            PageSize="20" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="#E5E5E5" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <div id="gv">
                                                            <asp:CheckBox ID="chkSelect" runat="server" Onclick="ClientCheck();" AutoPostBack="false" />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="cmdClientName" CssClass="ShowHand" runat="server" Text='<%#Eval("ClientName")%>'
                                                            ID="ClientName" CommandArgument='<% #Bind("ClientName") %>' OnClick="ClientName_Click">LinkButton </asp:LinkButton>
                                                        <ajaxToolkit:PopupControlExtender ID="PopupControlExtender2" runat="server" TargetControlID="ClientName"
                                                            PopupControlID="pnlAgentView" CommitProperty="value" Position="Bottom" CommitScript="e.value += ' - do not forget!';" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CommodityName" HeaderText="Commodity Grade" ItemStyle-Width="20px" ItemStyle-Wrap="True" />
                                                <asp:BoundField DataField="WarehouseReceiptNo" ItemStyle-Width="20px" HeaderText="WareHouse Receipt" />
                                                <asp:BoundField DataField="GRNNo" HeaderText="GRN No" />
                                                <asp:BoundField DataField="ShedName" HeaderText="Shed" />
                                                <asp:BoundField DataField="QuantityInLot" HeaderText="Qty" ItemStyle-Width="80px" />
                                                <asp:BoundField DataField="WeightInKg" HeaderText="Weight" ItemStyle-Width="80px" />
                                                <asp:BoundField DataField="ProductionYear" ItemStyle-Width="20px" HeaderText="Production Year" />
                                                <asp:BoundField DataField="ExpirationDate" DataFormatString="{0:M-dd-yyyy}" ItemStyle-Width="80px" HeaderText="Expiration Date" />
                                                <asp:BoundField DataField="StatusName" HeaderText="Status" />
                                                <asp:TemplateField HeaderText="UnApprovedGINCount" Visible='false'>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnApprovedGINCount" runat="server" Text='<%# Eval("UnApprovedGINCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblEmptyData" runat="server" BackColor="White" BorderColor="White"
                                                    BorderStyle="None" BorderWidth="0px" Text="There is no data with your selection criteria"></asp:Label>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowModalPopup"
                                        PopupControlID="divPopUp" BackgroundCssClass="popUpStyle" PopupDragHandleControlID="panelDragHandle"
                                        DropShadow="true" />
                                    <br />
                                    <div id="divPopUp" style="border-style: solid; border-width: thin; display: none;
                                        background-color: White; border-color: Black;">
                                        <asp:Label ID="lblGINNo" runat="server"></asp:Label>
                                        <br />
                                        <center>
                                            <asp:DataList ID="dlAgent" runat="server" Width="300px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgentIDNo" runat="server" Style="font-weight: 700" Text="Agent ID Number"></asp:Label>
                                                    :&nbsp;
                                                    <asp:Label ID="AgentIDNumberLabel" runat="server" Text='<%# Eval("AgentIDNumber") %>' />
                                                    <br />
                                                    <asp:Label ID="lblAgentName" runat="server" Style="font-weight: 700" Text="Agent Name:"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="AgentNameLabel" runat="server" Text='<%# Eval("AgentName") %>' />
                                                    <br />
                                                    <br />
                                                </ItemTemplate>
                                            </asp:DataList>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- </form>--%><span class="style1"> </span>
            </td>
        </tr>
    </table>
</asp:Content>
