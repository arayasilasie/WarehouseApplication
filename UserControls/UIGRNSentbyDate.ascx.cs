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
    public partial class UIGRNSentbyDate : System.Web.UI.UserControl,ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack == false)
            {
                Page.DataBind();
                this.txtArrivalDate.Text = DateTime.Now.ToShortDateString();
            }
            ShowGRNSent();
            
        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            //throw new NotImplementedException();
            return null;
        }
        private void ShowGRNSent()
        {
            int TotalCount = 0;
            DateTime dateSent = DateTime.Parse( this.txtArrivalDate.Text);
            GRNSentBLL obj = new GRNSentBLL();
            List<GRNSentBLL> list = obj.getCount(dateSent, out TotalCount );
            gvDetail.DataSource = list;
            gvDetail.DataBind();
            this.lblTotal.Text = TotalCount.ToString();
           
        }

        #endregion

        protected void txtArrivalDate_TextChanged(object sender, EventArgs e)
        {
            //ShowGRNSent();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ShowGRNSent();
        }
    }
}