<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GINProcess.aspx.cs" Inherits="WarehouseApplication.GINProcess" Title="Untitled Page" %>
<%@ Register src="UserControls/GINDataEditor.ascx" tagname="GINDataEditor" tagprefix="uc2" %>
<%@ Register src="UserControls/GINGridViewer.ascx" tagname="GINGridViewer" tagprefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="warehouse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
<ContentTemplate>
    <table width="700px" class="Text">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMessage" runat="server" class="Message"  />
        </td>
    </tr>
    <tr>
        <td>
            <div style="width:300px">
            <uc2:GINDataEditor ID="GINDataEditor1" runat="server" CaptionClass="PreviewEditorCaption" 
                    CommandClass="PreviewEditorCommand" CssClass="PreviewEditor" />
            </div>
            <br />
            <div style="width:700px">
                <table>
                    <tr>
                        <td>
                             <uc3:GINGridViewer ID="GINGridViewer1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnAddTruck" runat="server" onclick="btnAddTruck_Click" 
                                Text="Add Truck" /></td>
                    </tr>
                </table>
             
            </div>
            <br />
            <asp:Button ID="btnDummy" runat="server" style="display:none" />
            <asp:Button ID="btnCancel" runat="server" style="display:none" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnCompleteLoading" runat="server" Text="Loading Completed"  
                CssClass="Button" onclick="btnCompleteLoading_Click"/>
        </td>
    </tr>
    </table>
    <asp:UpdatePanel ID="TruckEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="TruckDataEditorContainer" runat="server" style="width:400px">
            <uc2:GINDataEditor ID="GINDataEditor2" runat="server" CaptionClass="EditorCaption" 
                CommandClass="EditorCommand" CssClass="Editor" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeTruckDataEditorExtender" runat="server" 
       PopupControlID="TruckEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnDummy" 
       CancelControlID="btnCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
    </asp:Content>
