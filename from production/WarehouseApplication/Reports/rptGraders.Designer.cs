namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGraders.
    /// </summary>
    partial class rptGraders
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptGraders));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtName = new DataDynamics.ActiveReports.TextBox();
            this.ckhIsSup = new DataDynamics.ActiveReports.CheckBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckhIsSup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1});
            this.pageHeader.Height = 0.25F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label1
            // 
            this.label1.Height = 0.2F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.208F;
            this.label1.Name = "label1";
            this.label1.Style = "";
            this.label1.Text = "Cuppers:";
            this.label1.Top = 0.01F;
            this.label1.Width = 1F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtName,
            this.ckhIsSup});
            this.detail.Height = 0.3124999F;
            this.detail.Name = "detail";
            // 
            // txtName
            // 
            this.txtName.DataField = "GraderName";
            this.txtName.Height = 0.2F;
            this.txtName.Left = 0.219F;
            this.txtName.Name = "txtName";
            this.txtName.Text = null;
            this.txtName.Top = 0.063F;
            this.txtName.Width = 1.635F;
            // 
            // ckhIsSup
            // 
            this.ckhIsSup.DataField = "IsSupervisor";
            this.ckhIsSup.Height = 0.2F;
            this.ckhIsSup.Left = 2.386F;
            this.ckhIsSup.Name = "ckhIsSup";
            this.ckhIsSup.Style = "";
            this.ckhIsSup.Text = "";
            this.ckhIsSup.Top = 0.063F;
            this.ckhIsSup.Width = 0.1659999F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label2,
            this.label3});
            this.groupHeader1.Height = 0.3125F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // label2
            // 
            this.label2.Height = 0.1875F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.219F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Name ";
            this.label2.Top = 0.072F;
            this.label2.Width = 1.52F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 2.104F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "is Supervisor";
            this.label3.Top = 0.072F;
            this.label3.Width = 1F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptGraders
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.ReportEnd += new System.EventHandler(this.rptGraders_ReportEnd);
            this.DataInitialize += new System.EventHandler(this.rptGraders_DataInitialize);
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckhIsSup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox txtName;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.CheckBox ckhIsSup;
    }
}
