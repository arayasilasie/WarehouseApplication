using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class GenerateGIN : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();

            transferedData = new PageDataTransfer(Request.Path);
            GINDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GenerateGIN", "GIN");
            GeneralDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GenerateGIN", "General");
            CommodityDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GenerateGIN", "CommoditySpecifics");
            ProcessDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GenerateGIN", "ProcessSpecifics");
            TransportDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("GenerateGIN", "TransportSpecifics");
            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                GINDataEditor.Lookup = ginProcess.LookupSource;
                GeneralDataEditor.Lookup = ginProcess.LookupSource;
                CommodityDataEditor.Lookup = ginProcess.LookupSource;
                ProcessDataEditor.Lookup = ginProcess.LookupSource;
                TransportDataEditor.Lookup = ginProcess.LookupSource;

                GINDataEditor.Setup();
                GeneralDataEditor.Setup();
                CommodityDataEditor.Setup();
                ProcessDataEditor.Setup();
                TransportDataEditor.Setup();
            }
            catch (Exception ex)
            {
               throw ex;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GINDataEditor.DataSource = GINInformation;
                GINDataEditor.DataBind();
                try
                {
                    GINReportInfo ginReport = ginProcess.GetGINReport(GINInformation.GINId);
                    GeneralDataEditor.DataSource = ginReport;
                    GeneralDataEditor.DataBind();
                    CommodityDataEditor.DataSource = ginReport;
                    CommodityDataEditor.DataBind();
                    ProcessDataEditor.DataSource = ginReport;
                    ProcessDataEditor.DataBind();
                    TransportDataEditor.DataSource = ginReport;
                    TransportDataEditor.DataBind();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private GINInfo GINInformation
        {
            get
            {
                //Guid truckId = (Guid)transferedData.GetTransferedData("TruckId");
                //var selectedGIN = from truck in ginProcess.GINProcessInformation.Trucks
                //                  where truck.TruckId == truckId
                //                  select truck.GIN;
                //return selectedGIN.ElementAt(0);
                return ginProcess.GINProcessInformation.Trucks.ElementAt(0).GIN;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing);
            if (GINDataEditor.DataSource != null)
            {
                GINInfo editedGin = new GINInfo();
                editedGin.Copy((GINInfo)GINDataEditor.DataSource);
                //auditTrail.AddChange(GINTruckInformation.GIN, editedGin);
                GINTruckInformation.GIN.Copy((GINInfo)GINDataEditor.DataSource);
            }
            try
            {
                GINProcessWrapper.SaveGIN(GINTruckInformation.TruckId);//, auditTrail);
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckWeighing);
            if (GINDataEditor.DataSource != null)
            {
                GINInfo editedGin = new GINInfo();
                editedGin.Copy((GINInfo)GINDataEditor.DataSource);
                //auditTrail.AddChange(GINTruckInformation.GIN, editedGin);
                GINTruckInformation.GIN.Copy((GINInfo)GINDataEditor.DataSource);
            }
            try
            {
                GINProcessWrapper.SaveGIN(GINTruckInformation.TruckId);//, auditTrail);
                GINProcessWrapper.GINGenerated(GINTruckInformation.TruckId);
                //GINProcessWrapper.CompleteWorkflowTask(GINTruckInformation.TransactionId);
                PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
                reportTransfer.TransferData["TransactionId"] = GINTruckInformation.TransactionId;
                reportTransfer.TransferData["IsGINTransaction"] = true;
                reportTransfer.TransferData["RequestedReport"] = "rptGINReport";
                reportTransfer.TransferData["ReturnPage"] = transferedData.GetTransferedData("ReturnPage");
                reportTransfer.PersistToSession();
                ScriptManager.RegisterStartupScript(this,
                    this.GetType(),
                    "ShowReport",
                    "<script type=\"text/javascript\">" +
                        string.Format("javascript:window.open(\"ReportViewerForm.aspx?id={0}\", \"_blank\",\"height=400px,width=600px,top=0,left=0,resizable=yes,scrollbars=yes\");", Guid.NewGuid()) +
                        string.Format("location.href = '{0}';", transferedData.GetTransferedData("ReturnPage")) +
                    "</script>",
                    false);
                GINProcessWrapper.RemoveGINProcessInformation();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        private GINTruckInfo GINTruckInformation
        {
            get
            {
                return ginProcess.GINProcessInformation.Trucks.ElementAt(0);
            }
        }
    }
}
