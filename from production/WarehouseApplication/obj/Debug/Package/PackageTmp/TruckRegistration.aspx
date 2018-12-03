<%@ Page Language="C#" MasterPageFile="~/pTop.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="TruckRegistration.aspx.cs" Inherits="WarehouseApplication.TruckRegistration" Title="Untitled Page" %>
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
        	width:100%;
        }
        .popupStyle
        {
        	background-color:White;
        	width:618
        }
        .groupPopupStyleCaption
        {
        	border:none;
        	background-color:Transparent
        }
        .groupPopupStyleCommand
        {
        	display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<table style="width:700px">
    <tr>
        <td align="left">
            <asp:Label ID="lblMessage" runat="server" class="Message"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align:left; vertical-align:top">
                <uc1:GINDataEditor ID="PUNADataEditor" runat="server" CssClass="PreviewEditor puna" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate" />
        </td>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
             <tr>
                <td>&nbsp;</td>
            </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAddTruck" runat="server" Text="  Add  " 
                            onclick="btnAddTruck_Click" />&nbsp;
                        </td>
                </tr>
               <tr>
                    <td class="EmbeddedEditor EmbeddedEditorCaption">Trucks Queued</td>
                </tr>
                <tr>
                    <td>
                        <uc2:GINGridViewer ID="TruckGridViewer" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr valign="top" align="left">
        <td>
            <asp:Button ID="btnDummy" runat="server" style="display:none" />
            <asp:Button ID="btnCancel" runat="server" style="display:none" />
            <asp:Button ID="btnSave" runat="server" Text="   Ok   " CssClass="Button" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnConfirm" runat="server" Text="Cancel"  
                CssClass="Button" onclick="btnConfirm_Click"/>
        </td>
    </tr>
</table>
    <asp:UpdatePanel ID="TruckEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="popupStyle">
            <tr>
                <td colspan="2" class="EditorCommand">
                    Truck Registration
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="TruckRegistrationValidationSummary" runat="server"  ValidationGroup="TruckRegistration"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:GINDataEditor ID="DriverDataEditor" runat="server" CssClass="Editor locStyle" CaptionClass="groupPopupStyleCaption" CommandClass="EmbeddedEditorCommand" AlternateRowClass="EditorAlternate" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:GINDataEditor ID="TruckDataEditor" runat="server" CssClass="Editor" CaptionClass="groupPopupStyleCaption" CommandClass="EmbeddedEditorCommand" AlternateRowClass="EditorAlternate" />
                </td>
                <td>
                    <uc1:GINDataEditor ID="TrailerDataEditor" runat="server" CssClass="Editor" CaptionClass="groupPopupStyleCaption" CommandClass="EmbeddedEditorCommand" AlternateRowClass="EditorAlternate" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="EditorCommand">
                    <asp:Button ID="btnTruckRegistrationOk" runat="server" Text="  Ok  "  ValidationGroup="TruckRegistration"
                        onclick="TruckDataEditor_Ok" Width="49px" />&nbsp;
                    <asp:Button ID="btnTruckRegistrationCancel" runat="server" Text=" Cancel " 
                        onclick="TruckDataEditor_Cancel" />
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeTruckDataEditorExtender" runat="server" 
           PopupControlID="TruckEditorUpdatePanel"
           PopupDragHandleControlID="TruckEditorUpdatePanel"
           DropShadow="True" 
           TargetControlID="btnDummy" 
           CancelControlID="btnCancel" 
           BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
