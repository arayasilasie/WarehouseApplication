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
    public partial class PickupNoticeAcknowledged : System.Web.UI.Page
    {
        private IPickupNotice pickupNotice;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();
            transferedData = new PageDataTransfer(Request.Path);
            PUNADataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("PickupNoticeAkcnowledged", "PUNAcknowledgement");
            PUNADataEditor.Ok += new EventHandler(PUNADataEditor_Ok);
            PUNADataEditor.Cancel += new EventHandler(PUNADataEditor_Cancel);
            try
            {
                pickupNotice = PUNWrapper.GetPUN(IsPostBack);
                PUNADataEditor.Lookup = pickupNotice.LookupSource;

                PUNADataEditor.Setup();
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
                PUNADataEditor.DataSource = pickupNotice.PUNAInformation;
                ((PUNAcknowledgementInformation)PUNADataEditor.DataSource).PickupNoticeVerifier = new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]);
                PUNADataEditor.DataBind();
            }
        }

        void PUNADataEditor_Cancel(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        void PUNADataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                PUNAcknowledgementInformation puna = (PUNAcknowledgementInformation)PUNADataEditor.DataSource;
                PUNWrapper.AcknowledgePickupNotice(puna);//, new AuditTrailWrapper("Initiate PUN Process", 
                    //new object[][]{new object[]{pickupNotice.PUNAInformation, puna}}));
                
                PageDataTransfer reportTransfer = new PageDataTransfer(HttpContext.Current.Request.ApplicationPath + "/ReportViewerForm.aspx");
                reportTransfer.TransferData["TransactionId"] = puna.TransactionId;
                reportTransfer.TransferData["IsGINTransaction"] = false;
                reportTransfer.TransferData["RequestedReport"] = "rptPUNTrackingReport";
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
    }
}
