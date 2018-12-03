using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WarehouseApplication.DAL;
using WarehouseApplication.BLL;
using System.Collections.Generic;
using WarehouseApplication.SECManager;


namespace WarehouseApplication.UserControls
{
    public enum   VoucherStatus {
        Active = 1, Cancelled
    };
    public partial class InsertVoucherInformation : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string TranNo = "";
            TranNo = Request.QueryString["TranNo"];
            ViewState["vsTranNo"] = TranNo;

            //btnNext.Enabled = false;
            if (IsPostBack != true)
            {
                Guid Id;
                try
                {
                    //Get Id of the Commodity Deposit request
                    //Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
                    this.CommodityDepositRequestId.Value = Session["CommodityRequestId"].ToString();
                    Id = new Guid(this.CommodityDepositRequestId.Value.ToString());
                    this.lblMessage.Text = "";
                    BindData(Id);
                    LoadPageControls();
                    if (String.IsNullOrEmpty(TranNo) == true)
                    {
                        if (this.btnSave.Text.Trim() == "Save")
                        {
                            this.btnSave.Enabled = true;
                        }

                    }
                    TogglePlomps(Id);
                    Session["CommodityRequestId"] = null;
                }
                catch
                {
                    this.lblMessage.Text = "An error has occured please try again.";
                    return;
                }
                
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            Boolean isSaved = false;
            Guid Id,CommodityDepositeRequest;
            Guid CoffeeId;
            string CertificateNumber, VoucherNo, SpecificArea ;
            int NumberOfBags, NumberOfPlomps, TrailerNumberOfPlomps, Status;
            CoffeeId = new Guid(this.cboCoffeeType.SelectedValue.ToString());
            string TranNo;
            try
            {
                TranNo = Request.QueryString["TranNo"];
            }
            catch
            {
                this.lblMessage.Text = "All Required variables are not set please try again.";
                return;
            }

            NumberOfBags = Convert.ToInt32(this.txtNumberOfBags.Text);
            try
            {
                NumberOfPlomps = Convert.ToInt32(this.txtNoPlomps.Text);
            }
            catch
            {
                NumberOfPlomps = 0;
            }
            try
            {
                TrailerNumberOfPlomps = Convert.ToInt32(this.txtTrailerNoPlomps.Text);
            }
            catch
            {
                TrailerNumberOfPlomps = 0;
            }
            CertificateNumber = this.txtCertificateNo.Text ;
            SpecificArea = this.txtSpecificArea.Text;
            VoucherNo = this.txtVoucherNo.Text ;
            Status = (int)VoucherStatus.Active; // Voucher is entered as Active.
            Guid User = UserBLL.GetCurrentUser();
            VoucherInformationBLL objVoucher = new VoucherInformationBLL();
            try
            {
                if (this.btnSave.Text == "Save")
                {
                    if (this.CommodityDepositRequestId.Value == null)
                    {
                        //TODO Get From Tracking No 
                        this.lblMessage.Text = "Unable To get Commmodity Deposite Request id.Please try again.";
                        return;
                    }
                    try
                    {
                        CommodityDepositeRequest = new Guid(this.CommodityDepositRequestId.Value.ToString());
                    }
                    catch
                    {
                        //TODO Get From Tracking No 
                        this.lblMessage.Text = "Unable To get Commmodity Deposite Request id.Please try again.";
                        return;
                    }
                    try
                    {
                        isSaved = objVoucher.Save(CommodityDepositeRequest, VoucherNo, CoffeeId, SpecificArea, NumberOfBags,
                            NumberOfPlomps, TrailerNumberOfPlomps, CertificateNumber, User, Status, TranNo);
                    }
                    catch( Exception ex)
                    {
                        this.lblMessage.Text = ex.Message;
                        return;
                    }
                    if (isSaved == true)
                    {

                        this.btnSave.Text = "Update";
                        this.lblMessage.Text = "Record Added Successfully";
                        this.btnNext.Enabled = true;
                        BindData(CommodityDepositeRequest);
                        isSaved = false;
                        this.btnNext.Enabled = true;
                        

                    }
                    else
                    {
                         this.lblMessage.Text = "Unable to add the voucher information.";
                         isSaved = false;
                    }

                }
                else if (this.btnSave.Text == "Update")
                {
                    Guid RecReqId = new Guid (this.CommodityDepositRequestId.Value.ToString());
                    Id = new Guid(this.VoucherId.Value.ToString()); 
                    isSaved = false;
                    try
                    {
                        isSaved = objVoucher.Update(Id, RecReqId, VoucherNo, CoffeeId, SpecificArea, NumberOfBags,
                            NumberOfPlomps, TrailerNumberOfPlomps, CertificateNumber, User, Status);
                        if (isSaved == true)
                        {
                            this.lblMessage.Text = "Record Updated Successfully";
                            isSaved = false;
                        }
                        else
                        {
                            this.lblMessage.Text = "Unable to Update the Record.";
                            isSaved = false;
                        }
                    }
                    catch (GRNNotOnUpdateStatus exGRN)
                    {
                        this.lblMessage.Text = exGRN.msg;

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
        }
        private void LoadPageControls()
        {
            this.cboCoffeeType.Items.Clear();
            this.cboCoffeeType.Items.Add(new ListItem("Please Select Coffee Type", ""));
            List<commodityType> list = commodityType.GetAllCoffeeTypes();
            if (list != null)
            {

                foreach (commodityType i in list)
                {
                    this.cboCoffeeType.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                }
            }
        }
        private void BindData(Guid Id)
        {

            
            //Check if the Voucher Infomation is entered ,
            // if entered Display for Update.
            DataSet dsVoucherInformation = new DataSet();
            Voucher objVoucher = new Voucher();
            dsVoucherInformation = objVoucher.getVoucherInformation(Id);
            if (dsVoucherInformation.Tables[0].Rows.Count == 1)
            {
                this.VoucherId.Value = dsVoucherInformation.Tables[0].Rows[0]["VoucherId"].ToString();
                this.txtVoucherNo.Text = dsVoucherInformation.Tables[0].Rows[0]["VoucherNo"].ToString();
                this.txtCertificateNo.Text = dsVoucherInformation.Tables[0].Rows[0]["CertificateNo"].ToString();
                this.cboCoffeeType.SelectedValue = dsVoucherInformation.Tables[0].Rows[0]["CoffeeTypeId"].ToString();
                this.txtSpecificArea.Text = dsVoucherInformation.Tables[0].Rows[0]["SpecificArea"].ToString();
                this.txtNoPlomps.Text = dsVoucherInformation.Tables[0].Rows[0]["NumberOfPlomps"].ToString();
                this.txtTrailerNoPlomps.Text = dsVoucherInformation.Tables[0].Rows[0]["NumberOfPlompsTrailer"].ToString();
                this.btnSave.Text = "Update";
                this.txtNumberOfBags.Text = dsVoucherInformation.Tables[0].Rows[0]["NumberOfBags"].ToString();
            }
            TogglePlomps(Id);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (ViewState["vsTranNo"] != null)
            {
                ECXWF.CMessage msg = WFTransaction.Request(ViewState["vsTranNo"].ToString().Trim());
                if (msg != null)
                {
                    if (msg.Name == "AddVoucherInfo")
                    {
                        WFTransaction.WorkFlowManager(ViewState["vsTranNo"].ToString().Trim(), msg);
                    }
                }
            }
            Response.Redirect("ListInbox.aspx");
        }
        private void TogglePlomps(Guid CommoditydepositRequestId )
        {
            DriverInformationBLL obj = new DriverInformationBLL();
            List<DriverInformationBLL> list = null ;
            list = obj.GetActiveDriverInformationByReceivigRequestId(CommoditydepositRequestId);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    obj = list[0];
                    if (string.IsNullOrEmpty(obj.PlateNumber) == true)
                    {
                        this.txtNoPlomps.Text = "0";
                        this.txtNoPlomps.Enabled = false;
                        this.rfNoPlomps.Enabled = false;

                    }
                    if (string.IsNullOrEmpty(obj.TrailerPlateNumber) == true)
                    {
                        this.txtTrailerNoPlomps.Text = "0";
                        this.txtTrailerNoPlomps.Enabled = false;
                        this.rfvTrailerNoPlomps.Enabled = false;
                    }
                }
            }
        }
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnSave")
            {
                cmd.Add(this.btnSave);
                return cmd;
            }
            else if (name == "btnNext")
            {
                cmd.Add(this.btnNext);
                return cmd;
            }
            return null;
            
        }

        #endregion

        
    }
}