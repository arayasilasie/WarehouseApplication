<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="CloseInboxDetail.aspx.cs" Inherits="WarehouseApplication.CloseInboxDetail" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .form
        {
            width: 45%;
        }
        .leftControl
{
	margin-left: 10px;
	margin-right: 10px;
	width: 30%;
	float: left;
}
.rightControl
{
	float: left;
	width: 60%;
}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <br />
                    <div>
                        <uc1:Messages ID="Messages1" runat="server" />
                    </div>
                    <div style="margin-left: 90px; margin-bottom: 20;">
                    </div>
                    <div id="Div1" class="form" style="float: left; margin-left: 10pX; margin-top: 10pX;
                        height:auto; margin-left: 25%;">
                        <div class="formHeader" align="center">
                            <asp:Label ID="lblCloseDetail" runat="server" Text="Close Detail"></asp:Label>
                        </div>
                        <div style="border: solid 1px #999933; height: inherit; background-color: White;">
                            <%-- TrackingNo --%>
                            <div class="controlContainer" style="margin-top: 10px;">
                                <div class="leftControl">
                                    <asp:Label ID="Label4" runat="server" Text="Tracking No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblTrackingNo" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%-- Client ID--%>
                            <%--  <div class="controlContainer">
                                <div class="leftControl">
                                    <asp:Label ID="Label2" runat="server" Text="Client ID"></asp:Label>
                                </div>
                                <div class="rightControl">
                                   <asp:Label ID="lblClientID" runat="server"/>
                                </div>
                            </div>--%>
                            <%-- Client   --%>
                            <div class="controlContainer" >
                                <div class="leftControl">
                                    <asp:Label ID="Label09" runat="server" Text="Client Name"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblClient" runat="server" />
                                </div>
                            </div>
                            <%-- Sampling Code  --%>
                            <div id="divSamplingCode"  runat="server" class="controlContainer" visible="false">
                                <div class="leftControl">
                                    <asp:Label ID="Label3" runat="server" Text="Sampling Code"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblSamplingCode" runat="server" />
                                </div>
                            </div>
                            <%-- NewTrackingNo  --%>
                            <div id="divNewTrackingNo"  runat="server" class="controlContainer" visible="false">
                                <div class="leftControl">
                                    <asp:Label ID="Label6" runat="server" Text="New Tracking No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblNewTrackingNo" runat="server" />
                                </div>
                            </div>
                            <%-- GradingCode  --%>
                            <div id="divGradingCode"  runat="server"  class="controlContainer" visible="false">
                                <div class="leftControl">
                                    <asp:Label ID="Label5" runat="server" Text="GradingCode"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblGradingCode" runat="server" />
                                </div>
                            </div>
                            <%-- GrN No  --%>
                            <%--<div class="controlContainer" visible="false">
                                <div class="leftControl">
                                    <asp:Label ID="Label7" runat="server" Text="GRN No."></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:Label ID="lblGRNNo" runat="server" />
                                </div>
                            </div>--%>
                            <%-- Reason  --%>
                            <div class="controlContainer" style="height: 50px;">
                                <div class="leftControl">
                                    <asp:Label ID="Label8" runat="server" Text="Reason"></asp:Label>
                                </div>
                                <div class="rightControl">
                                    <asp:TextBox Width="170px" ID="txtReason" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtReason" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <%--   --%>
                            <div class="controlContainer" style="margin-top: 20px;" align="center">
                                <asp:Button ID="btnClose" runat="server" BackColor="#88AB2D" BorderStyle="None" ForeColor="White"
                                    Text="Close" Width="70px" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:Button ID="btnNext" runat="server" BackColor="#88AB2D" 
                                    BorderStyle="None" ForeColor="White" Visible="false"
                                    Text="Next->>" Width="70px" OnClick="btnNext_Click" />
                                <asp:ModalPopupExtender ID="btnClose_ModalPopupExtender" runat="server" DynamicServicePath=""
                                    Enabled="True" TargetControlID="btnClose" PopupControlID="pnlConfirmation" OkControlID="btnYes"
                                    CancelControlID="btnNo">
                                </asp:ModalPopupExtender>
                                <asp:ConfirmButtonExtender ID="LinkButton1_ConfirmButtonExtender" runat="server"
                                    ConfirmText="" Enabled="True" TargetControlID="btnClose" DisplayModalPopupID="btnClose_ModalPopupExtender">
                                </asp:ConfirmButtonExtender>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none; width: 300px;
                            background-color: White; border-width: 2px; border-color: #A5CBB0; border-style: solid;"
                            PopupControlID="pnlConfirmation">
                            <div class="formHeader">
                                <asp:Label ID="Label10" runat="server" Text="" Style="font-size: medium; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                            </div>
                            <div style="margin: 20px 20px;">
                                <asp:Label ID="configmMessage" runat="server" Text="Are you sure , You want to close"></asp:Label>
                            </div>
                            <div>
                                <div class="controlContainer" style="margin: 20px 20px;">
                                    <div style="width: 30%; float: left">
                                        <asp:Button ID="btnYes" runat="server" Text="Yes" Width="60px" />
                                    </div>
                                    <div style="float: left">
                                        <asp:Button ID="btnNo" runat="server" Text="No" Width="60px" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
