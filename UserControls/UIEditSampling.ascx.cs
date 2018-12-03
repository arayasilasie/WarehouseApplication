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
    public partial class UIEditSampling : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                this.Page.DataBind();
                if (Session["SamplingId"] != null)
                {
                    Guid Id = Guid.Empty;
                    Id = new Guid(Session["SamplingId"].ToString());
                    Session["SamplingId"] = null;
                    if (Id != Guid.Empty)
                    {
                        SamplingBLL obj = new SamplingBLL();
                        obj = obj.GetSampleDetail(Id);
                        obj.Id = Id;
                        if (obj != null)
                        {
                            this.lblSampleCode.Text = obj.SampleCode;
                            this.txtDateCodeGenrated.Text = obj.GeneratedTimeStamp.ToShortDateString();
                            this.txtTimeArrival.Text = obj.GeneratedTimeStamp.ToShortTimeString();
                            ViewState["SamplingId"] = Id;
                            CommodityDepositeRequestBLL objCDR = new CommodityDepositeRequestBLL();
                            objCDR = objCDR.GetCommodityDepositeDetailById(obj.ReceivigRequestId);
                            if (objCDR != null)
                            {
                                lblArrivalDate.Text = objCDR.DateTimeRecived.ToShortDateString();
                            }

                        }
                    }
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //update sampling Date 
            Guid SamplingId = Guid.Empty;
            SamplingId = new Guid(ViewState["SamplingId"].ToString());
            DateTime DateCoded;
            try
            {
                DateCoded = DateTime.Parse(this.txtDateCodeGenrated.Text + " " + this.txtTimeArrival.Text);
            }
            catch (ArgumentNullException)
            {
                this.lblMessage.Text = "please Check that Date sampled is in correct format";
                return;
            }
            catch (FormatException)
            {
                this.lblMessage.Text = "please Check that Date sampled is in correct format";
                return;
            }
            SamplingBLL obj = new SamplingBLL();
            obj.Id = SamplingId;
            obj.GeneratedTimeStamp = DateCoded;
            if (obj.UpdateDateCoded() == true)
            {
                this.lblMessage.Text = "Data Updated Successuly";
            }
            else
            {
                this.lblMessage.Text = "Unable to Update data please try again";
            }

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();
            cmd.Add(this.btnNext);
            return cmd;
        }

        #endregion
    }
}