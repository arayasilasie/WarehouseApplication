<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="TakePhysicalCount.aspx.cs" Inherits="WarehouseApplication.TakePhysicalCount" %>
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
                <uc1:GINDataEditor ID="PhysicalCountEditor" runat="server" CssClass="EmbeddedEditor pun" CommandClass="EmbeddedEditorCommand" CaptionClass="EmbeddedEditorCaption" AlternateRowClass="EditorAlternate" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr valign="top" align="left">
            <td>
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnAddInspector" runat="server" Text=" Add Inventory Counter " 
                                onclick="btnAddInspector_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:GINGridViewer ID="InspectorGridViewer" runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td>
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnAddStackCount" runat="server" Text=" Enter Bag Count " 
                                onclick="btnAddStackCount_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="stackCountDataSource" 
                              Runat="server" TypeName="WarehouseApplication.StackCountSource" 
                              SortParameterName="SortExpression"
                                SelectMethod="GetStackCounts" EnablePaging="True" 
                                SelectCountMethod="TotalNumberOfStacks">
                            </asp:ObjectDataSource>

                            <asp:GridView ID="StackGrid" runat="server"
                                AutoGenerateColumns="False"
                                Width="100%"
                                CssClass="Grid"
                                HeaderStyle-CssClass="GridHeader"
                                RowStyle-CssClass="GridRow"
                                AlternatingRowStyle-CssClass="GridAlternate"
                                PagerStyle-CssClass="GridPager"
                                DataSourceID="stackCountDataSource" 
                                AllowSorting="True" AllowPaging="True">
                                <RowStyle CssClass="GridRow"></RowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" 
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' CommandName="EditStackCount" 
                                                Text="Edit" onclick="lnkEdit_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShedNo" runat="server" 
                                                Text='<%# GetShedNo(DataBinder.Eval(Container.DataItem,"ShedId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stack No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStackNo" runat="server" 
                                                Text='<%# GetStackNo(DataBinder.Eval(Container.DataItem,"StackId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Physical Count" 
                                      DataField="Balance">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Count (System)" 
                                      DataField="ExpectedBalance">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cumulated Shortage" 
                                      DataField="CummulatedShortage">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cumulated Overage" 
                                      DataField="CumulatedOverage">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>

                                <PagerStyle CssClass="GridPager"></PagerStyle>

                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>

                                <AlternatingRowStyle CssClass="GridAlternate"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top" align="left">
            <td>
                <asp:Button ID="btnDummyInspector" runat="server" style="display:none" />
                <asp:Button ID="btnInspectorCancel" runat="server" style="display:none" />
                <asp:Button ID="btnDummyStackCount" runat="server" style="display:none" />
                <asp:Button ID="btnStackCountCancel" runat="server" style="display:none" />
                <asp:Button ID="btnSave" runat="server" Text="  Save  " CssClass="Button" 
                    onclick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel "  
                    CssClass="Button" onclick="btnCancel_Click"/>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="InspectorDataEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="InspectorDataEditorContainer" runat="server" style="width:550px">
                <div id="InspectorErrorMessage" runat="server" class="Message" />
                <uc1:GINDataEditor ID="InspectorDataEditor" runat="server" CaptionClass="EditorCaption" 
                    CommandClass="EditorCommand" CssClass="Editor popup"  AlternateRowClass="EditorAlternate"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeInspectorDataEditorExtender" runat="server" 
       PopupControlID="InspectorDataEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnDummyInspector" 
       CancelControlID="btnInspectorCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
    <asp:UpdatePanel ID="StackCountDataEditorUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="IStackCountDataEditorContainer" runat="server" style="width:550px">
                <div id="stackCountErrorMessage" runat="server" class="Message" />
                <uc1:GINDataEditor ID="StackCountDataEditor" runat="server" CaptionClass="EditorCaption" 
                    CommandClass="EditorCommand" CssClass="Editor popup"  AlternateRowClass="EditorAlternate"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ajax:ModalPopupExtender ID="mpeStackCountDataEditorExtender" runat="server" 
       PopupControlID="StackCountDataEditorUpdatePanel"
       DropShadow="True" 
       TargetControlID="btnDummyStackCount" 
       CancelControlID="btnStackCountCancel" 
       BackgroundCssClass="ModalBackgroundStyle" />
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
