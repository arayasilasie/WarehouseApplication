<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="ListInboxNew.aspx.cs" Inherits="WarehouseApplication.ListInboxNew" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">
                <div id="MainForm" class="form" style="margin: 10px; float: left; width: 43%;">
                    <div id="Header" class="formHeader" align="center">
                        <asp:Label ID="Label2" runat="server" Text="Warehouse Application Inbox"></asp:Label>
                        <%-- <a style="color: #669999; font-size: larger"><b >Warehouse Applicaion Inbox</b></a>--%>
                    </div>
                    <div style="width: 100%; margin-top: 10px;">
                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                            Loading...</a>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div id="Div1" class="formHeader" align="center"    
                            style="border-color: #FFCC00; 	color:#008000; background-color: #FFFFFF; vertical-align: middle;">
                            <asp:Label ID="Label1" runat="server" Text="GRN Creation Queue" ></asp:Label>
                        </div>
                        <%--<asp:ImageButton ID="lbtnReloadGRN" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                            Width="17px" ToolTip="Reload" />--%>
                        <asp:GridView ID="grvGRNCreation" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" OnRowDataBound="grvGRNCreation_RowDataBound" OnSelectedIndexChanged="grvGRNCreation_SelectedIndexChanged"
                            DataKeyNames="StepID,Task" Width="100%">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#DAE1CC" />
                            <Columns>
                                <asp:TemplateField HeaderText="Tasks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTasks" Width="250px" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StepID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStepID" runat="server" Text='<%# Bind("StepID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TypeID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTypeID" runat="server" Text='<%# Bind("TypeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Task Count">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaskCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Task List"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                                            Text="Task List"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                            <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="white" Font-Names="Verdana"
                                Font-Size="0.8em" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                        <br />
                        <br />
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                    Loading...</a>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                             <div id="Div4" class="formHeader" align="center"    
                            style="border-color: #FFCC00; 	color:#008000; background-color: #FFFFFF; vertical-align: middle;">
                            <asp:Label ID="Label5" runat="server" Text="GIN Creation Queue" ></asp:Label>
                        </div>
                              
                            
                                <%--<asp:ImageButton ID="lbtnReloadGIN" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                            Width="17px" ToolTip="Reload" />--%>
                                <asp:GridView ID="grvGINCreation" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Width="100%">
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#DAE1CC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tasks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTasks" Width="250px" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="StepID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStepID" runat="server" Text='<%# Bind("StepID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TypeID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeID" runat="server" Text='<%# Bind("TypeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Task List" NavigateUrl='<%# GetUrl(Eval("StepID")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                        BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                        Font-Size="0.8em" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                         <br />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                                    Loading...</a>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                         <div id="Div2" class="formHeader" align="center"    
                            style="border-color: #FFCC00; 	color:#008000; background-color: #FFFFFF; vertical-align: middle;">
                            <asp:Label ID="Label3" runat="server" Text="PSA Creation Queue" ></asp:Label>
                        </div>
                            
                                <%--<asp:ImageButton ID="lbtnReloadGIN" runat="server" Height="17px" ImageUrl="~/Images/Refresh.png"
                            Width="17px" ToolTip="Reload" />--%>
                                <asp:GridView ID="grvPSA" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Width="100%" >
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#DAE1CC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tasks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTasks" Width="250px" runat="server" Text='<%# Bind("Task") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaskCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="StepID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStepID" runat="server" Text='<%# Bind("StepID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TypeID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeID" runat="server" Text='<%# Bind("TypeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Task List" NavigateUrl='<%# GetUrl(Eval("StepID")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                        BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                        Font-Size="0.8em" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div id="divDetail" class="form" style="margin: 10px; float: right;">
                    <asp:Panel ID="pnlDetail" Visible="false" runat="server">
                        <div id="divDetailHeader" class="formHeader" style="width: 75%;" align="center">
                            <asp:Label ID="lblDetail" runat="server"></asp:Label>
                            <%-- <a style="color: #669999; font-size: larger"><b >Warehouse Applicaion Inbox</b></a>--%>
                        </div>
                        <div style="margin-top: 10px;">
                            <asp:GridView ID="grvDetail" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="1px" AllowPaging="True" OnPageIndexChanging="grvDetail_PageIndexChanging"
                                PageSize="40" OnRowCommand="grvDetail_RowCommand" OnSelectedIndexChanged="grvDetail_SelectedIndexChanged"
                                Width="75%">
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#DAE1CC" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Task No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTasks" Width="200px" runat="server" Text='<%# Bind("TaskNo") %>'></asp:Label>
                                            <asp:Label ID="lblTrackingNo" Width="200px" Visible="false" runat="server" Text='<%# Bind("TaskNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TaskNo" Visible="False" />
                                    <asp:ButtonField CommandName="Detail" Text="Detail" />
                                </Columns>
                                <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                    BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#88AB2D" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="0.8em" />
                                <EditRowStyle BackColor="#7C6F57" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            <br />
                        </div>
                    </asp:Panel>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
