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
    public partial class TruckLeftCompound : System.Web.UI.Page
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

            GINDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckLeftCompound", "GIN");
            GINDataEditor.IsNew = false;

            GINDataEditor.Ok += new EventHandler(GINDataEditor_Ok);
            GINDataEditor.Cancel += new EventHandler(GINDataEditor_Cancel);
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
                try
                {
                    GINDataEditor.DataSource = new LeavingTruck(GINTruckInformation);
                    LeavingTruck lt = (LeavingTruck)GINDataEditor.DataSource;
                    if (NullFinder.IsNull(lt.TruckCheckedOutOn, "System.DateTime"))
                    {
                        lt.TruckCheckedOutOn = DateTime.Now;
                    }
                    GINDataEditor.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void GINDataEditor_Cancel(object sender, EventArgs e)
        {
            GINProcessWrapper.RemoveGINProcessInformation();
            transferedData.Return();
        }

        void GINDataEditor_Ok(object sender, EventArgs e)
        {
            try
            {
                if (GINDataEditor.DataSource != null)
                {
                    GINInfo originalGIN = new GINInfo();
                    originalGIN.Copy(GINTruckInformation.GIN);
                    GINTruckInformation.GIN.Copy(((LeavingTruck)GINDataEditor.DataSource).GIN);
                    GINProcessWrapper.TruckLeftCompound(GINTruckInformation.TruckId);
                    GINProcessWrapper.RemoveGINProcessInformation();
                    transferedData.Return();
                }
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

    [Serializable]
    public class LeavingTruck
    {
        private GINTruckInfo truck;

        public LeavingTruck(GINTruckInfo truck)
        {
            this.truck = truck;
        }

        public string GINNo
        {
            get { return truck.GIN.GINNo; }
        }

        public string DriverName
        {
            get { return truck.DriverName; }
        }

        public string PlateNo
        {
            get { return truck.PlateNo; }
        }

        public string TrailerNo
        {
            get { return truck.TrailerNo; }
        }

        public Guid TruckCheckedOutBy
        {
            get { return truck.GIN.TruckCheckedOutBy; }
            set { truck.GIN.TruckCheckedOutBy = value; }
        }

        public DateTime TruckCheckedOutOn
        {
            get { return truck.GIN.TruckCheckedOutOn; }
            set { truck.GIN.TruckCheckedOutOn = value; }
        }

        public GINInfo GIN
        {
            get { return truck.GIN; }
            set { truck.GIN = value; }
        }
    }
}
