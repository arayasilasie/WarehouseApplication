using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class UIAddTrucksForSampling : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                this.btnPrint.Visible = false;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "";
            this.gvDetail.DataSource = null; ;
            this.gvDetail.DataBind();

            int NumberOfTrucks = 0;
            if (this.cboNoTrucks.SelectedValue != "")
            {

                NumberOfTrucks = int.Parse(this.cboNoTrucks.SelectedValue);
            }
            if (NumberOfTrucks == 0)
            {
                this.lblMessage.Text = "Please Select Number of Trucks";
                return;
            }
            try
            {
                List<TrucksForSamplingBLL> list = TrucksForSamplingBLL.GetRandomSample(UserBLL.GetCurrentWarehouse(), NumberOfTrucks);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        this.gvDetail.DataSource = list;
                        this.gvDetail.DataBind();
                        this.btnPrint.Visible = true;
                    }
                    else
                    {
                        this.lblMessage.Text = "There are no enteries Trucks Pending Sampling.";
                    }
                }
                else
                {
                    this.lblMessage.Text = "There are no enteries Trucks Pending Sampling.";
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }

        }
    }
}