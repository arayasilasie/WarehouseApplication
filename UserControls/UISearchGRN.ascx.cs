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
using System.Text;

namespace WarehouseApplication.UserControls
{
    public partial class UISearchGRN : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            this.lblMsg.Text = "";
            string GRN = "";
            string TrackingNo = "";
            Nullable<Guid> ClientId = null;
            Nullable<Guid> CommodityId = null;
            Nullable<Guid> CommodityClassId = null;
            Nullable<Guid> CommodityGradeId = null;
            Nullable<GRNStatus> Status = null;
            Nullable<DateTime> From = null;
            Nullable<DateTime> To = null;

            GRN = this.txtGRN.Text;
            TrackingNo = this.txtTrackingNo.Text;
            try
            {
                ClientId = new Guid(this.ClientSelector1.ClientGUID.Value);
            }
            catch
            {
                ClientId = null;
            }
            try
            {
                Status = (GRNStatus)int.Parse(this.cboStatus.SelectedValue.ToString());

            }
            catch
            {
                Status = null;
            }

            try
            {
                From = DateTime.Parse(this.txtFrom.Text);
            }
            catch
            {
                From = null;
            }
            try
            {
                To = DateTime.Parse(this.txtTo.Text);
            }
            catch
            {
                To = null;
            }
            // Assign values.
            if (this.txtGRN.Text != "")
            {
                GRN = this.txtGRN.Text;
            }
            GRNListBLL o = new GRNListBLL();
            List<GRNListBLL> lst = new List<GRNListBLL>();
            try
            {
                lst = o.Search(GRN, TrackingNo, ClientId, CommodityId, CommodityClassId, CommodityGradeId, Status, From, To);
                if (lst != null)
                {
                    if (lst.Count > 500)
                    {
                        this.lblMsg.Text = "Please provide more search critera";
                        lst = null;
                        return;
                    }
                }
            }
            catch (NULLSearchParameterException exn)
            {
                this.lblMsg.Text = "Please provide search parameter.";
                return;
            }
            catch (ClientInformationException)
            {
                this.lblMsg.Text = "Client Information error.";
                return;
            }

            //Bind the data 
            Session["GRNSearchList"] = lst;
            this.gvGRN.DataSource = lst;
            this.gvGRN.DataBind();

        }

        protected void gvGRN_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvGRN_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvGRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            // Label id = (Label)row.FindControl("lblGRN");
            int index = Convert.ToInt32(e.CommandArgument);
           
            GridViewRow rw = this.gvGRN.Rows[index];
           
            if (e.CommandName == "View")
            {


                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["GRNID"] = id.Text;
                        Response.Redirect("ViewGRN.aspx");
                    }

                }
            }
            //Update GRN No 
            if( e.CommandName == "cmdGRNNoUpdate")
            {
                Label id = (Label)rw.FindControl("lblId");
                if (id != null)
                {
                    //Session["GRNIDUpdateGRNNo"] = id.Text;
                    //Response.Redirect("UpdateGRNNumber.aspx");
                }
            }
            else if (e.CommandName == "cmdPrint")
            {
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["GRNIDPrint"] = id.Text;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script>");
                        sb.Append("window.open('rptGRN.aspx");
                        sb.Append("', '', 'toolbar=0');");
                        sb.Append("</scri");
                        sb.Append("pt>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowReport",
                            sb.ToString(), false);


                    }

                }
            }
            else if (e.CommandName == "Edit")
            {

                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    Label lblStatus = (Label)rw.FindControl("lblStatus");
                    if (lblStatus != null)
                    {
                        if (lblStatus.Text.ToUpper() != "NEW" && lblStatus.Text.ToUpper() != "OpenForEdit".ToUpper())
                        {
                            Session["GRNIDRequestCD"] = id.Text;
                            Response.Redirect("AddApprovedGRNEditRequest.aspx");
                        }
                        else if (lblStatus.Text.ToUpper() == "OpenForEdit".ToUpper())
                        {
                            Session["ReCreateGRNID"] = id.Text;
                            Response.Redirect("ReCreateGRN.aspx");
                        }
                        else if (lblStatus.Text.ToUpper() == "NEW")
                        {
                            Session["ReCreateGRNID"] = id.Text;
                            GRNBLL objGRN = new GRNBLL();
                            objGRN = objGRN.GetbyGRN_Number(new Guid(id.Text));
                            Session["ReCreateGRNTrackingNo"] = objGRN.TrackingNo;
                            Response.Redirect("ReCreateGRN.aspx");
                        }
                        else
                        {
                            Session["GRNID"] = id.Text;
                            Response.Redirect("ViewGRN.aspx");
                        }
                    }

                }
            }
            else if (e.CommandName == "cmdCancel")
            {
                if (rw != null)
                {

                    Label id = (Label)rw.FindControl("lblId");
                    Label lblStatus = (Label)rw.FindControl("lblStatus");
                    if (lblStatus != null)
                    {
                        if (lblStatus.Text.ToUpper() == "NEW" || lblStatus.Text.ToUpper() == "ClientAccepted".ToUpper() || lblStatus.Text.ToUpper() == "ClientRejected".ToUpper())
                        {
                            Session["GRNID"] = id.Text;
                            Response.Redirect("ViewGRN.aspx");
                        }
                        else if (lblStatus.Text.ToUpper() == "ManagerApproved".ToUpper())
                        {
                            Session["ApprovedGRNCancelationID"] = id.Text;
                            Response.Redirect("RequestApprovedGRNCancelation.aspx");
                        }
                    }
                }
            }

        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = null;
            if (name == "btnSearch")
            {
                cmd = new List<object>();
                cmd.Add(btnSearch);
            }
            else if (name == "cmdEdit")
            {
                foreach (TableRow row in this.gvGRN.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdEdit"));
                }
            }
            else if (name == "cmdView")
            {
                foreach (TableRow row in this.gvGRN.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdView"));
                }
            }
            else if (name == "cmdPrint")
            {
                foreach (TableRow row in this.gvGRN.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdPrint"));
                }
            }
            else if (name == "cmdCancel")
            {
                foreach (TableRow row in this.gvGRN.Rows)
                {
                    cmd = new List<object>();
                    cmd.Add(row.FindControl("cmdCancel"));
                }
            }
            return cmd;

        }

        #endregion

        protected void gvGRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvGRN.PageIndex = e.NewPageIndex;
        }

        protected void gvGRN_PageIndexChanged(object sender, EventArgs e)
        {
            if (Session["GRNSearchList"] != null)
            {
                List<GRNListBLL> lst = (List<GRNListBLL>)Session["GRNSearchList"];
                this.gvGRN.DataSource = lst;
                this.gvGRN.DataBind();
            }
        }
    }
}
