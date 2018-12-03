namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptInventoryBalance.
    /// </summary>
    partial class rptInventoryBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptInventoryBalance));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label28 = new DataDynamics.ActiveReports.Label();
            this.reportInfo2 = new DataDynamics.ActiveReports.ReportInfo();
            this.line6 = new DataDynamics.ActiveReports.Line();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtSymbol = new DataDynamics.ActiveReports.TextBox();
            this.txtPYear = new DataDynamics.ActiveReports.TextBox();
            this.txtPAddress = new DataDynamics.ActiveReports.TextBox();
            this.txtStackNo = new DataDynamics.ActiveReports.TextBox();
            this.txtBalaneceWeight = new DataDynamics.ActiveReports.TextBox();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.line3 = new DataDynamics.ActiveReports.Line();
            this.line5 = new DataDynamics.ActiveReports.Line();
            this.line7 = new DataDynamics.ActiveReports.Line();
            this.line8 = new DataDynamics.ActiveReports.Line();
            this.line9 = new DataDynamics.ActiveReports.Line();
            this.line10 = new DataDynamics.ActiveReports.Line();
            this.txtBalaneceBag = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.label10 = new DataDynamics.ActiveReports.Label();
            this.txtWarehouse = new DataDynamics.ActiveReports.TextBox();
            this.txtLIC = new DataDynamics.ActiveReports.TextBox();
            this.label25 = new DataDynamics.ActiveReports.Label();
            this.label17 = new DataDynamics.ActiveReports.Label();
            this.txtShed = new DataDynamics.ActiveReports.TextBox();
            this.label11 = new DataDynamics.ActiveReports.Label();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.line4 = new DataDynamics.ActiveReports.Line();
            this.textBox7 = new DataDynamics.ActiveReports.TextBox();
            this.textBox8 = new DataDynamics.ActiveReports.TextBox();
            this.textBox9 = new DataDynamics.ActiveReports.TextBox();
            this.textBox10 = new DataDynamics.ActiveReports.TextBox();
            this.textBox1 = new DataDynamics.ActiveReports.TextBox();
            this.txtBalaneceWeigt = new DataDynamics.ActiveReports.TextBox();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStackNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceBag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLIC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceWeigt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.picture1,
            this.label1,
            this.label28,
            this.reportInfo2,
            this.line6});
            this.pageHeader.Height = 0.962F;
            this.pageHeader.Name = "pageHeader";
            // 
            // picture1
            // 
            this.picture1.Height = 0.875F;
            this.picture1.HyperLink = null;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0F;
            this.picture1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0F;
            this.picture1.Width = 0.924F;
            // 
            // label1
            // 
            this.label1.Height = 0.362F;
            this.label1.HyperLink = null;
            this.label1.Left = 0.965F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 22pt; font-weight: bold; ddo-char-set: 1";
            this.label1.Text = " INVENTORY BALANCE ";
            this.label1.Top = 0.154F;
            this.label1.Width = 4.3F;
            // 
            // label28
            // 
            this.label28.Height = 0.1875F;
            this.label28.HyperLink = "";
            this.label28.Left = 5.37F;
            this.label28.Name = "label28";
            this.label28.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; ddo-char-set: 0";
            this.label28.Text = "Date Generated:";
            this.label28.Top = 0.634F;
            this.label28.Width = 1.1875F;
            // 
            // reportInfo2
            // 
            this.reportInfo2.FormatString = "{RunDateTime:dd-MMM-yyyy}";
            this.reportInfo2.Height = 0.1875F;
            this.reportInfo2.Left = 6.558F;
            this.reportInfo2.Name = "reportInfo2";
            this.reportInfo2.Style = "font-family: Tahoma; font-size: 9.75pt; ddo-char-set: 0";
            this.reportInfo2.Top = 0.634F;
            this.reportInfo2.Width = 0.9375F;
            // 
            // line6
            // 
            this.line6.Height = 0F;
            this.line6.Left = 0F;
            this.line6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.line6.LineWeight = 6F;
            this.line6.Name = "line6";
            this.line6.Top = 0.88F;
            this.line6.Width = 7.675F;
            this.line6.X1 = 0F;
            this.line6.X2 = 7.675F;
            this.line6.Y1 = 0.88F;
            this.line6.Y2 = 0.88F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtSymbol,
            this.txtPYear,
            this.txtPAddress,
            this.txtStackNo,
            this.txtBalaneceWeight,
            this.line1,
            this.line2,
            this.line3,
            this.line5,
            this.line7,
            this.line8,
            this.line9,
            this.line10,
            this.txtBalaneceBag});
            this.detail.Height = 0.352F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtSymbol
            // 
            this.txtSymbol.DataField = "Symbol";
            this.txtSymbol.Height = 0.35F;
            this.txtSymbol.Left = 0.07300001F;
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Style = "text-align: center; vertical-align: middle";
            this.txtSymbol.Text = "txtSymbol";
            this.txtSymbol.Top = 0F;
            this.txtSymbol.Width = 1.167F;
            // 
            // txtPYear
            // 
            this.txtPYear.DataField = "ProductionYear";
            this.txtPYear.Height = 0.35F;
            this.txtPYear.Left = 1.24F;
            this.txtPYear.Name = "txtPYear";
            this.txtPYear.Style = "text-align: center; vertical-align: middle";
            this.txtPYear.Text = "txtPYear";
            this.txtPYear.Top = 0.004000008F;
            this.txtPYear.Width = 0.709F;
            // 
            // txtPAddress
            // 
            this.txtPAddress.DataField = "PhysicalAddress";
            this.txtPAddress.Height = 0.35F;
            this.txtPAddress.Left = 1.949F;
            this.txtPAddress.Name = "txtPAddress";
            this.txtPAddress.Style = "text-align: center; vertical-align: middle";
            this.txtPAddress.Text = "txtPAddress";
            this.txtPAddress.Top = 0.002000004F;
            this.txtPAddress.Width = 0.987F;
            // 
            // txtStackNo
            // 
            this.txtStackNo.DataField = "StackNumber";
            this.txtStackNo.Height = 0.35F;
            this.txtStackNo.Left = 2.936F;
            this.txtStackNo.Name = "txtStackNo";
            this.txtStackNo.Style = "text-align: center; vertical-align: middle";
            this.txtStackNo.Text = "txtStackNo";
            this.txtStackNo.Top = 0.004000008F;
            this.txtStackNo.Width = 2.117F;
            // 
            // txtBalaneceWeight
            // 
            this.txtBalaneceWeight.DataField = "CurrentWeight";
            this.txtBalaneceWeight.Height = 0.35F;
            this.txtBalaneceWeight.Left = 6.173F;
            this.txtBalaneceWeight.Name = "txtBalaneceWeight";
            this.txtBalaneceWeight.Style = "text-align: center; vertical-align: middle";
            this.txtBalaneceWeight.Text = "txtBalaneceWeight";
            this.txtBalaneceWeight.Top = 0F;
            this.txtBalaneceWeight.Width = 1.322F;
            // 
            // line1
            // 
            this.line1.Height = 0.35F;
            this.line1.Left = 1.239833F;
            this.line1.LineColor = System.Drawing.Color.LightGray;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 0F;
            this.line1.Width = 0.0001670122F;
            this.line1.X1 = 1.24F;
            this.line1.X2 = 1.239833F;
            this.line1.Y1 = 0F;
            this.line1.Y2 = 0.35F;
            // 
            // line2
            // 
            this.line2.Height = 0.346F;
            this.line2.Left = 0.07283335F;
            this.line2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.004F;
            this.line2.Width = 0.0001666546F;
            this.line2.X1 = 0.07300001F;
            this.line2.X2 = 0.07283335F;
            this.line2.Y1 = 0.004F;
            this.line2.Y2 = 0.35F;
            // 
            // line3
            // 
            this.line3.Height = 0.35F;
            this.line3.Left = 1.948833F;
            this.line3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line3.LineWeight = 1F;
            this.line3.Name = "line3";
            this.line3.Top = 0F;
            this.line3.Width = 0.0001670122F;
            this.line3.X1 = 1.949F;
            this.line3.X2 = 1.948833F;
            this.line3.Y1 = 0F;
            this.line3.Y2 = 0.35F;
            // 
            // line5
            // 
            this.line5.Height = 0.35F;
            this.line5.Left = 2.935833F;
            this.line5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line5.LineWeight = 1F;
            this.line5.Name = "line5";
            this.line5.Top = 0F;
            this.line5.Width = 0.0001671314F;
            this.line5.X1 = 2.936F;
            this.line5.X2 = 2.935833F;
            this.line5.Y1 = 0F;
            this.line5.Y2 = 0.35F;
            // 
            // line7
            // 
            this.line7.Height = 0.346F;
            this.line7.Left = 5.052835F;
            this.line7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line7.LineWeight = 1F;
            this.line7.Name = "line7";
            this.line7.Top = 0.004F;
            this.line7.Width = 0.0001649857F;
            this.line7.X1 = 5.053F;
            this.line7.X2 = 5.052835F;
            this.line7.Y1 = 0.004F;
            this.line7.Y2 = 0.35F;
            // 
            // line8
            // 
            this.line8.Height = 0.342F;
            this.line8.Left = 6.172834F;
            this.line8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line8.LineWeight = 1F;
            this.line8.Name = "line8";
            this.line8.Top = 0.008F;
            this.line8.Width = 0.0001659393F;
            this.line8.X1 = 6.173F;
            this.line8.X2 = 6.172834F;
            this.line8.Y1 = 0.008F;
            this.line8.Y2 = 0.35F;
            // 
            // line9
            // 
            this.line9.Height = 0.352F;
            this.line9.Left = 7.495F;
            this.line9.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line9.LineWeight = 1F;
            this.line9.Name = "line9";
            this.line9.Top = 0F;
            this.line9.Width = 0F;
            this.line9.X1 = 7.495F;
            this.line9.X2 = 7.495F;
            this.line9.Y1 = 0F;
            this.line9.Y2 = 0.352F;
            // 
            // line10
            // 
            this.line10.Height = 0.002000004F;
            this.line10.Left = 0.07300001F;
            this.line10.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.line10.LineWeight = 1F;
            this.line10.Name = "line10";
            this.line10.Top = 0.35F;
            this.line10.Width = 7.422F;
            this.line10.X1 = 0.07300001F;
            this.line10.X2 = 7.495F;
            this.line10.Y1 = 0.35F;
            this.line10.Y2 = 0.352F;
            // 
            // txtBalaneceBag
            // 
            this.txtBalaneceBag.DataField = "CurrentBalance";
            this.txtBalaneceBag.Height = 0.35F;
            this.txtBalaneceBag.Left = 5.053F;
            this.txtBalaneceBag.Name = "txtBalaneceBag";
            this.txtBalaneceBag.Style = "text-align: center; vertical-align: middle";
            this.txtBalaneceBag.Text = "txtBalaneceBag";
            this.txtBalaneceBag.Top = 0F;
            this.txtBalaneceBag.Width = 1.12F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0.25F;
            this.pageFooter.Name = "pageFooter";
            // 
            // label10
            // 
            this.label10.Height = 0.2F;
            this.label10.HyperLink = null;
            this.label10.Left = 3.908F;
            this.label10.Name = "label10";
            this.label10.Style = "font-weight: bold";
            this.label10.Text = "Warehouse :";
            this.label10.Top = 0.05899976F;
            this.label10.Width = 1.547F;
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.DataField = "Warehouse";
            this.txtWarehouse.Height = 0.2F;
            this.txtWarehouse.Left = 5.566F;
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.Text = "txtWarehouse";
            this.txtWarehouse.Top = 0.05899976F;
            this.txtWarehouse.Width = 2.109F;
            // 
            // txtLIC
            // 
            this.txtLIC.DataField = "LIC";
            this.txtLIC.Height = 0.2F;
            this.txtLIC.Left = 1.499F;
            this.txtLIC.Name = "txtLIC";
            this.txtLIC.Text = "txtLIC";
            this.txtLIC.Top = 0.059F;
            this.txtLIC.Width = 2.1F;
            // 
            // label25
            // 
            this.label25.Height = 0.2F;
            this.label25.HyperLink = null;
            this.label25.Left = 0.07300001F;
            this.label25.Name = "label25";
            this.label25.Style = "font-weight: bold";
            this.label25.Text = "LIC:";
            this.label25.Top = 0.059F;
            this.label25.Width = 1.272F;
            // 
            // label17
            // 
            this.label17.Height = 0.2F;
            this.label17.HyperLink = null;
            this.label17.Left = 0.07300013F;
            this.label17.Name = "label17";
            this.label17.Style = "font-weight: bold";
            this.label17.Text = "Shed:";
            this.label17.Top = 0.3615F;
            this.label17.Width = 1.272F;
            // 
            // txtShed
            // 
            this.txtShed.DataField = "ShedNumber";
            this.txtShed.Height = 0.2F;
            this.txtShed.Left = 1.499F;
            this.txtShed.Name = "txtShed";
            this.txtShed.Text = "txtShed ";
            this.txtShed.Top = 0.3615F;
            this.txtShed.Width = 2.1F;
            // 
            // label11
            // 
            this.label11.Height = 0.2500001F;
            this.label11.HyperLink = "";
            this.label11.Left = 0F;
            this.label11.Name = "label11";
            this.label11.Style = "font-family: Tahoma; font-size: 12pt; font-weight: bold; text-align: center; ddo-" +
                "char-set: 1";
            this.label11.Text = "INVENTORY BALANCE DETAIL";
            this.label11.Top = 0.7600001F;
            this.label11.Width = 7.495F;
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label25,
            this.label10,
            this.txtWarehouse,
            this.txtLIC,
            this.label17,
            this.txtShed,
            this.label11,
            this.line4,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox1,
            this.txtBalaneceWeigt});
            this.groupHeader1.Height = 1.379F;
            this.groupHeader1.Name = "groupHeader1";
            // 
            // line4
            // 
            this.line4.Height = 0F;
            this.line4.Left = 0F;
            this.line4.LineWeight = 3F;
            this.line4.Name = "line4";
            this.line4.Top = 0.684F;
            this.line4.Width = 7.675F;
            this.line4.X1 = 0F;
            this.line4.X2 = 7.675F;
            this.line4.Y1 = 0.684F;
            this.line4.Y2 = 0.684F;
            // 
            // textBox7
            // 
            this.textBox7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox7.Height = 0.3F;
            this.textBox7.Left = 0.07300001F;
            this.textBox7.Name = "textBox7";
            this.textBox7.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.textBox7.Text = "Comodity Grade";
            this.textBox7.Top = 1.067F;
            this.textBox7.Width = 1.167F;
            // 
            // textBox8
            // 
            this.textBox8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox8.Height = 0.3F;
            this.textBox8.Left = 1.24F;
            this.textBox8.Name = "textBox8";
            this.textBox8.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.textBox8.Text = "P.Year";
            this.textBox8.Top = 1.067F;
            this.textBox8.Width = 0.709F;
            // 
            // textBox9
            // 
            this.textBox9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox9.Height = 0.3F;
            this.textBox9.Left = 1.949F;
            this.textBox9.Name = "textBox9";
            this.textBox9.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.textBox9.Text = "Phy. Address";
            this.textBox9.Top = 1.067F;
            this.textBox9.Width = 0.987F;
            // 
            // textBox10
            // 
            this.textBox10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox10.Height = 0.3F;
            this.textBox10.Left = 2.936F;
            this.textBox10.Name = "textBox10";
            this.textBox10.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.textBox10.Text = "Stack No";
            this.textBox10.Top = 1.067F;
            this.textBox10.Width = 2.117F;
            // 
            // textBox1
            // 
            this.textBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.textBox1.Height = 0.3F;
            this.textBox1.Left = 5.053F;
            this.textBox1.Name = "textBox1";
            this.textBox1.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.textBox1.Text = "Balance in Bags";
            this.textBox1.Top = 1.067F;
            this.textBox1.Width = 1.12F;
            // 
            // txtBalaneceWeigt
            // 
            this.txtBalaneceWeigt.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtBalaneceWeigt.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtBalaneceWeigt.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtBalaneceWeigt.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtBalaneceWeigt.Height = 0.3F;
            this.txtBalaneceWeigt.Left = 6.173F;
            this.txtBalaneceWeigt.Name = "txtBalaneceWeigt";
            this.txtBalaneceWeigt.Style = "background-color: LightGrey; font-weight: bold; text-align: center; vertical-alig" +
                "n: middle";
            this.txtBalaneceWeigt.Text = "Balance in Weight";
            this.txtBalaneceWeigt.Top = 1.067F;
            this.txtBalaneceWeigt.Width = 1.322F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0.26F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptInventoryBalance
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 8.442584F;
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
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportInfo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStackNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceBag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLIC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalaneceWeigt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label28;
        private DataDynamics.ActiveReports.ReportInfo reportInfo2;
        private DataDynamics.ActiveReports.Line line6;
        private DataDynamics.ActiveReports.Label label10;
        private DataDynamics.ActiveReports.TextBox txtWarehouse;
        private DataDynamics.ActiveReports.TextBox txtLIC;
        private DataDynamics.ActiveReports.Label label25;
        private DataDynamics.ActiveReports.Label label17;
        private DataDynamics.ActiveReports.TextBox txtShed;
        private DataDynamics.ActiveReports.Label label11;
        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.Line line4;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.TextBox textBox7;
        private DataDynamics.ActiveReports.TextBox textBox8;
        private DataDynamics.ActiveReports.TextBox textBox9;
        private DataDynamics.ActiveReports.TextBox textBox10;
        private DataDynamics.ActiveReports.TextBox textBox1;
        private DataDynamics.ActiveReports.TextBox txtBalaneceWeigt;
        private DataDynamics.ActiveReports.TextBox txtSymbol;
        private DataDynamics.ActiveReports.TextBox txtPYear;
        private DataDynamics.ActiveReports.TextBox txtPAddress;
        private DataDynamics.ActiveReports.TextBox txtStackNo;
        private DataDynamics.ActiveReports.TextBox txtBalaneceBag;
        private DataDynamics.ActiveReports.TextBox txtBalaneceWeight;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Line line3;
        private DataDynamics.ActiveReports.Line line5;
        private DataDynamics.ActiveReports.Line line7;
        private DataDynamics.ActiveReports.Line line8;
        private DataDynamics.ActiveReports.Line line9;
        private DataDynamics.ActiveReports.Line line10;
    }
}
