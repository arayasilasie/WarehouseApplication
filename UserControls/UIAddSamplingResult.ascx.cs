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
using System.Text;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;


namespace WarehouseApplication.UserControls
{
    public partial class SamplingResult : System.Web.UI.UserControl, ISecurityConfiguration
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.DataBind();
            BindGrid();
            this.chkisSupervisor.Checked = true;
            this.chkisSupervisor.Enabled = false;
            if (IsPostBack != true)
            {
                //18-01-2012
                //ViewState["SamplingResultList"] = null;
                Session["SamplingResultList"] = null;
                string TranNo;
                try
                {
                    TranNo = Request.QueryString["TranNo"];
                }
                catch
                {
                    TranNo = "";
                }
                //Check if exists a sampling Result with the appropriate TrackingNo.
                if (LoadManagerApprovalData(TranNo) == false)
                {
                    //TODO GET SamplingId by Tracking No.
                    BindData(TranNo);
                    if (Session["SamplingReasultAddId"] != null)
                    {
                        this.cboSampleCode.SelectedValue = Session["SamplingReasultAddId"].ToString();
                        this.cboSampleCode.Enabled = false;
                        SamplingCodechanged();
                        Session["SamplingReasultAddId"] = null;
                    }
                    //SamplingCodechanged();
                    pnlManagerApproval.Visible = false;
                }
                else
                {
                    btnAdd.Enabled = false;
                    btnSave.Enabled = false;
                }

            }
        }
        protected void cboSampleCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.cboSampler.Items.Clear();
            //Guid Id = new Guid(this.cboSampleCode.SelectedValue.ToString());
            //SamplerBLL obj = new SamplerBLL();
            //List<SamplerBLL> list = new List<SamplerBLL>();
            //list = obj.GetSamplerBySamplingId(Id);
            //if (list != null)
            //{
            //    this.cboSampler.Items.Add(new ListItem("Please Select Sampler.", ""));
            //    this.cboSampler.AppendDataBoundItems = true;
            //    foreach (SamplerBLL o in list)
            //    {
            //        this.cboSampler.Items.Add(new ListItem(o., o.SamplerId.ToString()));

            //    }
            //}


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.lblMsg.Text = "";
            bool isSaved = false;

            Guid SamplingId = Guid.Empty;
            try
            {
                SamplingId = new Guid(this.cboSampleCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "an error has occured please try agian.if the error persists Contact the administrator.";
                BindGrid();
                return;
            }


            SamplingResultBLL objSave = new SamplingResultBLL();
            int noSep = -1;
            noSep = int.Parse(this.txtNumberOfSeparations.Text);


            //18-01-2012
            //List<SamplingResultBLL> list = (List<SamplingResultBLL>)ViewState["SamplingResultList"];
            List<SamplingResultBLL> list = (List<SamplingResultBLL>)Session["SamplingResultList"];
            if (list != null)
            {
                if (noSep != list.Count)
                {
                    this.lblMsg.Text = "Number of Sampling Result Details should exactly be equal to the number of separations.";
                    BindGrid();
                    return;
                }
            }
            else
            {
                if (noSep > 0)
                {
                    this.lblMsg.Text = "Number of Sampling Result Details should exactly be equal to the number of separations.";
                    BindGrid();
                    return;
                }
            }

            DateTime ResultReceivedDateTime;
            if (DateTime.TryParse(this.txtArrivalDate.Text + " " + txtTimeArrival.Text, out ResultReceivedDateTime) == false)
            {
                this.lblMsg.Text = "Please enter a valid Date for Result Received date/time";
                BindGrid();
                return;
            }
            objSave.ResultReceivedDateTime = ResultReceivedDateTime;
            objSave.IsPlompOk = this.chkPlomps.Checked;
            isSaved = objSave.Add(list, SamplingId);
            if (isSaved == true)
            {

                this.lblMsg.Text = "Sampling Result Successfully Created.";
                btnNext.Enabled = true;
                Clear();
                try
                {
                    this.gvSamplingResultDetail.Columns[2].Visible = false;
                    this.gvSamplingResultDetail.Columns[3].Visible = true;
                    foreach (TableRow row in this.gvSamplingResultDetail.Rows)
                    {
                        LinkButton btn = (LinkButton)(row.FindControl("cmdPrint"));
                        btn.Visible = true;
                    }
                    foreach (TableRow row in this.gvSamplingResultDetail.Rows)
                    {
                        LinkButton btn = (LinkButton)(row.FindControl("cmdRemove"));
                        btn.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                BindGrid();
            }
            else
            {
                this.lblMsg.Text = "An error has occured please try again";
                string TranNo;
                try
                {
                    TranNo = Request.QueryString["TranNo"];
                }
                catch
                {
                    TranNo = "";
                }
                BindData(TranNo);
            }


        }
        public void BindData(string TranNo)
        {
            this.cboSampler.Items.Clear();

            Guid WarehouseId = UserBLL.GetCurrentWarehouse();
            List<SamplingBLL> list = new List<SamplingBLL>();
            SamplingBLL obj = new SamplingBLL();
            //18-01-2012
            list = obj.GetSamplesPenndingResult(WarehouseId, TranNo);
            this.cboSampleCode.Items.Add(new ListItem("Please Select Sample Code.", ""));
            this.cboSampler.Items.Clear();
            this.cboSampleCode.AppendDataBoundItems = true;
            this.cboSampleCode.DataSource = list;
            this.cboSampleCode.DataTextField = "SampleCode";
            this.cboSampleCode.DataValueField = "Id";
            this.cboSampleCode.DataBind();

        }
        public void Clear()
        {
            //this.cboSampleCode.SelectedValue = "";
            //this.cboSampler.SelectedValue = "";
            //this.txtNumberofbags.Text = "";
            //this.txtNumberOfSeparations.Text = "1";
            //this.txtSamplerCommment.Text = "";
            //this.txtRemark.Text = "";
            //this.chkisSupervisor.Checked = false;
            //this.cboSampleCode.AppendDataBoundItems = false;
            this.btnSave.Enabled = false;

        }
        public void SamplingCodechanged()
        {
            Guid id = Guid.Empty;
            this.cboSampler.Items.Clear();
            try
            {
                id = new Guid(this.cboSampleCode.SelectedValue.ToString());
            }
            catch
            {
                this.lblMsg.Text = "An error has occured please try again.If the error persists contact the administrator";
                return;
            }
            SamplerBLL obj = new SamplerBLL();
            List<SamplerBLL> list = new List<SamplerBLL>();
            SamplingBLL objSampling = new SamplingBLL();
            objSampling = objSampling.GetSampleDetail(id);
            if (objSampling != null)
            {
                lblSampleGenratedDateTime.Text = objSampling.GeneratedTimeStamp.ToShortDateString();
                //this.cmpSampGen.ValueToCompare = objSampling.GeneratedTimeStamp.ToShortDateString();
            }
            else
            {
                this.lblMsg.Text = "An error has occured please try again.If the error persists contact the administrator";
                return;
            }


            list = obj.GetSamplerBySamplingId(id);
            if (list != null)
            {
                this.cboSampler.Items.Add(new ListItem("Please Select Sampler.", ""));
                this.cboSampler.AppendDataBoundItems = true;
                foreach (SamplerBLL o in list)
                {
                    this.cboSampler.Items.Add(new ListItem(o.SamplerName, o.SamplerId.ToString()));
                    this.cboSampler.SelectedValue = o.SamplerId.ToString();

                }
            }
            this.cboSampler.Enabled = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.lblMsg.Text = "";
            List<SamplingResultBLL> list = new List<SamplingResultBLL>();


            string SampleCode = this.cboSampleCode.SelectedItem.Value.ToString();
            //18-01-2012
            //if (ViewState["SamplingResultList"] == null || ViewState["SamplingResultList"].ToString() == "")
            if (Session["SamplingResultList"] == null || Session["SamplingResultList"].ToString() == "")
            {


                SamplingResultBLL objSave = new SamplingResultBLL();
                objSave.SamplingId = new Guid(this.cboSampleCode.SelectedValue.ToString());
                objSave.EmployeeId = new Guid(this.cboSampler.SelectedValue.ToString());
                objSave.NumberOfBags = Convert.ToInt32(this.txtNumberofbags.Text);
                objSave.NumberOfSeparations = Convert.ToInt32(this.txtNumberOfSeparations.Text);
                objSave.SamplerComments = this.txtSamplerCommment.Text;
                objSave.IsSupervisor = this.chkisSupervisor.Checked;
                objSave.Remark = this.txtRemark.Text;
                objSave.Status = SamplingResultStatus.New;
                objSave.CreatedBy = UserBLL.GetCurrentUser();
                objSave.SamplingResultCode = this.cboSampleCode.SelectedItem.Text.ToString();
                objSave.IsPlompOk = this.chkPlomps.Checked;
                list.Add(objSave);
                Session["SamplingResultList"] = list;
                ViewState["MaxCode"] = 0;
                BindGrid();
            }
            else
            {
                //18-01-2012
                //list = (List<SamplingResultBLL>)ViewState["SamplingResultList"];
                list = (List<SamplingResultBLL>)Session["SamplingResultList"];
                if (list.Count >= Convert.ToInt32(this.txtNumberOfSeparations.Text))
                {
                    this.lblMsg.Text = "You can not add sampling Results more than the Number of Separations";
                    BindGrid();
                    return;
                }

                if (ViewState["MaxCode"] == null || ViewState["MaxCode"].ToString() == "")
                {
                    this.lblMsg.Text = "Please try again!";
                    BindGrid();
                }
                else
                {
                    //18-01-2012
                    //list = (List<SamplingResultBLL>)ViewState["SamplingResultList"];
                    list = (List<SamplingResultBLL>)Session["SamplingResultList"];
                    Nullable<int> temp = null;
                    try
                    {
                        temp = int.Parse(ViewState["MaxCode"].ToString());
                    }
                    catch
                    {
                        this.lblMsg.Text = "Please try again";
                        BindGrid();
                        return;
                    }
                    if (temp == null)
                    {
                        this.lblMsg.Text = "Please try again";
                        BindGrid();
                        return;
                    }
                    else
                    {
                        temp += 1;
                    }
                    SamplingResultBLL objSave = new SamplingResultBLL();
                    objSave.SamplingId = new Guid(this.cboSampleCode.SelectedValue.ToString());
                    objSave.EmployeeId = new Guid(this.cboSampler.SelectedValue.ToString());
                    try
                    {
                        objSave.NumberOfBags = Convert.ToInt32(this.txtNumberofbags.Text);
                    }
                    catch
                    {
                        this.lblMsg.Text = "Please check Number of Bags";
                        BindGrid();
                        return;
                    }
                    try
                    {
                        objSave.NumberOfSeparations = Convert.ToInt32(this.txtNumberOfSeparations.Text);
                    }
                    catch
                    {
                        this.lblMsg.Text = "Please check Numer of Separtions";
                        BindGrid();
                        return;
                    }
                    objSave.SamplerComments = this.txtSamplerCommment.Text;
                    objSave.IsSupervisor = this.chkisSupervisor.Checked;
                    objSave.Remark = this.txtRemark.Text;
                    objSave.Status = SamplingResultStatus.New;
                    objSave.CreatedBy = UserBLL.GetCurrentUser();
                    objSave.SamplingResultCode = this.cboSampleCode.SelectedItem.Text.ToString() + "-" + temp.ToString();
                    list.Add(objSave);
                    //18-01-2012
                    //ViewState["SamplingResultList"] = list;
                    Session["SamplingResultList"] = list;
                    ViewState["MaxCode"] = temp;
                }
                BindGrid();
            }
            this.txtNumberofbags.Text = "";
        }
        private void BindGrid()
        {
            //18-01-2012
            //List<SamplingResultBLL> list = (List<SamplingResultBLL>)ViewState["SamplingResultList"];
            List<SamplingResultBLL> list = (List<SamplingResultBLL>)Session["SamplingResultList"];
            if (list != null)
            {
                this.gvSamplingResultDetail.DataSource = list;
                this.gvSamplingResultDetail.DataBind();
            }
        }
        protected void gvSamplingResultDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdRemove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //18-01-2012
                //List<SamplingResultBLL> list = (List<SamplingResultBLL>)ViewState["SamplingResultList"];
                List<SamplingResultBLL> list = (List<SamplingResultBLL>)Session["SamplingResultList"];
                list.RemoveAt(index);
                //18-01-2012
                //ViewState["SamplingResultList"] = list;
                Session["SamplingResultList"] = list;
                if (list.Count == 0)
                {

                    ViewState["MaxCode"] = "0";
                }
                BindGrid();
            }
            else if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvSamplingResultDetail.Rows[index];
                Label lblCode = (Label)row.FindControl("lblSamplingResultCode");
                Label lblId = (Label)row.FindControl("lblId");
                if (lblId != null)
                {
                    if (lblId.Text != "")
                    {
                        //LoadManagerApprovalData(new Guid(lblId.Text));
                    }
                }

            }
            else if (e.CommandName == "Print")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvSamplingResultDetail.Rows[index];
                Label lblCode = (Label)row.FindControl("lblSamplingResultCode");
                if (lblCode != null)
                {
                    if (lblCode.Text != "")
                    {

                        Session["SamplingResultCode"] = lblCode.Text;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script>");
                        sb.Append("window.open('ReportSamplingResult.aspx");
                        sb.Append("', '', 'toolbar=0');");
                        sb.Append("</scri");
                        sb.Append("pt>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowReport",
                            sb.ToString(), false);
                    }
                }
            }
        }
        protected void gvSamplingResultDetail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListInbox.aspx");
        }
        #region ISecurityConfiguration Members

        public List<object> GetSecuredResource(string scope, string name)
        {
            List<object> cmd = new List<object>();

            if (name == "btnSave")
            {
                cmd.Add(this.btnSave);
                return cmd;
            }
            else if (name == "cboSampleCode")
            {
                cmd.Add(this.cboSampleCode);
                return cmd;
            }
            else if (name == "btnNext")
            {
                cmd.Add(this.btnNext);
                return cmd;
            }
            else if (name == "cmdRemove")
            {
                foreach (TableRow row in this.gvSamplingResultDetail.Rows)
                {
                    cmd.Add(row.FindControl("cmdRemove"));
                }

            }
            else if (name == "pnlManagerApproval")
            {
                cmd.Add(this.pnlManagerApproval);
                return cmd;
            }
            else if (name == "cmdPrint")
            {
                foreach (TableRow row in this.gvSamplingResultDetail.Rows)
                {
                    cmd.Add(row.FindControl("cmdPrint"));
                }
            }

            return cmd;
        }

        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.btnSave.UseSubmitBehavior = false;

            btnSave.OnClientClick = "javascript:";

            if (btnSave.CausesValidation)
            {
                btnSave.OnClientClick += " if ( Page_ClientValidate('" + btnSave.ValidationGroup + "') ){ ";
                btnSave.OnClientClick += "this.disabled=true; this.value='Please Wait...'; }";
            }
        }
        protected void cboSamplingResultCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool LoadManagerApprovalData(string TrackingNo)
        {
            bool isFound = false;
            SamplingResultBLL obj = new SamplingResultBLL();
            obj = obj.GetByTrackingNo(TrackingNo);
            if (obj != null)
            {
                isFound = true;
                this.cboSampleCode.Items.Clear();
                this.cboSampleCode.Items.Add(new ListItem(obj.SamplingResultCode, obj.SamplingId.ToString()));
                this.cboSampleCode.SelectedValue = obj.SamplingId.ToString();
                this.hfSamplingResultId.Value = obj.Id.ToString();
                SamplingCodechanged();
                this.lblSamplingResultCode.Text = obj.SamplingResultCode;
                this.lblResultTrackingNo.Text = obj.TrackingNo;
                this.cboResultStatus.SelectedValue = ((int)obj.Status).ToString();
                ViewState["oldResultMA"] = obj;

                //string[] RightArr = new string[] { "WHSupervisor" };
                //List<string> hasRight = UserBLL.HasRoles(UserBLL.GetCurrentUser(), RightArr);
                //if (hasRight.Count == 1)
                //{
                //    this.pnlManagerApproval.Visible = true;
                //}
                //else
                //{
                //    this.pnlManagerApproval.Visible = false;
                //}

            }
            return isFound;
        }
        protected void btnManagerApprove_Click(object sender, EventArgs e)
        {
            // Update the Record - status - Approved.
            // Make the WF Proceed.
            if (btnManagerApprove.Text == "Update")
            {
                if (string.IsNullOrEmpty(this.hfSamplingResultId.Value.ToString()) == true)
                {
                    this.lblMsg.Text = "Unable to get Sampling Result Id.Please Try Again";
                    return;
                }
                if (ViewState["oldResultMA"] == null)
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persists contact administrators.";
                    return;
                }
                SamplingResultBLL oldObj = (SamplingResultBLL)ViewState["oldResultMA"];
                if (oldObj == null)
                {
                    this.lblMsg.Text = "An error has occured please try again.If the error persists contact administrators.";
                    return;
                }

                SamplingResultBLL obj = new SamplingResultBLL();
                obj.Id = new Guid(this.hfSamplingResultId.Value.ToString());
                obj.Status = (SamplingResultStatus)int.Parse(this.cboResultStatus.SelectedValue.ToString());
                obj.ManagerApprovalRemark = this.txtManagerApproval.Text;
                obj.TrackingNo = this.lblResultTrackingNo.Text;
                bool isSaved = false;
                isSaved = obj.UpdateManagerApproval(oldObj);
                if (isSaved == true)
                {
                    this.lblMsg.Text = "Data Updated Successfully.";
                    this.btnManagerApprove.Text = "Next";
                    this.btnManagerApprove.Enabled = true;

                }
                else
                {
                    this.lblMsg.Text = "Unable to update data,please try again.";
                }
            }
            else
            {
                Response.Redirect("ListInbox.aspx");
            }

        }
    }
}