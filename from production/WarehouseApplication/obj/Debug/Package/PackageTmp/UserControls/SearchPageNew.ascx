<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPageNew.ascx.cs" Inherits="WarehouseApplication.UserControls.SearchPageNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<link href="../Styles/ControlsView.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.controlContainer
{
    margin-top:5px;
}
#searchResultsetGrid
{
    width:100%;
}
#topPane
{
    height:262px;
}
th
{
    text-align:left       
}
#searchForm
{
    width:250px;
    height:100%;
    float:left;
    border-color:#669999;       
}
#printOption
{
    width:250px;
    height:100%;
    float:left;
    border-color:#669999;  
    margin-left:80px;     
}
#printOption .formControlHolders .leftControl
{
    width:60%;
}
#printOption .formControlHolders .rightControl
{
    width:20%;
}
#printOption .formControlHolders .controlContainer
{
    height:20px;    
}
.controlContainer
{
    width:100%;
}
.rightControl
{
    margin-left:20px
}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<div class="container">
<div id="topPane" style="margin-left:8%;">
    <div class="form" id="searchForm" style="width:500px">
        <div class="formHeader" align="center">
            CRITERIA SELECTION</div>
        <div class="formControlHolders">
            <%--SEARCH CRITERIA SELECTION--%>
            <div class="controlContainer">
                <div class="leftControl">
                    <asp:Label ID="Label4" runat="server" Text="SEARCHING CRITERIA" ></asp:Label>
                </div>
                <div class="rightControl" align="left" style="margin-left:20px">
                    <asp:Label ID="Label6" runat="server" Text="SEARCHING KEY" ></asp:Label>                                        
                </div>
            </div>
            <div class="controlContainer">
                <div class="leftControl">
                    <asp:DropDownList ID="cmbSearchCriteria" runat="server" Width="180px">
                    <asp:ListItem Text="-- Select Criteria --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Tracking Number" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Sampling Code" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Grading Code" Value="3"></asp:ListItem>
                    <asp:ListItem Text="GRN Number" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Client ID" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Voucher Number" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Scale Ticket Number" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Deposit Ticket Number" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="rightControl" align="left">
                    <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>                                   
                </div>
            </div>
            <%--SEARCH BUTTON--%>
            <div class="controlContainer">
                <div class="leftControl">
                    <asp:Label ID="Label3" runat="server" Text="." ></asp:Label>
                </div>
                <div class="rightControl">

               <asp:Button ID="btnSearch" runat="server" Text="Search" Width="63px" Height="21px" 
                BorderStyle="None" BackColor="#88AB2D" ForeColor="#ffffff" 
                    onclick="btnSearch_Click" />
                </div>
            </div>
            <div class="controlContainer" align="right">
                <asp:Label ID="lblSearchResultStatus" runat="server" Font-Bold="False"  
                    Font-Names="Comic Sans MS" Font-Overline="False"></asp:Label>
            </div>   
        </div>
    </div>


<div id="printOption">
        <div class="formHeader" align="center">
            OPERATION</div>
   <div class="formControlHolders" style="height:90%">
            <%--ARRIVAL PRINT LINKS--%>
            <div class="controlContainer">
                <div class="leftControl">
                    <asp:Label ID="Label2" runat="server" Text="Arrival"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkPrintArrival" runat="server" 
                        onclick="lnkPrintArrival_Click" >Go</asp:LinkButton>                    
                </div>
            </div>
            <%--SAMPLING TICKET PRINT LINKS--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label8" runat="server" Text="Print Sample Ticket"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkPrintSamplingTicket" runat="server" 
                        onclick="lnkPrintSamplingTicket_Click">Go</asp:LinkButton>
                </div>
            </div>
            <%--SAMPLING RESULT PRINT LINKS--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label5" runat="server" Text="Sampling"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkPrintSampling" runat="server" 
                        onclick="lnkPrintSampling_Click">Go</asp:LinkButton>
                </div>
            </div>
              <%--Print Grading Code--%>
            <div class="controlContainer">
                <div class="leftControl" >
                  <asp:Label ID="lblPrintGradingCode" runat="server" Text="Print Grading Code"></asp:Label>  
                </div>
                <div class="rightControl">
                  <asp:LinkButton ID="lnkPrintGradingCode" runat="server" 
                        onclick="lnkPrintGradingCode_Click">Go</asp:LinkButton>  
                </div>
            </div>  
            <%--CLIENT ACCEPTANCE--%>
            <div class="controlContainer">
                <div class="leftControl" >
                  <asp:Label ID="Label7" runat="server" Text="Grading Result"></asp:Label>  
                </div>
                <div class="rightControl">
                  <asp:LinkButton ID="lnkPrintGrading" runat="server" 
                        onclick="lnkPrintGrading_Click">Go</asp:LinkButton>  
                </div>
            </div>  
            <%--GRADING PRINT LINKS--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label1" runat="server" Text="Acceptance"></asp:Label>
                    
                </div>
                <div class="rightControl">
                    
                        <asp:LinkButton ID="lnkClientAcceptance" runat="server" 
                        onclick="lnkClientAcceptance_Click">Go</asp:LinkButton>
                </div>
            </div>
            <%--GRN PRINT LINKS--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label9" runat="server" Text="GRN"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkPrintGrn" runat="server" onclick="lnkPrintGrn_Click">Go</asp:LinkButton>
                </div>
            </div>  
            <%--LIC APPROVAL LINK--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label10" runat="server" Text="LIC Approval"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkLicApproval" runat="server" 
                        onclick="lnkLicApproval_Click">Go</asp:LinkButton>
                </div>
            </div> 
            <%--SUPERVISOR APPROVAL LINK--%>
            <div class="controlContainer">
                <div class="leftControl" >
                    <asp:Label ID="Label11" runat="server" Text="Sup Approval"></asp:Label>
                </div>
                <div class="rightControl">
                    <asp:LinkButton ID="lnkSupervisorApproval" runat="server" 
                        onclick="lnkSupervisorApproval_Click">Go</asp:LinkButton>
                </div>
            </div> 
        </div>
    </div>
</div>
<br />
<%--GRID--%>
<div id="searchResultsetGrid"  style="clear:both; margin-left:8%;">

    <asp:GridView ID="grdSearchResultList" runat="server" 
        AutoGenerateColumns="False"
        
        
        
        
        DataKeyNames="ArrivalId,TrackingNumber,SamplingsID,GradingsId,GradingCode,GRNID,GradingTrackingNumber,WarehouseID,ProductionYear,VoucherNumber,ResultReceivedDateTime, Status, CodeReceivedDateTime,GradingFactorGroupID,GradingResultStatusID,ClientAcceptanceTimeStamp,ClientRejectTimeStamp" CellPadding="4" 
        ForeColor="#333333" GridLines="None" Width="35%" BorderColor="#336600" 
        BorderStyle="Solid" BorderWidth="1px" AllowPaging="True" 
        EnablePersistedSelection="True" 
        onpageindexchanging="grdSearchResultList_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Select choice" HeaderStyle-Width="130" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:CheckBox ID="chkPrintChoice" runat="server" AutoPostBack="true" oncheckedchanged="chkPrintChoice_CheckedChanged" />
            </ItemTemplate>
            <HeaderStyle Width="130px"></HeaderStyle>
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Searching Parameter" HeaderStyle-Width="180">
            <ItemTemplate>
                <asp:Label ID="lblSerchingParameter" runat="server" Text='<%#Bind("SerchingParameter") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField> 
               
        <asp:TemplateField HeaderText="Tracking No." HeaderStyle-Width="180" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblTrackingNumber" runat="server" Text='<%#Bind("TrackingNumber") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="New Tracking No." HeaderStyle-Width="180" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblSampleTrackingNumber" runat="server" Text='<%#Bind("SampleTrackingNumber") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField  HeaderText="Sampling Code" HeaderStyle-Width="180" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblSampleCode" runat="server" Text='<%#Bind("SampleCode") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField  HeaderText="Grading Code"  HeaderStyle-Width="180" Visible="false"> 
            <ItemTemplate>
                <asp:Label ID="lblGradingCode" runat="server" Text='<%#Bind("GradingCode") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField  HeaderText="GRN Number" HeaderStyle-Width="180" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblGrnNumber" runat="server" Text='<%#Bind("GRN_Number") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>

        <asp:TemplateField  HeaderText="GRN ID"  HeaderStyle-Width="180" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblGrnId" runat="server" Text='<%#Bind("GRNID") %>'></asp:Label>
            </ItemTemplate>
        <HeaderStyle Width="180px"></HeaderStyle>
        </asp:TemplateField>


    </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#4D693B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
   </div>

</div>

</ContentTemplate>
</asp:UpdatePanel>





