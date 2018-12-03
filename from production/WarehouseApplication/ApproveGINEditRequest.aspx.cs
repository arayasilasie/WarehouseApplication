using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using WarehouseApplication.DALManager;
using WarehouseApplication.GINLogic;
using WarehouseApplication.UserControls;

namespace WarehouseApplication
{
    public partial class ApproveGINEditRequest : System.Web.UI.Page
    {
        private IGINProcess ginProcess;
        private PageDataTransfer transferedData;
        private ErrorMessageDisplayer errorDisplayer;
        private GINTruckInfo proposedGINTruckInformation;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            errorDisplayer = new ErrorMessageDisplayer(lblMessage);
            errorDisplayer.ClearErrorMessage();

            transferedData = new PageDataTransfer(Request.Path);
            TruckDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "Truck");
            TruckLoadEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "TruckLoad");
            StackGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "StackGrid");
            TruckWeightEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "TruckWeight");
            ReturnedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "ReturnedBagsGrid");

            ProposedTruckDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "Truck");
            ProposedTruckLoadEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "TruckLoad");
            ProposedStackGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "StackGrid");
            ProposedTruckWeightEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "TruckWeight");
            ProposedReturnedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("ApproveGINEditRequest", "ReturnedBagsGrid");

            TruckDataEditor.IsNew = false;
            TruckLoadEditor.IsNew = false;
            TruckWeightEditor.IsNew = false;

            ProposedTruckDataEditor.IsNew = false;
            ProposedTruckLoadEditor.IsNew = false;
            ProposedTruckWeightEditor.IsNew = false;

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                TruckDataEditor.Lookup = ginProcess.LookupSource;
                TruckLoadEditor.Lookup = ginProcess.LookupSource;
                StackGridViewer.Lookup = ginProcess.LookupSource;
                TruckWeightEditor.Lookup = ginProcess.LookupSource;
                ReturnedBagsGridViewer.Lookup = ginProcess.LookupSource;

                ProposedTruckDataEditor.Lookup = ginProcess.LookupSource;
                ProposedTruckLoadEditor.Lookup = ginProcess.LookupSource;
                ProposedStackGridViewer.Lookup = ginProcess.LookupSource;
                ProposedTruckWeightEditor.Lookup = ginProcess.LookupSource;
                ProposedReturnedBagsGridViewer.Lookup = ginProcess.LookupSource;

                TruckDataEditor.Setup();
                TruckLoadEditor.Setup();
                TruckWeightEditor.Setup();

                ProposedTruckDataEditor.Setup();
                ProposedTruckLoadEditor.Setup();
                ProposedTruckWeightEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StackGridViewer.DataSource = //GINTruckInformation.Load.Stacks;
                            from stack in GINTruckInformation.Load.Stacks
                            select new TruckStackWrapper(stack, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);

            StackGridViewer.DataBind();

            ProposedStackGridViewer.DataSource = //GINTruckInformation.Load.Stacks;
                            from stack in ProposedGINTruckInformation.Load.Stacks
                            select new TruckStackWrapper(stack, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);

            ProposedStackGridViewer.DataBind();

            ReturnedBagsGridViewer.DataSource = //GINTruckInformation.Weight.ReturnedBags;
                            from returnedBags in GINTruckInformation.Weight.ReturnedBags
                            select new ReturnedBagsWrapper(returnedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            ReturnedBagsGridViewer.DataBind();

            ProposedReturnedBagsGridViewer.DataSource = //GINTruckInformation.Weight.ReturnedBags;
                            from returnedBags in ProposedGINTruckInformation.Weight.ReturnedBags
                            select new ReturnedBagsWrapper(returnedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            ProposedReturnedBagsGridViewer.DataBind();
            
            if (!IsPostBack)
            {
                TruckDataEditor.DataSource = GINTruckInformation;
                TruckDataEditor.DataBind();
                TruckLoadEditor.DataSource = GINTruckInformation.Load;
                TruckLoadEditor.DataBind();
                TruckWeightEditor.DataSource = GINTruckInformation.Weight;
                TruckWeightEditor.DataBind();

                ProposedTruckDataEditor.DataSource = ProposedGINTruckInformation;
                ProposedTruckDataEditor.DataBind();
                ProposedTruckLoadEditor.DataSource = ProposedGINTruckInformation.Load;
                ProposedTruckLoadEditor.DataBind();
                ProposedTruckWeightEditor.DataSource = ProposedGINTruckInformation.Weight;
                ProposedTruckWeightEditor.DataBind();
            }
        }

        private GINTruckInfo GINTruckInformation
        {
            get
            {
                return ginProcess.GINProcessInformation.Trucks[0];
            }
        }

        private GINTruckInfo ProposedGINTruckInformation
        {
            get
            {
                if (proposedGINTruckInformation == null)
                {
                    GINEditingRequest request = (GINEditingRequest)transferedData.GetTransferedData("GINEditingRequest");
                    XmlSerializer s = new XmlSerializer(typeof(GINProcessInfo));
                    GINProcessInfo ginProcessInformation = (GINProcessInfo)s.Deserialize(new StringReader(request.ProposedChange));
                    proposedGINTruckInformation = ginProcessInformation.Trucks[0];
                }
                return proposedGINTruckInformation;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GINProcessWrapper.ApproveGINEditRequest();
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                GINProcessWrapper.RejectGINEditRequest();
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
