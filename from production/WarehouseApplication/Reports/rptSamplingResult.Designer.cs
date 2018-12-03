namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for rptSamplingResult.
    /// </summary>
    partial class rptSamplingResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptSamplingResult));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.txtDateGenerated = new DataDynamics.ActiveReports.TextBox();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.brcTrackingNo = new DataDynamics.ActiveReports.Barcode();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.txtDateSampled = new DataDynamics.ActiveReports.TextBox();
            this.txtCode = new DataDynamics.ActiveReports.TextBox();
            this.txtPlateNo = new DataDynamics.ActiveReports.TextBox();
            this.label13 = new DataDynamics.ActiveReports.Label();
            this.txtTrailerPlateNo = new DataDynamics.ActiveReports.TextBox();
            this.txtNoBags = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateSampled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrailerPlateNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoBags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.label7,
            this.txtDateGenerated,
            this.picture1,
            this.brcTrackingNo});
            this.pageHeader.Height = 1.37525F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label1
            // 
            this.label1.Height = 0.5650001F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.782F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Verdana; font-size: 15pt; text-align: center; text-decoration: none;" +
                " ddo-char-set: 0";
            this.label1.Text = "Sampling  Result Ticket";
            this.label1.Top = 0.343F;
            this.label1.Width = 2.249F;
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
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label2,
            this.label3,
            this.label5,
            this.label6,
            this.txtDateSampled,
            this.txtCode,
            this.txtPlateNo,
            this.label13,
            this.txtTrailerPlateNo,
            this.txtNoBags});
            this.detail.Height = 1.364778F;
            this.detail.Name = "detail";
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Date:";
            this.label2.Top = 0F;
            this.label2.Width = 1.146F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "Sample Code:";
            this.label3.Top = 0.2625F;
            this.label3.Width = 1.146F;
            // 
            // label5
            // 
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.022F;
            this.label5.Name = "label5";
            this.label5.Style = "";
            this.label5.Text = "Plate No. :";
            this.label5.Top = 0.5375001F;
            this.label5.Width = 1.146F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.02200008F;
            this.label6.Name = "label6";
            this.label6.Style = "";
            this.label6.Text = "Number of Bags:";
            this.label6.Top = 1.1F;
            this.label6.Width = 1.146F;
            // 
            // txtDateSampled
            // 
            this.txtDateSampled.Height = 0.2F;
            this.txtDateSampled.Left = 1.168F;
            this.txtDateSampled.Name = "txtDateSampled";
            this.txtDateSampled.Text = null;
            this.txtDateSampled.Top = 0F;
            this.txtDateSampled.Width = 4.312F;
            // 
            // txtCode
            // 
            this.txtCode.Height = 0.2F;
            this.txtCode.Left = 1.168F;
            this.txtCode.Name = "txtCode";
            this.txtCode.Text = null;
            this.txtCode.Top = 0.2625F;
            this.txtCode.Width = 1.687F;
            // 
            // txtPlateNo
            // 
            this.txtPlateNo.Height = 0.2F;
            this.txtPlateNo.Left = 1.19F;
            this.txtPlateNo.Name = "txtPlateNo";
            this.txtPlateNo.Text = null;
            this.txtPlateNo.Top = 0.5375001F;
            this.txtPlateNo.Width = 1.687F;
            // 
            // label13
            // 
            this.label13.Height = 0.2F;
            this.label13.HyperLink = null;
            this.label13.Left = 0.022F;
            this.label13.Name = "label13";
            this.label13.Style = "";
            this.label13.Text = "Trailer Plate No. :";
            this.label13.Top = 0.818F;
            this.label13.Width = 1.146F;
            // 
            // txtTrailerPlateNo
            // 
            this.txtTrailerPlateNo.Height = 0.2F;
            this.txtTrailerPlateNo.Left = 1.19F;
            this.txtTrailerPlateNo.Name = "txtTrailerPlateNo";
            this.txtTrailerPlateNo.Text = null;
            this.txtTrailerPlateNo.Top = 0.818F;
            this.txtTrailerPlateNo.Width = 1.687F;
            // 
            // txtNoBags
            // 
            this.txtNoBags.Height = 0.2F;
            this.txtNoBags.Left = 1.19F;
            this.txtNoBags.Name = "txtNoBags";
            this.txtNoBags.Text = null;
            this.txtNoBags.Top = 1.1F;
            this.txtNoBags.Width = 1.687F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // rptSamplingResult
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
            this.DataInitialize += new System.EventHandler(this.rptSamplingResult_DataInitialize);
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateSampled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlateNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrailerPlateNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoBags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox txtDateGenerated;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Barcode brcTrackingNo;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox txtDateSampled;
        private DataDynamics.ActiveReports.TextBox txtCode;
        private DataDynamics.ActiveReports.TextBox txtPlateNo;
        private DataDynamics.ActiveReports.Label label13;
        private DataDynamics.ActiveReports.TextBox txtTrailerPlateNo;
        private DataDynamics.ActiveReports.TextBox txtNoBags;
    }
}
