namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptSubPSAApproval.
    /// </summary>
    partial class rptSubPSAApproval
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSubPSAApproval));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            this.txtSNo = new DataDynamics.ActiveReports.TextBox();
            this.txtCommodityName1 = new DataDynamics.ActiveReports.TextBox();
            this.txtWeightInKg1 = new DataDynamics.ActiveReports.TextBox();
            this.txtWarehouseReceiptNo1 = new DataDynamics.ActiveReports.TextBox();
            this.txtGINNumber = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.lblPUNWeight = new DataDynamics.ActiveReports.Label();
            this.lblGINWeight = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.label10 = new DataDynamics.ActiveReports.Label();
            this.label11 = new DataDynamics.ActiveReports.Label();
            this.label12 = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.label13 = new DataDynamics.ActiveReports.Label();
            this.label16 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label14 = new DataDynamics.ActiveReports.Label();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGINNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPUNWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtSNo,
            this.txtCommodityName1,
            this.txtWeightInKg1,
            this.txtWarehouseReceiptNo1,
            this.txtGINNumber,
            this.textBox2});
            this.detail.Height = 0.1875F;
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.09374994F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label8,
            this.label13,
            this.label16,
            this.label2,
            this.label1,
            this.label14});
            this.groupHeader1.Height = 0.1875F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // groupFooter1
            // 
            this.groupFooter1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.textBox1,
            this.label3,
            this.label4,
            this.label5,
            this.lblPUNWeight,
            this.lblGINWeight,
            this.label6,
            this.label7,
            this.label9,
            this.label10,
            this.label11,
            this.label12});
            this.groupFooter1.Height = 0.9374999F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // txtSNo
            // 
            this.txtSNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtSNo.DataField = "SNO";
            this.txtSNo.Height = 0.1875F;
            this.txtSNo.Left = 0F;
            this.txtSNo.Name = "txtSNo";
            this.txtSNo.Style = "text-align: center";
            this.txtSNo.SummaryFunc = DataDynamics.ActiveReports.SummaryFunc.Count;
            this.txtSNo.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.txtSNo.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
            this.txtSNo.Text = "000";
            this.txtSNo.Top = 0F;
            this.txtSNo.Width = 0.375F;
            // 
            // txtCommodityName1
            // 
            this.txtCommodityName1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCommodityName1.DataField = "CommodityName";
            this.txtCommodityName1.Height = 0.1875F;
            this.txtCommodityName1.Left = 2.375F;
            this.txtCommodityName1.Name = "txtCommodityName1";
            this.txtCommodityName1.Text = "txtCommodityName1";
            this.txtCommodityName1.Top = 0F;
            this.txtCommodityName1.Width = 1.272F;
            // 
            // txtWeightInKg1
            // 
            this.txtWeightInKg1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWeightInKg1.DataField = "Weight";
            this.txtWeightInKg1.Height = 0.1875F;
            this.txtWeightInKg1.Left = 4.816F;
            this.txtWeightInKg1.Name = "txtWeightInKg1";
            this.txtWeightInKg1.Style = "text-align: right";
            this.txtWeightInKg1.Text = "txtWeightInKg1";
            this.txtWeightInKg1.Top = 0F;
            this.txtWeightInKg1.Width = 1.6205F;
            // 
            // txtWarehouseReceiptNo1
            // 
            this.txtWarehouseReceiptNo1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtWarehouseReceiptNo1.DataField = "WarehouseReceiptNo";
            this.txtWarehouseReceiptNo1.Height = 0.1875F;
            this.txtWarehouseReceiptNo1.Left = 1.375F;
            this.txtWarehouseReceiptNo1.Name = "txtWarehouseReceiptNo1";
            this.txtWarehouseReceiptNo1.Text = "txtWarehouseReceiptNo1";
            this.txtWarehouseReceiptNo1.Top = 0F;
            this.txtWarehouseReceiptNo1.Width = 1F;
            // 
            // txtGINNumber
            // 
            this.txtGINNumber.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGINNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGINNumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGINNumber.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGINNumber.DataField = "GINNumber";
            this.txtGINNumber.Height = 0.1875F;
            this.txtGINNumber.Left = 0.375F;
            this.txtGINNumber.Name = "txtGINNumber";
            this.txtGINNumber.Text = "txtPSANumber";
            this.txtGINNumber.Top = 0F;
            this.txtGINNumber.Width = 1F;
            // 
            // textBox1
            // 
            this.textBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.DataField = "Weight";
            this.textBox1.DistinctField = "Weight";
            this.textBox1.Height = 0.2F;
            this.textBox1.Left = 4.814F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "text-align: right";
            this.textBox1.SummaryGroup = "groupHeader1";
            this.textBox1.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.All;
            this.textBox1.SummaryType = DataDynamics.ActiveReports.SummaryType.GrandTotal;
            this.textBox1.Text = null;
            this.textBox1.Top = 0F;
            this.textBox1.Width = 1.623F;
            // 
            // label3
            // 
            this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 3.647F;
            this.label3.Name = "label3";
            this.label3.Style = "font-weight: bold; text-align: right";
            this.label3.Text = "Total Weight : ";
            this.label3.Top = 0F;
            this.label3.Width = 1.1695F;
            // 
            // label4
            // 
            this.label4.Height = 0.1979167F;
            this.label4.HyperLink = null;
            this.label4.Left = 7.450581E-08F;
            this.label4.Name = "label4";
            this.label4.Style = "";
            this.label4.Text = "PUN Weight:";
            this.label4.Top = 0.485F;
            this.label4.Width = 1F;
            // 
            // label5
            // 
            this.label5.Height = 0.1979167F;
            this.label5.HyperLink = null;
            this.label5.Left = 0F;
            this.label5.Name = "label5";
            this.label5.Style = "";
            this.label5.Text = "GIN Weight:";
            this.label5.Top = 0.706F;
            this.label5.Width = 1F;
            // 
            // lblPUNWeight
            // 
            this.lblPUNWeight.DataField = "WeightInKg";
            this.lblPUNWeight.Height = 0.1979167F;
            this.lblPUNWeight.HyperLink = null;
            this.lblPUNWeight.Left = 1F;
            this.lblPUNWeight.Name = "lblPUNWeight";
            this.lblPUNWeight.Style = "";
            this.lblPUNWeight.Text = "lblPUNWeight";
            this.lblPUNWeight.Top = 0.485F;
            this.lblPUNWeight.Width = 1.719F;
            // 
            // lblGINWeight
            // 
            this.lblGINWeight.DataField = "totalGIN";
            this.lblGINWeight.Height = 0.1979167F;
            this.lblGINWeight.HyperLink = null;
            this.lblGINWeight.Left = 1F;
            this.lblGINWeight.Name = "lblGINWeight";
            this.lblGINWeight.Style = "";
            this.lblGINWeight.Text = "lblGINWeight";
            this.lblGINWeight.Top = 0.706F;
            this.lblGINWeight.Width = 1.719F;
            // 
            // label6
            // 
            this.label6.Height = 0.1979167F;
            this.label6.HyperLink = null;
            this.label6.Left = 0F;
            this.label6.Name = "label6";
            this.label6.Style = "";
            this.label6.Text = "Stack Number:";
            this.label6.Top = 0.252F;
            this.label6.Width = 1F;
            // 
            // label7
            // 
            this.label7.DataField = "StackNumber";
            this.label7.Height = 0.1979167F;
            this.label7.HyperLink = null;
            this.label7.Left = 1F;
            this.label7.Name = "label7";
            this.label7.Style = "";
            this.label7.Text = "label7";
            this.label7.Top = 0.252F;
            this.label7.Width = 2.479F;
            // 
            // label9
            // 
            this.label9.Height = 0.1979167F;
            this.label9.HyperLink = null;
            this.label9.Left = 3.031F;
            this.label9.Name = "label9";
            this.label9.Style = "";
            this.label9.Text = "Total Deposit Weight:";
            this.label9.Top = 0.485F;
            this.label9.Width = 1.4F;
            // 
            // label10
            // 
            this.label10.DataField = "PhysicalCount";
            this.label10.Height = 0.1979167F;
            this.label10.HyperLink = null;
            this.label10.Left = 4.431F;
            this.label10.Name = "label10";
            this.label10.Style = "";
            this.label10.Text = "label10";
            this.label10.Top = 0.485F;
            this.label10.Width = 1.01F;
            // 
            // label11
            // 
            this.label11.DataField = "PhysicalWeight";
            this.label11.Height = 0.1979167F;
            this.label11.HyperLink = null;
            this.label11.Left = 4.431F;
            this.label11.Name = "label11";
            this.label11.Style = "";
            this.label11.Text = "label11";
            this.label11.Top = 0.7060001F;
            this.label11.Width = 1.01F;
            // 
            // label12
            // 
            this.label12.Height = 0.1979167F;
            this.label12.HyperLink = null;
            this.label12.Left = 3.031F;
            this.label12.Name = "label12";
            this.label12.Style = "";
            this.label12.Text = "Total Delivery Weight:";
            this.label12.Top = 0.7060001F;
            this.label12.Width = 1.4F;
            // 
            // label8
            // 
            this.label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Height = 0.1875F;
            this.label8.HyperLink = "";
            this.label8.Left = 2.375F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
    "o-char-set: 1";
            this.label8.Text = "Commodity Grade";
            this.label8.Top = 0F;
            this.label8.Width = 1.272F;
            // 
            // label13
            // 
            this.label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Height = 0.1875F;
            this.label13.HyperLink = "";
            this.label13.Left = 4.816F;
            this.label13.Name = "label13";
            this.label13.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
    "o-char-set: 0";
            this.label13.Text = "Shortfall";
            this.label13.Top = 0F;
            this.label13.Width = 1.6205F;
            // 
            // label16
            // 
            this.label16.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label16.Height = 0.1875F;
            this.label16.HyperLink = "";
            this.label16.Left = 1.375F;
            this.label16.Name = "label16";
            this.label16.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
    "o-char-set: 0";
            this.label16.Text = "WHR";
            this.label16.Top = 0F;
            this.label16.Width = 1F;
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
            this.label2.Style = "font-family: Tahoma; font-size: 9pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "SNo.";
            this.label2.Top = 0F;
            this.label2.Width = 0.375F;
            // 
            // label1
            // 
            this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Height = 0.1875F;
            this.label1.HyperLink = "";
            this.label1.Left = 0.375F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
    "o-char-set: 0";
            this.label1.Text = "PSA No.";
            this.label1.Top = 0F;
            this.label1.Width = 1F;
            // 
            // label14
            // 
            this.label14.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label14.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label14.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label14.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label14.Height = 0.1875F;
            this.label14.HyperLink = "";
            this.label14.Left = 3.642F;
            this.label14.Name = "label14";
            this.label14.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: center; dd" +
    "o-char-set: 1";
            this.label14.Text = "Prodaction Year";
            this.label14.Top = 0F;
            this.label14.Width = 1.174F;
            // 
            // textBox2
            // 
            this.textBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox2.DataField = "ProductionYear";
            this.textBox2.Height = 0.1875F;
            this.textBox2.Left = 3.642F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Text = "prodYear";
            this.textBox2.Top = 0F;
            this.textBox2.Width = 1.172F;
            // 
            // rptSubPSAApproval
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.895833F;
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
            ((System.ComponentModel.ISupportInitialize)(this.txtSNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodityName1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeightInKg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouseReceiptNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGINNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPUNWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox txtSNo;
        private DataDynamics.ActiveReports.TextBox txtCommodityName1;
        private DataDynamics.ActiveReports.TextBox txtWeightInKg1;
        private DataDynamics.ActiveReports.TextBox txtWarehouseReceiptNo1;
        private DataDynamics.ActiveReports.TextBox txtGINNumber;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label lblPUNWeight;
        private DataDynamics.ActiveReports.Label lblGINWeight;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.Label label10;
        private DataDynamics.ActiveReports.Label label11;
        private DataDynamics.ActiveReports.Label label12;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.Label label13;
        private DataDynamics.ActiveReports.Label label16;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label14;
        private DataDynamics.ActiveReports.TextBox textBox2;


    }
}
