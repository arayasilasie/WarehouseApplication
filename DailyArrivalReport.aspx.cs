using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Web.UI.WebControls;
using System.Data;
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class DailyArrivalReport : System.Web.UI.Page
    {
        private DataTable _dt;
        private DataTable _newtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcell();
        }

        private void ExportToExcell()
        {
            ArrivalModel ar = new ArrivalModel();
            _dt = ar.SearchDailyArrivalList(txtDateFrom.Text, txtTo.Text);
            _newtbl = new DataTable();
            _newtbl.Columns.Add(new DataColumn("Warehouse", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ReceivedDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("VoucherNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DepositorName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Woreda", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Zone", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TruckPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TrailerPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Commodity", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CommodityType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Region", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("WashingStation", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ArrivalNoOfBags", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GradeCertificate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("EstimateWeight", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DriverName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ProductionYear", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Labcode", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Symbol", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("AcceptanceDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("SpecificArea", typeof(string)));
            
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    DataRow row = _newtbl.NewRow();
                    row["Warehouse"] = _dt.Rows[i]["warehouse"];
                    row["ReceivedDate"] = _dt.Rows[i]["DateTimeReceived"];
                    row["VoucherNumber"] = _dt.Rows[i]["VoucherNumber"];
                    row["DepositorName"] = _dt.Rows[i]["ClientName"];
                    row["Commodity"] = _dt.Rows[i]["Commodity"];
                    row["CommodityType"] = _dt.Rows[i]["CommodityType"];
                    row["Woreda"] = _dt.Rows[i]["Woreda"];
                    row["Zone"] = _dt.Rows[i]["Zone"];
                    row["TruckPlateNumber"] = _dt.Rows[i]["TruckPlateNumber"];
                    row["TrailerPlateNumber"] = _dt.Rows[i]["TrailerPlateNumber"];
                    row["Region"] = _dt.Rows[i]["Region"];
                    row["WashingStation"] = _dt.Rows[i]["WashingStation"];
                    row["ArrivalNoOfBags"] = _dt.Rows[i]["NumberofBags"];
                    row["GradeCertificate"] = _dt.Rows[i]["GradeCertificate"];
                    row["EstimateWeight"] = _dt.Rows[i]["EstimateWeight"];
                    row["DriverName"] = _dt.Rows[i]["DriverName"];
                    row["ProductionYear"] = _dt.Rows[i]["ProductionYear"];
                    row["Labcode"] = _dt.Rows[i]["Labcode"];
                    row["Symbol"] = _dt.Rows[i]["Symbol"];
                    row["AcceptanceDate"] = _dt.Rows[i]["AcceptanceDate"];
                    row["SpecificArea"] = _dt.Rows[i]["SpecificArea"];

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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DailyArrival.xls");

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