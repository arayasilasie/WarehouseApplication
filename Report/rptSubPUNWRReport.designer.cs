namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptSubPUNWRReport.
    /// </summary>
    partial class rptSubPUNWRReport
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSubPUNWRReport));
            DataDynamics.ActiveReports.DataSources.SqlDBDataSource sqlDBDataSource1 = new DataDynamics.ActiveReports.DataSources.SqlDBDataSource();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtSNo = new DataDynamics.ActiveReports.TextBox();
            this.txtCommodityName1 = new DataDynamics.ActiveReports.TextBox();
            this.txtWeightInKg1 = new DataDynamics.ActiveReports.TextBox();
            this.txtWarehouseReceiptNo1 = new DataDynamics.ActiveReports.TextBox();
            this.txtGRNNo1 = new DataDynamics.ActiveReports.TextBox();
            this.reportHeader1 = new DataDynamics.ActiveReports.ReportHeader();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.label13 = new DataDynamics.ActiveReports.Label();
            this.label16 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.reportFooter1 = new DataDynamics.ActiveReports.ReportFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtSNo,
            this.txtCommodityName1,
            this.txtWeightInKg1,
            this.txtWarehouseReceiptNo1,
            this.txtGRNNo1});
            this.detail.Height = 0.1875F;
            this.detail.Name = "detail";
            // 
            // txtSNo
            // 
            this.txtSNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.DataField = "SNO";
            this.txtSNo.DistinctField = "ID";
            this.txtSNo.Height = 0.1875F;
            this.txtSNo.Left = 0F;
            this.txtSNo.Name = "txtSNo";
            this.txtSNo.Style = "text-align: left";
            this.txtSNo.SummaryFunc = DataDynamics.ActiveReports.SummaryFunc.Count;
            this.txtSNo.SummaryGroup = "groupHeader1";
            this.txtSNo.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.txtSNo.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.txtSNo.Text = "000";
            this.txtSNo.Top = 0F;
            this.txtSNo.Width = 0.472F;
            // 
            // txtCommodityName1
            // 
            this.txtCommodityName1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.DataField = "CommodityName";
            this.txtCommodityName1.Height = 0.1875F;
            this.txtCommodityName1.Left = 2.931F;
            this.txtCommodityName1.Name = "txtCommodityName1";
            this.txtCommodityName1.Text = "txtCommodityName1";
            this.txtCommodityName1.Top = 0F;
            this.txtCommodityName1.Width = 2.838F;
            // 
            // txtWeightInKg1
            // 
            this.txtWeightInKg1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.DataField = "Weight";
            this.txtWeightInKg1.Height = 0.1875F;
            this.txtWeightInKg1.Left = 5.769F;
            this.txtWeightInKg1.Name = "txtWeightInKg1";
            this.txtWeightInKg1.OutputFormat = resources.GetString("txtWeightInKg1.OutputFormat");
            this.txtWeightInKg1.Text = "txtWeightInKg1";
            this.txtWeightInKg1.Top = 0F;
            this.txtWeightInKg1.Width = 1.856F;
            // 
            // txtWarehouseReceiptNo1
            // 
            this.txtWarehouseReceiptNo1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.DataField = "WarehouseReceiptNo";
            this.txtWarehouseReceiptNo1.Height = 0.1875F;
            this.txtWarehouseReceiptNo1.Left = 1.72F;
            this.txtWarehouseReceiptNo1.Name = "txtWarehouseReceiptNo1";
            this.txtWarehouseReceiptNo1.Text = "txtWarehouseReceiptNo1";
            this.txtWarehouseReceiptNo1.Top = 0F;
            this.txtWarehouseReceiptNo1.Width = 1.211F;
            // 
            // txtGRNNo1
            // 
            this.txtGRNNo1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo1.DataField = "GINNumber";
            this.txtGRNNo1.Height = 0.1875F;
            this.txtGRNNo1.Left = 0.472F;
            this.txtGRNNo1.Name = "txtGRNNo1";
            this.txtGRNNo1.Text = "txtGRNNo1";
            this.txtGRNNo1.Top = 0F;
            this.txtGRNNo1.Width = 1.248F;
            // 
            // reportHeader1
            // 
            this.reportHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label8,
            this.label13,
            this.label16,
            this.label2,
            this.label1});
            this.reportHeader1.Height = 0.2175F;
            this.reportHeader1.Name = "reportHeader1";
            // 
            // label8
            // 
            this.label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Height = 0.1875F;
            this.label8.HyperLink = "";
            this.label8.Left = 2.931F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 0";
            this.label8.Text = "Commodity Grade";
            this.label8.Top = 0.03F;
            this.label8.Width = 2.838F;
            // 
            // label13
            // 
            this.label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Height = 0.1875F;
            this.label13.HyperLink = "";
            this.label13.Left = 5.769F;
            this.label13.Name = "label13";
            this.label13.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 0";
            this.label13.Text = "Net Weight";
            this.label13.Top = 0.03F;
            this.label13.Width = 1.856F;
            // 
            // label16
            // 
            this.label16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Height = 0.1875F;
            this.label16.HyperLink = "";
            this.label16.Left = 1.72F;
            this.label16.Name = "label16";
            this.label16.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 0";
            this.label16.Text = "WHR";
            this.label16.Top = 0.03F;
            this.label16.Width = 1.211F;
            // 
            // label2
            // 
            this.label2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label2.Height = 0.1875F;
            this.label2.HyperLink = "";
            this.label2.Left = 0F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; text-align: center; ddo-c" +
                "har-set: 0";
            this.label2.Text = "SNo.";
            this.label2.Top = 0.03F;
            this.label2.Width = 0.472F;
            // 
            // label1
            // 
            this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Height = 0.1875F;
            this.label1.HyperLink = "";
            this.label1.Left = 0.472F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
                "o-char-set: 0";
            this.label1.Text = "GIN No.";
            this.label1.Top = 0.03F;
            this.label1.Width = 1.248F;
            // 
            // reportFooter1
            // 
            this.reportFooter1.Height = 0F;
            this.reportFooter1.Name = "reportFooter1";
            // 
            // groupHeader1
            // 
            this.groupHeader1.DataField = "WarehouseRecieptId";
            this.groupHeader1.Height = 0F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptSubPUNWRReport
            // 
            this.MasterReport = false;
            sqlDBDataSource1.ConnectionString = "data source=(local);initial catalog=WareHouseApplicationNew;integrated security=S" +
                "SPI;persist security info=False";
            sqlDBDataSource1.SQL = "SELECT     CommodityName, QuantityInLot, WeightInKg, WarehouseReceiptNo, GRNNo\r\nF" +
                "ROM         dbo.tblPickupNotices\r\nWHERE     (Id = N\'<%Id%>\')";
            this.DataSource = sqlDBDataSource1;
            this.PageSettings.DefaultPaperSize = false;
            this.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
            this.PageSettings.PaperHeight = 11.69291F;
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.PageSettings.PaperWidth = 8.268056F;
            this.PrintWidth = 8.011167F;
            this.Sections.Add(this.reportHeader1);
            this.Sections.Add(this.groupHeader1);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.groupFooter1);
            this.Sections.Add(this.reportFooter1);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.TextBox txtSNo;
        private DataDynamics.ActiveReports.ReportHeader reportHeader1;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.Label label13;
        private DataDynamics.ActiveReports.Label label16;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.ReportFooter reportFooter1;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox txtCommodityName1;
        private DataDynamics.ActiveReports.TextBox txtWeightInKg1;
        private DataDynamics.ActiveReports.TextBox txtWarehouseReceiptNo1;
        private DataDynamics.ActiveReports.TextBox txtGRNNo1;
    }
}
