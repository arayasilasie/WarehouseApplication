<%@ Page Language="C#" MasterPageFile="~/pUtility.Master" AutoEventWireup="true" CodeBehind="SelectWarehouse.aspx.cs" Inherits="WarehouseApplication.SelectWarehouse" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">
    
    <div style="margin: 5%; border: 1px solid #FDBF12; padding:5px; overflow: auto; width:340px">  
    <div style="background-color: #c7e3b6;font-weight: 500;background-color: #c7e3b6; font-variant: small-caps;     
         font-family: Arial, Helvetica, sans-serif;     font-size: large; font-weight:bold">
        Select Warehouse:
    </div>
    <div id="content">
        <table >
            <tr>
        
                <td class="Text" >Warehouse Name: </td>
                <td class="Input"><asp:DropDownList ID="ddlWarehouse" runat="server" Width="180px"/></td>
            </tr>
            <tr>

                <td />
                <td><asp:Button ID="btnOk" runat="server" Text="     OK      " 
                    onclick="btnOk_Click" /></td>
            </tr>
        </table>
    </div>
  </div>
</asp:Content>
