namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptGenerate.
    /// </summary>
    partial class rptGenerate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptGenerate));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.brcTrackingNo = new DataDynamics.ActiveReports.Barcode();
            this.label7 = new DataDynamics.ActiveReports.Label();
            this.txtDateGenerated = new DataDynamics.ActiveReports.TextBox();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.txtDateCoded = new DataDynamics.ActiveReports.TextBox();
            this.label3 = new DataDynamics.ActiveReports.Label();
            this.txtCode = new DataDynamics.ActiveReports.TextBox();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.txtGradingClass = new DataDynamics.ActiveReports.TextBox();
            this.textBox3 = new DataDynamics.ActiveReports.TextBox();
            this.line6 = new DataDynamics.ActiveReports.Line();
            this.label43 = new DataDynamics.ActiveReports.Label();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.line4 = new DataDynamics.ActiveReports.Line();
            this.lblGINID = new DataDynamics.ActiveReports.Label();
            this.chkHasMoldOrFungus = new DataDynamics.ActiveReports.CheckBox();
            this.chkHasChemicalOrPetrol = new DataDynamics.ActiveReports.CheckBox();
            this.chkHasLiveInsect = new DataDynamics.ActiveReports.CheckBox();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtName = new DataDynamics.ActiveReports.TextBox();
            this.chkChecked = new DataDynamics.ActiveReports.CheckBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.label8 = new DataDynamics.ActiveReports.Label();
            this.textBox2 = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCoded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGradingClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasMoldOrFungus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasChemicalOrPetrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasLiveInsect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChecked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label1,
            this.brcTrackingNo,
            this.label7,
            this.txtDateGenerated,
            this.label2,
            this.txtDateCoded,
            this.label3,
            this.txtCode,
            this.label5,
            this.label6,
            this.label4,
            this.txtGradingClass,
            this.textBox3,
            this.line6,
            this.label43,
            this.picture1,
            this.line4,
            this.lblGINID,
            this.chkHasMoldOrFungus,
            this.chkHasChemicalOrPetrol,
            this.chkHasLiveInsect});
            this.pageHeader.Height = 2.80175F;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Format += new System.EventHandler(this.pageHeader_Format);
            this.pageHeader.BeforePrint += new System.EventHandler(this.pageHeader_BeforePrint);
            // 
            // label1
            // 
            this.label1.Height = 0.375F;
            this.label1.HyperLink = null;
            this.label1.Left = 1.621F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: Tahoma; font-size: 22pt; font-weight: bold; ddo-char-set: 1";
            this.label1.Text = "Coding Ticket";
            this.label1.Top = 0.3165002F;
            this.label1.Width = 2.311F;
            // 
            // brcTrackingNo
            // 
            this.brcTrackingNo.DataField = "GradingCode";
            this.brcTrackingNo.Font = new System.Drawing.Font("Courier New", 8F);
            this.brcTrackingNo.Height = 0.5F;
            this.brcTrackingNo.Left = 4.407F;
            this.brcTrackingNo.Name = "brcTrackingNo";
            this.brcTrackingNo.Text = "barcode1";
            this.brcTrackingNo.Top = 0.2765002F;
            this.brcTrackingNo.Width = 1.5F;
            // 
            // label7
            // 
            this.label7.Height = 0.1979167F;
            this.label7.HyperLink = null;
            this.label7.Left = 3.594F;
            this.label7.Name = "label7";
            this.label7.Style = "";
            this.label7.Text = "Date Generated:";
            this.label7.Top = 1.1635F;
            this.label7.Width = 1.061F;
            // 
            // txtDateGenerated
            // 
            this.txtDateGenerated.Height = 0.2F;
            this.txtDateGenerated.Left = 4.751991F;
            this.txtDateGenerated.Name = "txtDateGenerated";
            this.txtDateGenerated.Text = null;
            this.txtDateGenerated.Top = 1.1635F;
            this.txtDateGenerated.Width = 1F;
            // 
            // label2
            // 
            this.label2.Height = 0.2F;
            this.label2.HyperLink = null;
            this.label2.Left = 0.1930001F;
            this.label2.Name = "label2";
            this.label2.Style = "";
            this.label2.Text = "Date:";
            this.label2.Top = 1.5155F;
            this.label2.Width = 1.146F;
            // 
            // txtDateCoded
            // 
            this.txtDateCoded.DataField = "DateTimeCoded";
            this.txtDateCoded.Height = 0.2F;
            this.txtDateCoded.Left = 1.448F;
            this.txtDateCoded.Name = "txtDateCoded";
            this.txtDateCoded.Text = null;
            this.txtDateCoded.Top = 1.5155F;
            this.txtDateCoded.Width = 2.145F;
            // 
            // label3
            // 
            this.label3.Height = 0.2F;
            this.label3.HyperLink = null;
            this.label3.Left = 0.1930001F;
            this.label3.Name = "label3";
            this.label3.Style = "";
            this.label3.Text = "Code:";
            this.label3.Top = 1.778F;
            this.label3.Width = 1.146F;
            // 
            // txtCode
            // 
            this.txtCode.DataField = "GradingCode";
            this.txtCode.Height = 0.2F;
            this.txtCode.Left = 1.448F;
            this.txtCode.Name = "txtCode";
            this.txtCode.Text = null;
            this.txtCode.Top = 1.778F;
            this.txtCode.Width = 4.312F;
            // 
            // label5
            // 
            this.label5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label5.Height = 0.1875F;
            this.label5.HyperLink = null;
            this.label5.Left = 0.1930003F;
            this.label5.Name = "label5";
            this.label5.Style = "font-weight: bold";
            this.label5.Text = "Name ";
            this.label5.Top = 2.614F;
            this.label5.Width = 2.621F;
            // 
            // label6
            // 
            this.label6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.label6.Height = 0.1875F;
            this.label6.HyperLink = null;
            this.label6.Left = 2.814F;
            this.label6.Name = "label6";
            this.label6.Style = "font-weight: bold";
            this.label6.Text = "Is Lab coordinator?";
            this.label6.Top = 2.614F;
            this.label6.Width = 1.593F;
            // 
            // label4
            // 
            this.label4.Height = 0.2F;
            this.label4.HyperLink = null;
            this.label4.Left = 0.1930001F;
            this.label4.Name = "label4";
            this.label4.Style = "";
            this.label4.Text = "Grading Class :";
            this.label4.Top = 2.0485F;
            this.label4.Width = 1.146F;
            // 
            // txtGradingClass
            // 
            this.txtGradingClass.DataField = "Class";
            this.txtGradingClass.Height = 0.2F;
            this.txtGradingClass.Left = 1.448F;
            this.txtGradingClass.Name = "txtGradingClass";
            this.txtGradingClass.Text = null;
            this.txtGradingClass.Top = 2.0485F;
            this.txtGradingClass.Width = 4.312F;
            // 
            // textBox3
            // 
            this.textBox3.DataField = "SamplingStatusID";
            this.textBox3.Height = 0.2F;
            this.textBox3.Left = 4.751992F;
            this.textBox3.Name = "textBox3";
            this.textBox3.Text = null;
            this.textBox3.Top = 0.8905001F;
            this.textBox3.Visible = false;
            this.textBox3.Width = 1.061F;
            // 
            // line6
            // 
            this.line6.Height = 0F;
            this.line6.Left = 0.04000012F;
            this.line6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.line6.LineWeight = 6F;
            this.line6.Name = "line6";
            this.line6.Top = 1.4195F;
            this.line6.Width = 5.78F;
            this.line6.X1 = 0.04000012F;
            this.line6.X2 = 5.82F;
            this.line6.Y1 = 1.4195F;
            this.line6.Y2 = 1.4195F;
            // 
            // label43
            // 
            this.label43.Height = 0.348F;
            this.label43.HyperLink = null;
            this.label43.Left = 0.9400002F;
            this.label43.Name = "label43";
            this.label43.Style = "font-family: Candara; font-size: 9pt; font-weight: normal";
            this.label43.Text = "Alsam Chelelek Tower 2, Tel:+251 554 7001, Fax: +251-11- 554 7010, \r\nWebsite: www" +
                ".ecx.com.et, P.O.Box 17341, Addis Ababa, Ethiopia.";
            this.label43.Top = 0.7765002F;
            this.label43.Width = 3.725F;
            // 
            // picture1
            // 
            this.picture1.Height = 0.825F;
            this.picture1.HyperLink = null;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 0.04F;
            this.picture1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 0.26F;
            this.picture1.Width = 0.8999999F;
            // 
            // line4
            // 
            this.line4.Height = 0.002999961F;
            this.line4.Left = 1.272F;
            this.line4.LineWeight = 1F;
            this.line4.Name = "line4";
            this.line4.Top = 0.7495002F;
            this.line4.Width = 2.66F;
            this.line4.X1 = 1.272F;
            this.line4.X2 = 3.932F;
            this.line4.Y1 = 0.7525002F;
            this.line4.Y2 = 0.7495002F;
            // 
            // lblGINID
            // 
            this.lblGINID.DataField = "id";
            this.lblGINID.Height = 0.2F;
            this.lblGINID.HyperLink = null;
            this.lblGINID.Left = 3.665F;
            this.lblGINID.Name = "lblGINID";
            this.lblGINID.Style = "";
            this.lblGINID.Text = "";
            this.lblGINID.Top = 1.5155F;
            this.lblGINID.Visible = false;
            this.lblGINID.Width = 1F;
            // 
            // chkHasMoldOrFungus
            // 
            this.chkHasMoldOrFungus.DataField = "HasMoldOrFungus";
            this.chkHasMoldOrFungus.Height = 0.188F;
            this.chkHasMoldOrFungus.Left = 0.2070001F;
            this.chkHasMoldOrFungus.Name = "chkHasMoldOrFungus";
            this.chkHasMoldOrFungus.Style = "";
            this.chkHasMoldOrFungus.Text = "Has Mold Or Fungus";
            this.chkHasMoldOrFungus.Top = 2.333F;
            this.chkHasMoldOrFungus.Width = 1.479F;
            // 
            // chkHasChemicalOrPetrol
            // 
            this.chkHasChemicalOrPetrol.DataField = "HasChemicalOrPetrol";
            this.chkHasChemicalOrPetrol.Height = 0.2F;
            this.chkHasChemicalOrPetrol.Left = 1.828F;
            this.chkHasChemicalOrPetrol.Name = "chkHasChemicalOrPetrol";
            this.chkHasChemicalOrPetrol.Style = "";
            this.chkHasChemicalOrPetrol.Text = "Has Chemical Or Petrol";
            this.chkHasChemicalOrPetrol.Top = 2.333F;
            this.chkHasChemicalOrPetrol.Width = 1.677F;
            // 
            // chkHasLiveInsect
            // 
            this.chkHasLiveInsect.DataField = "HasLiveInsect";
            this.chkHasLiveInsect.Height = 0.2F;
            this.chkHasLiveInsect.Left = 3.607F;
            this.chkHasLiveInsect.Name = "chkHasLiveInsect";
            this.chkHasLiveInsect.Style = "";
            this.chkHasLiveInsect.Text = "Has Live Insect";
            this.chkHasLiveInsect.Top = 2.333F;
            this.chkHasLiveInsect.Width = 1.344F;
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtName,
            this.chkChecked});
            this.detail.Height = 0.1953331F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtName
            // 
            this.txtName.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtName.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtName.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtName.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtName.DataField = "Name";
            this.txtName.DistinctField = "Name";
            this.txtName.Height = 0.221F;
            this.txtName.Left = 0.193F;
            this.txtName.Name = "txtName";
            this.txtName.Text = null;
            this.txtName.Top = 0F;
            this.txtName.Width = 2.621F;
            // 
            // chkChecked
            // 
            this.chkChecked.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.chkChecked.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.chkChecked.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.chkChecked.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.chkChecked.Checked = true;
            this.chkChecked.DataField = "isSupervisor";
            this.chkChecked.Height = 0.221F;
            this.chkChecked.Left = 2.814F;
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Style = "";
            this.chkChecked.Text = "";
            this.chkChecked.Top = 0F;
            this.chkChecked.Width = 1.593F;
            // 
            // pageFooter
            // 
            this.pageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.label8,
            this.textBox2});
            this.pageFooter.Height = 0.9583333F;
            this.pageFooter.Name = "pageFooter";
            // 
            // label8
            // 
            this.label8.Height = 0.2F;
            this.label8.HyperLink = null;
            this.label8.Left = 0.2070002F;
            this.label8.Name = "label8";
            this.label8.Style = "";
            this.label8.Text = "Remark:";
            this.label8.Top = 0.052F;
            this.label8.Visible = false;
            this.label8.Width = 1.635F;
            // 
            // textBox2
            // 
            this.textBox2.DataField = "SamplingStatusName";
            this.textBox2.Height = 0.544F;
            this.textBox2.Left = 2.188F;
            this.textBox2.Name = "textBox2";
            this.textBox2.Text = null;
            this.textBox2.Top = 0.052F;
            this.textBox2.Visible = false;
            this.textBox2.Width = 3.646F;
            // 
            // rptGenerate
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 5.907F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            this.PageStart += new System.EventHandler(this.rptGenerate_PageStart);
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCoded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGradingClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasMoldOrFungus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasChemicalOrPetrol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHasLiveInsect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkChecked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Barcode brcTrackingNo;
        private DataDynamics.ActiveReports.Label label7;
        private DataDynamics.ActiveReports.TextBox txtDateGenerated;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.TextBox txtDateCoded;
        private DataDynamics.ActiveReports.Label label3;
        private DataDynamics.ActiveReports.TextBox txtCode;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.TextBox txtName;
        private DataDynamics.ActiveReports.CheckBox chkChecked;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.TextBox txtGradingClass;
        private DataDynamics.ActiveReports.TextBox textBox3;
        private DataDynamics.ActiveReports.Line line6;
        private DataDynamics.ActiveReports.Label label43;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.Line line4;
        private DataDynamics.ActiveReports.Label lblGINID;
        private DataDynamics.ActiveReports.CheckBox chkHasMoldOrFungus;
        private DataDynamics.ActiveReports.CheckBox chkHasChemicalOrPetrol;
        private DataDynamics.ActiveReports.CheckBox chkHasLiveInsect;
        private DataDynamics.ActiveReports.Label label8;
        private DataDynamics.ActiveReports.TextBox textBox2;
    }
}
