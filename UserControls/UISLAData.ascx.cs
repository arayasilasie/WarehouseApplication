using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;

namespace WarehouseApplication.UserControls
{
    public partial class UISLAData : System.Web.UI.UserControl
    {
        StringBuilder str = new StringBuilder();
        StringBuilder str1 = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string warehouseName = WarehouseBLL.GetWarehouseNameById(UserBLL.GetCurrentWarehouse());
            this.lblWN.Text = warehouseName;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getData(DateTime.Parse(this.txtFrom.Text));
            str1.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            str1.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            str1.Append("<DIV  style='font-size:12px;'>");
            str1.Append(str.ToString());
            str1.Append("</div></body></html>");
            string strFile = "Text_Excel.xls";
            string strcontentType = "application/excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;

            //file open mode with file name

            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(str1.ToString());
            Response.Flush();
            Response.Close();
            Response.End();
        }
        private void getData(DateTime from)
        {
            SLABLL o = new SLABLL();
            List<SLABLL> list = null;
            Guid Id = UserBLL.GetCurrentWarehouse();

            list = o.GetSLA(Id, from, 0);

            if (list != null)
            {

                var q = (from l in list
                         orderby l.objArrival.DateTimeRecived ascending
                         select new
                         {
                             VoucherNo = l.objVoucher.VoucherNo,
                             Client = ClientBLL.GetClinetNameById(l.objGRN.ClientId),
                             PlateNumber = l.objDriver.PlateNumber,
                             TrailerPlateNumber = l.objDriver.TrailerPlateNumber,
                             TotalNumberOfBags = l.objUnloading.TotalNumberOfBags,
                             ArrivalDate = l.objArrival.DateTimeRecived.ToShortDateString(),
                             //ArrivalDateSystem = l.objArrival.CreatedTimestamp ,
                             SampledDate = l.objSampling.GeneratedTimeStamp.ToShortDateString(),
                             ArrivalToSample = (l.objSampling.GeneratedTimeStamp.Date.Subtract(l.objArrival.DateTimeRecived.Date)).Days,
                             //Sampling Result
                             //SamplingResultDate = (l.objSamplingResult.CreatedTimestamp).ToShortDateString(),
                             SamplingResultDate = l.objSamplingResult.ResultReceivedDateTime.ToShortDateString(),
                             SamplingToResult = (l.objSamplingResult.ResultReceivedDateTime.Date.Subtract(l.objSampling.GeneratedTimeStamp.Date)).Days,
                             //Coding 
                             CodingDate = l.objGrading.DateCoded.ToShortDateString(),
                             SamplingResultToCode = (l.objGrading.DateCoded.Date.Subtract(l.objSamplingResult.ResultReceivedDateTime.Date)).Days,
                             //Grading Result
                             GradeRecivedDate = l.objGradingResult.CreatedTimestamp.ToShortDateString(),
                             GradeToResult = (l.objGradingResult.GradeRecivedTimeStamp.Date.Subtract(l.objGrading.DateCoded.Date)).Days,
                             // GR to Client Acceptance

                             GradeClientResponse = ((DateTime)l.objGradingResult.ClientAcceptanceTimeStamp).ToShortDateString(),
                             GraderesultStatus = l.objGradingResult.Status,
                             GradingResultToCleintAcceptance = (((DateTime)l.objGradingResult.ClientAcceptanceTimeStamp).Date.Subtract(l.objGradingResult.GradeRecivedTimeStamp.Date)).Days,
                             GradingRecivedResultStatus = l.objGradingResult.Status,
                             GradeResultToClientresponse = (((DateTime)l.objGradingResult.ClientAcceptanceTimeStamp).Date.Subtract(l.objGradingResult.GradeRecivedTimeStamp.Date)).Days,
                             //Unloading 
                             DateDeposited = l.objUnloading.DateDeposited.ToShortDateString(),
                             GradeClientResponsetoUnloading = l.objUnloading.DateDeposited.Date.Subtract(((DateTime)l.objGradingResult.ClientAcceptanceTimeStamp).Date).Days,
                             NoBags = l.objUnloading.TotalNumberOfBags,
                             //Scaling 
                             DateWeighed = l.objScaling.DateWeighed.ToShortDateString(),
                             GradeClientResponsetoScaling = l.objScaling.DateWeighed.Date.Subtract(((DateTime)l.objGradingResult.ClientAcceptanceTimeStamp).Date).Days,

                             //GRN creation 
                             GRN_Number = l.objGRN.GRN_Number,
                             GRNCreatedDate = l.objGRN.CreatedTimestamp.ToShortDateString(),
                             ScalingToGRN = l.objGRN.CreatedTimestamp.Date.Subtract(l.objScaling.DateWeighed.Date).Days,
                             //GRN Acceptance
                             GRNCleintResponse = l.objGRN.ClientAcceptedTimeStamp.ToShortDateString(),
                             GRNToCleintResponse = l.objGRN.ClientAcceptedTimeStamp.Date.Subtract(l.objGRN.CreatedTimestamp.Date).Days,
                             //Manager Approval
                             ManagerAppp = l.objGRN.ApprovedTimeStamp.ToShortDateString(),
                             GRNClientResponseToApproval = l.objGRN.ApprovedTimeStamp.Date.Subtract(l.objGRN.ClientAcceptedTimeStamp.Date).Days,

                             //From Arrival to GRN 
                             FromArrivalToDeposit = l.objUnloading.DateDeposited.Date.Subtract(l.objArrival.DateTimeRecived.Date).Days,
                             ArrivalToGRNCreation = l.objGRN.CreatedTimestamp.Date.Subtract(l.objArrival.DateTimeRecived.Date).Days,
                             ArrivalToClientAccptance = l.objGRN.ClientAcceptedTimeStamp.Date.Subtract(l.objArrival.DateTimeRecived.Date).Days,
                             ArrivalToGRNApproval = l.objGRN.ApprovedTimeStamp.Date.Subtract(l.objArrival.DateTimeRecived.Date).Days



                         });



                if (q != null)
                {
                    if (q.Count() > 0)
                    {
                        str.Append("<table  align='center' border='1' bordercolor='#000000' width='99%' class='reporttable1' cellspacing='0' cellpadding='0' style='font-size:10;'>");
                        str.Append("<tr style='color:#000000; font-weight:bold>' ");

                        //1
                        str.Append("<td>S.No");
                        str.Append("</td>");

                        //2
                        str.Append("<td>voucher.No");
                        str.Append("</td>");

                        //3
                        str.Append("<td>Company Name");
                        str.Append("</td>");

                        //4
                        str.Append("<td>Plate No.");
                        str.Append("</td>");

                        //5
                        str.Append("<td>Trailer Plate No.");
                        str.Append("</td>");

                        //6
                        str.Append("<td>No. Bags");
                        str.Append("</td>");

                        //7
                        str.Append("<td>Date of Arrival");
                        str.Append("</td>");

                        //8
                        str.Append("<td>Sampling Date");
                        str.Append("</td>");

                        //9
                        str.Append("<td>Arrival to Sampling ");
                        str.Append("</td>");

                        //10
                        str.Append("<td>Sampling Result Date ");
                        str.Append("</td>");

                        //11
                        str.Append("<td>Sampling To Sampling Result ");
                        str.Append("</td>");

                        //12
                        str.Append("<td>Date Coded ");
                        str.Append("</td>");

                        //13
                        str.Append("<td>Sampling Result To Code ");
                        str.Append("</td>");

                        //14
                        str.Append("<td>Grade Recieved Date ");
                        str.Append("</td>");

                        //15
                        str.Append("<td>Grading To Result");
                        str.Append("</td>");

                        //16
                        str.Append("<td>Grading Status");
                        str.Append("</td>");

                        //17
                        str.Append("<td>Grading Result Cleint Respons date");
                        str.Append("</td>");


                        //18
                        str.Append("<td>Grading Result To Cleint Respons");
                        str.Append("</td>");

                        //19
                        str.Append("<td>Date Deposited");
                        str.Append("</td>");

                        //20
                        str.Append("<td>Client Response to Unloading");
                        str.Append("</td>");

                        //21
                        str.Append("<td>DateWeighed");
                        str.Append("</td>");

                        //22
                        str.Append("<td>Client Response to Scaling");
                        str.Append("</td>");

                        //23
                        str.Append("<td>GRN Number ");
                        str.Append("</td>");

                        //24
                        str.Append("<td>GRN Created Date  ");
                        str.Append("</td>");

                        //25
                        str.Append("<td>Scaling to GRN ");
                        str.Append("</td>");

                        //26
                        str.Append("<td>GRN Client Response  ");
                        str.Append("</td>");

                        //27
                        str.Append("<td>GRN To Cleint Response ");
                        str.Append("</td>");

                        //28
                        str.Append("<td>GRN Approval date ");
                        str.Append("</td>");

                        //29
                        str.Append("<td>Cleint Response to Manger Approval ");
                        str.Append("</td>");

                        //29.1
                        str.Append("<td>Arrival to Deposit");
                        str.Append("</td>");


                        //30
                        str.Append("<td>Arrival To GRN  ");
                        str.Append("</td>");

                        //31
                        str.Append("<td>Arrival To GRN Acceptance  ");
                        str.Append("</td>");


                        //32
                        str.Append("<td>Arrival To GRN Approval  ");
                        str.Append("</td>");

                        str.Append("</tr>");

                        int sno = 0;
                        foreach (var i in q)
                        {
                            sno++;
                            str.Append("<tr>");

                            //1
                            str.Append("<td>");
                            str.Append(sno.ToString());
                            str.Append("</td>");

                            //2
                            str.Append("<td>");
                            str.Append(i.VoucherNo.ToString());
                            str.Append("</td>");

                            //3
                            str.Append("<td>");
                            str.Append(i.Client.ToString());
                            str.Append("</td>");

                            //4
                            str.Append("<td>");
                            str.Append(i.PlateNumber.ToString());
                            str.Append("</td>");

                            //5
                            str.Append("<td>");
                            str.Append(i.TrailerPlateNumber.ToString());
                            str.Append("</td>");

                            //6
                            str.Append("<td>");
                            str.Append(i.TotalNumberOfBags.ToString());
                            str.Append("</td>");

                            //7
                            str.Append("<td>");
                            str.Append(i.ArrivalDate);
                            str.Append("</td>");

                            //8
                            str.Append("<td>");
                            str.Append(i.SampledDate);
                            str.Append("</td>");

                            //9
                            str.Append("<td>");
                            str.Append(i.ArrivalToSample);
                            str.Append("</td>");

                            //10
                            str.Append("<td>");
                            str.Append(i.SamplingResultDate);
                            str.Append("</td>");

                            //11
                            str.Append("<td>");
                            str.Append(i.SamplingToResult);
                            str.Append("</td>");

                            //12
                            str.Append("<td>");
                            str.Append(i.CodingDate);
                            str.Append("</td>");

                            //13
                            str.Append("<td>");
                            str.Append(i.SamplingResultToCode);
                            str.Append("</td>");

                            //14
                            str.Append("<td>");
                            str.Append(i.GradeRecivedDate);
                            str.Append("</td>");

                            //15
                            str.Append("<td>");
                            str.Append(i.GradeToResult);
                            str.Append("</td>");

                            //16
                            str.Append("<td>");
                            str.Append(i.GraderesultStatus);
                            str.Append("</td>");

                            //17
                            str.Append("<td>");
                            str.Append(i.GradeClientResponse);
                            str.Append("</td>");

                            //18
                            str.Append("<td>");

                            str.Append(i.GradingResultToCleintAcceptance);
                            str.Append("</td>");

                            //19
                            str.Append("<td>");
                            str.Append(i.DateDeposited);
                            str.Append("</td>");

                            //20
                            str.Append("<td>");
                            str.Append(i.GradeClientResponsetoUnloading);
                            str.Append("</td>");

                            //21
                            str.Append("<td>");
                            str.Append(i.DateWeighed);
                            str.Append("</td>");

                            //22
                            str.Append("<td>");
                            str.Append(i.GradeClientResponsetoScaling);
                            str.Append("</td>");

                            if (i.GRN_Number != "" && i.GRN_Number != null && DateTime.Parse(i.GRNCreatedDate) != DateTime.Parse("1/1/0001"))
                            {
                                //23
                                str.Append("<td>");
                                str.Append(i.GRN_Number);
                                str.Append("</td>");

                                //24
                                str.Append("<td>");
                                str.Append(i.GRNCreatedDate);
                                str.Append("</td>");

                                //25
                                str.Append("<td>");
                                str.Append(i.ScalingToGRN);
                                str.Append("</td>");

                                if (DateTime.Parse(i.GRNCleintResponse) != DateTime.Parse("1/1/0001"))
                                {
                                    //26
                                    str.Append("<td>");
                                    str.Append(i.GRNCleintResponse);
                                    str.Append("</td>");


                                    //27
                                    str.Append("<td>");
                                    str.Append(i.GRNToCleintResponse);
                                    str.Append("</td>");
                                }
                                else
                                {
                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");

                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");
                                }

                                if (DateTime.Parse("1/1/0001") != DateTime.Parse(i.ManagerAppp))
                                {
                                    //28
                                    str.Append("<td>");
                                    str.Append(i.ManagerAppp);
                                    str.Append("</td>");

                                    //29
                                    str.Append("<td>");
                                    str.Append(i.GRNClientResponseToApproval);
                                    str.Append("</td>");
                                }
                                else
                                {
                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");

                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");
                                }

                                //29.1
                                str.Append("<td>");
                                str.Append(i.FromArrivalToDeposit);
                                str.Append("</td>");

                                //30
                                if (DateTime.Parse(i.GRNCreatedDate) != DateTime.Parse("1/1/0001"))
                                {
                                    str.Append("<td>");
                                    str.Append(i.ArrivalToGRNCreation);
                                    str.Append("</td>");
                                }
                                else
                                {
                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");
                                }

                                if (DateTime.Parse(i.GRNCleintResponse) != DateTime.Parse("1/1/0001"))
                                {
                                    //31
                                    str.Append("<td>");
                                    str.Append(i.ArrivalToClientAccptance);
                                    str.Append("</td>");
                                }
                                else
                                {
                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");
                                }

                                if (DateTime.Parse("1/1/0001") != DateTime.Parse(i.ManagerAppp))
                                {
                                    //32
                                    str.Append("<td>");
                                    str.Append(i.ArrivalToGRNApproval);
                                    str.Append("</td>");
                                }
                                else
                                {
                                    str.Append("<td>");
                                    str.Append(" ");
                                    str.Append("</td>");
                                }



                            }



                            str.Append("</tr>");
                        }

                        str.Append("</table>".ToString());

                    }
                }

            }


        }
    }
}