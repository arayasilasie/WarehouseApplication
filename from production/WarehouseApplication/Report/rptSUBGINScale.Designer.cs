namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptSUBGINScale.
    /// </summary>
    partial class rptSUBGINScale
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSUBGINScale));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtWeightInKg1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.reportHeader1 = new DataDynamics.ActiveReports.ReportHeader();
            this.label13 = new DataDynamics.ActiveReports.Label();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.reportFooter1 = new DataDynamics.ActiveReports.ReportFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.txtTWeight = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtWeightInKg1,
            this.textBox3});
            this.detail.Height = 0.1895F;
            this.detail.Name = "detail";
            // 
            // txtWeightInKg1
            // 
            this.txtWeightInKg1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.DataField = "NetWeight";
            this.txtWeightInKg1.Height = 0.1875F;
            this.txtWeightInKg1.Left = 1.54F;
            this.txtWeightInKg1.Name = "txtWeightInKg1";
            this.txtWeightInKg1.Style = "text-align: right";
            this.txtWeightInKg1.Text = "txtWeightInKg1";
            this.txtWeightInKg1.Top = 3.49246E-09F;
            this.txtWeightInKg1.Width = 1.814F;
            // 
            // textBox3
            // 
            this.textBox3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox3.DataField = "ScaleTicketNumber";
            this.textBox3.Height = 0.1875F;
            this.textBox3.Left = 0.083F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Text = "txtScaleTicketNo";
            this.textBox3.Top = 0F;
            this.textBox3.Width = 1.457F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // reportHeader1
            // 
            this.reportHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label13,
            this.textBox2});
            this.reportHeader1.Height = 0.358F;
            this.reportHeader1.Name = "reportHeader1";
            // 
            // label13
            // 
            this.label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Height = 0.354F;
            this.label13.HyperLink = "";
            this.label13.Left = 1.54F;
            this.label13.Name = "label13";
            this.label13.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 0";
            this.label13.Text = "Weight Currently  Issued";
            this.label13.Top = 0.004F;
            this.label13.Width = 1.814F;
            // 
            // textBox2
            // 
            this.textBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Height = 0.354F;
            this.textBox2.Left = 0.083F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Style = "font-weight: bold; text-align: center";
            this.textBox2.Text = "Scale Ticket No.";
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.457F;
            // 
            // reportFooter1
            // 
            this.reportFooter1.Height = 0F;
            this.reportFooter1.Name = "reportFooter1";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Height = 0F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label3,
            this.txtTWeight});
            this.groupFooter1.Height = 0.202F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // label3
            // 
            this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.083F;
            this.label3.Name = "label3";
            this.label3.Style = "font-weight: bold; text-align: right";
            this.label3.Text = "Total Weight : ";
            this.label3.Top = 0F;
            this.label3.Width = 1.457F;
            // 
            // txtTWeight
            // 
            this.txtTWeight.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTWeight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTWeight.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTWeight.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTWeight.DataField = "NetWeight";
            this.txtTWeight.DistinctField = "NetWeight";
            this.txtTWeight.Height = 0.2F;
            this.txtTWeight.Left = 1.54F;
            this.txtTWeight.Name = "txtTWeight";
            this.txtTWeight.OutputFormat = resources.GetString("txtTWeight.OutputFormat");
            this.txtTWeight.Style = "text-align: right";
            this.txtTWeight.SummaryGroup = "groupHeader1";
            this.txtTWeight.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.txtTWeight.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.txtTWeight.Text = null;
            this.txtTWeight.Top = 3.49246E-09F;
            this.txtTWeight.Width = 1.814F;
            // 
            // rptSUBGINScale
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 3.41675F;
            this.Sections.Add(this.reportHeader1);
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.Sections.Add(this.reportFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.ReportHeader reportHeader1;
        private DataDynamics.ActiveReports.ReportFooter reportFooter1;
        private DataDynamics.ActiveReports.Label label13;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox txtWeightInKg1;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox txtTWeight;
    }
}
