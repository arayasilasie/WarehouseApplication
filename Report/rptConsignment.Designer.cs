namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for testRpt.
    /// </summary>
    partial class rptConsignment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptConsignment));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.lblDate = new DataDynamics.ActiveReports.Label();
            this.line8 = new DataDynamics.ActiveReports.Line();
            this.label15 = new DataDynamics.ActiveReports.Label();
            this.label43 = new DataDynamics.ActiveReports.Label();
            this.lblOrg = new DataDynamics.ActiveReports.Label();
            this.imgLogo = new DataDynamics.ActiveReports.Picture();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.lblWhr = new DataDynamics.ActiveReports.Label();
            this.lblGrnNumber = new DataDynamics.ActiveReports.Label();
            this.lblClientId = new DataDynamics.ActiveReports.Label();
            this.lblPlate = new DataDynamics.ActiveReports.Label();
            this.lblTrailer = new DataDynamics.ActiveReports.Label();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.txtExpiryDate = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.groupHeader1 = new DataDynamics.ActiveReports.GroupHeader();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.label9 = new DataDynamics.ActiveReports.Label();
            this.label10 = new DataDynamics.ActiveReports.Label();
            this.lblSymbol = new DataDynamics.ActiveReports.Label();
            this.label13 = new DataDynamics.ActiveReports.Label();
            this.groupFooter1 = new DataDynamics.ActiveReports.GroupFooter();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWhr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGrnNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTrailer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.line2,
            this.lblDate,
            this.line8,
            this.label15,
            this.label43,
            this.lblOrg,
            this.imgLogo});
            this.pageHeader.Height = 0.91725F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            // 
            // line2
            // 
            this.line2.Height = 0.004999995F;
            this.line2.Left = 1.03F;
            this.line2.LineWeight = 1F;
            this.line2.Name = "line2";
            this.line2.Top = 0.748F;
            this.line2.Width = 8.97F;
            this.line2.X1 = 1.03F;
            this.line2.X2 = 10F;
            this.line2.Y1 = 0.753F;
            this.line2.Y2 = 0.748F;
            // 
            // lblDate
            // 
            this.lblDate.Height = 0.1880001F;
            this.lblDate.HyperLink = null;
            this.lblDate.Left = 8.771018F;
            this.lblDate.Name = "lblDate";
            this.lblDate.Style = "";
            this.lblDate.Text = "";
            this.lblDate.Top = 0.542F;
            this.lblDate.Width = 1F;
            // 
            // line8
            // 
            this.line8.Height = 0.004999876F;
            this.line8.Left = 1.03F;
            this.line8.LineWeight = 1F;
            this.line8.Name = "line8";
            this.line8.Top = 0.5100001F;
            this.line8.Width = 8.97F;
            this.line8.X1 = 1.03F;
            this.line8.X2 = 10F;
            this.line8.Y1 = 0.515F;
            this.line8.Y2 = 0.5100001F;
            // 
            // label15
            // 
            this.label15.Height = 0.188F;
            this.label15.HyperLink = null;
            this.label15.Left = 7.684017F;
            this.label15.Name = "label15";
            this.label15.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: bold; text-align: left; ddo-" +
                "char-set: 0";
            this.label15.Text = "Printed Date: ";
            this.label15.Top = 0.542F;
            this.label15.Width = 1.087F;
            // 
            // label43
            // 
            this.label43.Height = 0.188F;
            this.label43.HyperLink = null;
            this.label43.Left = 1.1F;
            this.label43.Name = "label43";
            this.label43.Style = "font-family: Candara; font-size: 8.25pt; font-weight: normal; ddo-char-set: 0";
            this.label43.Text = "Alsam Chelelek Tower 2, Tel:+251 554 7001, Fax: +251-11- 554 7010, Website: www.e" +
                "cx.com.et, P.O.Box 17341, Addis Ababa, Ethiopia.";
            this.label43.Top = 0.5420001F;
            this.label43.Width = 6.3535F;
            // 
            // lblOrg
            // 
            this.lblOrg.Height = 0.375F;
            this.lblOrg.HyperLink = null;
            this.lblOrg.Left = 2.865999F;
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Style = "font-family: Verdana; font-size: 14.25pt; font-weight: bold; text-align: left; dd" +
                "o-char-set: 0";
            this.lblOrg.Text = "Expired Bonded Yard List";
            this.lblOrg.Top = 0F;
            this.lblOrg.Width = 4.1875F;
            // 
            // imgLogo
            // 
            this.imgLogo.Height = 0.8650001F;
            this.imgLogo.HyperLink = null;
            this.imgLogo.ImageData = ((System.IO.Stream)(resources.GetObject("imgLogo.ImageData")));
            this.imgLogo.Left = 0.03F;
            this.imgLogo.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.imgLogo.Top = 0F;
            this.imgLogo.Width = 0.7810001F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblWhr,
            this.lblGrnNumber,
            this.lblClientId,
            this.lblPlate,
            this.lblTrailer,
            this.label1,
            this.txtExpiryDate});
            this.detail.Height = 0.302F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // lblWhr
            // 
            this.lblWhr.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWhr.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWhr.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWhr.DataField = "WarehouseRecieptId";
            this.lblWhr.Height = 0.302F;
            this.lblWhr.HyperLink = null;
            this.lblWhr.Left = 1.03F;
            this.lblWhr.Name = "lblWhr";
            this.lblWhr.Style = "";
            this.lblWhr.Text = "";
            this.lblWhr.Top = 0F;
            this.lblWhr.Width = 1.15F;
            // 
            // lblGrnNumber
            // 
            this.lblGrnNumber.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGrnNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGrnNumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGrnNumber.DataField = "GRNNumber";
            this.lblGrnNumber.Height = 0.302F;
            this.lblGrnNumber.HyperLink = null;
            this.lblGrnNumber.Left = 2.18F;
            this.lblGrnNumber.Name = "lblGrnNumber";
            this.lblGrnNumber.Style = "";
            this.lblGrnNumber.Text = "";
            this.lblGrnNumber.Top = 0F;
            this.lblGrnNumber.Width = 1.004F;
            // 
            // lblClientId
            // 
            this.lblClientId.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientId.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientId.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientId.DataField = "IDNo";
            this.lblClientId.Height = 0.302F;
            this.lblClientId.HyperLink = null;
            this.lblClientId.Left = 8.284018F;
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Style = "";
            this.lblClientId.Text = "";
            this.lblClientId.Top = 0F;
            this.lblClientId.Width = 1.286F;
            // 
            // lblPlate
            // 
            this.lblPlate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblPlate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblPlate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblPlate.DataField = "CarPlateNumber";
            this.lblPlate.Height = 0.302F;
            this.lblPlate.HyperLink = null;
            this.lblPlate.Left = 4.135001F;
            this.lblPlate.Name = "lblPlate";
            this.lblPlate.Style = "";
            this.lblPlate.Text = "";
            this.lblPlate.Top = 0F;
            this.lblPlate.Width = 1.515F;
            // 
            // lblTrailer
            // 
            this.lblTrailer.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblTrailer.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblTrailer.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblTrailer.DataField = "TrailerPlateNumber";
            this.lblTrailer.Height = 0.302F;
            this.lblTrailer.HyperLink = null;
            this.lblTrailer.Left = 5.650017F;
            this.lblTrailer.Name = "lblTrailer";
            this.lblTrailer.Style = "";
            this.lblTrailer.Text = "";
            this.lblTrailer.Top = 0F;
            this.lblTrailer.Width = 1.68F;
            // 
            // label1
            // 
            this.label1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label1.DataField = "Symbol";
            this.label1.Height = 0.302F;
            this.label1.HyperLink = null;
            this.label1.Left = 7.330017F;
            this.label1.Name = "label1";
            this.label1.Style = "";
            this.label1.Text = "";
            this.label1.Top = 0F;
            this.label1.Width = 0.9540001F;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtExpiryDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtExpiryDate.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtExpiryDate.DataField = "ExpiryDate";
            this.txtExpiryDate.Height = 0.302F;
            this.txtExpiryDate.Left = 3.183999F;
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.OutputFormat = resources.GetString("txtExpiryDate.OutputFormat");
            this.txtExpiryDate.Text = null;
            this.txtExpiryDate.Top = 0F;
            this.txtExpiryDate.Width = 0.9510002F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // groupHeader1
            // 
            this.groupHeader1.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label3,
            this.label6,
            this.label8,
            this.label9,
            this.label10,
            this.lblSymbol,
            this.label13});
            this.groupHeader1.Height = 0.261F;
            this.groupHeader1.Name = "groupHeader1";
            this.groupHeader1.Format += new System.EventHandler(this.groupHeader1_Format);
            // 
            // label3
            // 
            this.label3.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label3.Height = 0.261F;
            this.label3.HyperLink = null;
            this.label3.Left = 1.04F;
            this.label3.Name = "label3";
            this.label3.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label3.Text = "S-WHR Number";
            this.label3.Top = 0F;
            this.label3.Width = 1.15F;
            // 
            // label6
            // 
            this.label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Height = 0.261F;
            this.label6.HyperLink = null;
            this.label6.Left = 2.19F;
            this.label6.Name = "label6";
            this.label6.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label6.Text = "GRN Number";
            this.label6.Top = 0F;
            this.label6.Width = 1.004F;
            // 
            // label8
            // 
            this.label8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label8.Height = 0.261F;
            this.label8.HyperLink = null;
            this.label8.Left = 3.193999F;
            this.label8.Name = "label8";
            this.label8.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label8.Text = "Expired Date";
            this.label8.Top = 0F;
            this.label8.Width = 0.9510003F;
            // 
            // label9
            // 
            this.label9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label9.Height = 0.261F;
            this.label9.HyperLink = null;
            this.label9.Left = 4.145001F;
            this.label9.Name = "label9";
            this.label9.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label9.Text = "Truck Plate Number";
            this.label9.Top = 0F;
            this.label9.Width = 1.515F;
            // 
            // label10
            // 
            this.label10.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label10.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label10.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label10.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label10.Height = 0.261F;
            this.label10.HyperLink = null;
            this.label10.Left = 5.660023F;
            this.label10.Name = "label10";
            this.label10.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label10.Text = "Trailer Plate Number";
            this.label10.Top = 0F;
            this.label10.Width = 1.679994F;
            // 
            // lblSymbol
            // 
            this.lblSymbol.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblSymbol.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblSymbol.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblSymbol.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblSymbol.Height = 0.261F;
            this.lblSymbol.HyperLink = null;
            this.lblSymbol.Left = 7.340017F;
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.lblSymbol.Text = "Symbol";
            this.lblSymbol.Top = 0F;
            this.lblSymbol.Width = 0.9539995F;
            // 
            // label13
            // 
            this.label13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label13.Height = 0.261F;
            this.label13.HyperLink = null;
            this.label13.Left = 8.294018F;
            this.label13.Name = "label13";
            this.label13.Style = "background-color: Lavender; font-family: Tahoma; font-size: 9.75pt; font-weight: " +
                "bold; text-align: left; ddo-char-set: 0";
            this.label13.Text = "Client ID Number";
            this.label13.Top = 0F;
            this.label13.Width = 1.286F;
            // 
            // groupFooter1
            // 
            this.groupFooter1.Height = 0F;
            this.groupFooter1.Name = "groupFooter1";
            // 
            // rptConsignment
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 10.23958F;
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
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWhr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGrnNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPlate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTrailer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpiryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.GroupHeader groupHeader1;
        private DataDynamics.ActiveReports.GroupFooter groupFooter1;
        private DataDynamics.ActiveReports.Label lblWhr;
        private DataDynamics.ActiveReports.Label lblGrnNumber;
        private DataDynamics.ActiveReports.Label lblClientId;
        private DataDynamics.ActiveReports.Label lblPlate;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Label lblDate;
        private DataDynamics.ActiveReports.Line line8;
        private DataDynamics.ActiveReports.Label label15;
        private DataDynamics.ActiveReports.Label label43;
        private DataDynamics.ActiveReports.Label lblOrg;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.Label label9;
        private DataDynamics.ActiveReports.Label label10;
        private DataDynamics.ActiveReports.Label lblSymbol;
        private DataDynamics.ActiveReports.Label label13;
        private DataDynamics.ActiveReports.Picture imgLogo;
        private DataDynamics.ActiveReports.Label lblTrailer;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.TextBox txtExpiryDate;
    }
}
