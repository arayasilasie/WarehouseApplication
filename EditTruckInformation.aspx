<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditTruckInformation.aspx.cs" Inherits="WarehouseApplication.EditTruckInformation" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .puna
        {
        	width:750px;
        }
        .locStyle
        {
        	width:450px;
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
            <td>
                <uc1:GINDataEditor ID="TruckDataEditor" runat="server" CssClass="Editor locStyle" CaptionClass="EditorCaption" CommandClass="EditorCommand" AlternateRowClass="EditorAlternate" />
            </td>
        </tr>
    </table>
</asp:Content>
