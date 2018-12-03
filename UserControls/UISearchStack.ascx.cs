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
    public partial class UISearchStack : System.Web.UI.UserControl , ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                Session["StackSearch"] = "";
                LoadControls();
            }
        }

        protected void gvStack_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Nullable<Guid> ShedId = null;
            Nullable<Guid> CommodityGradeId = null;
            String StackNumber = "";

            try
            {
                ShedId = new Guid(this.cboShed.SelectedValue.ToString());
            }
            catch
            {
               ShedId = null;
            }
            try
            {
                CommodityGradeId = new Guid(this.cboCommodityGrade.SelectedValue.ToString());
            }
            catch
            {
                CommodityGradeId = null;
            }
            StackNumber = this.txtStackNumber.Text;

            StackBLL objstack = new StackBLL();
            List<StackBLL> list = new List<StackBLL>();
            Session["StackSearch"] = list;
            list = objstack.Search(ShedId, CommodityGradeId, StackNumber);
            this.gvStack.DataSource = list;

            this.gvStack.DataBind();
        }
        private void LoadControls()
        {
            // Loading Warehoouse.

            List<WarehouseBLL> listWarehouse = new List<WarehouseBLL>();
            try
            {
                listWarehouse = WarehouseBLL.GetAllActiveWarehouse();
            }
            catch (Exception ex)
            {
                this.lblmsg.Text = ex.Message;
                return;
            }
            this.cboWarehouse.Items.Add(new ListItem("Please Select warehouse", ""));
            if (listWarehouse.Count > 0)
            {
                this.cboWarehouse.AppendDataBoundItems = true;
                foreach (WarehouseBLL ow in listWarehouse)
                {
                    this.cboWarehouse.Items.Add(new ListItem(ow.WarehouseName.ToString(), ow.WarehouseId.ToString()));
                }
                this.cboWarehouse.AppendDataBoundItems = false;

            }



        }

        protected void cboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboShed.Items.Clear();
            // load Stack based on the selected index.
            if (this.cboWarehouse.SelectedValue == "")
            {
                return;
            }
            else
            {
                Guid WarehouseId = Guid.Empty;
                ShedBLL objShed = new ShedBLL();

                try
                {
                    WarehouseId = new Guid(this.cboWarehouse.SelectedValue.ToString());
                }
                catch
                {
                    this.lblmsg.Text = "Please select warehouse and try again";
                    return;
                }

                List<ShedBLL> list = new List<ShedBLL>();
                list = objShed.GetActiveShedByWarehouseId(WarehouseId);
                this.cboShed.Items.Add(new ListItem("Please Select Shed", ""));
                if (list.Count > 0)
                {
                    foreach (ShedBLL oshed in list)
                    {
                        this.cboShed.Items.Add(new ListItem(oshed.ShedNumber, oshed.Id.ToString()));
                    }
                }
            }

        }

        protected void gvStack_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
          
        }

        protected void gvStack_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<ShedBLL> list = new List<ShedBLL>();
            list =(List<ShedBLL>) Session["StackSearch"];
            this.gvStack.PageIndex = e.NewPageIndex;
            this.gvStack.DataSource = list;
            this.DataBind();


        }



        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSave")
            {
                cmd = new List<object>();
                cmd.Add(btnSave);
               
            }
            else if(name == "cmdEdit")
            {
                foreach (TableRow row in this.gvStack.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            return cmd;
        }

        #endregion
    }
}