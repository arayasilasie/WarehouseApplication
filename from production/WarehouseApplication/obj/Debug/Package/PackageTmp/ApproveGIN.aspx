<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ApproveGIN.aspx.cs" Inherits="WarehouseApplication.ApproveGIN" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc2" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" >
        .puna
        {
        	width:500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="800px" class="Text">
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" class="Message"  />
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr valign="top">
        <td>
            <uc2:GINDataEditor ID="GINDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="PreviewEditor puna" AlternateRowClass="EditorAlternate" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm"  
                CssClass="Button" onclick="btnConfirm_Click"/>
        </td>
    </tr>
    </table>
</asp:Content>
