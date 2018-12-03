namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGRNService.
    /// </summary>
    partial class rptGRNService
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptGRNService));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.lblServiceName = new DataDynamics.ActiveReports.Label();
            this.lblQty = new DataDynamics.ActiveReports.Label();
            this.reportHeader1 = new DataDynamics.ActiveReports.ReportHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.reportFooter1 = new DataDynamics.ActiveReports.ReportFooter();
            this.line2 = new DataDynamics.ActiveReports.Line();
            ((System.ComponentModel.ISupportInitialize)(this.lblServiceName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblServiceName,
            this.lblQty});
            this.detail.Height = 0.25F;
            this.detail.Name = "detail";
            // 
            // lblServiceName
            // 
            this.lblServiceName.DataField = "ServiceName";
            this.lblServiceName.Height = 0.2F;
            this.lblServiceName.HyperLink = null;
            this.lblServiceName.Left = 0.302F;
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Style = "font-size: 9pt; font-family: Verdana";
            this.lblServiceName.Text = "";
            this.lblServiceName.Top = 0.01F;
            this.lblServiceName.Width = 2.52F;
            // 
            // lblQty
            // 
            this.lblQty.DataField = "Qty";
            this.lblQty.Height = 0.2F;
            this.lblQty.HyperLink = null;
            this.lblQty.Left = 3.093F;
            this.lblQty.Name = "lblQty";
            this.lblQty.Style = "font-size: 9pt; font-family: Verdana; text-align: center";
            this.lblQty.Text = "";
            this.lblQty.Top = 0.01F;
            this.lblQty.Width = 0.6560001F;
            // 
            // reportHeader1
            // 
            this.reportHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.label2,
            this.label3,
            this.line1});
            this.reportHeader1.Height = 0.61475F;
            this.reportHeader1.Name = "reportHeader1";
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.094F;
            this.label1.Name = "label1";
            this.label1.Style = "font-weight: bold; font-family: Verdana";
            this.label1.Text = "GRN Service";
            this.label1.Top = 0.04F;
            this.label1.Width = 1.729F;
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.302F;
            this.label2.Name = "label2";
            this.label2.Style = "font-size: 9pt; font-family: Verdana; font-weight: bold";
            this.label2.Text = "Service Name:";
            this.label2.Top = 0.366F;
            this.label2.Width = 2.52F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 3.041F;
            this.label3.Name = "label3";
            this.label3.Style = "font-size: 9pt; font-family: Verdana; font-weight: bold";
            this.label3.Text = "Quantity:";
            this.label3.Top = 0.366F;
            this.label3.Width = 1F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.09333372F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0.5970001F;
            this.line1.Width = 4.291667F;
            this.line1.X1 = 4.385F;
            this.line1.X2 = 0.09333372F;
            this.line1.Y1 = 0.5970001F;
            this.line1.Y2 = 0.5970001F;
            // 
            // reportFooter1
            // 
            this.reportFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.line2});
            this.reportFooter1.Height = 0.09375F;
            this.reportFooter1.Name = "reportFooter1";
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0.09375F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.05208333F;
            this.line2.Width = 4.333333F;
            this.line2.X1 = 4.427083F;
            this.line2.X2 = 0.09375F;
            this.line2.Y1 = 0.05208333F;
            this.line2.Y2 = 0.05208333F;
            // 
            // rptGRNService
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.reportHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.reportFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.FetchData += new DataDynamics.ActiveReports.ActiveReport.FetchEventHandler(this.rptGRNService_FetchData);
            this.ReportEnd += new System.EventHandler(this.rptGRNService_ReportEnd);
            this.DataInitialize += new System.EventHandler(this.rptGRNService_DataInitialize);
            this.ReportStart += new System.EventHandler(this.rptGRNService_ReportStart);
            ((System.ComponentModel.ISupportInitialize)(this.lblServiceName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label lblServiceName;
        private DataDynamics.ActiveReports.Label lblQty;
        private DataDynamics.ActiveReports.ReportHeader reportHeader1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.ReportFooter reportFooter1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;

    }
}
