<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Location.ascx.cs" Inherits="WarehouseApplication.UserControls.Location" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="cboRegion" runat="server">
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="DropDownList1_CascadingDropDown" runat="server" 
            Enabled="True" TargetControlID="cboRegion" 
            ParentControlID="Loading region" PromptText="Please Select Region" 
            ServiceMethod="GetActiveRegions" 
            ServicePath="http://10.1.10.30:5000/ecxlookup/ecxlookup.asmx">
        </cc1:CascadingDropDown>
        <br />
        <asp:DropDownList ID="cboZone" runat="server">
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="DropDownList2_CascadingDropDown" runat="server" 
            Enabled="True" TargetControlID="cboZone" LoadingText="[Loading Zones...]" 
            ParentControlID="cboRegion" PromptText="Please Select Zone" 
            ServiceMethod="GetActiveZones" 
            ServicePath="http://10.1.10.30:5000/ecxlookup/ecxlookup.asmx">
        </cc1:CascadingDropDown>
        <br />
        <asp:DropDownList ID="cboWoreda" runat="server" 
            onselectedindexchanged="DropDownList3_SelectedIndexChanged">
        </asp:DropDownList>
        <cc1:CascadingDropDown ID="DropDownList3_CascadingDropDown" runat="server" 
            Enabled="True" TargetControlID="cboWoreda" LoadingText="Loading Zone" 
            ParentControlID="cboZone" PromptText="Please Select Woreda" 
            ServiceMethod="GetActiveWoredas">
        </cc1:CascadingDropDown>
        <br />
    </ContentTemplate>
</asp:UpdatePanel>
