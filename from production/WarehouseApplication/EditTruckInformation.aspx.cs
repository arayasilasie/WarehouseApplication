using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class EditTruckInformation : System.Web.UI.Page
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
            TruckDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("EditTruckInformation", "Truck");

            TruckDataEditor.IsNew = false;

            TruckDataEditor.Ok += new EventHandler(TruckDataEditor_Ok);
            TruckDataEditor.Cancel += new EventHandler(TruckDataEditor_Cancel);

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                TruckDataEditor.Lookup = ginProcess.LookupSource;

                TruckDataEditor.Setup();
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
                TruckDataEditor.DataSource = GINTruckInformation;
                TruckDataEditor.DataBind();
            }
        }

        void TruckDataEditor_Cancel(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        void TruckDataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckRegistration);
                //GINTruckInfo original = new GINTruckInfo();
                //original.Copy(GINTruckInformation);
                //auditTrail.AddChange(original, TruckDataEditor.DataSource);
                GINTruckInformation.Copy((GINTruckInfo)TruckDataEditor.DataSource);
                GINProcessWrapper.SaveTruckInformation(GINTruckInformation.TruckId);//, auditTrail);
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
                return ginProcess.GINProcessInformation.Trucks[0];
            }
        }

    }
}
