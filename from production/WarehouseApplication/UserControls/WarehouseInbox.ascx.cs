using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication.UserControls
{
    public partial class WarehouseInbox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserBLL.CurrentUser == null)
            {
                Response.Redirect("portal.ecx.com.et?CMD=logoff");
                return;
            }
            if (IsPostBack != true)
            {





                List<InboxContent> listTaskNameWithCount = null;
                listTaskNameWithCount = InboxContent.GetListForInbox();
                List<InBoxList> list = new List<InBoxList>();
                InboxRowGrid os = new InboxRowGrid();
                //GRN


                XMLHelper oGRNXML = new XMLHelper("InboxGRNQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oGRNXML.GetGrantedInBoxItems());
                this.gvInbox.DataSource = list;
                this.gvInbox.DataBind();

                //Grade Dispute 
                XMLHelper oGDXML = new XMLHelper("InboxGDQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oGDXML.GetGrantedInBoxItems());
                this.gvGradeDispute.DataSource = list;
                this.gvGradeDispute.DataBind();


                //GIN
                XMLHelper oGINXML = new XMLHelper("InboxGINQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oGINXML.GetGrantedInBoxItems());
                this.gvGIn.DataSource = list;
                this.gvGIn.DataBind();


                //ReSampling
                XMLHelper oInboxRCRSQueueXML = new XMLHelper("InboxRCRSQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oInboxRCRSQueueXML.GetGrantedInBoxItems());
                this.gvReSampling.DataSource = list;
                this.gvReSampling.DataBind();

                //Edit Approved GRN 
                XMLHelper oInboxApprovedGRNEditRequestXML = new XMLHelper("InboxApprovedGRNEditRequest.xml");
                list = os.GetInbox(listTaskNameWithCount, oInboxApprovedGRNEditRequestXML.GetGrantedInBoxItems());
                this.gvRequestToEditApprovedGRN.DataSource = list;
                this.gvRequestToEditApprovedGRN.DataBind();

                //Cancel Approved GRN 
                XMLHelper oInboxCancelApprovedGRNQueueXML = new XMLHelper("InboxCancelApprovedGRNQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oInboxCancelApprovedGRNQueueXML.GetGrantedInBoxItems());
                this.gvCancelApprovedGRN.DataSource = list;
                this.gvCancelApprovedGRN.DataBind();


                //GIN Edit
                XMLHelper oInboxGINEditQueueXML = new XMLHelper("InboxGINEditQueue.xml");
                list = os.GetInbox(listTaskNameWithCount, oInboxGINEditQueueXML.GetGrantedInBoxItems());
                this.gvGINEdit.DataSource = list;
                this.gvGINEdit.DataBind();
            }
        }

        //GRN Creation
        protected void gvInbox_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvInbox.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxGRNQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGRNQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }
                }
            }
        }

        protected void gvGradeDispute_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGradeDispute.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxGDQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGDQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }
                }
            }
        }
        protected void gvGIN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvGIN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGIn.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id.Text.Trim() == "Data assistant GIN Approval")
                    {
                        Session["Inboxpath"] = "InboxGINQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGINQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("GINApprove.aspx");
                    }
                    if (id.Text.Trim() == "Supervisor GIN Approval")
                    {
                        Session["Inboxpath"] = "InboxGINQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGINQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ManagerGINApprove.aspx");
                    }
                }
            }
        }
        protected void gvGINEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvGINEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvGINEdit.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxGINEditQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGINQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }

                }
            }
        }

        protected void gvReSampling_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvReSampling.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxRCRSQueue.xml";
                        //Session["Inboxpath"] = "http://localhost:9282/WarehouseApplication/InboxGINQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }

                }
            }
        }

        protected void gvInbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvRequestToEditApprovedGRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvRequestToEditApprovedGRN.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxApprovedGRNEditRequest.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }

                }
            }
        }

        protected void gvRequestToCancelApprovedGRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = this.gvCancelApprovedGRN.Rows[index];
                if (rw != null)
                {
                    Label id = (Label)rw.FindControl("lblId");
                    if (id != null)
                    {
                        Session["Inboxpath"] = "InboxCancelApprovedGRNQueue.xml";
                        Session["WarehouseInboxItemName"] = id.Text;
                        Response.Redirect("ListInboxDetail.aspx");
                    }

                }
            }
        }

        protected void gvCancelApprovedGRN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }
}