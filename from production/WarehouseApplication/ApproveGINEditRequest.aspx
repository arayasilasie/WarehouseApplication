<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ApproveGINEditRequest.aspx.cs" Inherits="WarehouseApplication.ApproveGINEditRequest" %>
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
    <table>
        <tr>
            <td >
                <asp:Label ID="lblMessage" runat="server" class="Message"  />
            </td>
        </tr>
        <tr>
            <td>Original</td>
        </tr>
        <tr>
            <td>
                <ajax:TabContainer ID="tcCurrentState" runat="server" ActiveTabIndex="2" 
                    Width="800px">
                    <ajax:TabPanel ID="tpDriverInfo" runat="server" HeaderText="Driver Information">
                        <ContentTemplate>
                            <uc1:GINDataEditor ID="TruckDataEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate" />
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="tpLoadingInfo" runat="server" HeaderText="Loading Information">
                        <ContentTemplate>
                            <table class="Text" style="width:100%">
                                <tr>
                                    <td>
                                        <uc1:GINDataEditor ID="TruckLoadEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc2:GINGridViewer ID="StackGridViewer" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="tpWeighingInfo" runat="server" HeaderText="Weighing Information" >
                        <ContentTemplate>
                            <table class="Text" style="width:100%">
                                <tr>
                                    <td>
                                        <uc1:GINDataEditor ID="TruckWeightEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc2:GINGridViewer ID="ReturnedBagsGridViewer" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
            </td>
        </tr>
        <tr>
            <td>Proposed</td>
        </tr>
        <tr>
            <td>
                <ajax:TabContainer ID="tcProposedState" runat="server" ActiveTabIndex="2" 
                    Width="800px">
                    <ajax:TabPanel ID="tpProposedDriverInfo" runat="server" HeaderText="Driver Information">
                        <ContentTemplate>
                            <uc1:GINDataEditor ID="ProposedTruckDataEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate" />
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="tpProposedLoadingInfo" runat="server" HeaderText="Loading Information">
                        <ContentTemplate>
                            <table class="Text" style="width:100%">
                                <tr>
                                    <td>
                                        <uc1:GINDataEditor ID="ProposedTruckLoadEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc2:GINGridViewer ID="ProposedStackGridViewer" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajax:TabPanel>
                    <ajax:TabPanel ID="tpProposedWeighingInfo" runat="server" HeaderText="Weighing Information" >
                        <ContentTemplate>
                            <table class="Text" style="width:100%">
                                <tr>
                                    <td>
                                        <uc1:GINDataEditor ID="ProposedTruckWeightEditor" runat="server" CssClass="PreviewEditor pun" CommandClass="PreviewEditorCommand" CaptionClass="PreviewEditorCaption" AlternateRowClass="EditorAlternate"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc2:GINGridViewer ID="ProposedReturnedBagsGridViewer" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajax:TabPanel>
                </ajax:TabContainer>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                    onclick="btnApprove_Click" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" 
                    onclick="btnReject_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
