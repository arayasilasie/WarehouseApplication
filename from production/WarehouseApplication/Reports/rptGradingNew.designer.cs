namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGradingNew.
    /// </summary>
    partial class rptGradingNew
    {
        private DataDynamics.ActiveReports.Detail detail;

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptGradingNew));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.lblGradingCode = new DataDynamics.ActiveReports.Label();
            this.label23 = new DataDynamics.ActiveReports.Label();
            this.label29 = new DataDynamics.ActiveReports.Label();
            this.subReport1 = new DataDynamics.ActiveReports.SubReport();
            this.pageBreak1 = new DataDynamics.ActiveReports.PageBreak();
            ((System.ComponentModel.ISupportInitialize)(this.lblGradingCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblGradingCode,
            this.label23,
            this.label29,
            this.subReport1,
            this.pageBreak1});
            this.detail.Height = 1.39575F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // lblGradingCode
            // 
            this.lblGradingCode.DataField = "GradingCode";
            this.lblGradingCode.Height = 0.2F;
            this.lblGradingCode.HyperLink = null;
            this.lblGradingCode.Left = 0F;
            this.lblGradingCode.Name = "lblGradingCode";
            this.lblGradingCode.Style = "font-family: Palatino Linotype; font-size: 11pt; font-weight: bold";
            this.lblGradingCode.Text = "";
            this.lblGradingCode.Top = 0F;
            this.lblGradingCode.Width = 1.593F;
            // 
            // label23
            // 
            this.label23.Height = 0.2F;
            this.label23.HyperLink = null;
            this.label23.Left = 0.8330001F;
            this.label23.Name = "label23";
            this.label23.Style = "font-family: Verdana; font-size: 10pt; font-weight: bold; ddo-char-set: 0";
            this.label23.Text = "Grading Factor Name";
            this.label23.Top = 0.25F;
            this.label23.Width = 1.761F;
            // 
            // label29
            // 
            this.label29.Height = 0.2F;
            this.label29.HyperLink = null;
            this.label29.Left = 3.822F;
            this.label29.Name = "label29";
            this.label29.Style = "font-family: Verdana; font-size: 10pt; font-weight: bold; ddo-char-set: 0";
            this.label29.Text = "Value";
            this.label29.Top = 0.25F;
            this.label29.Width = 1.761F;
            // 
            // subReport1
            // 
            this.subReport1.CloseBorder = false;
            this.subReport1.Height = 0.6459999F;
            this.subReport1.Left = 0.8329999F;
            this.subReport1.Name = "subReport1";
            this.subReport1.Report = null;
            this.subReport1.ReportName = "subReport1";
            this.subReport1.Top = 0.594F;
            this.subReport1.Width = 4.354F;
            // 
            // pageBreak1
            // 
            this.pageBreak1.Height = 0.2F;
            this.pageBreak1.Left = 0F;
            this.pageBreak1.Name = "pageBreak1";
            this.pageBreak1.Size = new System.Drawing.SizeF(6.5F, 0.2F);
            this.pageBreak1.Top = 1.312F;
            this.pageBreak1.Width = 6.5F;
            // 
            // rptGradingNew
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.lblGradingCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label lblGradingCode;
        private DataDynamics.ActiveReports.Label label23;
        private DataDynamics.ActiveReports.Label label29;
        private DataDynamics.ActiveReports.SubReport subReport1;
        private DataDynamics.ActiveReports.PageBreak pageBreak1;
    }
}
