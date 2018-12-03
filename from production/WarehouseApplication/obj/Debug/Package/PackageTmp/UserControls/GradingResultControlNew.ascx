<%@ Control Language="C#" CodeBehind="GradingResultControlNew.ascx.cs" 
    Inherits="WarehouseApplication.UserControls.GradingResultControlNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    function DrpChange(drp, gr) {
        var cgv = document.getElementById(gr);
        var cdrp = document.getElementById(drp);
        cdrp.value = cgv.value;
    }   
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:DropDownList ID="drpGradeResult" runat="server" Height="16px" Width="93px" >
        </asp:DropDownList>
        <asp:TextBox ID="txtdrpGradeResult" runat="server" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="txtGradeResult" runat="server" onchange="DrpChange(this);"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvdrpGradeResult" runat="server" ErrorMessage="Please Select one value"
            ControlToValidate="drpGradeResult" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcerfvdrpGradeResult" runat="server" Enabled="True"
                                TargetControlID="rfvdrpGradeResult">
                            </cc1:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="rfvtxtGradeResult" runat="server" ErrorMessage="required"
            ControlToValidate="txtGradeResult" ValidationGroup="save">*</asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcerfvtxtGradeResult" runat="server" Enabled="True"
                                TargetControlID="rfvtxtGradeResult">
                            </cc1:ValidatorCalloutExtender>
        <asp:RangeValidator ID="rangeevtxtGradeResult" runat="server" MaximumValue="" MinimumValue=""
        ErrorMessage="" ControlToValidate="txtGradeResult" ValidationGroup="save">*
        </asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vcerangeevtxtGradeResult" runat="server" Enabled="True"
                                TargetControlID="rangeevtxtGradeResult">
                            </cc1:ValidatorCalloutExtender>
        <asp:CheckBox ID="chkGradeResult" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
