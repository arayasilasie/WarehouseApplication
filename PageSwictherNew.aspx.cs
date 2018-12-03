using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class PageSwictherNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string transactionNo = Request.QueryString["TranNo"];
            string taskName = Request.QueryString["Task"];
            if (!(string.IsNullOrEmpty(transactionNo) && string.IsNullOrEmpty(taskName)))
            {
                if ("Full Arrival" == taskName)
                {
                    ArrivalModel theArrivalModel = new ArrivalModel();
                    theArrivalModel.TrackingNumber = transactionNo;
                    //theArrivalModel = theArrivalModel.GetByTrackingNo();
                    //theArrivalModel.IsNew = false;
                    Session["Arrival"] = theArrivalModel;
                    Response.Redirect("AddArrival.aspx?TrackingNo=" + transactionNo + " &CommandType=Insert");
                }
                else if ("Sampling Code" == taskName)
                {
                    Response.Redirect("GetSampleTicketNew.aspx");
                }
                else if ("Sampling Result".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                    Response.Redirect("AddSamplingResultNew.aspx?sampleCode=" + transactionNo);
                }
                else if("Grading Code".ToUpper().Trim() == taskName.ToUpper().Trim() )
                {
                    Response.Redirect("GenerateGrading.aspx?CurrentWarehouse=" 
                        + UserBLL.GetCurrentWarehouse() + "&sampleCode=" + transactionNo);

                }
                else if ("Code Received at Lab".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                    Response.Redirect("SampleCodeReceive.aspx?CurrentWarehouse="
                        + UserBLL.GetCurrentWarehouse() + "&GradingCode=" + transactionNo);

                }
                else if ("Grading Result".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                    Response.Redirect("GradingResult.aspx?CurrentWarehouse="
                         + UserBLL.GetCurrentWarehouse() + "&GradingCode=" + transactionNo
                         + "&EditMode=false"
                        );
                }
                //else if ("Grading Result Client Acceptance".ToUpper().Trim() == taskName.ToUpper().Trim())
                //{
                //    Response.Redirect("GradingResultClientAcceptanceNew.aspx?CurrentWarehouse="
                //        + UserBLL.GetCurrentWarehouse() + "&GradingCode=" + transactionNo);
                   
                //}
                else if ("Client Response".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                    Response.Redirect("GradingResultClientAcceptanceNew.aspx?CurrentWarehouse="
                        + UserBLL.GetCurrentWarehouse() + "&GradingCode=" + transactionNo);

                }
                else if ("Unloading Scaling and GRN".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                    Response.Redirect("AddUnloadingNew.aspx?CurrentWarehouse="
                        + UserBLL.GetCurrentWarehouse() + "&GradingCode=" + transactionNo
                        + "&CommandName=Insert"
                        );

                }
                //else if ("Data assistant GRN Approval".ToUpper().Trim() == taskName.ToUpper().Trim())
                //{
                //    Response.Redirect("GRNClientSign.aspx?CurrentWarehouse="
                //      + UserBLL.GetCurrentWarehouse());
                //}
                else if ("Supervisor GRN Approval".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                }
                else if ("GradingReSampling".ToUpper().Trim() == taskName.ToUpper().Trim())
                {
                }
                
                else
                {
                    return;
                }
            }
        }
    }
}