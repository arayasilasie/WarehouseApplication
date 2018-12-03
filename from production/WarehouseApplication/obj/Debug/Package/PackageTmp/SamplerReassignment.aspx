<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="SamplerReassignment.aspx.cs" Inherits="WarehouseApplication.SamplerReassignment" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
       <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc1:Messages ID="Messages1" runat="server" />
                <%--<asp:Panel ID="pnlMessage" runat="server" BackColor="#FBFBE2">
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Agency FB" Font-Size="14pt"
                        Style="font-family: Verdana; font-size: small;"></asp:Label>
                </asp:Panel>--%>
                <div id="Div1" class="form" style="float: left; margin-left: 10pX; margin-top: 10pX;
                    height: 210px; margin-left: 25%;">
                    <div class="formHeader" align="center">
                        <asp:Label ID="Label1" runat="server" Text="SAMPLER REASSIGNMENT"></asp:Label>
                    </div>
                    <div style="border: solid 1px #999933; height: inherit; background-color: White;">
                        <%-- SAMPLE CODE --%>
                        <div class="controlContainer" style="margin-top: 10px;">
                            <div class="leftControl">
                                <asp:Label ID="Label4" runat="server" Text="Sample Code"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="ddlSampleCode" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                    Width="175px" OnSelectedIndexChanged="ddlSampleCode_SelectedIndexChanged">
                                    <asp:ListItem Text="Select Sample Code" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSampleCode"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%-- OLD SAMPLER  --%>
                        <div class="controlContainer" style="margin-top: 5px;">
                            <div class="leftControl">
                                <asp:Label ID="Label2" runat="server" Text="Old Sampler"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:Label ID="lblSampler" runat="server" Width="175px" />
                            </div>
                        </div>
                        <%-- NEW SAMPLER --%>
                        <div class="controlContainer" style="margin-top: 5px;">
                            <div class="leftControl">
                                <asp:Label ID="Label3" runat="server" Text="New Sampler"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:DropDownList ID="ddlNewSampler" runat="server" AppendDataBoundItems="True" Width="175px">
                                    <asp:ListItem Text="Select New Sampler" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlNewSampler"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%-- REASON  --%>
                        <div class="controlContainer" style="height: 50px;">
                            <div class="leftControl">
                                <asp:Label ID="Label5" runat="server" Text="Reason"></asp:Label>
                            </div>
                            <div class="rightControl">
                                <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server" Width="168px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReason"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <%--   --%>
                        <div class="controlContainer" style="margin-top: 10px;" align="center">
                            <asp:Button ID="btnUpdate" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                ForeColor="White" OnClick="btnUpdate_Click" Text="Update" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
