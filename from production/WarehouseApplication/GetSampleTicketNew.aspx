<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="GetSampleTicketNew.aspx.cs" Inherits="WarehouseApplication.GetSampleTicketNew" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .label
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:Messages ID="Messages" runat="server" />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="container">
        <div style="width: 980px">
        <asp:UpdatePanel ID="upSearch" runat="server">
                        <ContentTemplate>
            <div style="margin: 10px 0 5px 0; height: 52px; border-bottom-width: medium; border-bottom-style: double;
                vertical-align: bottom; border-bottom-color: #88AB2D">
                <div style="background-color: #88AB2D; margin-bottom: 2px; text-align: justify;">
                    <asp:Label ID="lblClassHeader" runat="server" ForeColor="white" Text="Search Criteria’s"
                        Style="font-size: larger; font-weight: bolder;"></asp:Label></div>
                <div style="float: left; margin-left: 10px; margin-bottom: 10px;">
                    <asp:Label ID="lblTrackingNo" runat="server" Text="Tracking No: " ToolTip="Enter Tracking No to search with."
                        Width="90px"></asp:Label>
                    <asp:TextBox ID="txtTrackingNo" runat="server" ToolTip="Enter Tracking No to search with."></asp:TextBox>
                </div>
                <div style="float: left; margin-left: 10px; margin-bottom: 10px;">
                    <asp:Label ID="lblPreSampleCode" runat="server" Text="Prev Sample Code: " ToolTip="Enter Prev Sample Code to search with. For Driver Missing and ReSample Case Only"
                        Width="125px"></asp:Label>
                    <asp:TextBox ID="txtPreSampleCode" runat="server" ToolTip="Enter Prev Sample Code to search with. For Driver Missing and ReSample Case Only"></asp:TextBox>
                </div>
                <div style="float: left; margin-left: 10px; margin-bottom: 10px;">
                    <asp:Label ID="lblPreGradingCode" runat="server" Text="Prev Grading Code: " ToolTip="Enter Prev Grading Code to search with. For ReSample Case Only"
                        Width="133px"></asp:Label>
                    <asp:TextBox ID="txtPreGradingCode" runat="server" ToolTip="Enter Prev Grading Code to search with. For ReSample Case Only"></asp:TextBox>
                </div>
                <div style="float: left; margin-left: 10px; margin-top:9px; vertical-align: bottom;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="vertical-align: bottom;"
                        BackColor="#88AB2D" ForeColor="White" Font-Size="X-Small" Width="59px" BorderStyle="None"
                        Height="15px" onclick="btnSearch_Click" />
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                Loading...</a>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
            <div>
                <div valign="top" style="height: 930px; border-color: Gray; border-style: solid;
                    border-width: thin; float: left; margin-left: 1px; width: 160px;">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                Loading...</a>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--<asp:LinkButton ID="lbtnReloadForNew" runat="server" onclick="lbtnReload_Click">Reload</asp:LinkButton>--%>
                            <a style="color: #669999; font-size: larger"><b>New Arrivals</b></a>
                            <asp:ImageButton ID="lbtnReloadForNew" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                                Width="17px" OnClick="btnReloadForResample_Click" ToolTip="Reload" /><br />
                            <asp:Button ID="btnGetSample" runat="server" Text="Get Sample Ticket" BackColor="#88AB2D"
                                ForeColor="White" Width="117px" BorderStyle="None" OnClick="btnGetSample_Click"
                                Height="23px" />
                            <asp:GridView ID="gvWaitingForSampling" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="Black" EmptyDataText="No Samples Pending"
                                ShowHeaderWhenEmpty="True" AutoGenerateSelectButton="True" AllowPaging="True"
                                OnPageIndexChanging="gv_PageIndexChanging" PageSize="27" Width="160px" CssClass="label">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#DAE1CC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalId" runat="server" Text='<%# Bind("ArrivalId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("SampleId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradeId" runat="server" Text='<%# Bind("GradeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tracking No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrackingNo" Style="overflow: auto; direction:rtl;" runat="server" Width="100px"
                                                Text='<%# Bind("TrackingNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Date of Arrival">
                    <ItemTemplate>
                        <asp:Label ID="lblDateRequested" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                    </ItemTemplate>       
                </asp:TemplateField>--%>
                                </Columns>
                                <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                    BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="0.8em" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div valign="top" style="height: 930px; border-color: Gray; border-style: solid;
                    border-width: thin; float: left; margin-left: 31px; width: 290px">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                        <ProgressTemplate>
                            <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                Loading...</a>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <%--<asp:LinkButton ID="lbtnReloadForDriverMissing" runat="server" 
                                onclick="lbtnReload_Click">Reload</asp:LinkButton>--%>
                            <a style="color: #669999; font-size: larger"><b>Drivers Missing</b></a>
                            <asp:ImageButton ID="lbtnReloadForDriverMissing" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                                Width="17px" OnClick="btnReloadForResample_Click" ToolTip="Reload" />
                            <br />
                            <asp:Button ID="btnGetSampleForDriverMissing" runat="server" Text="Get Sample Ticket"
                                BackColor="#88AB2D" ForeColor="White" Width="131px" BorderStyle="None" OnClick="btnGetSampleForDriverMissing_Click"
                                Height="21px" />
                            <asp:GridView ID="gvWaitForDriver" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="None" AutoGenerateSelectButton="True"
                                EmptyDataText="No Arrivals Pending Sampling" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                OnPageIndexChanging="gv_PageIndexChanging" PageSize="25" Width="290px" CssClass="label">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#DAE1CC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalId" runat="server" Text='<%# Bind("ArrivalId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("SampleId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradeId" runat="server" Text='<%# Bind("GradeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tracking No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrackingNo" Style="overflow: auto;direction:rtl;" Width="118px" runat="server"
                                                Text='<%# Bind("TrackingNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Sample Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreSampleCode" runat="server" Style="overflow: auto;direction:rtl;" Width="105px"
                                                Text='<%# Bind("SampleCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Date of Arrival">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateRequested" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>--%>
                                </Columns>
                                <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                    BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="0.8em" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div valign="top" style="height: 930px; border-color: Gray; border-style: solid;
                    border-width: thin; float: left; margin-left: 31px; width: 455px;">
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                        <ProgressTemplate>
                            <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                Loading...</a>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <%--<asp:LinkButton ID="lbtnReloadForResample" runat="server" 
                                                    onclick="lbtnReload_Click">Reload</asp:LinkButton>--%>
                            <a style="color: #669999; font-size: larger"><b>ReSampling</b></a>
                            <asp:ImageButton ID="lbtnReloadForResample" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                                Width="17px" OnClick="btnReloadForResample_Click" ToolTip="Reload" />
                            <br />
                            <asp:Button ID="btnGetSampleTicketForMF" runat="server" Text="Get Sample Ticket"
                                BackColor="#88AB2D" ForeColor="White" Width="132px" BorderStyle="None" OnClick="btnGetSampleTicketForMF_Click"
                                Height="21px" />
                            <asp:GridView ID="gvWaitForReSamle" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" BorderColor="Black" AutoGenerateSelectButton="True"
                                EmptyDataText="No Samples Pending" ShowHeaderWhenEmpty="True" AllowPaging="True"
                                OnPageIndexChanging="gv_PageIndexChanging" PageSize="21" Width="455px" CssClass="label">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#DAE1CC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblArrivalId" runat="server" Text='<%# Bind("ArrivalId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("SampleId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradeId" runat="server" Text='<%# Bind("GradeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreSamplerId" runat="server" Text='<%# Bind("PreSamplerID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreSamplerName" runat="server" Text='<%# Bind("PreSamplerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreSampleInspectorID" runat="server" Text='<%# Bind("PreSampleInspectorID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradingStatusID" runat="server" Text='<%# Bind("GradingStatusID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tracking No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrackingNo" Style="overflow: auto;direction:rtl;" Width="100px" runat="server"
                                                Text='<%# Bind("TrackingNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Sample Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreSampleCode" runat="server" Style="overflow: auto;direction:rtl;" Width="100px"
                                                Text='<%# Bind("SampleCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Grading Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreGradingCode" runat="server" Style="overflow: auto;direction:rtl;" Width="93px"
                                                Text='<%# Bind("GradingCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grading Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGradingStatus" runat="server" Style="overflow: auto;direction:rtl;" Width="80px"
                                                Text='<%# Bind("GradingStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Date of Arrival">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateRequested" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                                </ItemTemplate>       
                            </asp:TemplateField>--%>
                                </Columns>
                                <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                    BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="0.8em" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
