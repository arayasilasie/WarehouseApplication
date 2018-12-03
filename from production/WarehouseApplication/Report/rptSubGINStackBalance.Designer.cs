namespace WarehouseApplication.Report
{
    /// <summary>
    /// Summary description for rptSubGINStackBalance.
    /// </summary>
    partial class rptSubGINStackBalance
    {
        private DataDynamics.ActiveReports.Detail detail;

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSubGINStackBalance));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.txtNetWeight = new DataDynamics.ActiveReports.Label();
            this.txtDateDeposited = new DataDynamics.ActiveReports.Label();
            this.txtGRNNo = new DataDynamics.ActiveReports.Label();
            this.txtNoOfBag = new DataDynamics.ActiveReports.Label();
            this.txtRebagging = new DataDynamics.ActiveReports.Label();
            this.txtAddDeductBag = new DataDynamics.ActiveReports.Label();
            this.txtAddDeductWeight = new DataDynamics.ActiveReports.Label();
            this.txtAdjustmentTypeID = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDeposited)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoOfBag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebagging)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddDeductBag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddDeductWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustmentTypeID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtNetWeight,
            this.txtDateDeposited,
            this.txtGRNNo,
            this.txtNoOfBag,
            this.txtRebagging,
            this.txtAddDeductBag,
            this.txtAddDeductWeight,
            this.txtAdjustmentTypeID});
            this.detail.Height = 0.17F;
            this.detail.Name = "detail";
            // 
            // txtNetWeight
            // 
            this.txtNetWeight.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNetWeight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNetWeight.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNetWeight.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNetWeight.DataField = "NetWeight";
            this.txtNetWeight.Height = 0.17F;
            this.txtNetWeight.HyperLink = "";
            this.txtNetWeight.Left = 5.126F;
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtNetWeight.Text = "";
            this.txtNetWeight.Top = 0F;
            this.txtNetWeight.Width = 0.8650001F;
            // 
            // txtDateDeposited
            // 
            this.txtDateDeposited.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtDateDeposited.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtDateDeposited.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtDateDeposited.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtDateDeposited.DataField = "DateIssued";
            this.txtDateDeposited.Height = 0.17F;
            this.txtDateDeposited.HyperLink = "";
            this.txtDateDeposited.Left = 0F;
            this.txtDateDeposited.Name = "txtDateDeposited";
            this.txtDateDeposited.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtDateDeposited.Text = "";
            this.txtDateDeposited.Top = 0F;
            this.txtDateDeposited.Width = 0.9490001F;
            // 
            // txtGRNNo
            // 
            this.txtGRNNo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtGRNNo.DataField = "GINNumber";
            this.txtGRNNo.Height = 0.17F;
            this.txtGRNNo.HyperLink = "";
            this.txtGRNNo.Left = 0.9490001F;
            this.txtGRNNo.Name = "txtGRNNo";
            this.txtGRNNo.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 1";
            this.txtGRNNo.Text = "";
            this.txtGRNNo.Top = 0F;
            this.txtGRNNo.Width = 1.126F;
            // 
            // txtNoOfBag
            // 
            this.txtNoOfBag.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNoOfBag.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNoOfBag.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNoOfBag.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtNoOfBag.DataField = "NoOfBags";
            this.txtNoOfBag.Height = 0.17F;
            this.txtNoOfBag.HyperLink = "";
            this.txtNoOfBag.Left = 4.645F;
            this.txtNoOfBag.Name = "txtNoOfBag";
            this.txtNoOfBag.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtNoOfBag.Text = "";
            this.txtNoOfBag.Top = 0F;
            this.txtNoOfBag.Width = 0.4800003F;
            // 
            // txtRebagging
            // 
            this.txtRebagging.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtRebagging.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtRebagging.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtRebagging.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtRebagging.DataField = "NoOfRebags";
            this.txtRebagging.Height = 0.17F;
            this.txtRebagging.HyperLink = "";
            this.txtRebagging.Left = 2.075F;
            this.txtRebagging.Name = "txtRebagging";
            this.txtRebagging.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtRebagging.Text = "";
            this.txtRebagging.Top = 0F;
            this.txtRebagging.Width = 0.605F;
            // 
            // txtAddDeductBag
            // 
            this.txtAddDeductBag.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductBag.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductBag.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductBag.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductBag.DataField = "BagAdjustment";
            this.txtAddDeductBag.Height = 0.17F;
            this.txtAddDeductBag.HyperLink = "";
            this.txtAddDeductBag.Left = 3.327F;
            this.txtAddDeductBag.Name = "txtAddDeductBag";
            this.txtAddDeductBag.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtAddDeductBag.Text = "";
            this.txtAddDeductBag.Top = 0F;
            this.txtAddDeductBag.Width = 0.5430004F;
            // 
            // txtAddDeductWeight
            // 
            this.txtAddDeductWeight.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductWeight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductWeight.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductWeight.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAddDeductWeight.DataField = "WeightAdjustment";
            this.txtAddDeductWeight.Height = 0.17F;
            this.txtAddDeductWeight.HyperLink = "";
            this.txtAddDeductWeight.Left = 3.87F;
            this.txtAddDeductWeight.Name = "txtAddDeductWeight";
            this.txtAddDeductWeight.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtAddDeductWeight.Text = "";
            this.txtAddDeductWeight.Top = 0F;
            this.txtAddDeductWeight.Width = 0.7720001F;
            // 
            // txtAdjustmentTypeID
            // 
            this.txtAdjustmentTypeID.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAdjustmentTypeID.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAdjustmentTypeID.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAdjustmentTypeID.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtAdjustmentTypeID.DataField = "AdjustmentTypeID";
            this.txtAdjustmentTypeID.Height = 0.17F;
            this.txtAdjustmentTypeID.HyperLink = "";
            this.txtAdjustmentTypeID.Left = 2.68F;
            this.txtAdjustmentTypeID.Name = "txtAdjustmentTypeID";
            this.txtAdjustmentTypeID.Style = "font-family: Tahoma; font-size: 9.75pt; font-weight: normal; text-align: center; " +
                "vertical-align: middle; ddo-char-set: 0";
            this.txtAdjustmentTypeID.Text = "";
            this.txtAdjustmentTypeID.Top = 0F;
            this.txtAdjustmentTypeID.Width = 0.6469998F;
            // 
            // rptSubGINStackBalance
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.021F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateDeposited)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGRNNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoOfBag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRebagging)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddDeductBag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddDeductWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustmentTypeID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label txtNetWeight;
        private DataDynamics.ActiveReports.Label txtDateDeposited;
        private DataDynamics.ActiveReports.Label txtGRNNo;
        private DataDynamics.ActiveReports.Label txtNoOfBag;
        private DataDynamics.ActiveReports.Label txtRebagging;
        private DataDynamics.ActiveReports.Label txtAddDeductBag;
        private DataDynamics.ActiveReports.Label txtAddDeductWeight;
        private DataDynamics.ActiveReports.Label txtAdjustmentTypeID;
    }
}
