using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;

namespace WarehouseApplication
{
    public partial class EditGRN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LblConfirmation.Visible = false;
                LblConfirm.Visible = false;
                btnSave.Enabled = true;
                btnClear.Enabled = false;
            }

            btnSave.Enabled = true;
        }

        public void BindWHShed(Guid LIC)
        {
            DDLShed.Items.Clear();

            DDLShed.DataSource = GRNApprovalModel.GetWHShedsbyLIC(new Guid(Session["CurrentWarehouse"].ToString()), LIC);
            DDLShed.DataTextField = "ShedNumber";
            DDLShed.DataValueField = "ID";
            DDLShed.Items.Add(new ListItem("Select Shed . . .", null));
            DDLShed.DataBind();
        }

        public void BindLIC(int status)
        {
            ddLIC.Items.Clear();

            ddLIC.DataSource = GRNApprovalModel.GetAllLICs(new Guid(Session["CurrentWarehouse"].ToString()));
            ddLIC.DataTextField = "Name";
            ddLIC.DataValueField = "ID";
            ddLIC.Items.Add(new ListItem("Select LIC", null));
            ddLIC.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // comment bel bi
            SearchGRN();
        }

        private void SearchGRN()
        {
            DataTable dt = new DataTable();
            int found = 0;
            dt = getExpiredWHRsonTruck(new Guid(Session["CurrentWarehouse"].ToString()));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["grnnumber"].ToString() == txtGRNNo.Text)
                {
                    
                    btnClear.Enabled = true;
                    btnSave.Enabled = true;

                    TxtGRNNum.Text = dt.Rows[i]["grnnumber"].ToString();
                    TxtCLIENT.Text = dt.Rows[i]["IDNo"].ToString();
                    TxtSymbol.Text = dt.Rows[i]["Symbol"].ToString();

                    found = 1;
                    BindLIC(2);
                }

            }

            if(found==0)
            {
                LblConfirm.Text = "The GRNNo you entered is not found among the Receipts which are ONTRUCK and EXPIRED !!";
                LblConfirm.Visible = true;
            }
            if (dt.Rows.Count == 0)
            {
                LblConfirm.Text = "There is no GRN in the current Warehouse which is ONTRUCK and EXPIRED !!";
                LblConfirm.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!DDLShed.SelectedItem.Text.Equals("Select Shed . . ."))
            {
                LblConfirm.Text = "";
                LblConfirm.Visible = false;
                try
                {
                    UpdateGRNs();
                    UpdateReceipt();
                    LblConfirmation.Visible = true;
                    LblConfirm.Visible = false;
                }
                catch (Exception ex)
                {
                    LblConfirm.Text = "unable to update GRN/WHR";
                }
                Clear();
               
            }
            else
            {
                LblConfirm.Text = "Please select the Shed . . .";
                LblConfirm.Visible = true;
            }
        }

        public void UpdateGRNs()
        {
            string GRNNo = txtGRNNo.Text;
            Guid LICName2Id = new Guid(ddLIC.SelectedValue.ToString());
            string LICName2= ddLIC.SelectedItem.ToString();
            string quad = TxtQuadrant.Text; ;
            string Shed = DDLShed.SelectedItem.ToString();
            //GRN_BL objGrnNew = new GRN_BL();
            //GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRNNo);
            GRNApprovalModel.EditGRNToWarehouse(GRNNo, Shed, LICName2Id, quad);

            //WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
        }

        public void UpdateReceipt()
        {
            string GRNNo = txtGRNNo.Text;
            Guid LICName2 = new Guid(ddLIC.SelectedValue.ToString());
            //Guid Shed = new Guid();
            string Shed = DDLShed.SelectedItem.ToString();
            GRN_BL objGrnNew = new GRN_BL();
            GRNBLL objGrnOld = objGrnNew.GetWarehouseReciptByGRNNumber(GRNNo);
            WarehouseRecieptBLL objWR = new WarehouseRecieptBLL(objGrnOld);
            objWR.EditWHRtoWarehouse(GRNNo, Shed);
        }

        public DataTable getExpiredWHRsonTruck(Guid warehouseid)
        {
            DataTable dt = new DataTable();
            WarehouseRecieptBLL whrbll = new WarehouseRecieptBLL();
            dt = whrbll.getexpiredWHRsOnTruck(warehouseid);
            return dt;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {

            TxtGRNNum.Text = "";
            TxtCLIENT.Text = "";
            TxtSymbol.Text = "";
            ddLIC.SelectedItem.Text.Equals("Select LIC");
            DDLShed.SelectedItem.Equals("Select Shed . . .");
        }

        protected void ddLIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWHShed(new Guid(ddLIC.SelectedValue.ToString()));
        }

        protected void DDLShed_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}