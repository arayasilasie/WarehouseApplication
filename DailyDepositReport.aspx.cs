using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class DailyDepositReport : System.Web.UI.Page
    {
        private DataTable _dt;
        private DataTable _newtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            GRN_BL dp = new GRN_BL();
            _dt = dp.SearchDailyDepositList(txtDateFrom.Text, txtTo.Text);
            _newtbl = new DataTable();
            _newtbl.Columns.Add(new DataColumn("Warehouse", typeof(string)));
            
            _newtbl.Columns.Add(new DataColumn("DateTimeReceived", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("VoucherNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DepositorName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Woreda", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Zone", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Region", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ArrivalNoOfBags", typeof(int)));


            _newtbl.Columns.Add(new DataColumn("Dateofsample", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("dateofcoding", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("dateofdecoding", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("gradeissueddate", typeof(DateTime)));
            _newtbl.Columns.Add(new DataColumn("ClientAcceptanceDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Symbol", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Commodity", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ArrivalWeight", typeof(float)));


            _newtbl.Columns.Add(new DataColumn("GRNNetWeight", typeof(float)));
            _newtbl.Columns.Add(new DataColumn("TruckPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TrailerPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ConsignmentType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GRNNoOfBags", typeof(int)));
            _newtbl.Columns.Add(new DataColumn("GRN_Number", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DateDeposited", typeof(DateTime)));
            _newtbl.Columns.Add(new DataColumn("status", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("traceable", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TruckType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("LICName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Shade", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Rebagging", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ProductionYear", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Remark", typeof(string)));

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    DataRow row = _newtbl.NewRow();
                    row["Warehouse"] = _dt.Rows[i]["Warehouse"];
                    row["VoucherNumber"] = _dt.Rows[i]["VoucherNumber"];
                    row["DepositorName"] = _dt.Rows[i]["DepositorName"];
                    row["Woreda"] = _dt.Rows[i]["Woreda"];
                    row["Zone"] = _dt.Rows[i]["Zone"];
                    row["Region"] = _dt.Rows[i]["Region"];
                    row["ArrivalNoOfBags"] = _dt.Rows[i]["ArrivalNoBugs"];
                    row["Dateofsample"] = _dt.Rows[i]["DateOfSample"];
                   

                    row["dateofcoding"] = _dt.Rows[i]["DateOFCoding"];
                    row["DateTimeReceived"] = _dt.Rows[i]["ArrivaDate"];
                    row["dateofdecoding"] = _dt.Rows[i]["VoucherNumber"];
                    row["gradeissueddate"] = _dt.Rows[i]["GradeIssuedDate"];
                    row["ClientAcceptanceDate"] = _dt.Rows[i]["ClientAcceptanceDate"];
                    row["Symbol"] = _dt.Rows[i]["Symbol"];
                    row["Commodity"] = _dt.Rows[i]["Commodity"];
                    row["ArrivalWeight"] = _dt.Rows[i]["Quantity"];

                    row["GRNNetWeight"] = _dt.Rows[i]["GRnNetWeight"];
                    row["TruckPlateNumber"] = _dt.Rows[i]["CarPlateNumber"];
                    row["TrailerPlateNumber"] = _dt.Rows[i]["TrailerPlateNumber"];
                    row["ConsignmentType"] = _dt.Rows[i]["Consignment type"];
                    row["GRNNoOfBags"] = _dt.Rows[i]["TotalNumberOfBags"];
                    row["GRN_Number"] = _dt.Rows[i]["GRN_Number"];
                    row["DateDeposited"] = _dt.Rows[i]["DateDeposited"];
                    row["status"] = _dt.Rows[i]["GRNStatus"];
                    row["traceable"] = _dt.Rows[i]["IsTraceable"];
                    row["TruckType"] = _dt.Rows[i]["TruckType"];
                    row["LICName"] = _dt.Rows[i]["LICName"];
                    row["Shade"] = _dt.Rows[i]["Shade"];
                    row["Rebagging"] = _dt.Rows[i]["Rebagging"];
                    row["ProductionYear"] = _dt.Rows[i]["ProductionYear"];
                    row["Remark"] = _dt.Rows[i]["StatusRemark"];

                    _newtbl.Rows.Add(row);
                }
                PrepareExcel(_newtbl);
            }
            
        }
        private void PrepareExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Deposit.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<BR><BR><BR>");

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR bgcolor='seagreen'>");

            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

       
    }
}