using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Web;
using WarehouseApplication.BLL;
using System.Data;
namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptInventoryTransfer.
    /// </summary>
    public partial class rptInventoryTransfer : DataDynamics.ActiveReports.ActiveReport
    {

        public rptInventoryTransfer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["StackId"] == null)
            {
                throw new Exception("Your Session has expired");
            }

            DataRow dr = InventoryTransferModel.GetInventoryTransaction( new Guid(HttpContext.Current.Session["StackId"].ToString()));
            txtLIC.Text = dr["LIC"].ToString();
            txtLIC2.Text = dr["LIC2"].ToString();
            txtLICName.Text = dr["LIC"].ToString();
            txtLIC2Name.Text = dr["LIC2"].ToString();
            txtPhysicalCount.Text=dr["PhysicalCount"].ToString();
            txtPhysicalCount2.Text=dr["PhysicalCountTo"].ToString();
            txtPhysicalWeight.Text = dr["PhysicalWeight"].ToString();
            txtPhysicalWeight2.Text = dr["PhysicalWeighTo"].ToString();
            txtProductionYear.Text = dr["ProductionYear"].ToString();
            txtShed.Text = dr["Shed"].ToString();
            txtShed2.Text = dr["Shed2"].ToString();
            txtStackNo.Text = dr["StackNumber"].ToString();
            txtStackNo2.Text = dr["StackNumber2"].ToString();
            txtSymbol.Text = dr["Symbol"].ToString();
            txtSystemCount.Text = dr["SystemCount"].ToString();
            txtSystemCount2.Text = dr["SystemCountTo"].ToString();
            txtSystemWeight.Text = dr["SystemWeight"].ToString();
            txtSystemWeight2.Text = dr["SystemWeighTo"].ToString();
            txtTransferDate.Text = dr["TransitionDate"].ToString();
            txtWarehouse.Text = dr["WareHouse"].ToString();
                    

        }


    }
}
