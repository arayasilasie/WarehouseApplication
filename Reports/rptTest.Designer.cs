namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptTest.
    /// </summary>
    partial class rptTest
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptTest));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.textBox4 = new DataDynamics.ActiveReports.TextBox();
            this.textBox5 = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0.25F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.detail.Height = 3.239583F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox1
            // 
            this.textBox1.DataField = "ClientName";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 0.166F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Text = "textBox1";
            this.textBox1.Top = 0.114F;
            this.textBox1.Width = 4.177001F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "ComodityName";
            this.textBox2.Height = 0.2F;
            this.textBox2.Left = 0.166F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Text = "textBox1";
            this.textBox2.Top = 0.3765F;
            this.textBox2.Width = 4.177001F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "CommodityGradeId";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 0.166F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Text = "textBox1";
            this.textBox3.Top = 0.6390001F;
            this.textBox3.Width = 4.177001F;
            // 
            // textBox4
            // 
            this.textBox4.DataField = "ProductionYear";
            this.textBox4.Height = 0.2F;
            this.textBox4.Left = 0.166F;
            this.textBox4.Name = "textBox4";
            this.textBox4.Text = "textBox1";
            this.textBox4.Top = 0.9015F;
            this.textBox4.Width = 4.177001F;
            // 
            // textBox5
            // 
            this.textBox5.DataField = "GradingCode";
            this.textBox5.Height = 0.2F;
            this.textBox5.Left = 0.166F;
            this.textBox5.Name = "textBox5";
            this.textBox5.Text = "textBox1";
            this.textBox5.Top = 1.164F;
            this.textBox5.Width = 4.177001F;
            // 
            // rptTest
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
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox textBox2;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.TextBox textBox4;
        private DataDynamics.ActiveReports.TextBox textBox5;
    }
}
