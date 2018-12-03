using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using WarehouseApplication.BLL;
using GINBussiness;

namespace WarehouseApplication
{
    public partial class DailyDeliveryReport : System.Web.UI.Page
    {
        private DataTable _dt;
        private DataTable _newtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
       
        protected void btnExport_Click(object sender, EventArgs e)
        {
            GINBussiness.GINModel dl = new GINModel();
            _dt = dl.SearchDailyDeliveryList(txtDateFrom.Text, txtTo.Text);
            _newtbl = new DataTable();
            _newtbl.Columns.Add(new DataColumn("GINNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("BWHR", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GRN", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("BuyerClientName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Symbol", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("PUNPrintDateTime", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DateIssued", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DateTimeLoaded", typeof(DateTime)));


            _newtbl.Columns.Add(new DataColumn("ConsignmentType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GINCreatedDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TruckPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TrailerPlateNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GINWeight", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("PUNWeight", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("Over/UnderDelivery", typeof(string)));

            _newtbl.Columns.Add(new DataColumn("WarehouseName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("LeadInventoryController", typeof(string)));


            _newtbl.Columns.Add(new DataColumn("AgentName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GINStatus", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("IsTraceable", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("TradeDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("IDType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("AgentIDNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DriverName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("LicenseNumber", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("LicenseIssuedBy", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ProductionYear", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CommodityType", typeof(string)));
            
            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    DataRow row = _newtbl.NewRow();
                    row["GINNumber"] = _dt.Rows[i]["GINNumber"];
                    row["BWHR"] = _dt.Rows[i]["BWHR"];
                    row["GRN"] = _dt.Rows[i]["GRN"];
                    row["BuyerClientName"] = _dt.Rows[i]["BuyerClientName"];
                    row["Symbol"] = _dt.Rows[i]["Symbol"];
                    row["PUNPrintDateTime"] = _dt.Rows[i]["PUNPrintDateTime"];
                    row["DateIssued"] = _dt.Rows[i]["DateIssued"];
                    row["DateTimeLoaded"] = _dt.Rows[i]["DateTimeLoaded"];
                    row["ConsignmentType"] = _dt.Rows[i]["ConsignmentType"];

                    row["GINCreatedDate"] = _dt.Rows[i]["GINCreatedDate"];
                    row["TruckPlateNumber"] = _dt.Rows[i]["TruckPlateNumber"];
                    row["TrailerPlateNumber"] = _dt.Rows[i]["TrailerPlateNumber"];
                    row["GINWeight"] = _dt.Rows[i]["GINWeight"];
                    row["PUNWeight"] = _dt.Rows[i]["PUNWeight"];
                    row["Over/UnderDelivery"] = _dt.Rows[i]["Over/UnderDelivery"];
                    row["WarehouseName"] = _dt.Rows[i]["WarehouseName"];
                    row["LeadInventoryController"] = _dt.Rows[i]["LeadInventoryController"];
                    row["AgentName"] = _dt.Rows[i]["AgentName"];
                    row["GINStatus"] = _dt.Rows[i]["GINStatus"];
                    row["IsTraceable"] = _dt.Rows[i]["IsTraceable"];
                    row["TradeDate"] = _dt.Rows[i]["TradeDate"];
                    row["IDType"] = _dt.Rows[i]["IDType"];
                    row["AgentIDNumber"] = _dt.Rows[i]["AgentIDNumber"];
                    row["DriverName"] = _dt.Rows[i]["DriverName"];
                    row["LicenseNumber"] = _dt.Rows[i]["LicenseNumber"];
                    row["LicenseIssuedBy"] = _dt.Rows[i]["LicenseIssuedBy"];
                    row["ProductionYear"] = _dt.Rows[i]["ProductionYear"];
                    row["CommodityType"] = _dt.Rows[i]["CommodityType"];
                   
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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=DailyDeliveryReport.xls");

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