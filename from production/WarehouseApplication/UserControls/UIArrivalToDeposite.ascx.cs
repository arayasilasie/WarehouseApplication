using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.BLL;
using WarehouseApplication.SECManager;
using System.Text;

namespace WarehouseApplication.UserControls
{
    public partial class UIArrivalToDeposite : System.Web.UI.UserControl
    {
        StringBuilder str = new StringBuilder();
        StringBuilder str1 = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string warehouseName = WarehouseBLL.GetWarehouseNameById(UserBLL.GetCurrentWarehouse());
            this.lblWN.Text = warehouseName;

            //str.Append("<table  align='center' border='1' bordercolor='#00aeef' width='99%' class='reporttable1' cellspacing='0' cellpadding='0' style='font-size:10;'>");
            //str.Append("<tr>");
            //str.Append("<td>");
            //str.Append("<b>S.No</b>");
            //str.Append("</td>");
            //str.Append("<td>");
            //str.Append("<b>Name</b>");
            //str.Append("</td>");
            //str.Append("</tr>");
            //str.Append("<tr>");
            //str.Append("<td>");
            //str.Append("1");
            //str.Append("</td>");
            //str.Append("<td>");
            //str.Append("Shubhang Mathur");
            //str.Append("</td>");
            //str.Append("</tr>");
            //str.Append("<tr>");
            //str.Append("<td>");
            //str.Append("2");
            //str.Append("</td>");
            //str.Append("<td>");
            //str.Append("Shubhang Sahai Mathur");
            //str.Append("</td>");
            //str.Append("</tr>");
            //str.Append("</table>".ToString());
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
            List<rptArrivalToDepositeBLL> lst = null;
            rptArrivalToDepositeBLL o = new rptArrivalToDepositeBLL();
            lst = o.GetReportData(UserBLL.GetCurrentWarehouse(), from);
            if (lst != null)
            {
                if (lst.Count > 0)
                {
                    str.Append("<table  align='center' border='1' bordercolor='#000000' width='99%' class='reporttable1' cellspacing='0' cellpadding='0' style='font-size:10;'>");
                    str.Append("<tr style='color:#000000; font-weight:bold>' ");

                    str.Append("<td>S.No");
                    str.Append("</td>");

                    str.Append("<td>voucher.No");
                    str.Append("</td>");

                    str.Append("<td>Company Name");
                    str.Append("</td>");

                    str.Append("<td>Plate No.");
                    str.Append("</td>");

                    str.Append("<td>Trailer Plate No.");
                    str.Append("</td>");

                    str.Append("<td>No. Bags");
                    str.Append("</td>");

                    str.Append("<td>Date of Arrival");
                    str.Append("</td>");

                    str.Append("<td>Date of Deposit");
                    str.Append("</td>");

                    str.Append("</tr>");

                    int sno = 0;
                    foreach (rptArrivalToDepositeBLL i in lst)
                    {
                        sno++;
                        str.Append("<tr>");

                        str.Append("<td>");
                        str.Append(sno.ToString());
                        str.Append("</td>");


                        str.Append("<td>");
                        str.Append(i.VoucherNo.ToString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.ClientName.ToString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.PlateNo.ToString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.TrailerPlateNo.ToString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.NoBags.ToString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.ArrivalDate.ToShortDateString());
                        str.Append("</td>");

                        str.Append("<td>");
                        str.Append(i.unloadedDate.ToShortDateString());
                        str.Append("</td>");

                        str.Append("</tr>");
                    }

                    str.Append("</table>".ToString());

                }
            }

        }
    }
}