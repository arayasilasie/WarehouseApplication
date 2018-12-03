<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarToggler.ascx.cs" Inherits="WarehouseApplication.UserControls.CalendarToggler" %>
<link href="../warehouse.css" type="text/css" />
<asp:Panel ID="panShortsand" runat="server">
    <table>
        <tr>
            <td class="Text">
                <asp:Label ID="lblShorthand" runat= "server" />
            </td>
            <td class="Input">
                <asp:Button ID="btnSet" Text="Set" runat="server" onclick="btnSet_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<div style="position: relative; top: 0px"  class="Input"><asp:Calendar ID="calExpanded" runat="server" 
                    onselectionchanged="calExpanded_SelectionChanged"/></div>