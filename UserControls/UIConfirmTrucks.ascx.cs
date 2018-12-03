using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WarehouseApplication.UserControls
{
    public partial class UIConfirmTrucks : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                BindTrucksPendingConfirmation();
            }
        }

        protected void btnConform_Click(object sender, EventArgs e)
        {
            //validation
            CheckBox chk;
            DropDownList cboRem;
            Label lblId, lblerr;
            TextBox lblReMark;

            List<TrucksForSamplingBLL> list = new List<TrucksForSamplingBLL>();
            foreach (GridViewRow rowItem in gvDetail.Rows)
            {

                chk = (CheckBox)(rowItem.FindControl("chkConfirmed"));
                cboRem = (DropDownList)(rowItem.FindControl("cboRemark"));
                lblReMark = (TextBox)(rowItem.FindControl("txtRemark"));
                lblerr = (Label)(rowItem.FindControl("lblerr"));
                //if (chk.Checked != true)
                //{
                //    if (cboRem.SelectedValue == "Ready For Sampling")
                //    {
                //        rowItem.CssClass = "GridSelectedRow";
                //        this.lblMessage.Text = "Please select Reason not selecting the Truck";
                //        return;
                //    }
                //}
                //else
                //{
                //    if (cboRem.SelectedValue != "Ready For Sampling")
                //    {
                //        rowItem.CssClass = "GridSelectedRow";
                //        this.lblMessage.Text = "Please select Reason not selecting the Truck";
                //        return;
                //    }
                //}
                //everthig is fine so update all
                if (chk.Checked == true)
                {
                    lblId = (Label)(rowItem.FindControl("lblId"));
                    if (lblId != null)
                    {
                        TrucksForSamplingBLL obj = new TrucksForSamplingBLL();
                        obj.Id = new Guid(lblId.Text);
                        if (cboRem.SelectedValue.ToString().Trim() == "1")
                        {
                            obj.Status = TrucksForSamplingStatus.Confirmed;
                        }
                        else if (cboRem.SelectedValue.ToString().Trim() == "2")
                        {
                            obj.Status = TrucksForSamplingStatus.TruckMissingOnSamplingQueue;
                        }
                        else
                        {
                            obj.Status = TrucksForSamplingStatus.Other;
                        }
                        if (obj.Status != TrucksForSamplingStatus.Confirmed && obj.Status != TrucksForSamplingStatus.TruckMissingOnSamplingQueue)
                        {
                            if (lblReMark == null)
                            {

                                lblerr.Visible = true;
                                return;
                            }
                            else
                            {
                                if (lblReMark.Text == "")
                                {
                                    lblerr.Visible = true;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            lblerr.Visible = false;
                        }
                        Label lblTrackingNo = (Label)(rowItem.FindControl("lblTrackingNo"));
                        obj.TrackingNo = lblTrackingNo.Text;
                        if (lblReMark != null)
                        {

                            obj.Remark = lblReMark.Text;
                        }


                        list.Add(obj);
                    }
                }
            }////////////////checked
            // Update the changes
            bool isSaved = false;
            TrucksForSamplingBLL objUpdate = new TrucksForSamplingBLL();
            isSaved = objUpdate.Confirm(list);
            if (isSaved == true)
            {
                this.lblMessage.Text = "Data updated Successfully";
                this.btnConform.Enabled = false;
                BindTrucksPendingConfirmation();
                this.UIAbsentTrucks1.LoadData();
                return;
            }
            else
            {
                this.lblMessage.Text = "Unable to Update Data please try again";
                return;
            }


        }

        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> list = null;
            if (name == "btnConform")
            {
                list = new List<object>();
                list.Add(this.btnConform);
            }
            return list;

        }

        #endregion

        protected void gvDetail_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }
        private void BindTrucksPendingConfirmation()
        {
            List<TrucksForSamplingBLL> list = new List<TrucksForSamplingBLL>();
            TrucksForSamplingBLL obj = new TrucksForSamplingBLL();
            list = obj.GetTruckspendingConfirmation();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    this.gvDetail.DataSource = list;
                    this.gvDetail.DataBind();
                    CheckBox chk;
                    foreach (GridViewRow rowItem in gvDetail.Rows)
                    {
                        chk = (CheckBox)(rowItem.FindControl("chkConfirmed"));
                        chk.Checked = true;
                        chk.Enabled = true;
                        chk.Attributes.Add("OnCheckedChanged", "chkCount(this)");
                    }
                }
                else
                {
                    this.btnConform.Enabled = false;
                    this.lblMessage.Text = "There are no Trucks Pending Confirmation";
                    this.gvDetail.DataSource = null;
                    this.gvDetail.DataBind();
                }
            }
            else
            {
                this.btnConform.Enabled = false;
                this.lblMessage.Text = "There are no Trucks Pending Confirmation";
                this.gvDetail.DataSource = null;
                this.gvDetail.DataBind();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
    }
}