using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class SearchPSA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPSA();
            Session["ReportType"] = "PSA";
        }

        public void BindPSA()
        {
            int WHR = 0;
            if (txtWHReceiptNo.Text != "")
                WHR = int.Parse(txtWHReceiptNo.Text);

            DataTable dt = GINBussiness.GINModel.GetPSA(txtGINNo.Text, txtClientId.Text, WHR, new Guid(Session["CurrentWarehouse"].ToString()));
            grvPSA.DataSource = dt;
            grvPSA.DataBind();

            }
        }
    }
