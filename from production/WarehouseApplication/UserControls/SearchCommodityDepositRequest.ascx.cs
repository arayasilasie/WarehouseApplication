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
using System.Data.SqlClient;
using WarehouseApplication.BLL;
using WarehouseApplication.DAL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class SearchCommodityDepositRequest : System.Web.UI.UserControl,ISecurityConfiguration
    {
        public DataSet dsResult;
        public DataSet Result
        {
            get
            {
                return this.dsResult;
            }
            set
            {
                this.dsResult = value;
            }
        }
       
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                CommodityGradeBLL objGrade = new CommodityGradeBLL();
                List<CommodityGradeBLL> list = CommodityGradeBLL.GetAllCommodity();
                this.cboCommodity.Items.Add(new ListItem("Please Select Commodity", ""));
                if (list != null)
                {

                    foreach (CommodityGradeBLL i in list)
                    {
                        this.cboCommodity.Items.Add(new ListItem(i.Commodity ,i.CommodityId.ToString()));
                    }
                }
                dsResult = new DataSet();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            List<CommodityDepositeRequestBLL> list = new List<CommodityDepositeRequestBLL>();
            CommodityDepositeRequestBLL obj = new CommodityDepositeRequestBLL();
            string strTrackingNo = this.txtTrackingNumber.Text;
            string strVoucherNo = this.txtVoucherNo.Text;
            Nullable<Guid> ClientId = null;
            if (this.ClientSelector1.ClientGUID.Value.ToString() != "")
            {
                ClientId = new Guid(this.ClientSelector1.ClientGUID.Value.ToString());
            }
            Nullable<Guid> CommodityId = null;
            if (this.cboCommodity.SelectedValue != "")
            {
                CommodityId = new Guid(this.cboCommodity.SelectedValue);
            }
            Nullable<DateTime> from = null;
            if (this.dtFrom.Text != "")
            {
                from = Convert.ToDateTime(this.dtFrom.Text);
            }
            Nullable<DateTime> to = null;
            if (this.dtTo.Text != "")
            {
                to = Convert.ToDateTime(this.dtTo.Text);
            }
            //Check at least one search parameter is provided.
            if (strTrackingNo == "" && strVoucherNo == "" && ClientId == null && CommodityId == null && from == null && to == null)
            {
                this.lblMsg.Text = "Please provide at least one search Criteria";
                return;
            }

            list = obj.SearchCommodityDeposite(strTrackingNo, strVoucherNo, ClientId, CommodityId, from, to);
            ViewState["list"] = list;
                
       
            if (list != null)
            {
                this.lblMsg.Text = "";
                this.gvDepositeRequetsList.DataSource = list;
                this.gvDepositeRequetsList.DataBind();
                if (list.Count == 0)
                {
                    lblMsg.Text = "No record found";
                }

            }
            else
            {
                lblMsg.Text = "No record found";
            }

        }

        protected void gvDepositeRequetsList_Edit(object sender, GridViewCommandEventArgs e)
        {
            Guid SelectedId =Guid.Empty ;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rw = this.gvDepositeRequetsList.Rows[index];
            if (rw != null)
            {
                Label id = (Label)rw.FindControl("lblId");
                if (id != null)
                {
                    SelectedId = new Guid( id.Text);
                    
                }
            }
            
            if (e.CommandName.CompareTo("cmdDriver") == 0)
            {
                if (rw != null)
                {
                    LinkButton id = (LinkButton)rw.FindControl("cmdEdit");
                    if (id != null)
                    {
                        Session["CommodityRequestId"] = SelectedId;
                        Response.Redirect("AddDriverInformation.aspx?id=" + SelectedId);
                    }
                }
                //determine the Id of the Selected Row
                
                
            }
            else if (e.CommandName.CompareTo("cmdVoucher") == 0)
            {
                //determine the Id of the Selected Row
                Session["CommodityRequestId"] = SelectedId;
                Response.Redirect("AddVoucherInformation.aspx?id=" + SelectedId);
            }
            else if (e.CommandName.CompareTo("cmdEdit") == 0)
            {
                //Response.Redirect("EditCommodityDepositRequest.aspx?id=" + SelectedId);
                Response.Redirect("UpdateArrival.aspx?id=" + SelectedId);
            }
        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
   
            List<object> links = new List<object>();
            string Id = string.Empty;
            if (name == "cmdEdit")
            {
               
                Id = "cmdEdit";
            }
            else if (name == "cmdDriver")
            {
                
                Id = "cmdDriver";
            }
            else if (name == "cmdVoucher")
            {
               
                Id = "cmdEdit";
            }
            else if (name == "btnSearch")
            {

                links.Add(this.btnSearch);
                return links;
            }
          
            foreach (TableRow row in gvDepositeRequetsList.Rows)
            {
                links.Add(row.FindControl(Id));
            }
            return links;
        }

        #endregion

        protected void gvDepositeRequetsList_PageIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvDepositeRequetsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<CommodityDepositeRequestBLL> list = new List<CommodityDepositeRequestBLL>();
            if (ViewState["list"] != null)
            {
                list = (List<CommodityDepositeRequestBLL>)ViewState["list"];
            }
            this.gvDepositeRequetsList.PageIndex =  e.NewPageIndex;
            this.gvDepositeRequetsList.DataSource = list;
            this.gvDepositeRequetsList.DataBind();
        }
    }
}