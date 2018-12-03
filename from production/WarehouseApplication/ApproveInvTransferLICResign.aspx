<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ApproveInvTransferLICResign.aspx.cs" Inherits="WarehouseApplication.ApproveInvTransferLICResign" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .label
        {
            display: block;
            font-size: small;
            font-family: 'Verdana';
            color: #000000;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function toggle(toggeldivid, toggeltext) {
            var divelement = document.getElementById(toggeldivid);
            var lbltext = document.getElementById(toggeltext);
            if (divelement.style.display == "block") {
                divelement.style.display = "none";
                lbltext.innerHTML = "+ Show Responses";
            }
            else {
                divelement.style.display = "block";
                lbltext.innerHTML = "- Hide Responses";

            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <uc1:Messages ID="Messages1" runat="server" />
                </div>
                <br />
                <div class="formHeader" id="Header" style="width: 50%; margin-left: 10px; margin-top: 10px;margin-left:22%;"
                    align="center" >
                    <asp:Label ID="lblDetail" Text=" INVENTORY TRANSFER APPROVAL WHEN LIC RESIGN" runat="server"></asp:Label>
                </div>
                <div style="width: 50%; margin-left: 10px; margin-top: 10px; margin-left:22%;"">
                    <asp:GridView ID="grvInvTransferApproval" runat="server" AutoGenerateColumns="False"
                        BorderColor="White" CellPadding="4" DataKeyNames="ID" Style="font-size: small"
                        CssClass="label" CellSpacing="1" GridLines="None" OnRowDataBound="grvInvTransferApproval_RowDataBound"
                        OnSelectedIndexChanged="grvInvTransferApproval_SelectedIndexChanged">
                        <EmptyDataTemplate>
                            <asp:Label ID="lbl" runat="server" Text="No inventory transfer to approve." /></EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC From" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC" runat="server" Text='<%# Bind("LIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LIC To" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLIC2" runat="server" Text='<%# Bind("LIC2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TransitionDate" HeaderStyle-Font-Bold="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTransitionDate" runat="server" Text='<%# Bind("TransitionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Approve" ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#DAE1CC" />
                        <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                            BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" />
                        <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#88AB2D" ForeColor="#CCFFCC" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
