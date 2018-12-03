namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptPUNReport.
    /// </summary>
    partial class rptPickupNoticeExpiredList
    {

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptPickupNoticeExpiredList));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtWeight = new DataDynamics.ActiveReports.TextBox();
            this.txtProductionYear = new DataDynamics.ActiveReports.TextBox();
            this.txtWarehouseReceiptNo = new DataDynamics.ActiveReports.TextBox();
            this.txtShed = new DataDynamics.ActiveReports.TextBox();
            this.txtGRNNo = new DataDynamics.ActiveReports.TextBox();
            this.txtCommodityName1 = new DataDynamics.ActiveReports.TextBox();
            this.txtExpirationDate = new DataDynamics.ActiveReports.TextBox();
            this.txtPUNPrintedDate = new DataDynamics.ActiveReports.TextBox();
            this.shape4 = new DataDynamics.ActiveReports.Shape();
            this.shape1 = new DataDynamics.ActiveReports.Shape();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.reportInfo1 = new DataDynamics.ActiveReports.ReportInfo();
            this.line7 = new DataDynamics.ActiveReports.Line();
            this.label43 = new DataDynamics.ActiveReports.Label();
            this.label24 = new DataDynamics.ActiveReports.Label();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.lblWareHouse = new DataDynamics.ActiveReports.Label();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.groupHeader2 = new DataDynamics.ActiveReports.GroupHeader();
            this.lblClientName = new DataDynamics.ActiveReports.Label();
            this.txtClientName = new DataDynamics.ActiveReports.TextBox();
            this.lblCommodityGrade = new DataDynamics.ActiveReports.Label();
            this.lblExpirationDate = new DataDynamics.ActiveReports.Label();
            this.lblWHR = new DataDynamics.ActiveReports.Label();
            this.lblGRNNo = new DataDynamics.ActiveReports.Label();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.lblShed = new DataDynamics.ActiveReports.Label();
            this.lblProductionYear = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.txtStatusName = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter2 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUNPrintedDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWareHouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCommodityGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWHR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGRNNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProductionYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtWeight,
            this.txtProductionYear,
            this.txtWarehouseReceiptNo,
            this.txtShed,
            this.txtGRNNo,
            this.txtCommodityName1,
            this.txtExpirationDate,
            this.txtPUNPrintedDate});
            this.detail.Height = 0.187F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtWeight
            // 
            this.txtWeight.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeight.DataField = "WeightInKg";
            this.txtWeight.Height = 0.1875F;
            this.txtWeight.Left = 5.149F;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.OutputFormat = resources.GetString("txtWeight.OutputFormat");
            this.txtWeight.Text = "txtWeight";
            this.txtWeight.Top = 5.960464E-08F;
            this.txtWeight.Width = 0.9680007F;
            // 
            // txtProductionYear
            // 
            this.txtProductionYear.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtProductionYear.DataField = "ProductionYear";
            this.txtProductionYear.Height = 0.1875F;
            this.txtProductionYear.Left = 6.117F;
            this.txtProductionYear.Name = "txtProductionYear";
            this.txtProductionYear.OutputFormat = resources.GetString("txtProductionYear.OutputFormat");
            this.txtProductionYear.Text = "txtProductionYear";
            this.txtProductionYear.Top = 5.960464E-08F;
            this.txtProductionYear.Width = 1.18F;
            // 
            // txtWarehouseReceiptNo
            // 
            this.txtWarehouseReceiptNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo.DataField = "WarehouseReceiptNo";
            this.txtWarehouseReceiptNo.Height = 0.1875F;
            this.txtWarehouseReceiptNo.Left = 1.55F;
            this.txtWarehouseReceiptNo.Name = "txtWarehouseReceiptNo";
            this.txtWarehouseReceiptNo.OutputFormat = resources.GetString("txtWarehouseReceiptNo.OutputFormat");
            this.txtWarehouseReceiptNo.Text = "txtWarehouseReceiptNo1";
            this.txtWarehouseReceiptNo.Top = 2.980232E-08F;
            this.txtWarehouseReceiptNo.Width = 1.177F;
            // 
            // txtShed
            // 
            this.txtShed.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtShed.DataField = "ShedName";
            this.txtShed.Height = 0.1875F;
            this.txtShed.Left = 3.938F;
            this.txtShed.Name = "txtShed";
            this.txtShed.OutputFormat = resources.GetString("txtShed.OutputFormat");
            this.txtShed.Text = "txtShed";
            this.txtShed.Top = 5.960464E-08F;
            this.txtShed.Width = 1.211F;
            // 
            // txtGRNNo
            // 
            this.txtGRNNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo.DataField = "GRNNo";
            this.txtGRNNo.Height = 0.1875F;
            this.txtGRNNo.Left = 2.727F;
            this.txtGRNNo.Name = "txtGRNNo";
            this.txtGRNNo.OutputFormat = resources.GetString("txtGRNNo.OutputFormat");
            this.txtGRNNo.Text = "txtGRNNo";
            this.txtGRNNo.Top = 2.980232E-08F;
            this.txtGRNNo.Width = 1.211F;
            // 
            // txtCommodityName1
            // 
            this.txtCommodityName1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.DataField = "CommodityName";
            this.txtCommodityName1.Height = 0.187F;
            this.txtCommodityName1.Left = 0.057F;
            this.txtCommodityName1.Name = "txtCommodityName1";
            this.txtCommodityName1.Text = "txtCommodityName1";
            this.txtCommodityName1.Top = 2.980232E-08F;
            this.txtCommodityName1.Width = 1.493F;
            // 
            // txtExpirationDate
            // 
            this.txtExpirationDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtExpirationDate.DataField = "ExpirationDate";
            this.txtExpirationDate.Height = 0.187F;
            this.txtExpirationDate.Left = 7.297F;
            this.txtExpirationDate.Name = "txtExpirationDate";
            this.txtExpirationDate.OutputFormat = resources.GetString("txtExpirationDate.OutputFormat");
            this.txtExpirationDate.Text = "txtExpirationDate";
            this.txtExpirationDate.Top = 0F;
            this.txtExpirationDate.Width = 1.21F;
            // 
            // txtPUNPrintedDate
            // 
            this.txtPUNPrintedDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtPUNPrintedDate.DataField = "PUNPrintDateTime";
            this.txtPUNPrintedDate.Height = 0.187F;
            this.txtPUNPrintedDate.Left = 8.507F;
            this.txtPUNPrintedDate.Name = "txtPUNPrintedDate";
            this.txtPUNPrintedDate.OutputFormat = resources.GetString("txtPUNPrintedDate.OutputFormat");
            this.txtPUNPrintedDate.Text = "txtPUNPrintedDate";
            this.txtPUNPrintedDate.Top = 0F;
            this.txtPUNPrintedDate.Width = 1.21F;
            // 
            // shape4
            // 
            this.shape4.Height = 0.34375F;
            this.shape4.Left = 0.057F;
            this.shape4.Name = "shape4";
            this.shape4.RoundingRadius = 9.999999F;
            this.shape4.Top = 0F;
            this.shape4.Width = 7.818F;
            // 
            // shape1
            // 
            this.shape1.Height = 0.34375F;
            this.shape1.Left = 0.057F;
            this.shape1.Name = "shape1";
            this.shape1.RoundingRadius = 9.999999F;
            this.shape1.Top = 0.353F;
            this.shape1.Width = 7.818F;
            // 
            // label1
            // 
            this.label1.Height = 0.1875F;
            this.label1.HyperLink = "";
            this.label1.Left = 0.191F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label1.Text = "ECX use only:";
            this.label1.Top = 0.06200002F;
            this.label1.Width = 1.125F;
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.picture1,
            this.line1,
            this.label2,
            this.reportInfo1,
            this.line7,
            this.label43,
            this.label24});
            this.pageHeader.Height = 0.9166667F;
            this.pageHeader.Name = "pageHeader";
            // 
            // picture1
            // 
            this.picture1.Height = 0.875F;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0F;
            this.picture1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0F;
            this.picture1.Width = 0.934F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0F;
            this.line1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.line1.LineWeight = 6F;
            this.line1.Name = "line1";
            this.line1.Top = 0.875F;
            this.line1.Width = 7.875F;
            this.line1.X1 = 0F;
            this.line1.X2 = 7.875F;
            this.line1.Y1 = 0.875F;
            this.line1.Y2 = 0.875F;
            // 
            // label2
            // 
            this.label2.Height = 0.1875F;
            this.label2.HyperLink = "";
            this.label2.Left = 5.625F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "Date Generated:";
            this.label2.Top = 0.625F;
            this.label2.Width = 1.1875F;
            // 
            // reportInfo1
            // 
            this.reportInfo1.FormatString = "{RunDateTime:dd-MMM-yyyy}";
            this.reportInfo1.Height = 0.1875F;
            this.reportInfo1.Left = 6.8125F;
            this.reportInfo1.Name = "reportInfo1";
            this.reportInfo1.Style = "font-family: Tahoma; font-size: 9.75pt; ddo-char-set: 0";
            this.reportInfo1.Top = 0.625F;
            this.reportInfo1.Width = 0.9375F;
            // 
            // line7
            // 
            this.line7.Height = 0F;
            this.line7.Left = 2.189F;
            this.line7.LineWeight = 1F;
            this.line7.Name = "line7";
            this.line7.Top = 0.528F;
            this.line7.Width = 2.771022F;
            this.line7.X1 = 2.189F;
            this.line7.X2 = 4.960022F;
            this.line7.Y1 = 0.528F;
            this.line7.Y2 = 0.528F;
            // 
            // label43
            // 
            this.label43.Height = 0.348F;
            this.label43.HyperLink = null;
            this.label43.Left = 1.881F;
            this.label43.Name = "label43";
            this.label43.Style = "font-family: Candara; font-size: 9pt; font-weight: normal";
            this.label43.Text = "Alsam Chelelek Tower 2, Tel:+251 554 7001, Fax: +251-11- 554 7010, \r\nWebsite: www" +
                ".ecx.com.et, P.O.Box 17341, Addis Ababa, Ethiopia.";
            this.label43.Top = 0.538F;
            this.label43.Width = 4.302F;
            // 
            // label24
            // 
            this.label24.Height = 0.396F;
            this.label24.HyperLink = "";
            this.label24.Left = 2.501F;
            this.label24.Name = "label24";
            this.label24.Style = "font-family: Tahoma; font-size: 22pt; font-weight: bold; ddo-char-set: 1";
            this.label24.Text = "Pickup Notice Expired List";
            this.label24.Top = 0.142F;
            this.label24.Width = 4.055F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.line2,
            this.shape4,
            this.label1,
            this.shape1});
            this.pageFooter.Height = 1.163F;
            this.pageFooter.Name = "pageFooter";
            // 
            // line2
            // 
            this.line2.Height = 0F;
            this.line2.Left = 0F;
            this.line2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.line2.LineWeight = 3F;
            this.line2.Name = "line2";
            this.line2.Top = 0F;
            this.line2.Width = 7.875F;
            this.line2.X1 = 0F;
            this.line2.X2 = 7.875F;
            this.line2.Y1 = 0F;
            this.line2.Y2 = 0F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblWareHouse});
            this.groupHeader1.DataField = "WarehouseName";
            this.groupHeader1.Height = 0.2073334F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // lblWareHouse
            // 
            this.lblWareHouse.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWareHouse.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWareHouse.DataField = "WarehouseName";
            this.lblWareHouse.Height = 0.1875F;
            this.lblWareHouse.HyperLink = "";
            this.lblWareHouse.Left = 0F;
            this.lblWareHouse.Name = "lblWareHouse";
            this.lblWareHouse.Style = "color: DarkGreen; font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text" +
                "-align: left; ddo-char-set: 0";
            this.lblWareHouse.Text = "";
            this.lblWareHouse.Top = 0F;
            this.lblWareHouse.Width = 3.22F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // groupHeader2
            // 
            this.groupHeader2.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblClientName,
            this.txtClientName,
            this.lblCommodityGrade,
            this.lblExpirationDate,
            this.lblWHR,
            this.lblGRNNo,
            this.label3,
            this.lblShed,
            this.lblProductionYear,
            this.label4,
            this.txtStatusName});
            this.groupHeader2.DataField = "ClientName";
            this.groupHeader2.Height = 0.5615F;
            this.groupHeader2.Name = "groupHeader2";
            // 
            // lblClientName
            // 
            this.lblClientName.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientName.Height = 0.1875F;
            this.lblClientName.HyperLink = "";
            this.lblClientName.Left = 0F;
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblClientName.Text = "Client Name";
            this.lblClientName.Top = 0F;
            this.lblClientName.Width = 3.22F;
            // 
            // txtClientName
            // 
            this.txtClientName.DataField = "ClientName";
            this.txtClientName.Height = 0.187F;
            this.txtClientName.Left = 0F;
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Text = "txtClientName";
            this.txtClientName.Top = 0.187F;
            this.txtClientName.Width = 9.794001F;
            // 
            // lblCommodityGrade
            // 
            this.lblCommodityGrade.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblCommodityGrade.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblCommodityGrade.Height = 0.1875F;
            this.lblCommodityGrade.HyperLink = "";
            this.lblCommodityGrade.Left = 0.035F;
            this.lblCommodityGrade.Name = "lblCommodityGrade";
            this.lblCommodityGrade.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblCommodityGrade.Text = "Commodity Grade";
            this.lblCommodityGrade.Top = 0.374F;
            this.lblCommodityGrade.Width = 1.515F;
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblExpirationDate.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblExpirationDate.Height = 0.1875F;
            this.lblExpirationDate.HyperLink = "";
            this.lblExpirationDate.Left = 7.297F;
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblExpirationDate.Text = "Expiration Date";
            this.lblExpirationDate.Top = 0.374F;
            this.lblExpirationDate.Width = 1.204F;
            // 
            // lblWHR
            // 
            this.lblWHR.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWHR.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWHR.Height = 0.1875F;
            this.lblWHR.HyperLink = "";
            this.lblWHR.Left = 1.55F;
            this.lblWHR.Name = "lblWHR";
            this.lblWHR.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblWHR.Text = "WHR";
            this.lblWHR.Top = 0.374F;
            this.lblWHR.Width = 1.177F;
            // 
            // lblGRNNo
            // 
            this.lblGRNNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGRNNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGRNNo.Height = 0.1875F;
            this.lblGRNNo.HyperLink = "";
            this.lblGRNNo.Left = 2.727F;
            this.lblGRNNo.Name = "lblGRNNo";
            this.lblGRNNo.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblGRNNo.Text = "GRN No";
            this.lblGRNNo.Top = 0.374F;
            this.lblGRNNo.Width = 1.211F;
            // 
            // label3
            // 
            this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Height = 0.1875F;
            this.label3.HyperLink = "";
            this.label3.Left = 5.149F;
            this.label3.Name = "label3";
            this.label3.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.label3.Text = "Weight";
            this.label3.Top = 0.374F;
            this.label3.Width = 0.9680001F;
            // 
            // lblShed
            // 
            this.lblShed.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblShed.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblShed.Height = 0.1875F;
            this.lblShed.HyperLink = "";
            this.lblShed.Left = 3.938F;
            this.lblShed.Name = "lblShed";
            this.lblShed.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblShed.Text = "Shed";
            this.lblShed.Top = 0.374F;
            this.lblShed.Width = 1.211F;
            // 
            // lblProductionYear
            // 
            this.lblProductionYear.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblProductionYear.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblProductionYear.Height = 0.1875F;
            this.lblProductionYear.HyperLink = "";
            this.lblProductionYear.Left = 6.117F;
            this.lblProductionYear.Name = "lblProductionYear";
            this.lblProductionYear.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.lblProductionYear.Text = "Production Year";
            this.lblProductionYear.Top = 0.374F;
            this.lblProductionYear.Width = 1.18F;
            // 
            // label4
            // 
            this.label4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label4.Height = 0.1875F;
            this.label4.HyperLink = "";
            this.label4.Left = 8.507F;
            this.label4.Name = "label4";
            this.label4.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.label4.Text = "PUN Printed Date";
            this.label4.Top = 0.374F;
            this.label4.Width = 1.287F;
            // 
            // txtStatusName
            // 
            this.txtStatusName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtStatusName.DataField = "StatusName";
            this.txtStatusName.Height = 0.1875F;
            this.txtStatusName.Left = 5.588F;
            this.txtStatusName.Name = "txtStatusName";
            this.txtStatusName.OutputFormat = resources.GetString("txtStatusName.OutputFormat");
            this.txtStatusName.Text = "txtStatusName";
            this.txtStatusName.Top = 0F;
            this.txtStatusName.Visible = false;
            this.txtStatusName.Width = 0.9680007F;
            // 
            // groupFooter2
            // 
            this.groupFooter2.Height = 0.25F;
            this.groupFooter2.Name = "groupFooter2";
            // 
            // rptPickupNoticeExpiredList
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Bottom = 0.3F;
            this.PageSettings.Margins.Left = 0.3F;
            this.PageSettings.Margins.Right = 0.3F;
            this.PageSettings.Margins.Top = 0.3F;
            this.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 9.865F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.groupHeader2);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter2);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.ReportStart += new System.EventHandler(this.rptPUNReport_ReportStart);
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductionYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUNPrintedDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWareHouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCommodityGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWHR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGRNNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProductionYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatusName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.Shape shape4;
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.ReportInfo reportInfo1;
        private DataDynamics.ActiveReports.Label label43;
        private DataDynamics.ActiveReports.PageFooter pageFooter;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Label label24;
        private DataDynamics.ActiveReports.Line line7;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Shape shape1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.TextBox txtCommodityName1;
        private DataDynamics.ActiveReports.TextBox txtExpirationDate;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox txtWeight;
        private DataDynamics.ActiveReports.TextBox txtProductionYear;
        private DataDynamics.ActiveReports.TextBox txtWarehouseReceiptNo;
        private DataDynamics.ActiveReports.TextBox txtShed;
        private DataDynamics.ActiveReports.TextBox txtGRNNo;
        private DataDynamics.ActiveReports.TextBox txtPUNPrintedDate;
        private DataDynamics.ActiveReports.GroupHeader groupHeader2;
        private DataDynamics.ActiveReports.Label lblWareHouse;
        private DataDynamics.ActiveReports.GroupFooter groupFooter2;
        private DataDynamics.ActiveReports.Label lblClientName;
        private DataDynamics.ActiveReports.TextBox txtClientName;
        private DataDynamics.ActiveReports.Label lblCommodityGrade;
        private DataDynamics.ActiveReports.Label lblExpirationDate;
        private DataDynamics.ActiveReports.Label lblWHR;
        private DataDynamics.ActiveReports.Label lblGRNNo;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label lblShed;
        private DataDynamics.ActiveReports.Label lblProductionYear;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.TextBox txtStatusName;
    }
}
