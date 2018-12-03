<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="VerifyGINAvailability.aspx.cs" Inherits="WarehouseApplication.VerifyGINAvailability" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .puna
        {
        	width:700px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<table style="width:700px">
    <tr>
        <td align="left">
            <asp:Label ID="lblMessage" runat="server" class="Message"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align:left; vertical-align:top">
                <uc1:GINDataEditor ID="PUNADataEditor" runat="server" CssClass="EmbeddedEditor puna" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption"  AlternateRowClass="EditorAlternate" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnOk" runat="server" Text="   Ok   " 
                onclick="btnConfirm_Click" />
            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " 
                onclick="btnCancel_Click" />
            <asp:Button ID="btnAbort" runat="server" Text="Abort PUN" 
                onclick="btnAbort_Click" />
            
        </td>
    </tr>
</table>
</asp:Content>
