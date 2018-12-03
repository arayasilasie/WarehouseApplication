<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Messages.ascx.cs" Inherits="WarehouseApplication.Messages" %>
    <asp:Panel ID="pnlMessage" Runat="server" Visible="False">
        <div id="divMessagesLogo" runat="server" class="messages-logo"></div>
        <div class="messages-text"><asp:Literal id="litMessage" runat="server"></asp:Literal></div>
    </asp:Panel>
    <asp:Literal ID="litSpace" Runat="server"></asp:Literal>