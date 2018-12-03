using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UIEditScaling : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
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
                if (Session["ScalingInfoEdit"].ToString() == "")
                {
                    throw new Exception("The Session has expired");
                }
                else
                {
                    Guid ScalingId = new Guid(Session["ScalingInfoEdit"].ToString());
                    BindData(ScalingId);
                }

            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ScalingBLL obj = new ScalingBLL();
            try
            {
                obj.Id = new Guid(this.hfId.Value.ToString());
            }
            catch
            {
                this.lblMessage.Text = "An Error has ccured please try again. If the error persists Contact the Administrator";
                return;
            }
            obj.ScaleTicketNumber = this.txtScaleTicket.Text;
            try
            {
                obj.DateWeighed = DateTime.Parse(this.txtDateWeighed.Text.ToString());
            }
            catch
            {
                this.lblMessage.Text = "Incorrect Date weighed";
                return;
            }
            try
            {
                obj.GrossWeightWithTruck = float.Parse(this.txtGrossTruckWeight.Text);
            }
            catch
            {
                this.lblMessage.Text = "Incorrect Gross Truck Weight";
                return;
            }
            try
            {
                obj.TruckWeight = float.Parse(this.txtTruckWeight.Text);
            }
            catch
            {
                this.lblMessage.Text = "Incorrect Gross Truck Weight";
                return;
            }
            obj.GrossWeight = obj.GrossWeightWithTruck - obj.TruckWeight;
            try
            {
                obj.Status = (ScalingStatus)int.Parse(this.cboStatus.SelectedValue.ToString());
            }
            catch
            {
                this.lblMessage.Text = "Please Select Status";
                return;
            }
            obj.Remark = this.txtRemark.Text;
            obj.WeigherId = new Guid(this.cboWeigher.SelectedValue.ToString());
            bool isSaved = false;
            isSaved = obj.Update();
            if( isSaved == true)
            {
                this.lblMessage.Text = "Update Sucessfull";
            }
            else
            {
                this.lblMessage.Text = "Unable to update.Please try agin";
            }


        }
        private void BindData(Guid Id)
        {
            ScalingBLL obj = new ScalingBLL();
            obj = obj.GetById(Id);
            if (obj != null)
            {
                if (obj.Id != null)
                {
                    this.hfId.Value = obj.Id.ToString();
                }
                if (obj.ScaleTicketNumber != "")
                {
                    this.txtScaleTicket.Text = obj.ScaleTicketNumber.ToString();
                }
                if (obj.DateWeighed != null)
                {
                    this.txtDateWeighed.Text = obj.DateWeighed.ToString();
                }
                this.cboWeigher.SelectedValue = obj.WeigherId.ToString();
                this.txtGrossTruckWeight.Text = obj.GrossWeightWithTruck.ToString();
               
                this.txtGrossWeoght.Text = obj.GrossWeight.ToString();
                this.txtTruckWeight.Text = obj.TruckWeight.ToString();
             
                this.txtRemark.Text = obj.Remark;
                this.cboStatus.SelectedValue =((int) obj.Status).ToString();
                DriverInformationBLL objDriver = new DriverInformationBLL();
                if( obj.DriverInformationId != null)
                {
                    objDriver = objDriver.GetById(obj.DriverInformationId);
                    if (objDriver != null)
                    {
                        this.lblPlateNo.Text = objDriver.PlateNumber;
                        this.lblTrailerNo.Text = objDriver.TrailerPlateNumber;
                    }
                }
                bool hasGRN = false;
                GRNBLL objGRN = new GRNBLL();
                hasGRN = objGRN.HasGRN("tblScaling", obj.Id);
                if (hasGRN == true)
                {
                    this.btnAdd.Enabled = false;
                    this.lblMessage.Text = "You can't update this Data because a GRN has already been Created for it.";
                }

                


            }
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnAdd")
            {
                cmd = new List<object>();
                cmd.Add(this.btnAdd);
            }
            return cmd;
        }

        #endregion
    }
}