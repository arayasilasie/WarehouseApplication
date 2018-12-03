<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditConsignment.aspx.cs" Inherits="WarehouseApplication.EditConsignment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">

<div style="clear: both; width: 87%; margin-left: 40px;">
<asp:GridView ID="grvGRNApproval" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataKeyNames="GRNNumber" GridLines="Vertical" Style="font-size: small" 
        CssClass="label"  OnRowDataBound="grvGRNApproval_RowDataBound" AllowPaging="True" 
         PageSize="30" onselectedindexchanged="grvGRNApproval_SelectedIndexChanged" >
                        <EmptyDataTemplate>
                            <asp:Label ID="lbl" runat="server" Text="No GRN to approve." /></EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Select" Visible="false">
                                <ItemTemplate>
                                    <%--  <div id="gv">--%>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" Onclick="ClientCheck();" />
                                    <%-- </div>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRNNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblGRNNo" runat="server" Text='<%# Bind("GRNNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblClientID" runat="server" Text='<%# Bind("ClientID") %>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client">
                                <ItemTemplate>
                                    <asp:Label ID="lblClient" runat="server" Text='<%# Bind("IDNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Symbol">
                                <ItemTemplate>
                                    <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P.Year" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionYear" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Warehouse">
                                <ItemTemplate>
                                    <%--  <div id="gv">--%>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="false" Onclick="ClientCheck();" />
                                    <%-- </div>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weight" ItemStyle-VerticalAlign="NotSet" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNetWeight" Width="60px" runat="server" Text='<%# Bind("NetWeight") %>'></asp:TextBox>
                                    <%--<asp:Label ID="lblNetWeight" runat="server" Text='<%# Bind("NetWeight") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="StackID" Visible="true">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtStackID" Width="60px" runat="server"></asp:TextBox>                                   
                                </ItemTemplate>
                            </asp:TemplateField><%--Text='<%# Bind("StackID")' %> Text='<%# Bind("ShedNumber") %>'Text='<%# Bind("LIC") %>'--%>
                            <asp:TemplateField HeaderText="Shed">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtShedNumber" Width="60px" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLIC" Width="60px" runat="server" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DateTimeLICSigned" HeaderText="LIC Signed">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Status
                                            </td>
                                            <td>
                                                Date
                                            </td>
                                            <td>
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
                                                    ControlToValidate="txtDateTimeLICSigned"></asp:TextBox>
                                                <asp:Label Width="70px" Visible="false" Text='<%#Bind("GRNCreatedDate") %>' ID="lblGRNCreatedDate"
                                                    runat="server"></asp:Label>
                                                <asp:RangeValidator ID="DateRangeValidator" ValidationGroup="Save" Type="Date" ControlToValidate="txtDateTimeLICSigned"
                                                    Display="None" ForeColor="Tomato" runat="server" ErrorMessage="Please enter valid date."></asp:RangeValidator>
                                                <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="DateRangeValidator">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTimeLICSigned" Width="60px" runat="server" Text='<%# Bind("ApprovedTime") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <ajaxToolkit:MaskedEditExtender ID="EarliestTimeExtender1" runat="server" AcceptAMPM="True"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtTimeLICSigned">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="EarliestTimeValidator1" runat="server" ControlExtender="EarliestTimeExtender1"
                                        ControlToValidate="txtTimeLICSigned" Display="Dynamic" InvalidValueMessage="Please enter a valid time."
                                        SetFocusOnError="True">                                          
                                    </ajaxToolkit:MaskedEditValidator>
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
                                    <asp:TextBox ID="txtLICStatus" Width="60px" runat="server" Text='<%# Bind("LICStatus") %>'></asp:TextBox>
                                    <%--<asp:Label ID="lblLICStatus" runat="server" Text='<%# Bind("LICStatus") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Approve" ShowSelectButton="True" 
                                ValidationGroup="Save" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#e4efd0" />
                    </asp:GridView>

</div>


</asp:Content>
