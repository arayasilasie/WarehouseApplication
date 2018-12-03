<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="EditSamplerAttendance.aspx.cs" Inherits="WarehouseApplication.EditSamplerAttendance" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
        }
    </style>

    <script language="javascript" type="text/javascript">

        function SelectAll(Id) {

            var myform = document.forms[0];

            var len = myform.elements.length;

            var IsChecked = document.getElementById(Id).checked;

            document.getElementById(Id).checked == true ? document.getElementById(Id).checked = false : document.getElementById(Id).checked = true;

            for (var i = 0; i < len; i++) {

                if (myform.elements[i].type == 'checkbox') {
                    myform.elements[i].checked = IsChecked;

                }
            }
        }


        function Selectchildcheckboxes(Id) {
            var myform = document.forms[0];
            var len = myform.elements.length;
            var count = 0;
            var len2 = 0;
            for (var i = 0; i < len; i++) {

                if (myform.elements[i].type == 'checkbox') {

                    if (myform.elements[i].checked) {
                        if (myform.elements[i].id != 'ctl00_ContentPlaceApp_grvSamplersAttenendance_ctl01_chkSelectAll') {
                            count++;
                        }
                    }
                    len2++;
                }
            }
            if (count == len2 - 1) {
                document.getElementById(Id).checked = true;
            }

            else {
                document.getElementById(Id).checked = false;
            }

        }
   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
               <br />
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <div class="formHeader" align="center" style="width: 50%; margin: 10px;">
                    <asp:Label ID="lblHeader" runat="server" Text="EDIT SAMPLERS DAILY ATTENDANCE"></asp:Label>
                </div>
                <div style="margin-top: 10px; margin-left: 10px; width: 55%;">
                    <asp:GridView ID="grvSamplersAttenendance" runat="server" AutoGenerateColumns="False"
                        Width="91%" BorderColor="White" CellPadding="4" DataKeyNames="ID" Style="font-size: small"
                        CssClass="label" CellSpacing="1" GridLines="None" OnSelectedIndexChanged="grvSamplersAttenendance_SelectedIndexChanged"
                        OnRowDataBound="grvSamplersAttenendance_RowDataBound" OnRowCreated="grvSamplersAttenendance_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperatorType" runat="server" Text='<%# Bind("OperatorType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this.id)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chbIsAvailable" runat="server" Checked='<%#Bind("Status") %>' onclick="Selectchildcheckboxes('ctl00_ContentPlaceApp_grvSamplersAttenendance_ctl01_chkSelectAll')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReason" runat="server" Width="170px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Update" ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#DAE1CC" />
                        <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                            BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <br />
                </div>
                <div style="margin-left: 10px; margin-top: 10px;">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="#88AB2D" BorderStyle="None"
                        CssClass="style1" ForeColor="White" OnClick="btnAdd_Click" Width="63px" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
