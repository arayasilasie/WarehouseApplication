namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for GradingResultReport.
    /// </summary>
    partial class GradingResultReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GradingResultReport));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.txtDateGenerated = new DataDynamics.ActiveReports.TextBox();
            this.brcTrackingNo = new DataDynamics.ActiveReports.Barcode();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.txtResultDate = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.txtTrackingNo = new DataDynamics.ActiveReports.TextBox();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.txtGradeReceived = new DataDynamics.ActiveReports.TextBox();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.checkBox1 = new DataDynamics.ActiveReports.CheckBox();
            this.checkBox2 = new DataDynamics.ActiveReports.CheckBox();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.line3 = new DataDynamics.ActiveReports.Line();
            this.line4 = new DataDynamics.ActiveReports.Line();
            this.line5 = new DataDynamics.ActiveReports.Line();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.txtCode = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResultDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGradeReceived)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.picture1,
            this.label1,
            this.label7,
            this.txtDateGenerated,
            this.brcTrackingNo});
            this.pageHeader.Height = 1.344F;
            this.pageHeader.Name = "pageHeader";
            // 
            // picture1
            // 
            this.picture1.Height = 1.344F;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0F;
            this.picture1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0F;
            this.picture1.Width = 1.508F;
            // 
            // label1
            // 
            this.label1.Height = 0.315F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.638F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Verdana; font-size: 15pt; ddo-char-set: 0";
            this.label1.Text = "Grading Result Report";
            this.label1.Top = 0.271F;
            this.label1.Width = 2.437F;
            // 
            // label7
            // 
            this.label7.Height = 0.1979167F;
            this.label7.HyperLink = null;
            this.label7.Left = 3.792001F;
            this.label7.Name = "label7";
            this.label7.Style = "";
            this.label7.Text = "Date Generated:";
            this.label7.Top = 0.9480001F;
            this.label7.Width = 1.061F;
            // 
            // txtDateGenerated
            // 
            this.txtDateGenerated.Height = 0.2F;
            this.txtDateGenerated.Left = 4.95F;
            this.txtDateGenerated.Name = "txtDateGenerated";
            this.txtDateGenerated.Text = null;
            this.txtDateGenerated.Top = 0.9480001F;
            this.txtDateGenerated.Width = 1F;
            // 
            // brcTrackingNo
            // 
            this.brcTrackingNo.Font = new System.Drawing.Font("Courier New", 8F);
            this.brcTrackingNo.Height = 0.5F;
            this.brcTrackingNo.Left = 4.45F;
            this.brcTrackingNo.Name = "brcTrackingNo";
            this.brcTrackingNo.Text = "barcode1";
            this.brcTrackingNo.Top = 0.148F;
            this.brcTrackingNo.Width = 1.5F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label2,
            this.txtResultDate,
            this.label3,
            this.txtTrackingNo,
            this.label4,
            this.txtGradeReceived,
            this.label5,
            this.line1,
            this.label6,
            this.line2,
            this.checkBox1,
            this.checkBox2,
            this.label8,
            this.line3,
            this.line4,
            this.line5,
            this.label9,
            this.txtCode});
            this.detail.Height = 2.75F;
            this.detail.Name = "detail";
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.4700001F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Result Received Date:";
            this.label2.Top = 0.125F;
            this.label2.Width = 1.146F;
            // 
            // txtResultDate
            // 
            this.txtResultDate.Height = 0.2F;
            this.txtResultDate.Left = 1.638F;
            this.txtResultDate.Name = "txtResultDate";
            this.txtResultDate.Text = null;
            this.txtResultDate.Top = 0.125F;
            this.txtResultDate.Width = 4.312F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.4700001F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "Tracking No:";
            this.label3.Top = 0.3875F;
            this.label3.Width = 1.146F;
            // 
            // txtTrackingNo
            // 
            this.txtTrackingNo.Height = 0.2F;
            this.txtTrackingNo.Left = 1.638F;
            this.txtTrackingNo.Name = "txtTrackingNo";
            this.txtTrackingNo.Text = null;
            this.txtTrackingNo.Top = 0.3875F;
            this.txtTrackingNo.Width = 4.312F;
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 0.47F;
            this.label4.Name = "label4";
            this.label4.Style = "";
            this.label4.Text = "Grade Received:";
            this.label4.Top = 0.8800001F;
            this.label4.Width = 1.146F;
            // 
            // txtGradeReceived
            // 
            this.txtGradeReceived.Height = 0.2F;
            this.txtGradeReceived.Left = 1.638F;
            this.txtGradeReceived.Name = "txtGradeReceived";
            this.txtGradeReceived.Text = null;
            this.txtGradeReceived.Top = 0.8800001F;
            this.txtGradeReceived.Width = 4.312F;
            // 
            // label5
            // 
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 2.011F;
            this.label5.Name = "label5";
            this.label5.Style = "";
            this.label5.Text = "Signature:";
            this.label5.Top = 2.196F;
            this.label5.Width = 0.7700001F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 2.927001F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 2.396F;
            this.line1.Width = 2.718999F;
            this.line1.X1 = 2.927001F;
            this.line1.X2 = 5.646F;
            this.line1.Y1 = 2.396F;
            this.line1.Y2 = 2.396F;
            // 
            // label6
            // 
            this.label6.Height = 0.2000001F;
            this.label6.HyperLink = null;
            this.label6.Left = 2.011F;
            this.label6.Name = "label6";
            this.label6.Style = "";
            this.label6.Text = "Date:";
            this.label6.Top = 2.4585F;
            this.label6.Width = 0.77F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 2.927001F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 2.639F;
            this.line2.Width = 2.718999F;
            this.line2.X1 = 2.927001F;
            this.line2.X2 = 5.646F;
            this.line2.Y1 = 2.639F;
            this.line2.Y2 = 2.639F;
            // 
            // checkBox1
            // 
            this.checkBox1.Height = 0.2F;
            this.checkBox1.Left = 2.011F;
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Style = "";
            this.checkBox1.Text = "Result Accepted";
            this.checkBox1.Top = 1.908F;
            this.checkBox1.Width = 1.479F;
            // 
            // checkBox2
            // 
            this.checkBox2.Height = 0.2F;
            this.checkBox2.Left = 3.73F;
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Style = "";
            this.checkBox2.Text = "Result Rejected";
            this.checkBox2.Top = 1.908F;
            this.checkBox2.Width = 1.479F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 0.47F;
            this.label8.Name = "label8";
            this.label8.Style = "";
            this.label8.Text = "Remark:";
            this.label8.Top = 1.1425F;
            this.label8.Width = 1.146F;
            // 
            // line3
            // 
            this.line3.Height = 0F;
            this.line3.Left = 1.638F;
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 1.343F;
            this.line3.Width = 4.312F;
            this.line3.X1 = 1.638F;
            this.line3.X2 = 5.95F;
            this.line3.Y1 = 1.343F;
            this.line3.Y2 = 1.343F;
            // 
            // line4
            // 
            this.line4.Height = 0F;
            this.line4.Left = 1.638F;
            this.line4.LineWeight = 1F;
            this.line4.Name = "line4";
            this.line4.Top = 1.576F;
            this.line4.Width = 4.312F;
            this.line4.X1 = 1.638F;
            this.line4.X2 = 5.95F;
            this.line4.Y1 = 1.576F;
            this.line4.Y2 = 1.576F;
            // 
            // line5
            // 
            this.line5.Height = 0F;
            this.line5.Left = 1.638F;
            this.line5.LineWeight = 1F;
            this.line5.Name = "line5";
            this.line5.Top = 1.789F;
            this.line5.Width = 4.312F;
            this.line5.X1 = 1.638F;
            this.line5.X2 = 5.95F;
            this.line5.Y1 = 1.789F;
            this.line5.Y2 = 1.789F;
            // 
            // label9
            // 
            this.label9.Height = 0.2F;
            this.label9.HyperLink = null;
            this.label9.Left = 0.4700001F;
            this.label9.Name = "label9";
            this.label9.Style = "";
            this.label9.Text = "Code:";
            this.label9.Top = 0.65F;
            this.label9.Width = 1.146F;
            // 
            // txtCode
            // 
            this.txtCode.Height = 0.2F;
            this.txtCode.Left = 1.638F;
            this.txtCode.Name = "txtCode";
            this.txtCode.Text = null;
            this.txtCode.Top = 0.65F;
            this.txtCode.Width = 4.312F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // GradingResultReport
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.GradingResultReport_ReportStart);
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResultDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGradeReceived)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox txtDateGenerated;
        private DataDynamics.ActiveReports.Barcode brcTrackingNo;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox txtResultDate;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox txtTrackingNo;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.TextBox txtGradeReceived;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.CheckBox checkBox1;
        private DataDynamics.ActiveReports.CheckBox checkBox2;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.Line line3;
        private DataDynamics.ActiveReports.Line line4;
        private DataDynamics.ActiveReports.Line line5;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.TextBox txtCode;
    }
}
