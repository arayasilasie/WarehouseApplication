using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using GradingBussiness;

namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGenerate.
    /// </summary>
    public partial class rptGenerate : DataDynamics.ActiveReports.ActiveReport
    {

        public rptGenerate()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        private void pageHeader_Format(object sender, EventArgs e)
        {

        }

        private void rptGenerate_PageStart(object sender, EventArgs e)
        {
            this.txtDateGenerated.Text = DateTime.Now.ToString("dd MMM-yyyy");
            if (textBox3.Text == Convert.ToInt32(SamplingBussiness.SamplingStatus.SeparationRequested).ToString())
            {
                label8.Visible = true;
                textBox2.Visible = true;
                textBox2.Text = SamplingBussiness.SamplingStatus.SeparationRequested.ToString();
            }

            //textBox2.Text = (chkHasChemicalOrPetrol.Checked ? chkHasChemicalOrPetrol.Text : "") +
            //    (textBox2.Text.Trim().Length > 0 ? Environment.NewLine : "" ) + textBox2.Text;
            //textBox2.Text = (chkHasLiveInsect.Checked ? chkHasLiveInsect.Text : "") +
            //    (textBox2.Text.Trim().Length > 0 ? Environment.NewLine : "") + textBox2.Text;
            //textBox2.Text = (chkHasMoldOrFungus.Checked ? chkHasMoldOrFungus.Text : "") +
            //    (textBox2.Text.Trim().Length > 0 ? Environment.NewLine : "") + textBox2.Text;

            //if (textBox2.Text.Trim().Length > 0)
            //{
            //    label8.Visible = true;
            //    textBox2.Visible = true;
            //}
        }

        private void detail_Format(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            if (lblGINID.Text != null)
                dt = GradingModel.GetComodityClass(new Guid(lblGINID.Text));


            if (dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count; i > 0; i--)
                {
                    if (!dt.Rows[i - 1]["Class"].ToString().EndsWith("Q"))
                        if (i == dt.Rows.Count)
                            txtGradingClass.Text = dt.Rows[i - 1]["Class"].ToString();
                        else if (txtGradingClass.Text == string.Empty)
                            txtGradingClass.Text = dt.Rows[i - 1]["Class"].ToString();
                        else
                            txtGradingClass.Text = txtGradingClass.Text + " ; " + dt.Rows[i - 1]["Class"].ToString();
                }
            }
        }

        private void pageHeader_BeforePrint(object sender, EventArgs e)
        {

        }
    }
}
