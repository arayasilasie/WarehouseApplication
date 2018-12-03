<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GenerateGrading.aspx.cs" Inherits="WarehouseApplication.GenerateGrading" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .align
        {
            text-align: left;
        }
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
        .formHeaderl
        {
            height: 25px;
            background-color: #88AB2D;
        }
        .style1
        {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <table style="width: 100%">
            <tr>
                <td class="style1">
                    <uc1:Messages ID="Messages" runat="server" />
                </td>
            </tr>
        </table>
        <div id="receiveSampleCodeForm" class="form" style="width: 50%; padding-top: 10%;
            margin-left: 23%;">
            <div class="formHeader" align="center">
                <asp:Label ID="lblInformation" runat="server" Text="Generate Code"></asp:Label>
            </div>
            <div class="formControlHolders">
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblSampleCode" Text="Sample Code" runat="server"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:Label ID="lblSampleCodeValue" runat="server"></asp:Label>
                    </div>
                </div>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblSampleDate" Text="Sample Date" runat="server"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:Label ID="lblSampleDateValue" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Panel ID="pnlGradingClass" Visible="true" runat="server">
                    <%-- --%>
                    <div class="controlContainer">
                        <div class="leftControl">
                            <asp:Label ID="lblGradingClass" Text="Grading Class" runat="server"></asp:Label>
                        </div>
                        <div class="rightControl">
                            <asp:Label ID="lblClassValue" runat="server"></asp:Label>
                        </div>
                    </div>
                    <%-- --%>
                </asp:Panel>
                <asp:Panel ID="pnlWereda" Visible="false" runat="server">
                    <div class="controlContainer">
                        <div class="leftControl">
                            <asp:Label ID="lblWoreda" Text="Woreda" runat="server"></asp:Label>
                        </div>
                        <div class="rightControl">
                            <asp:Label ID="lblWoredaValue" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblGraderCupper" runat="server" Text="Grader/Cupper :"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:DropDownList ID="drpGrader" runat="server" Width="250px">
                        </asp:DropDownList>
                    </div>
                </div>
                <%-- --%>
                <div class="controlContainer">
                    <div class="leftControl">
                        <asp:Label ID="lblIsSupervisor" runat="server" Text="Is Supervisor?"></asp:Label>
                    </div>
                    <div class="rightControl">
                        <asp:CheckBox ID="chkIsSupervisor" runat="server" />
                    </div>
                </div>
                <%-- --%>
                <center>
                <div class="controlContainer">
                    <div class="rightControl">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#88AB2D" ForeColor="White"
                                        Height="20px" Width="100px" BorderStyle="None" CssClass="style1" OnClick="btnAdd_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnGenerateCode" runat="server" Text="Generate Code" BackColor="#88AB2D"
                                        Width="100px" ForeColor="White" BorderStyle="None" CssClass="style1" OnClick="btnGenerateCode_Click"
                                        Height="20px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNext" runat="server" BackColor="#88AB2D" ForeColor="White" Style="margin-right: 70px;"
                                        Width="100px" BorderStyle="None" Height="20px" Text="Next->>" 
                                        OnClick="btnNext_Click" Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                </center>
            </div>
            <%-- --%>
            <div class="controlContainer">
                <asp:GridView ID="gvGradingBy" runat="server" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" ForeColor="Black" AutoGenerateColumns="False" PageSize="3"
                    CssClass="label" Width="100%" DataKeyNames="UserId" OnSelectedIndexChanged="gvGradingBy_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="userId">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="isSupervisor" HeaderText="isSupervisor" SortExpression="isSupervisor">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:CommandField SelectText="Remove" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#fcbe29" Font-Bold="false" Font-Size="small" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <table bgcolor="white">
        <tr>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td class="style1">
                <fieldset>
                    <legend class="Text" style="color: #424D2D; font-size: medium">
                        <asp:Label ID="lblInboxItemName" runat="server" Text="Generate Code"></asp:Label>
                    </legend>
                    <table bgcoor="white">
                        <tr>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
