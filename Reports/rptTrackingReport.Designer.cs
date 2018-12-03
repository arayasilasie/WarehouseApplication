namespace WarehouseApplication
{
    /// <summary>
    /// Summary description for rptTrackingReport.
    /// </summary>
    partial class rptTrackingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptTrackingReport));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.txtDateGenerated = new DataDynamics.ActiveReports.TextBox();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.barcode1 = new DataDynamics.ActiveReports.Barcode();
            this.reportInfo2 = new DataDynamics.ActiveReports.ReportInfo();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.txtTrackingNumber = new DataDynamics.ActiveReports.TextBox();
            this.txtClientName = new DataDynamics.ActiveReports.TextBox();
            this.txtTruckPlateNumber = new DataDynamics.ActiveReports.TextBox();
            this.txtDateReceived = new DataDynamics.ActiveReports.TextBox();
            this.txtWarehouse = new DataDynamics.ActiveReports.TextBox();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.txtTrailerPlateNumber = new DataDynamics.ActiveReports.TextBox();
            this.label10 = new DataDynamics.ActiveReports.Label();
            this.txtClinetId = new DataDynamics.ActiveReports.TextBox();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.txtVoucherNumber = new DataDynamics.ActiveReports.TextBox();
            this.label11 = new DataDynamics.ActiveReports.Label();
            this.chkIsTruckInCompound = new DataDynamics.ActiveReports.CheckBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckPlateNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateReceived)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrailerPlateNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClinetId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruckInCompound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.label7,
            this.txtDateGenerated,
            this.picture1,
            this.line1,
            this.barcode1,
            this.reportInfo2});
            this.pageHeader.Height = 1.256944F;
            this.pageHeader.Name = "pageHeader";
            // 
            // label1
            // 
            this.label1.Height = 0.315F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.626F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Verdana; font-size: 15pt; ddo-char-set: 0";
            this.label1.Text = "Arrival Tracking Report";
            this.label1.Top = 0.122F;
            this.label1.Width = 2.791F;
            // 
            // label7
            // 
            this.label7.Height = 0.1979167F;
            this.label7.HyperLink = null;
            this.label7.Left = 1.691F;
            this.label7.Name = "label7";
            this.label7.Style = "";
            this.label7.Text = "Date Generated:";
            this.label7.Top = 0.702F;
            this.label7.Width = 1.061F;
            // 
            // txtDateGenerated
            // 
            this.txtDateGenerated.Height = 0.2F;
            this.txtDateGenerated.Left = 4.417F;
            this.txtDateGenerated.Name = "txtDateGenerated";
            this.txtDateGenerated.Text = null;
            this.txtDateGenerated.Top = 0.9F;
            this.txtDateGenerated.Width = 1.281001F;
            // 
            // picture1
            // 
            this.picture1.Height = 1.101F;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0.218F;
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0.122F;
            this.picture1.Width = 1.167F;
            // 
            // line1
            // 
            this.line1.Height = 0.001000047F;
            this.line1.Left = 1.636F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 1.155F;
            this.line1.Width = 4.79F;
            this.line1.X1 = 6.426F;
            this.line1.X2 = 1.636F;
            this.line1.Y1 = 1.156F;
            this.line1.Y2 = 1.155F;
            // 
            // barcode1
            // 
            this.barcode1.DataField = "TrackingNumber";
            this.barcode1.Font = new System.Drawing.Font("Courier New", 8F);
            this.barcode1.Height = 0.646F;
            this.barcode1.Left = 4.469F;
            this.barcode1.Name = "barcode1";
            this.barcode1.Text = "barcode1";
            this.barcode1.Top = 0.122F;
            this.barcode1.Width = 1.957F;
            // 
            // reportInfo2
            // 
            this.reportInfo2.FormatString = "{RunDateTime:dd-MMM-yyyy hh:mm tt}";
            this.reportInfo2.Height = 0.1875F;
            this.reportInfo2.Left = 2.833F;
            this.reportInfo2.Name = "reportInfo2";
            this.reportInfo2.Style = "font-family: Tahoma; font-size: 9.75pt; ddo-char-set: 0";
            this.reportInfo2.Top = 0.7130001F;
            this.reportInfo2.Width = 1.479F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label2,
            this.label3,
            this.label4,
            this.label5,
            this.label6,
            this.txtTrackingNumber,
            this.txtClientName,
            this.txtTruckPlateNumber,
            this.txtDateReceived,
            this.txtWarehouse,
            this.label8,
            this.txtTrailerPlateNumber,
            this.label10,
            this.txtClinetId,
            this.label9,
            this.txtVoucherNumber,
            this.label11,
            this.chkIsTruckInCompound});
            this.detail.Height = 2.972F;
            this.detail.Name = "detail";
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.323F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Tracking No:";
            this.label2.Top = 0.114F;
            this.label2.Width = 1F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.323F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "Client Id:";
            this.label3.Top = 0.385F;
            this.label3.Width = 1F;
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 0.323F;
            this.label4.Name = "label4";
            this.label4.Style = "";
            this.label4.Text = "Date: ";
            this.label4.Top = 1.826F;
            this.label4.Width = 1F;
            // 
            // label5
            // 
            this.label5.Height = 0.2F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.323F;
            this.label5.Name = "label5";
            this.label5.Style = "";
            this.label5.Text = "Truck Plate No:";
            this.label5.Top = 1.232F;
            this.label5.Width = 1.16F;
            // 
            // label6
            // 
            this.label6.Height = 0.2F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.323F;
            this.label6.Name = "label6";
            this.label6.Style = "";
            this.label6.Text = "Warehouse:";
            this.label6.Top = 2.105F;
            this.label6.Width = 1F;
            // 
            // txtTrackingNumber
            // 
            this.txtTrackingNumber.DataField = "TrackingNumber";
            this.txtTrackingNumber.Height = 0.2F;
            this.txtTrackingNumber.Left = 2.04F;
            this.txtTrackingNumber.Name = "txtTrackingNumber";
            this.txtTrackingNumber.Text = null;
            this.txtTrackingNumber.Top = 0.114F;
            this.txtTrackingNumber.Width = 1.99F;
            // 
            // txtClientName
            // 
            this.txtClientName.DataField = "ClientName";
            this.txtClientName.Height = 0.2F;
            this.txtClientName.Left = 2.03F;
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Text = null;
            this.txtClientName.Top = 0.647F;
            this.txtClientName.Width = 4.396F;
            // 
            // txtTruckPlateNumber
            // 
            this.txtTruckPlateNumber.DataField = "TruckPlateNumber";
            this.txtTruckPlateNumber.Height = 0.2F;
            this.txtTruckPlateNumber.Left = 2.03F;
            this.txtTruckPlateNumber.Name = "txtTruckPlateNumber";
            this.txtTruckPlateNumber.Text = null;
            this.txtTruckPlateNumber.Top = 1.201F;
            this.txtTruckPlateNumber.Width = 1.99F;
            // 
            // txtDateReceived
            // 
            this.txtDateReceived.DataField = "DateReceived";
            this.txtDateReceived.Height = 0.2F;
            this.txtDateReceived.Left = 2.03F;
            this.txtDateReceived.Name = "txtDateReceived";
            this.txtDateReceived.Text = null;
            this.txtDateReceived.Top = 1.784F;
            this.txtDateReceived.Width = 1.99F;
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.DataField = "WarehouseName";
            this.txtWarehouse.Height = 0.2F;
            this.txtWarehouse.Left = 2.031F;
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Text = null;
            this.txtWarehouse.Top = 2.105F;
            this.txtWarehouse.Width = 1.99F;
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 0.323F;
            this.label8.Name = "label8";
            this.label8.Style = "";
            this.label8.Text = "Trailer Plate No:";
            this.label8.Top = 1.524F;
            this.label8.Width = 1.16F;
            // 
            // txtTrailerPlateNumber
            // 
            this.txtTrailerPlateNumber.DataField = "TrailerPlateNumber";
            this.txtTrailerPlateNumber.Height = 0.2F;
            this.txtTrailerPlateNumber.Left = 2.03F;
            this.txtTrailerPlateNumber.Name = "txtTrailerPlateNumber";
            this.txtTrailerPlateNumber.Text = null;
            this.txtTrailerPlateNumber.Top = 1.482F;
            this.txtTrailerPlateNumber.Width = 1.99F;
            // 
            // label10
            // 
            this.label10.Height = 0.2105001F;
            this.label10.HyperLink = null;
            this.label10.Left = 0.323F;
            this.label10.Name = "label10";
            this.label10.Style = "";
            this.label10.Text = "Client Name :";
            this.label10.Top = 0.6580001F;
            this.label10.Width = 1F;
            // 
            // txtClinetId
            // 
            this.txtClinetId.DataField = "ClientId";
            this.txtClinetId.Height = 0.2F;
            this.txtClinetId.Left = 2.041F;
            this.txtClinetId.Name = "txtClinetId";
            this.txtClinetId.Text = null;
            this.txtClinetId.Top = 0.385F;
            this.txtClinetId.Width = 1.99F;
            // 
            // label9
            // 
            this.label9.Height = 0.2F;
            this.label9.HyperLink = null;
            this.label9.Left = 0.323F;
            this.label9.Name = "label9";
            this.label9.Style = "";
            this.label9.Text = "Voucher No:";
            this.label9.Top = 0.937F;
            this.label9.Width = 1F;
            // 
            // txtVoucherNumber
            // 
            this.txtVoucherNumber.DataField = "VoucherNumber";
            this.txtVoucherNumber.Height = 0.2F;
            this.txtVoucherNumber.Left = 2.03F;
            this.txtVoucherNumber.Name = "txtVoucherNumber";
            this.txtVoucherNumber.Text = null;
            this.txtVoucherNumber.Top = 0.937F;
            this.txtVoucherNumber.Width = 1.99F;
            // 
            // label11
            // 
            this.label11.Height = 0.2F;
            this.label11.HyperLink = null;
            this.label11.Left = 0.324F;
            this.label11.Name = "label11";
            this.label11.Style = "";
            this.label11.Text = "Is Truck In Compound?";
            this.label11.Top = 2.458F;
            this.label11.Width = 1.607F;
            // 
            // chkIsTruckInCompound
            // 
            this.chkIsTruckInCompound.DataField = "IsTruckInCompound";
            this.chkIsTruckInCompound.Height = 0.2F;
            this.chkIsTruckInCompound.Left = 2.041F;
            this.chkIsTruckInCompound.Name = "chkIsTruckInCompound";
            this.chkIsTruckInCompound.Style = "";
            this.chkIsTruckInCompound.Text = "";
            this.chkIsTruckInCompound.Top = 2.458F;
            this.chkIsTruckInCompound.Width = 0.2720001F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // rptTrackingReport
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.90625F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrackingNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckPlateNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateReceived)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTrailerPlateNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClinetId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruckInCompound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox txtDateGenerated;
        private DataDynamics.ActiveReports.TextBox txtTrackingNumber;
        private DataDynamics.ActiveReports.TextBox txtClientName;
        private DataDynamics.ActiveReports.TextBox txtTruckPlateNumber;
        private DataDynamics.ActiveReports.TextBox txtDateReceived;
        private DataDynamics.ActiveReports.TextBox txtWarehouse;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.TextBox txtTrailerPlateNumber;
        private DataDynamics.ActiveReports.Label label10;
        private DataDynamics.ActiveReports.TextBox txtClinetId;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.TextBox txtVoucherNumber;
        private DataDynamics.ActiveReports.Label label11;
        private DataDynamics.ActiveReports.CheckBox chkIsTruckInCompound;
        private DataDynamics.ActiveReports.Barcode barcode1;
        private DataDynamics.ActiveReports.ReportInfo reportInfo2;
    }
}
