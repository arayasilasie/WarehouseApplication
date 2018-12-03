using System;
using System.Data;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using System.Web;
using System.Web.UI;
using GradingBussiness;
using System.Collections.Generic;


namespace WarehouseApplication.UserControls
{
    public partial class SearchPageNew : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSearchResultStatus.Text = "";


            if (!IsPostBack)
            {
                HttpContext.Current.Session["ArrivalId_Search"] = null;
                HttpContext.Current.Session["SamplingsId_Search"] = null;
                HttpContext.Current.Session["GradingsId_Search"] = null;
                HttpContext.Current.Session["GradingCode_Search"] = null;
                HttpContext.Current.Session["CodeReceivedDateTime_Search"] = null;
                HttpContext.Current.Session["ClientAcceptanceTimeStamp_Search"] = null;
                HttpContext.Current.Session["ClientRejectTimeStamp_Search"] = null;
                HttpContext.Current.Session["GRNID_Search"] = null;
                HttpContext.Current.Session["TraNo_Search"] = null;
                HttpContext.Current.Session["SampleResultReceivedDate_Search"] = null;
                HttpContext.Current.Session["GRNStatus_Search"] = null;
                HttpContext.Current.Session["SampleCode"] = null;
                DisableLinks();
            }
        }

        private void DisableLinks()
        {
            lnkPrintArrival.Enabled = false;
            lnkPrintArrival.ForeColor = System.Drawing.Color.Gray;
            lnkPrintSamplingTicket.Enabled = false;
            lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
            lnkPrintSampling.Enabled = false;
            lnkPrintSampling.ForeColor = System.Drawing.Color.Gray;
            lnkClientAcceptance.Enabled = false;
            lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
            lnkPrintGrading.Enabled = false;
            lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
            lnkPrintGrn.Enabled = false;
            lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
            lnkLicApproval.Enabled = false;
            lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
            lnkSupervisorApproval.Enabled = false;
            lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            lnkPrintGradingCode.Enabled = false;
            lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;
        }

        private void BindSearchGrid()
        {
            GRN_BL objGrn = new GRN_BL();
            if (cmbSearchCriteria.SelectedValue == "0" || string.IsNullOrEmpty(txtValue.Text))
            {
                lblSearchResultStatus.ForeColor = System.Drawing.Color.Orange;
                lblSearchResultStatus.Text = "Please select searching criteria or searching key!";
                return;
            }
            DataTable dt = objGrn.SearchRecords(cmbSearchCriteria.SelectedValue, txtValue.Text);
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Clear();
                grdSearchResultList.DataSource = dt;
                grdSearchResultList.DataBind();
                lblSearchResultStatus.ForeColor = System.Drawing.Color.Orange;
                lblSearchResultStatus.Text = "No record found with specified searching criteria!";
                return;
            }
            lblSearchResultStatus.ForeColor = System.Drawing.Color.Green;
            int count = dt.Rows.Count;
            lblSearchResultStatus.Text = (count == 1 ? count + " record found" : count + " records found");
            grdSearchResultList.DataSource = dt;
            grdSearchResultList.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DisableLinks();
                BindSearchGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void chkPrintChoice_CheckedChanged(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (GridViewRow grdRow in grdSearchResultList.Rows)
            {
                CheckBox chkItem = (CheckBox)grdRow.FindControl("chkPrintChoice");
                if (chkItem.Checked == true)
                {
                    counter = 1;
                    Session["ArrivalId_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["ArrivalId"].ToString();
                    Session["TrackingNumber_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["TrackingNumber"].ToString();
                    Session["SamplingsId_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["SamplingsID"].ToString();
                    Session["GradingsId_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GradingsId"].ToString();
                    Session["GradingCode_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GradingCode"].ToString();
                    Session["CodeReceivedDateTime_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["CodeReceivedDateTime"].ToString();
                    Session["GradingFactorGroupID_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GradingFactorGroupID"].ToString();
                    Session["GradingResultStatusID_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GradingResultStatusID"].ToString();
                    Session["ClientAcceptanceTimeStamp_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["ClientAcceptanceTimeStamp"].ToString();
                    Session["ClientRejectTimeStamp_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["ClientRejectTimeStamp"].ToString();
                    Session["GRNID_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GRNID"].ToString();
                    Session["GradingTrackingNumber_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["GradingTrackingNumber"].ToString();
                    Session["SampleResultReceivedDate_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["ResultReceivedDateTime"].ToString();
                    Session["WarehouseId"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["WarehouseID"].ToString();
                    Session["ProductionYear"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["ProductionYear"].ToString();
                    Session["GRNStatus_Search"] = grdSearchResultList.DataKeys[grdRow.RowIndex].Values["Status"].ToString();
                    Session["SampleCode"] = ((Label)grdSearchResultList.Rows[grdRow.RowIndex].Cells[0].FindControl("lblSampleCode")).Text;
                    break;
                }
                else
                {
                    DestroySessions();
                }
            }
            if (counter == 1)
                foreach (GridViewRow grdRow in grdSearchResultList.Rows)
                {
                    CheckBox chkItem = (CheckBox)grdRow.FindControl("chkPrintChoice");
                    if (chkItem.Checked == true)
                        chkItem.Enabled = true;
                    else
                        chkItem.Enabled = false;
                }
            else
                foreach (GridViewRow grdRow in grdSearchResultList.Rows)
                {
                    CheckBox chkItem = (CheckBox)grdRow.FindControl("chkPrintChoice");
                    chkItem.Enabled = true;
                }
            if (Session["ArrivalId_Search"] == null)
            {
                lnkPrintArrival.Enabled = false;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = false;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
                return;
            }
            string arrivalId = Session["ArrivalId_Search"].ToString();
            string samplingId = Session["SamplingsId_Search"].ToString();
            string sampleResultRecivedDate = Session["SampleResultReceivedDate_Search"].ToString();
            string gradingId = Session["GradingsId_Search"].ToString();
            //string codeReceivedAtLab = Session["CodeReceivedDateTime_Search"].ToString();
            string cradingFactorGroupID = Session["GradingFactorGroupID_Search"].ToString();
            string gradingResultStatusID = Session["GradingResultStatusID_Search"].ToString();
            string clientAcceptanceTimeStamp = Session["ClientAcceptanceTimeStamp_Search"].ToString();
            string clientRejectTimeStamp = Session["ClientRejectTimeStamp_Search"].ToString();
            string grnId = Session["GRNID_Search"].ToString();
            string approvalStatus = Session["GRNStatus_Search"].ToString();

            if (!string.IsNullOrEmpty(approvalStatus) && approvalStatus == "3")
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = true;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Green;
                lnkClientAcceptance.Enabled = true;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Green;
                lnkPrintGrn.Enabled = true;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Green;
                lnkLicApproval.Enabled = true;
                lnkLicApproval.ForeColor = System.Drawing.Color.Green;
                lnkSupervisorApproval.Enabled = true;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Green;

            }
            else if (!string.IsNullOrEmpty(approvalStatus) && approvalStatus == "2")
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = true;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Green;
                lnkClientAcceptance.Enabled = true;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Green;
                lnkPrintGrn.Enabled = true;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Green;
                lnkLicApproval.Enabled = true;
                lnkLicApproval.ForeColor = System.Drawing.Color.Green;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!string.IsNullOrEmpty(grnId))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = true;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Green;
                lnkClientAcceptance.Enabled = true;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Green;
                lnkPrintGrn.Enabled = true;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Green;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;

            }
            else if (!string.IsNullOrEmpty(clientAcceptanceTimeStamp) || !string.IsNullOrEmpty(clientRejectTimeStamp))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = true;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Green;
                lnkClientAcceptance.Enabled = true;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Green;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;

            }
            else if (!string.IsNullOrEmpty(cradingFactorGroupID) && !string.IsNullOrEmpty(gradingResultStatusID))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = true;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Green;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;

            }
            else if (!string.IsNullOrEmpty(gradingId))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = true;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Green;

                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;

            }
            else if (!string.IsNullOrEmpty(sampleResultRecivedDate))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = true;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Green;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!string.IsNullOrEmpty(samplingId))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = true;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Green;
                lnkPrintSampling.Enabled = false;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            }
            else if (!string.IsNullOrEmpty(arrivalId))
            {
                lnkPrintArrival.Enabled = true;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Green;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = false;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                lnkPrintArrival.Enabled = false;
                lnkPrintArrival.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSamplingTicket.Enabled = false;
                lnkPrintSamplingTicket.ForeColor = System.Drawing.Color.Gray;
                lnkPrintSampling.Enabled = false;
                lnkPrintSampling.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGradingCode.Enabled = false;
                lnkPrintGradingCode.ForeColor = System.Drawing.Color.Gray;

                lnkPrintGrading.Enabled = false;
                lnkPrintGrading.ForeColor = System.Drawing.Color.Gray;
                lnkClientAcceptance.Enabled = false;
                lnkClientAcceptance.ForeColor = System.Drawing.Color.Gray;
                lnkPrintGrn.Enabled = false;
                lnkPrintGrn.ForeColor = System.Drawing.Color.Gray;
                lnkLicApproval.Enabled = false;
                lnkLicApproval.ForeColor = System.Drawing.Color.Gray;
                lnkSupervisorApproval.Enabled = false;
                lnkSupervisorApproval.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void lnkPrintArrival_Click(object sender, EventArgs e)
        {
            if (Session["TrackingNumber_Search"] == null)
            {
                lblSearchResultStatus.Text = "Unable to get tracking number! ";
                return;
            }
            string TrackingNo = Session["TrackingNumber_Search"].ToString();
            //Response.Redirect("AddArrival.aspx?TrackingNo=" + TrackingNo);

            ArrivalModel theArrivalModel = new ArrivalModel();
            theArrivalModel.TrackingNumber = TrackingNo;
            //theArrivalModel = theArrivalModel.GetByTrackingNo();
            //theArrivalModel.IsNew = false;
            Session["Arrival"] = theArrivalModel;
            Response.Redirect("AddArrival.aspx?TrackingNo=" + TrackingNo + " &CommandType=Update");
        }

        protected void lnkPrintSamplingTicket_Click(object sender, EventArgs e)
        {
            //Session["CommandType_GRN"] = "Update";
            if (Session["SamplingsId_Search"] == null)
            {
                lblSearchResultStatus.Text = "Unable to get sampling! ";
                return;
            }
            Session["ReportType"] = "SampleTicket";
            Session["SampleId"] = Session["SamplingsId_Search"];


            ScriptManager.RegisterStartupScript(UpdatePanel1,
                                                   this.GetType(),
                                                   "ShowReport",
                                                   "<script type=\"text/javascript\">" +
                                                   string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                   "</script>",
                                                   false);
        }

        protected void lnkPrintSampling_Click(object sender, EventArgs e)
        {
            //Session["CommandType_GRN"] = "Update";
            if (Session["SamplingsId_Search"] == null)
            {
                lblSearchResultStatus.Text = "Unable to get sampling! ";
                return;
            }
            //string gradingTrackingNumber = Session["SamplingsId_Search"].ToString();
            Response.Redirect("AddSamplingResultNew.aspx?&CommandName=Update");
        }

        protected void lnkClientAcceptance_Click(object sender, EventArgs e)
        {
            //Session["EditMode"] = true;
            Response.Redirect("GradingResultClientAcceptanceNew.aspx?GradingCode=" + Session["GradingCode_Search"].ToString() + "&EditMode=True");
        }

        protected void lnkPrintGradingCode_Click(object sender, EventArgs e)
        {


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt = GradingModel.GetSampleDate(Session["SampleCode"].ToString());
            List<GradingModel> samplingList = GradingModel.getSamplingInfo(Session["SampleCode"].ToString());
            dt1 = GradingModel.GetWoredaName(samplingList[0].WoredaID);
            dt2 = GradingModel.GetCommodityClass(samplingList[0].CommodityID, samplingList[0].WoredaID, null, samplingList[0].VoucherCommodityTypeID);

            Session["VoucherCommodityTypeID"] = samplingList[0].VoucherCommodityTypeID;
            Session["CommodityID"] = samplingList[0].CommodityID;
            Session["GenerateCode"] = Session["GradingsId_Search"];
            Session["ReportType"] = "GradingCode";
            ScriptManager.RegisterStartupScript(UpdatePanel1,
                                                 this.GetType(),
                                                 "ShowReport",
                                                 "<script type=\"text/javascript\">" +
                                                 string.Format("javascript:window.open(\"ReportViewer.aspx\", \"_blank\",\"height=1000px,width=1000px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +

                                                 "</script>",
                                                 false);            
        }
        protected void lnkPrintGrading_Click(object sender, EventArgs e)
        {
            Response.Redirect("GradingResult.aspx?GradingCode=" + Session["GradingCode_Search"].ToString() + " &EditMode=true");
        }

        protected void lnkPrintGrn_Click(object sender, EventArgs e)
        {
            //Session["CommandType_GRN"] = "Update";
            if (Session["GradingTrackingNumber_Search"] == null)
            {
                lblSearchResultStatus.Text = "Unable to get grading tracking number! ";
                return;
            }
            string gradingTrackingNumber = Session["GradingTrackingNumber_Search"].ToString();
            Response.Redirect("AddUnloadingNew.aspx?TranNo=" + gradingTrackingNumber + "&CommandName=Update");
        }

        protected void lnkLicApproval_Click(object sender, EventArgs e)
        {
            if (Session["GRNStatus_Search"] == null && Session["GRNID_Search"] == null)
            {
                lblSearchResultStatus.Text = "Unable to get GRN status! ";
                return;
            }
            string grnStatus = Session["GRNStatus_Search"].ToString();
            Response.Redirect("GRNApproval.aspx?GRNStatus=" + grnStatus + "&GRNID=" + Session["GRNID_Search"].ToString() + "&From=2");
        }

        protected void lnkSupervisorApproval_Click(object sender, EventArgs e)
        {
            if (Session["GRNStatus_Search"] == null && Session["GRNID_Search"] == null)
            {
                if (Session["GRNID_Search"] == null)
                    return;
                lblSearchResultStatus.Text = "Unable to get GRN status! ";
                return;
            }
            string grnStatus = Session["GRNStatus_Search"].ToString();
            Response.Redirect("GRNApprovalSupervisor.aspx?GRNStatus=" + grnStatus + "&GRNID=" + Session["GRNID_Search"].ToString() + "&From=3");
        }

        protected void grdSearchResultList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSearchResultList.PageIndex = e.NewPageIndex;
            grdSearchResultList.DataBind();
            BindSearchGrid();
        }

        public void DestroySessions()
        {
            Session["ArrivalId_Search"] = null;
            Session["TrackingNumber_Search"] = null;
            Session["SamplingsId_Search"] = null;
            Session["GradingsId_Search"] = null;
            Session["GradingCode_Search"] = null;
            Session["CodeReceivedDateTime_Search"] = null;
            Session["GradingFactorGroupID_Search"] = null;
            Session["GradingResultStatusID_Search"] = null;
            Session["ClientAcceptanceTimeStamp_Search"] = null;
            Session["ClientRejectTimeStamp_Search"] = null;
            Session["GRNID_Search"] = null;
            Session["GradingTrackingNumber_Search"] = null;
            Session["SampleResultReceivedDate_Search"] = null;
            Session["WarehouseId"] = null;
            Session["ProductionYear"] = null;
            Session["GRNStatus_Search"] = null;
        }


    }
}