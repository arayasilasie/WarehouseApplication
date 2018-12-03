namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGradingFactorsResultReport.
    /// </summary>
    partial class rptGradingFactorsResultReport
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptGradingFactorsResultReport));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.lblGradingFactor = new DataDynamics.ActiveReports.Label();
            this.lblValue = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lblGradingFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblGradingFactor,
            this.lblValue});
            this.detail.Height = 0.2916667F;
            this.detail.Name = "detail";
            // 
            // lblGradingFactor
            // 
            this.lblGradingFactor.DataField = "GradingFactorName";
            this.lblGradingFactor.Height = 0.168F;
            this.lblGradingFactor.HyperLink = null;
            this.lblGradingFactor.Left = 0F;
            this.lblGradingFactor.Name = "lblGradingFactor";
            this.lblGradingFactor.Style = "font-family: Verdana; font-size: 9pt";
            this.lblGradingFactor.Text = "";
            this.lblGradingFactor.Top = 0F;
            this.lblGradingFactor.Width = 3.042F;
            // 
            // lblValue
            // 
            this.lblValue.DataField = "ReceivedValue";
            this.lblValue.Height = 0.168F;
            this.lblValue.HyperLink = null;
            this.lblValue.Left = 3.292F;
            this.lblValue.Name = "lblValue";
            this.lblValue.Style = "font-family: Verdana; font-size: 9pt";
            this.lblValue.Text = "";
            this.lblValue.Top = 0F;
            this.lblValue.Width = 1F;
            // 
            // rptGradingFactorsResultReport
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
            ((System.ComponentModel.ISupportInitialize)(this.lblGradingFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label lblGradingFactor;
        private DataDynamics.ActiveReports.Label lblValue;
    }
}
