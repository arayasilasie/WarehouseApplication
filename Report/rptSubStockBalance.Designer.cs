namespace WarehouseApplication.Reports
{
    /// <summary>
    /// Summary description for rptSubStockBalance.
    /// </summary>
    partial class rptSubStockBalance
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(rptSubStockBalance));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.lblDepositedDate = new DataDynamics.ActiveReports.Label();
            this.lblClientName = new DataDynamics.ActiveReports.Label();
            this.lblGINNumber = new DataDynamics.ActiveReports.Label();
            this.lblGrade = new DataDynamics.ActiveReports.Label();
            this.lblDepositTicketNumber = new DataDynamics.ActiveReports.Label();
            this.lblWHRNumber = new DataDynamics.ActiveReports.Label();
            this.lblScaleTicketNumber = new DataDynamics.ActiveReports.Label();
            this.lblTruckPlateNumber = new DataDynamics.ActiveReports.Label();
            this.lblTrailerPlateNumber = new DataDynamics.ActiveReports.Label();
            this.lblNumberOfBags = new DataDynamics.ActiveReports.Label();
            this.lblNumberOfRebagging = new DataDynamics.ActiveReports.Label();
            this.lblNetWeight = new DataDynamics.ActiveReports.Label();
            this.lblAdd_DedBag = new DataDynamics.ActiveReports.Label();
            this.lblWbsp = new DataDynamics.ActiveReports.Label();
            this.lblAdd_DedWeight = new DataDynamics.ActiveReports.Label();
            this.lblLabourerGroup = new DataDynamics.ActiveReports.Label();
            this.lblStackNumber = new DataDynamics.ActiveReports.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepositedDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepositTicketNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWHRNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblScaleTicketNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTruckPlateNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTrailerPlateNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberOfBags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberOfRebagging)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNetWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAdd_DedBag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWbsp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAdd_DedWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabourerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStackNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblDepositedDate,
            this.lblClientName,
            this.lblGINNumber,
            this.lblGrade,
            this.lblDepositTicketNumber,
            this.lblWHRNumber,
            this.lblScaleTicketNumber,
            this.lblTruckPlateNumber,
            this.lblTrailerPlateNumber,
            this.lblNumberOfBags,
            this.lblNumberOfRebagging,
            this.lblNetWeight,
            this.lblAdd_DedBag,
            this.lblWbsp,
            this.lblAdd_DedWeight,
            this.lblLabourerGroup,
            this.lblStackNumber});
            this.detail.Height = 0.5410001F;
            this.detail.Name = "detail";
            // 
            // lblDepositedDate
            // 
            this.lblDepositedDate.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblDepositedDate.DataField = "DateTimeLoaded";
            this.lblDepositedDate.Height = 0.532F;
            this.lblDepositedDate.HyperLink = null;
            this.lblDepositedDate.Left = 0F;
            this.lblDepositedDate.Name = "lblDepositedDate";
            this.lblDepositedDate.Style = "font-weight: normal";
            this.lblDepositedDate.Text = "";
            this.lblDepositedDate.Top = 0F;
            this.lblDepositedDate.Width = 0.83F;
            // 
            // lblClientName
            // 
            this.lblClientName.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblClientName.DataField = "ClientName";
            this.lblClientName.Height = 0.532F;
            this.lblClientName.HyperLink = null;
            this.lblClientName.Left = 0.83F;
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Style = "font-weight: normal";
            this.lblClientName.Text = "";
            this.lblClientName.Top = 0F;
            this.lblClientName.Width = 1.753F;
            // 
            // lblGINNumber
            // 
            this.lblGINNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGINNumber.DataField = "GINNumber";
            this.lblGINNumber.Height = 0.532F;
            this.lblGINNumber.HyperLink = null;
            this.lblGINNumber.Left = 2.583F;
            this.lblGINNumber.Name = "lblGINNumber";
            this.lblGINNumber.Style = "font-weight: normal";
            this.lblGINNumber.Text = "";
            this.lblGINNumber.Top = 0F;
            this.lblGINNumber.Width = 0.75F;
            // 
            // lblGrade
            // 
            this.lblGrade.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblGrade.DataField = "Symbol";
            this.lblGrade.Height = 0.532F;
            this.lblGrade.HyperLink = null;
            this.lblGrade.Left = 3.333F;
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Style = "font-weight: normal";
            this.lblGrade.Text = "";
            this.lblGrade.Top = 0F;
            this.lblGrade.Width = 0.597F;
            // 
            // lblDepositTicketNumber
            // 
            this.lblDepositTicketNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblDepositTicketNumber.DataField = "LoadUnloadTicketNO";
            this.lblDepositTicketNumber.Height = 0.532F;
            this.lblDepositTicketNumber.HyperLink = null;
            this.lblDepositTicketNumber.Left = 4.638F;
            this.lblDepositTicketNumber.Name = "lblDepositTicketNumber";
            this.lblDepositTicketNumber.Style = "font-weight: normal";
            this.lblDepositTicketNumber.Text = "";
            this.lblDepositTicketNumber.Top = 0F;
            this.lblDepositTicketNumber.Width = 0.823F;
            // 
            // lblWHRNumber
            // 
            this.lblWHRNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWHRNumber.DataField = "WarehouseReceiptNo";
            this.lblWHRNumber.Height = 0.532F;
            this.lblWHRNumber.HyperLink = null;
            this.lblWHRNumber.Left = 3.93F;
            this.lblWHRNumber.Name = "lblWHRNumber";
            this.lblWHRNumber.Style = "font-weight: normal";
            this.lblWHRNumber.Text = "";
            this.lblWHRNumber.Top = 0F;
            this.lblWHRNumber.Width = 0.7079993F;
            // 
            // lblScaleTicketNumber
            // 
            this.lblScaleTicketNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblScaleTicketNumber.DataField = "ScaleTicketNumber";
            this.lblScaleTicketNumber.Height = 0.532F;
            this.lblScaleTicketNumber.HyperLink = null;
            this.lblScaleTicketNumber.Left = 5.461F;
            this.lblScaleTicketNumber.Name = "lblScaleTicketNumber";
            this.lblScaleTicketNumber.Style = "font-weight: normal";
            this.lblScaleTicketNumber.Text = "";
            this.lblScaleTicketNumber.Top = 0F;
            this.lblScaleTicketNumber.Width = 0.812F;
            // 
            // lblTruckPlateNumber
            // 
            this.lblTruckPlateNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblTruckPlateNumber.DataField = "PlateNumber";
            this.lblTruckPlateNumber.Height = 0.532F;
            this.lblTruckPlateNumber.HyperLink = null;
            this.lblTruckPlateNumber.Left = 6.273F;
            this.lblTruckPlateNumber.Name = "lblTruckPlateNumber";
            this.lblTruckPlateNumber.Style = "font-weight: normal";
            this.lblTruckPlateNumber.Text = "";
            this.lblTruckPlateNumber.Top = 0F;
            this.lblTruckPlateNumber.Width = 0.761F;
            // 
            // lblTrailerPlateNumber
            // 
            this.lblTrailerPlateNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblTrailerPlateNumber.DataField = "TrailerPlateNumber";
            this.lblTrailerPlateNumber.Height = 0.532F;
            this.lblTrailerPlateNumber.HyperLink = null;
            this.lblTrailerPlateNumber.Left = 7.034F;
            this.lblTrailerPlateNumber.Name = "lblTrailerPlateNumber";
            this.lblTrailerPlateNumber.Style = "font-weight: normal";
            this.lblTrailerPlateNumber.Text = "";
            this.lblTrailerPlateNumber.Top = 0F;
            this.lblTrailerPlateNumber.Width = 0.927F;
            // 
            // lblNumberOfBags
            // 
            this.lblNumberOfBags.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblNumberOfBags.DataField = "NoOfBags";
            this.lblNumberOfBags.Height = 0.532F;
            this.lblNumberOfBags.HyperLink = null;
            this.lblNumberOfBags.Left = 7.961F;
            this.lblNumberOfBags.Name = "lblNumberOfBags";
            this.lblNumberOfBags.Style = "font-weight: normal; text-align: right";
            this.lblNumberOfBags.Text = "";
            this.lblNumberOfBags.Top = 0F;
            this.lblNumberOfBags.Width = 0.7710007F;
            // 
            // lblNumberOfRebagging
            // 
            this.lblNumberOfRebagging.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblNumberOfRebagging.DataField = "NoOfRebags";
            this.lblNumberOfRebagging.Height = 0.532F;
            this.lblNumberOfRebagging.HyperLink = null;
            this.lblNumberOfRebagging.Left = 8.732F;
            this.lblNumberOfRebagging.Name = "lblNumberOfRebagging";
            this.lblNumberOfRebagging.Style = "font-weight: normal; text-align: right";
            this.lblNumberOfRebagging.Text = "";
            this.lblNumberOfRebagging.Top = 0F;
            this.lblNumberOfRebagging.Width = 0.6560001F;
            // 
            // lblNetWeight
            // 
            this.lblNetWeight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblNetWeight.DataField = "NetWeight";
            this.lblNetWeight.Height = 0.532F;
            this.lblNetWeight.HyperLink = null;
            this.lblNetWeight.Left = 9.388F;
            this.lblNetWeight.Name = "lblNetWeight";
            this.lblNetWeight.Style = "font-weight: normal; text-align: right";
            this.lblNetWeight.Text = "";
            this.lblNetWeight.Top = 0F;
            this.lblNetWeight.Width = 1.016F;
            // 
            // lblAdd_DedBag
            // 
            this.lblAdd_DedBag.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblAdd_DedBag.DataField = "BagAdjustment";
            this.lblAdd_DedBag.Height = 0.532F;
            this.lblAdd_DedBag.HyperLink = null;
            this.lblAdd_DedBag.Left = 10.404F;
            this.lblAdd_DedBag.Name = "lblAdd_DedBag";
            this.lblAdd_DedBag.Style = "font-weight: normal; text-align: right";
            this.lblAdd_DedBag.Text = "";
            this.lblAdd_DedBag.Top = 0F;
            this.lblAdd_DedBag.Width = 0.792F;
            // 
            // lblWbsp
            // 
            this.lblWbsp.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblWbsp.DataField = "ServiceProviderName";
            this.lblWbsp.Height = 0.5320001F;
            this.lblWbsp.HyperLink = null;
            this.lblWbsp.Left = 12.061F;
            this.lblWbsp.Name = "lblWbsp";
            this.lblWbsp.Style = "font-weight: normal";
            this.lblWbsp.Text = "";
            this.lblWbsp.Top = 0.009000001F;
            this.lblWbsp.Width = 1.785001F;
            // 
            // lblAdd_DedWeight
            // 
            this.lblAdd_DedWeight.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblAdd_DedWeight.DataField = "WeightAdjustment";
            this.lblAdd_DedWeight.Height = 0.532F;
            this.lblAdd_DedWeight.HyperLink = null;
            this.lblAdd_DedWeight.Left = 11.196F;
            this.lblAdd_DedWeight.Name = "lblAdd_DedWeight";
            this.lblAdd_DedWeight.Style = "font-weight: normal; text-align: right";
            this.lblAdd_DedWeight.Text = "";
            this.lblAdd_DedWeight.Top = 0F;
            this.lblAdd_DedWeight.Width = 0.8649999F;
            // 
            // lblLabourerGroup
            // 
            this.lblLabourerGroup.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblLabourerGroup.DataField = "AssociationName";
            this.lblLabourerGroup.Height = 0.5320001F;
            this.lblLabourerGroup.HyperLink = null;
            this.lblLabourerGroup.Left = 13.846F;
            this.lblLabourerGroup.Name = "lblLabourerGroup";
            this.lblLabourerGroup.Style = "font-weight: normal";
            this.lblLabourerGroup.Text = "";
            this.lblLabourerGroup.Top = 0.009000001F;
            this.lblLabourerGroup.Width = 1.749998F;
            // 
            // lblStackNumber
            // 
            this.lblStackNumber.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblStackNumber.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblStackNumber.DataField = "StackNumber";
            this.lblStackNumber.Height = 0.5320001F;
            this.lblStackNumber.HyperLink = null;
            this.lblStackNumber.Left = 15.596F;
            this.lblStackNumber.Name = "lblStackNumber";
            this.lblStackNumber.Style = "font-weight: normal";
            this.lblStackNumber.Text = "";
            this.lblStackNumber.Top = 0F;
            this.lblStackNumber.Width = 1.238001F;
            // 
            // rptSubStockBalance
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 16.88542F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.lblDepositedDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblClientName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGINNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepositTicketNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWHRNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblScaleTicketNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTruckPlateNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTrailerPlateNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberOfBags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberOfRebagging)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNetWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAdd_DedBag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWbsp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAdd_DedWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLabourerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStackNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Label lblDepositedDate;
        private DataDynamics.ActiveReports.Label lblClientName;
        private DataDynamics.ActiveReports.Label lblGINNumber;
        private DataDynamics.ActiveReports.Label lblGrade;
        private DataDynamics.ActiveReports.Label lblDepositTicketNumber;
        private DataDynamics.ActiveReports.Label lblWHRNumber;
        private DataDynamics.ActiveReports.Label lblScaleTicketNumber;
        private DataDynamics.ActiveReports.Label lblTruckPlateNumber;
        private DataDynamics.ActiveReports.Label lblTrailerPlateNumber;
        private DataDynamics.ActiveReports.Label lblNumberOfBags;
        private DataDynamics.ActiveReports.Label lblNumberOfRebagging;
        private DataDynamics.ActiveReports.Label lblNetWeight;
        private DataDynamics.ActiveReports.Label lblAdd_DedBag;
        private DataDynamics.ActiveReports.Label lblWbsp;
        private DataDynamics.ActiveReports.Label lblAdd_DedWeight;
        private DataDynamics.ActiveReports.Label lblLabourerGroup;
        private DataDynamics.ActiveReports.Label lblStackNumber;
    }
}
