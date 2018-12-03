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
    public partial class UIGradingFactors : System.Web.UI.UserControl, ISecurityConfiguration
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GradingFactorTypeBLL objGFT = new GradingFactorTypeBLL();
                List<GradingFactorTypeBLL> list = objGFT.GetActiveGradingfactorType();
                if (list != null)
                {
                    this.cboGradingFactorType.AppendDataBoundItems = true;
                    this.cboSearchGradingFactorTypeId.AppendDataBoundItems = true;
                    foreach (GradingFactorTypeBLL i in list)
                    {

                        this.cboGradingFactorType.Items.Add(new ListItem(i.GradingFactorTypeName, i.Id.ToString()));
                        this.cboSearchGradingFactorTypeId.Items.Add(new ListItem(i.GradingFactorTypeName, i.Id.ToString()));
                    }
                    this.cboGradingFactorType.AppendDataBoundItems = false;
                    this.cboSearchGradingFactorTypeId.AppendDataBoundItems = false;
                }
            }
            else
            {
                //GridViewRow rw = this.gvGF.Rows[this.gvGF.EditIndex];
                //DropDownList dr = (DropDownList)rw.FindControl("cboEditGradingFactorType");
            }
               
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            Guid GradingFactorTypeId = Guid.Empty;
            GradingFactorTypeId = new Guid(this.cboGradingFactorType.SelectedValue);
            GradingFactorBLL obj = new GradingFactorBLL();
            obj.Id = Guid.NewGuid();
            obj.GradingFactorName = this.txtGradingFactorName.Text;
            obj.Status = (GradingFactorStatus)(int.Parse(this.cboStatus.SelectedValue.ToString()));
            obj.GradingTypeId = GradingFactorTypeId;
            if (obj.Save() == true)
            {
                this.lblMessage.Text = "Data updated Successfully";
                
            }
            else
            {
                this.lblMessage.Text = "Unable to inser Data please try Again";
            }
            
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
            
        }

        protected void gvGF_PageIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void gvGF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<GradingFactorBLL> list = (List<GradingFactorBLL>) ViewState["listGF"];
            this.gvGF.PageIndex = e.NewPageIndex;
            this.gvGF.DataSource = list;
            this.gvGF.DataBind();
        }

        protected void gvGF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnAdd")
            {
                cmd = new List<object>();
                cmd.Add(this.btnSave);
            }
            else if (name == "cmdEdit")
            {
                foreach (TableRow row in this.gvGF.Rows)
                {
                    if (row.FindControl("cmdEdit") != null)
                    {
                        cmd = new List<object>();
                        cmd.Add(row.FindControl("cmdEdit"));
                    }
                }
            }

            return cmd;
            
        }

        #endregion

        protected void gvGF_RowEditing(object sender, GridViewEditEventArgs e)
        {



            int editindex = -1;
            editindex = e.NewEditIndex;
            gvGF.EditIndex = editindex;
            GridViewRow rw = this.gvGF.Rows[editindex];
            if (editindex != -1)
            {
                List<GradingFactorBLL> list = (List<GradingFactorBLL>)ViewState["listGF"];
                this.gvGF.DataSource = list;
                gvGF.DataBind();
            }
        }

        protected void gvGF_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            Search();

        }

        protected void gvGF_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Code to Update.
            //get the old Value 
            // get the new value 
            int editindex = -1;
            editindex = this.gvGF.EditIndex;
            GridViewRow rw = this.gvGF.Rows[editindex];
            if (editindex != -1)
            {
                //Update the Grading Factor Name 
                GradingFactorBLL obj = new GradingFactorBLL();
                Label lblId = (Label)rw.FindControl("lblEditId");
                TextBox txtGFN = (TextBox)rw.FindControl("txtGradingFactorName");
                DropDownList dr = (DropDownList)rw.FindControl("cboEditGradingFactorType");
                DropDownList drStatus = (DropDownList)rw.FindControl("cboEditStatus");

                if (lblId != null)
                {
                    obj.Id = new Guid(lblId.Text);
                }
                if (txtGFN != null)
                {
                    obj.GradingFactorName = txtGFN.Text;
                }
                if (dr != null)
                {
                    obj.GradingTypeId = new Guid(dr.SelectedValue.ToString());
                    obj.GradingFactorTypeName = dr.SelectedItem.ToString();
                }
                if (drStatus != null)
                {
                    if (drStatus.SelectedValue != "")
                    {
                        if (drStatus.SelectedValue == "Active")
                        {
                            obj.Status = GradingFactorStatus.Active;
                        }
                        else if (drStatus.SelectedValue == "Cancelled")
                        {
                            obj.Status = GradingFactorStatus.Cancelled;
                        }
                    }
                }
                bool isSaved = false;
                GradingFactorBLL objOld = new GradingFactorBLL();
                objOld = (GradingFactorBLL)ViewState["OldGF"];
                isSaved = obj.Update(objOld);
                if (isSaved == true)
                {
                    this.lblMessage.Text = "Data updated Successfully.";
                    this.gvGF.EditIndex = -1;
                    gvGF.DataBind();
                    Search();
                }
                else
                {
                    this.lblMessage.Text = "Unable to update Data.";
                }





            }


        }

        protected void gvGF_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGF.EditIndex = -1;
            List<GradingFactorBLL> list = (List<GradingFactorBLL>)ViewState["listGF"];
            this.gvGF.DataSource = list;
            gvGF.DataBind();

        }

        protected void gvGF_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (this.gvGF.EditIndex == e.Row.RowIndex && this.gvGF.EditIndex != -1)
            {
                GridViewRow rw = e.Row;
                //Update the Grading Factor Name 
                GradingFactorBLL objOld = new GradingFactorBLL();
                Label lblId = (Label)rw.FindControl("lblEditId");
                TextBox txtGFN = (TextBox)rw.FindControl("txtGradingFactorName");
                DropDownList dr = (DropDownList)rw.FindControl("cboEditGradingFactorType");
                DropDownList drStatus = (DropDownList)rw.FindControl("cboEditStatus");
                Label lblGTId = (Label)rw.FindControl("GradingTypeId");

                if (lblId != null)
                {
                    objOld.Id = new Guid(lblId.Text);
                }
                if (txtGFN != null)
                {
                    objOld.GradingFactorName = txtGFN.Text;
                }
                if (dr != null)
                {
                    objOld.GradingTypeId = new Guid(lblGTId.Text);
                    objOld.GradingFactorTypeName = dr.SelectedItem.ToString();
                }
                if (drStatus != null)
                {
                    if (drStatus.SelectedValue != "")
                    {
                        if (drStatus.SelectedValue == "Active")
                        {
                            objOld.Status = GradingFactorStatus.Active;
                        }
                        else if (drStatus.SelectedValue == "Cancelled")
                        {
                            objOld.Status = GradingFactorStatus.Cancelled;
                        }
                    }
                }
                ViewState["OldGF"] = objOld;





                GradingFactorTypeBLL objGFT = new GradingFactorTypeBLL();
                List<GradingFactorTypeBLL> listGFT = objGFT.GetActiveGradingfactorType();
                if (dr != null)
                {
                    if (listGFT != null)
                    {
                        dr.Items.Clear();
                        dr.AppendDataBoundItems = true;
                        foreach (GradingFactorTypeBLL i in listGFT)
                        {
                            dr.Items.Add(new ListItem(i.GradingFactorTypeName, i.Id.ToString()));
                        }
                        dr.AppendDataBoundItems = false;

                    }
                }
                if (lblGTId != null)
                {
                    dr.SelectedValue = lblGTId.Text;
                }

            }
        }
        private void Search()
        {
            List<GradingFactorBLL> list = null;
            this.gvGF.DataSource = list;
            this.DataBind();
            GradingFactorBLL obj = new GradingFactorBLL();
            Nullable<Guid> GFTID = null;
            if (this.cboSearchGradingFactorTypeId.SelectedValue != "")
            {
                GFTID = new Guid(this.cboSearchGradingFactorTypeId.SelectedValue);
            }
            Nullable<GradingFactorStatus> Status = null;
            if (this.cboSearchStatus.SelectedValue != "")
            {
                Status = (GradingFactorStatus)(int.Parse(this.cboSearchStatus.SelectedValue));
            }
            list = obj.Search(this.txtSearchGradingFactorName.Text, GFTID, Status);
            if (list != null)
            {
                ViewState["listGF"] = list;
                this.gvGF.DataSource = list;
                this.DataBind();
            }
            
        }
    }
}