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
using WarehouseApplication.BLL;

namespace WarehouseApplication
{
    public partial class VerifyGINAvailability : System.Web.UI.Page
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

            PUNADataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("VerifyGINAvailability", "PUNAcknowledgement");
            PUNADataEditor.IsNew = false;

            //PUNADataEditor.Ok += new EventHandler(PUNADataEditor_Ok);
            //PUNADataEditor.Cancel += new EventHandler(PUNADataEditor_Cancel);
            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                PUNADataEditor.Lookup = ginProcess.LookupSource;

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
                try
                {
                    //ginProcess.GINProcessInformation.PUNAcknowledgement.AvailabilityVerifier = new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]);
                    PUNADataEditor.DataSource = ginProcess.GINProcessInformation.PUNAcknowledgement;
                    ((PUNAcknowledgementInformation)PUNADataEditor.DataSource).AvailabilityVerifier = new Guid(SystemLookup.LookupSource.GetLookup("CurrentUser")["Id"]);
                    PUNADataEditor.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //void PUNADataEditor_Cancel(object sender, EventArgs e)
        //{
        //    GINProcessWrapper.RemoveGINProcessInformation();
        //    transferedData.Return();
        //}

        //void PUNADataEditor_Ok(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        PUNAcknowledgementInformation acknowledgement = (PUNAcknowledgementInformation)PUNADataEditor.DataSource;
        //        GINProcessWrapper.SaveAvailabilityVerification(acknowledgement);
        //        GINProcessWrapper.CompleteAvailabilityVerification();
        //        GINProcessWrapper.RemoveGINProcessInformation();
        //        transferedData.Return();
        //    }
        //    catch (Exception ex)
        //    {
        //        errorDisplayer.ShowErrorMessage(ex.Message);
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                PUNAcknowledgementInformation acknowledgement = (PUNAcknowledgementInformation)PUNADataEditor.DataSource;
                PUNAcknowledgementInformation originalAcknowledgement = ginProcess.GINProcessInformation.PUNAcknowledgement;
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper("Inverntory Verification",
                //    new object[][]{new object[]{originalAcknowledgement, acknowledgement}});
                GINProcessWrapper.SaveAvailabilityVerification(acknowledgement);//, auditTrail);
                GINProcessWrapper.CompleteAvailabilityVerification();
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            try
            {
                GINProcessWrapper.RejectGINProcess();
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }
    }
}
