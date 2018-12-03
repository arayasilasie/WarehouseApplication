using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Data;
using System.Text;
namespace WarehouseApplication
{
    public partial class ListInboxNew2 : System.Web.UI.Page
    {
        int StepID
        {
            get
            {
                return (int) ViewState["StepID"];
            }
        }
        int TypeID;

        DataTable dtbl
        {
            get
            {
                if (ViewState["dtbl"] != null)
                    return (DataTable)(ViewState["dtbl"]);
                else
                    return null;
            }
        }

        bool? firsTime
        {
            get
            {
                if (ViewState["firsTime"] != null)
                    return (bool)ViewState["firsTime"];
                else
                    return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            BindGRNCreationGridview();
            BindGINCreationGridview();
            BindPSACreationGridview();
        }

        public void BindGRNCreationGridview()
        {
            grvGRNCreation.DataSource = InboxModel.GetInboxList(new Guid(Session["CurrentWarehouse"].ToString()), 1);
            grvGRNCreation.DataBind();
        }

        public void BindPSACreationGridview()
        {
            grvPSA.DataSource = InboxModel.GetInboxList(new Guid(Session["CurrentWarehouse"].ToString()), 5);
            grvPSA.DataBind();
        }

        public void BindGINCreationGridview()
        {
            grvGINCreation.DataSource = InboxModel.GetInboxListForGIN(new Guid(Session["CurrentWarehouse"].ToString()));
            grvGINCreation.DataBind();
        }

        /// <summary>
        ///  get the url according to stepID for GIN and PSA
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public string GetUrl(object stepID)
        {
            int Id = int.Parse(stepID.ToString());
            string url;
            if (Id == 12)
                url = "~/GINApprove.aspx";
            else if (Id == 13)
                url = "~/ManagerGINApprove.aspx";

            else if (Id == 14)
                url = "~/PSAApprove.aspx";
            else
                url = "~/ManagerPSAApprove.aspx";
            return url;

        }

        /// <summary>
        /// get the url according to stepID for GRN
        /// </summary>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public string GetGRNUrl(object stepID)
        {
            int Id = int.Parse(stepID.ToString());
            string url = "";

            return url;

        }

        protected void grvGRNCreation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int stepID=int.Parse(((Label)e.Row.FindControl("lblStepID")).Text);                
                HyperLink hl = (HyperLink)e.Row.FindControl("HyperLink1");
                if (hl != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    int stepID = int.Parse(drv["StepID"].ToString());
                    string Task = drv["Task"].ToString();
                    int TypeID = int.Parse(drv["TypeID"].ToString());

                    if (stepID < 9)
                    {
                        hl.NavigateUrl = "~/ListInboxDetailNew.aspx?StepID=" + stepID + "&Task=" + Task + "&TypeID=" + TypeID;
                    }
                    else if (stepID == 9)
                    {
                        hl.NavigateUrl = "~/GRNApproval.aspx?StepID=" + stepID + "&Task=" + Task;
                    }

                    else
                    {
                        hl.NavigateUrl = "~/GRNApprovalSupervisor.aspx?StepID=" + stepID + "&Task=" + Task;
                    }


                }
            }
        }

        protected void grvGRNCreation_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDetail.Visible = true;
            Session["Task"] = grvGRNCreation.SelectedDataKey[1].ToString();
            lblDetail.Text = Session["Task"].ToString();
            ViewState.Add("firsTime", true);
            ViewState["StepID"] = int.Parse(grvGRNCreation.SelectedDataKey[0].ToString());
            TypeID = 1;
            if (StepID == 2)
            {
                Response.Redirect("~/GetSampleTicketNew.aspx");
            }
            else if (StepID < 9)
            {
                BindDetailGridview();

            }
            else if (StepID == 9)
            {
                Session["StepID"] = StepID;
                Session["TypeID"] = TypeID;
                Response.Redirect("~/GRNApproval.aspx");
            }
            else
            {
                Session["StepID"] = StepID;
                Session["TypeID"] = TypeID;
                Response.Redirect("~/GRNApprovalSupervisor.aspx");
            }

        }

        public void BindDetailGridview()
        {
            if (((bool)ViewState["firsTime"]))
            {
                DataTable dt = InboxModel.GetInboxDetailList(new Guid(Session["CurrentWarehouse"].ToString()), StepID, TypeID);
                ViewState.Add("dtbl", dt);
            }
            grvDetail.DataSource = dtbl;
            grvDetail.DataBind();

            ////// Make Close Button Available for Arrival Only
            ////if (StepID == 1)
            ////    grvDetail.Columns[4].Visible = true;
            ////else
            ////    grvDetail.Columns[4].Visible = false;
        }

        protected void grvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ViewState.Add("firsTime", false);
            grvDetail.PageIndex = e.NewPageIndex;
            BindDetailGridview();

        }
     

        protected void grvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string taskName = "";
            taskName = Session["Task"].ToString();
            if (e.CommandName == "Detail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.grvDetail.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblTrackingNo");
                    if (id != null)
                    {

                        Response.Redirect("PageSwictherNew.aspx?TranNo=" + id.Text + "&task=" + taskName);
                    }
                }
            }

            if (e.CommandName == "Close")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.grvDetail.Rows[index];
                if (rw != null)
                {
                    // in arrival WorkflowID and Id are the same but in case of sampling and Grading Id is sampling or grading Id
                    Label WorkflowID = (Label)rw.FindControl("lblWorkflowID");
                    Label Id = (Label)rw.FindControl("lblID");
                    if (WorkflowID != null)
                    {

                        Response.Redirect("CloseInboxDetail.aspx?StepID=" + StepID + "&WorkflowID=" + WorkflowID.Text + "&Id=" + Id.Text);
                    }
                }
            }
        }

  


    }
}

