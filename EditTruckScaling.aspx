<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EditTruckScaling.aspx.cs" Inherits="WarehouseApplication.EditTruckScaling" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .puna
    {
    	width:550px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
<ContentTemplate>
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" class="Message"  />
            </td>
        </tr>
        <tr style="vertical-align:top;text-align:left;">
            <td class="Text">
                <uc1:GINDataEditor ID="TruckWeightEditor" runat="server" CssClass="EmbeddedEditor puna" AlternateRowClass="EditorAlternate" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" />
            </td>
        </tr>
                <tr style="vertical-align:top;text-align:left;">
            <td class="Text">
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnAddReturnedBags" runat="server" Text="  Return  " 
                                onclick="btnAddReturnedBags_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:GINGridViewer ID="ReturnedBagsGridViewer" runat="server" CssClass="EmbeddedEditor puna" AlternateRowClass="EditorAlternate" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" />
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Button ID="btnAddAddedBags" runat="server" Text="  Add  " 
                                onclick="btnAddAddedBags_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:GINGridViewer ID="AddedBagsGridViewer" runat="server" CssClass="EmbeddedEditor puna" AlternateRowClass="EditorAlternate" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" />
                        </td>
                    </tr>
               </table>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td colspan="2">
                <asp:Button ID="btnReturnedDummy" runat="server" style="display:none" />
                <asp:Button ID="btnAddedDummy" runat="server" style="display:none" />
                <asp:Button ID="btnCancel" runat="server" style="display:none" />
                <asp:Button ID="btnOk" runat="server" Text="  Ok  " CssClass="Button" 
                    onclick="btnOk_Click" />
                <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel"  
                    CssClass="Button" onclick="btnCancelEdit_Click"/>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="ReturnedBagsDataEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="ReturnedBagsDataEditorContainer" runat="server" style="width:550px">
            <uc1:GINDataEditor ID="ReturnedBagsDataEditor" runat="server" CaptionClass="EditorCaption" 
                CommandClass="EditorCommand" CssClass="Editor puna"  AlternateRowClass="EditorAlternate"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeReturnedBagsDataEditorExtender" runat="server" 
       PopupControlID="ReturnedBagsDataEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnReturnedDummy" 
       CancelControlID="btnCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
    <asp:UpdatePanel ID="AddedBagsDataEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="AddedBagsDataEditorContainer" runat="server" style="width:550px">
            <uc1:GINDataEditor ID="AddedBagsDataEditor" runat="server" CaptionClass="EditorCaption" 
                CommandClass="EditorCommand" CssClass="Editor puna"  AlternateRowClass="EditorAlternate"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeAddedBagsDataEditorExtender" runat="server" 
       PopupControlID="AddedBagsDataEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnAddedDummy" 
       CancelControlID="btnCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
