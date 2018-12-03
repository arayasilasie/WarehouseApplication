<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="PickupNoticeAcknowledged.aspx.cs" Inherits="WarehouseApplication.PickupNoticeAcknowledged" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .puna
        {
        	width:600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<table style="width:700px">
    <tr>
        <td align="left">
            <asp:Label ID="lblMessage" runat="server" class="Message" Text="Hello!"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align:left">
                <uc1:GINDataEditor ID="PUNADataEditor" runat="server" CaptionClass="EditorCaption" 
                CommandClass="EditorCommand" CssClass="Editor puna" AlternateRowClass="EditorAlternate" />
        </td>
    </tr>
</table>
</asp:Content>
