using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GINBussiness;
using System.Text;

namespace WarehouseApplication
{
    public partial class DailyPSAReport : System.Web.UI.Page
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
            GINModel PSA = new GINModel();
            _dt = PSA.GetDailyPSA(txtDateFrom.Text, txtTo.Text);
            _newtbl = new DataTable();
            _newtbl.Columns.Add(new DataColumn("PSANo", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("LeadInventoryController", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("DateIssued", typeof(string)));//
            _newtbl.Columns.Add(new DataColumn("Warehouse", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GinWeight", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("PSAWeight", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("OverUnderDelivery", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ClientName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ClientIDNo", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("WarehouseReceiptNo", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ExpirationDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("ShedName", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CommodityType", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CommodityGrade", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("CommoditySymbol", typeof(string)));
            _newtbl.Columns.Add(new DataColumn("GINStatus", typeof(string)));

            _newtbl.Columns.Add(new DataColumn("remark", typeof(string)));
            //_newtbl.Columns.Add(new DataColumn("Symbol", typeof(string)));
            //_newtbl.Columns.Add(new DataColumn("AcceptanceDate", typeof(string)));
            //_newtbl.Columns.Add(new DataColumn("SpecificArea", typeof(string)));

            if (_dt.Rows.Count > 0)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    DataRow row = _newtbl.NewRow();
                    row["PSANo"] = _dt.Rows[i]["PSANo"];
                    row["LeadInventoryController"] = _dt.Rows[i]["LeadInventoryController"];
                    row["DateIssued"] = _dt.Rows[i]["DateIssued"];
                    row["Warehouse"] = _dt.Rows[i]["Warehouse"];
                    row["GinWeight"] = _dt.Rows[i]["GinWeight"];
                    row["PSAWeight"] = _dt.Rows[i]["PSAWeight"];
                    row["OverUnderDelivery"] = _dt.Rows[i]["OverUnderDelivery"];
                    row["ClientName"] = _dt.Rows[i]["ClientName"];
                    row["ClientIDNo"] = _dt.Rows[i]["ClientIDNo"];
                    row["WarehouseReceiptNo"] = _dt.Rows[i]["WarehouseReceiptNo"];
                    row["ExpirationDate"] = _dt.Rows[i]["ExpirationDate"];
                    row["CreatedDate"] = _dt.Rows[i]["CreatedDate"];
                    row["ShedName"] = _dt.Rows[i]["ShedName"];
                    row["CommodityType"] = _dt.Rows[i]["CommodityType"];
                    row["CommodityGrade"] = _dt.Rows[i]["CommodityGrade"];
                    row["CommoditySymbol"] = _dt.Rows[i]["CommoditySymbol"];
                    row["GINStatus"] = _dt.Rows[i]["GINStatus"];
                    row["remark"] = _dt.Rows[i]["remark"];
                    //row["Symbol"] = _dt.Rows[i]["Symbol"];
                    //row["AcceptanceDate"] = _dt.Rows[i]["AcceptanceDate"];
                    //row["SpecificArea"] = _dt.Rows[i]["SpecificArea"];

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
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=DailyPSA.xls");

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