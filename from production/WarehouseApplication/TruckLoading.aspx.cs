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
    public partial class TruckLoading : System.Web.UI.Page
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
            TruckDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckLoading", "Truck");
            TruckLoadEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckLoading", "TruckLoad");
            StackDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckLoading", "Stack");
            StackGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckLoading", "StackGrid");

            TruckDataEditor.IsNew = false;

            StackDataEditor.Ok += new EventHandler(StackDataEditor_Ok);
            StackDataEditor.Cancel += new EventHandler(StackDataEditor_Cancel);

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);

                TruckDataEditor.Lookup = ginProcess.LookupSource;
                TruckLoadEditor.Lookup = ginProcess.LookupSource;
                StackDataEditor.Lookup = ginProcess.LookupSource;
                StackGridViewer.Lookup = ginProcess.LookupSource;

                TruckDataEditor.Setup();
                TruckLoadEditor.Setup();
                StackDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StackGridViewer.DataSource = 
                from stack in GINTruckInformation.Load.Stacks
                select new TruckStackWrapper(stack, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);

            StackGridViewer.DataBind();
            var gridCommands = from command in StackGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                TruckDataEditor.DataSource = GINTruckInformation;
                TruckDataEditor.DataBind();
                TruckLoadEditor.DataSource = GINTruckInformation.Load;
                TruckLoadEditor.DataBind();
            }
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditStack")
            {
                StackDataEditor.IsNew = false;
                var stackToEdit = from stack in GINTruckInformation.Load.Stacks
                                  where stack.TruckStackId == new Guid((string)e.CommandArgument)
                                  select stack;
                StackDataEditor.DataSource = new TruckStackWrapper(stackToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                StackDataEditor.DataBind();
                mpeStackDataEditorExtender.Show();
            }
        }


        void StackDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeStackDataEditorExtender.Hide();
        }

        void StackDataEditor_Ok(object sender, EventArgs e)
        {
            if (((TruckStackWrapper)StackDataEditor.DataSource).StackId == Guid.Empty)
            {
                errorDisplayer.ShowErrorMessage("Stack is required");
            }
            else if (StackDataEditor.IsNew)
            {
                ginProcess.AddStack(GINTruckInformation.Load.TruckId, ((TruckStackWrapper)StackDataEditor.DataSource).TSInfo);
                StackGridViewer.DataBind();
                TruckLoadEditor.DataSource = GINTruckInformation.Load;
                TruckLoadEditor.DataBind();
                StackDataEditor.IsNew = false;
            }
            else
            {
                var editedStack = from stack in GINTruckInformation.Load.Stacks
                                  where stack.TruckStackId == ((TruckStackWrapper)StackDataEditor.DataSource).TruckStackId
                                  select stack;
                if (editedStack.Count() > 0)
                {
                    editedStack.ElementAt(0).Copy(((TruckStackWrapper)StackDataEditor.DataSource).TSInfo);
                    StackGridViewer.DataBind();
                    TruckLoadEditor.DataSource = GINTruckInformation.Load;
                    TruckLoadEditor.DataBind();
                }
            }
            mpeStackDataEditorExtender.Hide();
        }

        private GINTruckInfo GINTruckInformation
        {
            get
            {
                return ginProcess.GINProcessInformation.Trucks[0];
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading);
                GINTruckInformation.Copy((GINTruckInfo)TruckDataEditor.DataSource);
                if (TruckLoadEditor.DataSource != null)
                {
                    //TruckLoadInfo originalLoad = new TruckLoadInfo();
                    //originalLoad.Copy(GINTruckInformation.Load);
                    GINTruckInformation.Load.Copy((TruckLoadInfo)TruckLoadEditor.DataSource);
                    //auditTrail.AddChange(originalLoad, GINTruckInformation.Load);
                }
                GINProcessWrapper.SaveLoading(GINTruckInformation.TruckId);//, auditTrail);
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
            try
            {
                //AuditTrailWrapper auditTrail = new AuditTrailWrapper(AuditTrailWrapper.TruckLoading);
                GINTruckInformation.Copy((GINTruckInfo)TruckDataEditor.DataSource);
                if (TruckLoadEditor.DataSource != null)
                {
                    TruckLoadInfo originalLoad = new TruckLoadInfo();
                    originalLoad.Copy(GINTruckInformation.Load);
                    GINTruckInformation.Load.Copy((TruckLoadInfo)TruckLoadEditor.DataSource);
                    //auditTrail.AddChange(originalLoad, GINTruckInformation.Load);
                }
                GINProcessWrapper.SaveLoading(GINTruckInformation.TruckId);//, auditTrail);
                GINProcessWrapper.CompleteLoading(GINTruckInformation.TruckId);
                GINProcessWrapper.RemoveGINProcessInformation();
                transferedData.Return();
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnAddStack_Click(object sender, EventArgs e)
        {
            StackDataEditor.IsNew = true;
            StackDataEditor.DataSource = new TruckStackWrapper(ginProcess.GetBlankTruckStack(GINTruckInformation.Load.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            StackDataEditor.DataBind();
            mpeStackDataEditorExtender.Show();
        }
    }

    [Serializable]
    public class TruckStackWrapper
    {
        private TruckStackInfo tsInfo;
        private Guid commodityGradeId;
        private int productionYear;
        public TruckStackWrapper(TruckStackInfo tsInfo, Guid commodityGradeId, int productionYear)
        {
            this.tsInfo = tsInfo;
            this.commodityGradeId = commodityGradeId;
            this.productionYear = productionYear;
        }

        public TruckStackInfo TSInfo
        {
            get { return tsInfo; }
        }

        public int Bags
        {
            get { return tsInfo.Bags; }
            set { tsInfo.Bags = value; }
        }

        public Guid LoadingSupervisor
        {
            get { return tsInfo.LoadingSupervisor; }
            set { tsInfo.LoadingSupervisor = value; }
        }

        public string Shed
        {
            get { return string.Format("{0}_{1}_{2}", tsInfo.Shed, commodityGradeId, productionYear); }
            set 
            {
                string[] idPair = value.Split('_');
                tsInfo.Shed = new Guid(idPair[0]);
                commodityGradeId = new Guid(idPair[1]);
                productionYear = int.Parse(idPair[2]);
            }
        }

        public Guid StackId
        {
            get { return tsInfo.StackId; }
            set { tsInfo.StackId = value; }
        }

        public Guid TruckId
        {
            get { return tsInfo.TruckId; }
            set { tsInfo.TruckId = value; }
        }

        public Guid TruckStackId
        {
            get { return tsInfo.TruckStackId; }
            set { tsInfo.TruckStackId = value; }
        }
    }
}
