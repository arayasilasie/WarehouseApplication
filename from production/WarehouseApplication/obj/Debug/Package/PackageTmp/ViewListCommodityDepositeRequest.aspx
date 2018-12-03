<%@ Page Language="C#" MasterPageFile="~/pTop.Master" AutoEventWireup="true" CodeBehind="ViewListCommodityDepositeRequest.aspx.cs" Inherits="WarehouseApplication.ViewListCommodityDepositeRequest" Title="Untitled Page" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceApp" runat="server">

  <div id="DepositRequestInformation" style="position:fixed; top: 15px; left: 10px; width: 975px; height: 557px;">
   <fieldset style="height: 161px; width: 917px;">
     <legend style="width: 188px; margin-top: 0px;" >Commodity Deposit Request</legend>
        <div style="width: 91px; position:fixed; top: 45px; left: 26px;">Client Name:</div>
        <div style="width: 339px; position:fixed; top: 45px; left: 137px;">
             <asp:DropDownList ID="cboClientName" runat="server"  Width="339px" ></asp:DropDownList>
        </div>
        <div style="width: 90px; position:fixed; top: 81px; left: 25px; right: 996px;">Region :</div>
        <div style="width: 131px; position:fixed; top: 78px; left: 136px;">
                <asp:DropDownList ID="cboRegion" runat="server" Width="124px"></asp:DropDownList>
        </div>
        <div style="width: 37px; position:fixed; top: 75px; left: 283px;">Zone:</div>
        <div style="width: 135px; position:fixed; top: 73px; left: 340px; height: 22px;">
                <asp:DropDownList ID="cboZone" runat="server" Width="125px"></asp:DropDownList>
        </div>
        <div style="width: 58px; position:fixed; top: 80px; left: 490px;">Woreda:</div>
        <div style="width: 174px; position:fixed; top: 79px; left: 593px;">
                <asp:DropDownList ID="cboWoreda" runat="server" Width="172px"></asp:DropDownList>
        </div>
         <div style="width: 93px; position:fixed; top: 48px; left: 489px; right: 529px;">Representative:</div>
         <div style="width: 307px; position:fixed; top: 45px; left: 590px; height: 24px;">
             <asp:DropDownList ID="cboReprsentative" runat="server"  Width="300px" 
                 TabIndex="1" ></asp:DropDownList>
         </div>
         <div style="width: 92px; position:fixed; top: 111px; left: 21px; right: 998px;">Commodity :</div>
                     <div style="width: 131px; position:fixed; top: 109px; left: 135px; height: 24px;">
                <asp:DropDownList ID="cboCommodity" runat="server" Width="124px"></asp:DropDownList>
         </div>
         <div style="width: 52px; position:fixed; top: 112px; left: 281px; right: 778px;">Weight:</div>
         <div style="width: 130px; position:fixed; top: 109px; left: 342px; height: 24px;">
                <asp:TextBox ID="txtWeight" runat="server" Width="123px"></asp:TextBox>
         </div>
         <div style="width: 90px; position:fixed; top: 112px; left: 488px; right: 533px;">No. of Bags:</div>
         <div style="width: 130px; position:fixed; top: 111px; left: 592px; height: 24px;">
                <asp:TextBox ID="txtNumberOfBags" runat="server" Width="121px"></asp:TextBox>
         </div> 
         <div style="width: 103px; position:fixed; top: 147px; left: 20px; ">Production 
             Year:</div>
         <div style="width: 131px; position:fixed; top: 145px; left: 132px; height: 24px;">
                <asp:DropDownList ID="cboProductionYear" runat="server" Width="124px"></asp:DropDownList>
         </div>
   </fieldset>
  </div>   
      <div id="VoucherInformation" style="position: fixed; height: 124px;"  >
        <fieldset style="width: 915px; height: 131px">
             <legend >Voucher Information</legend>
             <div style="width: 90px; position:fixed; top: 391px; left: 30px; height: 19px;">
                 Driver Name :</div>
             <div style="width: 131px; position:fixed; top: 424px; left: 27px; right: 1255px;">
                <asp:TextBox ID="txtDriverName" runat="server" Width="123px"></asp:TextBox>
             </div>
             <div style="position:fixed; top: 395px; left: 674px; right: 590px;">
                 Trailer
                 Plate Number</div>
             <div style="width: 125px; position:fixed; top: 395px; left: 496px; right: 792px;">
                 Plate Number</div>
             <div style="width: 125px; position:fixed; top: 393px; left: 339px; right: 949px;">
                 Place Issued</div>
             <div style="width: 125px; position:fixed; top: 393px; left: 174px; right: 1114px;">
                 License Number
             </div>
             <div style="width: 131px; position:fixed; top: 424px; left: 336px; right: 946px;">
                <asp:TextBox ID="txtPlaceIssued" runat="server" Width="123px"></asp:TextBox>
             </div>
             
             <div style="width: 131px; position:fixed; top: 424px; left: 175px; right: 1107px;">
                <asp:TextBox ID="txtLicenseNumber" runat="server" Width="123px"></asp:TextBox>
             </div>
             
            <div style="width: 131px; position:fixed; top: 425px; left: 498px; right: 784px;">
                <asp:TextBox ID="txtPlateNumber" runat="server" Width="123px"></asp:TextBox>
             </div>
             <div style="width: 131px; position:fixed; top: 427px; left: 673px; right: 609px;">
                <asp:TextBox ID="txtTrailerNumber" runat="server" Width="123px"></asp:TextBox>
             </div>
             
        </fieldset>
  </div>
  

 
        
   
   <div id="DriverInformation" 
        style="position:fixed; top: 354px; left: 14px; height: 120px;">
        <fieldset style="width: 915px; height: 117px">
            <legend style="width: 135px; margin-top: 0px;" >Driver Information</legend>
            <div style="width: 90px; position:fixed; top: 232px; left: 24px; right: 1299px;">VoucherNo :</div>
            <div style="width: 131px; position:fixed; top: 232px; left: 131px; right: 1151px;"><asp:TextBox ID="txtVoucherNo" runat="server" Width="123px"></asp:TextBox></div>
            <div style="width: 127px; position:fixed; top: 233px; left: 326px; right: 960px;">Certificate Number:</div>
            <div style="position: fixed; top: 230px; left: 466px; width: 132px; ">
                       <asp:TextBox ID="txtCertificateNo" runat="server"></asp:TextBox>
            </div>
            <div style="width: 90px; position:fixed; top: 265px; left: 26px; right: 1297px;">Coffee Type :</div>
            <div style="width: 132px; position:fixed; top: 262px; left: 131px; height: 22px;">
                   <asp:DropDownList ID="DropDownList9" runat="server" Width="124px"></asp:DropDownList>
            </div>
            <div style="width: 90px; position:fixed; top: 262px; left: 325px; right: 998px;">Specific Area :</div>
            <div style="position: fixed; top: 258px; left: 465px; width: 130px; ">
                       <asp:TextBox ID="txtSpecificArea" runat="server"></asp:TextBox>
            </div>
            <div style="width: 90px; position:fixed; top: 299px; left: 25px; right: 1298px;"> No. Plomps:</div>
            <div style="position: fixed; top: 295px; left: 130px; width: 130px; ">
                       <asp:TextBox ID="txtNoPlomps" runat="server"></asp:TextBox>
            </div>
            <div style="width: 131px; position:fixed; top: 295px; left: 323px; right: 959px;">Trailer No. Plomps:</div>
            <div style="position: fixed; top: 290px; left: 467px; width: 131px; right: 815px;">
                       <asp:TextBox ID="txtTrailerNoPlomps" runat="server" Width="129px"></asp:TextBox>
            </div>
        </fieldset>
  </div>
  <div id="command0" 
        
        
        
        style="position:fixed; top: 502px; left: 789px; width: 102px; height: 44px;">
      <asp:Button ID="btnClear" runat="server" Text="Clear" Width="95px" />
  </div>
</asp:Content>
