<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GenerateGIN.aspx.cs" Inherits="WarehouseApplication.GenerateGIN" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc2" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table width="800px" class="Text">
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMessage" runat="server" class="Message"  />
        </td>
    </tr>
    <tr valign="top">
        <td>
            <div style="width:250px">
                <uc2:GINDataEditor ID="GeneralDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="Editor" />
            </div>
        </td>
        <td>
            <div style="width:250px">
                <uc2:GINDataEditor ID="GINDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="PreviewEditor" />
            </div>
            <div style="width:250px">
                <uc2:GINDataEditor ID="ProcessDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="PreviewEditor" />
            </div>
        </td>
        <td>
            <div style="width:250px">
                <uc2:GINDataEditor ID="CommodityDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="PreviewEditor" />
            </div>
            <div style="width:250px">
                <uc2:GINDataEditor ID="TransportDataEditor" runat="server" CaptionClass="PreviewEditorCaption" 
                        CommandClass="PreviewEditorCommand" CssClass="PreviewEditor" />
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnGenerate" runat="server" Text="Generate"  
                CssClass="Button" onclick="btnGenerate_Click"/>
        </td>
    </tr>
    </table>
</asp:Content>
