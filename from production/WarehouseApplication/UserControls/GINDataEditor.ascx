<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GINDataEditor.ascx.cs" Inherits="WarehouseApplication.UserControls.GINDataEditor" %>
<asp:Panel ID="RenderPanel" runat="server" BorderWidth="1">
    <asp:Table ID="RenderTable" runat="server">
        <asp:TableHeaderRow ID="CaptionRow" runat="server" style="text-align:center">
            <asp:TableCell ColumnSpan="3">
                <asp:Panel ID="CaptionPanel" runat="server">
                    <asp:Label ID="lblCaption" runat="server" OnDataBinding="lblCaption_DataValidating" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow ID="MessageRow" runat="server">
            <asp:TableCell ColumnSpan="3">
                <asp:Panel ID="MessagePanel" runat="server">
                    <asp:ValidationSummary ID="ValidationMessage" runat="server" ValidationGroup="GINDataEditor" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableFooterRow ID="CommandRow" runat="server">
            <asp:TableCell ColumnSpan="3">
                <asp:Panel ID="CommandPanel" runat="server">
                    <asp:Button ID="btnOk" runat="server" Text = "  Ok  " OnClick="btnOk_Click" ValidationGroup="GINDataEditor" />
                    <asp:Button ID="btnCancel" runat="server" Text=" Cancel " OnClick="btnCancel_Click" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    <asp:UpdatePanel ID="TextChangeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="ExtenderPanel" runat="server">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
