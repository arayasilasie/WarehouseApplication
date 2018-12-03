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
using WarehouseApplication.BLL;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class ApproveGIN : System.Web.UI.Page
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
            GINDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGIN", "GIN");
            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                GINDataEditor.Lookup = ginProcess.LookupSource;

                GINDataEditor.Setup();
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
                GINInfo ginInfo = (GINInfo)GINDataEditor.DataSource;
                if (NullFinder.IsNull(ginInfo.DateApproved, "System.DateTime"))
                {
                    ginInfo.DateApproved = DateTime.Now;
                }
                GINDataEditor.DataBind();
            }
        }

        private GINInfo GINInformation
        {
            get
            {
                GINInfo gin = ginProcess.GINProcessInformation.Trucks.ElementAt(0).GIN;
                gin.ApprovedBy = UserBLL.GetCurrentUser();
                return gin;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.GINAcceptance);
            if (GINDataEditor.DataSource != null)
            {
                //GINInfo editedGin = new GINInfo();
                //editedGin.Copy((GINInfo)GINDataEditor.DataSource);
                //auditTrail.AddChange(GINTruckInformation.GIN, editedGin);
                GINTruckInformation.GIN.Copy((GINInfo)GINDataEditor.DataSource);
            }
            try
            {
                GINProcessWrapper.SaveGIN(GINTruckInformation.TruckId);//, auditTrail);
                //GINProcessWrapper.RemoveGINProcessInformation();
                //transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.GINApproval);
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
                GINProcessWrapper.GINApproved(GINTruckInformation.TruckId);
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
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
