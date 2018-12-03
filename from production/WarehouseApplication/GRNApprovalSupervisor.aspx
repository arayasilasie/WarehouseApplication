<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GRNApprovalSupervisor.aspx.cs" Inherits="WarehouseApplication.GRNApprovalSupervisor" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //checking future date
        function dateselect(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Please enter valid date.");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))


                var textBox2 = sender._textbox

                textBox2.value = ''

            }
            else {
                var calendarBehavior1 = $find("CalanderDateTimeClientSigned");
                var d = calendarBehavior1._selectedDate;
                var now = new Date();
                calendarBehavior1.get_element().value = d.format("MM/dd/yyyy") + " " + now.format("HH:mm:ss")
            }

        }


    </script>

    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .style1
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                 <uc1:Messages ID="Messages1" runat="server" />


                  <br />
                <div id="Header" class="formHeader" style="width: 87%; margin-left: 40px; margin-top: 5px;"
                    align="center">
                    <asp:Label ID="lblDetail" Text="Supervisor GRN Approval" Width="100%" runat="server"></asp:Label>
                </div>
                <div style="float: left; width: 87%; margin-left: 40px;">
                    <div style="margin-bottom: 10px;">
                        <div style="border: solid 1px #88AB2D; height: 70px;">
                            <div style="margin-top: 10px; float: left; height: 26px; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="GRN No :"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtGRNNo" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; margin-left: 7px; float: left;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblClientId0" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblLIC0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddLIC" runat="server" AppendDataBoundItems="True" CssClass="style1"
                                        ValidationGroup="Search" Width="145px">
                                        <asp:ListItem Value="">Select LIC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 20px;">
                                <div style="height: 26px;">
                                </div>
                                <div>
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: right; margin-right: 10px;">
                                <div style="height: 26px">
                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both; width: 87%; margin-left: 40px;">
                    <asp:GridView ID="grvGRNApproval" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ID"
                        GridLines="Vertical" Style="font-size: small" CssClass="label" OnPageIndexChanging="grvGRNApproval_PageIndexChanging"
                        AllowPaging="True" OnRowDataBound="grvGRNApproval_RowDataBound" PageSize="10"
                        OnSelectedIndexChanged="grvGRNApproval_SelectedIndexChanged">
                        <EmptyDataTemplate>
                            <asp:Label ID="lbl" runat="server" Text="No GRN to approve." /></EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="GRNNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblGRNNo" runat="server" Text='<%# Bind("GRN_Number") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblClientID" runat="server" Text='<%# Bind("ClientID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client">
                                <ItemTemplate>
                                    <asp:Label ID="lblClient" runat="server" Text='<%# Bind("Client") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Symbol">
                                <ItemTemplate>
                                    <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weight" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWeight" runat="server" Text='<%# Bind("NetWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shed">
                                <ItemTemplate>
                                    <asp:Label ID="lblShedNumberr" runat="server" Text='<%# Bind("ShedNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DateTimeLICSigned" HeaderText="WH Supervisor Signed">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="style1">
                                                Status
                                            </td>
                                            <td class="style1">
                                                Date
                                            </td>
                                            <td class="style1">
                                                Time
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpLICStatus" Width="70px" runat="server">
                                                    <%-- <asp:ListItem Text="Select" Value=""> </asp:ListItem>--%>
                                                    <asp:ListItem Text="Accept" Value="1"> </asp:ListItem>
                                                    <asp:ListItem Text="Reject" Value="2"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               
                                                <asp:TextBox ID="txtDateTimeLICSigned" Width="70px" runat="server" Text='<%# Bind("ApprovedDateTime") %>'
                                                    ControlToValidate="txtDateTimeLICSigned" ValidationGroup="Approve"></asp:TextBox>
                                                <asp:Label Width="70px" Visible="false" Text='<%#Bind("GRNCreatedDate") %>' ID="lblGRNCreatedDate"
                                                    runat="server"></asp:Label>
                                                <asp:RangeValidator ID="DateRangeValidator" ValidationGroup="Save" Type="Date" ControlToValidate="txtDateTimeLICSigned"
                                                    Display="None" ForeColor="Tomato" runat="server" ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="DateRangeValidator">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTimeLICSigned" Width="60px" runat="server" 
                                                    Text='<%# Bind("ApprovedTime") %>' ValidationGroup="Save"></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeLICSigned">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                        ControlToValidate="txtTimeLICSigned" Display="Dynamic" InvalidValueMessage="Please enter a valid time."
                                        SetFocusOnError="True" ValidationGroup="Save"></ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="EarliestTimeValidator1_ValidatorCalloutExtender" 
                                        runat="server" Enabled="True" TargetControlID="EarliestTimeValidator1">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderDateTimeLICSigned" runat="server"
                                        Enabled="True" TargetControlID="txtDateTimeLICSigned">
                                    </ajaxToolkit:CalendarExtender>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LICStatus" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLICStatus" runat="server" Text='<%# Bind("LICStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StackID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStackID" runat="server" Text='<%# Bind("StackID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Approve" ShowSelectButton="True" 
                                ValidationGroup="Save" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#e4efd0" />
                    </asp:GridView>
                </div>




            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
