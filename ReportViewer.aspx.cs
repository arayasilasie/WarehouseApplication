using System;
using WarehouseApplication.Reports;
using DataDynamics.ActiveReports;
using GINBussiness;
using WarehouseApplication.BLL;
using GradingBussiness;
using System.Data;
using WarehouseApplication.Report;
using DataDynamics.ActiveReports.Export.Pdf;
using System.Configuration;
namespace WarehouseApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DataTable dt = new DataTable();
            ActiveReport rpt = null;
            if (Session["ReportType"].ToString() == "PreArrival")
            {
                BLL.PreArrival objPreArrival = new BLL.PreArrival();
                DataTable dt = objPreArrival.PopulatePreArrival(new Guid(Session["CommodityRequestId"].ToString()));
                rpt = new rptTrackingReport();
                rpt.DataSource = dt;
                WebViewer1.Report = rpt;
                Session["CommodityRequestId"] = null;
                WebViewer1.Width = 600;
                WebViewer1.Height = 400;
            }
            if (Session["ReportType"].ToString() == "GRN")
            {
                rpt = new rptGRNnew();
                GRN_BL objGrnBl = new GRN_BL();
                DataTable dt = objGrnBl.GetGRNReport(new Guid(Session["GRNId_GRN"].ToString()));
                rpt.DataSource = dt;
                WebViewer1.Report = rpt;
            }
            if (Session["ReportType"].ToString() != "DoNuthing")
            {

                if (Session["ReportType"].ToString() == "PUN")
                {
                    rpt = new rptPUNReport();
                    rpt.DataSource = GINBussiness.PickupNoticeModel.PrintPUN(Session["PUNID"].ToString());
                }
                else if (Session["ReportType"].ToString() == "GIN")
                {
                    rpt = new rptGINReport();
                    Session["GINID"] = ((GINModel)Session["GINMODEL"]).ID;
                    rpt.DataSource = GINBussiness.PickupNoticeModel.PrintGIN(Convert.ToBoolean(Session["EditModePrint"]), new Guid(Session["GINID"].ToString()));
                }
                else if (Session["ReportType"].ToString() == "PSA")
                {
//----------Updated START ------ NOV 27 2013

                    // if it is redirected from search page
                    string statusx = "Unapproved PSA";
                    if (Request.QueryString["GINID"] == null)
                    {
                        rpt = new rptPSAReport(statusx);
                        Session["GINPSAID"] = ((GINModel)Session["GINMODEL"]).ID;
                        rpt.DataSource = GINBussiness.PickupNoticeModel.PrintPSA(new Guid(Session["GINPSAID"].ToString()));
                    }
                    else
                    {
                        statusx = (Convert.ToInt16(Request.QueryString["ST"]) % 11 == 0) ? "Approved" : "Unapproved PSA";
                        rpt = new rptPSAReport(statusx);
                        if(statusx.Equals("Approved"))
                            rpt.DataSource = GINBussiness.PickupNoticeModel.PrintPSA_Approved(new Guid(Request.QueryString["GINID"]));
                        else
                            rpt.DataSource = GINBussiness.PickupNoticeModel.PrintPSA(new Guid(Request.QueryString["GINID"]));
                    }
//----------Updated End ------ NOV 27 2013
                }
                else if (Session["ReportType"].ToString() == "ExpierdList")
                {
                    rpt = new rptPickupNoticeExpiredList();
                    DataTable dt = new DataTable();
                    dt= PickupNoticeModel.SearchExpieredList(UserBLL.GetCurrentWarehouse(), Convert.ToDateTime(Session["ExpirationDateFrom"]), Convert.ToDateTime(Session["ExpirationDateTo"]));
                    if (dt.Rows.Count > 0)
                    {
                        rpt.DataSource = dt;
                    }
                    else
                        return;
                }
                else if (Session["ReportType"].ToString() == "ExpierdListAdmin")
                {
                    rpt = new rptPickupNoticeExpiredList();
                    DataTable dt = new DataTable();
                    dt = PickupNoticeModel.SearchExpieredListAdmin(Convert.ToDateTime(Session["ExpirationDateFrom"]), Convert.ToDateTime(Session["ExpirationDateTo"]));
                    if (dt.Rows.Count > 0)
                    {
                        rpt.DataSource = dt;
                    }
                    else
                        return;
                }
                else if (Session["ReportType"].ToString() == "GINApproval")
                {
                    rpt = new rptGINApproval();
                    rpt.DataSource = GINBussiness.GINModel.PrintGINApprovalReport(UserBLL.GetCurrentWarehouse(), new Guid(Session["SelectedLIC"].ToString()), Session["LICName"].ToString());
                    WebViewer1.Report = rpt;
                    rpt.PageSettings.Margins.Top = 0;
                }
                else if (Session["ReportType"].ToString() == "SampleTicket")
                {
                    //rpt = new rptSampleTicketCoffeeNew();
                    //rpt.DataSource = SamplingBussiness.SamplingModel.GetSampleTicketReport(new Guid(Session["SampleId"].ToString()));

                    rpt = new rptSampleTicketCoffeeNew();
                    rpt.DataSource = SamplingBussiness.SamplingModel.GetSampleTicketReport(new Guid(Session["SampleId"].ToString()));
                }
                else if (Session["ReportType"].ToString() == "GradingResult")
                {
                    rpt = new rptResultReport();

                    rpt.DataSource = GradingModel.GradingResultreport(Session["GradingCode"].ToString());
                }
                else if (Session["ReportType"].ToString() == "GradingResultNoDeposit")
                {
                    rpt = new rptInspectionTestResult();

                    rpt.DataSource = GradingModel.GetInspectionTestResult(Session["GradingCode"].ToString(), UserBLL.GetCurrentWarehouse());
                }
                else if (Session["ReportType"].ToString() == "GradingCode")
                {
                    rpt = new rptGenerate();
                    DataTable dt = new DataTable();
                    dt = GradingModel.printCode(new Guid(Session["GenerateCode"].ToString()), new Guid(Session["VoucherCommodityTypeID"].ToString()), new Guid(Session["CommodityID"].ToString()));

                    rpt.DataSource = dt;
                }
                else if (Session["ReportType"].ToString() == "GradingResultForSegrigation")
                {
                    rpt = new Report.rptSegrigationGradingReport();
                    rpt.DataSource = GradingModel.GradingResultreportForSegrigation(Session["GradingCode"].ToString());
                }
                if (Session["ReportType"].ToString() == "ExpiredCons")
                {
                    //rpt = new rptConsignment();
                    DataTable dt = new DataTable();
                    dt = ExpiredConsignment.SearchConsExpieredList(UserBLL.GetCurrentWarehouse());
                    //Guid wrid = new Guid("b16b8134-dec7-4c75-a4e7-c27e0b85f1a3");
                    //dt = ExpiredConsignment.SearchConsExpieredList(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rpt = new rptConsignment(dt);
                        WebViewer1.Report = rpt;// new rptConsignment(dt);
                        WebViewer1.Visible = true;
                    }
                    else
                        return;
                }
                if (Session["ReportType"].ToString() != "GINApproval")
                {
                    rpt.PageSettings.Margins.Top = 0;
                    rpt.PageSettings.Margins.Left = 0.4f;
                    rpt.PageSettings.Margins.Right = 0.4f;
                    WebViewer1.Report = rpt;

                    //rpt.Run(false);
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

                    //// Create the PDF export object
                    //PdfExport pdf = new PdfExport();
                    //// Create a new memory stream that will hold the pdf output
                    //System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                    //// Export the report to PDF:
                    //pdf.Export(rpt.Document, memStream);
                    //// Write the PDF stream out
                    //Response.BinaryWrite(memStream.ToArray());
                    //// Send all buffered content to the client
                    //Response.End();
                }



            }

        }
    }
}
