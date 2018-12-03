<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true"
    CodeBehind="MapingCommodityClassGrade.aspx.cs" Inherits="WarehouseApplication.MapingCommodityClassGrade"
    EnableEventValidation="false" %>

<%@ Register Src="Messages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="messages.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValueChanged(rb) {
            var forclass = document.getElementById('<%= panelForClass.ClientID %>');
            var forgrade = document.getElementById('<%= panelForGrade.ClientID %>');
            var pnSrhLocation = document.getElementById('<%= pnSrhLocation.ClientID %>');
            var gvforclass = document.getElementById('<%= gvCommodityClassGrade.ClientID %>');
            var gvforgrade = document.getElementById('<%= gvCommodityGrade.ClientID %>');
            var lblclasssysmbol = document.getElementById('<%= lblClassSymbol.ClientID %>');
            var txtclasssymbol = document.getElementById('<%= txtClassSymbol.ClientID %>');
            var lblgradesymbol = document.getElementById('<%= lblGrade.ClientID %>');
            var txtgradesymbol = document.getElementById('<%= txtGrade.ClientID %>');
            var radiolist = document.getElementById('<%= rbCommodityClassGrade.ClientID %>');
            var radio = radiolist.getElementsByTagName("input");
            if (radio[0].checked) {
                forclass.style.display = "inline-block";
                forgrade.style.display = "none";
                gvforclass.style.display = "inline-block";
                pnSrhLocation.style.display = "inline-block";
                gvforgrade.style.display = "none";
                lblclasssysmbol.style.display = "none";
                txtclasssymbol.style.display = "none";
                lblgradesymbol.style.display = "none";
                txtgradesymbol.style.display = "none";
            }
            else {
                forclass.style.display = "none";
                forgrade.style.display = "inline-block";
                gvforclass.style.display = "none";
                pnSrhLocation.style.display = "none";
                gvforgrade.style.display = "inline-block";
                lblclasssysmbol.style.display = "inline";
                txtclasssymbol.style.display = "inline";
                lblgradesymbol.style.display = "inline";
                txtgradesymbol.style.display = "inline";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:Messages ID="Messages" runat="server" />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="">
        <div>
            <div style="background-color: #88AB2D; margin-top: 7px">
                <asp:Label ID="Label1" runat="server" ForeColor="white" Text="Search Criteria's"
                    CssClass="style1"></asp:Label></div>
            <div style="margin-top: 5px;">
                <div>
                    <asp:RadioButtonList RepeatColumns="3" RepeatDirection="Vertical" ID="rbCommodityClassGrade"
                        runat="server" Width="489px" Height="25px">
                        <asp:ListItem Text="Commodity Grading Class" Value="1" Selected="True" onclick="ValueChanged(this);"></asp:ListItem>
                        <asp:ListItem Text="Commodity Grade" Value="2" onclick="ValueChanged(this);"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div style="margin-left: 20px; float: left;">
                    <%-- --%>
                    <div class="controlContainer" style="margin-bottom: 6px;">
                        <div class="leftControl">
                            <asp:Label ID="lblSymbol" runat="server" Text="Symbol:" Width="100px"></asp:Label>
                        </div>
                        <div class="rightControl">
                            <asp:TextBox ID="txtSymbol" runat="server" Height="22px" Width="167px"></asp:TextBox>
                        </div>
                    </div>
                    <div style="margin-bottom: 10px;">
                        <div class="leftControl">
                            <asp:Label ID="lblCommodity" runat="server" Text="Commodity:" Width="100px"></asp:Label></div>
                        <div class="rightControl">
                            <asp:DropDownList ID="cboCommodity" runat="server" Height="22px" Width="168px" OnSelectedIndexChanged="cboCommodity_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="rightControl" style="text-align: center; margin-top: 8px;">
                            <asp:CheckBox ID="chkShowInActive" runat="server" Text="Show Inactive" />
                        </div>
                    </div>
                </div>
                <div style="margin-left: 20px; float: left;">
                    <%--<asp:DropDownList
        ID="cboCommodityClassForSrh" runat="server" Height="22px" Width="168px" Visible="false" ></asp:DropDownList>--%>
                    <div class="controlContainer" style="margin-bottom: 6px; width: 447px;">
                        <div class="leftControl" style="width: 147px;">
                            <asp:Label ID="lblClassSymbol" runat="server" Width="175px" Text="Grading Class Symbol:"
                                Height="20px"></asp:Label></div>
                        <div class="rightControl" style="margin-left: 10px;">
                            <asp:TextBox ID="txtClassSymbol" runat="server" Height="22px" Width="167px"></asp:TextBox></div>
                    </div>
                    <div style="margin-bottom: 10px; width: 447px; margin-top: 10px;">
                        <div class="leftControl" style="width: 147px;">
                            <asp:Label ID="lblGrade" runat="server" Text='Grade:' Width="170px"></asp:Label></div>
                        <div class="rightControl" style="margin-left: 10px;">
                            <asp:TextBox ID="txtGrade" runat="server" Height="22px" Width="167px"></asp:TextBox></div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="margin-left: 20px; clear: both; width: 527px; height: 33px;">
                            <asp:Panel ID="pnSrhLocation" runat="server" Style="display: inline-block; border: solid 1px #88AB2D;
                                clear: both; width: 487px;">
                                <div style="float: left; margin-top: 5px;">
                                    <asp:Label ID="lblSrhRegion" runat="server" Text="Region:" Width="50px"></asp:Label>
                                    <asp:DropDownList ID="drpSrhRegion" runat="server" Height="22px" Width="100px" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpSrhRegion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; margin-left: 8px; margin-top: 5px;">
                                    <asp:Label ID="lblSrhZone" runat="server" Text="Zone:" Width="50px"></asp:Label>
                                    <asp:DropDownList ID="drpSrhZone" runat="server" Height="22px" Width="100px" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpSrhZone_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div style="float: left; margin-left: 8px; margin-top: 5px;">
                                    <asp:Label ID="lblSrhWoreda" runat="server" Text="Woreda:" Width="55px"></asp:Label>
                                    <asp:DropDownList ID="drpSrhWoreda" runat="server" Height="22px" Width="100px" ValidationGroup="wa">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="clear: both; margin-left: 20px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                    <ProgressTemplate>
                        <a style="font-family: Agency FB; font-size: 14pt; color: Green; font-weight: lighter">
                            Loading...</a>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:LinkButton ID="lbtnReloadForResample" runat="server" OnClick="lbtnReload_Click">Search</asp:LinkButton>
                        <asp:GridView ID="gvCommodityClassGrade" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" AutoGenerateSelectButton="True" ShowHeaderWhenEmpty="True"
                            OnSelectedIndexChanged="gvCommodityClassGrade_SelectedIndexChanged" EmptyDataText="The list empty"
                            PageSize="10" AllowPaging="True" OnPageIndexChanging="gvCommodityClassGrade_PageIndexChanging">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#DAE1CC" />
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentID" runat="server" Text='<%# Bind("ParentID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# Bind("CommodityID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradingFactorGroupID" runat="server" Text='<%# Bind("GradingFactorGroupID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Symbol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Factor Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGradingFactorGroup" runat="server" Text='<%# Bind("GradingFactorGroup") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Commodity Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodityType" runat="server" Text='<%# Bind("CommodityGroupI") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                            <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="0.8em" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <asp:GridView ID="gvCommodityGrade" runat="server" DataKeyNames="ID" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" AutoGenerateSelectButton="True" ShowHeaderWhenEmpty="True"
                            OnSelectedIndexChanged="gvCommodityClassGrade_SelectedIndexChanged" EmptyDataText="The list empty"
                            PageSize="10" AllowPaging="True" OnPageIndexChanging="gvCommodityClassGrade_PageIndexChanging">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#DAE1CC" />
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParentID" runat="server" Text='<%# Bind("ParentID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# Bind("CommodityID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Symbol">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Class") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGrade" runat="server" Text='<%# Bind("Grade") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Min Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMinimumTotalValue" runat="server" Text='<%# Bind("MinimumTotalValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Max Total Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaximumTotalValue" runat="server" Text='<%# Bind("MaximumTotalValue") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Classification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassification" runat="server" Text='<%# Bind("Classification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ClassificationNo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassificationNo" runat="server" Text='<%# Bind("ClassificationNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
            <div style="border-top-color: #88AB2D; border-top-style: solid; border-top-width: thin;
                height: 3px; margin-top: 8px;">
            </div>
            <div style="float: left; margin-left: 20px;">
                <asp:UpdatePanel ID="upClass" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panelForClass" runat="server" Style="display: inline-block;">
                            <div style="text-align: justify; margin-top: 8px;">
                                <asp:CheckBox ID="chkInActiveClass" runat="server" Text="Inactive" />
                            </div>
                            <div style="background-color: #88AB2D; margin-top: 7px">
                                <asp:Label ID="lblClassHeader" runat="server" ForeColor="white" Text="For Commodity Class Case only"
                                    CssClass="style1"></asp:Label></div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblRegion" runat="server" Text="Region:" Width="100px"></asp:Label>
                                <asp:DropDownList ID="cboRegion" runat="server" Height="22px" Width="168px" ValidationGroup="wa"
                                    AutoPostBack="true" OnSelectedIndexChanged="cboRegion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cboRegion"
                                    ForeColor="Red" ErrorMessage="Region must first be selected!" ValidationGroup="wa">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator5">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblZone" runat="server" Text="Zone:" Width="100px"></asp:Label>
                                <asp:DropDownList ID="cboZone" runat="server" Height="22px" Width="168px" AutoPostBack="true"
                                    ValidationGroup="wa" OnSelectedIndexChanged="cboZone_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboZone"
                                    ForeColor="Red" ErrorMessage="Zone must first be selected!" ValidationGroup="wa">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator6">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblWoreda" runat="server" Text="Woreda:" Width="100px"></asp:Label>
                                <asp:DropDownList ID="cboWoreda" runat="server" Height="22px" Width="168px" ValidationGroup="wa">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboWoreda"
                                    ForeColor="Red" ErrorMessage="Woreda name must first be selected!" ValidationGroup="wa">*</asp:RequiredFieldValidator>
                                <asp:Button ID="btnAddWoreda" runat="server" Text="Add Woreda To Class" BackColor="#88AB2D"
                                    ForeColor="White" Width="172px" BorderStyle="None" OnClick="btnAddWoreda_Click"
                                    Height="22px" ValidationGroup="wa" />
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator4">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:GridView ID="gvClassWereda" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" ShowHeaderWhenEmpty="True" EmptyDataText="The list empty" Width="600px"
                                    AllowPaging="True" OnPageIndexChanging="gvClassWereda_PageIndexChanging" PageSize="6">
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#DAE1CC" />
                                    <Columns>
                                        <asp:TemplateField ControlStyle-Width="50Px" HeaderText="" Visible="true">
                                            <ItemTemplate>
                                                <a id="linkRemoveWoreda" onserverclick="lbtnRemoveWoreda_Click" href='#' woredaid='<%# Eval("WoredaID") %>'
                                                    runat="server">Remove</a>
                                            </ItemTemplate>
                                            <ControlStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWoredaID" runat="server" Text='<%# Bind("WoredaID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Woreda">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWoreda" runat="server" Text='<%# Bind("WoredaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#6A7C49" ForeColor="White" HorizontalAlign="Center" BorderColor="Black"
                                        BorderStyle="Inset" BorderWidth="1px" Font-Bold="True" Font-Names="Verdana" Font-Size="0.8em" />
                                    <SelectedRowStyle BackColor="#FBE49F" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#6A7C49" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                        Font-Size="0.8em" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblFactorGroup" runat="server" Text="Factor Group:" Width="118px"></asp:Label>
                                <asp:DropDownList ID="cboFactorGroup" runat="server" Height="22px" ValidationGroup="mfg"
                                    Width="168px" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvcboFactorGroup" runat="server" ControlToValidate="cboFactorGroup"
                                    ErrorMessage="Please select Facot Group" ForeColor="Red" ValidationGroup="mfg"></asp:RequiredFieldValidator></div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblCommodityType" runat="server" Text="Commodity Type:" Width="118px"></asp:Label>
                                <asp:DropDownList ID="drpCommodityType" runat="server" Height="22px" ValidationGroup="mfg"
                                    Enabled="false" Width="168px" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfdrpCommodityType" runat="server" ControlToValidate="drpCommodityType"
                                    Enabled="false" ErrorMessage="Please select Commodity Type" ForeColor="Red" ValidationGroup="mfg"></asp:RequiredFieldValidator></div>
                            <div style="float: none; margin-top: 5px; text-align: center">
                                <asp:Button ID="btnMapFactorGroup" runat="server" Text="Map To Class" BackColor="#88AB2D"
                                    ForeColor="White" Width="160px" BorderStyle="None" OnClick="btnMapFactorGroup_Click"
                                    Height="22px" ValidationGroup="mfg" />
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="rfvcboFactorGroup">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upGrade" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="panelForGrade" runat="server">
                            <div style="text-align: justify; margin-top: 8px;">
                                <asp:CheckBox ID="chkInActiveGrade" runat="server" Text="InActive" />
                            </div>
                            <div style="background-color: #88AB2D; margin-top: 7px">
                                <asp:Label ID="lblGradeHeader" runat="server" Text="For Commodity Grade Case only"
                                    CssClass="style1" ForeColor="white"></asp:Label></div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblCommodityClass" runat="server" Text="Commodity Grading Class:"
                                    Width="124px"></asp:Label>
                                <asp:DropDownList ID="cboCommodityClass" runat="server" Height="22px" Width="168px"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboCommodityClass"
                                    ErrorMessage="Commodity Class is required" ForeColor="Red" ValidationGroup="mgrade">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblMinTotalValue" runat="server" Text="Min Total Value:" Width="124px"></asp:Label>
                                <asp:TextBox ID="txtMinTotalValue" runat="server" Height="22px" Width="168px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMinTotalValue"
                                    ErrorMessage="Min Total Value is required" ForeColor="Red" ValidationGroup="mgrade">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator2">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtMinTotalValue"
                                    ErrorMessage="Can be a number between -1 and 100" ForeColor="Red" MaximumValue="100"
                                    MinimumValue="-1" Type="Integer" ValidationGroup="mgrade">*
                                </asp:RangeValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True"
                                    TargetControlID="RangeValidator2">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblMaxTotalValue" runat="server" Text="Max Total Value:" Width="124px"></asp:Label>
                                <asp:TextBox ID="txtMaxTotalValue" runat="server" Height="22px" Width="168px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMaxTotalValue"
                                    ErrorMessage="Max Total Value is required" ForeColor="Red" ValidationGroup="mgrade">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator3">
                                </cc1:ValidatorCalloutExtender>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMaxTotalValue"
                                    ErrorMessage="Can be a whole number between 1 and 100" ForeColor="Red" MaximumValue="100"
                                    MinimumValue="1" Type="Integer" ValidationGroup="mgrade">*
                                </asp:RangeValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
                                    TargetControlID="RangeValidator1">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:Label ID="lblGradeValue" runat="server" Text="Grade Value:" Width="124px"></asp:Label>
                                <asp:TextBox ID="txtGradeValue" runat="server" Height="22px" Width="168px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvtxtGradeValue" runat="server" ControlToValidate="txtGradeValue"
                                    ErrorMessage="Grade Value is required" ForeColor="Red" ValidationGroup="mgrade">*</asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True"
                                    TargetControlID="rvtxtGradeValue">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div style="margin-top: 5px;">
                                <asp:CheckBoxList ID="chkList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                <asp:RadioButtonList ID="rbList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                                <div style="text-align: center; margin-top: 5px">
                                    <asp:Button ID="btnMapCommodityGrade" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        ForeColor="White" Height="22px" OnClick="btnMapCommodityGrade_Click" Text="Map To Commodity Grade"
                                        ValidationGroup="mgrade" Width="188px" /></div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="border-top-color: #88AB2D; border-top-style: solid; border-top-width: thin;
            height: 3px; margin-top: 15px; clear: both;">
        </div>
    </div>
    </div>
</asp:Content>
