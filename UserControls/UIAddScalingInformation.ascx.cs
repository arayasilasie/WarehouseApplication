using System;
using System.Collections;
using System.Collections.Generic;
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
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddScalingInformation : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {



           
            if (IsPostBack != true)
            {
               
                this.Page.DataBind();
                this.chkTruckScaled.Checked = true;
                if (Session["AddScalingGradingCode"] != null)
                {
                    LoadPage();
                    LoadTruckInformation();
                    loadGradingInfo();
                    
                }
                this.cboWeigher.Items.Add(new ListItem("Please select Inveroty Controller", ""));
                List<UserBLL> list = UserRightBLL.GetUsersWithRight("Weigher");
                if (list != null)
                {
                    if (list.Count > 0)
                    {

                        foreach (UserBLL u in list)
                        {
                            this.cboWeigher.Items.Add(new ListItem(u.FullName, u.UserId.ToString()));
                        }
                    }
                }
            }
            if (this.gvDriverInformation.SelectedIndex == -1)
            {
                this.btnAdd.Enabled = false;
            }
            
        }
        protected void cboGradingCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadTruckInformation();
        }
        private void LoadPage()
        {
            //TODO Warehouse from security manager.
            Guid warehouseId = UserBLL.GetCurrentWarehouse();

            List<GradingResultBLL> list = null;
            GradingResultBLL obj = new GradingResultBLL();
            try
            {
                list = obj.GetAcceptedresultsPendingScaling(warehouseId);
               
                if (list != null || list.Count > 0)
                {
                    int flag = -1;
                    this.cboGradingCode.Items.Add(new ListItem("Please Select Code", ""));
                    this.cboGradingCode.AppendDataBoundItems = true;
                    foreach (GradingResultBLL o in list)
                    {
                        this.cboGradingCode.Items.Add(new ListItem(o.GradingCode.ToString(), o.CommodityDepositRequestId.ToString() + "/" + o.ID.ToString()));
                        if (o.GradingCode == Session["AddScalingGradingCode"].ToString().Trim())
                        {
                            flag = 0;
                            this.cboGradingCode.SelectedValue = o.CommodityDepositRequestId.ToString() + "/" + o.ID.ToString();
                        }
                    }
                    if (flag == -1)
                    {
                        this.btnAdd.Enabled = false;
                    }
                    this.cboGradingCode.AppendDataBoundItems = false;
                }
                else
                {
                    this.lblmsg.Text = "There are No records pending Scaling.";
                }

            }
            catch 
            {
                this.lblmsg.Text = "An error occured,please check the data entered  and try agian.If the error persists, contact IT Support.";
            }
        }
        private void LoadTruckInformation()
        {
            if(this.cboGradingCode.SelectedValue.ToString() == "")
            {
                this.lblmsg.Text = "Please select Tracking No.";
                this.cboGradingCode.SelectedIndex  = -1;
                return ;
            }
            string str = this.cboGradingCode.SelectedValue.ToString();
            string[] temp = new string[2];
            temp = str.Split('/');
            if(temp[0] == "" || temp[0] == null)
            {
                this.lblmsg.Text = "Please try agian.";
                this.cboGradingCode.SelectedIndex  = -1;
                return ;
            }
            Guid ReceivigRequestId  ;
            try
            {
              ReceivigRequestId = new Guid(temp[0].ToString());
            }
            catch
            {
                this.lblmsg.Text = "Please try agian.";
                this.cboGradingCode.SelectedIndex  = -1;
                return ;
            }
            try
            {
                this.hfGradingResultId.Value = temp[1].ToString();
            }
            catch 
            {
                this.lblmsg.Text = "An error occured, please try agian.";
                this.cboGradingCode.SelectedIndex = -1;
                return;
            }
            this.hfReceivigRequestId.Value = ReceivigRequestId.ToString();
            List<DriverInformationBLL> list = new List<DriverInformationBLL>();
            DriverInformationBLL objTruck = new DriverInformationBLL();
            list = objTruck.GetActiveDriverInformationByReceivigRequestId(ReceivigRequestId);
            this.gvDriverInformation.DataSource = list;
            this.gvDriverInformation.DataBind();
            //Get detail gradingResult
            GradingResultBLL objGradingResult = new GradingResultBLL();
            objGradingResult = objGradingResult.GetGradingResultById(new Guid(temp[1].ToString()));
            UnloadingBLL objUnloading = new UnloadingBLL();
            objUnloading = objUnloading.GetApprovedUnloadingByGradingResultId(objGradingResult.ID);
            if (objUnloading != null)
            {
                this.lblDateUnloaded.Text = objUnloading.DateDeposited.ToShortDateString();
                this.cmpSampGen.ValueToCompare =  objUnloading.DateDeposited.ToShortDateString();
            }
            this.hfTrackingNo.Value = objGradingResult.TrackingNo;



            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScalingBLL obj = new ScalingBLL();
            obj.ReceivigRequestId = new Guid(this.hfReceivigRequestId.Value.ToString());
            obj.DriverInformationId = new Guid(this.hfDriverInformationId.Value.ToString());
            obj.GradingResultId = new Guid(this.hfGradingResultId.Value.ToString());
            obj.ScaleTicketNumber = this.txtScaleTicket.Text;
            obj.DateWeighed = Convert.ToDateTime((this.txtDateWeighed.Text + " " + this.txtTime.Text) );
            obj.GrossWeightWithTruck = float.Parse(this.txtGrossTruckWeight.Text);
            obj.TruckWeight = float.Parse(this.txtTruckWeight.Text);
            obj.GrossWeight = obj.GrossWeightWithTruck - obj.TruckWeight;
            obj.Status = ScalingStatus.New;
            obj.Remark = this.txtRemark.Text;
            obj.WeigherId = new Guid( this.cboWeigher.SelectedValue.ToString());
            if (string.IsNullOrEmpty(this.hfTrackingNo.Value.ToString()) == true)
            {
                this.lblmsg.Text = "Unable to get Tracking Number.";
                return;
            }
            if (obj.GrossWeightWithTruck < obj.TruckWeight )
            {
                this.lblmsg.Text = "Gross Truck Weight should be greater than Truck Weight.";
                return;
            }
            obj.TrackingNo = this.hfTrackingNo.Value.ToString();
            try
            {
                if (obj.GetCount() == 0)
                {
                    if (obj.Add() == true)
                    {
                        this.lblmsg.Text = "Data updated Successfully.";
                        Response.Redirect("ListInbox.aspx");
                    }
                    else
                    {
                        this.lblmsg.Text = "Please try again.";
                        return;
                    }
                }
                else
                {
                    this.lblmsg.Text = "An active record already exists.";
                    return;
                }
            }
            catch
            {
                this.lblmsg.Text = "Unable To add this record please try again.";
            }
            finally
            {
                Clear();
            }
        }
        protected void gvDriverInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = this.gvDriverInformation.SelectedRow;
            // get selected Id.
            Nullable<Guid> IdDriver = null;
            Label id = row.FindControl("lblId") as Label;
            if(id == null)
            {
                this.lblmsg.Text = "Please Try Again";
                return;
            }
            if(DataValidationBLL.isGUID(id.Text,out IdDriver)== false)
            {
                this.lblmsg.Text = "Please Try Again";
                return;
            }
            this.hfDriverInformationId.Value = IdDriver.ToString();
            Label Truck = row.FindControl("lblPlateNumber") as Label;
            if (Truck != null)
            {
                this.lblPlateNo.Text = Truck.Text;
            }
            Label Trailer = row.FindControl("lblTrailerPlateNumber") as Label;
            if (Trailer != null)
            {
                this.lblPlateNo.Text = Trailer.Text;
            }
            this.btnAdd.Enabled = true;
            this.pnAdd.Visible = true;


        }
        protected void gvDriverInformation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            this.gvDriverInformation.SelectedIndex = e.NewSelectedIndex;         
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            this.gvDriverInformation.SelectedIndex = -1;
            this.lblPlateNo.Text = "";
            this.lblTrailerNo.Text = "";
            this.hfDriverInformationId.Value = "";
            this.txtScaleTicket.Text = "";
            this.txtDateWeighed.Text = "";
            this.txtGrossTruckWeight.Text = "";
            this.txtTruckWeight.Text = "";
            this.lblmsg.Text = "";
            //this.txtTimeArrival.Text = "";
            LoadTruckInformation();
        }
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            if (name == "btnAdd")
            {
                
                cmd.Add(this.btnAdd);
                return cmd;
            }
            else if (name == "cboGradingCode")
            {
                cmd.Add(this.cboGradingCode);
                return cmd;
            }
            else
            {
                return null;
            }
        }

        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.btnAdd.UseSubmitBehavior = false;

            btnAdd.OnClientClick = "javascript:";

            if (btnAdd.CausesValidation)
            {
                btnAdd.OnClientClick += " if ( Page_ClientValidate('" + btnAdd.ValidationGroup + "') ){ ";
                btnAdd.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }
        }

        protected void chkTruckScaled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTruckScaled.Checked == true)
            {
            }
            else
            {
            }
        }
        private void loadGradingInfo()
        {
            //Displaay the commodity Grade.
            GradingResultBLL objGR = new GradingResultBLL();
            objGR = objGR.GetGradingResultById(new Guid(this.hfGradingResultId.Value.ToString()));
            if (objGR != null)
            {
                CommodityGradeBLL oCG = CommodityGradeBLL.GetCommodityGrade(objGR.CommodityGradeId);
                ViewState["CommId"] = oCG.CommodityId.ToString();
                this.lblCG.Text = oCG.GradeName;
                if (oCG.CommodityId == (Guid)Utility.GetCommodityId("Coffee"))
                {
                    this.lblUnit.Text = "(KG)";
                    this.lblUnit1.Text = "(KG)";
                }
                else
                {
                    this.lblUnit.Text = "(Qtls)";
                    this.lblUnit1.Text = "(Qtls)";
                }
            }
        }

       
    }
}