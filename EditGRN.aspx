<%@ Page Title="" Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="EditGRN.aspx.cs" Inherits="WarehouseApplication.EditGRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
<div id="Div1" style="width: 87%; margin-left: 20px; margin-top: 5px;" align="center">
                    <asp:Label ID="LblConfirmation" runat="server" 
                        Text="GRN / Warehouse Receipt Successfully Updated!!" ForeColor="Green" 
                        Visible="False"></asp:Label>
                                 <asp:Label ID="LblConfirm" runat="server" Text="" ForeColor="Tomato"></asp:Label>
                </div>
            <div id="Header" class="formHeader" style="width: 87%; margin-left: 20px; margin-top: 5px;" align="center">
                    <asp:Label ID="lblDetail" Text="EDIT GRN TO WAREHOUSE" Width="100%" runat="server"></asp:Label>
                </div>

                <div style="float: left; width: 87%; margin-left: 20px;">
                    <div style="margin-bottom: 10px;">
                        <div style="border: solid 1px #88AB2D; height: 70px;">
                            <div style="margin-top: 10px; float: left; height: 26px; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="GRN No :"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtGRNNo" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                           <%-- <div style="margin-top: 10px; margin-left: 7px; float: left;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblClientId0" runat="server" Text="Client Id :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="margin-top: 10px; float: left; margin-left: 7px;">
                                <div style="height: 26px;">
                                    <asp:Label ID="lblLIC0" runat="server" Text="LIC :" CssClass="label"></asp:Label>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddLIC" runat="server" AppendDataBoundItems="True" CssClass="style1"
                                        ValidationGroup="Search" Width="145px">
                                        <asp:ListItem Value="">Select LIC</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                            <div style="margin-top: 10px; float: left; margin-left: 20px;">
                                <div style="height: 26px;">
                                </div>
                                <div>
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#88AB2D" BorderStyle="None"
                                        CssClass="style1" ForeColor="White" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                                </div>
                           
                            </div>
                            <div style="margin-top: 10px; float:right; margin-right: 10px;">
                                <div style="height: 26px">
                                </div>
                                <div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<div id="commodityDepositRequestForm" class="form"> 
                 <div class="formHeader" align="center"> 
    UDATE GRN INFORMATION </div>
<div style="border: solid 1px #999933;">
           
    <%--START OF LEFT TOP FORM--%>

<%--GRNNo--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label6" runat="server" Text="GRNNo:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="TxtGRNNum" runat="server" Width="150px" Enabled="False"></asp:TextBox>
           

            </div>        
          </div>

<%--CLIENT--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label5" runat="server" Text="CLIENT:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="TxtCLIENT" runat="server" Width="150px" Enabled="False"></asp:TextBox>
            

            </div>        
          </div>

<%--SHADE--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label4" runat="server" Text="Symbol:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="TxtSymbol" runat="server" Width="150px" Enabled="False"></asp:TextBox>
            

            </div>        
          </div>

<%--Quadrant:--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label7" runat="server" Text="Quadrant:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="TxtQuadrant" runat="server" Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save" ForeColor="Tomato" 
            ControlToValidate="TxtQuadrant" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>        
          </div>

<%--LIC--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label1" runat="server" Text="LIC Name 2:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:DropDownList ID="ddLIC" runat="server" Width="150px" AutoPostBack="True" 
                    onselectedindexchanged="ddLIC_SelectedIndexChanged" AppendDataBoundItems="true">
                </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save" ForeColor="Tomato" 
            ControlToValidate="ddLIC" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>        
          </div>


<%--SHADE--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label10" runat="server" Text="Shade"></asp:Label>
            </div>        
            <div class="rightControl">

                <%--<asp:TextBox ID="txtShed" runat="server" Width="150px"></asp:TextBox>--%>
                <asp:DropDownList ID="DDLShed" runat="server" Width="150px" 
                    AppendDataBoundItems="true" 
                    onselectedindexchanged="DDLShed_SelectedIndexChanged"></asp:DropDownList>                
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Save" ForeColor="Tomato" 
            ControlToValidate="DDLShed" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>        
          </div>

<%--CONSIGNMENT TYPE--%>
          <div class="controlContainer">    
            <div class="leftControl">   
              <asp:Label ID="Label3" runat="server" Text="Consignment Type:"></asp:Label>
            </div>        
            <div class="rightControl">

                <asp:TextBox ID="TextBox2" runat="server" Width="150px" ReadOnly="true" Text="Truck To Warehouse"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save" ForeColor="Tomato" 
            ControlToValidate="TextBox2" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

            </div>        
          </div>
   <div>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnSave" runat="server" Text="Edit / Update" onclick="btnSave_Click" Width="100px" style="" Height="21px" 
         ValidationGroup="Save" BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" Enabled="false"/>

       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

       <asp:Button ID="btnClear" runat="server" Text="Reset" CssClass="Forbtn" 
           CausesValidation="False" Width="50px" style="" Height="21px" 
        BorderStyle="None" BackColor="#88AB2D" ForeColor="#FFFFCC" Enabled="false" onclick="btnClear_Click" />
            
          
                             
       </div>

     </div>

  


  </div>
          
</asp:Content>
