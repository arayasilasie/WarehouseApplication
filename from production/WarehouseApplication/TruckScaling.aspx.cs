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
    public partial class TruckScaling : System.Web.UI.Page
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
            GINDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "GIN");
            TruckWeightEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "TruckWeight");
            GINIssuanceEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "GINIssuance");
            ReturnedBagsDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "ReturnedBags");
            ReturnedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "ReturnedBagsGrid");
            AddedBagsDataEditor.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "AddedBags");
            AddedBagsGridViewer.Driver = GINViewConfigurationReader.GetViewConfiguration("TruckScaling", "AddedBagsGrid");

            GINDataEditor.IsNew = false;
            TruckWeightEditor.IsNew = false;
            GINIssuanceEditor.IsNew = false;

            ReturnedBagsDataEditor.Ok += new EventHandler(ReturnedBagsDataEditor_Ok);
            ReturnedBagsDataEditor.Cancel += new EventHandler(ReturnedBagsDataEditor_Cancel);
            AddedBagsDataEditor.Ok += new EventHandler(AddedBagsDataEditor_Ok);
            AddedBagsDataEditor.Cancel += new EventHandler(AddedBagsDataEditor_Cancel);

            try
            {
                ginProcess = GINProcessWrapper.GetGINProcess(IsPostBack);
                GINDataEditor.Lookup = ginProcess.LookupSource;
                TruckWeightEditor.Lookup = ginProcess.LookupSource;
                GINIssuanceEditor.Lookup = ginProcess.LookupSource;
                ReturnedBagsDataEditor.Lookup = ginProcess.LookupSource;
                ReturnedBagsGridViewer.Lookup = ginProcess.LookupSource;
                AddedBagsDataEditor.Lookup = ginProcess.LookupSource;
                AddedBagsGridViewer.Lookup = ginProcess.LookupSource;

                GINDataEditor.Setup();
                TruckWeightEditor.Setup();
                GINIssuanceEditor.Setup();
                ReturnedBagsDataEditor.Setup();
                AddedBagsDataEditor.Setup();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReturnedBagsGridViewer.DataSource =
                from returnedBags in GINTruckInformation.Weight.ReturnedBags
                select new ReturnedBagsWrapper(returnedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            ReturnedBagsGridViewer.DataBind();

            AddedBagsGridViewer.DataSource =
                from addedBags in GINTruckInformation.Weight.AddedBags
                select new ReturnedBagsWrapper(addedBags, ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            AddedBagsGridViewer.DataBind();

            var gridCommands = from command in ReturnedBagsGridViewer.Driver.Columns
                               where command.IsCommand
                               select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in gridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            var addedGridCommands = from command in AddedBagsGridViewer.Driver.Columns
                                    where command.IsCommand
                                    select command.AttachedRenderer;
            foreach (LinkGINColumnRenderer linkCommand in addedGridCommands)
            {
                linkCommand.Command += new CommandEventHandler(linkCommand_Command);
            }

            if (!IsPostBack)
            {
                try
                {
                    GINDataEditor.DataSource = ginProcess.GetGINReport(GINTruckInformation.TruckId);
                    GINDataEditor.DataBind();
                    TruckWeightEditor.DataSource = GINTruckInformation.Weight;
                    TruckWeightEditor.DataBind();
                    GINIssuanceEditor.DataSource = GINTruckInformation.GIN;
                    GINInfo ginInfo = (GINInfo)GINIssuanceEditor.DataSource;
                    if (NullFinder.IsNull(ginInfo.DateIssued, "System.DateTime"))
                    {
                        ginInfo.DateIssued = DateTime.Now;
                    }
                    GINIssuanceEditor.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            TruckWeightInfo newWeight = (TruckWeightInfo)TruckWeightEditor.DataSource;
            decimal truckWeight = GINTruckInformation.Weight.TruckWeight;
            decimal grossWeight = GINTruckInformation.Weight.GrossWeight;
            GINTruckInformation.Weight.TruckWeight = newWeight.TruckWeight;
            GINTruckInformation.Weight.GrossWeight = newWeight.GrossWeight;
            lblNetWeight.Text = ginProcess.CalculateNetWeight(newWeight.TruckId).ToString();
            GINTruckInformation.Weight.TruckWeight = truckWeight;
            GINTruckInformation.Weight.GrossWeight = grossWeight;
        }

        void linkCommand_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditReturnedBags")
            {
                ReturnedBagsDataEditor.IsNew = false;
                var returnedBagsToEdit = from returnedBags in GINTruckInformation.Weight.ReturnedBags
                                         where returnedBags.ReturnedBagsId == new Guid((string)e.CommandArgument)
                                         select returnedBags;
                ReturnedBagsDataEditor.DataSource = new ReturnedBagsWrapper(returnedBagsToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                ReturnedBagsDataEditor.DataBind();
                mpeReturnedBagsDataEditorExtender.Show();
            }
            if (e.CommandName == "EditAddedBags")
            {
                AddedBagsDataEditor.IsNew = false;
                var addedBagsToEdit = from addedBags in GINTruckInformation.Weight.AddedBags
                                      where addedBags.ReturnedBagsId == new Guid((string)e.CommandArgument)
                                      select addedBags;
                AddedBagsDataEditor.DataSource = new ReturnedBagsWrapper(addedBagsToEdit.ElementAt(0), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
                AddedBagsDataEditor.DataBind();
                mpeAddedBagsDataEditorExtender.Show();
            }
        }


        void ReturnedBagsDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeReturnedBagsDataEditorExtender.Hide();
        }

        void ReturnedBagsDataEditor_Ok(object sender, EventArgs e)
        {
            ReturnedBagsWrapper rbw = (ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource;
            if (rbw.StackId == Guid.Empty)
            {
                errorDisplayer.ShowErrorMessage("Stack is required");
            }
            else if (rbw.Size < 0.0001M)
            {
                errorDisplayer.ShowErrorMessage("Wieght is required");
            }
            else if (ReturnedBagsDataEditor.IsNew)
            {
                ginProcess.AddReturnedBags(GINTruckInformation.Weight.TruckId, ((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).RBInfo);
                ReturnedBagsGridViewer.DataBind();
                ReturnedBagsDataEditor.IsNew = false;
            }
            else
            {
                var editedReturnedBags = from returnedBags in GINTruckInformation.Weight.ReturnedBags
                                         where returnedBags.ReturnedBagsId == ((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).ReturnedBagsId
                                         select returnedBags;
                if (editedReturnedBags.Count() > 0)
                {
                    editedReturnedBags.ElementAt(0).Copy(((ReturnedBagsWrapper)ReturnedBagsDataEditor.DataSource).RBInfo);
                    ReturnedBagsGridViewer.DataBind();
                }
            }
            mpeReturnedBagsDataEditorExtender.Hide();
        }


        void AddedBagsDataEditor_Cancel(object sender, EventArgs e)
        {
            mpeAddedBagsDataEditorExtender.Hide();
        }

        void AddedBagsDataEditor_Ok(object sender, EventArgs e)
        {
            ReturnedBagsWrapper rbw = (ReturnedBagsWrapper)AddedBagsDataEditor.DataSource;
            if (rbw.StackId == Guid.Empty)
            {
                errorDisplayer.ShowErrorMessage("Stack is required");
            }
            else if (rbw.Size < 0.0001M)
            {
                errorDisplayer.ShowErrorMessage("Wieght is required");
            }
            else if (AddedBagsDataEditor.IsNew)
            {
                ginProcess.AddReturnedBags(GINTruckInformation.Weight.TruckId, ((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).RBInfo);
                AddedBagsGridViewer.DataBind();
                AddedBagsDataEditor.IsNew = false;
            }
            else
            {
                var editedAddedBags = from addedBags in GINTruckInformation.Weight.AddedBags
                                      where addedBags.ReturnedBagsId == ((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).ReturnedBagsId
                                      select addedBags;
                if (editedAddedBags.Count() > 0)
                {
                    editedAddedBags.ElementAt(0).Copy(((ReturnedBagsWrapper)AddedBagsDataEditor.DataSource).RBInfo);
                    AddedBagsGridViewer.DataBind();
                }
            }
            mpeAddedBagsDataEditorExtender.Hide();
        }

        protected void btnAddReturnedBags_Click(object sender, EventArgs e)
        {
            ReturnedBagsDataEditor.IsNew = true;
            ReturnedBagsWrapper returnedBagsWrapper = new ReturnedBagsWrapper(ginProcess.GetBlankReturnedBags(GINTruckInformation.Weight.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            returnedBagsWrapper.Returned = true;
            ReturnedBagsDataEditor.DataSource = returnedBagsWrapper;
            ReturnedBagsDataEditor.DataBind();
            mpeReturnedBagsDataEditorExtender.Show();
        }

        protected void btnAddAddedBags_Click(object sender, EventArgs e)
        {
            AddedBagsDataEditor.IsNew = true;
            ReturnedBagsWrapper addedBagsWrapper = new ReturnedBagsWrapper(ginProcess.GetBlankReturnedBags(GINTruckInformation.Weight.TruckId), ginProcess.GINProcessInformation.CommodityGradeId, ginProcess.GINProcessInformation.ProductionYear);
            addedBagsWrapper.Returned = false;
            AddedBagsDataEditor.DataSource = addedBagsWrapper;
            AddedBagsDataEditor.DataBind();
            mpeAddedBagsDataEditorExtender.Show();
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
                GINTruckInformation.Weight.Copy((TruckWeightInfo)TruckWeightEditor.DataSource);
                if (SuspeciousTruckWeight())
                {
                    ViewState["SuspeciousSaveMode"] = "Save";
                    currentTruckWeight.InnerText = GINTruckInformation.Weight.TruckWeight.ToString();
                    lastTruckWeight.InnerText = new TruckRegistrationInfo()
                    {
                        GINTruck = GINTruckInformation
                    }.LatestActiveWeight.Weight.ToString();
                    mpeSuspeciousTruckWarningExtender.Show();
                }
                else
                {
                    Save();
                }
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
                GINTruckInformation.Weight.Copy((TruckWeightInfo)TruckWeightEditor.DataSource);
                if (SuspeciousTruckWeight())
                {
                    ViewState["SuspeciousSaveMode"] = "Confirm";
                    currentTruckWeight.InnerText = GINTruckInformation.Weight.TruckWeight.ToString();
                    lastTruckWeight.InnerText = new TruckRegistrationInfo()
                    {
                        GINTruck = GINTruckInformation
                    }.LatestActiveWeight.Weight.ToString();
                    mpeSuspeciousTruckWarningExtender.Show();
                }
                else
                {
                    Save();
                    Confirm();
                }
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            //TruckWeightInfo oldWeight = new TruckWeightInfo();
            //oldWeight.Copy(GINTruckInformation.Weight);
            //GINTruckInformation.Weight.Copy((TruckWeightInfo)this.TruckWeightEditor.DataSource);
            TruckWeightInfo newWeight = (TruckWeightInfo)TruckWeightEditor.DataSource;
            decimal truckWeight = GINTruckInformation.Weight.TruckWeight;
            decimal grossWeight = GINTruckInformation.Weight.GrossWeight;
            GINTruckInformation.Weight.TruckWeight = newWeight.TruckWeight;
            GINTruckInformation.Weight.GrossWeight = newWeight.GrossWeight;
            lblNetWeight.Text = ginProcess.CalculateNetWeight(newWeight.TruckId).ToString();
            GINTruckInformation.Weight.TruckWeight = truckWeight;
            GINTruckInformation.Weight.GrossWeight = grossWeight;
            //GINTruckInformation.Weight.Copy(oldWeight);
        }

        private void Save()
        {
            if (GINIssuanceEditor.DataSource != null)
            {
                GINTruckInformation.GIN.Copy((GINInfo)GINIssuanceEditor.DataSource);
            }
            if (TruckWeightEditor.DataSource != null)
            {
                TruckWeightInfo truckWeight = (TruckWeightInfo)TruckWeightEditor.DataSource;

                GINTruckInformation.Weight.Copy(truckWeight);
            }
            GINProcessWrapper.SaveScaling(GINTruckInformation.TruckId);
            GINProcessWrapper.SaveGIN(GINTruckInformation.TruckId);
        }

        private void Confirm()
        {
            GINProcessWrapper.CompleteScaling(GINTruckInformation.TruckId);
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
        private bool SuspeciousTruckWeight()
        {
            TruckRegistrationInfo truckRegistration = new TruckRegistrationInfo();
            truckRegistration.GINTruck = GINTruckInformation;
            //{
            //    GINTruck = GINTruckInformation
            //};
            return truckRegistration.IsSuspecious;

        }

        protected void btnTruckWeightOk_Click(object sender, EventArgs e)
        {
            mpeSuspeciousTruckWarningExtender.Hide();
            try
            {
                Save();
                if ((string)ViewState["SuspeciousSaveMode"] == "Confirm")
                {
                    Confirm();
                }
            }
            catch (Exception ex)
            {
                errorDisplayer.ShowErrorMessage(ex.Message);
            }
        }

        protected void btnTruckWeightNotOk_Click(object sender, EventArgs e)
        {
            mpeSuspeciousTruckWarningExtender.Hide();
        }
    }

    [Serializable]
    public class ReturnedBagsWrapper
    {
        private ReturnedBagsInfo rbInfo;
        private Guid commodityGradeId;
        private int productionYear;

        public ReturnedBagsWrapper(ReturnedBagsInfo rbInfo, Guid commodityGradeId, int productionYear)
        {
            this.rbInfo = rbInfo;
            this.commodityGradeId = commodityGradeId;
            this.productionYear = productionYear;
        }

        public ReturnedBagsInfo RBInfo
        {
            get { return rbInfo; }
        }

        public int Bags
        {
            get { return rbInfo.Bags; }
            set { rbInfo.Bags = value; }
        }

        public Guid ReturnedBagsId
        {
            get { return rbInfo.ReturnedBagsId; }
            set { rbInfo.ReturnedBagsId = value; }
        }

        public string Shed
        {
            get { return string.Format("{0}_{1}_{2}", rbInfo.Shed, commodityGradeId, productionYear); }
            set
            {
                string[] idPair = value.Split('_');
                rbInfo.Shed = new Guid(idPair[0]);
                commodityGradeId = new Guid(idPair[1]);
                productionYear = int.Parse(idPair[2]);
            }
        }

        public decimal Size
        {
            get { return rbInfo.Size; }
            set { rbInfo.Size = value; }
        }

        public bool Returned
        {
            get { return rbInfo.Returned; }
            set { rbInfo.Returned = value; }
        }

        public Guid StackId
        {
            get { return rbInfo.StackId; }
            set { rbInfo.StackId = value; }
        }

        public Guid TruckId
        {
            get { return rbInfo.TruckId; }
            set { rbInfo.TruckId = value; }
        }
    }
}
