namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptInspectionTestResultDetail.
    /// </summary>
    partial class rptInspectionTestResultDetail
    {
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptInspectionTestResultDetail));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.reportHeader1 = new DataDynamics.ActiveReports.ReportHeader();
            this.reportFooter1 = new DataDynamics.ActiveReports.ReportFooter();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0.1979167F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2});
            this.detail.Height = 0.1770834F;
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.DataField = "GradingFactorName";
            this.textBox1.Height = 0.1979167F;
            this.textBox1.Left = 0.04700001F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Text = "textBox1";
            this.textBox1.Top = 0F;
            this.textBox1.Width = 2.735F;
            // 
            // textBox2
            // 
            this.textBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.DataField = "ReceivedValue";
            this.textBox2.Height = 0.1979167F;
            this.textBox2.Left = 2.787F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Text = "textBox1";
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.879F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // label3
            // 
            this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.ThickSolid;
            this.label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Height = 0.1999999F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.047F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold";
            this.label3.Text = "Grading Factor Name:";
            this.label3.Top = 0F;
            this.label3.Width = 2.735F;
            // 
            // label1
            // 
            this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.ThickSolid;
            this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Height = 0.1999999F;
            this.label1.HyperLink = null;
            this.label1.Left = 2.787F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold";
            this.label1.Text = "Value:";
            this.label1.Top = 0F;
            this.label1.Width = 1.879F;
            // 
            // reportHeader1
            // 
            this.reportHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label3,
            this.label1});
            this.reportHeader1.Height = 0.1979167F;
            this.reportHeader1.Name = "reportHeader1";
            // 
            // reportFooter1
            // 
            this.reportFooter1.Height = 0.25F;
            this.reportFooter1.Name = "reportFooter1";
            // 
            // rptInspectionTestResultDetail
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.reportHeader1);
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.Sections.Add(this.reportFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.ReportHeader reportHeader1;
        private DataDynamics.ActiveReports.ReportFooter reportFooter1;
    }
}
