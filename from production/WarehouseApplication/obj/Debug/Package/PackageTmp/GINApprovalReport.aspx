<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="GINApprovalReport.aspx.cs" Inherits="WarehouseApplication.GINApprovalReport" %>
<%@ Register src="Messages.ascx" tagname="Messages" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style1
    {
        width: 906px;
    }
    .style2
    {
        width: 145px;
    }
    .style3
    {
        width: 751px;
    }
.messages-logo {height:32px; width:32px; float:left; background:url(Images/message_logos.png)}
.messages-text {margin-left:40px; padding:6px 0}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <table width="800px"  bgcolor="white" class="style1">
                                <tr>
                                 
                                    <td colspan="2">
                                     
                                        <uc1:Messages ID="Messages" runat="server" />
                                     
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td class="style2">
                                        <asp:DropDownList ID="drpLIC" runat="server" Width="145px" >
                                        </asp:DropDownList>
                                    </td>
                                   
                                    <td class="style3">
                                        <asp:Button ID="btnPrint" runat="server" onclick="btnApproval_Click" 
                                            Text="Print" />
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <hr style="width: 900px;" />
                                    </td>
                                </tr>
                            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    </asp:UpdatePanel>
    </asp:Content>
