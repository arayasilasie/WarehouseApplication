<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="TruckLoading.aspx.cs" Inherits="WarehouseApplication.TruckLoading" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc1" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .pun
    {
    	width:500px;
    }
    .popup
    {
    	width:550px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
<ContentTemplate>
    <table class="Text" style="width:800px">
        <tr>
            <td >
                <asp:Label ID="lblMessage" runat="server" class="Message"  />
            </td>
        </tr>
        <tr align="left" style="vertical-align:top">
            <td style="width:40%">
                <uc1:GINDataEditor ID="TruckDataEditor" runat="server" CssClass="EmbeddedEditor pun" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" AlternateRowClass="EditorAlternate" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr align="left" style="vertical-align:top">
            <td>
                <uc1:GINDataEditor ID="TruckLoadEditor" runat="server" CssClass="EmbeddedEditor pun" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" AlternateRowClass="EditorAlternate"  />
            </td>
        </tr>
        <tr valign="top" align="left">
            <td>
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnAddStack" runat="server" Text="  Load  " 
                                onclick="btnAddStack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:GINGridViewer ID="StackGridViewer" runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td>
                <asp:Button ID="btnDummyStack" runat="server" style="display:none" />
                <asp:Button ID="btnDummyLoader" runat="server" style="display:none" />
                <asp:Button ID="btnCancel" runat="server" style="display:none" />
                <asp:Button ID="btnSave" runat="server" Text="  Save  " CssClass="Button" 
                    onclick="btnSave_Click" />
                <asp:Button ID="btnconfirm" runat="server" Text=" Confirm "  
                    CssClass="Button" onclick="btnConfirm_Click"/>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="StackDataEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="StackDataEditorContainer" runat="server" style="width:550px">
            <uc1:GINDataEditor ID="StackDataEditor" runat="server" CaptionClass="EditorCaption" 
                CommandClass="EditorCommand" CssClass="Editor popup"  AlternateRowClass="EditorAlternate"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeStackDataEditorExtender" runat="server" 
       PopupControlID="StackDataEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnDummyStack" 
       CancelControlID="btnCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
