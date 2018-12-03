<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="SampleGIN.aspx.cs" Inherits="WarehouseApplication.SampleGIN" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table class="Text" border="2" cellpadding="2" cellspacing="2" width="700px">
    <tr valign="top">
        <td style="width:40%" rowspan="2">
            <uc1:GINDataEditor ID="GINProcessDataEditor" runat="server" CssClass="EmbeddedEditor" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" AlternateRowClass="GridAlternate" />
        </td>
        <td style="width:60%">
            <table>
                <tr>
                    <td>
                        <uc2:GINGridViewer ID="SampleGridViewer" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAddSample" runat="server" Text="Add Sample" 
                            onclick="btnAddSample_Click" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:1px">
            <div id="SampleDataEditorContainer" class="HidePopupEditor" runat="server">
                <uc1:GINDataEditor ID="SampleDataEditor" runat="server" CssClass="Editor" CaptionClass="EditorCaption" CommandClass="EditorCommand" AlternateRowClass="GridAlternate" />
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
                 <asp:Button ID="btnOk" runat="server" Text="   Ok   " CssClass="Button" 
                    onclick="btnOk_Click" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel "  
                    CssClass="Button" onclick="btnCancel_Click"/>
       </td>
    </tr>
</table>
</asp:Content>
