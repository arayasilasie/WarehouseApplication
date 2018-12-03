<%@ Page Language="C#" Trace="true" EnableEventValidation="false" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="TruckScaling.aspx.cs" Inherits="WarehouseApplication.TruckScaling" Title="Untitled Page" %>
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
            <td >
                <uc1:GINDataEditor ID="GINDataEditor" runat="server" CssClass="PreviewEditor puna"  AlternateRowClass="EditorAlternate" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" />
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
                            Net Weight (Kg) :
                            <asp:Label ID="lblNetWeight" runat="server" />&nbsp;
                            <asp:Button ID="btnRecalculate" runat="server" Text=" Calculate " 
                                onclick="btnRecalculate_Click" />
                        </td>
                    </tr>
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
        <tr style="vertical-align:top;text-align:left;">
            <td>
                <table style="width:100%">
                    <tr>
                        <td>
                            <uc1:GINDataEditor ID="GINIssuanceEditor" runat="server" CssClass="EmbeddedEditor puna" AlternateRowClass="EditorAlternate" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" />
                        </td>
                    </tr>
                    <tr valign="top" align="left">
                        <td colspan="2">
                            <asp:Button ID="btnReturnedDummy" runat="server" style="display:none" />
                            <asp:Button ID="btnAddedDummy" runat="server" style="display:none" />
                            <asp:Button ID="btnCancel" runat="server" style="display:none" />
                            <asp:Button ID="btnSave" runat="server" Text="  Save  " CssClass="Button" 
                                onclick="btnSave_Click" />
                            <asp:Button ID="btnConfirm" runat="server" Text="Generate GIN"  
                                CssClass="Button" onclick="btnConfirm_Click"/>
                        </td>
                    </tr>
                </table>
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
    <asp:UpdatePanel ID="SuspeciousTruckWarningUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width:350px;background-color:White">
                <tr>
                    <td class="EditorCaption">Suspecious Truck Weight</td>
                </tr>
                <tr>
                    <td class="Message">The truck weight you specified (<span id="currentTruckWeight" runat="server"/>Kg)
                    was found to be different from the corresponding last registred weight (<span id="lastTruckWeight" runat="server"/>Kg).
                    Are you sure you want to save this?</td>
                </tr>
                <tr>
                    <td class="EditorCommand">
                        <asp:Button ID="btnTruckWeightOk" runat="server" Text="  Yes  " 
                            onclick="btnTruckWeightOk_Click" />&nbsp;
                        <asp:Button ID="btnTruckWeightNotOk" runat="server" Text="  No  " 
                            onclick="btnTruckWeightNotOk_Click" />
                        </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeSuspeciousTruckWarningExtender" runat="server" 
       PopupControlID="SuspeciousTruckWarningUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnAddedDummy"
       CancelControlID="btnTruckWeightNotOk" 
       PopupDragHandleControlID="SuspeciousTruckWarningUpdatePanel"
       BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
