<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="TruckLeftCompound.aspx.cs" Inherits="WarehouseApplication.TruckLeftCompound" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .puna
    {
    	width:600px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" class="Message"  />
            </td>
        </tr>
        <tr style="vertical-align:top;text-align:left;">
            <td >
                <uc1:GINDataEditor ID="GINDataEditor" runat="server" CssClass="Editor puna"  AlternateRowClass="EditorAlternate" CommandClass="EditorCommand" CaptionClass="EditorCaption" />
            </td>
        </tr>
    </table>
</asp:Content>
