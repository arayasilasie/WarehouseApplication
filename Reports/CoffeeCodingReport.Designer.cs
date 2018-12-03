namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for CoffeeCodingReport.
    /// </summary>
    partial class CoffeeCodingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoffeeCodingReport));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.brcTrackingNo = new DataDynamics.ActiveReports.Barcode();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.txtDateGenerated = new DataDynamics.ActiveReports.TextBox();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.txtOrigin = new DataDynamics.ActiveReports.TextBox();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.txtDateCoded = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.txtCode = new DataDynamics.ActiveReports.TextBox();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtName = new DataDynamics.ActiveReports.TextBox();
            this.chkChecked = new DataDynamics.ActiveReports.CheckBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCoded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.picture1,
            this.brcTrackingNo,
            this.label7,
            this.txtDateGenerated,
            this.label4,
            this.txtOrigin,
            this.label2,
            this.txtDateCoded,
            this.label3,
            this.txtCode,
            this.label5,
            this.label6,
            this.line2});
            this.pageHeader.Height = 3.021583F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label1
            // 
            this.label1.Height = 0.315F;
            this.label1.HyperLink = null;
            this.label1.Left = 2.001F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Verdana; font-size: 15pt; ddo-char-set: 0";
            this.label1.Text = "Coding Ticket";
            this.label1.Top = 0.395F;
            this.label1.Width = 1.801F;
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
            this.picture1.Width = 1.448F;
            // 
            // brcTrackingNo
            // 
            this.brcTrackingNo.Font = new System.Drawing.Font("Courier New", 8F);
            this.brcTrackingNo.Height = 0.5F;
            this.brcTrackingNo.Left = 4.322F;
            this.brcTrackingNo.Name = "brcTrackingNo";
            this.brcTrackingNo.Text = "barcode1";
            this.brcTrackingNo.Top = 0.21F;
            this.brcTrackingNo.Width = 1.5F;
            // 
            // label7
            // 
            this.label7.Height = 0.1979167F;
            this.label7.HyperLink = null;
            this.label7.Left = 3.884001F;
            this.label7.Name = "label7";
            this.label7.Style = "";
            this.label7.Text = "Date Generated:";
            this.label7.Top = 1.064F;
            this.label7.Width = 1.061F;
            // 
            // txtDateGenerated
            // 
            this.txtDateGenerated.Height = 0.2F;
            this.txtDateGenerated.Left = 5.042F;
            this.txtDateGenerated.Name = "txtDateGenerated";
            this.txtDateGenerated.Text = null;
            this.txtDateGenerated.Top = 1.064F;
            this.txtDateGenerated.Width = 1F;
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 0.28F;
            this.label4.Name = "label4";
            this.label4.Style = "";
            this.label4.Text = "Origin:";
            this.label4.Top = 2.325F;
            this.label4.Width = 1.146F;
            // 
            // txtOrigin
            // 
            this.txtOrigin.Height = 0.2F;
            this.txtOrigin.Left = 1.448F;
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Text = null;
            this.txtOrigin.Top = 2.325F;
            this.txtOrigin.Width = 4.312F;
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.28F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Date:";
            this.label2.Top = 1.8F;
            this.label2.Width = 1.146F;
            // 
            // txtDateCoded
            // 
            this.txtDateCoded.Height = 0.2F;
            this.txtDateCoded.Left = 1.448F;
            this.txtDateCoded.Name = "txtDateCoded";
            this.txtDateCoded.Text = null;
            this.txtDateCoded.Top = 1.8F;
            this.txtDateCoded.Width = 4.312F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.28F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "Code:";
            this.label3.Top = 2.0625F;
            this.label3.Width = 1.146F;
            // 
            // txtCode
            // 
            this.txtCode.Height = 0.2F;
            this.txtCode.Left = 1.448F;
            this.txtCode.Name = "txtCode";
            this.txtCode.Text = null;
            this.txtCode.Top = 2.0625F;
            this.txtCode.Width = 4.312F;
            // 
            // label5
            // 
            this.label5.Height = 0.1875F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.193F;
            this.label5.Name = "label5";
            this.label5.Style = "";
            this.label5.Text = "Name ";
            this.label5.Top = 2.737F;
            this.label5.Width = 1.52F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 2.114F;
            this.label6.Name = "label6";
            this.label6.Style = "";
            this.label6.Text = "Is Lab coordinator?";
            this.label6.Top = 2.724F;
            this.label6.Width = 1.583F;
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0.137F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 2.658F;
            this.line2.Width = 5.996996F;
            this.line2.X1 = 0.137F;
            this.line2.X2 = 6.133996F;
            this.line2.Y1 = 2.658F;
            this.line2.Y2 = 2.658F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtName,
            this.chkChecked});
            this.detail.Height = 0.2395831F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtName
            // 
            this.txtName.DataField = "Grader";
            this.txtName.Height = 0.221F;
            this.txtName.Left = 0.28F;
            this.txtName.Name = "txtName";
            this.txtName.Text = null;
            this.txtName.Top = 0F;
            this.txtName.Width = 1.635F;
            // 
            // chkChecked
            // 
            this.chkChecked.Checked = true;
            this.chkChecked.DataField = "IsSuper";
            this.chkChecked.Height = 0.2F;
            this.chkChecked.Left = 2.635F;
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Style = "";
            this.chkChecked.Text = "";
            this.chkChecked.Top = 0F;
            this.chkChecked.Width = 0.1770001F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // CoffeeCodingReport
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
            this.FetchData += new DataDynamics.ActiveReports.ActiveReport.FetchEventHandler(this.CoffeeCodingReport_FetchData);
            this.ReportStart += new System.EventHandler(this.CoffeeCodingReport_ReportStart);
            this.ReportEnd += new System.EventHandler(this.CoffeeCodingReport_ReportEnd);
            this.DataInitialize += new System.EventHandler(this.CoffeeCodingReport_DataInitialize);
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCoded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Barcode brcTrackingNo;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox txtDateGenerated;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox txtDateCoded;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox txtCode;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.TextBox txtOrigin;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox txtName;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.CheckBox chkChecked;
    }
}
